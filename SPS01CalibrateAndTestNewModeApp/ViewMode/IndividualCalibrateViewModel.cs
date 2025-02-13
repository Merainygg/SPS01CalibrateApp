using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO.Ports;
using System.Linq;
using MySql.Data.MySqlClient;
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

        private bool _t0P1Visible;
        private bool _t0P2Visible;
        private bool _t0P3Visible;
        private bool _t0P4Visible;
        private bool _t1P1Visible;
        private bool _t1P2Visible;
        private bool _t2P1Visible;
        private bool _t2P2Visible;
        private bool _t3P1Visible;
        private bool _t3P2Visible;
        
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

        private string _id;
        
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

        public bool T0P1Visible
        {
            get { return _t0P1Visible; }
            set
            {
                if (_t0P1Visible!= value)
                {
                    _t0P1Visible = value;
                    OnPropertyChanged("T0P1Visible");
                }
            }
        }
        public bool T0P2Visible
        {
            get { return _t0P2Visible; }
            set
            {
                if (_t0P2Visible!= value)
                {
                    _t0P2Visible = value;
                    OnPropertyChanged("T0P2Visible");
                }
            }
        }
        public bool T0P3Visible
        {
            get { return _t0P3Visible; }
            set
            {
                if (_t0P3Visible!= value)
                {
                    _t0P3Visible = value;
                    OnPropertyChanged("T0P3Visible");
                }
            }
        }
        public bool T0P4Visible
        {
            get { return _t0P4Visible; }
            set
            {
                if (_t0P4Visible!= value)
                {
                    _t0P4Visible = value;
                    OnPropertyChanged("T0P4Visible");
                }
            }
        }
        public bool T1P1Visible
        {
            get { return _t1P1Visible; }
            set
            {
                if (_t1P1Visible!= value)
                {
                    _t1P1Visible = value;
                    OnPropertyChanged("T1P1Visible");
                }
            }
        }
        public bool T1P2Visible
        {
            get { return _t1P2Visible; }
            set
            {
                if (_t1P2Visible!= value)
                {
                    _t1P2Visible = value;
                    OnPropertyChanged("T1P2Visible");
                }
            }
        }
        public bool T2P1Visible
        {
            get { return _t2P1Visible; }
            set
            {
                if (_t2P1Visible!= value)
                {
                    _t2P1Visible = value;
                    OnPropertyChanged("T2P1Visible");
                }
            }
        }
        public bool T2P2Visible
        {
            get { return _t2P2Visible; }
            set
            {
                if (_t2P2Visible!= value)
                {
                    _t2P2Visible = value;
                    OnPropertyChanged("T2P2Visible");
                }
            }
        }
        public bool T3P1Visible
        {
            get { return _t3P1Visible; }
            set
            {
                if (_t3P1Visible!= value)
                {
                    _t3P1Visible = value;
                    OnPropertyChanged("T3P1Visible");
                }
            }
        }
        public bool T3P2Visible
        {
            get { return _t3P2Visible; }
            set
            {
                if (_t3P2Visible!= value)
                {
                    _t3P2Visible = value;
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
        public bool Tt0Visible
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
        public bool Tt1Visible
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
        public bool Tt2Visible
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
        public bool Tt3Visible
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
        
        public string Id
        {
            get { return _id; }
            set
            {
                if (_id!= value)
                {
                    _id = value;
                    OnPropertyChanged("ID");
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
            UpdateTextboxVisibilities();
        }
        
        public void UpdateTextboxVisibilities()
        {
           T0P1Visible = true;
           T0P2Visible = true;
           T0P3Visible = false;
           T0P4Visible = false;
           T1P1Visible = false;
           T1P2Visible = false;
           T2P1Visible = false;
           T2P2Visible = false;
           T3P1Visible = false;
           T3P2Visible = false;
           
           T0Visible = true;
           T1Visible = false;
           T2Visible = false;
           T3Visible = false;

           O1Visible = true;
           O2Visible = true;
           O3Visible = false;
           O4Visible = false;
           
           Tt0Visible = true;
           Tt1Visible = false;
           Tt2Visible = false;
           Tt3Visible = false;
           
           var tpoint = SelectedRawCalibrateMode.Substring(0, 1);
           var ppoint = SelectedRawCalibrateMode.Substring(2, 1);
           switch (ppoint)
           {
               case "3":
                   T0P3Visible = true;
                   O3Visible = true;
                   break;
               case "4":
                   T0P3Visible = true;
                   T0P4Visible = true;
                   O3Visible = true;
                   O4Visible = true;
                   break;
           }

           switch (tpoint)
           {
               case "2":
                   T1P1Visible = true;
                   T1P2Visible = true;
                   T1Visible = true;
                   Tt1Visible = true;
                   break;
               case "3":
                   T1P1Visible = true;
                   T1P2Visible = true;
                   T2P1Visible = true;
                   T2P2Visible = true;
                   T1Visible = true;
                   T2Visible = true;
                   Tt1Visible = true;
                   Tt2Visible = true;
                   break;
               case "4":
                   T1P1Visible = true;
                   T1P2Visible = true;
                   T2P1Visible = true;
                   T2P2Visible = true;
                   T3P1Visible = true;
                   T3P2Visible = true;
                   T1Visible = true;
                   T2Visible = true;
                   T3Visible = true;
                   
                   Tt1Visible = true;
                   Tt2Visible = true;
                   Tt3Visible = true;
                   break;
           }
           
           _spsCalibration.CalibraMode = SelectedRawCalibrateMode;
        }

        private int GetRawData(Func<int, int, int> getDataFunc)
        {
            var rawData = getDataFunc(SelectedRawDataJumpPoint, SelectedRawDataAvgPoint);
            // Console.WriteLine($"rawData: {rawData}");
            return rawData;
        }

        public int GetP1Data(string name)
        {
            var rawData = GetRawData(_evbSerialModel.GetP1Data);
            _spsCalibration.FullBridgeRawData[name] = rawData;
            return rawData;
        }

        public int GetP2Data(string name)
        {
            var rawData = GetRawData(_evbSerialModel.GetP2Data);
            _spsCalibration.HalfBridgeRawData[name] = rawData;
            return rawData;
            
        }

        public int GetTsiData(string name)
        {
            var rawData = GetRawData(_evbSerialModel.GetTSIData);
            _spsCalibration.TsiTempRaw[name] = rawData;
            return rawData;
        }

        public int GetTseData(string name)
        {
            var rawData = GetRawData(_evbSerialModel.GetTSEData);
            _spsCalibration.TseTempRaw[name] = rawData;
            return rawData;
        }

        public void GetId()
        {
            var id = _evbSerialModel.GetId();
            if (id == "00000000")
            {
                // 查询数据库ID的总数，并将其加1作为新的ID
                var connectionStr = "Server=172.20.100.20;Database=product_base_sps01;Uid=Link_pb;Pwd=link*1234;";
                using (var connection = new MySqlConnection(connectionStr))
                {
                    connection.Open();
                    var cmd = new MySqlCommand("SELECT COUNT(*) FROM spsdata", connection);
                    var count = Convert.ToInt32(cmd.ExecuteScalar());
                    // 转换为8位 的十六进制字符串
                    var newId = (count + 1).ToString("X8");
                    // 将新的ID写入数据库
                    cmd = new MySqlCommand("INSERT INTO spsdata (id) VALUES (@id)", connection);
                    cmd.Parameters.AddWithValue("@id", newId);
                    cmd.ExecuteNonQuery();
                    _evbSerialModel.SetId(newId);
                }
                // 写入产品Id
                id = _evbSerialModel.GetId();
            }
            _spsCalibration.ID = id;
            Id = id;
            _spsCalibration.InsertDataBase();
        }

        public void UpdatePressTarget(List<double> targetList)
        {
            var index = 0;
            foreach (var key in _spsCalibration.FullBridgeTarget.Keys)
            {
                _spsCalibration.FullBridgeTarget[key] = targetList[index];
                index++;
            }
            _spsCalibration.UpdateDataBase();
        }

        public void UpdateTempTarget(List<double> targetList)
        {
            var index = 0;
            foreach (var key in _spsCalibration.TsiTempTarget.Keys)
            {
                _spsCalibration.TsiTempTarget[key] = targetList[index];
                index++;
            }
            _spsCalibration.UpdateDataBase();
        }

        public void Calibrate()
        {
            
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