
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMI.Utility
{
    /// <summary>
    /// Point Data Struct 
    /// </summary>
    public struct MyPointStruct
    {
        public string m_pointName;
        public double m_dRow;
        public double m_dColumn;
        public double m_dDiff;
    }


    /// <summary>
    /// CSV Data Save And Transfer
    /// </summary>
    public class CSVTool
    {
        /// <summary>
        /// CSV File Data To List
        /// </summary>
        /// <param name="path"></param>
        /// <param name="ListStr"></param>
        public static void CSV2List(String path, out List<MyPointStruct> ListStr)
        {
            MyPointStruct temp = new MyPointStruct();
            DataTable datatable = new DataTable();
            ListStr = new List<MyPointStruct>();

            OpenCSVFile(ref datatable, path);
            try
            {
                for (int i = 1; i < datatable.Rows.Count; i++)
                {
                    temp.m_pointName = datatable.Rows[i][0].ToString();
                    temp.m_dRow = Convert.ToDouble(datatable.Rows[i][1]);
                    temp.m_dColumn = Convert.ToDouble(datatable.Rows[i][2]);
                    temp.m_dDiff = 0;
                    ListStr.Add(temp);
                }
            }
            catch (System.Exception ex)
            {
                //MessageBox.Show("CSV2List Fail！Error：" + ex.Message);
            }

        }

        /// <summary>
        /// CSV File Data To DataTable
        /// </summary>
        /// <param name="mycsvdt"></param>
        /// <param name="filepath"></param>
        /// <returns></returns>
        public static bool OpenCSVFile(ref DataTable mycsvdt, string filepath)
        {
            string strpath = filepath; 
            try
            {
                int intColCount = 0;
                bool blnFlag = true;

                DataColumn mydc;
                DataRow mydr;

                string strline;
                string[] aryline;
                StreamReader mysr = new StreamReader(strpath, System.Text.Encoding.Default);

                while ((strline = mysr.ReadLine()) != null)
                {
                    aryline = strline.Split(new char[] { ',' });
                    if (blnFlag)
                    {
                        blnFlag = false;
                        intColCount = aryline.Length;
                        int col = 0;
                        for (int i = 0; i < aryline.Length; i++)
                        {
                            col = i + 1;
                            mydc = new DataColumn(col.ToString());
                            mycsvdt.Columns.Add(mydc);
                        }
                    }
                    mydr = mycsvdt.NewRow();
                    for (int i = 0; i < intColCount; i++)
                    {
                        mydr[i] = aryline[i];
                    }
                    mycsvdt.Rows.Add(mydr);
                }
                mysr.Close();
                return true;

            }
            catch (Exception e)
            {
                Trace.TraceError("{0}-Error happened during Get data from CSV, ex:{1}", filepath, e.Message);
                return false;
            }
        }
    }
}
