using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using course.core;
using System.ComponentModel;
using System.Windows.Forms;

namespace course.work
{
    public class mounting
    {
        static BackgroundWorker _dokanWorker;
        
        // a driveletter where we can mount (we start checking from D: forward)
       

        public static BackgroundWorker mount(RichTextBox rtb ,String mountPoint )
        {
            // Set us up a tray-icon to interact with the user;
            try
            {
                // Set to false if you want to test with the AweBuffers 
                // instead of a MemoryStream


                // run the dokan-task
                _dokanWorker = new BackgroundWorker();
                _dokanWorker.DoWork += delegate
                {
                    DokanOptions options = new DokanOptions
                    {
                        DriveLetter = mountPoint[0],
                        DebugMode = true,
                        UseStdErr = true,
                        NetworkDrive = false,
                        Removable = false,          // provides an "eject"-menu to unmount
                        UseKeepAlive = true,        // auto-unmount
                        ThreadCount = 1, // limit to one thread during debugging!
                        VolumeLabel = "razvashenko"
                    };
                    DokanNet.DokanMain(options, new DokanMemoryStreamOperations(rtb));
                };
                _dokanWorker.RunWorkerAsync();
                _dokanWorker.WorkerSupportsCancellation = true;
            }
            catch (Exception)
            {

                rtb.Text+="Exception erorr/n";
                return _dokanWorker;

            }
            rtb.Text += "mount done\n";
            return _dokanWorker;
        }
    }
}
