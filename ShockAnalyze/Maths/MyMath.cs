
using System;
using System.Collections.Generic;
namespace ShockAnalyze
{
    public class MyMath
    {
        //通过速度计算频率(Hz)
        public static double ConvertToFrequency(double speed, double travel)
        {
            return (2000 * speed) / (2 * 3.1415926f * travel);
        }

        //通过频率计算速度(m/s)
        public static double ConvertToSpeed(double frequency, double travel)
        {
            return (2 * 3.1415926f * travel) * frequency / 2000;
        }

        //计算电机速度(PPU/S)
        public static double ConvertToMotorSpeed(double targetSpeed, double travel)
        {
            return ConvertToFrequency(targetSpeed, travel) * 60 * 5;
        }

        public static double ConvertToMotorSpeed(double frequency)
        {
            return frequency * 60 * 5;
        }

        // 曲线拟合
        public static double[] Polyfit(double[] xValues, double[] yValues)
        {
            //拟合公式
            GaussNewton.F function = delegate(double[] coefficients, double x)
            {
                return coefficients[0] * Math.Pow(x, 4) + coefficients[1] * Math.Pow(x, 3) + coefficients[2] * Math.Pow(x, 2) + coefficients[3] * Math.Pow(x, 1) + coefficients[4] * Math.Pow(x, 0);
            };

            //拟合数据
            GaussNewton gaussNewton = new GaussNewton(5);
            gaussNewton.Initialize(yValues, xValues, function);

            return gaussNewton.Coefficients;
        }

        // 位移修正
        public static List<AcquisitionData> FixedTrvaelData(List<AcquisitionData> acquisitionDatas, int cirlce)
        {
            int acqDataCount = acquisitionDatas.Count;
            int splitIndex = acqDataCount / (2*cirlce);

            List<double[]> times = new List<double[]>();
            List<double[]> travels = new List<double[]>();
            for (int i = 0; i < 2 * cirlce; i++)
            {
                if (i ==2 * cirlce - 1)
                {
                    times.Add(new double[acqDataCount-splitIndex * (2 * cirlce - 1)]);
                    travels.Add(new double[acqDataCount-splitIndex * (2 * cirlce - 1)]);
                    continue;
                }

                times.Add(new double[splitIndex]);
                travels.Add(new double[splitIndex]);
            }

            for (int i = 0; i < acqDataCount; i++)
            {
                double time = acquisitionDatas[i].AcqTime;
                double travel = acquisitionDatas[i].Displacement;
                int index = i / splitIndex;
                if (index >= 2 * cirlce) { index = index - 1; }
                times[index][i - index * splitIndex] = time;
                travels[index][i - index * splitIndex] = travel;
            }

            List<double[]> newTravels = new List<double[]>();
            for (int i = 0; i < 2 * cirlce; i++) { newTravels.Add(FixedTrvael(times[i], travels[i])); }

            for (int i = 0; i < acqDataCount; i++)
            {
                int index = i / splitIndex;
                if (index >= 2 * cirlce) { index = index - 1; }
                acquisitionDatas[i].Displacement = newTravels[index][i - index * splitIndex];
            }
            
            return acquisitionDatas;
        }

        // 先5次项拟合，再与原来的数据差值，大于差值平均数5倍则用拟合的数据
        private static double[] FixedTrvael(double[] times, double[] travels)
        {
            List<double> subSums = new List<double>();
            int acqDataCount = times.Length;
            double[] coefficients = MyMath.Polyfit(times, travels);
            double[] newTravel = new double[acqDataCount];
            for (int i = 0; i < acqDataCount; i++)
            {
                double x = times[i];
                newTravel[i] = coefficients[0] * Math.Pow(x, 4) + coefficients[1] * Math.Pow(x, 3) + coefficients[2] * Math.Pow(x, 2) + coefficients[3] * Math.Pow(x, 1) + coefficients[4] * Math.Pow(x, 0);
                double currentSubTravel = Math.Abs(newTravel[i] - travels[i]);

                if (i < 10)
                {
                    newTravel[i] = travels[i];
                    subSums.Add(currentSubTravel); 
                }
                else
                {
                    double sum = 0;
                    for (int j = 0; j < subSums.Count; j++) { sum += subSums[j]; }

                    double avg = sum / subSums.Count;
                    if (currentSubTravel < 4.5 * avg)
                    {
                        subSums.Remove(0);
                        subSums.Add(currentSubTravel);
                        newTravel[i] = travels[i];
                    }
                }
            }

            return newTravel;
        }
    }
}
