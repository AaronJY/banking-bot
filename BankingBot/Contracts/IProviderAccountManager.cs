using BankingBot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankingBot.Contracts
{
    public interface IProviderAccountManager
    {
        IEnumerable<Account> GetAccounts();

        IEnumerable<Transaction> GetTransactions(string accountNumber);
    }
}
