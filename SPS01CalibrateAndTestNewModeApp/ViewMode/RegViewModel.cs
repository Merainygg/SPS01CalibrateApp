using System.ComponentModel;

namespace SPS01CalibrateAndTestNewModeApp.ViewMode
{
    public class RegViewModel:INotifyPropertyChanged
    {
        private string _buttonPageAdd;
        private string _buttonPageSub;
        public string ButtonPageAdd
        {
            get => _buttonPageAdd;
            set
            {
                _buttonPageAdd = value;
                OnPropertyChanged(nameof(ButtonPageAdd));
            }
        }
        
        public string ButtonPageSub
        {
            get => _buttonPageSub;
            set
            {
                _buttonPageSub = value;
                OnPropertyChanged(nameof(ButtonPageSub));
            }
        }
        
        
        
        
        public event PropertyChangedEventHandler PropertyChanged;

        public virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        
    }
}