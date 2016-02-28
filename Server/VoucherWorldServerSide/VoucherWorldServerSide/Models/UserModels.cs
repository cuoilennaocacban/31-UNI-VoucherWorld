using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace VoucherWorldServerSide.Models
{
    public enum UserType
    {
        Administrator,
        User,
        StoreManager        
    }
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id{set;get; }
        string Name{ set;get;}
        UserType Type {set;get;}
        string Email { get; set; }
        string Address { get; set; }
        string Telephone { get; set; }
        List<Route> History { get; set; }
    }
}