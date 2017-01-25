using BankingBot.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BankingBot.Models;

namespace BankingBot.ActionManagers.AccountManagers
{
    public class AccountManager : ActionManager, IAccountManager
    {
        public AccountManager(IBrowserBot browserBot)
            : base(browserBot)
        {
        }

        public IEnumerable<Account> GetAccounts()
        {
            throw new NotImplementedException();
        }
    }
}
