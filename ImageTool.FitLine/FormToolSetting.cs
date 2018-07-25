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
using LMI.Utility;
using AltSerialize;
using System.IO;
using LMI.GeneralLib;

namespace ImageTool.FitLine
{
    public partial class FormToolSetting : Form
    {
        public FormToolSetting()
        {
            InitializeComponent();
        }
        readonly string filePath = @"C:\LMIVision\ConfigFile\FitLine_01.xml";
        HImage _image = new HImage();
        public FormToolSetting(HImage image)
        {
            this.Image = image;
            InitializeComponent();
        }
        ToolSetting toolSetting;
        SerializeFileTool<ToolSetting> serializeFileTool = new SerializeFileTool<ToolSetting>();
        AltSerialize.AltSerializer altSerializer = new AltSerializer();
        HWindow hWindow;
        HTuple width;
        HTuple height;
        List<HObject> graphicsQueue = new List<HObject>();
        HTuple winID;
        public HImage Image { get => _image; set => _image = value; }

        HPointGroup pointGroup = new HPointGroup();

        private void FormToolSetting_Load(object sender, EventArgs e)
        {
            toolSetting = new ToolSetting();
            if (!File.Exists(filePath))
            {
                MessageBox.Show("Setting File not exist,default setting will be used");
            }
            else
            {
                toolSetting = serializeFileTool.GetConfig(filePath);
            }
        
            propertyGrid1.PropertySort = PropertySort.NoSort;
            propertyGrid1.SelectedObject = toolSetting;
            hWindow = hWindowControl1.HalconWindow;
            winID = hWindowControl1.HalconID;
            this.Image.GetImageSize(out width, out height);
            hWindow.SetPart(0, 0, height, width);
            hWindow.DispImage(this.Image);
        }

        private void buttonLoad_Click(object sender, EventArgs e)
        {
            toolSetting = serializeFileTool.GetConfig(filePath);

            MessageBox.Show("Setting Loaded");
        }

        private void buttonROI_Click(object sender, EventArgs e)
        {
            hWindow.SetDraw("margin");
            HObject rectROI = new HObject();
            HRegion hRegion = new HRegion((double)100, 100, 200, 200);
            HTuple rowOut;
            HTuple columnOut;
            HTuple phiOut;
            HTuple length1Out;
            HTuple length2Out;
            hWindow.SetColor("blue");
            HOperatorSet.DrawRectangle2Mod(winID, toolSetting.Row, toolSetting.Column, toolSetting.Phi, toolSetting.Length1,
                toolSetting.Length2, out rowOut, out columnOut, out phiOut, out length1Out, out length2Out);
            HOperatorSet.GenRectangle2(out rectROI, rowOut, columnOut, phiOut, length1Out, length2Out);

            toolSetting.Row = rowOut.D;
            toolSetting.Column = columnOut.D;
            toolSetting.Phi = phiOut.D;
            toolSetting.Length1 = length1Out.D;
            toolSetting.Length2 = length2Out.D;
            serializeFileTool.SetConfig(filePath, toolSetting);
            propertyGrid1.SelectedObject = toolSetting;
            hWindow.SetColor("green");
            hWindow.DispObj(rectROI);







        }

        private void buttonSave_Click(object sender, EventArgs e)
        {

            serializeFileTool.SetConfig(filePath, toolSetting);


            MessageBox.Show("Setting Saved");
        }

        private void buttonRun_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
            HTuple result = Run();

            int count = result.Length;

            HTuple row = result[0];
            HTuple column = result[1];
            HTuple amplitude = result[2];
            string resultString = "row:" + row.D.ToString() + Environment.NewLine;
            resultString += "Column:" + column.D.ToString() + Environment.NewLine;
            resultString += "Amplitude:" + amplitude.D.ToString();
            richTextBox1.AppendText(resultString);
            HObject pointSelected = new HObject();
            HOperatorSet.GenCircle(out pointSelected, row, column, 2);
            pointGroup.RowGroup.Append(row);
            pointGroup.ColumnGroup.Append(column);
            graphicsQueue.Add(pointSelected);
            if (pointGroup.Count == toolSetting.CalliperNumber)
            {
                FitLine(pointGroup);
            }
    
            refreshDisplay();
          
        }

        void GUIDisplay()
        {
            hWindow.SetColor("green");
            if (graphicsQueue.Count > 0)
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
            Image.GetImageSize(out width, out height);
            hWindow.SetPart(0, 0, height, width);
            hWindow.DispImage(Image);

        }

        void refreshDisplay()
        {
            hWindow.ClearWindow();
            Image.GetImageSize(out width, out height);
            hWindow.SetPart(0, 0, height, width);
            hWindow.DispImage(Image);
            GUIDisplay();
        }


        HTuple Run()
        {

            HTuple row = toolSetting.Row;
            HTuple column = toolSetting.Column;
            HTuple phi = toolSetting.Phi;
            HTuple length1 = toolSetting.Length1;
            HTuple length2 = toolSetting.Length2;
            HTuple measureHandle = new HTuple();
            HTuple rowEdges = new HTuple();
            HTuple columnEdges = new HTuple();
            HTuple amplitude = new HTuple();
            HTuple distance = new HTuple();

            HOperatorSet.GenMeasureRectangle2(row, column, phi, length1, length2, width, height, "nearest_neighbor", out measureHandle);
            HOperatorSet.MeasurePos(this.Image, measureHandle, toolSetting.Sigma,
                toolSetting.Threshold, toolSetting.Transition, toolSetting.SelectType,
                out rowEdges, out columnEdges, out amplitude, out distance);
            HTuple result = new HTuple();
            result.Append(rowEdges);
            result.Append(columnEdges);
            result.Append(amplitude);
            result.Append(distance);
            HOperatorSet.CloseMeasure(measureHandle);
            return result;
        }


        void FitLine(HPointGroup pointGroup)
        {


            HTuple startRow;
            HTuple endRow;
            HTuple startColumn;
            HTuple endColumn;
            HTuple nr;
            HTuple nc;
            HTuple dist;
            int count = pointGroup.Count;
            HObject lineContour = new HObject();
            HOperatorSet.GenContourPolygonXld(out lineContour, pointGroup.RowGroup, pointGroup.ColumnGroup);
            HOperatorSet.FitLineContourXld(lineContour, "tukey", -1, 0, 5, 2, out startRow, out startColumn, out endRow, out endColumn, out nr, out nc, out dist);
            HObject lineObj = new HObject();
            HOperatorSet.GenRegionLine(out lineObj, startRow, startColumn, endRow, endColumn);

            graphicsQueue.Add(lineObj);
        }

       HTuple GenMeasureROI(int CalipperNumber)
        {
            HTuple row = toolSetting.Row;
            HTuple column = toolSetting.Column;
            HTuple phi = toolSetting.Phi;
            HTuple length1 = toolSetting.Length1;
            HTuple length2 = toolSetting.Length2;
            HTuple measureHandle = new HTuple();
            HTuple rowEdges = new HTuple();
            HTuple columnEdges = new HTuple();
            HTuple amplitude = new HTuple();
            HTuple distance = new HTuple();

            HOperatorSet.GenMeasureRectangle2(row, column, phi, length1, length2, width, height, "nearest_neighbor", out measureHandle);
            HOperatorSet.MeasurePos(this.Image, measureHandle, toolSetting.Sigma,
                toolSetting.Threshold, toolSetting.Transition, toolSetting.SelectType,
                out rowEdges, out columnEdges, out amplitude, out distance);
            HTuple result = new HTuple();
            result.Append(rowEdges);
            result.Append(columnEdges);
            result.Append(amplitude);
            result.Append(distance);
            return result;
        }
    }
}
