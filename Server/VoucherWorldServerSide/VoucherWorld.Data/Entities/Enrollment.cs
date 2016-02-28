using System;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.Text;
using Repository.Pattern.Ef6;
using VoucherWorld.Data.Enums;

namespace VoucherWorld.Data.Entities
{
    public class Enrollment : Entity
    {
        public int NormalUserId { get; set; }
        public NormalUser NormalUser { get; set; }

        public int RouteId { get; set; }
        public Route Route { get; set; }

        public string GiftCode { get; set; }

        public DateTime EnrollDate { get; set; }

        public EnrollStatus EnrollStatus { get; set; }

        public GiftCodeStatus GiftCodeStatus { get; set; }

        public Enrollment()
        {
            EnrollDate = DateTime.Now;
            GiftCodeStatus = GiftCodeStatus.Available;
            EnrollStatus = EnrollStatus.OnGoing;
            GenerateGiftCode();
        }

        public void GenerateGiftCode()
        {
            int maxSize = 9;
            //int minSize = 6;
            char[] chars = new char[62];
            string a;
            a = "abcdefghijklmnopqrstuvwxyz123456789";
            chars = a.ToCharArray();
            int size = maxSize;
            byte[] data = new byte[1];
            RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider();
            crypto.GetNonZeroBytes(data);
            size = maxSize;
            data = new byte[size];
            crypto.GetNonZeroBytes(data);
            StringBuilder result = new StringBuilder(size);
            foreach (byte b in data)
            {
                result.Append(chars[b % (chars.Length - 1)]);
            }
            GiftCode = result.ToString();
        }
    }
}
