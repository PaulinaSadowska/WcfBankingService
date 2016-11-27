using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WcfBankingService.accunt;

namespace WcfBankingService.account
{
    public interface IAccountNumberFactory
    {
        bool isAccountNumberValid(String accountNumber);
        AccountNumber createAccountNumber(String number);
        AccountNumber getAccountNumber(String accountNumber);
    }
}
