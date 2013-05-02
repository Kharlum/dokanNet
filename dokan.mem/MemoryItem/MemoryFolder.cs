using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace course.work
{
    /// <summary>
    /// Represents a directory in Memory; may have othe files or folders as
    /// children. 
    /// </summary>
     public class MemoryFolder : MemoryItem
    {
        List<MemoryItem> children = new List<MemoryItem>();

        internal MemoryFolder(MemoryFolder parent, string name)
            : base(parent, name)
        {
            Attributes = FileAttributes.Directory;
        }

        /// <summary>
        /// The MemoryFolder and MemoryFile item-collection
        /// </summary>
        internal List<MemoryItem> Children
        {
            get
            {
                return children;
            }
            set
            {
                if (children != value)
                    children = value;
            }
        }

        internal MemoryFolder GetFolderByPath(string path)
        {
            if (path.Equals(FullPath, StringComparison.OrdinalIgnoreCase)) return this;

            foreach (MemoryFolder folder in 
                from c in children
                where c is MemoryFolder 
                select c)
            {
                MemoryFolder child = folder.GetFolderByPath(path);
                if (child.Exists()) return child;
            }

            return null;
        }

        /// <summary>
        /// Creates a folder (and subfolders) in this MemoryFolder
        /// </summary>
        /// <param name="path">the path to be created</param>
        internal void CreatePath(string path)
        {
            string[] pathParts = path.Split(new char[] { '\\' }, StringSplitOptions.RemoveEmptyEntries);
            if (pathParts.Length > 0)
            {
                MemoryFolder newFolder = null;

                // find it,
                var searchResult = 
                    from c in children
                    where c is MemoryFolder 
                    && c.Name.Equals(pathParts[0], StringComparison.OrdinalIgnoreCase)
                    select (c as MemoryFolder);

                // or create it, if it doesn't exist;
                if (searchResult.Count() > 0) 
                    newFolder = searchResult.First();
                else
                    newFolder = new MemoryFolder(this, pathParts[0]);

                // Create more?
                if (pathParts.Length > 1)
                {
                    string subPath = path.Remove(0, pathParts[0].Length + 1);
                    newFolder.CreatePath(subPath);
                }
            }
        }

        /// <summary>
        /// Returns the size of the files and the files in any subfolders
        /// </summary>
        internal ulong TotalSize
        {
            get
            {
                // the total size of the files in this folder;
                ulong result = (ulong)(
                    from c in children //.AsParallel?
                    where c is MemoryFile
                    select (c as MemoryFile).Size
                    ).Sum();

                // plus size of subfolders;
                foreach (MemoryFolder folder in 
                    from c in children 
                    where c is MemoryFolder
                    select c)
                {
                    result += folder.TotalSize;
                }

                return result;
            }
        }

        /// <summary>
        /// Returns a file from this folder
        /// </summary>
        /// <param name="name">The filename, without path</param>
        /// <returns>A MemoryFile, with info on the file and it's contents, or 
        /// null if it can't be found</returns>
        internal MemoryFile FetchFile(string name)
        {
            var files =
                from c in children 
                where c is MemoryFile && c.Name.Equals(name, StringComparison.OrdinalIgnoreCase)
                select c as MemoryFile;

            return files.Count() > 0 ? files.First() : null;
        }
    }
}
