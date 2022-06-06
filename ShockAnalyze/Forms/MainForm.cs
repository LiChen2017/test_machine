using DevExpress.XtraBars;
using DevExpress.XtraBars.Ribbon;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace ShockAnalyze
{
    public partial class MainForm : RibbonForm
    {
        #region 参数声明

        // 文件列表
        private DataTable dtFileList;
        // 选中文件字典
        private Dictionary<string, string> dicChecks = new Dictionary<string, string>();
        // 测试参数设置
        private TestSetting currentTestSetting;
        // 系统设置
        private SyetemSetting currentSyetemSetting;
        // 当前选中结果数据
        private ResultData currentResult;
        // 当前选中的文件名
        private string currentTitle;
        // NI数采
        private NI_Daq daq;
        // 电机
        private MotorDeveice motorDeveice; //电机工具类
        // 图表类
        ChartManager chartManager=new ChartManager();

        private PointF[] points = new PointF[0];

        #endregion

        #region 窗体事件

        // 构造函数
        public MainForm()
        {
            InitializeComponent();

            // 文件数据读取
            DeserializationTestParam();
            DeserializationSystemSetting();

            int type = currentSyetemSetting.LanguageType;
            language.ReadLanguage(type);
            bhiShowLog.Caption = language.FindText("没有操作信息");
            BindText(type);

            Init();
        }

        // 窗体加载
        private void MainForm_Load(object sender, EventArgs e)
        {
        
        }

        // 窗体改变大小
        private void MainForm_SizeChanged(object sender, System.EventArgs e)
        {
            // Logo位置
            int x = this.Width - picBoxLogo.Size.Width - 12;
            int y = ribControlTop.Size.Height - picBoxLogo.Size.Height;
            picBoxLogo.Location = new Point(x, y);

            // 分析结果列表的大小
            int w = gridControlTestResults.Size.Width;
            int h=panelControlRight.Height-(gridControlTestResults.Location.Y-panelControlRight.Location.Y);
            gridControlTestResults.Size = new Size(w,h);

            // Fm&Fc
            panelControl2.Location = new Point(panelControl2.Location.X, lblFc.Location.Y - 10);
            labelControl10.Location = new Point(labelControl10.Location.X, gridControlTestResults.Location.Y-43); //308 351
            lblFc.Location = new Point(lblFc.Location.X, gridControlTestResults.Location.Y - 43);//308
            labelControl13.Location = new Point(labelControl13.Location.X, gridControlTestResults.Location.Y -23);//328
            lblFm.Location = new Point(lblFm.Location.X, gridControlTestResults.Location.Y - 23);//328

            // 文件列表的大小
            int w1 = gridControlFiles.Size.Width;
            int h1 = panelControlLeft.Height - gridControlFiles.Location.Y;
            gridControlFiles.Size = new Size(w1, h1);
        }

        // 窗体关闭
        private void MainForm_FormClosing(object sender,FormClosingEventArgs e)
        {
            // 退出对话框
            DialogResult dr = MessageDxUtil.ShowYesNoAndWarning(language.FindText("确定退出系统?"));
            if (dr == DialogResult.No) // 取消
            {
                e.Cancel = true;
            }

            motorDeveice.Relese();
            // 点击确定继续执行,关闭硬件
            if (daq != null) { daq.Relese (); } // 数采释放
        }

        #endregion

        #region 头部菜单 BarButtonItem Click

        // 显示文件
        private void btiShow_ItemClick(object sender, ItemClickEventArgs e)
        {
            int rowIndex = gridViewShowFile.FocusedRowHandle;
            DataRow row = gridViewShowFile.GetDataRow(rowIndex);
            string filePath = row["filePath"].ToString();
            Process.Start("notepad.exe", filePath);
        }

        // 删除文件
        private void btiDelete_ItemClick(object sender, ItemClickEventArgs e)
        {
            int rowIndex = gridViewShowFile.FocusedRowHandle;
            DataRow row = gridViewShowFile.GetDataRow(rowIndex);
            string filePath = row["filePath"].ToString();
            string fileName = Path.GetFileNameWithoutExtension(filePath);
            if (File.Exists(filePath))
            {
                DialogResult result = MessageDxUtil.ShowYesNoAndTips(language.FindText("确定删除") +":"+ fileName);
                if (result == DialogResult.Yes)
                {
                    try
                    {
                        File.Delete(filePath);
                        if (dicChecks.ContainsKey(filePath)) { dicChecks.Remove(filePath); }
                        points = new PointF[0];
                        ClearShowInfo();
                        int index = 0;
                        for (int i=0;i<dtFileList.Rows.Count;i++)
                        {
                            DataRow dataRow = dtFileList.Rows[i];
                            if (dataRow["filePath"].ToString().Equals(filePath))
                            {
                                index = i;
                                break;
                            }
                        }

                        dtFileList.Rows.RemoveAt(index);
                        bhiShowLog.Caption= language.FindText("删除")+"[" +fileName+"]";
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.ToString());
                        MessageDxUtil.ShowWarning(language.FindText("删除失败"));
                    }
                }
            }
        }

        // 路径选择
        private void btiFloder_ItemClick(object sender, ItemClickEventArgs e)
        {
            // 文件浏览对话框
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.SelectedPath = currentSyetemSetting.ProjectDirectory;
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {                
                ReadFolder(folderBrowserDialog.SelectedPath,"");
                currentSyetemSetting.ProjectDirectory = folderBrowserDialog.SelectedPath;
                SerializationSystemSetting();
                this.Text = language.FindText("示功机") + "(" + folderBrowserDialog.SelectedPath + ")";
                bhiShowLog.Caption = language.FindText("项目当前路径") +":" + folderBrowserDialog.SelectedPath;
            }
        }

        // 打开文件
        private void btiOpenFile_ItemClick(object sender, ItemClickEventArgs e)
        {
            //文件选择
            openFileDialog.Multiselect = true;//文件多选
            openFileDialog.Filter = language.FindText("数据结果文件") +"(*.dat)|*.dat";//文件类型为.dat
            openFileDialog.InitialDirectory = currentSyetemSetting.ProjectDirectory;//初始化选择路径

            //文件选中后操作
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                tChart.Series.Clear();

                //开始读取
                foreach (string filePath in openFileDialog.FileNames)
                {
                    string time=new DirectoryInfo(filePath).LastWriteTime.ToString("yyyy-MM-dd HH:mm:ss");
                    // 读取数据
                    ReadFile(filePath, true,time,true);
                    // 添加至列表
                    DataRow row = dtFileList.NewRow();
                    row[0] = true;
                    row[1] = Path.GetFileNameWithoutExtension(filePath);
                    row[2] = filePath;
                    row[3] = time;
                    dtFileList.Rows.Add(row);
                    bhiShowLog.Caption = language.FindText("打开文件") + ":" + filePath;
                }
                // 按事件倒序,最新的排在前面
                dtFileList.DefaultView.Sort = "time DESC";
                gridControlFiles.DataSource = dtFileList;
            }
        }

        // 文件重命名
        private void btiFileNamed_ItemClick(object sender, ItemClickEventArgs e)
        {
            try
            {
                Rename();
            }
            catch (Exception ex)
            {
                MessageDxUtil.ShowError(language.FindText("没有选中数据"));
            }
        }

        private void Rename()
        {
            // 获取选中项信息
            int rowIndex = gridViewShowFile.FocusedRowHandle;
            if (rowIndex < 0)
            {
                MessageDxUtil.ShowError(language.FindText("没有选中数据"));
                return;
            }
            DataRow row = gridViewShowFile.GetDataRow(rowIndex);
            string filePath = row["filePath"].ToString();
            string oldFileName = Path.GetFileNameWithoutExtension(filePath);

            // 重命名窗体
            NamedForm namedForm = new NamedForm(language);
            namedForm.FileName = oldFileName;
            if (namedForm.ShowDialog() == DialogResult.OK)
            {
                // 文件名称改变
                string newFileName = namedForm.FileName;
                string floderPath = Path.GetDirectoryName(filePath);
                bool isSuccess = ChangeFileName(floderPath, oldFileName, newFileName);

                // 执行成功
                if (isSuccess)
                {
                    ClearShowInfo();
                    if (dicChecks.ContainsKey(filePath)) { dicChecks.Remove(filePath); }
                    foreach (DataRow dataRow in dtFileList.Rows)
                    {
                        if (dataRow["fileName"].Equals(oldFileName))
                        {
                            dataRow["check"] = false;
                            dataRow["fileName"] = newFileName;
                            dataRow["filePath"] = floderPath + @"/" + newFileName + ".dat";
                            break;
                        }
                    }

                    bhiShowLog.Caption = "[" + oldFileName + "]" + language.FindText("修改名称为") + "[" + newFileName + "]";
                    MessageDxUtil.ShowTips(language.FindText("重命名成功"));
                }
            }
        }

        // 打开文件目录
        private void btnOpen_Floder_ItemClick(object sender, ItemClickEventArgs e)
        {
            // 获取选中项信息并打开文件所在目录
            int rowIndex = gridViewShowFile.FocusedRowHandle;
            if (rowIndex < 0)
            {
                MessageDxUtil.ShowError(language.FindText("没有选中数据"));
                return;
            }
            DataRow row = gridViewShowFile.GetDataRow(rowIndex);
            string filePath = row["filePath"].ToString();
            if (File.Exists(filePath)) { Process.Start("Explorer.exe", @"/select," + filePath); }
        }

        // 力-位移图表
        private void btiFD_ItemClick(object sender, ItemClickEventArgs e)
        {
            btiFV.Down = false;
            btiFD.Down = true;
            chartManager.InitChart(tChart, false,false,language);
            ShowChart();
        }

        // 力-速度图表
        private void btiFV_ItemClick(object sender, ItemClickEventArgs e)
        {
            btiFD.Down = false;
            btiFV.Down = true;
            chartManager.InitChart(tChart, true, false, language);
            ShowChart();
        }

        // 截图
        private void btiScreenshot_ItemClick(object sender, ItemClickEventArgs e)
        {
            // 获取选中项
            int rowIndex = gridViewShowFile.FocusedRowHandle;
            if (rowIndex < 0)
            {
                MessageDxUtil.ShowError(language.FindText("没有选中数据"));
                return;
            }
            DataRow row = gridViewShowFile.GetDataRow(rowIndex);
            string fileName = row["fileName"].ToString();

            // 保存对话框
            saveFileDialog1.DefaultExt = tChart.Export.Image.PNG.FileExtension;
            saveFileDialog1.FileName = fileName + DateTime.Now.ToString("yyyyMMddHHmmss") + "." + saveFileDialog1.DefaultExt;
            saveFileDialog1.Filter = Steema.TeeChart.Texts.PNGFilter;
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                // 面板截图
                int width = panelCenter.Width;
                int height = panelCenter.Height;
                Bitmap sourceBitmap = new Bitmap(width, height);
                panelCenter.DrawToBitmap(sourceBitmap, new Rectangle(0, 0, width, height));
                sourceBitmap.Save(saveFileDialog1.FileName);
                MessageDxUtil.ShowTips(language.FindText("截图完成"));
            }
        }

        // 初始载荷
        private void btiInitLoad_ItemClick(object sender, ItemClickEventArgs e)
        {
            daq.Start();
            InitLoadForm initLoadForm = new InitLoadForm(language);
            initLoadForm.testSetting = currentTestSetting;
            NI_Daq.OnSampleDataHandler onSampleDataHandler = new NI_Daq.OnSampleDataHandler(initLoadForm.flush_data);
            daq.Event_SampleData += onSampleDataHandler;
            if (initLoadForm.ShowDialog() == DialogResult.Cancel)
            {
                daq.Stop();
                daq.Event_SampleData -= onSampleDataHandler;
                SerializationTestParam();
            }
        }

        // 设置测试参数
        private void btiTestParam_ItemClick(object sender, ItemClickEventArgs e)
        {
            daq.Start();
            TestParamForm testParamForm = new TestParamForm(language);
            // 数采回调数据
            NI_Daq.OnSampleDataHandler onSampleDataHandler = new NI_Daq.OnSampleDataHandler(testParamForm.flush_data);
            daq.Event_SampleData += onSampleDataHandler;
            testParamForm.testSetting = currentTestSetting;
            if (testParamForm.ShowDialog() == DialogResult.OK)
            {
                daq.Stop();
                daq.Event_SampleData -= onSampleDataHandler;
                SerializationTestParam();
                tsTemp.Checked = currentTestSetting.isNeedTemp;
                if (btiFV.Down) { chartManager.AddTemp(tChart, currentTestSetting); }
                MessageDxUtil.ShowTips(language.FindText("设置成功"));
            }
        }

        // 测试界面
        TestForm testForm = null;
        private void btiTest_ItemClick(object sender, ItemClickEventArgs e)
        {
            daq.Start();
            testForm = new TestForm(language);
            // 数采回调数据
            NI_Daq.OnSampleDataHandler onSampleDataHandler = new NI_Daq.OnSampleDataHandler(testForm.flush_data);
            daq.Event_SampleData += onSampleDataHandler;
            // 调用数采类
            TestForm.OnDaqHandler onDaqEvent = new TestForm.OnDaqHandler(OnDaq);
            testForm.OnDaqEvent += onDaqEvent;
            // 测试结束回调
            testForm.OnTestFinishEvent += new TestForm.OnTestFinishHandler(TestFinish);
            // 参数赋值
            testForm.testSetting = currentTestSetting;
            testForm.syetemSetting = currentSyetemSetting;
            if (testForm.ShowDialog() == DialogResult.Cancel)
            {
                daq.Stop();
                testForm.OnDaqEvent -= onDaqEvent;
                testForm = null;
                daq.Event_SampleData -= onSampleDataHandler;
            }
        }

        // 生成报告
        private void btiReport_ItemClick(object sender, ItemClickEventArgs e)
        {
            ReportForm reportForm = new ReportForm(language);
            // 获取选中项信息
            int rowIndex = gridViewShowFile.FocusedRowHandle;
            if (rowIndex >= 0)
            {
                DataRow row = gridViewShowFile.GetDataRow(rowIndex);
                ReadFile(row["filePath"].ToString(), false, row["time"].ToString(), false);
                reportForm.title = currentTitle;
                reportForm.resultData = currentResult;
            }
            reportForm.syetemSetting = currentSyetemSetting;
            reportForm.testSetting = currentTestSetting;

            reportForm.ShowDialog();
        }

        // 导入设置文件
        private void btiImport_ItemClick(object sender, ItemClickEventArgs e)
        {
            //文件选择
            openSettingDialog.Multiselect = false;//文件多选
            openSettingDialog.Filter = language.FindText("测试参数文件") +"(*.cfg)|*.cfg";//文件类型为.dat

            //文件选中后操作
            if (openSettingDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    File.Copy(openSettingDialog.FileName, GlobalData.BasePath + "//TestParams.cfg", true);
                    //DeserializationSystemSetting();
                    DeserializationTestParam();
                    MessageDxUtil.ShowTips(language.FindText("导入成功"));
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Import:" + ex.ToString());
                    MessageDxUtil.ShowError(language.FindText("导入失败"));
                }
            }
        }

        // 导出设置文件
        private void btiExport_ItemClick(object sender, ItemClickEventArgs e)
        {
            // 设置文件类型 
            saveSettingDialog.Filter = language.FindText("设置文件") +"(*.cfg)|*.cfg";
            saveSettingDialog.FileName = language.FindText("测试参数文件")+"-" + DateTime.Now.ToString("yyyyMMddHHmmss");

            // 点了保存按钮进入 
            if (saveSettingDialog.ShowDialog() == DialogResult.OK)
            {
                //输出xml文件
                string destFilePath = saveSettingDialog.FileName.ToString(); //获得文件路径 
                string newFilePath = destFilePath.Remove(destFilePath.LastIndexOf(".")) + ".cfg";
                File.Copy(GlobalData.BasePath + "//TestParams.cfg", newFilePath, true);

                MessageDxUtil.ShowTips(language.FindText("导出成功"));
            }
        }

        // 设置界面
        private void btiSetting_ItemClick(object sender, ItemClickEventArgs e)
        {
            SettingForm settingForm = new SettingForm(language, currentSyetemSetting.LanguageType);
            settingForm.syetemSetting = currentSyetemSetting;
            if(settingForm.ShowDialog() == DialogResult.OK)
            {
                SerializationSystemSetting();
                BindText(currentSyetemSetting.LanguageType);
                bhiShowLog.Caption = language.FindText("语言切换");
                MessageDxUtil.ShowTips(language.FindText("设置成功"));
            }
        }

        // 使用帮助
        private void btiHelp_ItemClick(object sender, ItemClickEventArgs e)
        {
            //if (File.Exists(GlobalData.BasePath + "//Help.pdf"))
            //{
            //    Process.Start(GlobalData.BasePath + "//Help.pdf");
            //}

            MessageDxUtil.ShowTips("Empty");
        }

        // 低通滤波开关
        private void tsFilter_CheckedChanged(object sender, ItemClickEventArgs e)
        {
            tChart.Series.Clear();
            ShowChart();
        }

        // 滤波系数变化
        private void BarEditItem_EditValueChanged(object sender, EventArgs e)
        {
            tChart.Series.Clear();
            ShowChart();
        }

        private void tsTemp_CheckedChanged(object sender, ItemClickEventArgs e)
        {
            currentTestSetting.isNeedTemp = tsTemp.Checked;
            if (btiFV.Down) { chartManager.AddTemp(tChart, currentTestSetting); }
        }

        #endregion

        #region 文件列表事件

        // 文件列表点击行
        private void gridViewShowFile_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            //int rowindex = e.RowHandle; //gridViewShowFile.FocusedRowHandle;
            //DataRow row = gridViewShowFile.GetDataRow(rowindex);

            int rowIndex = gridViewShowFile.FocusedRowHandle;
            DataRow row = gridViewShowFile.GetDataRow(rowIndex);
            ReadFile(row["filePath"].ToString(), false, row["time"].ToString(),true);
            bhiShowLog.Caption = language.FindText("当前选中第") + (rowIndex+1) + language.FindText("行")+":" + row["fileName"].ToString();
        }

        // 文件列表checkBox变化
        private void gridViewShowFile_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            // 获取选中项信息
            int rowIndex = gridViewShowFile.FocusedRowHandle;// e.RowHandle;
            DataRow row = gridViewShowFile.GetDataRow(rowIndex);
            string filePath = row["filePath"].ToString();
            string fileName=Path.GetFileNameWithoutExtension(filePath);

            // 点击Check状态
            bool isChecked = e == null ? true : (bool)e.Value;
            row["check"] = isChecked;
            if (isChecked) // 开始读取并显示图表
            {
                if (!dicChecks.ContainsKey(filePath)) 
                { 
                    dicChecks.Add(filePath, row["time"].ToString());
                    ReadFile(filePath, true, row["time"].ToString(), true);
                }
            }
            else // 移除显示
            {
                chartManager.RemoveSeries(tChart, fileName);
                if (dicChecks.ContainsKey(filePath)) { dicChecks.Remove(filePath); }
                points = new PointF[0];
                tChart.Refresh();
            }
            
            bhiShowLog.Caption = (isChecked ? language.FindText("显示") +"[" : language.FindText("不显示") +"[") + fileName + "]"+ language.FindText("曲线");
        }

        // 文件列表右击弹出对话框
        private void gridControlFiles_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                popupMenu.ShowPopup(Control.MousePosition);
            }
        }

        //  文件列表Cell
        private void gridViewShowFile_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (gridViewShowFile.GetRow(e.RowHandle) == null)
            {
                return;
            }

            e.Appearance.BackColor = Color.Transparent;
        }

        // 测试结果Cell
        private void gridViewTestResults_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (gridViewTestResults.GetRow(e.RowHandle) == null)
            {
                return;
            }

            e.Appearance.BackColor = Color.White;
        }

        #endregion

        #region 调用方法
        
        // 初始化操作
        private void Init()
        {
            // 初始图表F_D
            chartManager.InitChart(tChart,false,false, language);

            // 文件列表
            dtFileList = new DataTable();
            dtFileList.Columns.Add("check", typeof(bool));
            dtFileList.Columns.Add("fileName", typeof(string));
            dtFileList.Columns.Add("filePath", typeof(string));
            dtFileList.Columns.Add("time", typeof(string));

            ReadFolder(currentSyetemSetting.ProjectDirectory,"");

            // 显示
            //this.Text = "示功机" + "(" + currentSyetemSetting.ProjectDirectory + ")";

            // 检测tdms路径
            string tdmsPath = GlobalData.BasePath + "\\NI_Tdms";
            if (!Directory.Exists(tdmsPath))
            {
                Directory.CreateDirectory(tdmsPath);
            }

            tsTemp.Checked = currentTestSetting.isNeedTemp;

            // 电机
            bool isSuccess1 = false, isSuccess2 = false;
            motorDeveice = new MotorDeveice();
            isSuccess1 = motorDeveice.Init();

            // 数采
            daq = new NI_Daq();
            daq.onErrorHandler += new NI_Daq.OnErrorHandler(onError);
            isSuccess2=daq.InitDaq();
            if(isSuccess2)
            {
                daq.Start();
                daq.Stop();
            }

            // 硬件连接检测反馈
            if (!isSuccess1 && !isSuccess2)
            {
                MessageDxUtil.ShowError(language.FindText("电机数采集连接发生错误,请检查硬件连接后打开软件"));
            }
            else if (!isSuccess1)
            {
                MessageDxUtil.ShowError(language.FindText("电机连接发生错误,请检查电机连接后打开软件"));
            }
            else if (!isSuccess2)
            {
                MessageDxUtil.ShowError(language.FindText("数采连接发生错误,请检查数采连接后打开软件"));
            }

        }

        // 显示图表类型转换
        private void ShowChart()
        {
            if (btiFV.Down) 
            { 
                chartManager.AddTemp(tChart, currentTestSetting); 
            }
            foreach (string filePath in dicChecks.Keys)
            {
                string time = dicChecks[filePath];
                ReadFile(filePath, true, time, false);
            } 
        }

        // 显示试验信息
        private void ShowTestInfo(ResultData resultData, string fileName,string time)
        {
            // 文件名称
            lblFileName.Text = fileName;
            labelControl11.Text = language.FindText("文 件 名 称") + ":"+ fileName;

            // 测试信息
            labelControl1.Text = language.FindText("减震器名称") + ": "+ resultData.ShockInf.Shock_Name;
            labelControl2.Text = language.FindText("活 塞 直 径") + ": "+ resultData.ShockInf.Diameter;
            labelControl9.Text = language.FindText(" 安 装 位 置") + ": "+ resultData.ShockInf.Installation_Position;
            labelControl3.Text = language.FindText("压缩力指标") + ": "+ resultData.ShockInf.Compression_Valving;
            labelControl4.Text = language.FindText("复原力指标") + ": "+ resultData.ShockInf.Rebound_Valving;
            labelControl5.Text = language.FindText("试 验 地 点") + ": "+ resultData.ShockInf.Location;
            labelControl6.Text = language.FindText("试 验 时 间") + ": "+ time;
            labelControl7.Text = language.FindText("备         注") + ": "+ resultData.ShockInf.Notes;

            // Fc&Fm
            labelControl10.Text = language.FindText("充气力(N)") + ": "+ resultData.Fc;
            labelControl13.Text = language.FindText("摩擦力(N)") + ": " + resultData.Fm;

            //创建表
            DataTable dtTestResults = new DataTable();
            //添加列
            dtTestResults.Columns.Add("speed", typeof(string));
            dtTestResults.Columns.Add("Fmf", typeof(string));
            dtTestResults.Columns.Add("Fmy", typeof(string));

            //添加数据行
            for (int i = 0; i < resultData.AnalyzeDatas.Count; i++)
            {
                AnalyzeData analyzeData = resultData.AnalyzeDatas[i];
                DataRow row = dtTestResults.NewRow();
                row[0] = analyzeData.ActSpeed.ToString("F2");
                row[1] = analyzeData.ReboundForce;
                row[2] = analyzeData.CompForce;
                dtTestResults.Rows.Add(row);
            }

            // 测试结果列表
            gridControlTestResults.DataSource = dtTestResults;
        }

        // 清除显示信息
        private void ClearShowInfo()
        {
            chartManager.RemoveSeries(tChart, lblFileName.Text);
            // 文件名称
            labelControl11.Text = language.FindText("文 件 名 称") + ":";

            // 测试信息
            labelControl1.Text = language.FindText("减震器名称") + ":";
            labelControl2.Text = language.FindText("活 塞 直 径") + ":";
            labelControl9.Text = language.FindText(" 安 装 位 置") + ":";
            labelControl3.Text = language.FindText("压缩力指标") + ":";
            labelControl4.Text = language.FindText("复原力指标") + ":";
            labelControl5.Text = language.FindText("试 验 地 点") + ":";
            labelControl6.Text = language.FindText("试 验 时 间") + ":";
            labelControl7.Text = language.FindText("备         注") + ":";

            labelControl10.Text = language.FindText("充气力(N)") + ":";
            labelControl13.Text = language.FindText("摩擦力(N)") + ":";

            gridControlTestResults.DataSource = null;
        }

        // 读取文件夹的文件信息
        private void ReadFolder(string floderPath,string fileName)
        {
            tChart.Series.Clear();
            ClearShowInfo();
            dicChecks.Clear();
            dtFileList.Rows.Clear();
            DirectoryInfo folder = new DirectoryInfo(floderPath);
            foreach (FileInfo file in folder.GetFiles("*.dat"))
            {
                DataRow row = dtFileList.NewRow();
                row[0] = false;
                row[1] = Path.GetFileNameWithoutExtension(file.Name);
                row[2] = file.FullName;
                row[3] = file.LastWriteTime.ToString("yyyy-MM-dd HH:mm:ss");
                dtFileList.Rows.Add(row); 
            }

            dtFileList.DefaultView.Sort = "time DESC";
            gridControlFiles.DataSource = dtFileList;

            // 如果有文件数据,开始默认选中并读取第一个数据
            if (dtFileList.Rows.Count > 0) 
            {
                int index = 0;
                for (int i = 0; i < dtFileList.Rows.Count; i++)
                {
                    DataRow row = gridViewShowFile.GetDataRow(i);
                    //Console.WriteLine(row["filePath"].ToString() + "  " + fileName);
                    if (Path.GetFileNameWithoutExtension(row["filePath"].ToString()).Equals(fileName))
                    {
                        index= i;
                    }
                }

                this.gridViewShowFile.FocusedRowHandle = index;
                this.gridViewShowFile_CellValueChanging(this, null);
            }
        }

        // 读取文件
        private void ReadFile(string filePath,bool isShowChart,string time,bool isShowTestInfo)
        {
            if (!File.Exists(filePath))
            {
                dicChecks.Remove(filePath);
                return;
            }

            //读取文件
            string fileName = Path.GetFileNameWithoutExtension(filePath);
            string[] fileArray = File.ReadAllLines(filePath, UnicodeEncoding.GetEncoding("GB2312"));

            try
            {
                // 读取结果数据
                ResultData resultData = new ResultData(); 
                resultData.ReadResultData(fileArray);

                //显示测试信息
                if (isShowTestInfo) { ShowTestInfo(resultData, fileName, time); }
                // 显示图表
                if (isShowChart) 
                {
                    if (btiFV.Down) { chartManager.AddTemp(tChart, currentTestSetting); }
                    chartManager.SetParam(tsFilter.Checked, Convert.ToDouble(beiSamplingrate.EditValue), Convert.ToDouble(beiCutOff.EditValue));
                    chartManager.ShowChart(tChart,fileName, btiFV.Down,resultData,false);
                }

                // 选中信息,赋值全局,方便调用
                if (!isShowTestInfo && !isShowChart)
                {
                    currentResult = resultData;
                    currentTitle = fileName;
                }

                // 添加速度显示点
                if (btiFV.Down && dicChecks.ContainsKey(filePath))
                {
                    PointF[] currentPoints = new PointF[resultData.AnalyzeDatas.Count * 2];
                    for (int i = 0; i < resultData.AnalyzeDatas.Count; i++)
                    {
                        AnalyzeData analyzeData = resultData.AnalyzeDatas[i];
                        currentPoints[i] = new PointF((float)analyzeData.ActSpeed, (float)analyzeData.ReboundForce);
                        currentPoints[i + resultData.AnalyzeDatas.Count] = new PointF((float)analyzeData.ActSpeed, (float)analyzeData.CompForce);
                    }
                    points = currentPoints;
                }

                tChart.Refresh();

            }
            catch (Exception ex)
            {
                MessageDxUtil.ShowError(language.FindText("读取发生错误") + ":" + ex.Message);
            }
        }

        //修改文件名称
        private bool ChangeFileName(string floderPath, string oldFileName, string newFileName)
        {
            try
            {
                // 列表中的原始文件全路径名
                string oldStr = floderPath + @"/" + oldFileName + ".dat";

                // 新文件全路径名
                string newStr = floderPath + @"/" + newFileName + ".dat";

                // 改名方法
                FileInfo fi = new FileInfo(oldStr);
                fi.MoveTo(Path.Combine(newStr));
            }
            catch (Exception ex)
            {
                MessageDxUtil.ShowTips(language.FindText("重名名失败")+":" + ex.Message.ToString());
                return false;
            }

            return true;
        }

        //序列化_测试参数
        private void SerializationTestParam()
        {
            BinarySerialize<TestSetting> serialize = new BinarySerialize<TestSetting>();
            string filePath = GlobalData.BasePath+"//TestParams.cfg";
            serialize.Serialize(currentTestSetting, filePath);
        }

        //反序列化_测试参数
        private void DeserializationTestParam()
        {
            BinarySerialize<TestSetting> serialize = new BinarySerialize<TestSetting>();
            TestSetting testSetting = serialize.DeSerialize(GlobalData.BasePath + "//TestParams.cfg");
            currentTestSetting = testSetting != null?testSetting:new TestSetting();
        }

        //序列化_系统设置
        private void SerializationSystemSetting()
        {
            BinarySerialize<SyetemSetting> serialize = new BinarySerialize<SyetemSetting>();
            string filePath = GlobalData.BasePath + "//SysSetting.mzs";
            serialize.Serialize(currentSyetemSetting, filePath);
        }

        //反序列化_系统设置
        private void DeserializationSystemSetting()
        {
            BinarySerialize<SyetemSetting> serialize = new BinarySerialize<SyetemSetting>();
            SyetemSetting syetemSetting = serialize.DeSerialize(GlobalData.BasePath + "//SysSetting.mzs");
            currentSyetemSetting = syetemSetting != null ? syetemSetting : new SyetemSetting();

            if (currentSyetemSetting.ProjectDirectory.Length == 0 ||!Directory.Exists(currentSyetemSetting.ProjectDirectory))
            {
                currentSyetemSetting.ProjectDirectory = GlobalData.BasePath + "\\Data";
            }
        }
        
        #endregion

        #region 其他

        // 图表点标注绘制
        private void tChart_AfterDraw(object sender, Steema.TeeChart.Drawing.Graphics3D g)
        {
            if (!btiFV.Down) { return; }

            Color color = Color.Red;

            //文字样式
            g.Font.Color = color;
            g.Font.Bold = true;
            g.Font.Size = 10;
            g.TextAlign = StringAlignment.Near;

            //画笔
            g.Pen.Color = color;
            g.Pen.Width = 3;
            g.BackColor = Color.Transparent;

            //画点
            int offset = 0, ratio = 1;
            for (int i = 0; i < points.Length; i++)
            {
                if (i <= 1)
                {
                    ratio = i % 2 == 0 ? -1 : 0;
                    offset = i % 2 == 0 ? -15 : 15;
                }
                else
                {
                    ratio = i % 2 == 0 ? 0 : -1;
                    offset = i % 2 == 0 ? 15 : -15;
                }

                //绘制圆
                PointF point = points[i];
                int x = tChart.Axes.Bottom.CalcXPosValue(point.X);
                int y = tChart.Axes.Left.CalcYPosValue(point.Y);
                g.Ellipse(new Rectangle(x - 3, y - 3, 6, 6));

                //绘制文字
                string text = "(" + point.X.ToString("F2") + " , " + point.Y.ToString("F2") + ")";
                int width = (int)g.TextWidth(text);
                int height = (int)g.TextHeight(text);
                g.TextOut(x - width*3/5, y + ratio * height + offset, text);

            }
        }

        // 测试完成回调
        public void TestFinish(string filePath)
        {
            daq.Stop();
            ReadFolder(currentSyetemSetting.ProjectDirectory, Path.GetFileNameWithoutExtension(filePath));
        }

        // 测试界面回调,分段采集
        private void OnDaq(string name)
        {
            daq.RecordFile(name);
        }

        // 显示提示错误信息
        private void onError()
        {
            //string errorMsg = isMotorState ? "数采连接存在问题" : "数采和电机连接存在问题";
            //bhiShowLog.Caption = errorMsg;
            //MessageDxUtil.ShowError(errorMsg);

            if (testForm != null) { testForm.onError(); }
            daq.Reset();
        }

        #endregion

        Language language = new Language();
        private void BindText(int languageType)
        {
            language.ChangeLanguage(languageType == 1 ? "English" : "Chinese");
            this.Text = language.FindText("示功机") + "(" + currentSyetemSetting.ProjectDirectory + ")";

            ribbonPageGroup2.Text = language.FindText("文件");
            btiFloder.Caption = language.FindText("路径选择");
            btiOpenFile.Caption = language.FindText("打开");
            btnOpenFloder.Caption = language.FindText("文件目录");
            btiFileNamed.Caption = language.FindText("重命名");

            ribbonPageGroup5.Text = language.FindText("图表");
            btiFD.Caption = language.FindText("力-位移");
            btiFV.Caption = language.FindText("力-速度");
            btiScreenshot.Caption = language.FindText("截图");
            tsTemp.Caption= language.FindText("目标对比");

            ribbonPageGroup6.Text= language.FindText("测试");
            btiTestParam.Caption = language.FindText("试验参数");
            btiInitLoad.Caption = language.FindText("初始载荷");
            btiTest.Caption = language.FindText("试验测试");
            btiReport.Caption = language.FindText("生成报告");

            ribbonPageGroup7.Text = language.FindText("设置");
            btiImport.Caption = language.FindText("导入测试参数");
            btiExport.Caption = language.FindText("导出测试参数");
            btiSetting.Caption = language.FindText("参数设置");

            ribbonPageGroup8.Text= language.FindText("滤波");
            tsFilter.Caption = language.FindText("低通");
            beiSamplingrate.Caption = language.FindText("频率");
            beiCutOff.Caption =language.FindText("常数");

            ribbonPageGroup9.Text = language.FindText("帮助");
            btiHelp.Caption = language.FindText("使用帮助");

            navBarGroup1.Caption = language.FindText("测试信息");
            labelControl11.Text= language.FindText("文 件 名 称") +":";
            labelControl1.Text = language.FindText("减震器名称") + ":";
            labelControl2.Text = language.FindText("活 塞 直 径") + ":";
            labelControl9.Text = language.FindText(" 安 装 位 置") + ":";
            labelControl3.Text = language.FindText("压缩力指标") + ":";
            labelControl4.Text = language.FindText("复原力指标") + ":";
            labelControl5.Text = language.FindText("试 验 地 点") + ":";
            labelControl6.Text = language.FindText("试 验 时 间") + ":";
            labelControl7.Text = language.FindText("备         注") + ":";

            navBarGroup2.Caption = language.FindText("测试结果");
            labelControl10.Text = language.FindText("充气力(N)") + ":";
            labelControl13.Text = language.FindText("摩擦力(N)") + ":";
            gridColumn3.Caption = language.FindText("速度") + (languageType == 1 ? "\n" : "") + "(m/s)";
            gridColumn4.Caption = language.FindText("复原") + (languageType == 1 ? "\n" : "") + language.FindText("力(N)");
            gridColumn5.Caption = language.FindText("压缩") + (languageType == 1 ? "\n" : "") + language.FindText("力(N)");

            navBarGroup3.Caption= language.FindText("文件列表");
            gridColumn1.Caption = language.FindText("选择");
            gridColumn2.Caption = language.FindText("文件名称");

            barHeaderItem2.Caption= language.FindText("版本V1.1");

            btiShow.Caption= language.FindText("打开显示");
            barButtonItem1.Caption = language.FindText("生成报告");
            btiNamed.Caption = language.FindText("重命名");
            btiDelete.Caption = language.FindText("删除");
            barButtonItem8.Caption = language.FindText("文件目录");
            barButtonItem5.Caption = language.FindText("路径选择");
        }

    }
}