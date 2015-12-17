// ***********************************************************************
// Assembly         : Plivo_MVC_Samples
// Author           : Paul
// Created          : 12-17-2015
//
// Last Modified By : Paul
// Last Modified On : 12-16-2015
// ***********************************************************************
// <copyright file="PlivoController.cs" company="">
//     Copyright ©  2015
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using System.Diagnostics;
using Plivo.XML;
using Plivo.API;
using RestSharp;
using Plivo_MVC_Samples.Utilities;
using System.Text;
using Plivo_MVC_Samples.Models;

namespace Plivo_MVC_Samples.Controllers
{
    /// <summary>
    /// Class PlivoController.
    /// </summary>
    public class PlivoController : Controller
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
        /// A valid telephone number that the samples can forward calls onto
        /// Taken from the App Settings of the web.config
        /// </summary>
        private string _forwardCallNumber;

        /// <summary>
        /// [ERROR: invalid expression FieldName.Words.TheAndAllAsSentence]
        /// </summary>
        private string _emailTo;

        /// <summary>
        /// Initializes a new instance of the <see cref="PlivoController"/> class.
        /// </summary>
        public PlivoController()
        {
            _baseUrl = ConfigurationManager.AppSettings["BaseUrl"];
            _authID = ConfigurationManager.AppSettings["AuthID"];
            _authToken = ConfigurationManager.AppSettings["AuthToken"];
            _emailTo = ConfigurationManager.AppSettings["EmailTo"];
            _forwardCallNumber = "442921202870";
        }

        /// <summary>
        /// Greets the caller by name by looking up the caller from number and using text to speech to pronounce the callers name.
        /// </summary>
        /// <param name="From">From.</param>
        /// <returns>ActionResult.</returns>
        /// <remarks>In this example the look up numbers are stored in a dictionary, but in a production environment you would get this from external storage via a repository layer.</remarks>
        public ActionResult GreetCaller(String From=null)
        {  

            // Dictionary of known callers
            var callers = new Dictionary<string, string>() {
                    {"1111111111","ABFD GRE"},
                    {"2222222222","VWXYZ"},
                    {"3333333333","QWERTY"}
                };

            Plivo.XML.Response resp = new Plivo.XML.Response();

            // Can we get the callers name?
            if (From !=null && callers.ContainsKey(From))
            {
                string body = "Hello " + callers[From];
                resp.AddSpeak(body, new Dictionary<string, string>() { });
            }
            else // No? Then we announce a default greeting.
            {
                resp.AddSpeak("Hello Stranger!", new Dictionary<string, string>() { });
            }
            var xml = resp.ToString();
            return Content(xml, "text/xml");
        }

        /// <summary>
        /// Transfer an incoming call to another number, but first announces the transfer using text to speech.
        /// The redirect is than carried out by requesting a secondary url. In this example we redirect to the Connect Action.
        /// </summary>
        /// <param name="From">From.</param>
        /// <returns>ActionResult.</returns>
        public ActionResult CallTransfer(String From)
        {
            Plivo.XML.Response resp = new Plivo.XML.Response();
            resp.AddSpeak("Please wait while your call is being transferred", new Dictionary<string, string>() { });
            resp.AddRedirect(_baseUrl + "Plivo/Connect", new Dictionary<string, string>() { });
            var xml = resp.ToString();
            return Content(xml, "text/xml");
        }

        /// <summary>
        /// An alternative way of connecting a call. This example uses a call back url that is hit when the call has been dialed.
        /// </summary>
        /// <returns>ActionResult.</returns>
        /// <remarks>Calling a URL after the dial allows us to do things like get the status of a call.</remarks>
        public ActionResult Connect_Example2()
        {
            Plivo.XML.Response resp = new Plivo.XML.Response();

            // Generate Dial XML
            Plivo.XML.Dial dial = new Plivo.XML.Dial(new Dictionary<string, string>()
                {
                    {"action",_baseUrl + "DialStatus"}, // Redirect to this URL after leaving Dial. 
                    {"method","GET"}, // Submit to action URL using GET or POST.
                    {"redirect", "true"} // If set to false, do not redirect to action URL. We expect an XML from the action URL if this parameter is set to true. 
                });
            dial.AddNumber(_forwardCallNumber, new Dictionary<string, string>() { });

            resp.Add(dial);
            var xml = resp.ToString();
            return Content(xml, "text/xml");
        }

        /// <summary>
        /// Connects a caller to a number announcing that the Call is being connected.
        /// </summary>
        /// <returns>ActionResult.</returns>
        public ActionResult Connect()
        {
            Plivo.XML.Response resp = new Plivo.XML.Response();

            // Add Speak XML Tag
            resp.AddSpeak("Connecting your call..", new Dictionary<string, string>() { });

            // Add Dial XML Tag
            Plivo.XML.Dial dial = new Plivo.XML.Dial(new Dictionary<string, string>() { });
            dial.AddNumber(_forwardCallNumber, new Dictionary<string, string>() { });

            resp.Add(dial);
            Debug.WriteLine(resp.ToString());

            var xml = resp.ToString();
            return Content(xml, "text/xml");

        }

        /// <summary>
        /// Dials a number and then redirects to the DialStatus after dialing.
        /// </summary>
        /// <returns>ActionResult.</returns>
        public ActionResult Dial()
        {
            Plivo.XML.Response resp = new Plivo.XML.Response();

            // Generate Dial XML
            Plivo.XML.Dial dial = new Plivo.XML.Dial(new Dictionary<string, string>()
                {
                    {"action",_baseUrl + "/DialStatus"}, // Redirect to this URL after leaving Dial.
                    {"method","GET"} // Submit to action URL using GET or POST.
                });
            dial.AddNumber(_forwardCallNumber, new Dictionary<string, string>() { });

            resp.Add(dial);

            var xml = resp.ToString();
            return Content(xml, "text/xml");
        }

        /// <summary>
        /// Gets the status of a call
        /// </summary>
        /// <param name="DialStatus">The dial status.</param>
        /// <param name="DialALegUUID">The dial a leg UUID.</param>
        /// <param name="DialBLegUUID">The dial b leg UUID.</param>
        /// <returns>ActionResult.</returns>
        public ActionResult DialStatus(string DialStatus, string DialALegUUID, string DialBLegUUID)
        {
            // After completion of the call, Plivo will report back the status to the action URL in the Dial XML.
            String status = DialStatus;
            String aleg = DialALegUUID;
            String bleg = DialBLegUUID;

            Debug.WriteLine("Status : {0}, ALeg UUID : {1}, BLeg UUID : {2}", status, aleg, bleg);
            return Content("OK", "text/xml");
        }

        /// <summary>
        /// Speak to the caller using text to speech.
        /// </summary>
        /// <returns>ActionResult.</returns>
        public ActionResult Speak()
        {
            Plivo.XML.Response resp = new Plivo.XML.Response();

            // Add Speak XML Tag
            resp.AddSpeak("Hello, Welcome to Plivo", new Dictionary<string, string>() { });

            Debug.WriteLine(resp.ToString());

            var xml = resp.ToString();
            return Content(xml, "text/xml");
        }

        /// <summary>
        /// Speak to the caller using text to speech.
        /// </summary>
        /// <returns>ActionResult.</returns>
        /// <remarks>Example uses alternative languages for the text to speech.</remarks>
        public ActionResult Speech()
        {
            Plivo.XML.Response resp = new Plivo.XML.Response();

            // Add Speak XML Tag
            resp.AddSpeak("This is English", new Dictionary<string, string>()
                {
                    {"language", "en-GB"},
                    {"voice", "MAN"}
                });

            // Add Speak XML Tag
            resp.AddSpeak("Ce texte généré aléatoirement peut-être utilisé dans vos maquettes", new Dictionary<string, string>()
                {
                    {"language", "fr-FR"},
                    {"voice", "WOMAN"}
                });

            // Add Speak XML Tag
            resp.AddSpeak("Это случайно сгенерированный текст может быть использован в макете", new Dictionary<string, string>()
                {
                    {"language", "ru-RU"},
                    {"voice", "WOMAN"}
                });

            Debug.WriteLine(resp.ToString());

            var xml = resp.ToString();
            return Content(xml, "text/xml");

        }

        /// <summary>
        /// Get_s the ap i_ identifier.
        /// </summary>
        /// <returns>ActionResult.</returns>
        public ActionResult Get_API_ID()
        {
            RestAPI plivo = new RestAPI(_authID, _authToken);

            // API ID is returned for every API request. 
            // Request UUID is request id of the call. This ID is returned as soon as the call is fired irrespective of whether the call is answered or not

            IRestResponse<Call> resp = plivo.make_call(new Dictionary<string, string>()
            {
                { "from", "1111111111" }, // The phone number to which the call has to be placed
                { "to", "2222222222" }, // The phone number to be used as the caller Id
                { "answer_url", _baseUrl + "speak" }, // The URL invoked by Plivo when the outbound call is answered
                { "answer_method","GET"} // The method used to invoke the answer_url
            });

            //Prints the response
            return Content(resp.Content, "text/plain");
        }

        /// <summary>
        /// Get_s the uui d_ identifier.
        /// </summary>
        /// <returns>ActionResult.</returns>
        public ActionResult Get_UUID_ID()
        {
            RestAPI plivo = new RestAPI(_authID, _authToken);

            // Call UUID is the id of a live call. This ID is returned only after the call is answered.

            IRestResponse<LiveCall> resp = plivo.get_live_call(new Dictionary<string, string>()
            {
                { "call_uuid", "cd8fb3a0-b2a6-11e4-9a04-f5504e456438" } // The status of the call
            });

            //Prints the message details
            return this.Content(resp.Content, "text/plain");
        }

        /// <summary>
        /// Calls the hunting.
        /// </summary>
        /// <returns>ActionResult.</returns>
        public ActionResult CallHunting()
        {
            Plivo.XML.Response resp = new Plivo.XML.Response();

            // Generate Dial XML
            Plivo.XML.Dial dial = new Plivo.XML.Dial(new Dictionary<string, string>() { });
            dial.AddUser("sip:abcd1234@phone.plivo.com", new Dictionary<string, string>() { });
            dial.AddNumber("2222222222", new Dictionary<string, string>() { });
            dial.AddNumber("3333333333", new Dictionary<string, string>() { });
            resp.Add(dial);

            var xml = resp.ToString();
            return Content(xml, "text/xml");
        }

        /// <summary>
        /// Voicemails this instance.
        /// </summary>
        /// <returns>ActionResult.</returns>
        public ActionResult Voicemail()
        {
            Plivo.XML.Response resp = new Plivo.XML.Response();
            resp.AddSpeak("Please leave your message after the beep", new Dictionary<string, string>() { });
            resp.AddRecord(new Dictionary<string, string>()
                {
                    {"action",_baseUrl + "Plivo/SaveRecordUrl"}, // Submit the result of the record to this URL
                    {"method","GET"}, // HTTP method to submit the action URL
                    {"maxLength","30"}, // Maximum number of seconds to record 
                    {"transcriptionType","auto"}, // The type of transcription required
                    {"transcriptionUrl", _baseUrl + "Plivo/Transcription"}, // The URL where the transcription while be sent from Plivo
                    {"transcriptionMethod","GET"} // The method used to invoke transcriptionUrl
                });
            resp.AddSpeak("Recording not received", new Dictionary<string, string>() { });

            var xml = resp.ToString();
            return Content(xml, "text/xml");
        }

        /// <summary>
        /// Saves the record URL.
        /// </summary>
        /// <param name="RecordParameters">The record parameters.</param>
        /// <returns>ActionResult.</returns>
        public ActionResult SaveRecordUrl(RecordRequestParameters RecordParameters)
        {
            // Do something with the RecordUrl here such as send the link as an email or store it in a database etc.

            StringBuilder emailBody = new StringBuilder();
            emailBody.Append("Hi\r\n");
            emailBody.Append("Click the url below to listen to the voice mail.\r\n");
            emailBody.Append(String.Format("Recording Url: {0} \r\n", RecordParameters.RecordUrl));
            emailBody.Append(String.Format("Digits: {0} \r\n", RecordParameters.Digits));
            emailBody.Append(String.Format("Recording Duration: {0} \r\n", RecordParameters.RecordingDuration));
            emailBody.Append(String.Format("Recording Duration Ms: {0} \r\n", RecordParameters.RecordingDurationMs));
            emailBody.Append(String.Format("RecordingStartMs: {0} \r\n", RecordParameters.RecordingStartMs));
            emailBody.Append(String.Format("RecordingEndMs: {0} \r\n", RecordParameters.RecordingEndMs));
            emailBody.Append(String.Format("RecordingID: {0} \r\n", RecordParameters.RecordingID));

            Email.SendEmail(_emailTo, "Voicemail recording from Plivo Samples", emailBody.ToString());

            return Content("OK", "text/xml");
        }

        /// <summary>
        /// Transcriptions the specified transcription parameters.
        /// </summary>
        /// <param name="TranscriptionParameters">The transcription parameters.</param>
        /// <returns>ActionResult.</returns>
        public ActionResult Transcription(TranscriptionRequestParameters TranscriptionParameters)
        {
            // Do something with the transcription here such as send the text as an email, or store it in a database.

            StringBuilder emailBody = new StringBuilder();
            emailBody.Append("Hi\r\n");
            emailBody.Append("Here is the transcription of the voicemail\r\n");
            emailBody.Append(String.Format("Transcription: {0} \r\n", TranscriptionParameters.transcription));
            emailBody.Append(String.Format("Transcription charge: {0} \r\n", TranscriptionParameters.transcription_charge));
            emailBody.Append(String.Format("Duration: {0} \r\n", TranscriptionParameters.duration));
            emailBody.Append(String.Format("Call_uuid: {0} \r\n", TranscriptionParameters.call_uuid));
            emailBody.Append(String.Format("Transcription_rate: {0} \r\n", TranscriptionParameters.transcription_rate));
            emailBody.Append(String.Format("Recording_id: {0} \r\n", TranscriptionParameters.recording_id));

            Email.SendEmail(_emailTo, "Voicemail transcription from Plivo Samples", emailBody.ToString());

            return Content("OK", "text/xml");
        }
    }
}