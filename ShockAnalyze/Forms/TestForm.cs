using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using NationalInstruments.Tdms;

namespace ShockAnalyze
{
    public partial class TestForm : DevExpress.XtraEditors.XtraForm
    {
        #region 声明参数

        public delegate void OnTestFinishHandler(string filePath);
        public event OnTestFinishHandler OnTestFinishEvent;

        public delegate void OnDaqHandler(string name);
        public event OnDaqHandler OnDaqEvent;

        // 测试参数设置
        public TestSetting testSetting = new TestSetting();
        public TestSetting Test_Setting
        {
            get { return this.testSetting; }
            set { this.testSetting = value; }
        }

        // 测试参数设置
        public SyetemSetting syetemSetting = new SyetemSetting();
        public SyetemSetting Syetem_Setting
        {
            get { return this.syetemSetting; }
            set { this.syetemSetting = value; }
        }

        private MotorDeveice motorDeveice; // 电机控制类
        private bool isRuning = true, isStop = true, isFinish = false;

        Language language;

        #endregion

        #region 窗体事件

        // 构造函数
        public TestForm(Language language)
        {
            InitializeComponent();
            this.Size = new Size(700, 625);

            this.language = language;

            BindText(language);
        }

        private void BindText(Language language)
        {
            this.Text = language.FindText("测试");

            groupControl2.Text = language.FindText("通道数据");
            groupControl1.Text = language.FindText("测试步骤");
            groupControl3.Text = language.FindText("当前状态");
            groupControl4.Text = language.FindText("测试操作");

            sbtnTest.Text = language.FindText("开始测试");
            sbtnGas.Text = language.FindText("排气测试");
            sbtnCircle.Text = language.FindText("点动电机");
            sbtnReposition.Text = language.FindText("电机回位");
            sbtnClose.Text= language.FindText("关闭");

            labelControl11.Text = "*" + language.FindText("注意");
            labelControl13.Text = language.FindText("1.每次测试都需要先检测行程,位移再自动清零");
            labelControl14.Text = language.FindText("2.拉力不能超过10000N,否则无法正常进行测试");

            cbZero.Text = language.FindText("测试结束电机回到原点");

            cbPreheating.Text = language.FindText("1.开始前预热");
            cbExhaust.Text = language.FindText("2.进行排气测试");
            cbForce.Text = language.FindText("3.获取充气力和摩擦力");

            lblStatus.Text= language.FindText("硬件连接正常");

            gridColumn2.Caption = language.FindText("选择");
            gridColumn1.Caption = language.FindText("序号");
            gridColumn3.Caption = language.FindText("速度") + "(m/s)";
            gridColumn5.Caption = language.FindText("目标频率") + "(Hz)";
        }

        // 窗体加载
        private void TestForm_Load(object sender, EventArgs e)
        {
            Init();
        }

        // 窗体关闭
        private void TestForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            // 电机关闭
            motorDeveice.DeveiceClose();

            // 退出线程
            isRuning = false;
            timerShow.Enabled = false;
            timerF0.Enabled = false;
        }

        #endregion

        #region 调用方法

        private void Init()
        {
            // 电机开启
            motorDeveice = new MotorDeveice();
            motorDeveice.InitDeveice();

            lblTravel.Text = language.FindText("当前行程")+":" + testSetting.Travel + "mm";

            // 设置数据赋值
            lblChannel1Name.Text = language.FindText(syetemSetting.Channels[0].Name);
            lblChannel1Unit.Text = syetemSetting.Channels[0].Unit;
            lblChannel2Name.Text = language.FindText(syetemSetting.Channels[1].Name);
            lblChannel2Unit.Text = syetemSetting.Channels[1].Unit;
            lblChannel3Name.Text = language.FindText(syetemSetting.Channels[2].Name);
            lblChannel3Unit.Text = syetemSetting.Channels[2].Unit;

            cbExhaust.Checked = false;
            cbPreheating.Checked = testSetting.isPreheating;
            cbForce.Checked = testSetting.isAutoCalculate;

            AddRow();

            // 回调事件
            EventHandler eventHandler = new EventHandler(delegate
            {
                if (CheckDataEvent != null && !isStop)
                {
                    if (daqData.Channel8 >= testSetting.MaxTemperature - 0.1f) // 温度报警
                    {
                        this.sbtnTest_Click(this, null);
                        MessageDxUtil.ShowWarning(language.FindText("已经到达最大温度，请先冷却后再测试"));
                    }

                    if (Math.Abs(daqData.Channel6 - testSetting.InitLoad) >= 10000) // 拉力不能超过10000N
                    {
                        this.sbtnTest_Click(this, null);
                        MessageDxUtil.ShowWarning(language.FindText("拉力到达10000N,测试异常"));
                    }

                    if (CheckDataEvent != null) { CheckDataEvent(daqData); }
                }
            });

            // 控制线程
            Task task = new Task(() =>
            {
                while (isRuning)
                {
                    this.BeginInvoke(eventHandler);
                    Thread.Sleep(10);
                }
            });

            task.Start();
        }

        // 添加数据
        DataTable dtSpeeds;
        private void AddRow()
        {
            // 创建表
            dtSpeeds = new DataTable();
            // 添加列
            dtSpeeds.Columns.Add("check", typeof(bool));
            dtSpeeds.Columns.Add("index", typeof(string));
            dtSpeeds.Columns.Add("speed", typeof(string));
            dtSpeeds.Columns.Add("frequency", typeof(string));

            // 添加数据行
            for (int i = 0; i < testSetting.TestSpeeds.Count; i++)
            {
                TestSpeed testSpeed = testSetting.TestSpeeds[i];
                DataRow row = dtSpeeds.NewRow();
                row[0] = true;
                row[1] = (i + 1) + "";
                row[2] = testSpeed.Speed;
                row[3] = testSpeed.Frequency;
                dtSpeeds.Rows.Add(row);
            }

            gridControl1.DataSource = dtSpeeds;
        }

        // 数采回调
        DaqData daqData = new DaqData();
        public void flush_data(object sender, NI_Daq.SampleDataEventArgs e)
        {
            daqData.Channel6 = e.data[2]; // 力值，减去初始载荷，归零
            daqData.Channel7 = e.data[1]; // 位移,减去中点，位移归零
            daqData.Channel8 = e.data[0]; // 温度
        }

        #endregion

        #region 按钮事件

        // 关闭
        private void sbtnClose_Click(object sender, EventArgs e)
        {
            if (!isStop)
            {
                MessageDxUtil.ShowWarning(language.FindText("正在测试,请先停止"));
            }
            else
            {
                this.DialogResult = DialogResult.Cancel;
            }
        }

        // 测试
        private void sbtnTest_Click(object sender, EventArgs e)
        {
            if (sbtnTest.Text.Equals(language.FindText("开始测试")))
            {
                // 位移数据初始化
                midTravel = 0;
                travel = 0;
                CheckDataEvent = null;

                isStop = false;
                sbtnTest.Text = language.FindText("停止测试");

                isFinish = false;

                Test(); 
            }
            else
            {
                isStop = true;
                CheckDataEvent = null;
                sbtnGas.Enabled = true;
                sbtnReposition.Enabled = true;

                timerInvalid.Enabled = false;
                timerF0.Enabled = false;

                OnDaqEvent("Normal");
                lblStatus.Text = e == null ? language.FindText("完成测试") : language.FindText("停止操作");
                sbtnTest.Text = language.FindText("开始测试");       
                motorDeveice.MoveStop();
            }
        }

        // 排气
        private void sbtnGas_Click(object sender, EventArgs e)
        {
            if (!isStop)
            {
                MessageDxUtil.ShowWarning(language.FindText("正在测试,请先停止"));
                return;
            }

            sbtnTest.Text = language.FindText("停止测试");
            sbtnGas.Enabled = false;

            isStop = false;
            isStartExhaust = false;
            TargetMotorSpeed = MyMath.ConvertToMotorSpeed(testSetting.ExhaustFrequency);
            motorDeveice.SetVelParam(0, TargetMotorSpeed, 0, 0); // 起始速度,运行速度
            motorDeveice.MotorMoveVel(1); // 一直运动,0是正向,1是反向 
            CheckDataEvent += Exhaust;
        }

        // 回位
        private void sbtnReposition_Click(object sender, EventArgs e)
        {
            if (!isStop)
            {
                MessageDxUtil.ShowWarning("正在测试,请先停止！");
                return;
            }

            sbtnTest.Text = language.FindText("停止测试");
            sbtnReposition.Enabled = false;
            isStop = false;

            // 电机参数
            mSpeed = mSpeed2;
            motorDeveice.SetVelParam(0, mSpeed2, 0, 0); // 起始速度,运行速度,加速度
            motorDeveice.MotorMoveVel(1); // 一直运动 

            // 位移数据初始化
            maxTravel = 0;
            minTravel = 0;
            midTravel = 0;
            travel = 0;
            isStartCompare = true;

            // 清除脉冲
            Thread.Sleep(100);
            motorDeveice.ClearCmdPosition();
            // 检测行程
            CheckDataEvent += CheckMidValue;
        }

        // 点动按钮按下
        private void sbtnCircle_MouseDown(object sender, MouseEventArgs e)
        {
            if (!isStop) { return; }

            motorDeveice.SetVelParam(0, mSpeed2, 0, 0);
            motorDeveice.MotorMoveVel(1);
            lblStatus.Text = language.FindText("点动电机");
        }

        // 点动按钮抬起
        private void sbtnCircle_MouseUp(object sender, MouseEventArgs e)
        {
            if (!isStop)
            {
                MessageDxUtil.ShowWarning(language.FindText("正在测试,请先停止"));
                return;
            }

            motorDeveice.MoveStop();
            lblStatus.Text = language.FindText("电机停止");
        }

        #endregion

        #region 测试步骤

        // 检测中点->预热?(速度)->排气(排气速度)->Fm&&Fc(0.005)->回位零点取F0->开始采集分析(targetSpeed)

        private double Initial_Temp = 0; // 测试初始温度

        private List<AcquisitionData> AcquisitionDatas;//采集数据
        private List<AnalyzeData> AnalyzeDatas;//每个速度点的分析数据
        private event Action<DaqData> CheckDataEvent;//返回数据进行检测事件

        private int testIndex = 0;
        private int mSpeed1 = 10, mSpeed2 = 40;
        private volatile bool isStartCompare = false, isStartAcquisition = false,isStartF0=false;
        private double TargetSpeed = 0, TargetMotorSpeed;// 目标速度，目标电机速度
        private double maxTravel, minTravel, midTravel, travel;//最大位移、最小位移、中点位置、行程

        // 开始测试
        private void Test()
        {
            // 获取测试的步骤
            dicTestSteup.Clear();
            if (cbPreheating.Checked) { dicTestSteup.Add(1, false); }
            if (cbExhaust.Checked) { dicTestSteup.Add(2, false); }
            if (cbForce.Checked) { dicTestSteup.Add(3, false); }
            dicTestSteup.Add(4, false); 

            // 测试速度
            testSpeeds.Clear();
            foreach (DataRow dataRow in dtSpeeds.Rows)
            {
                bool isCheck = (bool)dataRow["check"];
                double speed = double.Parse(dataRow["speed"].ToString());
                if (isCheck) { testSpeeds.Add(speed); }
            }

            // 检查行程
            testIndex = 0;
            isStartCompare = true;
            Thread.Sleep(100);
            motorDeveice.ClearCmdPosition();
            CheckDataEvent += CheckMidValue;    

            // 电机参数
            motorDeveice.SetVelParam(0, mSpeed2, 0, 0); // 起始速度,运行速度,加速度
            motorDeveice.MotorMoveVel(1); // 一直运动   

            // 分析数据
            AnalyzeDatas = new List<AnalyzeData>();
        }

        // 测试步骤集合
        Dictionary<int, bool> dicTestSteup = new Dictionary<int, bool>();
        List<double> testSpeeds = new List<double>();
        private void TestSteup()
        {
            isStartForce = false;
            isStartF0 = false;
            CheckDataEvent = null;
            bool isNeedTest = false;
            for (int i = 1; i <= 4; i++)
            {
                if (!dicTestSteup.ContainsKey(i)) { continue; }
                bool isTest = dicTestSteup[i];
                if (i == 1 && !isTest) // 预热
                {
                    isNeedTest = true;
                    CheckDataEvent += CheckTemperature;
                    dicTestSteup[i] = true;
                    break;
                }
                else if (i == 2 && !isTest)// 排气
                {
                    isNeedTest = true;
                    isStartExhaust = false;
                    TargetMotorSpeed = MyMath.ConvertToMotorSpeed(testSetting.ExhaustFrequency);
                    motorDeveice.ChangeSpeed(TargetMotorSpeed);
                    CheckDataEvent += Exhaust;
                    dicTestSteup[i] = true;
                    break;
                }
                else if (i == 3 && !isTest)// 获取充气力和摩擦力
                {
                    dataIdx = 0;
                    mSpeed = 0;
                    F_Test = 0;
                    CheckDataEvent += Reposition;
                    isNeedTest = true;
                    isStartForce = true;
                    dicTestSteup[i] = true;
                    break;
                }
                else if (i == 4 && !isTest)// F0
                {
                    isNeedTest = true;
                    mSpeed = 0;
                    dataIdx = 0;
                    isStartF0 = true;
                    CheckDataEvent += Reposition;
                    dicTestSteup[i] = true;
                    break;
                }

            }

            if (!isNeedTest) { ChangeMotorSpeed(); }
        }

        //1.获得中点位置
        private void CheckMidValue(DaqData data)
        {
            lblStatus.Text = language.FindText("检测行程");
            if (motorDeveice.GetCmdPosition()>= Constant.ONE_CIRCLE_PULSE)
            {
                midTravel = (maxTravel + minTravel) / 2;//零点位置
                travel = maxTravel - minTravel;//行程
                if (travel >= testSetting.Travel + 5 || travel <= testSetting.Travel - 5)
                {
                    //Console.WriteLine("minTravel=" + minTravel + "   maxTravel=" + maxTravel);
                    // 停止电机
                    isStop = true;
   
                    this.sbtnTest_Click(this, null);
                    MessageDxUtil.ShowError(language.FindText("行程选择错误"));
                    return;
                }

                if (Math.Abs(data.Channel7 - midTravel) < 5) { return; }
                //Console.WriteLine("minTravel=" + minTravel + "   maxTravel=" + maxTravel + "midTravel:" + midTravel);
                CheckDataEvent -= CheckMidValue;
                if (sbtnReposition.Enabled)// 电机回位
                {
                    TestSteup();
                }
                else
                {
                    //第一圈结束
                    dataIdx = 0;
                    CheckDataEvent += Reposition;
                }
            }
            else 
            {
                CompareValue(data.Channel7 - midTravel);
            }
        }

        //数值比较
        private void CompareValue(double value)
        {
            if (isStartCompare)
            {
                isStartCompare = false;
                maxTravel = value;
                minTravel = value;
            }
            else
            {
                if (maxTravel < value) { maxTravel = value; }
                else if (minTravel > value) { minTravel = value; }
            }
        }

        // 电机回位
        int mSpeed = 0,stopTravel=2;
        private void Reposition(DaqData data)
        {
            if (isStartForce) { lblStatus.Text = language.FindText("获取充气力和摩擦力")+" F1"; }
            else if (isStartF0) { lblStatus.Text = language.FindText("速度测试前回位"); }
            else { lblStatus.Text = language.FindText("电机回位"); }
            
            double currentTravel = data.Channel7 - midTravel;
            if (sbtnReposition.Enabled && !isStartForce && !isStartF0)
            {
                ControlSpeedAndDirection(currentTravel, false);
            }
            else
            {
                ControlSpeedAndDirection(currentTravel, true);
            }

            if(motorDeveice.GetMotorSpeed() < 20&&Math.Abs(currentTravel) <= stopTravel)
            {
                CheckDataEvent -= Reposition;
                if (!sbtnReposition.Enabled)
                {
                    this.sbtnTest_Click(this, null);
                }
                else if (isStartForce)
                {
                    //Console.WriteLine("获取充气力和摩擦力");
                    isStartForce = false;
                    isRecord1 = false;
                    isRecord2 = false;
                    TargetMotorSpeed = MyMath.ConvertToMotorSpeed(testSetting.FrictionSpeed, travel);
                    motorDeveice.ChangeSpeed(TargetMotorSpeed);
                    motorDeveice.MotorMoveVel(0);
                    CheckDataEvent += CheckForce;

                }
                else if (isStartF0)
                {
                    lblStatus.Text = language.FindText("测试前暂停3s");
                    isStartF0 = false;
                    motorDeveice.MoveStop(); // 暂停
                    timerF0.Enabled = true;
                }
                else
                {   // 结束
                    motorDeveice.MoveStop();
                    TestFinish();
                }
            }
        }

        // 控制速度和方向
        int dataIdx = 0;
        double startTravel=0;
        private void ControlSpeedAndDirection(double travel, bool isNeedDirection)
        {
            if (Math.Abs(travel) <= 10)
            {
                if (mSpeed != mSpeed1)
                {
                    mSpeed = mSpeed1;
                    motorDeveice.ChangeSpeed(mSpeed1);
                }
            }
            else
            {
                if (mSpeed != mSpeed2)
                {
                    mSpeed = mSpeed2;
                    motorDeveice.ChangeSpeed(mSpeed2);
                }
            }

            if (!isNeedDirection||dataIdx == 22) { return; }
            if (dataIdx == 0) 
            {
                //Console.WriteLine("startTravel:" + travel);
                startTravel = travel;
            }
            if (dataIdx < 21)
            {
                dataIdx++;
            }
            else if (dataIdx==21)
            {
                //Console.WriteLine("endtravel:" + travel);
                dataIdx = 22;
                if (travel > startTravel) // 增加,左侧
                {
                    motorDeveice.MotorMoveVel(travel > 0 ? 0 : 1); // 反转:正转
                }
                else  // 减少,右侧
                {
                    motorDeveice.MotorMoveVel(travel > 0 ? 1 : 0); // 正转:反转
                }
            }
        }

        // 2.预热
        private void CheckTemperature(DaqData data)
        {
            lblStatus.Text = language.FindText("正在预热");

            //检测当前温度是否达到指定温度
            double curTemperature = data.Channel8;
            if (curTemperature >= testSetting.InitTemperature - 0.1f)//温度到达,0.1f的误差
            {
                //预热结束，进行排气操作
                CheckDataEvent -= CheckTemperature;
                TestSteup();
            }
        }

        // 3.排气
        bool isStartExhaust = false;
        private void Exhaust(DaqData data)
        {
            if (!isStartExhaust)
            {
                int motorSpeed = motorDeveice.GetMotorSpeed();
                if (motorSpeed == (int)TargetMotorSpeed)
                {
                    // 达到目标速度
                    Thread.Sleep(100);
                    motorDeveice.ClearCmdPosition();
                    isStartExhaust = true;
                }

                return;
            }

            // 检测圈数
            double circle = motorDeveice.GetCmdPosition() / Constant.ONE_CIRCLE_PULSE;
            lblStatus.Text = language.FindText("排气测试第") + circle.ToString("F1") + language.FindText("圈");
            if (circle >= testSetting.ExhaustCircle)
            {
                CheckDataEvent -= Exhaust;
                if (sbtnGas.Enabled)
                {
                    TestSteup();
                }
                else
                {
                    // 延时2s减速,不急停
                    motorDeveice.ChangeSpeed(mSpeed2);
                    Thread.Sleep(2000);
                    this.sbtnTest_Click(this, null);
                }
            }
        }

        // 4.Fm&&Fc
        int recordDisplacement = 2,pointTravel=3;
        bool isStartForce = false, isRecord1 = false, isRecord2=false;
        double Fm = 0, Fc = 0; // 摩擦力、充气力
        int F_Test = 0;
        private void CheckForce(DaqData data)
        {
            double currentTravel = data.Channel7 - midTravel;

            // 第一次回零点，根据左右上下8mm,采集2mm数据,再回到零点位置
            if (F_Test == 0 && Math.Abs(currentTravel) >= pointTravel)
            {
                F_Test = currentTravel > 0 ? 1 : 2;
                motorDeveice.MotorMoveVel(1);
                isRecord1 = true;
            }
            else if (F_Test == 1 && currentTravel <= -pointTravel)// 10mm->-10mm
            {
                lblStatus.Text = language.FindText("获取充气力和摩擦力")+" F2";
                isRecord2 = true;
                motorDeveice.MotorMoveVel(0);
                F_Test = 3;
            }
            else if (F_Test == 2 && currentTravel >= pointTravel)// -10mm->10mm
            {
                lblStatus.Text = language.FindText("获取充气力和摩擦力") + " F2";
                isRecord2 = true;
                motorDeveice.MotorMoveVel(0);
                F_Test = 4;
            }
            else if (F_Test == 3 && currentTravel >= pointTravel||F_Test == 4 && currentTravel <= -pointTravel)
            {
                F_Test = 5;
                motorDeveice.MotorMoveVel(1);
            }
            else if (F_Test == 5 && Math.Abs(currentTravel) <= 1)
            {
                CheckDataEvent -= CheckForce;
                motorDeveice.MoveStop(); // 暂停

                lblStatus.Text = language.FindText("测试前暂停3s");
                isStartF0 = false;
                timerF0.Enabled = true;
                dicTestSteup[4] = true;
            }

            if (F_Test == 1 || F_Test == 2)
            {
                if (isRecord1 && Math.Abs(currentTravel) <= recordDisplacement)
                {
                    //Console.WriteLine("开始采集1:" + currentTravel);
                    isRecord1 = false;
                    OnDaqEvent("force1");
                }

                if (!isRecord1 && Math.Abs(currentTravel) > recordDisplacement)
                {
                    //Console.WriteLine("结束采集1:" + travel);
                    OnDaqEvent("Normal");
                }
            }

            if (F_Test == 3 || F_Test ==4)
            {
                if (isRecord2 && Math.Abs(currentTravel) <= recordDisplacement)
                {
                    //Console.WriteLine("开始采集2:" + currentTravel);
                    isRecord2 = false;
                    OnDaqEvent("force2");
                }

                if (!isRecord2 && Math.Abs(currentTravel) > recordDisplacement)
                {
                    OnDaqEvent("Normal");
                }
            }
        }

        // 改变电机转速
        private void ChangeMotorSpeed()
        {
            TargetSpeed = testSpeeds[testIndex];
            lblStatus.Text = language.FindText("测试速度")+":" + TargetSpeed;
            TargetMotorSpeed =MyMath.ConvertToMotorSpeed(TargetSpeed, travel);
            motorDeveice.ChangeSpeed(TargetMotorSpeed);
            timerInvalid.Enabled = true;
        }

        // 5.采集数据
        private volatile int acquisitionIndex = 0;
        private void PutAcquisitionData(DaqData data)
        {
            //第一次获取初始脉冲
            if (isStartAcquisition)
            {
                isStartAcquisition = false;
                Thread.Sleep(50);
                motorDeveice.ClearCmdPosition();
                OnDaqEvent(TargetSpeed.ToString());// 开始记录数据
                acquisitionIndex = 0;
                //Thread.Sleep(20);
                //Console.WriteLine(TargetSpeed + "：" + motorDeveice.GetCmdPosition());
            }

            acquisitionIndex++; // 2圈时间判定过短,出现采集不全问题

            //一个速度点测试结束,2圈速度,添加点
            if (acquisitionIndex > 2 && motorDeveice.GetCmdPosition() > 2 * Constant.ONE_CIRCLE_PULSE)
            {
                int count = GetReadTdmsCount(TargetSpeed);  // 获取当前的点数
                if (count >= 300)
                {

                    // Console.WriteLine(TargetSpeed+"：" + motorDeveice.GetCmdPosition());
                    CheckDataEvent -= PutAcquisitionData; // 移除事件

                    testIndex++;

                    OnDaqEvent("Normal");// 结束上一个记录数据,变速，更换记录数据
                    if (testIndex < testSpeeds.Count)
                    {
                        // 继续变速然后采集数据
                        ChangeMotorSpeed();
                    }
                    else
                    {
                        isFinish = true;
                        if (!cbZero.Checked)
                        {
                            // 结束
                            motorDeveice.MoveStop();
                            TestFinish();
                        }
                        else
                        {
                            SleepTime = 0;
                            mSpeed = mSpeed2;
                            motorDeveice.ChangeSpeed(mSpeed2);
                            CheckDataEvent += ShiftDown;
                        }

                    }
                }
                else
                {
                    acquisitionIndex = 16;
                }
            }

        }

        // 减速
        int SleepTime = 0;
        private void ShiftDown(DaqData data)
        {
            if (motorDeveice.GetMotorSpeed() == mSpeed2)
            {
                SleepTime++;
                lblStatus.Text = language.FindText("电机回位");

                if (SleepTime >= 50)
                {
                    CheckDataEvent -= ShiftDown;
                    dataIdx = 0;
                    CheckDataEvent += Reposition;
                }
            }
        }

        // 测试结束
        OutPutData outPutData = new OutPutData();
        private void TestFinish()
        {
            lblStatus.Text = language.FindText("测试完成,正在计算...");
            string filePath = "";

            // 控制线程
            EventHandler calulateHandler = new EventHandler(delegate
            {
                OnTestFinishEvent(filePath);
                this.sbtnTest_Click(this, null);
                this.Close();
                //timerTest.Enabled = true;
            });

            bool isCheck = cbForce.Checked;
            Task task = new Task(() =>
            {
                if (isCheck) { CalucateF0(); }
                ReadData(); // 先读取数据
                CalulateResultData(); // 先不计算,直接生成文件
                ResultData resultData = GetResultData();// 赋值结果数据

                // 开始存储
                int index = 1;
                string fileName;
                do
                {
                    fileName = syetemSetting.PrefixNamed + "_" + index + ".dat";
                    filePath = syetemSetting.ProjectDirectory + "\\" + fileName;
                    index++;
                } while (File.Exists(filePath));

                //生成文件
                outPutData.SetPath(filePath);
                outPutData.WriteDate(resultData);
                this.BeginInvoke(calulateHandler);
            });

            task.Start();
        }

        #endregion

        #region 读取TDMS文件数据

        // 读取数据
        private void ReadData()
        {
            foreach (double speed in testSpeeds)
            {
                string filePath = GlobalData.BasePath + "NI_Tdms\\" + speed + ".tdms";
                if (File.Exists(filePath))
                {
                    List<AcquisitionData> AcquisitionDatas = ReadTdmsFile(filePath, speed,11);//力滞后
                    // 对位移处理尖点
                    //MyMath.FixedTrvaelData(AcquisitionDatas,2);
                    AnalyzeDatas.Add(new AnalyzeData(speed, AcquisitionDatas));
                }
            }
        }

        private int GetReadTdmsCount(double speed)
        {
            string filePath = GlobalData.BasePath + "NI_Tdms\\" + speed + ".tdms";
            if (!File.Exists(filePath)) { return 0; }
            //Open TDMS file
            TdmsFile tdmsFile = new TdmsFile(filePath, TdmsFileAccess.Read);
            TdmsChannelGroupCollection tdmsChannelGroups = tdmsFile.GetChannelGroups();
            int count = (int)tdmsChannelGroups[0].GetChannels()[0].DataCount;

            return count;
        }

        int samplingrate = 1000, cutOff = 100; // 采样频率1000,截至频率200 
        private List<AcquisitionData> ReadTdmsFile(string filePath, double speed, int N)
        {
            //int N = 11;// 力通道采集滞后
            int count = 0;
            List<AcquisitionData> AcquisitionDatas = new List<AcquisitionData>();
            //Open TDMS file
            TdmsFile tdmsFile = new TdmsFile(filePath, TdmsFileAccess.Read);
            //Read file properties
            TdmsChannelGroupCollection tdmsChannelGroups = tdmsFile.GetChannelGroups();
            double[] travels = new double[0];
            double[] forces = new double[0];
            foreach (TdmsChannelGroup tdmsChannelGroup in tdmsChannelGroups)
            {
                TdmsChannelCollection tdmsChannels = tdmsChannelGroup.GetChannels();
                // 获取行数
                count = (int)tdmsChannels[0].DataCount - N;

                //object[] data1 = tdmsChannels[0].GetData(0, count); // 温度
                object[] data2 = tdmsChannels[1].GetData(0, count); // 位移
                object[] data3 = tdmsChannels[2].GetData(0, count); // 力

                travels = new double[count];
                forces = new double[count];
                for (int i = 0; i < count - N; i++)
                {
                    travels[i] = (double)data2[i];
                    forces[i] = (double)data3[i + N];
                }
            }

            // 滤波
            travels = Butterworth.Filter(travels, samplingrate, cutOff);
            forces = Butterworth.Filter(forces, samplingrate, cutOff); 

            // 赋值数组
            double time = 0;
            for (int i = 0; i < count; i++)
            {
                time += 1.0 / samplingrate; // 1000Hz
                AcquisitionDatas.Add(new AcquisitionData(time, travels[i], forces[i]));
            }
            
            //Close file
            tdmsFile.Close();

            return AcquisitionDatas;
        }

        //获得结果文件类
        private ResultData GetResultData()
        {
            ResultData resultData = new ResultData();//结果文件类

            //减震器信息
            object _ShockInf = testSetting.Shock_Info;
            if (_ShockInf != null) {resultData.ShockInf = (ShockInfo)_ShockInf;}
            else { resultData.ShockInf = new ShockInfo();}

            //初始温度
            resultData.Initial_Temp = Initial_Temp.ToString("F2");

            resultData.Fmf = Constant.EMPTY_STR;
            resultData.Fmy = Constant.EMPTY_STR;
            resultData.Fc = Fc.ToString("F2");
            resultData.Fm = Fm.ToString("F2");
            resultData.F0 = Constant.EMPTY_STR;

            resultData.Frequency = Constant.EMPTY_STR;

            //每个速度点分析数据
            resultData.AnalyzeDatas = AnalyzeDatas;

            return resultData;
        }

        #endregion

        #region 计时器

        // 检测速度到达目标速度点
        private void timerInvalid_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            //达到V_High后才开始采集数据
            double motorSpeed = motorDeveice.GetMotorSpeed();
            if (motorSpeed ==(int)TargetMotorSpeed)
            {
                //Console.WriteLine("到达目标速度:" + motorSpeed);
                // 达到目标速度，采集数据
                timerInvalid.Enabled = false;
                isStartAcquisition = true;
                AcquisitionDatas = new List<AcquisitionData>();
                Thread.Sleep(50);
                CheckDataEvent += PutAcquisitionData;//绑定采集事件
            }
        }

        // 显示实时数据
        private void timerShow_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            lblChannel1Value.Text = daqData.Channel8.ToString("F2");
            double force = Filter(daqData.Channel6 - testSetting.InitLoad);
            lblChannel2Value.Text = force.ToString("F2"); // 力
            lblChannel3Value.Text = (daqData.Channel7 - midTravel).ToString("F2");
        }

        // 零点位置的力
        private void timerF0_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            timerF0.Enabled = false;
            Initial_Temp = daqData.Channel8;
            motorDeveice.MotorMoveVel(1); // 一直运动,0是正向,1是反向 
            TestSteup();
        }
       
        #endregion

        #region 计算结果

        //数组初始化
        List<ZeroPositionTime> CompZeroPositionTimes = new List<ZeroPositionTime>();//压缩零点前后时刻
        List<ZeroPositionTime> ReboundZeroPositionTimes = new List<ZeroPositionTime>();//复原零点前后时刻
        List<AcquisitionData> CompDatas = new List<AcquisitionData>();//压缩1/8数据
        List<AcquisitionData> ReboundDatas = new List<AcquisitionData>();//复原1/8数据

        //清空数组
        private void ClearList()
        {
            CompZeroPositionTimes.Clear();
            ReboundZeroPositionTimes.Clear();
            CompDatas.Clear();
            ReboundDatas.Clear();
        }

        //数据结果计算
        private void CalulateResultData()
        {
            //遍历速度点
            foreach (AnalyzeData analyzeData in AnalyzeDatas)
            {
                // 截取数据
                ClearList();
                List<AcquisitionData> tempData = SubDatas(analyzeData);//赋值数据
                if (tempData == null) { break; }

                double speed = analyzeData.Speed;//速度点

                // 零点位置计算数据
                double Fmf = 0, Fmy = 0, t1 = 0, t2 = 0;
                try
                {
                    GetZeroPositionForce(speed, ReboundDatas, ref Fmf, ref t1);//复原力
                    GetZeroPositionForce(speed, CompDatas, ref Fmy, ref t2); //压缩力
                }
                catch (Exception ex)
                {
 
                }

                // 赋值标准的数据
                analyzeData.AcquisitionDatas.Clear();
                analyzeData.AcquisitionDatas = tempData;

                // 判断计算出来的力值是不是NaN，不是正常的数字,因为点密集的关系
                //bool isNormal = FixNum(ref t1, ref t2, ref Fmf, ref Fmy);

                // Fc&Fm加入计算
                if (cbForce.Checked)
                {
                    Fmf = Fmf + Math.Abs(Fc);//复原力+充气力
                    Fmy = Fmy + Math.Abs(Fc);//压缩力+充气力
                }

                //计算速度
                double actSpeed = MyMath.ConvertToSpeed(0.5 / Math.Abs(t2 - t1), travel);//isNormal ? MyMath.ConvertToSpeed(0.5 / Math.Abs(t2 - t1), 100) : speed;//速度
                analyzeData.SetData(actSpeed, Fmf, Fmy);//将分析数据赋值
            }
        }

        // 修正计算出Nan的问题
        private bool FixNum(ref double t1, ref double t2, ref double Fmf, ref double Fmy)
        {
            bool isNormal = true;
            if (!CheckNum(t1)) { isNormal = false; t1 = 0; }
            if (!CheckNum(t2)) { isNormal = false; t2 = 1; }
            if (!CheckNum(Fmf)) { isNormal = false; Fmf = 0; }
            if (!CheckNum(Fmy)) { isNormal = false; Fmy = 0; }

            return isNormal;
        }

        //检测数字是否正常
        private bool CheckNum(double num)
        {
            bool isNormal = double.IsNaN(num) || double.IsNegativeInfinity(num) || double.IsPositiveInfinity(num) ? false : true;

            return isNormal;
        }

        // 数据截取一圈
        private List<AcquisitionData> SubDatas(AnalyzeData analyzeData)
        {
            //进行位移修正
            List<AcquisitionData> curAcquisitionDatas = ZeroData(analyzeData.AcquisitionDatas);
            //理论频率
            double T = 1 / MyMath.ConvertToFrequency(analyzeData.Speed, testSetting.Travel); //Math.Abs(T_highests - T_lowests);
            int keyIndex = -1;
            AcquisitionData recAcquisitionData = null;//记录前一个采集的数据
            for (int i = 0; i < curAcquisitionDatas.Count; i++)
            {
                AcquisitionData acquisitionData = curAcquisitionDatas[i];
                //计算最高点，最低点，过零点的前后时间
                if (SearchTime(i, recAcquisitionData, acquisitionData, T))
                {
                    if (keyIndex == i - 1)
                    {
                        curAcquisitionDatas.RemoveAt(i - 1);
                        CompZeroPositionTimes.RemoveAt(CompZeroPositionTimes.Count - 1);
                        ReboundZeroPositionTimes.RemoveAt(ReboundZeroPositionTimes.Count - 1);
                        keyIndex = -1;
                    }
                    else
                    {
                        keyIndex = i;
                    }
                }
                //记录前一个数据
                recAcquisitionData = acquisitionData;
            }

            if (CompZeroPositionTimes.Count <= 1 || ReboundZeroPositionTimes.Count <= 1){return null;}

            double tStart = 0, tEnd = 0, tComp = 0, tRebound = 0;
            if (CompZeroPositionTimes[0].BeforeZeroTime > ReboundZeroPositionTimes[0].BeforeZeroTime)//复原力在前面，去掉复原第一个零点信息
            {
                tStart = CompZeroPositionTimes[0].AfterZeroTime;
                tEnd = CompZeroPositionTimes[1].BeforeZeroTime;
                tComp = CompZeroPositionTimes[0].ApproachZeroTime;
                tRebound = ReboundZeroPositionTimes[1].ApproachZeroTime;
            }
            else if (CompZeroPositionTimes[0].BeforeZeroTime < ReboundZeroPositionTimes[0].BeforeZeroTime)
            {
                tStart = ReboundZeroPositionTimes[0].AfterZeroTime;
                tEnd = ReboundZeroPositionTimes[1].BeforeZeroTime;
                tComp = CompZeroPositionTimes[1].ApproachZeroTime;
                tRebound = ReboundZeroPositionTimes[0].ApproachZeroTime;
            }

            //Console.WriteLine("tStart={0},tEnd={1},tRebound={2},tComp={3}", tStart, tEnd, tRebound, tComp);

            //取T1±T*1/8数据进行插值
            List<AcquisitionData> standardDatas = new List<AcquisitionData>();//标准数据

            // 往前挪T/4从峰值开始截取,国外是从峰值点截取
            //if ((tStart - T / 4) >= 0)
            //{
            //    tStart = tStart - T / 4;
            //    tEnd = tEnd - T / 4;
            //}
            //else
            //{
            //    tStart = tStart + T / 4;
            //    tEnd = tEnd + T / 4;
            //}

            foreach (AcquisitionData acquisitionData in curAcquisitionDatas)
            {
                double acqTime = acquisitionData.AcqTime;//采集时间
                double displacement = acquisitionData.Displacement;//位移
                double force = acquisitionData.Force;//力

                if (acqTime > tStart && acqTime < tEnd)
                {
                    standardDatas.Add(new AcquisitionData(acqTime, displacement, force));
                }

                if (acqTime >= tRebound - T / 8 && acqTime <= tRebound + T / 8) //复原力,±1/8T附近的点
                {
                    ReboundDatas.Add(new AcquisitionData(acqTime, displacement, force)); //添加符合要求的复原零点位置附近的点
                }
                else if (acqTime >= tComp - T / 8 && acqTime <= tComp + T / 8) //压缩力,±1/8T附近的点
                {
                    CompDatas.Add(new AcquisitionData(acqTime, displacement, force));//添加符合要求的压缩零点位置附近的点
                }
            }

            return standardDatas;
        }

        private List<AcquisitionData> ZeroData(List<AcquisitionData> acquisitionDatas)
        {
            //double _maxTravel = 0, _minTravel = 0, _midTravel = 0;

            //for (int i = 0; i < acquisitionDatas.Count; i++)
            //{
            //    double curTravel = acquisitionDatas[i].Displacement;
            //    if (i == 1)
            //    {
            //        _maxTravel = curTravel;
            //        _minTravel = curTravel;
            //    }

            //    _maxTravel = curTravel > _maxTravel ? curTravel : _maxTravel;
            //    _minTravel = curTravel < _minTravel ? curTravel : _minTravel;
            //}

            //_midTravel = (_maxTravel + _minTravel) / 2;
            for (int i = 0; i < acquisitionDatas.Count; i++)
            {
                acquisitionDatas[i].Displacement = acquisitionDatas[i].Displacement - midTravel; // 位移清零
                acquisitionDatas[i].Force = acquisitionDatas[i].Force - testSetting.InitLoad; 
            }

            return acquisitionDatas;
        }

        // 获得零点力
        private void GetZeroPositionForce(double speed,List<AcquisitionData> calculateDatas, ref double zeroForce, ref double zeroTime)
        {
            int count = calculateDatas.Count;
            double[] timeValues = new double[count];
            double[] travelValues = new double[count];
            double[] forceValues = new double[count];

            for (int i = 0; i < count; i++)
            {
                AcquisitionData acquisitionData = calculateDatas[i];

                timeValues[i] = double.Parse(acquisitionData.AcqTime.ToString("F5"));
                travelValues[i] = double.Parse(acquisitionData.Displacement.ToString("F5"));
                forceValues[i] = double.Parse(acquisitionData.Force.ToString("F5"));
            }

            //插值计算零点力值
            if (speed > 0.5) // 0.5m/s速度以上采用插值
            {
                Spline splineD_F = new Spline(travelValues, forceValues);
                zeroForce = splineD_F.SplineValue(0);//零点力值
                if (!CheckNum(zeroForce))
                {
                    travelValues = Butterworth.Filter(travelValues, 1000, 100);
                    splineD_F = new Spline(travelValues, forceValues);
                    zeroForce = splineD_F.SplineValue(0);//零点力值
                }
            }
            else // 0.5m/s速度以下采用拟合
            {
                // 进行拟合
                double[] f = Polyfit1.MultiLine(travelValues, forceValues, travelValues.Length, 2);
                double x = 0;
                zeroForce = f[0] + f[1] * x + f[2] * x * x;
            }

            //插值计算零点时间
            Spline splineD_T = new Spline(travelValues, timeValues);
            zeroTime = splineD_T.SplineValue(0);//零点时间
            if (!CheckNum(zeroTime))
            {
                travelValues = Butterworth.Filter(travelValues, 1000, 100);
                splineD_T = new Spline(travelValues, timeValues);
                zeroTime = splineD_T.SplineValue(0);//零点时间
            }
        }

        //寻找关键点时间
        bool isNegative = false;
        double keyPointTime = -1;
        private bool SearchTime(int index, AcquisitionData recAcquisitionData, AcquisitionData curAcquisitionData, double T)
        {
            bool isKeyPoint = false;

            double curAcqTime = curAcquisitionData.AcqTime;
            double curDisplacement = curAcquisitionData.Displacement;
            double curForce = curAcquisitionData.Force;

            double recAcqTime = 0, recDisplacement = 0, recForce = 0;

            if (index == 0)
            {
                keyPointTime = -1;
                isNegative = curDisplacement < 0 ? true : false;
            }
            else if (keyPointTime < 0 || (curAcqTime - keyPointTime) >= T / 8) // 理论是T/2周期出现的数据,排除因为抖动而出现的零点判定错误
            {
                recAcqTime = recAcquisitionData.AcqTime;
                recDisplacement = recAcquisitionData.Displacement;
                recForce = recAcquisitionData.Force;

                // 拉伸力>|压缩力|
                if (isNegative && curDisplacement >= 0)//负方向,压缩力
                {
                    isNegative = false;
                    CompZeroPositionTimes.Add(new ZeroPositionTime(recAcqTime, curAcqTime, recDisplacement, curDisplacement));
                    isKeyPoint = true;
                    keyPointTime = curAcqTime;
                }
                else if (!isNegative && curDisplacement <= 0)//正方向，拉伸力
                {
                    isNegative = true;
                    ReboundZeroPositionTimes.Add(new ZeroPositionTime(recAcqTime, curAcqTime, recDisplacement, curDisplacement));
                    isKeyPoint = true;
                    keyPointTime = curAcqTime;
                }
            }

            return isKeyPoint;
        }

        #endregion

        #region 其他

        // 计算Fm和Fc
        private void CalucateF0()
        {
            // 这里开始读取tdms数据,计算零点的力
            List<AcquisitionData> acquisitionDatas1 = ReadTdmsFile(GlobalData.BasePath + "NI_Tdms\\force1.tdms",0,0);
            List<AcquisitionData> acquisitionDatas2 = ReadTdmsFile(GlobalData.BasePath + "NI_Tdms\\force2.tdms",0,0);

            double f1 = GetFm1(acquisitionDatas1);
            double f2 = GetFm1(acquisitionDatas2);

            Fm = Math.Abs(Math.Abs(f1) - Math.Abs(f2)) / 2;//摩擦力
            Fc = -Math.Abs(Math.Abs(f1) + Math.Abs(f2)) / 2;//充气力
            System.Diagnostics.Debug.WriteLine("f1={0} f2={1} Fm={2} Fc={3}", f1, f2, Fm, Fc);
        }

        // 对零点附近的数据处理,返回零点的力
        private double GetFm1(List<AcquisitionData> acquisitionDatas1)
        {
            // 数据赋值
            int acqDataCount = acquisitionDatas1.Count;
            if(acqDataCount==0){return 0;}

            // 对数据进行处理,大于2mm的数据不要
            List<AcquisitionData> acquisitionDatas=new List<AcquisitionData>();
            for (int i = 0; i < acqDataCount; i++)
            {
                AcquisitionData acquisitionData=acquisitionDatas1[i];
                if (Math.Abs(acquisitionData.Displacement - midTravel) <=1)
                {
                    acquisitionDatas.Add(acquisitionData);
                }
            }

            acqDataCount = acquisitionDatas.Count;
            double[] travels = new double[acqDataCount];
            double[] forces = new double[acqDataCount];
            for (int i = 0; i < acqDataCount; i++)
            {
                travels[i] = acquisitionDatas[i].Displacement - midTravel;
                forces[i] = acquisitionDatas[i].Force-testSetting.InitLoad;
            }

            // 低通滤波
            travels = Butterworth.Filter(travels, samplingrate, cutOff);
            forces = Butterworth.Filter(forces, samplingrate, cutOff);
            // 进行拟合
            double[] f = Polyfit1.MultiLine(travels, forces, acqDataCount, 1);
            double x = 0;
            double zeroForce = f[0] + f[1] * x;

            return zeroForce;
        }

        // 数采StartNewFile.这个函数可能会报错
        public void onError()
        {
            // 出现数采报错，如果做完了则计算
            if (cbZero.Checked && isFinish)
            {
                motorDeveice.MoveStop();
                TestFinish();
            }
            else
            {
                if (sbtnTest.Text.Equals(language.FindText("停止测试")))
                {
                    this.sbtnTest_Click(this, null);
                }
                
                lblStatus.Text = language.FindText("数据连接");
                this.Close();
                MessageDxUtil.ShowError(language.FindText("数采连接出现问题,已尝试重新连接..."));
            }
        }

        // 滤波显示
        double preValue = 0;
        double a = 0.6;
        private double Filter(double value)
        {
            // 滤波后的数值
            double filterValue = (1-a)*value+a*preValue;
            preValue = value;

            return filterValue;
        }

        // 速度表格背景
        private void gridView1_CustomDrawCell(object sender, DevExpress.XtraGrid.Views.Base.RowCellCustomDrawEventArgs e)
        {
            if (gridView1.GetRow(e.RowHandle) == null)
            {
                return;
            }

            e.Appearance.BackColor = Color.White;
        }
        
        #endregion

    }
}
