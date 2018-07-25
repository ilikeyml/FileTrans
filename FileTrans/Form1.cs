using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LMI.Sensor;
using HalconDotNet;
using LMI.Utility;
using System.Runtime.InteropServices;
using LMI.PointCloudTool;
using AltSerialize;
using System.IO;
using System.Diagnostics;

namespace FileTrans
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

        }
        GocatorDevice _device = new GocatorDevice("127.0.0.1", 1,true);
        SerializeFileTool<ushort[]> serializeFileTool = new SerializeFileTool<ushort[]>();
        private void Form1_Load(object sender, EventArgs e)
        {
        

        }


        ushort[] rawdata;
        private void buttonGenImage_Click(object sender, EventArgs e)
        {
            //Stopwatch sw = new Stopwatch();
            //sw.Start();
            //altSerializer.Stream = File.OpenRead(@"C:\rawdata.dat");
            //rawdata = (ushort[])altSerializer.Deserialize();
            //rawdata = new ushort[width * height];
            rawdata = _device.PcData;

            int width = (int)_device.Width;
            int height = (int)_device.Height;
            HImage image = new HImage();
       
            int stepLength = Convert.ToInt16(textBox1.Text);
 
            //ushort[] filleddata =  GapFillingTool.SingleDirectionFilling(rawdata, width, height, FillingDirection.X, stepLength, 0, 0);

            //sw.Stop();
            //MessageBox.Show(sw.ElapsedMilliseconds.ToString());
            unsafe
            {
                fixed (ushort* charPointer = &rawdata[0])
                {                  
                    image.GenImage1("uint2", width, height, new System.IntPtr(charPointer));
                }
                
            }

            image.GetImageSize(out width, out height);
            hWindowControl1.HalconWindow.SetPart(0, 0, height, width);
            hWindowControl1.HalconWindow.DispImage(image);
            //image.WriteImage("png", 0, @"C:\hfilled.png");
            //image.WriteImage("png", 0, @"C:\himage.png");
            MessageBox.Show("OK");

        }

        private void button4_Click(object sender, EventArgs e)
        {
            //HImage image = new HImage(@"C:\hfilled.png");
            //HTuple width = new HTuple();
            //HTuple height = new HTuple();
            //image.GetImageSize(out width, out height);
            //hWindowControl1.HalconWindow.SetPart(0, 0, height, width);
            //hWindowControl1.HalconWindow.DispImage(image);

        }

        private void Form1_Load_1(object sender, EventArgs e)
        {
            _device.InitApi();
            _device.Connect();
            _device.Start();


        }
        AltSerialize.AltSerializer altSerializer = new AltSerializer();
        private void button5_Click(object sender, EventArgs e)
        {
            ushort[] rawdata = _device.PcData;

            
            
            MessageBox.Show("OK");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            rawdata = _device.PcData;

            int width = (int)_device.Width;
            int height = (int)_device.Height;
            HImage image = new HImage();

            int stepLength = Convert.ToInt16(textBox1.Text);
            GapFillingTool gapFillingTool = new GapFillingTool();
            //ushort[] filleddata = gapFillingTool.SingleDirectionFilling(rawdata, width, height, FillingDirection.Y, stepLength, 0, 0);


            unsafe
            {
                fixed (ushort* charPointer = &rawdata[0])
                {
                    image.GenImage1("uint2", width, height, new System.IntPtr(charPointer));
                }

            }

            image.GetImageSize(out width, out height);
            hWindowControl1.HalconWindow.SetPart(0, 0, height, width);
            hWindowControl1.HalconWindow.DispImage(image);
            sw.Stop();
            MessageBox.Show(sw.ElapsedMilliseconds.ToString());
        }
    }
}
