using System;
using System.IO;
using System.Windows.Forms;

namespace ShockAnalyze
{
    public class OutPutData
    {
        private string FlilePath;

        //构造函数
        public void SetPath(string filePath)
        {
            FlilePath = filePath;
        }

        //数据写入
        FileStream fs = null;
        public void WriteDate(ResultData resultData)
        {
            //创建文件
            try
            {
                fs = new FileStream(FlilePath, FileMode.Create);
            }
            catch (Exception ex)
            {
                MessageBox.Show("存储发生错误:" + ex.Message.ToString());
                return;
            }

            //头部
            WriteStrToFile(Constant.FILE_HEDD_PREFIX + Constant.FILE_HEADER);
            WriteStrToFile(SplicingStr(Constant.FILE_HEADER));//头部信息
            WriteStrToFile(Constant.FILE_RESULT_TYPE);

            //信息
            WriteStrToFile(Constant.FILE_HEDD_PREFIX + Constant.FILE_SHOCK_INF);
            WriteStrToFile(SplicingStr(Constant.FILE_SHOCK_INF));//减震器信息
            WriteStrToFile(string.Format(" Shock Name = {0}", resultData.ShockInf.Shock_Name));
            WriteStrToFile(string.Format(" Shock ID = {0}", resultData.ShockInf.Shock_Num));
            WriteStrToFile(string.Format(" Vehicle = {0}", resultData.ShockInf.Vehicle));
            WriteStrToFile(string.Format(" Location = {0}", resultData.ShockInf.Location));
            WriteStrToFile(string.Format(" Compression Valving = {0}", resultData.ShockInf.Compression_Valving));
            WriteStrToFile(string.Format(" Rebound Valving = {0}", resultData.ShockInf.Rebound_Valving));
            WriteStrToFile(string.Format(" Diameter = {0}", resultData.ShockInf.Diameter));
            WriteStrToFile(string.Format(" Installation Position = {0}", resultData.ShockInf.Installation_Position));
            WriteStrToFile(string.Format(" Compression Setting = {0}", resultData.ShockInf.Compression_Setting));
            WriteStrToFile(string.Format(" Rebound Setting = {0}", resultData.ShockInf.Rebound_Setting));
            WriteStrToFile(string.Format(" Preload Setting = {0}", resultData.ShockInf.Preload_Setting));
            WriteStrToFile(string.Format(" Notes = {0}", resultData.ShockInf.Notes));

            //温度
            WriteStrToFile(Constant.FILE_HEDD_PREFIX + Constant.FILE_TEMPERATURE);
            WriteStrToFile(SplicingStr(Constant.FILE_TEMPERATURE));//初始化温度信息
            WriteStrToFile(string.Format(" Initial_Temp = {0}", resultData.Initial_Temp));

            //试验频率
            WriteStrToFile(Constant.FILE_HEDD_PREFIX + Constant.FILE_FREQUENCY);
            WriteStrToFile(SplicingStr(Constant.FILE_FREQUENCY));//初始化温度信息
            WriteStrToFile(string.Format(" Frequency = {0}", resultData.Frequency));

            //摩擦力和充气力
            WriteStrToFile(Constant.FILE_HEDD_PREFIX + Constant.FILE_FM_FC);
            WriteStrToFile(SplicingStr(Constant.FILE_FM_FC));//力值信息
            WriteStrToFile(string.Format(" Fmf = {0}", resultData.Fmf));
            WriteStrToFile(string.Format(" Fmy = {0}", resultData.Fmy));
            WriteStrToFile(string.Format(" Fm = {0}", resultData.Fm));
            WriteStrToFile(string.Format(" Fc = {0}", resultData.Fc));
            WriteStrToFile(string.Format(" F0 = {0}", resultData.F0));

            //测试结果
            WriteStrToFile(Constant.FILE_HEDD_PREFIX + Constant.FILE_TEST_RESULT);
            WriteStrToFile(SplicingStr(Constant.FILE_TEST_RESULT));//结果信息

            //速度点遍历
            int index = 1;
            foreach (AnalyzeData analyzeData in resultData.AnalyzeDatas)
            {
                WriteStrToFile(string.Format(" Test Speed {0} = {1}", index, analyzeData.Speed.ToString("F3")));
                index++;
            }

            //速度点对应分析信息
            WriteStrToFile(Constant.FILE_SPEED_FORCE_DATA);
            WriteStrToFile(" Speed    ActSpeed   CompForce   ReboundForce");

            foreach (AnalyzeData analyzeData in resultData.AnalyzeDatas)
            {
                double speed = analyzeData.Speed;//速度点
                double actSpeed = analyzeData.ActSpeed;//实际速度
                double compForce = analyzeData.CompForce;//压缩力
                double reboundForce = analyzeData.ReboundForce;//复原力
                WriteStrToFile(string.Format(" {0}   {1}   {2}   {3}", speed.ToString("F3"), actSpeed.ToString("F3"), compForce.ToString("F2"), reboundForce.ToString("F2")));
            }

            //每个速度点采集数据
            index = 1;
            foreach (AnalyzeData analyzeData in resultData.AnalyzeDatas)
            {
                WriteStrToFile(string.Format("(Test Speed {0} indicator diagram data)", index));
                WriteStrToFile("  Time             Travel         Force");
                double startTime = 0;
                int i = 0;
                foreach (AcquisitionData acquisitionData in analyzeData.AcquisitionDatas)
                {

                    if (i == 0)
                        startTime = acquisitionData.AcqTime;//开始时间

                    double time = acquisitionData.AcqTime - startTime;//采集时间
                    double travel = acquisitionData.Displacement;//位移
                    double force = acquisitionData.Force;//力
                    WriteStrToFile(string.Format(" {0}     {1}     {2}", time.ToString("F10"), travel.ToString("F3"), force.ToString("F3")));
                    i++;
                }

                index++;
            }

            //清空缓冲区、关闭流
            fs.Flush();
            fs.Close();
        }

        //写入文件
        private void WriteStrToFile(string str)
        {
            //获得字节数组
            byte[] data = System.Text.Encoding.GetEncoding("GB2312").GetBytes(str + Environment.NewLine);
            //开始写入
            fs.Write(data, 0, data.Length);
        }

        private string SplicingStr(string field)
        {
            return string.Format("[{0}]", field);
        }
    }
}
