using System;
using NationalInstruments.DAQmx;

namespace ShockAnalyze
{
    public class NI_Daq
    {
        // 数采数据回调
        public delegate void OnSampleDataHandler(object sender, SampleDataEventArgs e);
        public event OnSampleDataHandler Event_SampleData;
        public class SampleDataEventArgs : EventArgs
        {
            public double[] data;
            public string str;
            public SampleDataEventArgs(double[] data1)
            {
                this.data = data1;
            }
        }

        // 数采错误回调
        public delegate void OnErrorHandler();
        public event OnErrorHandler onErrorHandler;

        // 数采参数
        private AsyncCallback analogCallback;
        private AnalogMultiChannelReader analogInReader;
        private Task mytask;
        private Task running_task; // 数采任务
        private double[,] arrive_data; //接受数据
        private int readSleep = 10;// 数据采集5ms回调一次,显示用的
        private bool isConnected = false;

        // 构造函数
        public NI_Daq()
        {
            //channel_id = "cDAQ9181-1CD6E71Mod1/ai0:2"; //@"Dev1/ai0:2"; 设备名/通道:采集几个通道
        }

        // 数采集返回数据回调
        public void OnSampleData(object sender, SampleDataEventArgs e)
        {
            if (Event_SampleData != null)
            {
                Event_SampleData(sender, e);
            }
        }

        // 错误回调
        private void onError()
        {
            if (onErrorHandler != null)
            {
                onErrorHandler();
            }
        }

        // 数据记录文件
        public void RecordFile(string name)
        {
            try
            {
                this.mytask.StartNewFile(GlobalData.BasePath + "NI_Tdms\\" + name + ".tdms");
            }
            catch (Exception ex)
            {
                onErrorHandler();
                Console.WriteLine(ex.ToString());
                //MessageDxUtil.ShowError(ex.ToString());
            }
        }

        // 初始化数采，加载任务
        public bool InitDaq()
        {
            mytask = null;
            running_task = null;
            try
            {
                this.mytask = DaqSystem.Local.LoadTask("myTask1");

                //this.mytask.AIChannels.CreateVoltageChannel(this.channel_id, "daq", (AITerminalConfiguration)(-1), -10, 10, AIVoltageUnits.Volts);
                //this.mytask.Timing.ConfigureSampleClock("", 1000, SampleClockActiveEdge.Rising, SampleQuantityMode.ContinuousSamples, 1000);
                this.mytask.ConfigureLogging(GlobalData.BasePath + "NI_Tdms\\Normal.tdms", TdmsLoggingOperation.CreateOrReplace, LoggingMode.LogAndRead, "Group Name");

                this.mytask.Control(TaskAction.Verify);

                this.analogInReader = new AnalogMultiChannelReader(this.mytask.Stream);
                this.analogInReader.SynchronizeCallbacks = true;
                this.analogCallback = new AsyncCallback(this.AnalogInCallback);
                isConnected = true;

                return true;
            }
            catch (Exception ex)
            {
                //onErrorHandler();
                Console.WriteLine(ex.ToString());
            }

            return false;
        }

        // 开始采集
        public void Start()
        {
            if (running_task != null||!isConnected) { return; }
            try
            {
                this.running_task = this.mytask;
                this.analogInReader.BeginReadMultiSample(readSleep, this.analogCallback, this.mytask);
            }
            catch (Exception ex)
            {
                onErrorHandler();
                Console.WriteLine(ex.ToString());
                //this.running_task = null;
                //this.mytask.Dispose();
                //MessageDxUtil.ShowError(ex.ToString());
            }
        }

        // 停止采集
        public void Stop()
        {
            if (running_task != null)
            {
                mytask.Stop();
                running_task.Stop();

                //mytask.Dispose();
                //running_task.Dispose();

                //mytask = null;
                //running_task = null;
            }
        }

        public void Relese()
        {
            // 这里关闭可能会报错
            if (running_task != null)
            {
                try
                {
                    mytask.Stop();
                    running_task.Stop();

                    mytask.Dispose();
                    running_task.Dispose();

                    mytask = null;
                    running_task = null;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
        }

        public void Reset()
        {
            Relese();
            InitDaq();
            Start();
        }

        // 采集回调
        private void AnalogInCallback(IAsyncResult ar)
        {
            try
            {
                if (this.running_task == ar.AsyncState)
                {
                    this.arrive_data = this.analogInReader.EndReadMultiSample(ar);
                    int channelCount = arrive_data.GetLength(0);
                    int datacount = arrive_data.GetLength(1);
                    if (datacount > 0)
                    {
                        double[] result = new double[channelCount];
                        for (int i = 0; i < channelCount; i++)
                        {
                            result[i] = arrive_data[i, datacount - 1];
                        }
                        if (result.Length > 0) { this.OnSampleData(null, new SampleDataEventArgs(result)); }
                    }

                    this.analogInReader.BeginReadMultiSample(readSleep, this.analogCallback, this.mytask);
                }
            }
            catch (DaqException ex)
            {
                //this.running_task = null;
                //this.mytask.Dispose();
                //MessageDxUtil.ShowError(ex.ToString());
                onErrorHandler();
                Console.WriteLine(ex.ToString());
            }
        }

    }
}
