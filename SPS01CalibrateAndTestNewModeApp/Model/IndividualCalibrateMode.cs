using System;
using System.ComponentModel;
using System.IO.Ports;
using System.Linq;
using System.Windows.Forms;
using SPS01CalibrateAndTestNewModeApp.Enume;


namespace SPS01CalibrateAndTestNewModeApp.Mode
{
    public class IndividualCalibrateMode:INotifyPropertyChanged
    {
        

        public IndividualCalibrateMode()
        {
            
        }
        
        
        
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}