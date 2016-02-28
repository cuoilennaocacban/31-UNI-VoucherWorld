using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Device.Location;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using GART.Data;
using TestGART.Annotations;

namespace TestGART.Model
{
    public class CityPlace : ARItem
    {
        private string description;

        public string Description
        {
            get
            {
                return description;
            }
            set
            {
                if (description != value)
                {
                    description = value;
                    NotifyPropertyChanged(() => Description);
                }
            }
        }

        public Diamond diamond;

        public Diamond Diamond
        {
            get { return diamond; }
            set
            {
                if (diamond != value)
                {
                    diamond = value;
                    NotifyPropertyChanged(() => Diamond);
                }
            }
        }
    }

    public class Diamond : INotifyPropertyChanged
    {
        private int _no;
        private GeoCoordinate _point;

        public int No
        {
            get { return _no; }
            set
            {
                if (value == _no) return;
                _no = value;
                OnPropertyChanged();
            }
        }

        public GeoCoordinate Point
        {
            get { return _point; }
            set
            {
                if (Equals(value, _point)) return;
                _point = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class Place : INotifyPropertyChanged
    {
        private string _id;
        private string _add;
        private string _name;
        private ObservableCollection<Diamond> _diamonds;

        public string Id
        {
            get { return _id; }
            set
            {
                if (value == _id) return;
                _id = value;
                OnPropertyChanged();
            }
        }

        public string Add
        {
            get { return _add; }
            set
            {
                if (value == _add) return;
                _add = value;
                OnPropertyChanged();
            }
        }

        public string Name
        {
            get { return _name; }
            set
            {
                if (value == _name) return;
                _name = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Diamond> Diamonds
        {
            get { return _diamonds; }
            set
            {
                if (Equals(value, _diamonds)) return;
                _diamonds = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }

    public class Cat : INotifyPropertyChanged
    {
        private string _name;
        private string _iconPath;
        private ObservableCollection<Place> _places;

        public string Name
        {
            get { return _name; }
            set
            {
                if (value == _name) return;
                _name = value;
                OnPropertyChanged();
            }
        }

        public string IconPath
        {
            get { return _iconPath; }
            set
            {
                if (value == _iconPath) return;
                _iconPath = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Place> Places
        {
            get { return _places; }
            set
            {
                if (Equals(value, _places)) return;
                _places = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
