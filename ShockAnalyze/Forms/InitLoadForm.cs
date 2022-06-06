using System;
using System.Drawing;
using System.Windows.Forms;

namespace ShockAnalyze
{
    public partial class InitLoadForm : DevExpress.XtraEditors.XtraForm
    {
        // 测试参数设置
        public TestSetting testSetting = new TestSetting();
        public TestSetting Test_Setting
        {
            get { return this.testSetting; }
            set { this.testSetting = value; }
        }

        // 当前力值
        double currentForce;

        // 构造函数
        public InitLoadForm(Language language)
        {
            InitializeComponent();
            this.Size = new Size(550, 330);

            BindText(language);
        }

        private void BindText(Language language)
        {
            this.Text = language.FindText("初始载荷");
            sbtnClose.Text = language.FindText("关闭");

            labelControl2.Text = language.FindText("步骤一");
            labelControl4.Text = language.FindText("请保证您的载荷单元处于空载状态，即减震器未装夹");
            sbtnNext.Text = language.FindText("下一步");

            labelControl9.Text = language.FindText("步骤二");
            labelControl7.Text= language.FindText("下方标示栏为当前力传感器测量值，请在稳定状态标定载荷单元值");
            sbtnPrevious.Text= language.FindText("上一步");
            sbtnRating.Text = language.FindText("标定");

            tabNavigationPage1.PageText= language.FindText("步骤一");
            tabNavigationPage2.PageText = language.FindText("步骤二");
        }

        // 标定
        private void sbtnRating_Click(object sender, EventArgs e)
        {
            // 保存当前的力值
            testSetting.InitLoad = currentForce;
        }

        // 下一步
        private void sbtnNext_Click(object sender, EventArgs e)
        {
            tabPane1.SelectedPageIndex = 1;
        }

        // 上一步
        private void sbtnPrevious_Click(object sender, EventArgs e)
        {
            tabPane1.SelectedPageIndex = 0;
        }

        // 关闭
        private void sbtnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }

        // 数采回调函数
        public void flush_data(object sender, NI_Daq.SampleDataEventArgs e)
        {
            //Console.WriteLine(e.data[0].ToString() + ":" + DateTime.Now.ToString("dd:HH:mm:ss"));
            currentForce = double.Parse(e.data[2].ToString("F2"));
        }

        private void timerShow_Tick(object sender, EventArgs e)
        {
            double value=currentForce - testSetting.InitLoad;
            //double fliterValue = Fliter(value);
            tedtForce.Text = value.ToString("F2");
        }

        //double sum = 0;//累加和
        //double[] forces = new double[10];//力矩数组
        //int count = 0;
        //private double Fliter(double value)
        //{
        //    // 滤波后的数值
        //    double fliterValue = value;
        //    // 这里滤波
        //    double min = 0, max = 0;
        //    if (count < forces.Length)//前N个点不做处理
        //    {
        //        forces[count] = value;
        //        count++;
        //        return fliterValue;
        //    }

        //    //后N个点平滑处理
        //    sum = 0;
        //    for (int x = 0; x < forces.Length; x++) { sum += forces[x]; }//N个点累加
        //    for (int z = 0; z < forces.Length; z++)
        //    {
        //        if (z == 0)
        //        {
        //            min = forces[0];
        //            max = forces[0];
        //        }
        //        else
        //        {
        //            if (value > max) { max = value; }
        //            if (value < min) { min = value; }
        //        }
        //    }

        //    sum = sum - max;
        //    sum = sum - min;

        //    //重新赋值
        //    for (int y = 0; y < forces.Length - 1; y++) { forces[y] = forces[y + 1]; }
        //    forces[forces.Length - 1] = value;

        //    //力矩数值
        //    value = sum / (forces.Length - 2);
        //    count++;

        //    return fliterValue;
        //}

    }
}
