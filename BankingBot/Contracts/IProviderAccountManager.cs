using BankingBot.Models;
using System.Collections.Generic;

namespace BankingBot.Contracts
{
    public interface IProviderAccountManager
    {
        IEnumerable<Account> GetAccounts();

        IEnumerable<Transaction> GetTransactions(string accountNumber);
    }
}
