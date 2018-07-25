using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HalconDotNet;

namespace ImageTool.FitLine
{
    public class ToolSetting
    {
        int calliperNumber = 10;
        double threshold = 40;
        double sigma = 1;
        string transition = "positive";
        string selectType = "first";
        double row = 100;
        double column = 100;
        double length1 = 100;
        double length2 = 100;
        double phi = 0;


        [Category("Params"),Description("Calliper Numbers in ROI"),DefaultValue(10)]
        public int CalliperNumber { get => calliperNumber; set => calliperNumber = value; }
        [Category("Params"),Description("threshhold value"),DefaultValue(40)]
        public double Threshold { get => threshold; set => threshold = value; }
        [Category("Params"),Description("Sigma filter value"),DefaultValue(1)]
        public double Sigma { get => sigma; set => sigma = value; }
        [Category("Params"), Description("polarity setting,all/positive/negative"), DefaultValue("positive")]
        public string Transition { get => transition; set => transition = value; }
        [Category("Params"), Description("data filter,all/first/last"), DefaultValue("last")]
        public string SelectType { get => selectType; set => selectType = value; }
        [Category("Params"), Description("ROI center Row"), DefaultValue(100)]
        public double Row { get => row; set => row = value; }
        [Category("Params"), Description("ROI center Column"), DefaultValue(100)]
        public double Column { get => column; set => column = value; }
        [Category("Params"), Description("ROI Length1"), DefaultValue(100)]
        public double Length1 { get => length1; set => length1 = value; }
        [Category("Params"), Description("ROI Length2"), DefaultValue(100)]
        public double Length2 { get => length2; set => length2 = value; }
        [Category("Params"), Description("ROI Phi"), DefaultValue(100)]
        public double Phi { get => phi; set => phi = value; }
       
    }
}
