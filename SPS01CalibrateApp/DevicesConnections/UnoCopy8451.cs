using System;
using System.Linq;
using System.Text;
using System.IO.Ports;
using log4net;
using System.Threading;
using System.Windows.Forms;

namespace TPLoopTestSystem
{
    public class UnoCopy8451
    {
        private readonly string[] _switchList = new string[64];
        private readonly string[] _switch595List = new string[64];
        public bool Flag = false;
        
        private readonly TimeLocal _timeLocal = new TimeLocal();

        public string PortName { get; set; }
        public int BaudRate { get; set; } = 9600;
        private SerialPort Serial { get; }
        public string VersionInfo { get; private set; }
        private string SwitchResult { get; set; }
        public bool IsConnect { get; private set; }
        public string ReceiveData { get; private set; }
        private static ILog Log { get; } = LogManager.GetLogger(typeof(UnoCopy8451));

        private static string StrSum(string str)
        {
            // 字符串转为ASCII 码,在计算求和
            var array = Encoding.ASCII.GetBytes(str);
            var sum = (byte)array.Aggregate(0, (current, t) => current + t);
            return str + sum.ToString("X2");
            //return str+sum.ToString();
        }


        public UnoCopy8451()
        {
            Serial = new SerialPort();
            SwitchList();
            Switch595List();
            Serial.DataReceived += DataReceived;

        }

        public void Open()
        {

            try
            {
                IsConnect = false;
                VersionInfo = null;
                if (Serial.IsOpen)
                {
                    Serial.Close();
                }
                else
                {
                    Serial.PortName = PortName;
                    Serial.BaudRate = BaudRate;
                    Serial.DataBits = 8;
                    Serial.StopBits = StopBits.One;
                    Serial.Parity = Parity.None;
                    Serial.ReadTimeout = 500;
                    Serial.WriteTimeout = 500;
                }
                Serial.Open();

                Send("AAZ0000");
                var count = 0;
                while (VersionInfo == null)
                {
                    TimeLocal.DelayMs(200);
                    count++;
                    if (count > 10)
                    {
                        break;
                    }
                }
                IsConnect = true;
                System.Diagnostics.Debug.WriteLine("VersionInfo:" + VersionInfo);
                Log.Info("VersionInfo:" + VersionInfo);
            }
            catch
            {
                VersionInfo = null;
                MessageBox.Show("串口打开失败");
                Log.Error("串口打开失败");
            }
        }

        public void Close()
        {
            if (Serial.IsOpen)
            {
                Serial.Close();
            }
            //serial.Close();
        }

        private void DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            // var data = "";
            ReceiveData = "";
            while(Serial.BytesToRead > 0)
            {
                ReceiveData += Serial.ReadExisting();
                Thread.Sleep(5);
            }
            if (ReceiveData.Contains("BBY"))
            {
                Log.Info("板子名称信息:" + ReceiveData);
                System.Diagnostics.Debug.WriteLine("data:" + ReceiveData);
            }
            else if(ReceiveData.Contains("BBZ"))
            {
                Log.Info("板子版本信息:" + ReceiveData);
                System.Diagnostics.Debug.WriteLine("data:" + ReceiveData);
                VersionInfo = ReceiveData;
            }
            else if (ReceiveData.Contains("BBD"))
            {
                Log.Info("开关切换:" + ReceiveData);
                System.Diagnostics.Debug.WriteLine("data:" + ReceiveData);
                SwitchResult = ReceiveData;
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("data:" + ReceiveData);
            }
        }

        private void Send(string str)
        {
            var sendStr = StrSum(str);
            Serial.Write(sendStr+"\n");
        }

        public void Switch(int index)
        {
            Console.WriteLine("切换到："+index);
            Send(_switchList[index]);
            var count = 0;
            while (true)
            {
                if (SwitchResult == StrSum("BB"+_switchList[index].Substring(2)))
                {
                    //System.Threading.Thread.Sleep(100);
                    return;
                    //break;
                }
                if (count > 10)
                {
                    Log.Error("切换失败"+SwitchResult);
                    return;
                    //break;
                }
                count++;
            }
        }

        private void SwitchList()
        {
            //string[] strings = new string[64];
            for (var i = 0;i<8;i++)
            {
                var topStatus = ~(0x01U << i)&0xFFU;
                uint botStatus = 0x00;
                for (uint j = 0; j < 8; j++ )
                {
                    _switchList[i * 8 + j] = "AAD0" + (topStatus * 8 + botStatus).ToString("X2");
                    botStatus = 0x01U + j;
                }
            }

        }

        private void Switch595List()
        {
            for (var i = 0; i < 16; i++)
            {
                var topStatus = (0x01U << i);
                for (uint j = 0; j < 4; j++)
                {
                    _switch595List[i * 4 + j] = "AAW" + j.ToString("X2")+topStatus.ToString("X4");
                    //Console.WriteLine(switch595List[i * 4 + j]);
                }
            }
        }


    }

   
}
