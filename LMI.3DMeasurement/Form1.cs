using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HalconDotNet;
using ImageTool.FitLine;


namespace LMI._3DMeasurement
{
    public partial class FormTest : Form
    {
        public FormTest()
        {
            InitializeComponent();
        }

        private void buttonGrab_Click(object sender, EventArgs e)
        {

            hImage.ReadImage(@"C:\test.bmp");
          
            hImage.GetImageSize(out width, out height);
            hWindow.SetPart(0,0,height, width);
            hWindow.DispImage(hImage);

            FitLineTool fitLineTool = new FitLineTool(hImage);
            fitLineTool.CallSettingForm();
          
        }
        HWindow hWindow;
        HTuple winWidth;
        HTuple winHeight;
        HTuple width;
        HTuple height;
        HImage hImage;
        List<HObject> graphicsQueue;
        private void FormTest_Load(object sender, EventArgs e)
        {
            hWindow = hWindowControl1.HalconWindow;
            winWidth = hWindowControl1.Width;
            winHeight = hWindowControl1.Height;
            graphicsQueue = new List<HObject>();
            hImage = new HImage();
        }

        private void buttonDraw_Click(object sender, EventArgs e)
        {
            hWindow.SetColor("red");
            HObject rect = new HObject();
            //HOperatorSet.GenRectangle2ContourXld(out rect, width/ 2-50, height/ 2-50, 0, 100, 100);
            //hWindow.DispObj(rect);
            double row;
            double column;
            double phi;
            double length1;
            double length2;

            hWindow.DrawRectangle2(out row, out column, out phi, out length1, out length2);

            HOperatorSet.GenRectangle2ContourXld(out rect, row, column, phi, length1, length2);
            graphicsQueue.Add(rect);
            GUIDisplay();
        }

        void GUIDisplay()
        {
            hWindow.SetColor("green");
            if (graphicsQueue.Count>0)
            {
                foreach (var item in graphicsQueue)
                {
                    hWindow.DispObj(item);
                }
            }

        }

        void clearDisplay()
        {
            hWindow.ClearWindow();
            graphicsQueue.Clear();
            hImage.GetImageSize(out width, out height);
            hWindow.SetPart(0, 0, height, width);
            hWindow.DispImage(hImage);

        }

        private void buttonClear_Click(object sender, EventArgs e)
        {
            clearDisplay();
        }
    }
}
