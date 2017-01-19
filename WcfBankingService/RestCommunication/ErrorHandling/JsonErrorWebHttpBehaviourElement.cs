using System;
using System.ServiceModel.Configuration;

namespace WcfBankingService.RestCommunication.ErrorHandling
{
    public class JsonErrorWebHttpBehaviorElement : BehaviorExtensionElement
    {
        /// 
        /// Get the type of behavior to attach to the endpoint
        /// 
        public override Type BehaviorType => typeof(JsonErrorWebHttpBehavior);

        /// 
        /// Create the custom behavior
        /// 
        protected override object CreateBehavior()
        {
            return new JsonErrorWebHttpBehavior();
        }
    }
}