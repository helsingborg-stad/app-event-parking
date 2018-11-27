using System;
using System.ComponentModel;

namespace EventParkering.Model
{
    public class EventItem //: INotifyPropertyChanged
    {
        public int id { get; set; }
        public string name { get; set; }
        public string streetAddress { get; set; }
        public string lat { get; set; }
        public string lon { get; set; }
        /*string _name;
        public string name 
        {
            get { return _name; }
            set
            {
                if (_name != value)
                    _name = value;
                OnPropertyChanged("name");
            }
        }

        string _streetAddress;
        public string streetAddress 
        {
            get { return _streetAddress; }
            set
            {
                if (_streetAddress != value)
                    _streetAddress = value;
                OnPropertyChanged("streetAddress");
            }
        }
        string _lat;
        public string lat 
        {
            get { return _lat; }
            set
            {
                if (_lat != value)
                    _lat = value;
                OnPropertyChanged("lat");
            }
        }
        string _lon;
        public string lon 
        {
            get { return _lon; }
            set
            {
                if (_lon != value)
                    _lon = value;
                OnPropertyChanged("lon");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }*/
    }
}
