using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Repository.Pattern.Ef6;
using Repository.Pattern.Infrastructure;
using VoucherWorld.Data;
using VoucherWorld.Data.Entities;
using VoucherWorld.Data.Enums;

namespace VoucherWorld.API.Controllers
{
    public class RoutesController : ApiController
    {
        //GET /api/routes/
        [ActionName("DefaultAction")]
        public HttpResponseMessage GetAllRoutes()
        {
            UnitOfWork myUnitOfWork = new UnitOfWork(new VoucherWorldContext());
            var result =
                myUnitOfWork.Repository<Route>()
                    .Query()
                    .Include(r => r.Gifts)
                    .Select()
                    .ToList();
            foreach (var route in result)
            {
                var routePlaceList =
                    myUnitOfWork.Repository<RoutePlace>()
                        .Query(rp => rp.RouteId == route.Id)
                        .Select(rp => rp.PlaceId)
                        .ToList();
                route.Places =
                    myUnitOfWork.Repository<Place>()
                    .Query(p => routePlaceList.Contains(p.Id))
                    .Select()
                    .ToList();
            }
            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        //GET /api/routes?lat=<value>&lon=<value>&distance=<value>&cat=<value>
        [ActionName("DefaultAction")]
        public HttpResponseMessage GetNearbyRoutes(double lat, double lon, double distance, int cat)
        {
            //var myUnitOfWork = new UnitOfWork(new VoucherWorldContext());

            //var nearbyPlaces =
            //    myUnitOfWork.Repository<Place>()
            //        .Query(p => p.IsInDistance(lat, lon, distance))
            //        .Select(p => p.Id)
            //        .ToList();

            //var routeIdList =
            //    myUnitOfWork.Repository<RoutePlace>()
            //        .Query(rp => nearbyPlaces.Contains(rp.PlaceId))
            //        .Select(rp => rp.RouteId)
            //        .ToList();

            //var result =
            //    myUnitOfWork.Repository<Route>()
            //        .Query(r => routeIdList.Contains(r.Id) && r.Category == (RouteCategory)cat)
            //        .Select()
            //        .ToList();
            
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        //GET /api/routes?id=<value>
        [ActionName("DefaultAction")]
        public HttpResponseMessage GetRouteDetails(int id)
        {
            UnitOfWork myUnitOfWork = new UnitOfWork(new VoucherWorldContext());
            var result = myUnitOfWork.Repository<Route>().Find(id);

            result.Gifts =
                myUnitOfWork.Repository<Gift>()
                    .Query(g => g.RouteId == id)
                    .Include(g => g.InfoImages)
                    .Select()
                    .ToList();

            var routePlaceList =
                    myUnitOfWork.Repository<RoutePlace>()
                        .Query(rp => rp.RouteId == id)
                        .Select(rp => rp.PlaceId)
                        .ToList();

            result.Places =
                    myUnitOfWork.Repository<Place>()
                    .Query(p => routePlaceList.Contains(p.Id))
                    .Select()
                    .ToList();


            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        //GET /api/routes/start?userId=<value>&routeId=<value>
        [ActionName("start")]
        [HttpGet]
        public HttpResponseMessage StartRoute(int userId, int routeId)
        {
            UnitOfWork myUnitOfWork = new UnitOfWork(new VoucherWorldContext());

            var user =
                myUnitOfWork
                    .Repository<NormalUser>()
                    .Find(userId);
            var route =
                myUnitOfWork
                    .Repository<Route>()
                    .Find(routeId);

            if (user == null || route == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "User or Route not found");
            }

            List<Gift> gifts =
                myUnitOfWork
                    .Repository<Gift>()
                    .Query(g => g.RouteId == routeId && g.StockAmount > 0)
                    .Select()
                    .ToList();

            if (gifts.Count == 0)
            {
                return Request.CreateResponse(HttpStatusCode.NoContent, "Out of Stock");
            }

            var gift = gifts[new Random().Next(0, gifts.Count)];
            --gift.StockAmount;
            gift.ObjectState = ObjectState.Modified;

            myUnitOfWork.Repository<Gift>().Update(gift);

            myUnitOfWork.SaveChanges();

            var enrollment = new Enrollment
            {
                NormalUser = user,
                Route = route,
                EnrollStatus = EnrollStatus.OnGoing,
                GiftCodeStatus = GiftCodeStatus.Assigned,
                Gift = gift,
                ObjectState = ObjectState.Added
            };

            myUnitOfWork.Repository<Enrollment>().InsertGraph(enrollment);

            myUnitOfWork.SaveChanges();

            return Request.CreateResponse(HttpStatusCode.OK, enrollment);
        }

        //GET /api/routes/cancel??userId=<value>&routeId=<value>
        [ActionName("cancel")]
        [HttpGet]
        public HttpResponseMessage CancelRoute(int userId, int routeId)
        {
            UnitOfWork myUnitOfWork = new UnitOfWork(new VoucherWorldContext());

            var user =
                myUnitOfWork
                    .Repository<NormalUser>()
                    .Find(userId);
            var route =
                myUnitOfWork
                    .Repository<Route>()
                    .Find(routeId);

            if (user == null || route == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "User or Route not found");
            }

            var enrollment = myUnitOfWork.Repository<Enrollment>().Find(userId, routeId);
            
            if (enrollment == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound, "Enrollment not found");
            }

            var gift = myUnitOfWork.Repository<Gift>().Find(enrollment.GiftId);
            ++gift.StockAmount;
            gift.ObjectState = ObjectState.Modified;

            myUnitOfWork.Repository<Gift>().Update(gift);

            enrollment.ObjectState = ObjectState.Deleted;

            myUnitOfWork.Repository<Enrollment>().Delete(enrollment);

            myUnitOfWork.SaveChanges();

            return Request.CreateResponse(HttpStatusCode.OK, "True");
        }
    }
}
