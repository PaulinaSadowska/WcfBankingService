using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WcfBankingService.user
{
    public interface IUser
    {
        string GenerateAccessToken(string password);
        
        bool ContainsAccessToken(string accessToken);

        string GetLogin();
    }
}
