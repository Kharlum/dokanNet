//перегрузка системных функций, необходимо для  работы

using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Win32;
using course.core;
using System.Windows.Forms;
using System.Threading;

namespace course.work
{
    class DokanMemoryStreamOperations : DokanOperations
    {
        public DokanMemoryStreamOperations(RichTextBox _rtb)
        {
           rtb=_rtb;
        }
        RichTextBox rtb;
        
        const string ROOT_FOLDER = "\\"; // корневая директория
        	
        static MemoryFolder _root = new MemoryFolder(null, String.Empty); 

        #region DokanOperations members

        public int Cleanup(string filename, DokanFileInfo info) // удаление
        {
          // string pushTolog  = "delete from "+ filename+"\n";
          //  UtilityMethods.SetTextSafe(rtb, pushTolog);
            
            return DokanNet.DOKAN_SUCCESS;
            

        }

        public int CloseFile(string filename, DokanFileInfo info) //закрытие
        {
           // string pushTolog = "close file " + filename + "\n";
            //UtilityMethods.SetTextSafe(rtb, pushTolog);
           
            return DokanNet.DOKAN_SUCCESS;
        }

        public int CreateDirectory(string filename, DokanFileInfo info)
        {
            // получение родителя где создавать
            string parentFolderPath = (filename).GetPathPart();
            MemoryFolder parentFolder = _root.GetFolderByPath(parentFolderPath);

            if (!parentFolder.Exists())
            {
                string pushTolog = "no path  \n";
                UtilityMethods.SetTextSafe(rtb, pushTolog);
           
                return -DokanNet.ERROR_PATH_NOT_FOUND;
            }

            // проверка имени
            string newName = filename.GetFilenamePart();
            if (newName.IndexOfAny(Path.GetInvalidFileNameChars()) > 0)
            {
                
                string pushTolog = "invalid name for " + filename + "\n";
                UtilityMethods.SetTextSafe(rtb, pushTolog);
                return -DokanNet.ERROR_INVALID_NAME;
            }
            if (string.IsNullOrEmpty(newName))
            { 
                 string pushTolog = " " + filename + "\n";
                 UtilityMethods.SetTextSafe(rtb, pushTolog);
                return -DokanNet.ERROR_INVALID_NAME;
            }
            // если уже сущ
            MemoryFolder testFolder = _root.GetFolderByPath(filename);
            if (testFolder.Exists())
            {
                string pushTolog ="already exist " + filename + "\n";
                UtilityMethods.SetTextSafe(rtb, pushTolog);
                return -DokanNet.ERROR_ALREADY_EXISTS;
            }

            // создание папки
            MemoryFolder newFolder = new MemoryFolder(parentFolder, newName);

            // информирование докана
            if (newFolder.Exists())
            {
                string pushTolog = "directory created " + filename + "\n";
                UtilityMethods.SetTextSafe(rtb, pushTolog);
                return DokanNet.DOKAN_SUCCESS; 
            }
            else {
                string pushTolog = "directory error " + filename + "\n";
                UtilityMethods.SetTextSafe(rtb, pushTolog);
                return DokanNet.DOKAN_ERROR; 
            }
        }

		public int CreateFile(
		    string filename,
		    FileAccess access,
		    FileShare share,
		    FileMode mode,
		    FileOptions options,
		    DokanFileInfo info)
		{
            if (filename == ROOT_FOLDER) //
            {
                //string pushTolog = "created file " + filename + "\n";
                //UtilityMethods.SetTextSafe(rtb, pushTolog);
                return DokanNet.DOKAN_SUCCESS;
            }
		    // получить род папку где создать файл
		    MemoryFolder parentFolder = _root.GetFolderByPath(filename.GetPathPart());
		
		    // если папка не существует
            if (!parentFolder.Exists())
            {
                string pushTolog = "created NO file " + filename + "\n";
                UtilityMethods.SetTextSafe(rtb, pushTolog);
                return -DokanNet.ERROR_PATH_NOT_FOUND;
            }
		    // получение имени файла
		    string newName = filename.GetFilenamePart();
            string newPath = filename.GetPathPart();
		    
		    // проверка имени директории и файла
            if (newName.IndexOfAny(Path.GetInvalidFileNameChars()) > 0)
            {
                string pushTolog = "invalid name " + filename + "\n";
                UtilityMethods.SetTextSafe(rtb, pushTolog);
                return -DokanNet.ERROR_INVALID_NAME;
            }
            if (string.IsNullOrEmpty(newName))
            {
                string pushTolog = "empty file name " + filename + "\n";
                UtilityMethods.SetTextSafe(rtb, pushTolog);
                return -DokanNet.ERROR_INVALID_NAME;
            }
		    // we'll need this file later on;
		    MemoryFile thisFile = (parentFolder.FetchFile(newName));
		
		    // this is called when we should create a new file;
		    // so raise an error if it's a directory;
		    MemoryFolder testFolder = _root.GetFolderByPath(filename);
		    if (testFolder.Exists())
		    {
		        //если это папка
		        info.IsDirectory = true;
		        if (mode == FileMode.Open || mode == FileMode.OpenOrCreate)
		        {
                    //файл это папка;
                    string pushTolog = "created file " + filename + "\n";
                    UtilityMethods.SetTextSafe(rtb, pushTolog);
                    return DokanNet.DOKAN_SUCCESS;
		        }

                string log = "already exists " + filename + "\n";
                UtilityMethods.SetTextSafe(rtb, log);
		        // невозможно содать файл с таким же именем как у папки
		        return -DokanNet.ERROR_ALREADY_EXISTS;
		    }
            
		
		    // если нет папки с таким именем и есть родитель;
		    // пробуем использовать файл
		    switch (mode)
		    {
		        // открыть файл если он сущ или создать новый файл
		        case FileMode.Append:
                    if (!thisFile.Exists())
                    {
                        MemoryFile.New(parentFolder, newName, newPath);
                        string pushTolog = "created file " + filename + "\n";
                        UtilityMethods.SetTextSafe(rtb, pushTolog);
                        return DokanNet.DOKAN_SUCCESS;
                    } break;
		        // определяет что ос должна создать новый файл
		        // если файл сущ перезаписать
		        case FileMode.Create:
                    if (!thisFile.Exists())
                    {
                        MemoryFile.New(parentFolder, newName, newPath);
                        //else
                         string pushTolog1 = "created file " + filename + "\n";
                        UtilityMethods.SetTextSafe(rtb, pushTolog1);
                        return DokanNet.DOKAN_SUCCESS;
                    }
                    break;
                // определяет что ос должна создать новый файл
		        // если файл сущ, исключение IOException
		        case FileMode.CreateNew:
                    if (thisFile.Exists())
                    {
                        String pushTolog = "exists " + filename + "\n";
                        UtilityMethods.SetTextSafe(rtb, pushTolog);
                        return DokanNet.DOKAN_SUCCESS;
                    }
                    else {
		            MemoryFile.New(parentFolder, newName,newPath);
                  //  Console.WriteLine("file is created ", filename);
                    
		            return DokanNet.DOKAN_SUCCESS;
                    } break;

                // определяет что ос должна открыть сущ файл 
		        // System.IO.FileNotFoundException если файл не сущ.
		        case FileMode.Open:
                   if (!thisFile.Exists())
                    {
                        //pushTolog = "file not found " + filename + "\n";
                        //UtilityMethods.SetTextSafe(rtb, pushTolog);
                        return -DokanNet.ERROR_FILE_NOT_FOUND;
                    }
                    else
                    {
                        string pushTolog3 = "created file " + filename + "\n";
                        UtilityMethods.SetTextSafe(rtb, pushTolog3);
                        return DokanNet.DOKAN_SUCCESS;
                   } break;

                // определяет что ос должна открыть сущ файл 
		        // иначе создаем новый файл
		        case FileMode.OpenOrCreate:
                    if (!thisFile.Exists())
                    {
                        MemoryFile.New(parentFolder, newName, newPath);
                        string pushTolog = "created file " + filename + "\n";
                        UtilityMethods.SetTextSafe(rtb, pushTolog);
                        return DokanNet.DOKAN_SUCCESS;
                    } break;

                // определяет что ос должна открыть сущ файл  
		        // открытый файл, обнуляем
		        case FileMode.Truncate:
                    if (!thisFile.Exists())
                    {
                        thisFile = MemoryFile.New(parentFolder, newName, newPath);
                        thisFile.Size = 0;
                        string pushTolog = "created file " + filename + "\n";
                        UtilityMethods.SetTextSafe(rtb, pushTolog);
                        return DokanNet.DOKAN_SUCCESS;
                    } break;
		    }

            string pushTolog2 = "error in creating file " + filename + "\n";
            UtilityMethods.SetTextSafe(rtb, pushTolog2);
		    return DokanNet.DOKAN_ERROR;
		}
		
        public int DeleteDirectory(string filename, DokanFileInfo info)
        {
            // то что удаляем
            MemoryFolder folder = _root.GetFolderByPath(filename);

            if (!folder.Exists())
            {
                string pushTolog = "path no found " + filename + "\n";
                return -DokanNet.ERROR_PATH_NOT_FOUND;
            }
            // отцепить папку от родителя
            folder.Parent.Children.Remove(folder);
            string pushTolog1 = "directoty deleted " + filename + "\n";
            UtilityMethods.SetTextSafe(rtb, pushTolog1);
            Directory.Delete(MemoryFile._root+filename,true);
            return DokanNet.DOKAN_SUCCESS;
        }

        public int DeleteFile(string filename, DokanFileInfo info)
        {
            // получаем род папку
            MemoryFolder parentFolder = _root.GetFolderByPath(
                filename.GetPathPart());


            if (!parentFolder.Exists())
            {
                string pushTolog = "path no found " + filename + "\n";
                UtilityMethods.SetTextSafe(rtb, pushTolog);
                return -DokanNet.ERROR_PATH_NOT_FOUND;
            }
            // найти файл
            MemoryFile file = parentFolder.FetchFile(
                filename.GetFilenamePart());


            if (!file.Exists())
            {
                string pushTolog = "file not found " + filename + "\n";
                UtilityMethods.SetTextSafe(rtb, pushTolog);
                return -DokanNet.ERROR_FILE_NOT_FOUND;
            }
            // удалить файл;
            parentFolder.Children.Remove(file);
            string pushTolog1 = "deleted file  " + filename + "\n";
            UtilityMethods.SetTextSafe(rtb, pushTolog1);
            File.Delete(MemoryFile._root + filename);
            return DokanNet.DOKAN_SUCCESS;
        }

        //очистка буферов
        public int FlushFileBuffers(
            string filename,
            DokanFileInfo info)
        {
           string pushTolog = "!!!!!!!!!!!!!!!!!! \n";
           UtilityMethods.SetTextSafe(rtb, pushTolog);
            return DokanNet.DOKAN_SUCCESS;
        }

        //поиск файла
        public int FindFiles(
            string filename,
            System.Collections.ArrayList files,
            DokanFileInfo info)
        {
               // do we have this folder?
            MemoryFolder folder = filename == ROOT_FOLDER ? _root : _root.GetFolderByPath(filename);
            if (!folder.Exists())
                return -DokanNet.ERROR_FILE_NOT_FOUND;

            // we have this folder, list all it's children;
            foreach (MemoryItem item in folder.Children)
            {
                FileInformation fileinfo = new FileInformation();
                fileinfo.FileName = item.Name;
                fileinfo.Attributes = item.Attributes;
                fileinfo.LastAccessTime = item.LastAccessTime;
                fileinfo.LastWriteTime = item.LastWriteTime;
                fileinfo.CreationTime = item.CreationTime;

                // if it's a file, then also report a size;
                if (item is MemoryFile)
                	fileinfo.Length = (item as MemoryFile).Size;

                files.Add(fileinfo);
            }
            return DokanNet.DOKAN_SUCCESS;
        }

        //получение информации о файле
        public int GetFileInformation(
            string filename,
            FileInformation fileinfo,
            DokanFileInfo info)
        {
            if (filename == ROOT_FOLDER || info.IsDirectory)
            {
                //если это папка
                MemoryFolder folder = (filename == ROOT_FOLDER) ? _root : _root.GetFolderByPath(filename);
                if (!folder.Exists())
                {
                    string pushTolog = "path no found " + filename + "\n";
                    UtilityMethods.SetTextSafe(rtb, pushTolog);
                    return -DokanNet.ERROR_PATH_NOT_FOUND;
                }
                fileinfo.FileName = folder.Name;
                fileinfo.Attributes = folder.Attributes;
                fileinfo.LastAccessTime = folder.LastAccessTime;
                fileinfo.LastWriteTime = folder.LastWriteTime;
                fileinfo.CreationTime = folder.CreationTime;
                rtb.Text += "information shows " + filename + "\n";
                return DokanNet.DOKAN_SUCCESS;
            }
            else
            {
                // если это файл
                string name = filename.GetFilenamePart();
                MemoryFolder parentFolder = _root.GetFolderByPath(filename.GetPathPart());

                // должен быть хотя бы один родитель
                if (!parentFolder.Exists())
                    return DokanNet.DOKAN_ERROR;

                // если файл существует
                MemoryFile file = parentFolder.FetchFile(name);
                if (!file.Exists())
                    return -DokanNet.ERROR_FILE_NOT_FOUND;

                fileinfo.FileName = file.Name;
                fileinfo.Attributes = file.Attributes;
                fileinfo.LastAccessTime = file.LastAccessTime;
                fileinfo.LastWriteTime = file.LastWriteTime;
                fileinfo.CreationTime = file.CreationTime;
                fileinfo.Length = file.Size; 
                return DokanNet.DOKAN_SUCCESS;
            }
        }

        public int LockFile(
            string filename,
            long offset,
            long length,
            DokanFileInfo info)
        {
            return DokanNet.DOKAN_SUCCESS;
        }

        public int MoveFile(
            string filename,
            string newname,
            bool replace,
            DokanFileInfo info)
        {
            string pushTolog = "file move from " + filename + "to" + newname + "\n";
            UtilityMethods.SetTextSafe(rtb, pushTolog);
            // найти нового родителя 
            MemoryFolder newParent = _root.GetFolderByPath(newname.GetPathPart());

            // если он сущ
            if (!newParent.Exists())
                return -DokanNet.ERROR_PATH_NOT_FOUND;

            // проверка что нет папки с именем файла
            MemoryFolder testNewFolder1 = _root.GetFolderByPath(newname);
            if (testNewFolder1.Exists())
            {
                pushTolog = "already exists"+filename+"\n";
                UtilityMethods.SetTextSafe(rtb, pushTolog);
                return -DokanNet.ERROR_ALREADY_EXISTS;
            }
            // проверка что нет файла с таким именем
            MemoryFile testNewFile = newParent.FetchFile(newname.GetFilenamePart());
            if (testNewFile.Exists())
            {
               pushTolog = "already exists no move \n";
               UtilityMethods.SetTextSafe(rtb, pushTolog);
                return -DokanNet.ERROR_FILE_EXISTS;
            }
            // проверка имени
            string newName = newname.GetFilenamePart();
            if (string.IsNullOrEmpty(newName))
            {
                pushTolog = "invalid name no move \n";
                UtilityMethods.SetTextSafe(rtb, pushTolog);
                return -DokanNet.ERROR_INVALID_NAME;
            }
            if (info.IsDirectory)
            {
                // поиск папки которую нужно переименовать
                MemoryFolder sourceFolder = _root.GetFolderByPath(filename);

                if (!sourceFolder.Exists())
                    return -DokanNet.ERROR_FILE_NOT_FOUND;

                // если нашли папку с таким именем переименовываем

                // новый родитель
                sourceFolder.Parent = newParent;

                // переименовываем
                sourceFolder.Name = newName;
                try
                {
                    Directory.Move(MemoryFile._root + filename, MemoryFile._root + newname);
                }
                catch (Exception)
                {
                   
                    pushTolog = "move operation done exception\n";
                    UtilityMethods.SetTextSafe(rtb, pushTolog);
                
                }
                return DokanNet.DOKAN_SUCCESS;
            }
            else
            {
                // если нужно переименовать файл
                string name = filename.GetFilenamePart();

                //найти род директорию файла
                MemoryFolder parentFolder = _root.GetFolderByPath(filename.GetPathPart());
                if (!parentFolder.Exists())
                {
                    string pushTolog1 = "already exists no move \n";
                    UtilityMethods.SetTextSafe(rtb, pushTolog1);
                    return -DokanNet.ERROR_PATH_NOT_FOUND;
                }
                // если есть файл
                MemoryFile thisFile = parentFolder.FetchFile(name);
                if (!thisFile.Exists())
                    return -DokanNet.ERROR_FILE_NOT_FOUND;

                // новый родитель
                thisFile.Parent = newParent;

                // переименовываение файла
                thisFile.Name = newName;

                pushTolog = "move operation done\n";
                UtilityMethods.SetTextSafe(rtb, pushTolog);
                try
                {
                    File.Move(MemoryFile._root + filename, MemoryFile._root + newname);
                }
                catch (Exception e)
                {
                    pushTolog = "move operation done exception\n";
                    UtilityMethods.SetTextSafe(rtb, pushTolog);
                }
                return DokanNet.DOKAN_SUCCESS;
            }
        }

        public int OpenDirectory(string filename, DokanFileInfo info)
        {
            if (filename == ROOT_FOLDER)
                return DokanNet.DOKAN_SUCCESS;

            MemoryFolder testFolder = _root.GetFolderByPath(filename);
            if (!testFolder.Exists())
                return -DokanNet.ERROR_FILE_NOT_FOUND; //#46

            return DokanNet.DOKAN_SUCCESS;
        }

        public int ReadFile(
            string filename,
            byte[] buffer,
            ref uint readBytes,
            long offset,
            DokanFileInfo info)
        {        	
            // поиск папки родителя
            MemoryFolder parentFolder = _root.GetFolderByPath(filename.GetPathPart());

            if (!parentFolder.Exists())
                return -DokanNet.ERROR_PATH_NOT_FOUND;
              
            // получаем файл
            string name = filename.GetFilenamePart();
            MemoryFile file = parentFolder.FetchFile(name);

      
            if (!file.Exists())
                return -DokanNet.ERROR_FILE_NOT_FOUND;

            // чтение
            readBytes = file.Read(offset, buffer);
            return DokanNet.DOKAN_SUCCESS;
        }

        public int WriteFile(string filename, byte[] buffer, ref uint writtenBytes,
             long offset, DokanFileInfo info)
        {
            // поиск родителя
            MemoryFolder parentFolder = _root.GetFolderByPath(filename.GetPathPart());

           
            if (!parentFolder.Exists())
                return -DokanNet.ERROR_PATH_NOT_FOUND;

            // получаем файл
            string name = filename.GetFilenamePart();
            MemoryFile file = parentFolder.FetchFile(name);

          
            if (!file.Exists())
                return -DokanNet.ERROR_FILE_NOT_FOUND;

            //если размер превышает выделенный расширить размер файла
            if (offset + buffer.Length > file.Size)
            	file.Size = offset + buffer.Length;
                        
            // запись
            writtenBytes = file.Write(offset, buffer);
            return DokanNet.DOKAN_SUCCESS;
        }

        public int SetEndOfFile(string filename, long length, DokanFileInfo info)
        {
            // получение родителя
            MemoryFolder parentFolder = _root.GetFolderByPath(filename.GetPathPart());

            
            if (!parentFolder.Exists())
                return -DokanNet.ERROR_PATH_NOT_FOUND;

            // получение файла
            string name = filename.GetFilenamePart();
            MemoryFile file = parentFolder.FetchFile(name);

           
            if (!file.Exists())
                return -DokanNet.ERROR_FILE_NOT_FOUND;

           	file.Size = length;
            return DokanNet.DOKAN_SUCCESS;
        }

        //выделение памяти для системы
        public int SetAllocationSize(string filename, long length, DokanFileInfo info)
        {
            // получить родителя
            MemoryFolder parentFolder = _root.GetFolderByPath(filename.GetPathPart());

         
            if (!parentFolder.Exists())
                return -DokanNet.ERROR_PATH_NOT_FOUND;

            //получение фалйа
            string name = filename.GetFilenamePart();
            MemoryFile file = parentFolder.FetchFile(name);

   
            if (!file.Exists())
                return -DokanNet.ERROR_FILE_NOT_FOUND;

			file.Size = length;
            return DokanNet.DOKAN_SUCCESS;
        }

        public int SetFileAttributes(
            string filename,
            FileAttributes attr,
            DokanFileInfo info)
        {
            return -DokanNet.DOKAN_ERROR;
        }

        public int SetFileTime(
            string filename,
            DateTime ctime,
            DateTime atime,
            DateTime mtime,
            DokanFileInfo info)
        {
            return -DokanNet.DOKAN_ERROR;
        }

        public int UnlockFile(string filename, long offset, long length, DokanFileInfo info)
        {
            return DokanNet.DOKAN_SUCCESS;
        }

        public int Unmount(DokanFileInfo info)
        {
            return DokanNet.DOKAN_SUCCESS;
        }


        // GetDiskFreeSpace вычисляет сколько памяти доступно в файловой системе

        public int GetDiskFreeSpace(
            ref ulong freeBytesAvailable,
            ref ulong totalBytes,
            ref ulong totalFreeBytes,
            DokanFileInfo info)
        {
            totalBytes = (ulong)Environment.WorkingSet + _root.TotalSize;

            // вычисление сколько свободно памяти
            freeBytesAvailable = (ulong)Environment.WorkingSet;
            // ???
            totalFreeBytes = 1000000;

            return DokanNet.DOKAN_SUCCESS;
        }

        #endregion
    }
}
