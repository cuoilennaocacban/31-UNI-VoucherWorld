using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoucherWorld.Data.Entities;
using VoucherWorld.Data.Utilities;

namespace VoucherWorld.Data
{
    public class ModelFactory
    {
        public ModelFactory(){}
        
        public GiftModel Create(Gift gift)
        {
            if (gift == null)
            {
                return null;
            }
            var result = new GiftModel
            {
                Id = gift.Id,
                GiftName = gift.GiftName,
                Images = gift.Images
            };
            return result;
        }

        public MerchantModel Create(Merchant merchant)
        {
            if (merchant == null)
            {
                return null;
            }
            var result = new MerchantModel
            {
                Id = merchant.Id,
                Name = merchant.Name,
                Address = merchant.Address,
                PhoneNumber = merchant.PhoneNumber,
                Website = merchant.Website,
            };

            return result;
        }

        public PlaceModel Create(Place place)
        {
            if (place == null)
            {
                return null;
            }
            Random r = new Random();
            double v = r.NextDouble();
            int bonus;
            if (v < 0.6)
            {
                bonus = 0;
            }
            else if (v < 0.9)
            {
                bonus = 5;
            }
            else
            {
                bonus = 10;
            }

            var result = new PlaceModel
            {
                Id = place.Id,
                Name = place.Name,
                Address = place.Address,
                BonusPoint = bonus,
                Longitude = place.Longitude,
                Latitude = place.Latitude,
                Altitude = place.Altitude,
                Question = Create(place.ScalingQuestion),
            };

            return result;
        }

        public QuestionModel Create(ScalingQuestion question)
        {
            if (question == null)
            {
                return null;
            }
            var result = new QuestionModel
            {
                Id = question.Id,
                Content = question.Content
            };
            return result;
        }

        public QuestionModel Create(OpenEndedQuestion question)
        {
            if (question == null)
            {
                return null;
            }
            var result = new QuestionModel
            {
                Id = question.Id,
                Content = question.Content
            };
            return result;
        }

        public RouteModel Create(Route route)
        {
            if (route == null)
            {
                return null;
            }
            var result = new RouteModel
            {
                Id = route.Id,
                Name = route.Name,
                Category = route.Category,
                Gift = Create(route.Gift),
                PlaceIcon = route.PlaceIcon,
                Question = Create(route.OpenEndedQuestion),
                Merchant = Create(route.Merchant)
            };

            var places = route.RoutePlaces.Select(x => x.Place).ToList();
            var placeModels = new List<PlaceModel>();
            foreach (var place in places)
            {
                placeModels.Add(Create(place));
            }

            result.Places = placeModels;

            return result;
        }

        public SimpleRouteModel Create(Route route, double lat, double lon)
        {
            if (route == null)
            {
                return null;
            }
            var result = new SimpleRouteModel
            {
                Id = route.Id,
                Name = route.Name,
                IsHidden = route.IsHidden,
                Category = route.Category,
                Gift = Create(route.Gift),
                PlaceIcon = route.PlaceIcon,
                Question = Create(route.OpenEndedQuestion),
                Merchant = Create(route.Merchant)
            };

            var places = route.RoutePlaces.Select(x => x.Place).ToList();

            result.Place = Create(places[0]);

            result.Distance = StaticMethods.Distance(places[0], lat, lon);

            return result;
        }
    }
}
