using System;
using System.Runtime.Serialization;
using Repository.Pattern.Ef6;
using VoucherWorld.Data.Enums;

namespace VoucherWorld.Data.Entities
{
    public class RoutePlace : Entity
    {
        public int RouteId { get; set; }
        public Route Route { get; set; }

        public int PlaceId { get; set; }
        public Place Place { get; set; }
        
        public DateTime AddedTime { get; set; }

        public RoutePlace()
        {
            AddedTime = DateTime.Now;
        }
    }
}
