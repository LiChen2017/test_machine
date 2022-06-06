using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using Steema.TeeChart;
using Steema.TeeChart.Drawing;
using Steema.TeeChart.Styles;
using System;

namespace ShockAnalyze
{
    public class ChartManager
    {
        // 图表设置系数
        private const int LINE_WIDTH = 1; // 曲线宽度
        private const int TRANSPARENCY = 70; // 表格透明度

        // 设置变量
        private bool isFilter = false; // 需不需要滤波
        private double samplingrate=0; // 滤波频率
        private double cutOff=0; // 滤波常数

        //曲线颜色库                            
        private Color[] SeriesColors = new Color[] {Color.Teal ,Color.Indigo,Color.Blue, Color.Brown,Color.OrangeRed,
                                                           Color.CornflowerBlue,Color.Orange,Color.CadetBlue,Color.Maroon,Color.DarkViolet,
                                                           Color.DeepSkyBlue,Color.RosyBrown ,Color.Chocolate,Color.DimGray,Color.MediumOrchid,
                                                           Color.DarkOliveGreen,Color.ForestGreen};

        // 设置滤波参数
        public void SetParam(bool isFilter, double samplingrate, double cutOff)
        {
            this.isFilter = isFilter;
            this.samplingrate = samplingrate;
            this.cutOff = cutOff;
        }

        //获得曲线颜色
        public Color GetSeriesColor(TChart tChart)
        {
            //int curIndex = 0;
            int count = tChart.Series.Count;
            //if (count > 0)
            //{
                //Color lastColor = tChart.Series[count - 1].Color;
                //for (int i = 0; i < SeriesColors.Length; i++)
                //{
                //    if (SeriesColors[i] == lastColor)
                //    {
                //        curIndex = i + 1;
                //    }
                //}
            //}

            int colorIndex = count % SeriesColors.Length;//颜色序号

            return SeriesColors[colorIndex];
        }

        // 初始化图表
        public void InitChart(TChart tChart, bool isF_V,bool isReport,Language language)
        {
            tChart.Series.Clear();
            tChart.Legend.Visible = false;

            // Prepare chart for maximum speed
            tChart.Axes.Depth.Grid.ZPosition = 0;
            tChart.Axes.Left.AxisPen.Width = 1;
            tChart.Axes.Bottom.AxisPen.Width = 1;
            tChart.Aspect.View3D = false;
            tChart.Footer.Visible = false;
            tChart.Panel.Shadow.Visible = false;
            tChart.Header.Shadow.Visible = false;
            tChart.Axes.Bottom.Labels.Shadow.Visible = false;
            tChart.Axes.Bottom.Labels.Font.Shadow.Visible = false;
            tChart.Axes.Bottom.Title.Font.Shadow.Visible = false;
            tChart.Axes.Bottom.Title.Shadow.Visible = false;
            tChart.Axes.Depth.Labels.Font.Shadow.Visible = false;
            tChart.Axes.Depth.Labels.Shadow.Visible = false;
            tChart.Axes.Depth.Title.Font.Shadow.Visible = false;
            tChart.Axes.Depth.Title.Shadow.Visible = false;
            tChart.Axes.DepthTop.Labels.Font.Shadow.Visible = false;
            tChart.Axes.DepthTop.Labels.Shadow.Visible = false;
            tChart.Axes.DepthTop.Title.Font.Shadow.Visible = false;
            tChart.Axes.DepthTop.Title.Shadow.Visible = false;
            tChart.Walls.Back.AutoHide = false;
            tChart.Walls.Back.Shadow.Visible = false;
            tChart.Walls.Bottom.AutoHide = false;
            tChart.Walls.Bottom.Shadow.Visible = false;
            tChart.Walls.Left.AutoHide = false;
            tChart.Walls.Left.Shadow.Visible = false;
            tChart.Walls.Right.AutoHide = false;
            tChart.Walls.Right.Shadow.Visible = false;
            tChart.Aspect.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            tChart.Graphics3D.BufferStyle = BufferStyle.DoubleBuffer;
            tChart.Aspect.SmoothingMode = SmoothingMode.HighQuality;

            //表格样式
            tChart.Axes.Left.Grid.Style = DashStyle.Dash;
            tChart.Axes.Left.Grid.Transparency = TRANSPARENCY;
            tChart.Axes.Bottom.Grid.Style = DashStyle.Dash;
            tChart.Axes.Bottom.Grid.Transparency = TRANSPARENCY;
            tChart.Aspect.SmoothingMode = SmoothingMode.HighQuality;

            tChart.Axes.Left.Title.Caption = language.FindText("力(N)");

            if (!isF_V)
            {
                tChart.Header.Text = language.FindText("力 vs 位移");
                tChart.Axes.Bottom.Title.Caption = language.FindText("位移(mm)");

                tChart.Axes.Bottom.MinimumOffset = isReport ? 10 : 30;
                tChart.Axes.Bottom.MaximumOffset = isReport ? 10 : 30;
                tChart.Axes.Left.MinimumOffset = isReport ? 10 : 20;
                tChart.Axes.Left.MaximumOffset = isReport ? 10 : 20;
                tChart.Axes.Bottom.Increment = 5;
            }
            else
            {
                tChart.Header.Text = language.FindText("力 vs 速度");
                tChart.Axes.Bottom.Title.Caption = language.FindText("速度")+"(m/s)";

                tChart.Axes.Bottom.MinimumOffset = 0;
                tChart.Axes.Bottom.MaximumOffset = 0;
                tChart.Axes.Left.MinimumOffset = 0;
                tChart.Axes.Left.MaximumOffset = 0;
                tChart.Axes.Bottom.Increment =0.1;
            }
            
            tChart.Axes.Left.Increment =100;
        }

        // 显示图表
        public void ShowChart(TChart tChart,string fileName, bool isF_V, ResultData resultData,bool isMinMax)
        {
            if (isF_V)// 显示速度-力图表
            {
                if (isMinMax)
                {
                    ShowF_VChart(tChart, fileName, resultData,double.Parse(resultData.Fc));
                }
                else
                {
                    ShowF_VChart(tChart, fileName, resultData);
                }
            }
            else  //显示位移-力图表
            {
                foreach (AnalyzeData analyzeData in resultData.AnalyzeDatas)
                {
                    ShowF_DChart(tChart, fileName, analyzeData.Speed, analyzeData.AcquisitionDatas, double.Parse(resultData.Fc));
                }

            }
        }

        public bool Exsit(TChart tChart,string name)
        {
            int i = 0;
            while (i < tChart.Series.Count)
            {
                FastLine fastLine = (FastLine)tChart.Series[i];
                if (fastLine.Title.Equals(name))
                {
                    return true;
                }
                else
                {
                    i++;
                }
            }

            return false;
        }

        // 显示位移-力图表
        public void ShowF_DChart(TChart tChart, string name, double speed, List<AcquisitionData> acquisitionDatas,double Fc)
        {
            int acqDataCount = acquisitionDatas.Count;
            double[] times = new double[acqDataCount];
            double[] travels = new double[acqDataCount];
            double[] forces = new double[acqDataCount];

            for (int i = 0; i < acqDataCount; i++)
            {
                times[i] = acquisitionDatas[i].AcqTime;
                travels[i] = acquisitionDatas[i].Displacement;
                forces[i] = acquisitionDatas[i].Force + Math.Abs(Fc);
            }

            // 是否进行滤波
            if (isFilter)
            {
                travels = Butterworth.Filter(travels, samplingrate, cutOff);
                forces = Butterworth.Filter(forces, samplingrate, cutOff);
            }

            // 赋值新数据
            double[] newTravels = new double[acqDataCount+1];
            double[] newForces = new double[acqDataCount+1];
            for (int i = 0; i < acqDataCount; i++)
            {
                newTravels[i] = -travels[i];
                newForces[i] = forces[i];
            }

            newTravels[acqDataCount] = newTravels[0];
            newForces[acqDataCount] = newForces[0];

            AddSeries(tChart, name, newTravels, newForces, false);
        }

        public void AddTemp(TChart tChart,TestSetting testSetting)
        {
            RemoveSeries(tChart, "temp");

            if (!testSetting.isNeedTemp) { return; }

            tChart.Axes.Bottom.Increment = 0.1;
            int count = testSetting.Temps1.Count * 2 + 1;
            double[] tempSpeeds1 = new double[count];
            double[] tempForces1 = new double[count];
            for (int i = testSetting.Temps1.Count - 1; i >= 0; i--)
            {
                Temp temp1 = testSetting.Temps1[i];

                // 复原力
                tempSpeeds1[testSetting.Temps1.Count - 1 - i] = temp1.Speed;
                tempForces1[testSetting.Temps1.Count - 1 - i] = temp1.Fmf;

                // 压缩力
                tempSpeeds1[testSetting.Temps1.Count + 1 + i] = temp1.Speed;
                tempForces1[testSetting.Temps1.Count + 1 + i] = temp1.Fmy;
            }

            AddSeries(tChart, "temp", tempSpeeds1, tempForces1, true);

            double[] tempSpeeds2 = new double[count];
            double[] tempForces2 = new double[count];
            for (int i = testSetting.Temps1.Count - 1; i >= 0; i--)
            {
                Temp temp2 = testSetting.Temps2[i];

                // 复原力
                tempSpeeds2[testSetting.Temps2.Count - 1 - i] = temp2.Speed;
                tempForces2[testSetting.Temps2.Count - 1 - i] = temp2.Fmf;

                // 压缩力
                tempSpeeds2[testSetting.Temps2.Count + 1 + i] = temp2.Speed;
                tempForces2[testSetting.Temps2.Count + 1 + i] = temp2.Fmy;
            }

            AddSeries(tChart, "temp", tempSpeeds2, tempForces2, true);
        }

        // 显示速度-力图表
        public void ShowF_VChart(TChart tChart, string name, ResultData resultData)
        {
            int count = resultData.AnalyzeDatas.Count * 2 + 1;
            double[] speeds = new double[count];// 速度
            double[] forces = new double[count];// 复原力
            for (int i = resultData.AnalyzeDatas.Count - 1; i >= 0; i--)
            {
                AnalyzeData analyzeData = resultData.AnalyzeDatas[i];

                // 复原力
                speeds[resultData.AnalyzeDatas.Count - 1 - i] = analyzeData.ActSpeed;
                forces[resultData.AnalyzeDatas.Count - 1 - i] = analyzeData.ReboundForce;

                // 压缩力
                speeds[resultData.AnalyzeDatas.Count + 1 + i] = analyzeData.ActSpeed;
                forces[resultData.AnalyzeDatas.Count + 1 + i] = analyzeData.CompForce;
            }

            if (isFilter) { forces = Butterworth.Filter(forces, samplingrate, cutOff); }

            AddSeries(tChart, name, speeds, forces, false);
        }

        // 显示速度-力图表
        public void ShowF_VChart(TChart tChart, string name, ResultData resultData,double Fc)
        {
            int count = resultData.AnalyzeDatas.Count * 2 + 1;
            double[] speeds = new double[count];// 速度
            double[] forces = new double[count];// 复原力
            for (int i = resultData.AnalyzeDatas.Count - 1; i >= 0; i--)
            {
                AnalyzeData analyzeData = resultData.AnalyzeDatas[i];

                // 复原力
                speeds[resultData.AnalyzeDatas.Count - 1 - i] = analyzeData.ActSpeed;
                forces[resultData.AnalyzeDatas.Count - 1 - i] = analyzeData.MaxForce + Math.Abs(Fc);

                // 压缩力
                speeds[resultData.AnalyzeDatas.Count + 1 + i] = analyzeData.ActSpeed;
                forces[resultData.AnalyzeDatas.Count + 1 + i] = analyzeData.MinForce + Math.Abs(Fc);
            }

            if (isFilter) { forces = Butterworth.Filter(forces, samplingrate, cutOff); }

            AddSeries(tChart, name, speeds, forces, false);
        }

        // 添加曲线static
        public void AddSeries(TChart tChart, string name, double[] xValues, double[] yValues, bool isDashDot)
        {
            //添加
            FastLine fastLine = new FastLine();//创建一条曲线
            fastLine.LinePen.Width = LINE_WIDTH;
            fastLine.Color = GetSeriesColor(tChart);
            fastLine.Title = name;
            fastLine.XValues.Order = ValueListOrder.None;
            if (isDashDot)
            {
                fastLine.LinePen.Style = DashStyle.Custom;
                fastLine.LinePen.DashPattern = new float[] { 5, 5 };
                fastLine.Color = Color.Red;
            }

            fastLine.Add(xValues, yValues);//添加点
            tChart.Series.Add(fastLine); //添加至图表
        }

        // 移除曲线
        public void RemoveSeries(TChart tChart, string name)
        {
            tChart.AutoRepaint = false;
            int i = 0;
            while (i < tChart.Series.Count)
            {
                FastLine fastLine = (FastLine)tChart.Series[i];
                if (fastLine.Title.Equals(name))
                {
                    tChart.Series.Remove(fastLine);
                }
                else 
                {
                    i++;
                }
            }

            if (tChart.Series.Count == 1)
            {
                tChart.Legend.Visible = false;
            }

            tChart.AutoRepaint = true;
            tChart.Refresh();
        }

        // 修改标题
        public void ChangeXYLabel(TChart tChart, string xLabel, string yLabel)
        {
            //图表属性
            tChart.Axes.Bottom.Title.Text = xLabel;
            tChart.Axes.Left.Title.Text = yLabel;
        }

        // 图表转成图片复制到内存
        public static void ImageCopyToClipboard(TChart tChart)
        {
            //输出emf格式图片至剪切板
            tChart.Export.Image.Metafile.EMFFormat = EmfType.EmfPlusDual;
            tChart.Export.Image.Metafile.CopyToClipboard();
        }
    }
}
