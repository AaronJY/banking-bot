using BankingBot.Enums;
using System;

namespace BankingBot.Contracts
{
    public interface IResponse
    {
        Exception Exception { get; }

        ResponseStatus Status { get; }
    }
}
