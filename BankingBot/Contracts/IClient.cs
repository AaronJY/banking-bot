using System.Collections;
using System.Collections.Generic;
using BankingBot.Models;
using BankingBot.Responses;

namespace BankingBot.Contracts
{
    public interface IClient
    {
        Response Login(ILoginCredentials credentials);

        decimal GetBalance();

        IEnumerable<Account> GetAccounts();
    }
}