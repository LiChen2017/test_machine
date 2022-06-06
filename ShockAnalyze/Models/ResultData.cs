using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ShockAnalyze
{
    public class DaqData
    {
        public double Channel1;//通道一
        public double Channel2;//通道二
        public double Channel3;//通道三
        public double Channel4;//通道四
        public double Channel5;//通道五
        public double Channel6;//通道六
        public double Channel7;//通道七
        public double Channel8;//通道八
        public double AcqTime;//采集时间
    }

    //零点位置的参考事件点
    public class ZeroPositionTime
    {
        public double BeforeZeroTime { get; set; }//零点位置前一个时间
        public double AfterZeroTime { get; set; } //零点位置后一个时间
        public double ApproachZeroTime { get; set; }//接近零点的时间

        public ZeroPositionTime(double beforeZeroTime, double afterZeroTime, double beforeZeroPosition, double afterZeroPosition)
        {
            BeforeZeroTime = beforeZeroTime;
            AfterZeroTime = afterZeroTime;

            ApproachZeroTime = Math.Abs(beforeZeroPosition) > Math.Abs(afterZeroPosition) ? afterZeroTime : beforeZeroTime;
        }
    }

    //采集数据
    public class AcquisitionData
    {
        public AcquisitionData(double displacement, double force)
        {
            Displacement = displacement;
            Force = force;
        }

        public AcquisitionData(double time, double displacement, double force)
        {
            AcqTime = time;
            Displacement = displacement;
            Force = force;
        }

        public AcquisitionData(double time, double displacement, double force, double temperature)
        {
            AcqTime = time;
            Displacement = displacement;
            Force = force;
            Temperature = temperature;
        }

        public double AcqTime { get; set; } //采集时间
        public double Displacement { get; set; }//位移
        public double Force { get; set; }//力
        public double Temperature { get; set; }//温度
    }

    //每个速度点分析数据
    public class AnalyzeData
    {
        //分析后的数据
        public double Speed { get; set; }//速度点
        public double ActSpeed { get; set; } //实际速度
        public double CompForce { get; set; }//压缩力
        public double ReboundForce { get; set; }//复原力

        public double MinForce = 0, MaxForce = 0; // 最小/最大力

        //计算数据
        public List<AcquisitionData> AcquisitionDatas { get; set; }//采集数据

        //构造函数
        public AnalyzeData(double speed)
        {
            Speed = speed; //速度点
        }

        public AnalyzeData(double speed, List<AcquisitionData> acquisitionDatas)
        {
            Speed = speed; //速度点
            AcquisitionDatas = acquisitionDatas;//采集数据
        }

        //赋值分析出来的数据
        public void SetData(double actSpeed, double reboundForce, double compForce)
        {
            ActSpeed = actSpeed; //实际速度
            CompForce = compForce; //压缩力
            ReboundForce = reboundForce; //拉伸力
        }

        // 获取最大最小值
        public void GetRange()
        {
            for (int i = 0; i < AcquisitionDatas.Count;i++)
            {
                AcquisitionData acquisitionData=AcquisitionDatas[i];
                if (i == 0)
                {
                    MinForce = acquisitionData.Force;
                    MaxForce = acquisitionData.Force;
                }

                if (MaxForce < acquisitionData.Force) { MaxForce = acquisitionData.Force; }
                else if (MinForce > acquisitionData.Force) { MinForce = acquisitionData.Force; }
            }
        }
    }

    // 测试结果数据
    public class ResultData
    {
        public ShockInfo ShockInf { get; set; }  // 减震器信息

        public string Initial_Temp { get; set; } // 初始温度
        public string Frequency { get; set; }    // 试验频率

        public string Fmf { get; set; } // 复原阻尼力
        public string Fmy { get; set; } // 压缩阻尼力
        public string Fm { get; set; }  // 摩擦力
        public string Fc { get; set; }  // 充气力
        public string F0 { get; set; }  // 减震器静止时刻的力
        public List<AnalyzeData> AnalyzeDatas { get; set; } //速度点分析数据

        public ResultData()
        {
            AnalyzeDatas = new List<AnalyzeData>();
        }

        // 读取结果文件数据
        public void ReadResultData(string[] fileArray)
        {
            //数据进行分割成5部分
            int i = 0, speedCount = 0;

            while (i < fileArray.Length)
            {
                string lineStr = fileArray[i];

                if (lineStr == SplicingStr(Constant.FILE_HEADER))//头部信息
                {
                    i = i + 2;
                }
                else if (lineStr == SplicingStr(Constant.FILE_SHOCK_INF))//减震器信息
                {
                    this.ShockInf = new ShockInfo();//实例化减震器信息类
                    this.ShockInf.Shock_Name = GetValue(fileArray[i + 1]);//减震器名称
                    this.ShockInf.Shock_Num = GetValue(fileArray[i + 2]);//减震器编号
                    this.ShockInf.Vehicle = GetValue(fileArray[i + 3]);//车名
                    this.ShockInf.Location = GetValue(fileArray[i + 4]);//试验地点
                    this.ShockInf.Compression_Valving = GetValue(fileArray[i + 5]);//压缩力指标
                    this.ShockInf.Rebound_Valving = GetValue(fileArray[i + 6]);//复原力指标
                    this.ShockInf.Diameter = GetValue(fileArray[i + 7]);//直径
                    this.ShockInf.Installation_Position = GetValue(fileArray[i + 8]);//安装位置
                    this.ShockInf.Compression_Setting = GetValue(fileArray[i + 9]);//压缩设置
                    this.ShockInf.Rebound_Setting = GetValue(fileArray[i + 10]);//复原设置
                    this.ShockInf.Preload_Setting = GetValue(fileArray[i + 11]);//预加载设置
                    this.ShockInf.Notes = GetValue(fileArray[i + 12]);//备注

                    i = i + 13;
                }
                else if (lineStr == SplicingStr(Constant.FILE_TEMPERATURE))//初始温度
                {
                    this.Initial_Temp = GetValue(fileArray[i + 1]);//初始温度

                    i = i + 2;
                }
                else if (lineStr == SplicingStr(Constant.FILE_FREQUENCY))
                {
                    this.Frequency = GetValue(fileArray[i + 1]);//试验频率

                    i = i + 2;
                }
                else if (lineStr == SplicingStr(Constant.FILE_FM_FC))//力值信息
                {
                    this.Fmf = GetValue(fileArray[i + 1]);//复原力
                    this.Fmy = GetValue(fileArray[i + 2]);//压缩力
                    this.Fm = GetValue(fileArray[i + 3]);//摩擦力 
                    this.Fc = GetValue(fileArray[i + 4]);//充气力
                    this.F0 = GetValue(fileArray[i + 5]);

                    i = i + 6;
                }
                else if (lineStr == SplicingStr(Constant.FILE_TEST_RESULT))//结果信息
                {
                    //结果部分开始序号
                    i++;
                    while (true)
                    {
                        if (fileArray[i] == Constant.FILE_SPEED_FORCE_DATA)
                        {
                            i = i + 2;
                            break;
                        }

                        speedCount++;
                        i++;
                    }

                    break;
                }
                else
                {
                    i++;//不满足以上情况自动+1
                }
            }

            //速度点数据
            int recIndx = 1, curIndex = 0;
            List<AnalyzeData> analyzeDatas = new List<AnalyzeData>();//每个速度点的分析数据
            List<AcquisitionData> acquisitionDatas = null;//采集数据
            for (int j = i; j < fileArray.Length; j++)
            {
                string[] fieldStrs = Regex.Split(fileArray[j], "   ", RegexOptions.IgnoreCase);
                if (j - i < speedCount)//速度点
                {
                    AnalyzeData analyzeData = new AnalyzeData(double.Parse(fieldStrs[0]));
                    analyzeData.SetData(double.Parse(fieldStrs[1]), double.Parse(fieldStrs[3]), double.Parse(fieldStrs[2]));
                    analyzeDatas.Add(analyzeData);
                }
                else
                {
                    //采集数据
                    if (fieldStrs.Length == 1)//速度点头
                        curIndex++;
                    else if (fieldStrs.Length == 8)
                        acquisitionDatas = new List<AcquisitionData>();

                    if (fieldStrs.Length == 3)//数据点
                        acquisitionDatas.Add(new AcquisitionData(double.Parse(fieldStrs[0]), double.Parse(fieldStrs[1]), double.Parse(fieldStrs[2])));

                    //结束
                    if (curIndex == recIndx + 1 || j == fileArray.Length - 1)
                    {
                        analyzeDatas[recIndx - 1].AcquisitionDatas = acquisitionDatas;
                        recIndx++;
                    }

                }
            }

            this.AnalyzeDatas = analyzeDatas;
        }

        private string GetValue(string field)
        {
            return field.Split('=')[1].Trim();
        }

        private string SplicingStr(string field)
        {
            return string.Format("[{0}]", field);
        }
    }

}
