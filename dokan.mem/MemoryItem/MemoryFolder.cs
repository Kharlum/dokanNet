using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace course.work
{
    class MemoryFolder : MemoryItem
    {
        List<MemoryItem> children = new List<MemoryItem>();

        internal MemoryFolder(MemoryFolder parent, string name)
            : base(parent, name)
        {
            Attributes = FileAttributes.Directory;
        }

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

        

        
    }
}
