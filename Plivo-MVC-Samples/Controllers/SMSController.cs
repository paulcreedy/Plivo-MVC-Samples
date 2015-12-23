using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using RestSharp;
using Plivo.API;
using Plivo_MVC_Samples.Models;
using System.Text;
using Plivo_MVC_Samples.Utilities;

namespace Plivo_MVC_Samples.Controllers
{
    /// <summary>
    /// The SMSController demonstrates ways of sending and receiving sms text messages.
    /// </summary>
    public class SMSController : Controller
    {
        /// <summary>
        /// The base url used in the samples. Set this to the website that your code is running on
        /// Read from the App Settings of the web.config.
        /// </summary>
        private string _baseUrl;

        /// <summary>
        /// Your Plivo Authorisation ID
        /// Read from the App Settings of the web.config
        /// </summary>
        private string _authID;

        /// <summary>
        /// Your Plivo Authentication Token.
        /// Read from the App Settings of the web.config
        /// </summary>
        private string _authToken;

        /// <summary>
        /// The senders phone number
        /// </summary>
        private string _senderPhone;

        /// <summary>
        /// The recipients phone number
        /// </summary>
        private string _recipientPhone;

        /// <summary>
        /// [ERROR: invalid expression FieldName.Words.TheAndAllAsSentence]
        /// </summary>
        private string _emailTo;

        /// <summary>
        /// Initializes a new instance of the <see cref="SMSController"/> class.
        /// </summary>
        public SMSController()
        {
            _baseUrl = ConfigurationManager.AppSettings["BaseUrl"];
            _authID = ConfigurationManager.AppSettings["AuthID"];
            _authToken = ConfigurationManager.AppSettings["AuthToken"];
            _emailTo = ConfigurationManager.AppSettings["EmailTo"];
            _senderPhone = ConfigurationManager.AppSettings["FromTelephone"];
            _recipientPhone = ConfigurationManager.AppSettings["ToTelephone"];
        }

        /// <summary>
        /// Sends a SMS message with a call back to a delivery report url.
        /// </summary>
        /// <returns>ActionResult.</returns>
        public ActionResult SendSMS()
        {
            RestAPI plivo = new RestAPI(_authID, _authToken);

            IRestResponse<MessageResponse> resp = plivo.send_message(new Dictionary<string, string>(){
                { "src", _senderPhone }, // Sender's phone number with country code
                { "dst", _recipientPhone }, // Receiver's phone number with country code
                { "text", "Hi, text from Plivo." }, // Your SMS text message
                // To send Unicode text
                // {"text", "こんにちは、元気ですか？"} // Your SMS text message - Japanese
                // {"text", "Ce est texte généré aléatoirement"} // Your SMS text message - French
                { "url",_baseUrl + "/SMS/delivery_report"}, // The URL to which with the status of the message is sent
                { "method", "POST"} // Method to invoke the url
            });

            // The response holds other information that we can do something with such as store the result in a database.



            return null;
        }

        /// <summary>
        /// Receives a delivery report from the sending of an sms message.
        /// </summary>
        /// <remarks>
        /// The Delivery_Report may get hit up to 3 times by the SendSMS as it delivers the status of the SMS as it goes enroute.
        /// You are likely to get a Sent, Queued and Delivered response.
        /// </remarks>
        /// <param name="response">The response.</param>
        /// <returns>ActionResult.</returns>
        public ActionResult Delivery_Report(SMSDeliveryResponseParameters response)
        {
            // Store the result of the delivery report in a database or something here.

            // As a test let's email the delivery report to ourself
            // Expecte 3 emails, 1 for each delivery status, Send, Queued, Delivered
            StringBuilder emailBody = new StringBuilder();
            emailBody.Append("Hi\r\n");
            emailBody.Append("Delivery report.\r\n");
            emailBody.Append(String.Format("From: {0} \r\n", response.From));
            emailBody.Append(String.Format("To: {0}.\r\n", response.To));
            emailBody.Append(String.Format("Parts: {0} \r\n", response.PartInfo));
            emailBody.Append(String.Format("Status: {0} \r\n", response.Status));
            emailBody.Append(String.Format("UUID: {0} \r\n", response.UUID));
            emailBody.Append(String.Format("Parent Message UUID: {0} \r\n", response.ParentMessageUUID));

            Email.SendEmail(_emailTo, "SMS Delivery Report from Plivo Samples", emailBody.ToString());

            return null;
        }

    }
}
