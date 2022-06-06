using System;
using System.Collections.Generic;

namespace ShockAnalyze
{
    // 系统设置
    [Serializable]
    public class SyetemSetting
    {
        public string PrefixNamed { get; set; } // 命名前缀
        public string ProjectDirectory { get; set; } // 项目目录
        public List<Channel> Channels { get; set; } // 通道设置
        public int LanguageType { get; set; } // 语言选择

        public SyetemSetting()
        {
            PrefixNamed = "XX";
            Channels = new List<Channel>();

            for (int i = 0; i < 3; i++)
            {
                Channel channel = new Channel();
                channel.Name = "通道" + (i + 1);
                channel.Unit = "单位";
                channel.Rate = 1;
                channel.Offset = 0;
                Channels.Add(channel);
            }

            LanguageType = 0;
        }
    }

    [Serializable]
    public class Channel
    {
        public string Name { get; set; } // 名称
        public string Unit { get; set; } // 单位
        public double Rate { get; set; } // 比例系数
        public double Offset { get; set; } // 偏移量
    }
}
