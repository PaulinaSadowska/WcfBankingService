using System;
using System.Net;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Web;
using WcfBankingService.Service.DataContract.Response;

namespace WcfBankingService.RestCommunication.ErrorHandling
{
    public class JsonErrorHandler : IErrorHandler
    {
        /// <summary>
        /// Checks if the error should be handled in this class
        /// </summary>
        /// <param name="error">error to handle</param>
        /// <returns>if the error should be handled in this class</returns>
        public bool HandleError(Exception error)
        {
            return error is WebFaultException;
        }


        /// <summary>
        /// Provides Json fault message with correct response code
        /// </summary>
        /// <param name="error">exception to handle</param>
        /// <param name="version">message version</param>
        /// <param name="fault">fault message</param>
        public void ProvideFault(Exception error, MessageVersion version,
            ref Message fault)
        {
            fault = this.GetJsonFaultMessage(version, error);

            ApplyJsonSettings(ref fault);
            ApplyHttpResponseSettings(ref fault, HttpStatusCode.BadRequest, "Bad request");
        }

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
            TransferResponse response;
            if (error is SerializationException)
            {
                response = new TransferResponse("Wrong request format");
            }
            else
            {
                response = new TransferResponse("Unauthorized");
            }

            var faultMessage = Message.CreateMessage(version, "", response,
                new DataContractJsonSerializer(response.GetType()));

            return faultMessage;
        }
    }
}