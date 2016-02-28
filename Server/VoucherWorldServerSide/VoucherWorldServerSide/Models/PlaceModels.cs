using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace VoucherWorldServerSide.Models
{
    public class Place
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get;set ;}
        public double Long { get; set; }
        public double Lat { get; set; }
        public double Att { get; set; }
        public string Addess { get; set; }
        public Challenge Challenge { get; set; }       
    }
}