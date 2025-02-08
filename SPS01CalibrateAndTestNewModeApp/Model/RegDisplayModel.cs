using System.ComponentModel;

namespace SPS01CalibrateAndTestNewModeApp.Mode
{
    public class RegDisplayModel:INotifyPropertyChanged
    {
        private int currentPage;

        public int CurrentPage
        {
            get => currentPage;
            set
            {
                currentPage = value;
                OnPropertyChanged(nameof(CurrentPage));
            }
        }

        public void ButtonPageAdd()
        {
            CurrentPage++;
        }

        public void ButtonPageSub()
        {
            if (CurrentPage > 0)
            {
                CurrentPage--;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
