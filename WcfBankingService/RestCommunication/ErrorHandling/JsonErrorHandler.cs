using System;
using System.Collections.Generic;
using System.Net;
using System.Runtime.Serialization.Json;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Web;
using WcfBankingService.Service.DataContract.Response;

namespace WcfBankingService.RestCommunication.ErrorHandling
{
    public class JsonErrorHandler : IErrorHandler
    {
        #region Public Method(s)

        #region IErrorHandler Members

        ///
        /// Is the error always handled in this class?
        ///
        public bool HandleError(Exception error)
        {
            return error is WebFaultException;
        }

        ///
        /// Provide the Json fault message
        ///
        public void ProvideFault(Exception error, MessageVersion version,
            ref Message fault)
        {
            fault = this.GetJsonFaultMessage(version, error);

            ApplyJsonSettings(ref fault);
            ApplyHttpResponseSettings(ref fault,
                System.Net.HttpStatusCode.Unauthorized, "response");
        }

        #endregion

        #endregion

        #region Protected Method(s)

        ///
        /// Apply Json settings to the message
        ///
        protected virtual void ApplyJsonSettings(ref Message fault)
        {
            // Use JSON encoding
            var jsonFormatting =
                new WebBodyFormatMessageProperty(WebContentFormat.Json);
            fault.Properties.Add(WebBodyFormatMessageProperty.Name, jsonFormatting);
        }

        ///
        /// Get the HttpResponseMessageProperty
        ///
        protected virtual void ApplyHttpResponseSettings(
            ref Message fault, HttpStatusCode statusCode,
            string statusDescription)
        {
            var httpResponse = new HttpResponseMessageProperty()
            {
                StatusCode = statusCode,
                StatusDescription = statusDescription
            };
            fault.Properties.Add(HttpResponseMessageProperty.Name, httpResponse);
        }

        ///
        /// Get the json fault message from the provided error
        ///
        protected virtual Message GetJsonFaultMessage(
            MessageVersion version, Exception error)
        {
            var knownTypes = new List<Type>(); 
            var faultType = error.GetType().Name; 

            if ((error is FaultException) && 
            (error.GetType().GetProperty("Detail") != null))
            {
                var detail = (error.GetType().GetProperty("Detail").GetGetMethod().Invoke(
                    error, null));
                knownTypes.Add(detail?.GetType());
                faultType = detail?.GetType().Name;
            }

            var response = new TransferResponse("Unauthorized");

            var faultMessage = Message.CreateMessage(version, "", response,
                new DataContractJsonSerializer(response.GetType(), knownTypes));

            return faultMessage;
        }

        #endregion
    }
}