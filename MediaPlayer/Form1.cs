using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MediaPlayer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void axWindowsMediaPlayer1_Enter(object sender, EventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //open dialog and select a file
            Stream myStream = null;
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = " MP3 Audio File (*.mp3)|*.mp3|Audio Video Interleave (*.avi)|*.avi|Windows Media File (*.wma)|*.wma|WAV Audio File  (*.wav)|*.wav|All files (*.*)|*.*";
            dialog.FilterIndex = 0;
            dialog.RestoreDirectory = false;
            dialog.Multiselect = true;

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if ((myStream = dialog.OpenFile()) != null)
                    {
                        using (myStream)
                        {
                            string[] fileNameandPath = dialog.FileNames;
                            string[] filename = dialog.SafeFileNames;

                            for (int i = 0; i < dialog.SafeFileNames.Count(); i++)
                            {
                                string[] saveItem = new string[2];

                                saveItem[0] = filename[i];
                                saveItem[1] = fileNameandPath[i];

                                ListViewItem lvi = new ListViewItem(saveItem);

                                listView1.Items.Add(lvi);
                            }
                        }
                    }
                }

                catch (Exception ex)
                {
                    MessageBox.Show("Error Couldn't find file on disk");
                }
            }
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {

        }


        private void button2_Click(object sender, EventArgs e)
        {
            WMPLib.IWMPPlaylist playlist = axWindowsMediaPlayer1.playlistCollection.newPlaylist("myplaylist");
            WMPLib.IWMPMedia media;

            for (int i = 0; i < listView1.Items.Count; i++)
            {

                int ii = 1;
                media = axWindowsMediaPlayer1.newMedia(listView1.Items[i].SubItems[ii].Text);
                playlist.appendItem(media);
                ii++;
                axWindowsMediaPlayer1.currentPlaylist = playlist;
                axWindowsMediaPlayer1.Ctlcontrols.play();
            }
        }


        //double click on a song for play
        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            string selectedFile = listView1.FocusedItem.SubItems[1].Text;
            
            axWindowsMediaPlayer1.URL = @selectedFile;                        
        }

        private void button3_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
        }        
    }
}