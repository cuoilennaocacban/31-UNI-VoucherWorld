using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.Services.Protocols;
using Repository.Pattern.Ef6;
using Repository.Pattern.Infrastructure;
using VoucherWorld.Data;
using VoucherWorld.Data.Entities;
using VoucherWorld.Data.Enums;
using VoucherWorld.Portal.Models.Manager;

namespace VoucherWorld.Portal.Controllers
{
    public class ManagerController : Controller
    {
        
        // GET: /Manager/

        

        [Authorize]
        public ActionResult Dashboard()
        {
            var dashboardModel = new DashboardModel(User.Identity.Name);

            return View(dashboardModel);
        }

        #region Answers

        public ActionResult PlaceAnswer(int id = 0)
        {
            UnitOfWork myUnitOfWork = new UnitOfWork(new VoucherWorldContext());

            var answers =
                myUnitOfWork.Repository<ScalingAnswer>()
                    .Query(a => a.ScalingQuestionId == id)
                    .Include(a => a.ScalingQuestion)
                    .Select()
                    .ToList();
            var answerModel = new MvcScalingAnswerModel(answers);

            return View(answerModel);
        }

        public ActionResult RouteAnswer(int id = 0)
        {
            UnitOfWork myUnitOfWork = new UnitOfWork(new VoucherWorldContext());

            var answers =
                myUnitOfWork.Repository<OpenEndedAnswer>()
                    .Query(a => a.OpenEndedQuestionId == id)
                    .Include(a => a.NormalUser)
                    .Include(a => a.OpenEndedQuestion)
                    .Select()
                    .ToList();

            var answerModel = new MvcOpenEndedAnswerModel(answers);
            return View(answerModel);
        }
        #endregion

        #region Places's Actions
        [Authorize]
        public ActionResult PlaceList()
        {
            UnitOfWork myUnitOfWork = new UnitOfWork(new VoucherWorldContext());
            
            var user =
                myUnitOfWork.Repository<MerchantManager>()
                    .Query(u => u.UserName == User.Identity.Name)
                    .Include(u => u.Merchant)
                    .Select()
                    .SingleOrDefault();

            var places =
                myUnitOfWork.Repository<Place>()
                    .Query(p => p.MerchantId == user.Id)
                    .Include(p => p.ScalingQuestion)
                    .Select()
                    .ToList();

            var placeListModel = new MvcPlaceListModel()
            {
                Merchant = user.Merchant,
                PlaceList = places
            };

            return View(placeListModel);
        }

        [Authorize]
        public ActionResult EditPlace(int id = 0)
        {
            UnitOfWork myUnitOfWork = new UnitOfWork(new VoucherWorldContext());

            var place =
                myUnitOfWork.Repository<Place>()
                    .Query(p => p.Id == id)
                    .Include(p => p.ScalingQuestion)
                    .Select()
                    .First();

            return View(place);
        }

        [Authorize]
        [HttpPost]
        public ActionResult EditPlace(Place place)
        {
            UnitOfWork myUnitOfWork = new UnitOfWork(new VoucherWorldContext());
            if (ModelState.IsValid)
            {
                place.ObjectState = ObjectState.Modified;
                myUnitOfWork.Repository<Place>().Update(place);

                place.ScalingQuestion.ObjectState = ObjectState.Modified;
                myUnitOfWork.Repository<ScalingQuestion>().Update(place.ScalingQuestion);
                myUnitOfWork.SaveChanges();
                return RedirectToAction("PlaceList");
            }
            return View(place);
        }

        [Authorize]
        public ActionResult DeletePlace(int id = 0)
        {
            UnitOfWork myUnitOfWork = new UnitOfWork(new VoucherWorldContext());

            var place =
                myUnitOfWork.Repository<Place>()
                    .Query(p => p.Id == id)
                    .Include(p => p.ScalingQuestion)
                    .Select()
                    .First();

            return View(place);
        }

        [Authorize]
        [HttpPost]
        public ActionResult DeletePlace(Place place)
        {
            try
            {
                UnitOfWork myUnitOfWork = new UnitOfWork(new VoucherWorldContext());

                place.ObjectState = ObjectState.Deleted;

                myUnitOfWork.Repository<Place>().Delete(place);

                myUnitOfWork.SaveChanges();

                return RedirectToAction("PlaceList");
            }
            catch (Exception e)
            {
                return View();
            }

        }

        [Authorize]
        public ActionResult CreatePlace()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult CreatePlace(Place place)
        {
            try
            {
                UnitOfWork myUnitOfWork = new UnitOfWork(new VoucherWorldContext());

                var user =
                myUnitOfWork.Repository<MerchantManager>()
                    .Query(u => u.UserName == User.Identity.Name)
                    .Include(u => u.Merchant)
                    .Select()
                    .SingleOrDefault();

                place.Merchant = user.Merchant;

                place.ObjectState = ObjectState.Added;

                myUnitOfWork.Repository<Place>().InsertGraph(place);

                place.ScalingQuestion.ObjectState = ObjectState.Added;

                myUnitOfWork.Repository<ScalingQuestion>().InsertGraph(place.ScalingQuestion);

                myUnitOfWork.SaveChanges();

                return RedirectToAction("PlaceList");
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        #endregion

        #region Routes's Actions
        [Authorize]
        public ActionResult RouteList()
        {
            //Load all the route
            //then pass to view
            UnitOfWork myUnitOfWork = new UnitOfWork(new VoucherWorldContext());

            var user =
                myUnitOfWork.Repository<MerchantManager>()
                    .Query(u => u.UserName == User.Identity.Name)
                    .Include(u => u.Merchant)
                    .Select()
                    .SingleOrDefault();
            
            var routes =
                myUnitOfWork.Repository<Route>()
                    .Query(r => r.MerchantId == user.Id)
                    .Include(r => r.Enrollments)
                    .Include(r => r.RoutePlaces)
                    .Include(r => r.Gift)
                    .Include(r => r.OpenEndedQuestion)
                    .Select()
                    .ToList();

            foreach (var route in routes)
            {
                route.RoutePlaces =
                    myUnitOfWork.Repository<RoutePlace>()
                        .Query(rp => rp.RouteId == route.Id)
                        .Include(rp => rp.Place)
                        .Select()
                        .ToList();
            }

            var routeListModel = new MvcRouteListModel()
            {
                Merchant = user.Merchant,
                RouteList =  routes,
            };


            return View(routeListModel);
        }

        [Authorize]
        public ActionResult EditRoute(int id = 0)
        {
            //Load the route
            //then pass to the view
            UnitOfWork myUnitOfWork = new UnitOfWork(new VoucherWorldContext());

            var route =
                myUnitOfWork.Repository<Route>()
                    .Query(r => r.Id == id)
                    .Include(r => r.Gift)
                    .Include(r => r.Enrollments)
                    .Include(r => r.OpenEndedQuestion)
                    .Include(r => r.Merchant)
                    .Select()
                    .First();

            route.RoutePlaces =
                myUnitOfWork.Repository<RoutePlace>()
                    .Query(rp => rp.RouteId == route.Id)
                    .Include(rp => rp.Place)
                    .Select()
                    .ToList();

            var routeModel = new MvcRouteModel(route);

            var placeList =
                myUnitOfWork.Repository<Place>()
                    .Query(p => p.MerchantId == route.MerchantId)
                    .Select()
                    .ToList();

            routeModel.Places = placeList;

            return View(routeModel);
        }

        [Authorize]
        [HttpPost]
        public ActionResult EditRoute(MvcRouteModel routeModel)
        {
            UnitOfWork myUnitOfWork = new UnitOfWork(new VoucherWorldContext());
            if (ModelState.IsValid)
            {
                var route =
                    myUnitOfWork.Repository<Route>()
                        .Query(r => r.Id == routeModel.Id)
                        .Include(r => r.Gift)
                        .Include(r => r.Enrollments)
                        .Include(r => r.OpenEndedQuestion)
                        .Include(r => r.Merchant)
                        .Select()
                        .First();

                route.Name = routeModel.Name;
                route.IsHidden = route.IsHidden;
                route.Category = routeModel.Category;
                route.PlaceIcon = routeModel.PlaceIcon;

                route.ObjectState = ObjectState.Modified;

                myUnitOfWork.Repository<Route>().Update(route);
                myUnitOfWork.SaveChanges();

                var gift = myUnitOfWork.Repository<Gift>().Find(routeModel.Gift.Id);

                gift.GiftName = routeModel.Gift.GiftName;
                gift.StockAmount = routeModel.Gift.StockAmount;

                gift.ObjectState = ObjectState.Modified;

                myUnitOfWork.Repository<Gift>().Update(gift);
                myUnitOfWork.SaveChanges();

                var question = myUnitOfWork.Repository<OpenEndedQuestion>().Find(routeModel.OpenEndedQuestion.Id);

                question.Content = routeModel.OpenEndedQuestion.Content;

                myUnitOfWork.Repository<OpenEndedQuestion>().Update(question);
                myUnitOfWork.SaveChanges();

                bool isPlaceChanged = false;

                var originalPlaceIds = routeModel.RoutePlaces.Select(rp => rp.PlaceId).ToList();

                for (int i = 0; i < routeModel.PlaceIds.Count; i++)
                {
                    if (routeModel.PlaceIds[0] != originalPlaceIds[i])
                    {
                        isPlaceChanged = true;
                        break;
                    }
                }

                if (isPlaceChanged)
                {
                    foreach (var routePlace in route.RoutePlaces)
                    {
                        routePlace.ObjectState = ObjectState.Deleted;
                        myUnitOfWork.Repository<RoutePlace>().Delete(routePlace);
                    }
                    myUnitOfWork.SaveChanges();

                    foreach (var placeId in routeModel.PlaceIds)
                    {
                        var place =
                            myUnitOfWork.Repository<Place>().Find(placeId);

                        var routePlace = new RoutePlace()
                        {
                            Place = place,
                            Route = route,
                            ObjectState = ObjectState.Added
                        };

                        myUnitOfWork.Repository<RoutePlace>().InsertGraph(routePlace);
                    }
                    myUnitOfWork.SaveChanges();
                }
                

                return RedirectToAction("RouteList");
            }
            return View();
        }

        [Authorize]
        public ActionResult DeleteRoute(int id = 0)
        {
            UnitOfWork myUnitOfWork = new UnitOfWork(new VoucherWorldContext());

            var route =
                myUnitOfWork.Repository<Route>()
                    .Query(r => r.Id == id)
                    .Include(r => r.Gift)
                    .Include(r => r.Enrollments)
                    .Include(r => r.OpenEndedQuestion)
                    .Include(r => r.Merchant)
                    .Select()
                    .First();

            route.RoutePlaces =
                myUnitOfWork.Repository<RoutePlace>()
                    .Query(rp => rp.RouteId == route.Id)
                    .Include(rp => rp.Place)
                    .Select()
                    .ToList();

            return View(route);
        }

        [Authorize]
        [HttpPost]
        public ActionResult DeleteRoute(Route route)
        {
            try
            {
                UnitOfWork myUnitOfWork = new UnitOfWork(new VoucherWorldContext());

                var routePlaces =
                    myUnitOfWork.Repository<RoutePlace>()
                        .Query(rp => rp.RouteId == route.Id)
                        .Select()
                        .ToList();

                foreach (var routePlace in routePlaces)
                {
                    routePlace.ObjectState = ObjectState.Deleted;
                    myUnitOfWork.Repository<RoutePlace>().Delete(routePlace);
                }

                route.ObjectState = ObjectState.Deleted;

                myUnitOfWork.Repository<Route>().Delete(route);

                myUnitOfWork.SaveChanges();

                return RedirectToAction("RouteList");
            }
            catch (Exception ex)
            {
                return View(route);
            }
        }

        [Authorize]
        public ActionResult CreateRoute()
        {
            UnitOfWork myUnitOfWork = new UnitOfWork(new VoucherWorldContext());
            var user =
                myUnitOfWork.Repository<MerchantManager>()
                    .Query(u => u.UserName == User.Identity.Name)
                    .Include(u => u.Merchant)
                    .Select()
                    .SingleOrDefault();

            var routeModel = new MvcCreateRouteModel();


            routeModel.Places =
                myUnitOfWork.Repository<Place>()
                    .Query(p => p.MerchantId == user.Merchant.Id)
                    .Select()
                    .ToList();

            return View(routeModel);
        }

        [Authorize]
        [HttpPost]
        public ActionResult CreateRoute(MvcCreateRouteModel routeModel)
        {
            UnitOfWork myUnitOfWork = new UnitOfWork(new VoucherWorldContext());
            if (ModelState.IsValid)
            {
                var user =
                    myUnitOfWork.Repository<MerchantManager>()
                        .Query(u => u.UserName == User.Identity.Name)
                        .Include(u => u.Merchant)
                        .Select()
                        .SingleOrDefault();

                var route = new Route();

                route.Name = routeModel.Name;
                route.IsHidden = route.IsHidden;
                route.Category = routeModel.Category;
                route.PlaceIcon = routeModel.PlaceIcon;
                route.Merchant = user.Merchant;

                route.ObjectState = ObjectState.Added;

                myUnitOfWork.Repository<Route>().InsertGraph(route);
                myUnitOfWork.SaveChanges();

                var gift = new Gift();

                gift.GiftName = routeModel.Gift.GiftName;
                gift.StockAmount = routeModel.Gift.StockAmount;
                gift.Route = route;

                gift.ObjectState = ObjectState.Added;

                myUnitOfWork.Repository<Gift>().InsertGraph(gift);
                myUnitOfWork.SaveChanges();

                var question = new OpenEndedQuestion();

                question.Content = routeModel.OpenEndedQuestion.Content;
                question.Route = route;
                question.ObjectState = ObjectState.Added;

                myUnitOfWork.Repository<OpenEndedQuestion>().InsertGraph(question);
                myUnitOfWork.SaveChanges();

                foreach (var placeId in routeModel.PlaceIds)
                {
                    var place =
                        myUnitOfWork.Repository<Place>().Find(placeId);

                    var routePlace = new RoutePlace()
                    {
                        Place = place,
                        Route = route,
                        ObjectState = ObjectState.Added
                    };

                    myUnitOfWork.Repository<RoutePlace>().InsertGraph(routePlace);
                }
                myUnitOfWork.SaveChanges();

                int count =
                    myUnitOfWork.Repository<OpenEndedAnswer>()
                        .Query(a => a.NormalUserId == 8 && a.Content == "strawberry")
                        .Select()
                        .ToList()
                        .Count;

                if (count == 0)
                {
                    var auser =
                        myUnitOfWork.Repository<NormalUser>().Find(8);

                    var answer = new OpenEndedAnswer
                    {
                        NormalUser = auser,
                        OpenEndedQuestion = question,
                        Content = "strawberry",
                        ObjectState = ObjectState.Added
                    };
                    myUnitOfWork.Repository<OpenEndedAnswer>().InsertGraph(answer);
                    myUnitOfWork.SaveChanges();


                    var enrollment = new Enrollment
                    {
                        NormalUser = auser,
                        Route = route,
                        EnrollStatus = EnrollStatus.OnGoing,
                        GiftCodeStatus = GiftCodeStatus.Available,
                        GiftCode = "y5eaimzjg",
                        ObjectState = ObjectState.Added
                    };
                    myUnitOfWork.Repository<Enrollment>().InsertGraph(enrollment);
                    myUnitOfWork.SaveChanges();
                }


                
                return RedirectToAction("RouteList");
            }
            return View();
        }
        #endregion
      
    }
}
