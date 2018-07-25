using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HalconDotNet;
using LMI.Utility;

namespace ImageTool.FitLine
{
    public class FitLineTool
    {

        ToolSetting toolSetting = new ToolSetting();
        SerializeFileTool<ToolSetting> serializeFileTool = new SerializeFileTool<ToolSetting>();
        readonly string filePath = @"C:\LMIVision\ConfigFile\FitLine_01.xml";
        public FitLineTool()
        {

        }


        public FitLineTool(HImage _image)
        {
            this.HImage = _image;
        }


        HImage hImage = new HImage();

        public HImage HImage { get => hImage; set => hImage = value; }

        public void CallSettingForm()
        {
            FormToolSetting formToolSetting = new FormToolSetting(this.hImage);
            formToolSetting.Show();
        }


        public void toolRun()
        {
           

        }

        void LoadSetting()
        {
            toolSetting = serializeFileTool.GetConfig(filePath);
        }

        void SplitMeasureROI(int CalipperNumber)
        {
            HTuple row = toolSetting.Row;
            HTuple column = toolSetting.Column;
            HTuple phi = toolSetting.Phi;
            HTuple length1 = toolSetting.Length1;
            HTuple length2 = toolSetting.Length2;




        }






    }
}
