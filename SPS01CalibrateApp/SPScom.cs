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

        private string curmode = null;
        private string connResult = null;
        private string connPdResult = null;
        public string PortName { get; set; }
        public SerialPort serialPort;

        public Dictionary<string,string> ComMode { get; set; }
        public Dictionary<string, string> ComMemMode { get; set; }
        public Dictionary<string, string> RawAddr { get; set; }

        public string DeviceAddr { get; set; }
        public string Receive { get; set; }

        
        public SPScom()
        {
            serialPort = new SerialPort();
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

            serialPort.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler); 
        }

        public void open()
        {
            try
            {

                if (serialPort.IsOpen)
                {
                    serialPort.Close();
                }
                serialPort.PortName = PortName;
                serialPort.BaudRate = 57600;
                serialPort.DataBits = 8;
                serialPort.Parity = Parity.None;
                serialPort.StopBits = StopBits.One;
                serialPort.ReadTimeout = 1000;
                serialPort.WriteTimeout = 1000;
                serialPort.Open();
            }
            catch
            {
                MessageBox.Show("串口打开失败");
                
            }

        }

        public void close()
        {
            if (serialPort.IsOpen)
            {
                serialPort.Close();
            }
        }

        public bool ConnTs()
        {
            if (!serialPort.IsOpen)
            {
                //open();
                return false;
            }
            curmode = "ConnTs";
            System.Diagnostics.Debug.WriteLine("ConnTs");
            connResult = null;
            string cmd = "@UART\r";
            serialPort.WriteLine(cmd);
            int count = 0;

            while (true)
            {
                System.Threading.Thread.Sleep(5);
                if (connResult != null)
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
            connPdResult = null;
            curmode = "ConnPd";
            string cmd = "@PON\r";
            serialPort.WriteLine(cmd);
            SetComMode("STRT_CM","01",false);
            int count = 0;
            while (true)
            {
                System.Threading.Thread.Sleep(5);
                if (connPdResult != null)
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
            curmode = "ConnPd";
            //string cmd = "@PON\r";
            SetComMode("STRT_NM", "01", false);
            int count = 0;
            while (true)
            {
                System.Threading.Thread.Sleep(5);
                if (connPdResult != null)
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
            if (ComMode.Keys.Contains(mode))
            {
                if (ismode)
                {
                    curmode = mode;
                }

                string cmd = "@IW"+ length + DeviceAddr + ComMode[mode]+"\r";
                System.Diagnostics.Debug.WriteLine("cmd:" + cmd);
                serialPort.WriteLine(cmd);
            }
        }

        public void SetMemMode(string mode, string value)
        {
            if (ComMemMode.Keys.Contains(mode))
            {
                curmode = mode;
                string cmd = "@IW" + DeviceAddr + ComMemMode[mode] + value;
                serialPort.WriteLine(cmd);
            }
        }

        public string GetAllReg()
        {

            SetComMode("STRT_CM", "01", true);
            System.Threading.Thread.Sleep(50);

            string reg = "";
            for (int i = 0; i < 4; i++) { 
                curmode = "ReadAllReg";
                string cmd = "@IR40" + DeviceAddr + ComMemMode["RD_NVMREG_BURST"] + (i*64).ToString("X2") +"\r";
                serialPort.WriteLine(cmd);
                int count = 0;
                while (curmode != null)
                {
                    count++;
                    if (count > 20)
                    {
                        break;
                    }
                    System.Threading.Thread.Sleep(5);
                }
                reg += Receive.Substring(0, 128);

            }

            return reg;

        }

        public string GetAllNvm()
        {
            SetComMode("STRT_CM", "01", true);
            System.Threading.Thread.Sleep(50);

            string nvm = "";
            for (int i = 0; i < 4; i++)
            {
                curmode = "ReadAllReg";
                string cmd = "@IR40" + DeviceAddr + ComMemMode["RD_NVM_BURST"] + (i * 64).ToString("X2") + "\r";
                serialPort.WriteLine(cmd);
                int count = 0;
                while (curmode != null)
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

            return nvm;

        }

        public int getRawdata(string rawMode,int jump,int avg)
        {

            if (serialPort.IsOpen == false)
            {
                return 0;
            }
            int sum = 0;
            // 命令格式：@IR读取的长度+设备地址+读取模式+寄存器地址
            //SetComMode("STRT_MEAS", "01", true);
            for (int i = 0; i < (jump+avg); i++)
            {

                System.Threading.Thread.Sleep(50);
                curmode = "ReadAllReg";
                string cmd = "@IR02" + DeviceAddr + ComMemMode["RD_OUTMEM_BURST"] + RawAddr[rawMode] + "\r";
                //System.Diagnostics.Debug.WriteLine("cmd:" + cmd);
                serialPort.WriteLine(cmd);
                while (curmode != null)
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

        public int getMidData(string rawMode, int jump, int avg)
        {

            if (serialPort.IsOpen == false)
            {
                return 0;
            }
            int sum = 0;
            // 命令格式：@IR读取的长度+设备地址+读取模式+寄存器地址
            //SetComMode("STRT_MEAS", "01", true);
            for (int i = 0; i < (jump + avg); i++)
            {

                System.Threading.Thread.Sleep(50);
                curmode = "ReadAllReg";
                string cmd = "@IR03" + DeviceAddr + ComMemMode["RD_OUTMEM_BURST"] + RawAddr[rawMode] + "\r";
                //System.Diagnostics.Debug.WriteLine("cmd:" + cmd);
                serialPort.WriteLine(cmd);
                while (curmode != null)
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



        public void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {
            //SerialPort sp = (SerialPort)sender;

            string receive = "";
            if (curmode == "ReadAllReg")
            {
                 
            }
            else
            {
                while (serialPort.BytesToRead > 0)
                {
                    receive += serialPort.ReadExisting();
                    System.Threading.Thread.Sleep(5);
                }
            }
            
            //System.Diagnostics.Debug.WriteLine("receive"+ receive);
            //System.Diagnostics.Debug.WriteLine("curmode"+ curmode);
            if (receive != "")
            {
                //return;
                Receive = receive;
            }
            switch (curmode)
            {
                case "ConnTs":
                    if (receive.Contains("ACK"))
                    {
                        curmode = null;
                        connResult = "ACK";
                    }
                    else
                    { }
                    break;
                case "ConnPd":
                    //System.Diagnostics.Debug.WriteLine("ConnPd");
                    if (receive.Contains("ACK"))
                    {
                        curmode = null;
                        connPdResult = "ACK";
                    }
                    break;
                case "ReadAllReg":
                    if (receive.Contains("41434B"))
                    {
                        curmode = null;
                        //System.Diagnostics.Debug.WriteLine("ReadAllReg");
                    }
                    break;
                default:
                    break;
            }
        }

        internal void runScript(string v)
        {
            //throw new NotImplementedException();
            // 删除空白
            v = v.Trim();

            string cmd = "@IW01" + DeviceAddr + v + "\r";
            serialPort.WriteLine(cmd);
            System.Diagnostics.Debug.WriteLine("cmd:"+cmd);
            System.Threading.Thread.Sleep(10);
        }
    }
}
