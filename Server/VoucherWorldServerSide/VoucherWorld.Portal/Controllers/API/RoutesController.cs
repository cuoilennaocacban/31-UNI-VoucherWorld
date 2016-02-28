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
using VoucherWorld.Data.Utilities;

namespace VoucherWorld.Portal.Controllers.API
{
    public class RoutesController : ApiController
    {
        //GET /api/routes?lat=<value>&lon=<value>&distance=<value>
        [ActionName("DefaultAction")]
        public HttpResponseMessage GetHiddenRoutes(double lat, double lon, double distance)
        {
            var myUnitOfWork = new UnitOfWork(new VoucherWorldContext());
            var myModelFactory = new ModelFactory();

            var routes =
                myUnitOfWork.Repository<Route>()
                    .Query(r => r.IsHidden == true)
                    .Include(r => r.Gift)
                    .Include(r => r.OpenEndedQuestion)
                    .Include(r => r.RoutePlaces)
                    .Include(r => r.Merchant)
                    .Select()
                    .ToList()
                    .Where(r => r.Gift.StockAmount > 0);

            foreach (var route in routes)
            {
                var routePlaces =
                myUnitOfWork.Repository<RoutePlace>()
                    .Query(rp => rp.RouteId == route.Id)
                    .Include(rp => rp.Place)
                    .Select()
                    .ToList();

                route.RoutePlaces = routePlaces;
            }

            var routeModels = routes.Select(r => myModelFactory.Create(r, lat, lon));


            var result =
                routeModels
                    .Where(r => r.Distance <= distance)
                    .ToList();

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        //GET /api/routes?lat=<value>&lon=<value>&distance=<value>&cat=<value>
        [ActionName("DefaultAction")]
        public HttpResponseMessage GetNearbyRoutes(double lat, double lon, double distance, int cat)
        {
            var myUnitOfWork = new UnitOfWork(new VoucherWorldContext());
            var myModelFactory = new ModelFactory();

            var routes =
                myUnitOfWork.Repository<Route>()
                    .Query(r => r.Category == (RouteCategory)cat && r.IsHidden == false)
                    .Include(r => r.Gift)
                    .Include(r => r.OpenEndedQuestion)
                    .Include(r => r.RoutePlaces)
                    .Include(r => r.Merchant)
                    .Select()
                    .ToList()
                    .Where(r => r.Gift.StockAmount > 0);

            foreach (var route in routes)
            {
                var routePlaces =
                myUnitOfWork.Repository<RoutePlace>()
                    .Query(rp => rp.RouteId == route.Id)
                    .Include(rp => rp.Place)
                    .Select()
                    .ToList();

                route.RoutePlaces = routePlaces;
            }

            var routeModels = routes.Select(r => myModelFactory.Create(r, lat, lon));


            var result =
                routeModels
                    .Where(r => r.Distance <= distance)
                    .ToList();

            return Request.CreateResponse(HttpStatusCode.OK, result);
        }

        //GET /api/routes?id=<value>
        [ActionName("DefaultAction")]
        public HttpResponseMessage GetRouteDetails(int id)
        {
            var myUnitOfWork = new UnitOfWork(new VoucherWorldContext());
            var myModelFactory = new ModelFactory();

            var route =
                myUnitOfWork.Repository<Route>()
                    .Query(r => r.Id == id)
                    .Include(r => r.Gift)
                    .Include(r => r.OpenEndedQuestion)
                    .Include(r => r.Merchant)
                    .Select()
                    .First();

            var routePlaces =
                myUnitOfWork.Repository<RoutePlace>()
                    .Query(rp => rp.RouteId == id)
                    .Include(rp => rp.Place.ScalingQuestion)
                    .Select()
                    .ToList();

            route.RoutePlaces = routePlaces;

            var result = myModelFactory.Create(route);

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
                    .Query(g => g.Id == routeId && g.StockAmount > 0)
                    .Select()
                    .ToList();

            if (gifts.Count == 0)
            {
                return Request.CreateResponse(HttpStatusCode.NoContent, "Out of Stock");
            }

            var gift = gifts[0];
            --gift.StockAmount;
            gift.ObjectState = ObjectState.Modified;

            myUnitOfWork.Repository<Gift>().Update(gift);

            myUnitOfWork.SaveChanges();

            var enrollment = new Enrollment
            {
                NormalUser = user,
                Route = route,
                ObjectState = ObjectState.Added
            };

            myUnitOfWork.Repository<Enrollment>().InsertGraph(enrollment);

            myUnitOfWork.SaveChanges();

            return Request.CreateResponse(HttpStatusCode.OK, enrollment.GiftCode);
        }

        //GET /api/routes/cancel?userId=<value>&routeId=<value>
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

            var gift = myUnitOfWork.Repository<Gift>().Find(enrollment.RouteId);
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
