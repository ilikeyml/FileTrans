using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LMI.Sensor
{
    public partial class InfoForm : Form
    {
        public InfoForm()
        {
            InitializeComponent();
        }


        public void UpdateInfo(string info)
        {
            richTextBox1.AppendText(info + Environment.NewLine);
        }
    }
}
