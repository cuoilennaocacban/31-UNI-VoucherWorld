using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Repository.Pattern.Ef6;

namespace VoucherWorld.Data.Entities
{
    public class Merchant : Entity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Address { get; set; }

        public string PhoneNumber { get; set; }

        public string Website { get; set; }

        public MerchantManager MerchantManager { get; set; }

        public ICollection<MerchantClient> MerchantClients { get; set; } 

        public ICollection<Place> Places { get; set; }
        
        public ICollection<Route> Routes { get; set; }
        
        public Merchant()
        {
            Places = new List<Place>();
            Routes = new List<Route>();
            MerchantClients = new List<MerchantClient>();
        }
    }

    public class MerchantModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Website { get; set; }
    }
}