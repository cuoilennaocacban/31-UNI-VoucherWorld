using System.Collections.Generic;
using System.Runtime.Serialization;
using Repository.Pattern.Ef6;

namespace VoucherWorld.Data.Entities
{
    public class Place : Entity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public double Longitude { get; set; }

        public double Latitude { get; set; }

        public double Altitude { get; set; }

        public ICollection<RoutePlace> RoutePlaces { get; set; } 

        public ScalingQuestion ScalingQuestion { get; set; }
        
        public int MerchantId { get; set; }
        public Merchant Merchant { get; set; }

        public Place()
        {
            RoutePlaces = new List<RoutePlace>();
            Altitude = 0;
        }
    }

    public class PlaceModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public int BonusPoint { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public double Altitude { get; set; }
        public QuestionModel Question { get; set; }
    }
}