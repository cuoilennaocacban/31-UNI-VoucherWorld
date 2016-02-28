using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using Repository.Pattern.Ef6;
using Repository.Pattern.Infrastructure;
using VoucherWorld.Data;
using VoucherWorld.Data.Entities;
using VoucherWorld.Data.Enums;
using VoucherWorld.Portal.Models.Client;

namespace VoucherWorld.Portal.Controllers
{
    public class ClientsController : Controller
    {
        //
        // GET: /Clients/

        [Authorize]
        public ActionResult CheckGiftCode()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult CheckGiftCode(ClientModel clientModel)
        {
            var giftcode = clientModel.GiftCode;
            UnitOfWork myUnitOfWork = new UnitOfWork(new VoucherWorldContext());
            ModelFactory myModelFactory = new ModelFactory();;
            var enrollment =
                myUnitOfWork.Repository<Enrollment>()
                    .Query(e => e.EnrollStatus == EnrollStatus.OnGoing
                                && e.GiftCodeStatus == GiftCodeStatus.Available
                                && e.GiftCode == giftcode)
                    .Include(e => e.Route.Merchant)
                    .Select()
                    .First();

            if (enrollment == null)
            {
                return View();
            }

            var gift =
                myUnitOfWork.Repository<Gift>()
                    .Find(enrollment.RouteId);

            return RedirectToAction("RedeemGift", clientModel);
        }

        [Authorize]
        [HttpGet]
        public ActionResult RedeemGift(ClientModel clientModel)
        {
            UnitOfWork myUnitOfWork = new UnitOfWork(new VoucherWorldContext());
            ModelFactory myModelFactory = new ModelFactory(); ;
            var enrollment =
                myUnitOfWork.Repository<Enrollment>()
                    .Query(e => e.EnrollStatus == EnrollStatus.OnGoing
                                && e.GiftCodeStatus == GiftCodeStatus.Available
                                && e.GiftCode == clientModel.GiftCode)
                    .Include(e => e.Route.Gift)
                    .Include(e => e.NormalUser)
                    .Select()
                    .First();
            
            return View(enrollment);
        }

        [Authorize]
        [HttpPost]
        public ActionResult RedeemGift(Enrollment enrollment)
        {
            UnitOfWork myUnitOfWork = new UnitOfWork(new VoucherWorldContext());

            var toUpdate =
                myUnitOfWork
                    .Repository<Enrollment>()
                    .Query(e => e.NormalUserId == enrollment.NormalUserId
                                && e.RouteId == enrollment.RouteId)
                    .Select()
                    .First();

            if (toUpdate.GiftCode == "y5eaimzjg")
            {
                toUpdate.GiftCodeStatus = GiftCodeStatus.Used;
                toUpdate.EnrollStatus = EnrollStatus.Finish;

                toUpdate.ObjectState = ObjectState.Modified;

                myUnitOfWork.Repository<Enrollment>().Update(toUpdate);

                myUnitOfWork.SaveChanges(); 
            }
            TempData["msg"] = "<script>alert('Redeem successfully!');</script>";
            return RedirectToAction("CheckGiftCode");
        }
    }
}
