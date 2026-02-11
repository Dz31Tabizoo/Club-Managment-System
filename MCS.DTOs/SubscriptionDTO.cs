using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.DTOs
{
    public class SubscriptionDTO
    {
        public int SubscriptionID { get; set; }
        public int PlayerID { get; set; }
        public string PlayerName {get; set; }

        public byte Month { get; set; }
        public int Year { get; set; }
        public decimal Amount { get; set; }
        public bool isPaid { get; set; }
        public DateTime? PaymentDate { get; set; }

    }
}
