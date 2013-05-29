using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Linq;
using System.IO;

namespace DokanMem
{

    internal static class UtilityMethods
    {
        internal static bool Exists(this MemoryItem item)
        {
            return item != null;
        }


        internal static string GetPathPart(this string sourcePath)
        {
            return sourcePath.Substring(0, sourcePath.LastIndexOf('\\'));
        }


        internal static string GetFilenamePart(this string sourcePath)
        {
            return sourcePath.Substring(sourcePath.LastIndexOf('\\') + 1);
        } 

    }
}
