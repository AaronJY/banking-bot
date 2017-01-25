using BankingBot.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankingBot.Enums;

namespace BankingBot.Responses
{
    public class Response : IResponse
    {
        public Exception Exception { get; set; }

        public ResponseStatus Status { get; set; }
    }
}
