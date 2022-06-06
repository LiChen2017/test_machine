using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Threading;
using System.Threading.Tasks;

namespace ShockAnalyze
{
    // 主要用于控制电机
    public class MotorDeveice
    {
        // 电机使能
        private byte[] CMD_SERVO_ON = { 0x7F, 0x06, 0x04, 0x0E, 0x00, 0xE1, 0x23, 0x6F };
        // 电机不使能
        private byte[] CMD_SERVO_OFF = { 0x7F, 0x06, 0x04, 0x0E, 0x00, 0xE0, 0xE2, 0xAF };
        // 刚开起来使能要关闭
        private byte[] CMD_SERVO_OFF_START = { 0x7F, 0x06, 0x03, 0x0C, 0x00, 0x01, 0x82, 0x53 };
        // 开始运转_正转
        private byte[] CMD_RUN_START1 = { 0x7F, 0x06, 0x04, 0x0A, 0x13, 0x87, 0xEF, 0xB4 };
        // 开始运转_反转
        private byte[] CMD_RUN_START2 = { 0x7F, 0x06, 0x04, 0x0A, 0x13, 0x86, 0x2E, 0x74 };
        // 停止运转
        private byte[] CMD_RUN_STOP = { 0x7F, 0x06, 0x04, 0x0A, 0x00, 0x00, 0xA2, 0xE6 };
        // 速度获取
        private byte[] CMD_RUN_SPEED = { 0x7F, 0x03, 0x00, 0x14, 0x00, 0x02, 0x8E, 0x11 };
        // 电子齿轮后脉冲
        private byte[] CMD_RUN_PULSE = { 0x7F, 0x03, 0x00, 0x16, 0x00, 0x02, 0x2F, 0xD1 };
        // 速度设置 byte[4] byte[5]速度值,byte[6] byte[7]CRC16
        private byte[] CmdSpeed = { 0x7F, 0x06, 0x04, 0x0A, 0x00, 0x00, 0x00, 0x00 };

        // 脉冲,速度
        private int InitPulse = 0, CurrentPulse = 0, CurrentSpeed = 0, StopSpeed = 1;
        // 脉冲清零
        private volatile bool isZero = false;
        // 读取类型：脉冲/速度
        private OptionType readType = OptionType.NONE;

        private bool isOpen = false;

        // 1.USB通信和数据采集连接检测
        // 2.通信超时处理

        public bool isRun = false, isStop = true;

        public string byteToHexStr(byte[] bytes)
        {
            string returnStr = "";
            if (bytes != null)
            {
                for (int i = 0; i < bytes.Length; i++)
                {
                    returnStr += bytes[i].ToString("X2");
                }
            }

            return returnStr;
        }

        #region 电机控制方法

        public bool Init()
        {
            isOpen = false;
            if (OpenPort())
            {
                SendCMD(OptionType.SERVO_OFF_START, CMD_SERVO_OFF_START);
                Thread.Sleep(50);
                ClosePort();
            }

            return isOpen;
        }

        // 关闭设备
        public void Relese()
        {
            if (OpenPort())
            {
                SendCMD(OptionType.RUN_STOP, CMD_RUN_STOP);
                Thread.Sleep(50);
                SendCMD(OptionType.SERVO_OFF_START, CMD_SERVO_OFF);
                Thread.Sleep(50);
                ClosePort();
            }
        }

        // 设备初始化
        public void InitDeveice()
        {
           // if (MessageQueue.Exists(MQ_PATH)) { MessageQueue.Delete(MQ_PATH); }
            MotorOpen();
            Thread.Sleep(100);
            ServoOn();//设备使能
        }

        // 打开设备
        public bool MotorOpen()
        {
            Task ReadTask = new Task(() =>
            {
                while (isRun)
                {
                    MsgModel msg = ReceiveComplexMsg();
                    if (msg != null)
                    {
                        // 这里接收,同步串口发送
                        SendCMD(msg.type, msg.cmd);
                    }
                    else if(!isZero)
                    {
                        // 发送检测速度或脉冲命令
                        if (readType == OptionType.RUN_PULSE) { SendCMD(OptionType.RUN_PULSE, CMD_RUN_PULSE); }
                        if (readType == OptionType.RUN_SPEED) { SendCMD(OptionType.RUN_SPEED, CMD_RUN_SPEED); }
                    }

                    Thread.Sleep(25);
                }
            });
            ReadTask.Start();
    
            return OpenPort();
        }

        // 设备使能
        public void ServoOn()
        {
            SendComplexMsg(OptionType.SERVO_ON, CMD_SERVO_ON);
        }

        // 设备不使能
        public void ServoOff()
        {
            readType = OptionType.NONE;
            queue.Clear();
            CurrentSpeed = 0;
            SendComplexMsg(OptionType.SERVO_OFF, CMD_SERVO_OFF);
        }

        // 关闭设备
        public void DeveiceClose()
        {
            SendCMD(OptionType.RUN_STOP, CMD_RUN_STOP);
            Thread.Sleep(50);
            ServoOff();
            Thread.Sleep(50);
            ClosePort();
        }

        // 电机转动一直运动,正转0/反转1
        public void MotorMoveVel(int direction)
        {
            SendComplexMsg(OptionType.RUN_START, direction == 0 ? CMD_RUN_START1 : CMD_RUN_START2);
        }

        // 改变电机速度
        public void ChangeSpeed(double newSpeed)
        {
            readType = OptionType.RUN_SPEED;
            int targetSpeed = (int)newSpeed;

            ConvertSpeedCMD(targetSpeed);
            SendComplexMsg(OptionType.SPEED_SET, CmdSpeed);
        }

        // 电机转动停止
        public void MoveStop()
        {
            // 降速到StopSpeed，再停止
            int speed = GetMotorSpeed();
            ConvertSpeedCMD(StopSpeed);
            SendComplexMsg(OptionType.RUN_STOP, CmdSpeed);
            Thread.Sleep(speed*5);
            SendComplexMsg(OptionType.RUN_STOP, CMD_RUN_STOP);
        }

        // 立马停止
        public void MoveStopNow()
        {
            SendComplexMsg(OptionType.RUN_STOP, CMD_RUN_STOP);
        }

        // 返回脉冲数
        public int GetCmdPosition()
        {
            return isZero ? 0 : (Math.Abs(CurrentPulse) - Math.Abs(InitPulse));
        }

        // 获得电机速度
        public int GetMotorSpeed()
        {
            return Math.Abs(CurrentSpeed) / 10;
        }

        // 脉冲初始化
        public void ClearCmdPosition()
        {
            isZero = true;
            readType = OptionType.RUN_PULSE;
            queue.Clear();
            InitPulse = CurrentPulse;
            SendComplexMsg(OptionType.RUN_PULSE, CMD_RUN_PULSE);
        }

        // 设置初始速度
        public void SetVelParam(double AxVelLow, double AxVelHigh, double AxAcc, double AxDec)
        {
            int speed = (int)AxVelHigh;
            ConvertSpeedCMD(speed);
            SendComplexMsg(OptionType.SPEED_SET, CmdSpeed);
            Thread.Sleep(5);
            CurrentSpeed = speed * 10;
        }

        // 转换速度命令
        private void ConvertSpeedCMD(int speed)
        {
            byte[] hex = Utils.DecimalToHex(speed.ToString());
            CmdSpeed[4] = hex[1];
            CmdSpeed[5] = hex[0];
            byte[] crc16 = Utils.CRC16(CmdSpeed, CmdSpeed.Length - 2);
            CmdSpeed[6] = crc16[0];
            CmdSpeed[7] = crc16[1];
        }

        #endregion

        #region 串口操作

        SerialPort mySerialPort = new SerialPort();

        // 寻找可用串口
        private void SearchAvailablePorts()
        {
            string[] allAvailablePorts = SerialPort.GetPortNames();
            foreach (string availablePort in allAvailablePorts)
            {
                //MessageBox.Show(availablePort);
            }
        }

        // 打开串口
        private bool OpenPort()
        {
            string portName = "COM3";
            isRun = true;

            // 921600 8 N 2
            mySerialPort.PortName = portName;
            mySerialPort.BaudRate = 921600;
            mySerialPort.DataBits = 8;
            mySerialPort.Parity = Parity.None;
            mySerialPort.StopBits = StopBits.Two;

            try
            {
                mySerialPort.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                return false;
            }

            return true;
        }

        // 关闭串口
        private void ClosePort()
        {
            isRun = false;
            if (mySerialPort != null && mySerialPort.IsOpen)
            {
                mySerialPort.Close();
            }
        }

        // 串口发送
        private void SendCMD(OptionType type, byte[] msgByte)
        {
            if (mySerialPort == null || !mySerialPort.IsOpen) { return; }
            try
            {
                mySerialPort.Write(msgByte, 0, msgByte.Length);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return;
            }
            //Console.WriteLine("Send:" + byteToHexStr(msgByte));

            // 同步接收
            int count = 0;
            int timeout = 500;
            while (mySerialPort.IsOpen && mySerialPort.BytesToRead == 0)
            {
                Thread.Sleep(1);
                count++;
                if (count == timeout)    // 超时
                {
                    Console.WriteLine("超时");
                    break;
                }
            }

            if (count >= timeout || !mySerialPort.IsOpen) { return; }
            //Thread.Sleep(20);
            //if (!mySerialPort.IsOpen || mySerialPort.BytesToRead == 0) { return; }

            byte[] receiveData = new byte[mySerialPort.BytesToRead];
            try
            {
                mySerialPort.Read(receiveData, 0, receiveData.Length);
                isOpen = true;
            }
            catch (Exception ex)
            { 
                Console.WriteLine(ex.ToString());
                return;
            }

            //Console.WriteLine("Receive:" + byteToHexStr(receiveData));
            if (type == OptionType.RUN_SPEED) // 速度
            {
                CurrentSpeed = ConvertToNum(receiveData);
                //Console.WriteLine("" + GetMotorSpeed());
            }
            else if (type == OptionType.RUN_PULSE) // 脉冲
            {
                int result = ConvertToNum(receiveData);
                if (isZero)
                {
                    InitPulse = result;
                    isZero = false;
                }
                CurrentPulse = result;
                // Console.WriteLine("Pulse:" + CurrentPulse);
            }
            //else
            //{
            //    // 检测命令是否一样
            //    if (receiveData.Length < (msgByte.Length + 3))
            //    {
            //        Console.WriteLine("Error1:" + type.ToString());
            //    }
            //    else
            //    {
            //        for (int i = 0; i < msgByte.Length; i++)
            //        {
            //            if (msgByte[i] != receiveData[i + 3])
            //            {
            //                Console.WriteLine("Error2");
            //                break;
            //            }
            //        }
            //    }
            //}

        }

        // 2个16bit转换成1个32bit数值
        private int ConvertToNum(byte[] receiveData)
        {
            if (receiveData.Length < 12) { return 0; }
            byte[] cmd = { receiveData[3], receiveData[4], receiveData[5], receiveData[6], receiveData[7], receiveData[8], receiveData[9], receiveData[10], receiveData[11] };
            if (receiveData[4] == 0x03 && receiveData[5] == 0x04 && Utils.CalculateCrc(cmd, cmd.Length) == 0x00)
            {
                //Console.WriteLine(byteToHexStr(receiveData));
                byte[] arrInt32 = new byte[4];
                arrInt32[1] = receiveData[6];
                arrInt32[0] = receiveData[7];
                arrInt32[3] = receiveData[8];
                arrInt32[2] = receiveData[9];
                int result = BitConverter.ToInt32(arrInt32, 0);

                return result;
            }

            return 0;
        }

        #endregion

        #region MSMQ消息队列

         private volatile Queue<MsgModel> queue = new Queue<MsgModel>();
        // 发送MSMQ消息
        //string MQ_PATH = @".\private$\MsgQueue1";
        public void SendComplexMsg(OptionType type, byte[] msgByte)
        {
            //if (!MessageQueue.Exists(MQ_PATH)) { MessageQueue.Create(MQ_PATH); }

            //MessageQueue MQ = new MessageQueue(MQ_PATH);
            //Message message = new Message();
            //message.Label = "电机命令";
            //message.Body = new MsgModel(type, msgByte);
            //MQ.Send(message);
            MsgModel msgModel=new MsgModel(type, msgByte);
            queue.Enqueue(msgModel);
        }

        // 接收MSMQ消息
        private MsgModel ReceiveComplexMsg()
        {
            //if (!MessageQueue.Exists(MQ_PATH)) { MessageQueue.Create(MQ_PATH); }

            //MessageQueue MQ = new MessageQueue(MQ_PATH);
            //if (MQ.GetAllMessages().Length > 0)
            //{
            //    System.Messaging.Message message = MQ.Receive(TimeSpan.FromSeconds(5));
            //    if (message != null)
            //    {
            //        message.Formatter = new System.Messaging.XmlMessageFormatter(new Type[] { typeof(MsgModel) });//消息类型转换
            //        MsgModel msg = (MsgModel)message.Body;

            //        return msg;
            //    }
            //}

            if (queue.Count > 0)
            {
                MsgModel msgModel = queue.Dequeue();
                return msgModel;
            }

            return null;
        }

        // 电机控制类型
        public enum OptionType
        {
            NONE = 0,
            SERVO_OFF_START = 1,
            SERVO_ON = 2, // 使能
            SERVO_OFF = 3, // 不使能
            SPEED_SET = 4,
            RUN_START = 5, // 转动
            RUN_STOP = 6, // 停止
            RUN_SPEED = 7, // 速度
            RUN_PULSE = 8 // 脉冲
        }

        // 自定义消息类型
        [Serializable]
        public class MsgModel
        {
            public OptionType type { get; set; } // 命令类型
            public byte[] cmd { get; set; } // 命令数据

            public MsgModel() { }
            public MsgModel(OptionType type, byte[] cmd)
            {
                this.type = type;
                this.cmd = cmd;
            }
        }

        #endregion

    }
}
