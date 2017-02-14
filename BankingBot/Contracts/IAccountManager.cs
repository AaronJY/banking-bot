using BankingBot.Enums;
using BankingBot.Models;
using System.Collections.Generic;

namespace BankingBot.Contracts
{
    public interface IAccountManager
    {
        void Init(Provider provider);

        IEnumerable<Account> GetAccounts();

        IEnumerable<Transaction> GetTransactions(string accountNumber);
    }
}
