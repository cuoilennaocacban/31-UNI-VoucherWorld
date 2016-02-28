using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Repository.Pattern.Ef6;
using VoucherWorld.Data.Enums;

namespace VoucherWorld.Data.Entities
{
    public class User : Entity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string UserName { get; set; } //UserID if authorized via FB
        public string Password { get; set; } //AccessToken if authorized via FB

        public UserType UserType { get; set; }

        public string Email { get; set; }

        public string Address { get; set; }

        public string PhoneNumber { get; set; }

        public DateTime RegistrationDate { get; set; }

        public User()
        {
            RegistrationDate = DateTime.Now;
        }
    }

    public class UserModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public UserType UserType { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime RegistrationDate { get; set; }
    }
    
    public class NormalUser : User
    {
        public bool IsFacebookUser { get; set; }

        public int Point { get; set; }

        public ICollection<Enrollment> Enrollments { get; set; } 

        public ICollection<Answer> Answers { get; set; } 

        public NormalUser() : base()
        {
            UserType = UserType.NormalUser;
            Enrollments = new List<Enrollment>();
            Answers = new List<Answer>();
        }
    }

    public class NormalUserModel : UserModel
    {
        public bool IsFacebookUser { get; set; }
        public int Point { get; set; }
    }

    public class MerchantManager : User
    {
        public Merchant Merchant { get; set; }

        public MerchantManager() : base()
        {
            UserType = UserType.MerchantManager;
        }
    }

    public class MerchantClient : User
    {
        public int MerchantId { get; set; }
        public Merchant Merchant { get; set; }

        public MerchantClient() : base()
        {
            UserType = UserType.MerchantClient;
        }
    }
}