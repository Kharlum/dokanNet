using System;
using System.IO;

namespace course.work
{
    /// <summary>
    /// Base class;
    /// Every folder or file in memory gets derived from this thing
    /// </summary>
    public class MemoryItem
    {
        MemoryFolder _parent;

        MemoryItem()
        {
        	throw new NotSupportedException();
        }
        
        public MemoryItem(MemoryFolder parent, string name)
        {
            Parent = parent;
            Name = name;

            CreationTime = DateTime.Now;
            LastAccessTime = DateTime.Now;
            LastWriteTime = DateTime.Now;
        }

        /// <summary>
        /// This is a reference to the parent-folder, where this item is located in
        /// </summary>
        public MemoryFolder Parent
        {
            get
            {
                return _parent;
            }
            set
            {
                if (_parent != value)
                {
                    if (_parent != null) _parent.Children.Remove(this);
                    if (value != null) value.Children.Add(this);

                    _parent = value;
                }
            }
        }

        /// <summary>
        /// This is the name of the item (file or folder), without a path
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The represent the attributes of the item;
        /// flags indicating whether it's a readonly/hidden
        /// </summary>
        public FileAttributes Attributes { get; set; }

        //
        // These represent the filedates;        
        public DateTime LastAccessTime { get; set; }
        public DateTime LastWriteTime { get; set; }
        public DateTime CreationTime { get; set; }

        /// <summary>
        /// Returns the full path to the memory-item
        /// </summary>
        public string FullPath
        {
            get
            {
                if (_parent == null)
                    return Name;
                else
                    return _parent.FullPath + "\\" + Name;
            }
        }
    }
}
