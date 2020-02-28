using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FunctionApp1
{
    public class Policy_Master
    {
        [Key]
        public int PolicyID { get; set; }
        public string PolicyNumber { get; set; }
        public string PolicyType { get; set; }
        public string Status { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public string Year { get; set; }
        public string VIN { get; set; }
        public decimal PremiumAmount { get; set; }
    }
}
