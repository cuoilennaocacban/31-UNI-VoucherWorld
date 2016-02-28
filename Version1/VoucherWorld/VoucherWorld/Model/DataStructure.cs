using System.Windows.Automation;
using GART.Data;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using VoucherWorld.Annotations;

namespace VoucherWorld.Model
{
    public class Category : INotifyPropertyChanged
    {

        private string _name;
        private string _path;

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

        public string Path
        {
            get { return _path; }
            set
            {
                if (value == _path) return;
                _path = value;
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

    public class OrientationAnimation
    {
        public string value { get; set; }
        public string id { get; set; }
    }

    public class TodoItem
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public bool Complete { get; set; }
    }

    public class Route
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsHidden { get; set; }
        public int Category { get; set; }
        public double Distance { get; set; }
        public Gift Gift { get; set; }
        public string PlaceIcon { get; set; }
        public Question Question { get; set; }
        public Place Place { get; set; }
        public Merchant Merchant { get; set; }
    }

    public class Merchant
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Website { get; set; }
    }

    public class Gift
    {
        public int Id { get; set; }
        public string GiftName { get; set; }
        public List<object> Images { get; set; }
    }

    public class Place : ARItem
    {
        public Place(PlaceBasic p)
        {
            Id = p.Id;
            Address = p.Address;
            BonusPoint = p.BonusPoint;
            Longitude = p.Longitude;
            Latitude = p.Latitude;
            Altitude = p.Altitude;
            Question = p.Question;
        }

        public Place()
        {}

        public static explicit operator Place(PlaceBasic p)
        {
            Place a = new Place(p);
            return a;
        }

        public int Id { get; set; }
        public string Address { get; set; }
        public int BonusPoint { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public double Altitude { get; set; }
        public object Question { get; set; }
    }

    public class PlaceBasic
    {
        public int Id { get; set; }
        public string Address { get; set; }
        public int BonusPoint { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }

        public double Altitude { get; set; }

        public Question Question { get; set; }
    }

    public class Question
    {
        public int Id { get; set; }
        public string Content { get; set; }
    }

    public class RouteDetailsModel
    {
        //public RouteDetailsModel(Enrrollments e)
        //{
        //    Id = e.Id;
        //    Name = e.Name;
        //    Category = e.Category;
        //    Gift = e.Gift;
        //    PlaceIcon = e.PlaceIcon;
        //    Question = e.Question;
        //    Places = e.Places;
        //    Merchant = e.Merchant;
        //}

        public static implicit operator RouteDetailsModel(RouteDetailsModelBasic e)
        {
            RouteDetailsModel a = new RouteDetailsModel();

            a.Id = e.Id;
            a.Name = e.Name;
            a.Category = e.Category;
            a.Gift = e.Gift;
            a.PlaceIcon = e.PlaceIcon;
            a.Question = e.Question;
            a.Places = new List<Place>();

            foreach (PlaceBasic placeBasic in e.Places)
            {
                Place temp = new Place(placeBasic);
                a.Places.Add(temp);
            }

            a.Merchant = e.Merchant;

            return a;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsHidden { get; set; }
        public int Category { get; set; }
        public Gift Gift { get; set; }
        public string PlaceIcon { get; set; }
        public Question Question { get; set; }
        public List<Place> Places { get; set; }
        public Merchant Merchant { get; set; }
    }

    public class RouteDetailsModelBasic
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsHidden { get; set; }
        public int Category { get; set; }
        public Gift Gift { get; set; }
        public string PlaceIcon { get; set; }
        public Question Question { get; set; }
        public List<PlaceBasic> Places { get; set; }
        public Merchant Merchant { get; set; }
    }

    public class Enrrollments : RouteDetailsModelBasic
    {
        //public Errollments(RouteDetailsModel baseData)
        //{
        //    Id = baseData.Id;
        //    Name = baseData.Name;
        //    IsHidden = baseData.IsHidden;
        //    Category = baseData.Category;
        //}

        /// <summary>
        /// Gift Code, get from Server
        /// </summary>
        public string GiftCode { get; set; }

        /// <summary>
        /// Number of Completed Places
        /// 3 is max
        /// </summary>
        public int CompletedPlace { get; set; }
    }

    public class User
    {
        public bool IsFacebookUser { get; set; }
        public int Point { get; set; }
        public List<object> Enrollments { get; set; }
        public List<object> Answers { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int UserType { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string RegistrationDate { get; set; }
        public int ObjectState { get; set; }
    }

    public class FacebookUser
    {
        public string id { get; set; }
        public string name { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string link { get; set; }
        public Hometown hometown { get; set; }
        public string bio { get; set; }
        public string gender { get; set; }
        public string email { get; set; }
        public int timezone { get; set; }
        public string locale { get; set; }
        public bool verified { get; set; }
        public string updated_time { get; set; }
        public string username { get; set; }
    }

    public class Hometown
    {
        public string id { get; set; }
        public string name { get; set; }
    }
}

namespace VoucherWorld.Model.AppLiveInfoModel
{
    public class AppLiveInfo
    {
        public string name { get; set; }
        public string ver { get; set; }
        public string wp8link { get; set; }
        public string wp7link { get; set; }
        public string w8link { get; set; }
        public string w81link { get; set; }
        public string admode { get; set; }
        public List<string> message { get; set; }
    }

    public class AppLiveInfoRootObject
    {
        public AppLiveInfo appLiveInfo { get; set; }
    }

}