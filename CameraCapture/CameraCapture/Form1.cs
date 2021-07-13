using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AForge;
using AForge.Video;
using AForge.Video.DirectShow;

namespace CameraCapture
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private FilterInfoCollection VideoCaptureDevices;
        private VideoCaptureDevice FinalVideoDevice;

        private void Form1_Load(object sender, EventArgs e)
        {
            VideoCaptureDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            try
{
                foreach (FilterInfo VidCapDev in VideoCaptureDevices)
                {
                    comboBox1.Items.Add(VidCapDev.Name);
                    comboBox1.SelectedIndex = 0;
                }
                FinalVideoDevice = new VideoCaptureDevice(VideoCaptureDevices[comboBox1.SelectedIndex].MonikerString);
                FinalVideoDevice.NewFrame += new NewFrameEventHandler(FinalVideoDevice_NewFrame);
                FinalVideoDevice.Start();
            }
            catch
            {
                MessageBox.Show("No camera found. Please connect your camera and click RESET.");
            }
        }

        void FinalVideoDevice_NewFrame(object sender, NewFrameEventArgs e)
        {
            try
            {
                pictureBox1.Image = (Bitmap)e.Frame.Clone();
            }
            catch { }
        }
    }
}
