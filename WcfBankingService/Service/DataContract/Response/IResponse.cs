using WcfBankingService.Service.DataContract.Response;

namespace WcfBankingService.SoapService.DataContract.Response
{
    public interface IResponse
    {
        ResponseStatus ResponseStatus { get; }
    }
}