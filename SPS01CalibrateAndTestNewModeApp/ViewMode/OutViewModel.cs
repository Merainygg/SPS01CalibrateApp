using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using SPS01CalibrateAndTestNewModeApp.Core;
using SPS01CalibrateAndTestNewModeApp.Enume;
using SPS01CalibrateAndTestNewModeApp.Mode;
using System.IO;

namespace SPS01CalibrateAndTestNewModeApp.ViewMode
{
    public class OutViewModel:INotifyPropertyChanged
    {
        private readonly EvbSerialModel _evbSerialModel;
        
        private List<string> _outputNames;
        private string _selectedOutputName;
        private readonly EvbCommandOfRaw _evbCommandOfRaw;
        
        private int _outputValue;
        private List<string> _allOutputValue;

        public List<string> OutputNames
        {
            get => _outputNames;
            set
            {
                if (_outputNames!= value)
                {
                    _outputNames = value;
                    OnPropertyChanged(nameof(OutputNames));
                }
            }
        }
        
        public string SelectedOutputName
        {
            get => _selectedOutputName;
            set
            {
                if (_selectedOutputName!= value)
                {
                    _selectedOutputName = value;
                    OnPropertyChanged(nameof(SelectedOutputName));
                }
            }
        }
        
        public int OutputValue
        {
            get => _outputValue;
            set
            {
                if (_outputValue!= value)
                {
                    _outputValue = value;
                    OnPropertyChanged(nameof(OutputValue));
                }
            }
        }

        public OutViewModel()
        {
            _evbSerialModel = ServiceContainer.Resolve<EvbSerialModel>();
            _evbCommandOfRaw = new EvbCommandOfRaw();
            // 获取字典的所有key值
            
            OutputNames = _evbCommandOfRaw.RawAddr.Keys.ToList();
            SelectedOutputName = OutputNames[0];
        }
        
        public List<string> AllOutputValue
        {
            get => _allOutputValue;
            set
            {
                if (_allOutputValue!= value)
                {
                    _allOutputValue = value;
                    OnPropertyChanged(nameof(AllOutputValue));
                }
            }
        }

        public void ButtonOutputClick()
        {
            if (SelectedOutputName.Contains("OFF") || SelectedOutputName.Contains("FG"))
            {
                OutputValue = _evbSerialModel.Get3ByteRawData(SelectedOutputName,0,1);
            }
            else
            {
                OutputValue = _evbSerialModel.Get2ByteRawData(SelectedOutputName,0,1);
            }
        }

        public void ButtonContinueOutputClick()
        {
            string filePath = DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss") + ".txt";
            for (int i = 0; i < 10; i++)
            {
                ButtonOutputClick();
                File.AppendAllText(filePath, OutputValue.ToString() + "\n");
            }
            
        }

        public void ButtonAllOutputClick()
        {
            AllOutputValue = new List<string>();
            var tempstr = SelectedOutputName;
            for (int i = 0; i < OutputNames.Count; i++)
            {
                SelectedOutputName = OutputNames[i];
                ButtonOutputClick();
                AllOutputValue.Add(SelectedOutputName + " : " + OutputValue.ToString());
            }
            SelectedOutputName = tempstr;
            Console.WriteLine(AllOutputValue);
        }
        
        
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}