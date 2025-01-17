using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.Security.Cryptography;
using System.Drawing.Printing;
using System.Windows.Forms;
using System.Diagnostics.Eventing.Reader;

namespace SPS01CalibrateApp
{    
    public class SPScom
    {

        private string _curMode;
        private string _connResult;
        private string _connPdResult;
        public string PortName { get; set; }
        public readonly SerialPort SerialPort;

        private Dictionary<string,string> ComMode { get; set; }
        private Dictionary<string, string> ComMemMode { get; set; }
        public Dictionary<string, string> RawAddr { get; set; }
        private Dictionary<string, string> ConnMode { get; set; }

        public string DeviceAddr { get; set; }
        public string Receive { get; private set; }
        public string ConnModeName { get; set; }
        private string Id { get; set; }
        
        public SPScom()
        {
            SerialPort = new SerialPort();
            /*
             *  STRT_CM     013C5B
                STRT_NM     020000
                STRT_MEAS   030000
                STRT_SENT   040000
                STOP_MEAS   050000
                STOP_SENT   060000    
                SOFTRESET   070000
             * 
             **/
            ComMode = new Dictionary<string, string> { { "STRT_CM", "013C5B" }, { "STRT_NM", "020000" }, { "STRT_MEAS", "030000" }, { "STRT_SENT", "040000" }, { "STOP_MEAS", "050000" }, { "STOP_SENT", "060000" }, { "SOFTRESET", "070000" } };   
            /*
             * RD_NVMREG_BYTE   10
             RD_NVMREG_BURST    11
             WR_NVMREG_BYTE     13
             WR_NVMREG_BURST    14
             RD_OUTMEM_BYTE     20
             RD_OUTMEM_BURST    21
             WR_OUTMEM_BYTE     22      
             WR_OUTMEM_BURST    23
             OPEN_NVM           81
             CLOSE_NVM          82
             RD_NVM_BYTE        83
             RD_NVM_BURST       84
             ERS_NVM_BYTE       85
             ERS_NVM_BULK       86
             PROG_NVM_BYTE      87
             PROG_NVM_BULK      88
             CP_NVMTOREG        A0
             CP_REGTONVM        A1
             WR_NVMWR_AUTH      B0
             RD_NWMWR_VLD       B1
             GEN_NVMCRC         C3
             CHK_NVMCRC         E3
             */
            ComMemMode  = new Dictionary<string, string> { { "RD_NVMREG_BYTE", "10" }, { "RD_NVMREG_BURST", "11" }, { "WR_NVMREG_BYTE", "13" }, { "WR_NVMREG_BURST", "14" }, { "RD_OUTMEM_BYTE", "20" }, { "RD_OUTMEM_BURST", "21" }, { "WR_OUTMEM_BYTE", "22" }, { "WR_OUTMEM_BURST", "23" }, { "OPEN_NVM", "81" }, { "CLOSE_NVM", "82" }, { "RD_NVM_BYTE", "83" }, { "RD_NVM_BURST", "84" }, { "ERS_NVM_BYTE", "85" }, { "ERS_NVM_BULK", "86" }, { "PROG_NVM_BYTE", "87" }, { "PROG_NVM_BULK", "88" }, { "CP_NVMTOREG", "A0" }, { "CP_REGTONVM", "A1" }, { "WR_NVMWR_AUTH", "B0" }, { "RD_NWMWR_VLD", "B1" }, { "GEN_NVMCRC", "C3" }, { "CHK_NVMCRC", "E3" } };

            RawAddr = new Dictionary<string, string> { { "P1", "00" }, { "P2", "02" }, { "P3", "04" }, { "TSI", "06" }, { "TSE", "08" }, { "VDDA", "0A" }, { "P1O", "0E" }, { "P2O", "10" }, { "TSIO", "12" }, { "TSEO", "14" }, { "P1DAC", "1E" }, { "P1SENT", "16" }, { "P2SENT", "18" }, { "P1VOFF", "20" } ,{ "P1FG","23"},{ "P2VOFF","26"},{ "P2FG","29"} };

            ConnMode = new Dictionary<string, string> { { "OWI", "O" }, { "IIC", "I" } };
            SerialPort.DataReceived += DataReceivedHandler; 
        }

        public void Open()
        {
            try
            {

                if (SerialPort.IsOpen)
                {
                    SerialPort.Close();
                }
                SerialPort.PortName = PortName;
                SerialPort.BaudRate = 57600;
                SerialPort.DataBits = 8;
                SerialPort.Parity = Parity.None;
                SerialPort.StopBits = StopBits.One;
                SerialPort.ReadTimeout = 1000;
                SerialPort.WriteTimeout = 1000;
                SerialPort.Open();
            }
            catch
            {
                MessageBox.Show("串口打开失败");
                
            }

        }

        public void Close()
        {
            if (SerialPort.IsOpen)
            {
                SerialPort.Close();
            }
        }

        public bool ConnTs()
        {
            if (!SerialPort.IsOpen)
            {
                //open();
                return false;
            }
            _curMode = "ConnTs";
            System.Diagnostics.Debug.WriteLine("ConnTs");
            _connResult = null;
            var cmd = "@UART\r";
            SerialPort.WriteLine(cmd);
            var count = 0;

            while (true)
            {
                System.Threading.Thread.Sleep(5);
                if (_connResult != null)
                {
                    //break;
                    return true;
                }
                count++;
                if (count > 20)
                {
                    return false;
                }
            }

        }

        public bool ConnPd()
        {
            _connPdResult = null;
            _curMode = "ConnPd";
            const string cmd = "@PON\r";
            SerialPort.WriteLine(cmd);
            SetComMode("STRT_CM","01",false);
            var count = 0;
            while (true)
            {
                System.Threading.Thread.Sleep(5);
                if (_connPdResult != null)
                {
                    //break;
                    return true;
                }
                count++;
                if (count > 40)
                {
                    return false;
                }
            }

        }

        public void Pd2Nm()
        {   
            _curMode = "ConnPd";
            //string cmd = "@PON\r";
            SetComMode("STRT_NM", "01", false);
            var count = 0;
            while (true)
            {
                System.Threading.Thread.Sleep(5);
                if (_connPdResult != null)
                {
                    //break;
                    return;
                }
                count++;
                if (count > 40)
                {
                    return;
                }
            }
        }

        public void SetComMode(string mode,string length, bool ismode=false)
        {
            if (!ComMode.Keys.Contains(mode)) return;
            if (ismode)
            {
                _curMode = mode;
            }

            var cmd = "@" + ConnMode[ConnModeName] +"W"+ length + DeviceAddr + ComMode[mode]+"\r";
            System.Diagnostics.Debug.WriteLine("cmd:" + cmd);
            SerialPort.WriteLine(cmd);
        }

        public void SetMemMode(string mode, string value)
        {
            if (!ComMemMode.Keys.Contains(mode)) return;
            _curMode = mode;
            var cmd = "@" + ConnMode[ConnModeName] +"W" + DeviceAddr + ComMemMode[mode] + value;
            SerialPort.WriteLine(cmd);
        }

        public string GetAllReg()
        {

            SetComMode("STRT_CM", "01", true);
            System.Threading.Thread.Sleep(50);

            var reg = "";
            for (var i = 0; i < 4; i++) { 
                _curMode = "ReadAllReg";
                var cmd = "@" + ConnMode[ConnModeName] +"R40" + DeviceAddr + ComMemMode["RD_NVMREG_BURST"] + (i*64).ToString("X2") +"\r";
                SerialPort.WriteLine(cmd);
                var count = 0;
                while (_curMode != null)
                {
                    count++;
                    if (count > 20)
                    {
                        break;
                    }
                    //Console.WriteLine(reg);-
                    System.Threading.Thread.Sleep(5);
                }
                if (Receive.Length < 128)
                {
                    return "";
                }
                reg += Receive.Substring(0, 128);
                //reg = Receive;
                //Console.WriteLine(reg);

            }

            return reg;

        }

        public string GetId()
        {
            SetComMode("STRT_CM", "01", true);
            System.Threading.Thread.Sleep(50);
            _curMode = "ReadAllReg";
            var cmd = "@" + ConnMode[ConnModeName] + "R04" + DeviceAddr + ComMemMode["RD_NVMREG_BURST"] + "F9" + "\r";
            SerialPort.WriteLine(cmd);
            var count = 0;
            while (_curMode!= null)
            {
                count++;
                if (count > 20)
                {
                    return "";
                    // break;
                }
                System.Threading.Thread.Sleep(5);
            }
            Id = Receive.Substring(0, 8);
            return Id;
        }

        public void SetId(string id)
        {
            SetComMode("STRT_CM", "01", true);
            System.Threading.Thread.Sleep(50);
            _curMode = "ReadAllReg";
            RunScript("12F9"+id.Substring(0,2));
            RunScript("12FA" + id.Substring(2, 2));
            RunScript("12FB" + id.Substring(4, 2));
            RunScript("12FC" + id.Substring(6, 2));
            RunScript("B03C5B");
            RunScript("810000");
            RunScript("A10000");
            RunScript("A00000");
            System.Threading.Thread.Sleep(300);
            RunScript("820000");
        }

        public string GetAllNvm()
        {
            SetComMode("STRT_CM", "01", true);
            System.Threading.Thread.Sleep(50);
            RunScript("B03C5B");
            RunScript("810000");
            System.Threading.Thread.Sleep(50);
            var nvm = "";
            for (var i = 0; i < 4; i++)
            {
                _curMode = "ReadAllReg";
                var cmd = "@" + ConnMode[ConnModeName] +"R40" + DeviceAddr + ComMemMode["RD_NVM_BURST"] + (i * 64).ToString("X2") + "\r";
                SerialPort.WriteLine(cmd);
                var count = 0;
                while (_curMode != null)
                {
                    count++;
                    if (count > 20)
                    {
                        break;
                    }
                    System.Threading.Thread.Sleep(5);
                }
                nvm += Receive.Substring(0, 128);

            }
            RunScript("820000");

            return nvm;

        }

        public int GetRawdata(string rawMode,int jump,int avg)
        {

            if (SerialPort.IsOpen == false)
            {
                return 0;
            }
            var sum = 0;
            // 命令格式：@IR读取的长度+设备地址+读取模式+寄存器地址
            //SetComMode("STRT_MEAS", "01", true);
            for (var i = 0; i < (jump+avg); i++)
            {

                System.Threading.Thread.Sleep(50);
                _curMode = "ReadAllReg";
                var cmd = "@" + ConnMode[ConnModeName] +"R02" + DeviceAddr + ComMemMode["RD_OUTMEM_BURST"] + RawAddr[rawMode] + "\r";
                //System.Diagnostics.Debug.WriteLine("cmd:" + cmd);
                SerialPort.WriteLine(cmd);
                while (_curMode != null)
                {
                    System.Threading.Thread.Sleep(5);
                }
                if (i >= jump)
                {
                    sum += Convert.ToInt32(Receive.Substring(0, 4), 16);
                }
            }
            return sum / avg;
            //return 0;
        }

        public int GetMidData(string rawMode, int jump, int avg)
        {

            if (SerialPort.IsOpen == false)
            {
                return 0;
            }
            var sum = 0;
            // 命令格式：@IR读取的长度+设备地址+读取模式+寄存器地址
            //SetComMode("STRT_MEAS", "01", true);
            for (var i = 0; i < (jump + avg); i++)
            {

                System.Threading.Thread.Sleep(50);
                _curMode = "ReadAllReg";
                var cmd = "@" + ConnMode[ConnModeName] +"R03" + DeviceAddr + ComMemMode["RD_OUTMEM_BURST"] + RawAddr[rawMode] + "\r";
                System.Diagnostics.Debug.WriteLine("cmd:" + cmd);
                SerialPort.WriteLine(cmd);
                while (_curMode != null)
                {
                    System.Threading.Thread.Sleep(5);
                }
                if (i >= jump)
                {
                    sum += Convert.ToInt32(Receive.Substring(0, 6), 16);
                }
            }
            return sum / avg;
        }


        private void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {
            //SerialPort sp = (SerialPort)sender;

            var receive = "";
            if (_curMode == "ReadAllReg")
            {
                 while(SerialPort.BytesToRead > 0)
                 {
                     var bytes = new byte[SerialPort.BytesToRead];
                     for (var i = 0; i < bytes.Length; i++)
                     {
                         bytes[i] = (byte)SerialPort.ReadByte();
                         receive += bytes[i].ToString("X2");
                     }
                     System.Threading.Thread.Sleep(5);
                 }
            }
            else
            {
                while (SerialPort.BytesToRead > 0)
                {
                    receive += SerialPort.ReadExisting();
                    System.Threading.Thread.Sleep(5);
                }
            }
            
            //System.Diagnostics.Debug.WriteLine("receive"+ receive);/
            //System.Diagnostics.Debug.WriteLine("curmode"+ curmode);
            if (receive != "")
            {
                //return;
                Receive = receive;
            }
            switch (_curMode)
            {
                case "ConnTs":
                    if (receive.Contains("ACK"))
                    {
                        _curMode = null;
                        _connResult = "ACK";
                    }
                    else
                    { }
                    break;
                case "ConnPd":
                    //System.Diagnostics.Debug.WriteLine("ConnPd");
                    if (receive.Contains("ACK"))
                    {
                        _curMode = null;
                        _connPdResult = "ACK";
                    }
                    break;
                case "ReadAllReg":
                    if (receive.Contains("41434B"))
                    {
                        _curMode = null;
                        //System.Diagnostics.Debug.WriteLine("ReadAllReg");
                    }
                    break;
                default:
                    break;
            }
        }

        internal void RunScript(string v)
        {
            //throw new NotImplementedException();
            // 删除空白
            v = v.Trim();

            var cmd = "@" + ConnMode[ConnModeName] +"W01" + DeviceAddr + v + "\r";
            SerialPort.WriteLine(cmd);
            System.Diagnostics.Debug.WriteLine("cmd:"+cmd);
            System.Threading.Thread.Sleep(10);
        }
    }
}
