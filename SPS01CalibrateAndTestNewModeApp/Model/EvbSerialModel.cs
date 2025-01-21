using System;
using System.ComponentModel;
using System.IO.Ports;
using System.Linq;
using System.Windows.Forms;
using SPS01CalibrateAndTestNewModeApp.Enume;

namespace SPS01CalibrateAndTestNewModeApp.Mode
{
    public class EvbSerialModel:INotifyPropertyChanged
    {
        private readonly SerialPort _serialPort;
        private string _receivedData;
        private string _curMode;
        private string _connResult;
        private string _deviceAddress = "D8";
        public readonly EvbCommandOfEngine _evbCommandOfEngine;
        public readonly EvbCommandOfWork _evbCommandOfWork;
        public readonly EvbCommandOfConn _evbCommandOfConn;
        public readonly EvbCommandOfRaw _evbCommandOfRaw;
        private string _connModeName;
        
        public EvbSerialModel()
        {
            _serialPort = new SerialPort();
            _evbCommandOfEngine = new EvbCommandOfEngine();
            _evbCommandOfWork = new EvbCommandOfWork();
            _evbCommandOfConn = new EvbCommandOfConn();
            _evbCommandOfRaw = new EvbCommandOfRaw();
            _serialPort.DataReceived += SerialPort_DataReceived;
        }

        public string ReceivedData
        {
            get { return _receivedData; }
            set
            {
                Console.WriteLine("ReceivedData: " + value);
                _receivedData = value;
                OnPropertyChanged("ReceivedData");
            }
        }
        
        public string ConnModeName
        {
            get { return _connModeName; }
            set
            {
                Console.WriteLine("ConnModeName: " + value);
                _connModeName = value;
                OnPropertyChanged("ConnModeName");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void SerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                var receive = "";
                if (_curMode == "ReadAllReg")
                {
                     while(_serialPort.BytesToRead > 0)
                     {
                         var bytes = new byte[_serialPort.BytesToRead];
                         for (var i = 0; i < bytes.Length; i++)
                         {
                             bytes[i] = (byte)_serialPort.ReadByte();
                             receive += bytes[i].ToString("X2");
                         }
                         System.Threading.Thread.Sleep(5);
                     }
                }
                else
                {
                    while (_serialPort.BytesToRead > 0)
                    {
                        receive += _serialPort.ReadExisting();
                        System.Threading.Thread.Sleep(5);
                    }
                }
                
                if (receive != "")
                {
                    //return;
                    ReceivedData = receive;
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
                            _connResult = "ACK";
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
            catch (Exception ex)
            {
                // 处理异常，例如显示错误消息
                MessageBox.Show($"Error reading from serial port: {ex.Message}");
            }
        }

        public void OpenPort(string portName, int baudRate)
        {
            if (!_serialPort.IsOpen)
            {
                _serialPort.PortName = portName;
                _serialPort.BaudRate = baudRate;
                
                Console.WriteLine("Opening serial port " + portName + "Bps" + baudRate);
                try
                {
                    _serialPort.Open();
                }
                catch (Exception ex)
                {
                    // 处理异常，例如显示错误消息
                    MessageBox.Show($"Error opening serial port: {ex.Message}");
                }
            }
        }

        public bool ConnectTest()
        {
            if (_serialPort.IsOpen)
            {
                var cmd = "@UART\r";
                _serialPort.Write(cmd);
                var count = 0;
                _curMode = "ConnTs";
                while (true)
                {
                    System.Threading.Thread.Sleep(5);
                    if (_connResult != null)
                    {
                        //break;
                        _connResult = null;
                        return true;
                    }
                    count++;
                    if (count > 20)
                    {
                        return false;
                    }
                }
            }
            return false;
        }

        public bool ConnPd()
        {
            _connResult = null;
            _curMode = "ConnPd";
            const string cmd = "@PON\r";
            _serialPort.WriteLine(cmd);
            SetComMode("STRT_CM","01",false);
            var count = 0;
            while (true)
            {
                System.Threading.Thread.Sleep(5);
                if (_connResult != null)
                {
                    //break;
                    _connResult = null;
                    return true;
                }
                count++;
                if (count > 40)
                {
                    return false;
                }
            }

        }
        
        public void SetComMode(string mode,string length, bool ismode=false)
        {
            if (!_evbCommandOfEngine.RunMode.Keys.Contains(mode)) return;
            if (ismode)
            {
                _curMode = mode;
            }

            var cmd = "@" + _evbCommandOfConn.ConnMode[ConnModeName] +"W"+ length + _deviceAddress + _evbCommandOfEngine.RunMode[mode]+"\r";
            // System.Diagnostics.Debug.WriteLine("cmd:" + cmd);
            _serialPort.WriteLine(cmd);
        }

        public int GetData(int jump, int avg, string register)
        {
            if (_serialPort.IsOpen == false)
            {
                return 0;
            }
            var sum = 0;
            // 命令格式：@IR读取的长度+设备地址+读取模式+寄存器地址
            for (var i = 0; i < (jump + avg); i++)
            {
                System.Threading.Thread.Sleep(50);
                _curMode = "ReadAllReg";
                var cmd = $"@{_evbCommandOfConn.ConnMode[ConnModeName]}R02{_deviceAddress}{_evbCommandOfWork.WorkMode["RD_OUTMEM_BURST"]}{_evbCommandOfRaw.RawAddr[register]}\r";
                _serialPort.WriteLine(cmd);
                while (_curMode != null)
                {
                    System.Threading.Thread.Sleep(5);
                }
                
                if (i >= jump)
                {
                    sum += Convert.ToInt32(ReceivedData.Substring(0, 4), 16);
                }
            }
            return sum / avg;
        }

        public int GetP1Data(int jump, int avg)
        {
            return GetData(jump, avg, "P1");
        }

        public int GetP2Data(int jump, int avg)
        {
            return GetData(jump, avg, "P2");
        }

        public int GetTSIData(int jump, int avg)
        {
            return GetData(jump, avg, "TSI");
        }

        public int GetTSEData(int jump, int avg)
        {
            return GetData(jump, avg, "TSE");
        }



        public void ClosePort()
        {
            if (_serialPort.IsOpen)
            {
                _serialPort.Close();
            }
        }

        public void SendData(string data)
        {
            if (_serialPort.IsOpen)
            {
                try
                {
                    _serialPort.Write(data);
                }
                catch (Exception ex)
                {
                    // 处理异常，例如显示错误消息
                    MessageBox.Show($"Error sending data: {ex.Message}");
                }
            }
        }
    }
}