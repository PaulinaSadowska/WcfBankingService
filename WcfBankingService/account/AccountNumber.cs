using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WcfBankingService.account;

namespace WcfBankingService.accunt
{
    public class AccountNumber
    {
        public readonly string BankId;
        public readonly string Number;
        public readonly string ControlSum;

        public AccountNumber(String bankId, String number, String controlSum)
        {
            BankId = bankId;
            Number = number;
            ControlSum = controlSum;
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }

        public override string ToString()
        {
            return $"{ControlSum}{BankId}{Number}";
        }
    }
}