using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace MediaPlayer
{
    public partial class PlayerForm : Form
    {
        public PlayerForm()
        {
            InitializeComponent();
        }

        private void LoadFiles()
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

                                lvItems.Items.Add(lvi);
                            }
                        }
                    }
                }

                catch (IOException ex)
                {
                    MessageBox.Show("Error Couldn't find file on disk");
                }
            }
        }

        private void PlayAllItems()
        {
            WMPLib.IWMPPlaylist playlist = axWindowsMediaPlayer1.playlistCollection.newPlaylist("myplaylist");
            WMPLib.IWMPMedia media;

            for (int i = 0; i < lvItems.Items.Count; i++)
            {
                int ii = 1;
                media = axWindowsMediaPlayer1.newMedia(lvItems.Items[i].SubItems[ii].Text);
                playlist.appendItem(media);
                ii++;
                axWindowsMediaPlayer1.currentPlaylist = playlist;
                axWindowsMediaPlayer1.Ctlcontrols.play();
            }
        }


        //double click on a song for play
        private void lvItems_DoubleClick(object sender, EventArgs e)
        {
            string selectedFile = lvItems.FocusedItem.SubItems[1].Text;

            axWindowsMediaPlayer1.URL = @selectedFile;
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            LoadFiles();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            lvItems.Items.Clear();
        }

        private void btnPlay_click(object sender, EventArgs e)
        {
            PlayAllItems();
        }
    }
}