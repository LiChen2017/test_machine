using System;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace ShockAnalyze
{
    public partial class ReportForm : DevExpress.XtraEditors.XtraForm
    {
        // 测试参数设置
        public SyetemSetting syetemSetting = new SyetemSetting();
        public SyetemSetting Syetem_Setting
        {
            get { return this.syetemSetting; }
            set { this.syetemSetting = value; }
        }

        // 标题
        public string title = "";
        public string Title
        {
            get { return this.title; }
            set { this.title = value; }
        }

        // 结果数据
        public ResultData resultData = new ResultData();
        public ResultData Result_Data
        {
            get { return this.resultData; }
            set { this.resultData = value; }
        }

        // 测试参数设置
        public TestSetting testSetting = new TestSetting();
        public TestSetting Test_Setting
        {
            get { return this.testSetting; }
            set { this.testSetting = value; }
        }

        // 测试信息列表数据
        DataTable dtTestInfo;
        // 图表类
        ChartManager chartManager = new ChartManager();

        Language language;

        // 构造函数
        public ReportForm(Language language)
        {
            InitializeComponent();

            this.Size = new Size(1066, 730);

            this.language = language;
            chartManager.InitChart(tChartF_D, false, true, language);
            chartManager.InitChart(tChartF_V, true, true, language);

            this.Text = language.FindText("生成报告");
            gridColumn1.Caption = language.FindText("速度") + "(m/s)";
            gridColumn2.Caption = language.FindText("复原") + language.FindText("力(N)");
            gridColumn6.Caption = language.FindText("压缩") + language.FindText("力(N)");

            labelControl1.Text = language.FindText("零点/峰值输出")+":";
            cbTemp.Text= language.FindText("目标对比");

            sbtnSubmit.Text= language.FindText("生成报告");
            sbtnClose.Text = language.FindText("关闭");
        }

        // 窗体加载
        private void ReportForm_Load(object sender, EventArgs e)
        {
            ShowResultData();
        }

        // 显示结果数据
        private void ShowResultData()
        {


            // 显示标题
            lblTitle.Text = title;
            cbTemp.Checked = testSetting.isNeedTemp;



            // 测试信息
            gridColumn4.Caption = language.FindText("参数");
            gridColumn5.Caption = language.FindText("数据");
            dtTestInfo = new DataTable();
            dtTestInfo.Columns.Add("key", typeof(string));
            dtTestInfo.Columns.Add("value", typeof(string));

            // 数据为空
            if (title.Length == 0) { return; }

            AddShockListItme(language.FindText("减震器名称"), resultData.ShockInf.Shock_Name);
            AddShockListItme(language.FindText("减振器编号"), resultData.ShockInf.Shock_Num);
            AddShockListItme(language.FindText("车 辆 名 称"), resultData.ShockInf.Vehicle);
            AddShockListItme(language.FindText("试 验 地 点"), resultData.ShockInf.Location);
            AddShockListItme(language.FindText("压缩力指标"), resultData.ShockInf.Compression_Valving);
            AddShockListItme(language.FindText("复原力指标"), resultData.ShockInf.Rebound_Valving);
            AddShockListItme(language.FindText("活 塞 直 径"), resultData.ShockInf.Diameter);
            AddShockListItme(language.FindText(" 安 装 位 置"), resultData.ShockInf.Installation_Position);
            AddShockListItme(language.FindText("压 缩 设 置"), resultData.ShockInf.Compression_Setting);
            AddShockListItme(language.FindText("复 原 设 置"), resultData.ShockInf.Rebound_Setting);
            AddShockListItme(language.FindText("预加载设置"), resultData.ShockInf.Preload_Setting);
            AddShockListItme(language.FindText("备         注"), resultData.ShockInf.Notes);
            AddShockListItme(language.FindText("充气力(N)"), resultData.Fc);
            AddShockListItme(language.FindText("摩擦力(N)"), resultData.Fm);

            gridControlTestInfo.DataSource = dtTestInfo;

         

            // 显示图表
            chartManager.ShowChart(tChartF_D, "F_D", false, resultData, false);
            chartManager.AddTemp(tChartF_V, testSetting);
            chartManager.ShowChart(tChartF_V, "F_V", true, resultData, false);
          
            AddRow();
        }

        private void AddRow()
        {
            // 测试结果
            DataTable dtResults = new DataTable();
            dtResults.Columns.Add("speed", typeof(string));
            dtResults.Columns.Add("Fmf", typeof(string));
            dtResults.Columns.Add("Fmy", typeof(string));

            int idx = 0;
            double Fc = Math.Abs(double.Parse(resultData.Fc));
            for (int i = 0; i < resultData.AnalyzeDatas.Count; i++)
            {
                AnalyzeData analyzeData = resultData.AnalyzeDatas[i];
                DataRow row = dtResults.NewRow();
                row[0] = analyzeData.ActSpeed.ToString("F2");
                row[1] = analyzeData.ReboundForce;
                row[2] = analyzeData.CompForce;

                if (cbMinMax.IsOn)
                {
                    analyzeData.GetRange();
                    row[1] = (analyzeData.MaxForce + Fc).ToString("F2");
                    row[2] = (analyzeData.MinForce + Fc).ToString("F2");
                }

                if (testSetting.isNeedTemp && testSetting.Temps1.Count > idx)
                {
                    Temp temp1 = testSetting.Temps1[idx];
                    Temp temp2 = testSetting.Temps2[idx];
                    if (analyzeData.Speed == temp1.Speed)
                    {
                        row[1] = row[1] + "(" + temp1.Fmf + "," + temp2.Fmf + ")";
                        row[2] = row[2] + "(" + temp1.Fmy + "," + temp2.Fmy + ")";
                        idx++;
                    }
                }

                dtResults.Rows.Add(row);
            }

            gridControlResults.DataSource = dtResults;
        }

        // 添加减震器信息Item
        private void AddShockListItme(string name, string value)
        {
            DataRow row = dtTestInfo.NewRow();
            row[0] = name;
            row[1] = value;
            dtTestInfo.Rows.Add(row);
        }

        // 生成报告
        private void sbtnSubmit_Click(object sender, EventArgs e)
        {
            // 保存对话框
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Title = language.FindText(language.FindText("生成报告"));
            saveFileDialog.DefaultExt = "pdf";
            saveFileDialog.FileName = title + "-" + DateTime.Now.ToString("yyyyMMddHHmmss");
            saveFileDialog.Filter = "(*.pdf)|*.pdf";//文件类型为.doc
            string currentDirectory = syetemSetting.ProjectDirectory;
            saveFileDialog.InitialDirectory = currentDirectory;
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                // 鼠标样式变等待
                this.Cursor = Cursors.WaitCursor;

                string sourcePath = GlobalData.BasePath + "\\TempDoc\\" + title + "-" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".doc";

                //图表转成英文
                //chartManager.ChangeXYLabel(tChartF_D, "Displacement(mm)", "Force(N)");
                //chartManager.ChangeXYLabel(tChartF_V, "Velocity(m/s)", "Force(N)");

                bool isSucess = false;
                try
                {
                    //输出报告
                    Report report = new Report();
                    report.OutputReport(title, sourcePath, saveFileDialog.FileName, tChartF_D, tChartF_V, resultData, testSetting, cbMinMax.IsOn);
                    isSucess = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message.ToString());
                    MessageDxUtil.ShowError(language.FindText("生成报告错误"));
                }

                //结束图表转换中文
                //chartManager.ChangeXYLabel(tChartF_D, "位移(mm)", "力(N)");
                //chartManager.ChangeXYLabel(tChartF_V, "速度(m/s)", "力(N)");

                // 鼠标样式正常
                this.Cursor = Cursors.Default;

                //打开生成好的Word
                if (isSucess)
                {
                    DialogResult dr = MessageDxUtil.ShowYesNoAndTips(language.FindText("是否打开文档")+"?");
                    if (dr == DialogResult.Yes) { Process.Start(saveFileDialog.FileName); }// report.OpenWordFile(saveFileDialog.FileName);
                }
            }
        }

        // 关闭界面
        private void sbtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void gridView1_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (gridView1.GetRow(e.RowHandle) == null)
            {
                return;
            }

            e.Appearance.BackColor = Color.White;
        }

        private void gridView2_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (gridView2.GetRow(e.RowHandle) == null)
            {
                return;
            }

            if (tChartF_D.Series.Count > e.RowHandle)
            {
                e.Appearance.ForeColor = tChartF_D.Series[e.RowHandle].Color;
            }

            e.Appearance.BackColor = Color.White;
        }

        // 目标对比
        private void cbTemp_CheckedChanged(object sender, EventArgs e)
        {
            testSetting.isNeedTemp = cbTemp.Checked;
            chartManager.AddTemp(tChartF_V, testSetting);
            AddRow();
        }

        // 峰值输出
        private void cbMinMax_Toggled(object sender, EventArgs e)
        {
            AddRow();
            chartManager.RemoveSeries(tChartF_V, "F_V");
            chartManager.ShowChart(tChartF_V, "F_V", true, resultData, cbMinMax.IsOn);
        }

    }
}
