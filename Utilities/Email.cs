// ***********************************************************************
// Assembly         : Utilities
// Author           : Paul
// Created          : 12-17-2015
//
// Last Modified By : Paul
// Last Modified On : 12-17-2015
// ***********************************************************************
// <copyright file="Email.cs" company="">
//     Copyright ©  2015
// </copyright>
// <summary></summary>
// ***********************************************************************
using System;
using System.Net.Mail;

namespace Plivo_MVC_Samples.Utilities
{
    /// <summary>
    /// Class Email.
    /// </summary>
    static public class Email
    {
        /// <summary>
        /// General Utility to Send an Email
        /// </summary>
        /// <param name="toAddress">To address.</param>
        /// <param name="subject">The subject.</param>
        /// <param name="body">The body.</param>
        static public void SendEmail(string toAddress, string subject, string body)
        {
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient();
                mail.To.Add(toAddress);
                mail.Subject = subject;
                mail.Body = body;

                //Attachment attachment;
                //attachment = new Attachment("your attachment file");
                //mail.Attachments.Add(attachment);

                SmtpServer.Send(mail);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
