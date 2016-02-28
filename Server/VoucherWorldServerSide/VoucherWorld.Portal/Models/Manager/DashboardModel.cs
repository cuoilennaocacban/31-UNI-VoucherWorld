using System.Linq;
using Repository.Pattern.Ef6;
using VoucherWorld.Data;
using VoucherWorld.Data.Entities;

namespace VoucherWorld.Portal.Models.Manager
{
    public class DashboardModel
    {
        public string UserName { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public Merchant Merchant { get; set; }

        public DashboardModel(string username)
        {
            UnitOfWork myUnitOfWork = new UnitOfWork(new VoucherWorldContext());

            var user =
                myUnitOfWork.Repository<MerchantManager>()
                    .Query(u => u.UserName == username)
                    .Select()
                    .SingleOrDefault();
            
            var merchant =
                myUnitOfWork.Repository<Merchant>()
                    .Query(m => m.Id == user.Id)
                    .Include(m => m.Routes)
                    .Include(m => m.Places)
                    .Select()
                    .First();

            UserName = user.UserName;
            Name = user.Name;
            Address = user.Address;
            Email = user.Email;
            PhoneNumber = user.PhoneNumber;
            Merchant = merchant;

        }
    }
}