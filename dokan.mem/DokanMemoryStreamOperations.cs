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
       
        
        const string ROOT_FOLDER = "\\"; // корневая директория
        	

        #region DokanOperations members

        public int Cleanup(string filename, DokanFileInfo info) // удаление
        {
           
            return DokanNet.DOKAN_SUCCESS;
          
        }

        public int CloseFile(string filename, DokanFileInfo info) //закрытие
        {
            return DokanNet.DOKAN_SUCCESS;
        }

        public int CreateDirectory(string filename, DokanFileInfo info)
        {
			return DokanNet.DOKAN_SUCCESS;
	    }

		public int CreateFile(
		    string filename,
		    FileAccess access,
		    FileShare share,
		    FileMode mode,
		    FileOptions options,
		    DokanFileInfo info)
		{
               return DokanNet.DOKAN_SUCCESS;;
		}



           

        public int DeleteDirectory(string filename, DokanFileInfo info)
        {
            return DokanNet.DOKAN_SUCCESS;
        }

        public int DeleteFile(string filename, DokanFileInfo info)
        {
            return DokanNet.DOKAN_SUCCESS;
        }

        //очистка буферов
        public int FlushFileBuffers(
            string filename,
            DokanFileInfo info)
        {
            return DokanNet.DOKAN_SUCCESS;
        }

        //поиск файла
        public int FindFiles(
            string filename,
            System.Collections.ArrayList files,
            DokanFileInfo info)
        {
            return DokanNet.DOKAN_SUCCESS;
        }

        //получение информации о файле
        public int GetFileInformation(
            string filename,
            FileInformation fileinfo,
            DokanFileInfo info)
        {
            return DokanNet.DOKAN_SUCCESS;
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
               return DokanNet.DOKAN_SUCCESS;
            
        }

        public int OpenDirectory(string filename, DokanFileInfo info)
        {
            return DokanNet.DOKAN_SUCCESS;
        }

        public int ReadFile(
            string filename,
            byte[] buffer,
            ref uint readBytes,
            long offset,
            DokanFileInfo info)
        {        	
            return DokanNet.DOKAN_SUCCESS;
        }

        public int WriteFile(string filename, byte[] buffer, ref uint writtenBytes,
             long offset, DokanFileInfo info)
        {
            return DokanNet.DOKAN_SUCCESS;
        }

        public int SetEndOfFile(string filename, long length, DokanFileInfo info)
        {
            return DokanNet.DOKAN_SUCCESS;
        }

        //выделение памяти для системы
        public int SetAllocationSize(string filename, long length, DokanFileInfo info)
        {
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
            totalBytes = (ulong)Environment.WorkingSet;

            // вычисление сколько свободно памяти
            freeBytesAvailable = totalBytes - _root.TotalSize;
            // ???
            totalFreeBytes = int.MaxValue;

            return DokanNet.DOKAN_SUCCESS;
        }

        #endregion
    }
	}
}