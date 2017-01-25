using BankingBot.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingBot.Contracts
{
    public interface IResponse
    {
        Exception Exception { get; }

        ResponseStatus Status { get; }
    }
}
