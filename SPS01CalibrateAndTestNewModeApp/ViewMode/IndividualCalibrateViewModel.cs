using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO.Ports;
using System.Linq;
using SPS01CalibrateAndTestNewModeApp.Core;
using SPS01CalibrateAndTestNewModeApp.Mode;


namespace SPS01CalibrateAndTestNewModeApp.ViewMode
{
    public class IndividualCalibrateViewModel:INotifyPropertyChanged
    {
        private readonly EvbSerialModel _evbSerialModel;
        private readonly SpsCalibration _spsCalibration;
        
        private List<string> _rawDataMode;
        private string _selectedRawDataMode;
        private List<int> _rawDataJumpPoint;
        private int _selectedRawDataJumpPoint;
        private List<int> _rawDataAvgPoint;
        private int _selectedRawDataAvgPoint;
        private List<string> _rawCalibrateMode;
        private string _selectedRawCalibrateMode;
        private List<bool> _textboxVisibles;
        
        private bool _t0p1Visible;
        private bool _t0p2Visible;
        private bool _t0p3Visible;
        private bool _t0p4Visible;
        private bool _t1p1Visible;
        private bool _t1p2Visible;
        private bool _t2p1Visible;
        private bool _t2p2Visible;
        private bool _t3p1Visible;
        private bool _t3p2Visible;
        
        private bool _t0Visible;
        private bool _t1Visible;
        private bool _t2Visible;
        private bool _t3Visible;
        private bool _o1Visible;
        private bool _o2Visible;
        private bool _o3Visible;
        private bool _o4Visible;
        
        private bool _tt0Visible;
        private bool _tt1Visible;
        private bool _tt2Visible;
        private bool _tt3Visible;
        
        
        public List<string> RawDataMode
        {
            get { return _rawDataMode; }
            set
            {
                if (_rawDataMode!= value)
                {
                    _rawDataMode = value;
                    OnPropertyChanged("RawDataMode");
                }
            }
        }
        public string SelectedRawDataMode
        {
            get { return _selectedRawDataMode; }
            set
            {
                if (_selectedRawDataMode!= value)
                {
                    _selectedRawDataMode = value;
                    OnPropertyChanged("SelectedRawDataMode");
                }
            }
        }
        public List<int> RawDataJumpPoint
        {
            get { return _rawDataJumpPoint; }
            set
            {
                if (_rawDataJumpPoint!= value)
                {
                    _rawDataJumpPoint = value;
                    OnPropertyChanged("RawDataJumpPoint");
                }
            }
        }
        public int SelectedRawDataJumpPoint
        {
            get { return _selectedRawDataJumpPoint; }
            set
            {
                if (_selectedRawDataJumpPoint!= value)
                {
                    _selectedRawDataJumpPoint = value;
                    OnPropertyChanged("SelectedRawDataJumpPoint");
                }
            }
        }
        public List<int> RawDataAvgPoint
        {
            get { return _rawDataAvgPoint; }
            set
            {
                if (_rawDataAvgPoint!= value)
                {
                    _rawDataAvgPoint = value;
                    OnPropertyChanged("RawDataAvgPoint");
                }
            }
        }
        public int SelectedRawDataAvgPoint
        {
            get { return _selectedRawDataAvgPoint; }
            set
            {
                if (_selectedRawDataAvgPoint!= value)
                {
                    _selectedRawDataAvgPoint = value;
                    OnPropertyChanged("SelectedRawDataAvgPoint");
                }
            }
        }
        public List<string> RawCalibrateMode
        {
            get { return _rawCalibrateMode; }
            set
            {
                if (_rawCalibrateMode!= value)
                {
                    _rawCalibrateMode = value;
                    OnPropertyChanged("RawCalibrateMode");
                }
            }
        }
        public string SelectedRawCalibrateMode
        {
            get { return _selectedRawCalibrateMode; }
            set
            {
                if (_selectedRawCalibrateMode!= value)
                {
                    _selectedRawCalibrateMode = value;
                    UpdateTextboxVisibilities();
                    OnPropertyChanged("SelectedRawCalibrateMode");
                }
            }
        }

        public bool T0p1Visible
        {
            get { return _t0p1Visible; }
            set
            {
                if (_t0p1Visible!= value)
                {
                    _t0p1Visible = value;
                    OnPropertyChanged("T0P1Visible");
                }
            }
        }
        public bool T0p2Visible
        {
            get { return _t0p2Visible; }
            set
            {
                if (_t0p2Visible!= value)
                {
                    _t0p2Visible = value;
                    OnPropertyChanged("T0P2Visible");
                }
            }
        }
        public bool T0p3Visible
        {
            get { return _t0p3Visible; }
            set
            {
                if (_t0p3Visible!= value)
                {
                    _t0p3Visible = value;
                    OnPropertyChanged("T0P3Visible");
                }
            }
        }
        public bool T0p4Visible
        {
            get { return _t0p4Visible; }
            set
            {
                if (_t0p4Visible!= value)
                {
                    _t0p4Visible = value;
                    OnPropertyChanged("T0P4Visible");
                }
            }
        }
        public bool T1p1Visible
        {
            get { return _t1p1Visible; }
            set
            {
                if (_t1p1Visible!= value)
                {
                    _t1p1Visible = value;
                    OnPropertyChanged("T1P1Visible");
                }
            }
        }
        public bool T1p2Visible
        {
            get { return _t1p2Visible; }
            set
            {
                if (_t1p2Visible!= value)
                {
                    _t1p2Visible = value;
                    OnPropertyChanged("T1P2Visible");
                }
            }
        }
        public bool T2p1Visible
        {
            get { return _t2p1Visible; }
            set
            {
                if (_t2p1Visible!= value)
                {
                    _t2p1Visible = value;
                    OnPropertyChanged("T2P1Visible");
                }
            }
        }
        public bool T2p2Visible
        {
            get { return _t2p2Visible; }
            set
            {
                if (_t2p2Visible!= value)
                {
                    _t2p2Visible = value;
                    OnPropertyChanged("T2P2Visible");
                }
            }
        }
        public bool T3p1Visible
        {
            get { return _t3p1Visible; }
            set
            {
                if (_t3p1Visible!= value)
                {
                    _t3p1Visible = value;
                    OnPropertyChanged("T3P1Visible");
                }
            }
        }
        public bool T3p2Visible
        {
            get { return _t3p2Visible; }
            set
            {
                if (_t3p2Visible!= value)
                {
                    _t3p2Visible = value;
                    OnPropertyChanged("T3P2Visible");
                }
            }
        }
        public bool T0Visible
        {
            get { return _t0Visible; }
            set
            {
                if (_t0Visible!= value)
                {
                    _t0Visible = value;
                    OnPropertyChanged("T0Visible");
                }
            }
        }
        public bool T1Visible
        {
            get { return _t1Visible; }
            set
            {
                if (_t1Visible!= value) 
                {
                    _t1Visible = value;
                    OnPropertyChanged("T1Visible");
                }
            }
        }
        public bool T2Visible
        {
            get { return _t2Visible; }
            set
            {
                if (_t2Visible!= value)
                {
                    _t2Visible = value;
                    OnPropertyChanged("T2Visible");
                }
            }
        }
        public bool T3Visible
        {
            get { return _t3Visible; }
            set
            {
                if (_t3Visible!= value)
                {
                    _t3Visible = value;
                    OnPropertyChanged("T3Visible");
                }
            }
        }
        public bool O1Visible
        {
            get { return _o1Visible; }
            set
            {
                if (_o1Visible!= value)
                {
                    _o1Visible = value;
                    OnPropertyChanged("O1Visible");
                }
            }
        }
        public bool O2Visible
        {
            get { return _o2Visible; }
            set
            {
                if (_o2Visible!= value)
                {
                    _o2Visible = value;
                    OnPropertyChanged("O2Visible");
                }
            }
        }
        public bool O3Visible
        {
            get { return _o3Visible; }
            set
            {
                if (_o3Visible!= value)
                {
                    _o3Visible = value;
                    OnPropertyChanged("O3Visible");
                }
            }
        }
        public bool O4Visible
        {
            get { return _o4Visible; }
            set
            {
                if (_o4Visible!= value)
                {
                    _o4Visible = value;
                    OnPropertyChanged("O4Visible");
                }
            }
        }
        
        public bool TT0Visible
        {
            get { return _tt0Visible; }
            set
            {
                if (_tt0Visible!= value)
                {
                    _tt0Visible = value;
                    OnPropertyChanged("TT0Visible");
                }
            }
        }
        public bool TT1Visible
        {
            get { return _tt1Visible; }
            set
            {
                if (_tt1Visible!= value)
                {
                    _tt1Visible = value;
                    OnPropertyChanged("TT1Visible");
                }
            }
        }
        
        public bool TT2Visible
        {
            get { return _tt2Visible; }
            set
            {
                if (_tt2Visible!= value)
                {
                    _tt2Visible = value;
                    OnPropertyChanged("TT2Visible");
                }
            }
        }
        
        public bool TT3Visible
        {
            get { return _tt3Visible; }
            set
            {
                if (_tt3Visible!= value)
                {
                    _tt3Visible = value;
                    OnPropertyChanged("TT3Visible");
                }
            }
        }
        
        public IndividualCalibrateViewModel()
        {
            _evbSerialModel = ServiceContainer.Resolve<EvbSerialModel>();
            _spsCalibration = ServiceContainer.Resolve<SpsCalibration>();
            
            RawDataMode = new List<string>(){"全桥","半桥","全桥+半桥"};
            SelectedRawDataMode = RawDataMode[0];
            RawDataJumpPoint = new List<int>(){0, 1, 2};
            SelectedRawDataJumpPoint = RawDataJumpPoint[0];
            RawDataAvgPoint = new List<int>(){1, 2, 3, 4, 5, 6, 7, 8, 9, 10};
            SelectedRawDataAvgPoint = RawDataAvgPoint[0];
            RawCalibrateMode = new List<string>() { "1T2P", "1T3P", "1T4P", "2T2P", "2T3P", "2T4P", "3T2P", "3T3P", "3T4P", "4T2P", "4T3P", "4T4P" };
            SelectedRawCalibrateMode = RawCalibrateMode[0];
            // TextboxVisibilities = new List<bool>() {false, false, false, false, false, false, false, false, false, false};
            // _textboxVisibles = new List<bool>(){false, false, false, false, false, false, false, false, false, false};
            UpdateTextboxVisibilities();
        }
        
        public void UpdateTextboxVisibilities()
        {
           T0p1Visible = true;
           T0p2Visible = true;
           T0p3Visible = false;
           T0p4Visible = false;
           T1p1Visible = false;
           T1p2Visible = false;
           T2p1Visible = false;
           T2p2Visible = false;
           T3p1Visible = false;
           T3p2Visible = false;
           
           T0Visible = true;
           T1Visible = false;
           T2Visible = false;
           T3Visible = false;

           O1Visible = true;
           O2Visible = true;
           O3Visible = false;
           O4Visible = false;
           
           TT0Visible = true;
           TT1Visible = false;
           TT2Visible = false;
           TT3Visible = false;
           
           var Tpoint = SelectedRawCalibrateMode.Substring(0, 1);
           var Ppoint = SelectedRawCalibrateMode.Substring(2, 1);
           switch (Ppoint)
           {
               case "3":
                   T0p3Visible = true;
                   O3Visible = true;
                   break;
               case "4":
                   T0p3Visible = true;
                   T0p4Visible = true;
                   O3Visible = true;
                   O4Visible = true;
                   break;
           }

           switch (Tpoint)
           {
               case "2":
                   T1p1Visible = true;
                   T1p2Visible = true;
                   T1Visible = true;
                   TT1Visible = true;
                   break;
               case "3":
                   T1p1Visible = true;
                   T1p2Visible = true;
                   T2p1Visible = true;
                   T2p2Visible = true;
                   T1Visible = true;
                   T2Visible = true;
                   TT1Visible = true;
                   TT2Visible = true;
                   break;
               case "4":
                   T1p1Visible = true;
                   T1p2Visible = true;
                   T2p1Visible = true;
                   T2p2Visible = true;
                   T3p1Visible = true;
                   T3p2Visible = true;
                   T1Visible = true;
                   T2Visible = true;
                   T3Visible = true;
                   
                   TT1Visible = true;
                   TT2Visible = true;
                   TT3Visible = true;
                   break;
               default:
                   break;
           }
        }

        private int GetRawData(string name, Func<int, int, int> getDataFunc)
        {
            var rawData = getDataFunc(SelectedRawDataJumpPoint, SelectedRawDataAvgPoint);
            // Console.WriteLine($"rawData: {rawData}");
            return rawData;
        }

        public int GetP1Data(string name)
        {
            var rawData = GetRawData(name, _evbSerialModel.GetP1Data);
            _spsCalibration.FullBridgeRawData[name] = rawData;
            return rawData;
        }

        public int GetP2Data(string name)
        {
            var rawData = GetRawData(name, _evbSerialModel.GetP2Data);
            _spsCalibration.HalfBridgeRawData[name] = rawData;
            return rawData;
            
        }

        public int GetTSIData(string name)
        {
            var rawData = GetRawData(name, _evbSerialModel.GetTSIData);
            _spsCalibration.TsiTempRaw[name] = rawData;
            return rawData;
        }

        public int GetTSEData(string name)
        {
            var rawData = GetRawData(name, _evbSerialModel.GetTSEData);
            _spsCalibration.TseTempRaw[name] = rawData;
            return rawData;
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