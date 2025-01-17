using System;
using System.ComponentModel;
using System.IO.Ports;
using System.Linq;
using System.Windows.Forms;
using SPS01CalibrateAndTestNewModeApp.Enume;

namespace SPS01CalibrateAndTestNewModeApp.Mode
{
    public class EvbSerialMode:INotifyPropertyChanged
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
        public EvbSerialMode()
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
                if (_receivedData!= value)
                {
                    _receivedData = value;
                    OnPropertyChanged("ReceivedData");
                }
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
                    _receivedData = receive;
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

        public bool ConnPd(string connModeName)
        {
            _connResult = null;
            _curMode = "ConnPd";
            const string cmd = "@PON\r";
            _serialPort.WriteLine(cmd);
            SetComMode("STRT_CM","01",connModeName,false);
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
        
        public void SetComMode(string mode,string length,string connModeName, bool ismode=false)
        {
            if (!_evbCommandOfEngine.RunMode.Keys.Contains(mode)) return;
            if (ismode)
            {
                _curMode = mode;
            }

            var cmd = "@" + _evbCommandOfConn.ConnMode[connModeName] +"W"+ length + _deviceAddress + _evbCommandOfEngine.RunMode[mode]+"\r";
            // System.Diagnostics.Debug.WriteLine("cmd:" + cmd);
            _serialPort.WriteLine(cmd);
        }

        public void ReadAllReg()
        {
            
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