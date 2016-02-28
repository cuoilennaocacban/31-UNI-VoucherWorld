using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Repository.Pattern.Ef6;

namespace VoucherWorld.Data.Entities
{
    public class Gift : Entity
    {
        public int Id { get; set; }

        public string GiftName { get; set; }

        public int StockAmount { get; set; }

        public Route Route { get; set; }

        public ICollection<string> Images { get; set; }

        public Gift()
        {
            Images = new List<string>();
        }
    }

    public class GiftModel
    {
        public int Id { get; set; }
        public string GiftName { get; set; }
        public ICollection<string> Images { get; set; }

        public GiftModel()
        {
            Images = new List<string>();
        }
    }
}