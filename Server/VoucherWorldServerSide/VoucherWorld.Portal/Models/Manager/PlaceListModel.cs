using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VoucherWorld.Data.Entities;

namespace VoucherWorld.Portal.Models.Manager
{
    public class MvcPlaceListModel
    {
        public Merchant Merchant { get; set; }
        public IEnumerable<Place> PlaceList { get; set; }

        public MvcPlaceListModel()
        {
            PlaceList = new List<Place>();
        }
    }
}