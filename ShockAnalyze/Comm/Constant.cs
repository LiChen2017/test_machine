
namespace ShockAnalyze
{
    public class Constant
    {
        public const string EMPTY_STR = "N/A";

        //文件模块
        public const string FILE_HEDD_PREFIX = "$---------------------------------------------------------------------";//前缀
        public const string FILE_RESULT_TYPE = " Result_Type = 'damper force performance'"; //结果文件类型
        public const string FILE_HEADER = "HEADER";//头
        public const string FILE_SHOCK_INF = "INFROMATION"; //减震器信息
        public const string FILE_TEMPERATURE = "TEMPERATURE"; //温度
        public const string FILE_FREQUENCY = "FREQUENCY"; //试验频率
        public const string FILE_FM_FC = "FM&FC";//摩擦力、充气力
        public const string FILE_TEST_RESULT = "TEST_RESULT";//测试结果
        public const string FILE_SPEED_FORCE_DATA = "(speed force data)";//速度力数据

        public const double ONE_CIRCLE_PULSE = 5*5000;//电机一圈脉冲
    }
}
