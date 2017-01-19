using System.ServiceModel.Description;

namespace WcfBankingService.RestCommunication.ErrorHandling
{
    public class JsonErrorWebHttpBehavior : WebHttpBehavior
    {
        /// 
        /// Add the json error handler to channel error handlers
        /// 
        protected override void AddServerErrorHandlers(ServiceEndpoint endpoint,
          System.ServiceModel.Dispatcher.EndpointDispatcher endpointDispatcher)
        {
            // clear default error handlers.
            endpointDispatcher.ChannelDispatcher.ErrorHandlers.Clear();

            // add the Json error handler.
            endpointDispatcher.ChannelDispatcher.ErrorHandlers.Add(
              new JsonErrorHandler());
        }
    }
}