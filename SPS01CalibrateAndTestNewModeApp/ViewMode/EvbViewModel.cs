using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO.Ports;
using System.Linq;
using SPS01CalibrateAndTestNewModeApp.Core;
using SPS01CalibrateAndTestNewModeApp.Mode;

namespace SPS01CalibrateAndTestNewModeApp.ViewMode
{
    public class EvbViewModel:INotifyPropertyChanged 
    {
        private readonly EvbSerialModel _evbSerialModel;
        private BindingList<string> _portNames;
        private List<int> _baudRates;
        private List<string> _productConnMode;

        private string _sendData;

        private string _selectedPortName;
        private int _selectedBaudRate;
        private string _selectedConnMode;
        
        public BindingList<string> PortNames
        {
            get { return _portNames; }
            set
            {
                Console.WriteLine("SetPortNames: " + value);
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

        public List<string> ProductConnMode
        {
            get { return _productConnMode; }
            set
            {
                if (_productConnMode!= value)
                {
                   
                    _productConnMode = value;
                    OnPropertyChanged("ProductConnMode");
                }
            }
        }

        public string SelectedConnMode
        {
            get
            {
                return _selectedConnMode;
            }
            set
            {
                
                _evbSerialModel.ConnModeName = value;
                _selectedConnMode = value;
                OnPropertyChanged("SelectedConnMode");
                
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
        public EvbViewModel()
        {
            _evbSerialModel =ServiceContainer.Resolve<EvbSerialModel>();
            
            PortNames = new BindingList<string>();
            // PortNames.Add("刷新");
            RefreshPortNames();
            if (PortNames.Count != 0)
            {
                SelectedPortName = PortNames[0];
            }
            BaudRates = new List<int>() { 9600, 19200, 38400, 57600, 115200 };
            SelectedBaudRate = BaudRates[0];
            ProductConnMode = new List<string>() { "IIC", "OWI"};
            SelectedConnMode = ProductConnMode[0];

        }

        public void RefreshPortNames()
        {
            var newPorts = SerialPort.GetPortNames().ToList();
            newPorts.Add("刷新");
            
            // 更新集合
            PortNames.Clear();
            foreach (var port in newPorts)
            {
                PortNames.Add(port);
            }
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
            return _evbSerialModel.ConnPd();
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