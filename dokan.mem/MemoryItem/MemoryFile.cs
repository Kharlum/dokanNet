using System;
using System.IO;

namespace course.work
{
    
   public class MemoryFile : MemoryItem
    {
    	//private FileStream _content;
       private long size;
        public static string _root = "";
        private string _path;

        internal MemoryFile(MemoryFolder parent, string name,string path)
            : base(parent, name)
        {
            //_content = createStream(Name, path);
            _path = path;
            Attributes = FileAttributes.Normal
                & FileAttributes.NotContentIndexed;
        }

        internal  long Size
        {
            get { return size; }
			set 
			{ 				
				if (size != value)
				{
					size = value;
				}				
			}
        }
        
        internal uint Write(long offset, byte[] buffer)
        {
            FileStream _content = createStream(Name, _path);
            _content.SetLength(size);
            _content.Seek(offset, SeekOrigin.Begin);
            _content.Write(buffer, 0, buffer.Length);
             uint writed = (uint) buffer.Length;
            _content.Close();
            return writed;
        }
        
        internal  uint Read(long offset, byte[] buffer)
        {
            FileStream _content = createStream(Name, _path);
            _content.Seek(offset, SeekOrigin.Begin);
              uint read = (uint) _content.Read(buffer, 0, buffer.Length);
              size = _content.Length;
            _content.Close();
            return read;
        }

        internal static MemoryFile New(MemoryFolder parent, string name,string path)
        {
            return new MemoryFile(parent, name,path);
        }

        internal static FileStream createStream(string Name, string path)
        {
            string allpath;
            if (string.IsNullOrEmpty(path))
                allpath = _root + "\\" + Name;
            else
                allpath = _root + path + "\\" + Name;
            string allpathfolder = UtilityMethods.GetPathPart(allpath);
            string allpathfile = UtilityMethods.GetFilenamePart(allpath);
            Directory.CreateDirectory(allpathfolder);
            return new FileStream(allpath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
        }
    }
}
