using System;

namespace ShockAnalyze
{
    //减震器信息
    [Serializable]
    public class ShockInfo
    {
        public string Shock_Name { get; set; }//减震器名称
        public string Shock_Num { get; set; }//减震器编号
        public string Vehicle { get; set; }//车名
        public string Location { get; set; }//试验地点
        public string Compression_Valving { get; set; }//压缩力指标
        public string Rebound_Valving { get; set; }//复原力指标
        public string Diameter { get; set; }//直径
        public string Installation_Position { get; set; }//安装位置
        public string Compression_Setting { get; set; }//压缩设置
        public string Rebound_Setting { get; set; }//复原设置
        public string Preload_Setting { get; set; }//预加载设置
        public string Notes { get; set; }//备注

        public ShockInfo()
        {
            Shock_Name = "";
            Shock_Num = Constant.EMPTY_STR;
            Vehicle = Constant.EMPTY_STR;
            Location = Constant.EMPTY_STR;
            Compression_Valving = Constant.EMPTY_STR;
            Rebound_Valving = Constant.EMPTY_STR;
            Diameter = Constant.EMPTY_STR;
            Installation_Position = "FRONT";
            Compression_Setting = Constant.EMPTY_STR;
            Rebound_Setting = Constant.EMPTY_STR;
            Preload_Setting = Constant.EMPTY_STR;
            Notes = Constant.EMPTY_STR;
        }
    }
}
