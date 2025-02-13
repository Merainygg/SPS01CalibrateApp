using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using SPS01CalibrateAndTestNewModeApp.Core;
using SPS01CalibrateAndTestNewModeApp.Mode;

namespace SPS01CalibrateAndTestNewModeApp.ViewMode
{
    public class RegViewModel:INotifyPropertyChanged
    {
        private ObservableCollection<string[]> _dataGridData;
        
        private readonly SpsCalibration _spsCalibration;
        private readonly EvbSerialModel _evbSerialModel;
        private int _currentPage;
        private List<string> _regDisplayName;
        
        private string _selectedRegDisplayName;
        
        public string CurrentPage
        {
            get => _currentPage.ToString();
            set
            {
                // Console.WriteLine("SetCurrentPage: " + value);
                if (_currentPage.ToString()!= value)
                {
                    _currentPage = int.Parse(value);
                    OnPropertyChanged(nameof(CurrentPage));
                }
            }
        }
        
        public List<string> RegDisplayName
        {
            get => _regDisplayName;
            set
            {
                Console.WriteLine("RegDisplayName: " + value);
                if (_regDisplayName!= value)
                {
                    _regDisplayName = value;
                    OnPropertyChanged(nameof(RegDisplayName));
                }
            }
        }
        
        public string SelectedRegDisplayName
        {
            get => _selectedRegDisplayName;
            set
            {
                if (_selectedRegDisplayName!= value)
                {
                    _selectedRegDisplayName = value;
                    OnPropertyChanged(nameof(SelectedRegDisplayName));
                }
            }
        }
        

        public ObservableCollection<string[]> DataGridData
        {
            get { return _dataGridData; }
            set
            {
                if (_dataGridData != value)
                {
                    _dataGridData = value;
                    OnPropertyChanged(nameof(DataGridData));
                }
            }
        }
        
        public RegViewModel()
        {
            _spsCalibration = ServiceContainer.Resolve<SpsCalibration>();
            _evbSerialModel = ServiceContainer.Resolve<EvbSerialModel>();
            CurrentPage = "3";
            RegDisplayName = new List<string>(){"Reg", "Nvm"};
            SelectedRegDisplayName = RegDisplayName[0];
        }

        public void ButtonPageAddClick()
        {
            // Console.WriteLine("ButtonPageAddClick: " + _currentPage);
            if (_currentPage < 4)
            {
                _currentPage++;
            }
            CurrentPage = _currentPage.ToString();
            
        }
        public void ButtonPageSubClick()
        {
            // Console.WriteLine("ButtonPageSubClick: " + _currentPage);
            if (_currentPage > 1)
            {
                _currentPage--;
            }
            CurrentPage = _currentPage.ToString();
            
        }

        public void ButtonGetAllRegClick()
        {
            var reg = _evbSerialModel.GetAllReg();
            // _evbSerialModel.GetAllReg();
            if (reg.Length < 512) return;
            for (var i = 0; i < 256; i++) 
                _spsCalibration.RegData[i] = Convert.ToByte(reg.Substring(i * 2, 2), 16);
        }

        public void ButtonGetAllNvmClick()
        {
            var nvm = _evbSerialModel.GetAllNvm();
            // _evbSerialModel.GetAllNvm();
            if (nvm.Length < 512) return;
            for (var i = 0; i < 256; i++)
                _spsCalibration.NvmData[i] = Convert.ToByte(nvm.Substring(i * 2, 2), 16);
        }
        
        public event PropertyChangedEventHandler PropertyChanged;

        public virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        
    }
}