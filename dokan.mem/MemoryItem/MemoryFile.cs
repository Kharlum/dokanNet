using System;
using System.IO;

namespace course.work
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

        internal  long Size
        {
        	get { return _content.Length; }
			set 
			{ 				
				//		
			}
        }
        
        internal uint Write(long offset, byte[] buffer)
        {
            //
        }
        
        internal  uint Read(long offset, byte[] buffer)
        {
        	//
        }

       
    }
}
