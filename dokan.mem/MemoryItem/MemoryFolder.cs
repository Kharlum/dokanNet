using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace DokanMem
{
    //Отображает директорию в памяти; может иметь наследников (файлы и папки)
    class MemoryFolder : MemoryItem
    {
        List<MemoryItem> children = new List<MemoryItem>();

        internal MemoryFolder(MemoryFolder parent, string name)
            : base(parent, name)
        {
            Attributes = FileAttributes.Directory;
        }

        /// The MemoryFolder and MemoryFile item-collection
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


        // создает папку и подпапки в MemoryFolder
        internal void CreatePath(string path)
        {
            string[] pathParts = path.Split(new char[] { '\\' }, StringSplitOptions.RemoveEmptyEntries);
            if (pathParts.Length > 0)
            {
                MemoryFolder newFolder = null;

                // найти ее
                var searchResult = 
                    from c in children
                    where c is MemoryFolder 
                    && c.Name.Equals(pathParts[0], StringComparison.OrdinalIgnoreCase)
                    select (c as MemoryFolder);

                // или создать если не сущ
                if (searchResult.Count() > 0) 
                    newFolder = searchResult.First();
                else
                    newFolder = new MemoryFolder(this, pathParts[0]);

                // если нужно создать еще
                if (pathParts.Length > 1)
                {
                    string subPath = path.Remove(0, pathParts[0].Length + 1);
                    newFolder.CreatePath(subPath);
                }
            }
        }


        // возвращает размер файлов и файлы в поддиректориях
        internal ulong TotalSize
        {
            get
            {
                // общий размер файлов в данной папке
                ulong result = (ulong)(
                    from c in children //.AsParallel?
                    where c is MemoryFile
                    select (c as MemoryFile).Size
                    ).Sum();

                // + размер подпапок 
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

        
    }
}
