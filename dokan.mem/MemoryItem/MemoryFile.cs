using System;
using System.IO;
using Thought.Research;

namespace DokanMem
{
    
    class MemoryFile : MemoryItem
    {
    	MemoryStream _content;

        internal MemoryFile(MemoryFolder parent, string name)
            : base(parent, name)
        {
        	_content = new MemoryStream();
        	
            Attributes = FileAttributes.Normal
                & FileAttributes.NotContentIndexed;
        }

        internal override long Size
        {
        	get { return _content.Length; }
			set 
			{ 				
				if (_content.Length != value)
				{
					_content.SetLength(value);
				}				
			}
        }
        
        internal override uint Write(long offset, byte[] buffer)
        {
            Stream writeStream = _content;
            writeStream.Seek(offset, SeekOrigin.Begin);
            writeStream.Write(buffer, 0, buffer.Length);
            return (uint)buffer.Length;
        }
        
        internal override uint Read(long offset, byte[] buffer)
        {
        	Stream readStream = _content;
            readStream.Seek(offset, SeekOrigin.Begin);
            return (uint)readStream.Read(buffer, 0, buffer.Length);
        }
    }
}
