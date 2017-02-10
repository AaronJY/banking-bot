using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingBot.Models
{
    public class Account
    {
        public string SortCode { get; set; }

        public string AccountNumber { get; set; }

        public string Name { get; set; }

        public decimal Balance { get; set; }
    }
}
