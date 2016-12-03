using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WcfBankingService.account.number;

namespace WcfBankingService.account
{
    public interface IAccountNumberFactory
    {
        bool IsAccountNumberValid(String accountNumber);
        AccountNumber CreateAccountNumber(String number);
        AccountNumber GetAccountNumber(String accountNumber);
    }
}
