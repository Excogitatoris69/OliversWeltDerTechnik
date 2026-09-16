using OtpMainGui.common;
using System.ComponentModel;

namespace OtpMainGui.viewmodels
{
    public class BasicVM : INotifyPropertyChanged
    {
        public string windowTitle { get; set; }
        public double windowLeft { get; set; }
        public double windowTop { get; set; }
        public double windowWidth { get; set; }
        public double windowHeight { get; set; }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

    }



}
