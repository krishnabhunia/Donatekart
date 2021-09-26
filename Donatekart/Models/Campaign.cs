using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Donatekart.Models
{
    public class Campaign
    {
        public string code { get; set; }
        public string title { get; set; }
        public int priority { get; set; }
        public string shortDesc { get; set; }
        public string imageSrc { get; set; }
        public DateTime created { get; set; }
        public DateTime endDate { get; set; }
        public decimal totalAmount { get; set; }
        public decimal procuredAmount { get; set; }
        public decimal totalProcured { get; set; }
        public decimal backersCount { get; set; }
        public decimal categoryId { get; set; }
        public string location { get; set; }
        public string ngoName { get; set; }
        public decimal daysLeft { get; set; }
        public decimal percentage { get; set; }
    }
}