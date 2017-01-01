using System.Runtime.Serialization;
using static System.Int32;

namespace WcfBankingService.RestService
{
    public class BankingRestService : IBankingRestService
    {
        public Output JsonData(string id)
        {
            return new Output
            {
                Index = Parse(id),
                ServiceName = "DupaDupa"
            };
        }
    }

    [DataContract]
    public class Output
    {
        [DataMember]
        public int Index { get; set; }

        [DataMember]
        public string ServiceName { get; set; }
    }
}