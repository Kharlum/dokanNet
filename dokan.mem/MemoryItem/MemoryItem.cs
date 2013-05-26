using System;
using System.IO;

namespace course.work
{
    
    class MemoryItem
    {
        MemoryFolder _parent;

        MemoryItem()
        {
        	throw new NotSupportedException();
        }
        
        protected MemoryItem(MemoryFolder parent, string name)
        {
            Parent = parent;
            Name = name;

            CreationTime = DateTime.Now;
            LastAccessTime = DateTime.Now;
            LastWriteTime = DateTime.Now;
        }

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

        public string Name { get; set; }

        public FileAttributes Attributes { get; set; }

        public DateTime LastAccessTime { get; set; }
        public DateTime LastWriteTime { get; set; }
        public DateTime CreationTime { get; set; }

        
    }
}
