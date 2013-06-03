using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;
using course.work;
using course.core;

namespace course.gui
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            
            rtb.Visible = false;
            Directory.CreateDirectory(folder_selector.SelectedPath);
            label3.Text += folder_selector.SelectedPath;
            Size = new Size(Size.Width, 160);
            foreach (char letter in GetAvailableDriveLetter())
            {
                selecter.Items.Add(letter);
            }
            selecter.SelectedIndex = 4;
        }

        private BackgroundWorker worker;

        private void mounter_button_Click(object sender, EventArgs e)
        {
            foreach ( Control contr in this.Controls)
            {
                contr.Visible = false;
            }
            rtb.Visible = true;
            Size = new Size(600, 500);
            rtb.Dock = DockStyle.Fill;
            rtb.Text += "folders and files will contains  "+folder_selector.SelectedPath + "\n";
            rtb.Text += "will mount on virtual disk " + selecter.SelectedItem.ToString()+"\n";
            string mountPoint = selecter.SelectedItem.ToString();
            worker =  mounting.mount(rtb, mountPoint);
            MemoryFile._root = folder_selector.SelectedPath;
        }

        private List<char> GetAvailableDriveLetter()
        {

            var usedDriveLetters =
                from drive
                in DriveInfo.GetDrives()
                select drive.Name.ToUpperInvariant()[0];


            string allDrives = string.Empty;
            for (char c = 'D'; c < 'Z'; c++)
                allDrives += c.ToString();


            var availableDriveLetters = allDrives.Except(usedDriveLetters);
            try
            {
                if (availableDriveLetters.Count() == 0)
                    throw new DriveNotFoundException("No drives available!");
            }
            catch (Exception)
            {
                this.Close();
            }

            return availableDriveLetters.ToList();
        }

        private void button_Folder_selecter_Click(object sender, EventArgs e)
        {
            if (folder_selector.ShowDialog() == DialogResult.OK) { }
               //
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (worker!=null)
            worker.CancelAsync();
            foreach (string direct in Directory.GetDirectories(folder_selector.SelectedPath))
            {
                Directory.Delete(direct,true);
            }
            foreach (string file in Directory.GetFiles(folder_selector.SelectedPath))
                File.Delete(file);
        }

       
    }
}
 