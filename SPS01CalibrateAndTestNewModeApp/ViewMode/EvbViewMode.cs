using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO.Ports;
using System.Linq;
using SPS01CalibrateAndTestNewModeApp.Mode;

namespace SPS01CalibrateAndTestNewModeApp.ViewMode
{
    public class EvbViewMode:INotifyPropertyChanged 
    {
        private readonly EvbSerialMode _evbSerialModel;
        private List<string> _portNames;
        private List<int> _baudRates;
        private List<string> _productConnectMode;

        private string _sendData;

        private string _selectedPortName;
        private int _selectedBaudRate;
        private string _selectedProductConnectMode;
        
        public List<string> PortNames
        {
            get { return _portNames; }
            set
            {
                if (_portNames!= value)
                {
                    _portNames = value;
                    OnPropertyChanged("PortNames");
                }
            }
        }

        public string SelectedPortName
        {
            get
            {
                Console.WriteLine("GetSelectedPortName: " + _selectedPortName);
                return _selectedPortName;
            }
            set
            {
                if (_selectedPortName!= value)
                {
                    Console.WriteLine("SetSelectedPortName: " + value);
                    _selectedPortName = value;
                    OnPropertyChanged("SelectedPortName");
                }
            }
        }
        
        public List<int> BaudRates
        {
            get { return _baudRates; }
            set
            {
                if (_baudRates!= value)
                {
                    _baudRates = value;
                    OnPropertyChanged("BaudRates");
                }
            }
        }

        public int SelectedBaudRate
        {
            get
            {
                // Console.WriteLine("GetSelectedBaudRate: " + _selectedBaudRate);    
                return _selectedBaudRate;
            }
            set
            {
                if (_selectedBaudRate!= value)
                {
                    // Console.WriteLine("SetSelectedBaudRate: " + value);
                    _selectedBaudRate = value;
                    OnPropertyChanged("SelectedBaudRate");
                }
            }
        }

        public List<string> ProductConnectMode
        {
            get { return _productConnectMode; }
            set
            {
                if (_productConnectMode!= value)
                {
                    _productConnectMode = value;
                    OnPropertyChanged("ProductConnectMode");
                }
            }
        }

        public string SelectedProductConnectMode
        {
            get
            {
                return _selectedProductConnectMode;
            }
            set
            {
                if (_selectedProductConnectMode != value)
                {
                    _selectedProductConnectMode = value;
                    OnPropertyChanged("SelectedProductConnectMode");
                }
            }
        }
        

        public string SendData
        {
            get { return _sendData; }
            set
            {
                if (_sendData!= value)
                {
                    _sendData = value;
                    OnPropertyChanged("SendData");
                }
            }
        }
        public EvbViewMode()
        {
            _evbSerialModel = new EvbSerialMode();
            
            PortNames = SerialPort.GetPortNames().ToList();
            if (PortNames.Count != 0)
            {
                _selectedPortName = PortNames[0];
            }
            BaudRates = new List<int>() { 9600, 19200, 38400, 57600, 115200 };
            _selectedBaudRate = BaudRates[0];
            ProductConnectMode = new List<string>() { "IIC", "OWI"};
            _selectedProductConnectMode = ProductConnectMode[0];

        }

        public void OpenPort()
        {
            _evbSerialModel.OpenPort(SelectedPortName, SelectedBaudRate);
        }

        public bool ConnectTest()
        {
            return _evbSerialModel.ConnectTest();
        }

        public bool ConnPd()
        {
            return _evbSerialModel.ConnPd(SelectedProductConnectMode);
        }

        public void ClosePort()
        {
            _evbSerialModel.ClosePort();
        }

        public void Send()
        {
            _evbSerialModel.SendData(SendData);
        }

        public string ReceivedData
        {
            get { return _evbSerialModel.ReceivedData; }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}