using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace ShockAnalyze
{
    public partial class TestParamForm : DevExpress.XtraEditors.XtraForm
    {
        // 速度列表
        DataTable dtSpeedList,dtTempList;
        // 测试参数设置
        public TestSetting testSetting = new TestSetting();
        public TestSetting Test_Setting
        {
            get { return this.testSetting; }
            set { this.testSetting = value; }
        }

        Language language = null;

        // 构造函数
        public TestParamForm(Language language)
        {
            InitializeComponent();

            this.Size = new Size(530, 430);

            // 创建列表
            dtSpeedList = new DataTable();
            
            // 添加列
            dtSpeedList.Columns.Add("index", typeof(string));
            dtSpeedList.Columns.Add("speed", typeof(string));
            dtSpeedList.Columns.Add("frequency", typeof(string));

            dtTempList = new DataTable();
            dtTempList.Columns.Add("index", typeof(string));
            dtTempList.Columns.Add("speed", typeof(string));
            dtTempList.Columns.Add("Fmf", typeof(string));
            dtTempList.Columns.Add("Fmy", typeof(string));

            this.language = language;
            BindText(language);
        }

        private void BindText(Language language)
        {
            this.Text = language.FindText("测试参数");

            tabNavigationPage1.Caption = language.FindText("样件参数");
            labelControl1.Text = "*" + language.FindText("减震器名称") + ":";
            labelControl2.Text = language.FindText("减振器编号") + ":";
            labelControl3.Text = language.FindText("车 辆 名 称") + ":";
            labelControl4.Text = language.FindText("试 验 地 点") + ":";
            labelControl5.Text = language.FindText("压缩力指标") + ":";
            labelControl6.Text = language.FindText("复原力指标") + ":";
            labelControl12.Text = language.FindText("活 塞 直 径") + ":";
            labelControl11.Text = language.FindText(" 安 装 位 置") + ":";
            labelControl10.Text = language.FindText("压 缩 设 置") + ":";
            labelControl9.Text = language.FindText("复 原 设 置") + ":";
            labelControl8.Text = language.FindText("预加载设置") + ":";
            labelControl7.Text = language.FindText("备         注") + ":";

            tabNavigationPage2.Caption = language.FindText("温度参数");
            labelControl13.Text = language.FindText("当前温度") + ":";
            labelControl14.Text = language.FindText("初始温度") + ":";
            labelControl15.Text = language.FindText("最大温度") + ":";
            labelControl16.Text = language.FindText("*注意:初始温度不能超过最大温度");
            cbPreheating.Text = language.FindText("试验前预热");


            tabNavigationPage3.Caption = language.FindText("排气") + "&" + language.FindText("摩擦力测试参数");
            labelControl20.Text = language.FindText("运行行程") + ":";
            labelControl39.Text = language.FindText("排气测试") + ":";
            labelControl21.Text = language.FindText("运行频率") + ":";
            labelControl22.Text = language.FindText("运行圈数") + ":";
            labelControl25.Text = language.FindText("圈");
            labelControl41.Text = language.FindText("摩擦力测试") + ":";
            labelControl30.Text = language.FindText("运行速度") + ":";
            labelControl29.Text = language.FindText("运行圈数") + ":";
            labelControl26.Text = language.FindText("圈");
            cedtAutoCalculate.Text = language.FindText("加入测试结果计算");

            tabNavigationPage5.Caption = language.FindText("测试速度参数");
            labelControl32.Text = language.FindText("行程") + ":";
            labelControl33.Text = language.FindText("速度") + ":";
            labelControl36.Text = language.FindText("目标频率") + ":";
            sbtnAdd.Text = language.FindText("添加");
            sbtnEdit.Text = language.FindText("修改");
            sbtnDelete.Text = language.FindText("删除");
            gridColumn1.Caption = language.FindText("序号");
            gridColumn3.Caption = language.FindText("速度")+"(m/s)";
            gridColumn5.Caption = language.FindText("目标频率") + "(Hz)";

            tabNavigationPage4.Caption = language.FindText("目标设定");
            cbAddChart.Text = language.FindText("加入图表显示");
            sbtnAdd1.Text = language.FindText("添加");
            sbtnDelete1.Text = language.FindText("删除");
            labelControl28.Text = language.FindText("点击行输入(数值1,数值2)");
            gridColumn2.Caption = language.FindText("序号");
            gridColumn4.Caption = language.FindText("速度") + "(m/s)";
            gridColumn6.Caption = language.FindText("复原力(N)范围");
            gridColumn7.Caption = language.FindText("压缩力(N)范围");

            cbtnSubmit.Text = language.FindText("确定");
            cbtnClose.Text= language.FindText("关闭");

        }

        // 窗体加载
        private void TestParamForm_Load(object sender, EventArgs e)
        {
            ShowTestParams();
        }

        // 显示测试参数
        private void ShowTestParams()
        {
            // 减震器信息
            tedtShockName.Text = testSetting.Shock_Info.Shock_Name;
            tedtShockNum.Text = testSetting.Shock_Info.Shock_Num;
            tedtCarName.Text = testSetting.Shock_Info.Vehicle;
            tedtTestLocation.Text = testSetting.Shock_Info.Location;
            tedtCompressionIndicator.Text = testSetting.Shock_Info.Compression_Valving;
            tedtResilienceIndicator.Text = testSetting.Shock_Info.Rebound_Valving;
            medtNotes.Text = testSetting.Shock_Info.Notes;
            sedtDiameter.Text = testSetting.Shock_Info.Diameter;
            cbedtInstallationPosition.Text = testSetting.Shock_Info.Installation_Position;
            tedtCompressSetting.Text = testSetting.Shock_Info.Compression_Setting;
            tedtResilienceSetting.Text = testSetting.Shock_Info.Rebound_Setting;
            tedtPreloadSetting.Text = testSetting.Shock_Info.Preload_Setting;

            // 温度信息
            sedtInitTemperature.EditValue = testSetting.InitTemperature;
            sedtMaxTemperature.EditValue = testSetting.MaxTemperature;
            cbPreheating.Checked = testSetting.isPreheating;

            tpbInitTemperature.TempVaue = testSetting.InitTemperature;
            tpbInitTemperature.Value = 100 * (testSetting.InitTemperature + 40) / 140;
            
            tpbMaxTemperature.TempVaue = testSetting.MaxTemperature;
            tpbMaxTemperature.Value = 100 * (testSetting.MaxTemperature + 40) / 140;

            // 行程
            cmbTravel1.Text = testSetting.Travel.ToString();
            cmbTravel2.Text = testSetting.Travel.ToString();

            // 排气测试
            sedtFrequency1.Text = testSetting.ExhaustFrequency.ToString();
            sedtCircle1.Text = testSetting.ExhaustCircle.ToString();

            // 摩擦力测试
            sedtSpeed1.Text = testSetting.FrictionSpeed.ToString();
            sedtCircle2.Text = testSetting.FrictionCircle.ToString();
            cedtAutoCalculate.Checked = testSetting.isAutoCalculate;

            // 测试速度
            for (int i = 0; i < testSetting.TestSpeeds.Count; i++)
            {
                TestSpeed testSpeed = testSetting.TestSpeeds[i];
                DataRow row = dtSpeedList.NewRow();
                row[0] = (i+1) + "";
                row[1] = testSpeed.Speed.ToString();
                row[2] = testSpeed.Frequency.ToString();
                dtSpeedList.Rows.Add(row);
            }

            gridControlSpeedList.DataSource = dtSpeedList;

            // 对标数据
            cbAddChart.Checked = testSetting.isNeedTemp;
            for (int i = 0; i < testSetting.Temps1.Count; i++)
            {
                Temp Temp1 = testSetting.Temps1[i];
                Temp Temp2 = testSetting.Temps2[i];
                DataRow row = dtTempList.NewRow();
                row[0] = (i+1) + "";
                row[1] = Temp1.Speed.ToString();
                row[2] = Temp1.Fmf + "," + Temp2.Fmf;
                row[3] = Temp1.Fmy + "," + Temp2.Fmy;
                dtTempList.Rows.Add(row);
            }

            gridControl1.DataSource = dtTempList;
        }

        // 赋值测试参数
        private void SetTestParams()
        {
            // 减震器信息
            testSetting.Shock_Info.Shock_Name=tedtShockName.Text;
            testSetting.Shock_Info.Shock_Num=tedtShockNum.Text;
            testSetting.Shock_Info.Vehicle=tedtCarName.Text;
            testSetting.Shock_Info.Location=tedtTestLocation.Text;
            testSetting.Shock_Info.Compression_Valving=tedtCompressionIndicator.Text;
            testSetting.Shock_Info.Rebound_Valving=tedtResilienceIndicator.Text;
            testSetting.Shock_Info.Notes=medtNotes.Text;
            testSetting.Shock_Info.Diameter=sedtDiameter.Text;
            testSetting.Shock_Info.Installation_Position=cbedtInstallationPosition.Text;
            testSetting.Shock_Info.Compression_Setting=tedtCompressSetting.Text;
            testSetting.Shock_Info.Rebound_Setting=tedtResilienceSetting.Text;
            testSetting.Shock_Info.Preload_Setting=tedtPreloadSetting.Text;

            // 温度信息
            testSetting.InitTemperature=int.Parse(sedtInitTemperature.EditValue.ToString());
            testSetting.MaxTemperature = int.Parse(sedtMaxTemperature.EditValue.ToString());
            testSetting.isPreheating=cbPreheating.Checked;

            // 行程
            testSetting.Travel=double.Parse(cmbTravel1.Text);

            // 排气测试
            testSetting.ExhaustFrequency=double.Parse(sedtFrequency1.Text);
            testSetting.ExhaustCircle=double.Parse(sedtCircle1.Text);

            // 摩擦力测试
            testSetting.FrictionSpeed=double.Parse(sedtSpeed1.Text);
            testSetting.FrictionCircle=double.Parse(sedtCircle2.Text);
            testSetting.isAutoCalculate=cedtAutoCalculate.Checked;

            // 测试速度
            testSetting.TestSpeeds.Clear();
            for (int i = 0; i < dtSpeedList.Rows.Count; i++)
            {
                DataRow dateRow = dtSpeedList.Rows[i];
                testSetting.TestSpeeds.Add(new TestSpeed(double.Parse(dateRow[1].ToString()), double.Parse(dateRow[2].ToString())));
            }

            // 对标数据
            testSetting.isNeedTemp=cbAddChart.Checked;
            testSetting.Temps1.Clear();
            testSetting.Temps2.Clear();
            for (int i = 0; i < dtTempList.Rows.Count; i++)
            {
                DataRow dateRow = dtTempList.Rows[i];
                double speed = double.Parse(dateRow[1].ToString());
                string[] strs1 = dateRow[2].ToString().Split(',');
                string[] strs2 = dateRow[3].ToString().Split(',');
                if (strs1.Length < 2 || strs2.Length < 2)
                {
                    testSetting.Temps1.Add(new Temp(speed, 0, 0));
                    testSetting.Temps2.Add(new Temp(speed, 0, 0));
                }

                testSetting.Temps1.Add(new Temp(speed, double.Parse(strs1[0]), double.Parse(strs2[0])));
                testSetting.Temps2.Add(new Temp(speed, double.Parse(strs1[1]), double.Parse(strs2[1])));
            }
        }

        // 初始温度编辑框
        private void sedtInitTemperature_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            int temperature = int.Parse(e.NewValue.ToString().Replace(".",""));
            if (temperature >= -40 && temperature <= 100)
            {
                tpbInitTemperature.TempVaue = temperature;
                tpbInitTemperature.Value = 100 * (temperature + 40) / 140;
            }
        }

        // 最大温度编辑框
        private void sedtMaxTemperature_EditValueChanging(object sender, DevExpress.XtraEditors.Controls.ChangingEventArgs e)
        {
            int temperature = int.Parse(e.NewValue.ToString().Replace(".", ""));
            if (temperature >= -40 && temperature <= 100)
            {
                tpbMaxTemperature.TempVaue = temperature;
                tpbMaxTemperature.Value = 100 * (temperature + 40) / 140;
            }
        }

        // 行程下拉选择
        double maxFrequency = 6.5;
        private void cmbTravel_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBoxEdit cmbTravel = sender as ComboBoxEdit;
            if (!cmbTravel1.Text.Equals(cmbTravel2.Text))
            {
                cmbTravel1.Text = cmbTravel.Text;
                cmbTravel2.Text = cmbTravel.Text;
            }

            this.sedtSpeed2_EditValueChanged(this, e);
            if (cmbTravel.Name.Equals("cmbTravel1")) { return; }

            double maxSpeed = MyMath.ConvertToSpeed(maxFrequency, double.Parse(cmbTravel.Text));
            for(int i = 0; i < dtSpeedList.Rows.Count; i++)
            {
                DataRow rowData = dtSpeedList.Rows[i];
                double speed = double.Parse(rowData[1].ToString());
                double frequency = MyMath.ConvertToFrequency(speed, double.Parse(cmbTravel.Text));

                if (speed >= maxSpeed)
                {
                    //isOver = true;
                    tabPane1.SelectedPageIndex = 3;
                    MessageDxUtil.ShowWarning(language.FindText("已经超过目前行程最大速度点") +":" + maxSpeed.ToString("F3"));
                    cmbTravel1.Text = testSetting.Travel.ToString();
                    cmbTravel2.Text = testSetting.Travel.ToString();
                    break;
                }


                // 这里要频率限制
                rowData[2] = frequency.ToString("F3");
            }
        }

        // 速度编辑框修改
        private void sedtSpeed2_EditValueChanged(object sender, EventArgs e)
        {
            try
            {
                double travel = double.Parse(cmbTravel2.Text);// 行程
                double speed = Convert.ToDouble(sedtSpeed2.EditValue);//速度

                //计算显示
                sedtFrequency2.Text = MyMath.ConvertToFrequency(speed, travel).ToString("F3");
            }
            catch (Exception ex)
            {
                Console.WriteLine("input:{0}", ex.ToString());
            }
        }

        // 列表行点击
        private void gridView1_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            int rowIndex = gridView1.FocusedRowHandle;
            DataRow row = gridView1.GetDataRow(rowIndex);
            sedtSpeed2.Text = row[1].ToString();
            sedtFrequency2.Text = row[2].ToString();
        }

        // 添加
        private void sbtnAdd_Click(object sender, EventArgs e)
        {
            double maxSpeed = MyMath.ConvertToSpeed(maxFrequency, double.Parse(cmbTravel2.Text));
            double speed=double.Parse(sedtSpeed2.EditValue.ToString());
            if (speed >= maxSpeed) 
            { 
                MessageDxUtil.ShowWarning(language.FindText("已经超过目前行程最大速度点") + ":" + maxSpeed.ToString("F3"));
                return;
            }

            DataRow rowData = dtSpeedList.NewRow();
            rowData[0] = dtSpeedList.Rows.Count + 1;
            rowData[1] = speed;
            rowData[2] = sedtFrequency2.EditValue;
            dtSpeedList.Rows.Add(rowData);
            gridView1.FocusedRowHandle = dtSpeedList.Rows.Count - 1;
        }

        // 修改
        private void sbtnEdit_Click(object sender, EventArgs e)
        {
            double maxSpeed = MyMath.ConvertToSpeed(maxFrequency, double.Parse(cmbTravel2.Text));
            double speed = double.Parse(sedtSpeed2.EditValue.ToString());
            if (speed >= maxSpeed)
            {
                MessageDxUtil.ShowWarning(language.FindText("已经超过目前行程最大速度点") + ":" + maxSpeed.ToString("F3"));
                return;
            }

            int rowindex = gridView1.FocusedRowHandle;
            DataRow rowData = gridView1.GetDataRow(rowindex);
            rowData[1] = sedtSpeed2.EditValue;
            rowData[2] = sedtFrequency2.EditValue;
        }

        // 删除
        private void sbtnDelete_Click(object sender, EventArgs e)
        {
            if (dtSpeedList.Rows.Count == 0) { return; }
            int rowindex = gridView1.FocusedRowHandle;
            DataRow rowData = gridView1.GetDataRow(rowindex);
            dtSpeedList.Rows.Remove(rowData);

            for (int i = 0; i < dtSpeedList.Rows.Count; i++)
            {
                dtSpeedList.Rows[i][0] = (i + 1);
            }
        }


        // 提交信息
        private void cbtnSubmit_CheckedChanged(object sender, EventArgs e)
        {
            SetTestParams();
            this.DialogResult = DialogResult.OK;
        }

        // 关闭窗体
        private void cbtnClose_CheckedChanged(object sender, EventArgs e)
        {
            this.Close();
        }

        // 数采回调函数
        private int currentTemperature = 0;
        public void flush_data(object sender, NI_Daq.SampleDataEventArgs e)
        {
            currentTemperature =(int)e.data[0];
        }

        private void timerShow_Tick(object sender, EventArgs e)
        {
            // 范围限制
            if (currentTemperature < -40) { currentTemperature = -40; }
            if (currentTemperature >100) { currentTemperature =100; }

            // 显示
            tpbCurrentTemperature.TempVaue = currentTemperature;
            tpbCurrentTemperature.Value = 100 * (currentTemperature + 40) / 140;
        }

        private void gridView1_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (gridView1.GetRow(e.RowHandle) == null)
            {
                return;
            }

            e.Appearance.BackColor = Color.Transparent;
        }

        private void sbtnAdd1_Click(object sender, EventArgs e)
        {
            DataRow row = dtTempList.NewRow();
            row[0] = (dtTempList.Rows.Count + 1) + "";
            row[1] = "0";
            row[2] = "0,0";
            row[3] = "0,0";
            dtTempList.Rows.Add(row);
        }

        private void sbtnDelete1_Click(object sender, EventArgs e)
        {
            if (dtTempList.Rows.Count == 0) { return; }
            int rowindex = gridView2.FocusedRowHandle;
            DataRow rowData = gridView2.GetDataRow(rowindex);
            dtTempList.Rows.Remove(rowData);

            for (int i = 0; i < dtTempList.Rows.Count; i++)
            {
                dtTempList.Rows[i][0] = (i + 1);
            }
        }

    }
}
