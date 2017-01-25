using BankingBot.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingBot.Responses
{
    public class Response : IResponse
    {
        public Exception exception { get; set; }

        public string Message { get; set; }

        public bool IsError { get; set; }
    }
}
