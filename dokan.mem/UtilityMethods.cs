using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Linq;
using System.IO;
using System.Windows.Forms;
using System.Collections.Generic;
namespace course.work
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


        internal static void   SetTextSafe(RichTextBox rtb, string newText)
        {
            if (rtb.InvokeRequired) rtb.Invoke(new Action<string>((s) => rtb.Text += s), newText);
            else rtb.Text = newText;
        }

        internal static string getpath(string root, string filename)
        {
            return root + filename;
        }
    }
}
