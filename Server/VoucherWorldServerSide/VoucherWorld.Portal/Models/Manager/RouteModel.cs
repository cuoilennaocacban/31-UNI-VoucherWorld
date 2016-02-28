using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VoucherWorld.Data.Entities;
using VoucherWorld.Data.Enums;

namespace VoucherWorld.Portal.Models.Manager
{
    public class MvcRouteModel
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

        public List<int> PlaceIds { get; set; }

        public IEnumerable<Place> Places { get; set; }

        public MvcRouteModel()
        {
            Gift = new Gift();
            OpenEndedQuestion = new OpenEndedQuestion();
            RoutePlaces = new List<RoutePlace>();
            PlaceIds = new List<int>() { 0, 0, 0 };
            Places = new List<Place>();
        }

        public MvcRouteModel(Merchant merchant)
        {
            Gift = new Gift();
            OpenEndedQuestion = new OpenEndedQuestion();
            RoutePlaces = new List<RoutePlace>();
            Merchant = merchant;
            PlaceIds = new List<int>() {0, 0, 0};
            Places = new List<Place>();
        }

        public MvcRouteModel(Route inputRoute)
        {
            Id = inputRoute.Id;
            Name = inputRoute.Name;
            IsHidden = inputRoute.IsHidden;
            Category = inputRoute.Category;
            Gift = inputRoute.Gift;
            PlaceIcon = inputRoute.PlaceIcon;
            OpenEndedQuestion = inputRoute.OpenEndedQuestion;
            RoutePlaces = inputRoute.RoutePlaces;
            Enrollments = inputRoute.Enrollments;
            MerchantId = inputRoute.MerchantId;
            Merchant = inputRoute.Merchant;

            PlaceIds = RoutePlaces.Select(rp => rp.PlaceId).ToList();


        }
    }

    public class MvcCreateRouteModel
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

        public List<int> PlaceIds { get; set; }

        public IEnumerable<Place> Places { get; set; }

        public MvcCreateRouteModel()
        {
            Gift = new Gift();
            OpenEndedQuestion = new OpenEndedQuestion();
            RoutePlaces = new List<RoutePlace>();
            PlaceIds = new List<int>() { 0, 0, 0 };
            Places = new List<Place>();
        }
    }
}