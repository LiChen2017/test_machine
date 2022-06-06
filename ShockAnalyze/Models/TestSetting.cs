using System;
using System.Collections.Generic;

namespace ShockAnalyze
{
    // 测试设置参数
    [Serializable]
    public class TestSetting
    {
        // 减震器信息
        public ShockInfo Shock_Info { get; set; }

        // 温度参数
        public int InitTemperature { get; set; } // 初始温度
        public int MaxTemperature { get; set; } // 最大温度
        public bool isPreheating { get; set; } // 是否预热

        // 测试行程
        public double Travel { get; set; }
        
        // 排气测试参数
        public double ExhaustFrequency { get; set; } // 排气频率
        public double ExhaustCircle { get; set; } // 排气圈数

        // 摩擦力测试参数
        public double FrictionSpeed { get; set; } // 摩擦力测试速度
        public double FrictionCircle { get; set; } // 摩擦力圈数
        public bool isAutoCalculate { get; set; } // 是否加入计算

        // 初始载荷
        public double InitLoad { get; set; }

        // 测试速度参数
        public List<TestSpeed> TestSpeeds{ get; set; }

        // 对标数据
        public bool isNeedTemp { get; set; }
        public List<Temp> Temps1 { get; set; }
        public List<Temp> Temps2 { get; set; }

        public TestSetting()
        {
            Shock_Info = new ShockInfo();
            TestSpeeds = new List<TestSpeed>();
            Temps1 = new List<Temp>();
            Temps2 = new List<Temp>();
            Travel = 100;
            MaxTemperature = 100;
            isNeedTemp = false;
            FrictionSpeed = 0.005;
            FrictionCircle = 1;
            ExhaustFrequency = 1.67;
            ExhaustCircle = 5;

        }
    }

    [Serializable]
    public class Temp
    {
        public double Speed { get; set; }//速度点
        public double Fmf { get; set; }//压缩力
        public double Fmy { get; set; }//复原力

        public Temp(double Speed, double Fmf, double Fmy)
        {
            this.Speed = Speed;
            this.Fmf = Fmf;
            this.Fmy = Fmy;
        }
    }

    [Serializable]
    public class TestSpeed
    {
        public double Speed { get; set; } // 测试速度
        public double Frequency { get; set; } // 测试频率

        public TestSpeed(double speed, double frequency)
        {
            this.Speed = speed;
            this.Frequency = frequency;
        }
    }

}
