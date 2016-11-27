using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WcfBankingService.account
{
    public interface IControlSumCalculator
    {
        string calculate(string bankId, string number);
    }
}
