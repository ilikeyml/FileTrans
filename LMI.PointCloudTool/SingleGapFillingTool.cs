using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Threading;
using System.ComponentModel;

namespace LMI.PointCloudTool
{

    /// <summary>
    /// 
    /// </summary>
    public enum FillingDirection
    {
        X,
        Y
    }

    class ValidPoint
    {

        int Position;
        ushort value;
        public ValidPoint(int x, ushort value)
        {
            this.Position = x;
            this.value = value;
        }


    

        public int XPosition { get => Position; set => Position = value; }

        public ushort Value { get => value; set => this.value = value; }
    
    }

    public class GapFillingTool
    {

        /// <summary>
        /// 对 行/列单方向上数据进行填充
        /// </summary>
        /// <param name="rawData">原始数据</param>
        /// <param name="width">数据宽度 X方向数据</param>
        /// <param name="height">数据长度 Y方向数据</param>
        /// <param name="fillingDirection">填充方向</param>
        /// <param name="stepLength">填充间隔 在间隔值范围内填充，超过间隔值不进行填充</param>
        /// <param name="threshhold">填充条件阈值</param>
        /// <param name="offset">填充条件阈值偏移 threshold ± offset，未启用</param>
        /// <returns></returns>
        public ushort[] SingleDirectionFilling(ushort[] rawData, long width, long height, FillingDirection fillingDirection, int stepLength, int threshhold, int offset)
        {
            switch (fillingDirection)
            {
                case FillingDirection.X:
                    List<ushort[]> _xdata = XDataList(rawData, (int)width, (int)height);
                    return profileFilling(_xdata, stepLength, threshhold, offset, FillingDirection.X);
                case FillingDirection.Y:
                    List<ushort[]> _ydata = YDataList(rawData, (int)width, (int)height);
                    return profileFilling(_ydata, stepLength, threshhold, offset, FillingDirection.Y);
                    //break;
                default:
                    break;
            }
            return new ushort[0];
        }

        /// <summary>
        /// 行数据分割
        /// </summary>
        /// <param name="rawData"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
         List<ushort[]> XDataList(ushort[] rawData, int width, int height)
        {
            Stopwatch sw = new Stopwatch();
            sw.Start();
            List<ushort[]> _xdata = new List<ushort[]>();
           
            for (int i = 0; i < height; i++)
            {
                ushort[] temp = new ushort[width];
                for (int j = 0; j < width; j++)
                {
                    temp[j] = rawData[j + i * width];
                }
                _xdata.Add(temp);
            }
            sw.Stop();
            long timeconsume = sw.ElapsedMilliseconds;
            return _xdata;
        }
        /// <summary>
        /// 列数据分割
        /// </summary>
        /// <param name="rawData"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
         List<ushort[]> YDataList(ushort[] rawData, int width, int height)
        {

            List<ushort[]> _ydata = new List<ushort[]>();
            List<ushort[]> _xdata = new List<ushort[]>();

            _xdata = XDataList(rawData, width, height);
            for (int k = 0; k < _xdata[0].Length; k++)
            {
                ushort[] subY = new ushort[_xdata.Count];
                for (int i = 0; i < _xdata.Count; i++)
                {
                    subY[i] = _xdata[i][k];
                }
                _ydata.Add(subY);
            }


            return _ydata;
        }



        class ArgsClass
        {
            List<ushort[]> data;
            int stepLength;
            int threshhold;


            public ArgsClass()
            {

            }

            public ArgsClass(List<ushort[]> _data, int _stepLength, int _threshhold)
            {
                this.data = _data;
                this.stepLength = _stepLength;
                this.threshhold = _threshhold;
            }

            public List<ushort[]> Data { get => data; set => data = value; }
            public int StepLength { get => stepLength; set => stepLength = value; }
            public int Threshhold { get => threshhold; set => threshhold = value; }

        }
        /// <summary>
        /// 填充
        /// </summary>
        /// <param name="splitedData"></param>
        /// <param name="stepLength"></param>
        /// <param name="threshhold"></param>
        /// <param name="offset"></param>
        /// <returns></returns>
         ushort[] profileFilling(List<ushort[]> splitedData, int stepLength, int threshhold, int offset, FillingDirection fillingDirection)
        {

            List<ushort[]> filledData = new List<ushort[]>();
            Stopwatch sw = new Stopwatch();
            sw.Start();
            filledData = DoWork(splitedData, stepLength, threshhold);
            sw.Stop();
            long timeconsume = sw.ElapsedMilliseconds;
            return rtnData(filledData, fillingDirection);

        }



        /// <summary>
        /// 转换成ushort array 输出
        /// </summary>
        /// <param name="splitedData"></param>
        /// <param name="fillingDirection"></param>
        /// <returns></returns>
        ushort[] rtnData(List<ushort[]> splitedData, FillingDirection fillingDirection)
        {
            ushort[] filledData = new ushort[splitedData.Count * splitedData[0].Length];
            switch (fillingDirection)
            {
                case FillingDirection.X:
               
                    for (int i = 0; i < splitedData.Count; i++)
                    {
                        for (int j = 0; j < splitedData[0].Length; j++)
                        {
                            filledData[j + splitedData[0].Length * i] = splitedData[i][j];
                        }
                    }
                    
                    break;
                case FillingDirection.Y:

                  
                    for (int i = 0; i < splitedData[0].Length; i++)
                    {
                        for (int j = 0; j < splitedData.Count; j++)
                        {
                            filledData[j + splitedData.Count * i] = splitedData[j][i];
                        }
                    }
                    break;
                default:
                    break;
            }
            return filledData;
        }



        List<ushort[]> DoWork( List<ushort[]> splitedData, int stepLength, int threshhold)
        {
            Parallel.ForEach(splitedData, item =>
            {
                int ignoreArea = 1;//边界忽略
                int count = item.Length;
                List<ValidPoint> pointInfo = new List<ValidPoint>();
                for (int i = ignoreArea; i < count - ignoreArea; i++)
                {
                    //找到所有边界点
                    if (item[i] <= threshhold)
                    {
                        if (item[i - 1] > threshhold)
                        {
                            ValidPoint tempPoint = new ValidPoint(i - 1, item[i - 1]);
                            pointInfo.Add(tempPoint);
                        }
                        if (item[i + 1] > threshhold)
                        {
                            ValidPoint tempPoint = new ValidPoint(i + 1, item[i + 1]);
                            pointInfo.Add(tempPoint);
                        }
                    }
                }

                int index = splitedData.IndexOf(item);

                if (pointInfo.Count > 1)
                {
                    for (int j = 0; j < pointInfo.Count - 1; j++)
                    {
                        //平滑从下降沿点开始

                        if (item[pointInfo[j].XPosition + 1] <= threshhold)
                        {
                            //以上升沿结束
                            if (item[pointInfo[j + 1].XPosition - 1] <= threshhold)
                            {

                                //开始平滑
                                int steplength = pointInfo[j + 1].XPosition - pointInfo[j].XPosition;
                                ushort smoothvalue = (ushort)((pointInfo[j + 1].Value - pointInfo[j].Value) / (double)steplength);
                                if (steplength > 0 && steplength <= stepLength)
                                {
                                    for (int k = 0; k < steplength; k++)
                                    {
                                        item[pointInfo[j].XPosition + k] = (ushort)(pointInfo[j].Value + k * smoothvalue);
                                    }
                                }


                            }
                        }
                    }
                }
            });
            return splitedData;
        }




    } 
    
}
