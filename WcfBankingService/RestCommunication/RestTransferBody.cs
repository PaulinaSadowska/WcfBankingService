// ReSharper disable InconsistentNaming
namespace WcfBankingService.RestCommunication
{
    public class RestTransferBody
    {
        public int amount { get; set; }
        public string sender_account { get; set; }
        public string receiver_account { get; set; }
        public string title { get; set; }
    }
}