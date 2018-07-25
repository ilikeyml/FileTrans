using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lmi3d.GoSdk;
using Lmi3d.GoSdk.Messages;
using Lmi3d.GoSdk.Outputs;
using Lmi3d.Zen;
using Lmi3d.Zen.Io;
using Lmi3d.GoSdk.Tools;
using System.Threading;
using System.Windows.Forms;
using LMI.Utility;
using LMI.Data;
using System.IO;
using System.Runtime.InteropServices;

namespace LMI.Sensor
{
    public enum TriggerType
    {
        TimeMode,
        EncoderMode
    }


    public class GocatorDevice
    {

        List<MeasurementsStuct> ResultList = new List<MeasurementsStuct>();

        GoSystem system;
        GoSensor sensor;
        List<KObject> dataList;
        int QueueSize = 1;
        string sensor_IP_Addr = "127.0.0.1";
        InfoForm infoForm;
        private ushort[] pcData;
        long width;
        long height;

        /// <summary>
        /// 
        /// </summary>
        public ListenEventCreater ResultEventCreater { get; set; }
        public ushort[] PcData { get => pcData; set => pcData = value; }
        public long Width { get => width; set => width = value; }
        public long Height { get => height; set => height = value; }

        public GocatorDevice(string ipAddr, int bufferSize, bool bShowMsg)
        {
            infoForm = new InfoForm();
            this.sensor_IP_Addr = ipAddr;
            this.QueueSize = bufferSize;
            this.dataList = new List<KObject>();

            if (bShowMsg)
            {
                infoForm.Show();
            }
            
        }

        public bool InitApi()
        {

            KApiLib.Construct();
            GoSdkLib.Construct();
            system = new GoSystem();
            this.infoForm.Invoke((MethodInvoker)delegate
            {
                this.infoForm.UpdateInfo("Init API");
            });
            return true;
        }

        public bool Connect()
        {
            KIpAddress ipAddress = KIpAddress.Parse(sensor_IP_Addr);
            GoDataSet dataSet = new GoDataSet();
            sensor = system.FindSensorByIpAddress(ipAddress);
            sensor.Connect();

            this.infoForm.Invoke((MethodInvoker)delegate
            {
                this.infoForm.UpdateInfo("Connected");

            });
            return true;
        }

        public bool Start()
        {

            system.EnableData(true);
            system.SetDataHandler(OnData);
            this.infoForm.Invoke((MethodInvoker)delegate
            {
                this.infoForm.UpdateInfo("Data tunnel opened");
            });
            system.Start();
            this.infoForm.Invoke((MethodInvoker)delegate
            {
                this.infoForm.UpdateInfo("System Start");
                    
            });
            return true;
        }

        public bool Stop()
        {
            if (sensor != null)
            {
                sensor.Stop();
                this.infoForm.Invoke((MethodInvoker)delegate
                {
                    this.infoForm.UpdateInfo("System stop");

                });
            }
            return true;
        }

        public bool Disconnect()
        {

            system.Disconnect();
            this.infoForm.Invoke((MethodInvoker)delegate
            {
                this.infoForm.UpdateInfo("System Disconnected");

            });
            return true;
        }




        /// <summary>
        /// Make sure stop sensor before calling this function
        /// </summary>
        /// <param name="triggerType"></param>
        public void SwitchTriggerMode(TriggerType triggerType, double fixedLength)
        {

            GoSetup goSetup;
            goSetup = sensor.Setup;
            //public const int Time = 0;
            //public const int Encoder = 1;
            //public const int DigitalInput = 2;
            //public const int Software = 3;
            switch (triggerType)
            {
                case TriggerType.TimeMode:
                    goSetup.TriggerSource = GoTrigger.Time;
                    goSetup.GetSurfaceGeneration().FixedLengthLength = fixedLength;
                    goSetup.FrameRate = 5;
                    SaveJob();                    
                    break;
                case TriggerType.EncoderMode:

                    goSetup.TriggerSource = GoTrigger.Encoder;
                    goSetup.GetSurfaceGeneration().FixedLengthLength = fixedLength;
                    SaveJob();
                    break;
                default:
                    break;
            }
        }


        public void SaveJob()
        {
            sensor.Flush();
            string curJob = "";
            bool bFlag = false;
            sensor.LoadedJob(ref curJob, ref bFlag);
            sensor.CopyFile("_live.job", curJob);
            sensor.DefaultJob = curJob;
        }
        Thread trd;
        private void OnData(KObject kObject)
        {
            this.dataList.Add(kObject);
            this.infoForm.Invoke((MethodInvoker)delegate
            {
                this.infoForm.UpdateInfo("data received");

            });
            if (dataList.Count == QueueSize)
            {
                trd = new Thread(new ThreadStart(ResolveData));
                trd.Start();
                this.infoForm.Invoke((MethodInvoker)delegate
                {
                    this.infoForm.UpdateInfo("Resolving data started");

                });
            }

        }

        void ResolveData()

        {
            if (this.dataList.Count > 0)
            {
                foreach (var item in this.dataList)
                {

                    DoData(item);
                }

                //ResultEventCreater.ChangeValue(new EventData(ResultList));
                this.infoForm.Invoke((MethodInvoker)delegate
                {
                    this.infoForm.UpdateInfo("Resolving data stopped");

                });
            }
        }


        void SaveData(object obj)
        {
            SerializeFileTool<object> serializeFileTool = new SerializeFileTool<object>();
            string timeStamp = DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss_fff");
            string fileName = timeStamp + ".dat";
            
        }

        private void DoData(KObject data)
        {
            GoDataSet dataSet = (GoDataSet)data;
            for (UInt32 i = 0; i < dataSet.Count; i++)
            {           
                GoDataMsg dataObj = (GoDataMsg)dataSet.Get(i);
                switch (dataObj.MessageType)
                {
                    case GoDataMessageType.Measurement:
                        {
                            GoMeasurementMsg measurementMsg = (GoMeasurementMsg)dataObj;
                            for (UInt32 k = 0; k < measurementMsg.Count; ++k)
                            {
                                GoMeasurementData measurementData = measurementMsg.Get(k);
                                this.infoForm.Invoke((MethodInvoker)delegate
                                {
                                    this.infoForm.UpdateInfo(measurementMsg.Id.ToString());
                                    this.infoForm.UpdateInfo(measurementData.Value.ToString());
                                    this.infoForm.UpdateInfo(measurementData.Decision.ToString());
                                    MeasurementsStuct measurementsStuct = new MeasurementsStuct(measurementMsg.Id, measurementData.Value);
                                    ResultList.Add(measurementsStuct);
                                });
                            }
                        }
                        break;

                    case GoDataMessageType.Surface:
                        {
                            GoSurfaceMsg surfaceMsg = (GoSurfaceMsg)dataObj;
                            long width = surfaceMsg.Width;
                            long height = surfaceMsg.Length;
                            long bufferSize = width * height;
                            IntPtr bufferPointer = surfaceMsg.Data;
                            this.Width = width;
                            this.Height = height;
                            this.PcData = new ushort[bufferSize];
                            short[] range = new short[bufferSize];
                            Marshal.Copy(bufferPointer, range, 0, PcData.Length);

                            Parallel.For(0, bufferSize, k =>
                            {
                                PcData[k] = (ushort)(range[k] + 32768);
                            });

                        }
                        break;
                }
            }
        }
    }
}
