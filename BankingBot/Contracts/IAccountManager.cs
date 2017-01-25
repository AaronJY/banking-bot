using BankingBot.Models;
using System.Collections.Generic;

namespace BankingBot.Contracts
{
    public interface IAccountManager
    {
        IEnumerable<Account> GetAccounts();
    }
}
