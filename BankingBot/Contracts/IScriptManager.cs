using System.Collections.Generic;

namespace BankingBot.Contracts
{
    public interface IScriptManager
    {
        T Execute<T>(string scriptPath, Dictionary<string, string> data, string[] scripts);
        void Execute(string scriptPath, Dictionary<string, string> data, string[] scripts);
    }
}