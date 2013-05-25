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
        
       
       

        public static void mount( )
        {
            

                _dokanWorker = new BackgroundWorker();
                _dokanWorker.DoWork += delegate
                {
                    DokanOptions options = new DokanOptions
                    {
                        DriveLetter = 'z',
                        DebugMode = true,
                        UseStdErr = true,
                        NetworkDrive = false,
                        Removable = false,          
                        UseKeepAlive = true,        
                        ThreadCount = 1, 
                        VolumeLabel = "razvashenko"
                    };
                    DokanNet.DokanMain(options, new DokanMemoryStreamOperations());
                };
                _dokanWorker.RunWorkerAsync();
            
           
          

        }
    }
}
