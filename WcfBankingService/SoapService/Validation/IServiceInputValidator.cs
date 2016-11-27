using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WcfBankingService.SoapService
{
    interface IServiceInputValidator
    {
        //throws exception when sign in data are not valid
        void CheckSignInDataValid(String login, String password);
    }
}
