using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using Repository.Pattern.Ef6;
using VoucherWorld.Data.Enums;

namespace VoucherWorld.Data.Entities
{
    public class Route : Entity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool IsHidden { get; set; }

        public RouteCategory Category { get; set; }

        public Gift Gift { get; set; }

        public string PlaceIcon { get; set; }

        public OpenEndedQuestion OpenEndedQuestion { get; set; }
        
        public ICollection<RoutePlace> RoutePlaces { get; set; }

        public ICollection<Enrollment> Enrollments { get; set; }

        public int MerchantId { get; set; }
        public Merchant Merchant { get; set; }

        public Route()
        {
            RoutePlaces = new List<RoutePlace>();
            Enrollments = new List<Enrollment>();
        }
    }

    public class RouteModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsHidden { get; set; }
        public RouteCategory Category { get; set; }
        public GiftModel Gift { get; set; }
        public string PlaceIcon { get; set; }
        public QuestionModel Question { get; set; }
        public ICollection<PlaceModel> Places { get; set; } 
        public MerchantModel Merchant { get; set; }
    }
    
    public class SimpleRouteModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsHidden { get; set; }
        public RouteCategory Category { get; set; }
        public double? Distance { get; set; }
        public GiftModel Gift { get; set; }
        public string PlaceIcon { get; set; }
        public QuestionModel Question { get; set; }
        public PlaceModel Place { get; set; }
        public MerchantModel Merchant { get; set; }
    }
}
