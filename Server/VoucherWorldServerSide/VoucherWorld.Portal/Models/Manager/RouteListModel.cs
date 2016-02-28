using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VoucherWorld.Data.Entities;

namespace VoucherWorld.Portal.Models.Manager
{
    public class MvcRouteListModel
    {
        public Merchant Merchant { get; set; }
        public IEnumerable<Route> RouteList { get; set; }

        public MvcRouteListModel()
        {
            RouteList = new List<Route>();
        }
    }
}