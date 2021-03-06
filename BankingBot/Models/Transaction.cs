﻿using BankingBot.Enums;
using System;

namespace BankingBot.Models
{
    public class Transaction
    {
        public decimal Amount { get; set; }

        public TransactionType Type { get; set; }

        public string Description { get; set; }

        public DateTime Date { get; set; }

        public bool IsPending { get; set; }
    }
}
