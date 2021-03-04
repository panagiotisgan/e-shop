using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;

namespace eShop.DataAccess.Services
{
    public class GmailSendEmail
    {

        //public async static Task<string[]> AccessTokenCreator()
        //{
        //    string[] results = new string[2];
        //    var credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
        //        new ClientSecrets
        //        {
        //            ClientId = "962810249223-1rgpa899224eql57gflkt2c8cqfdvqpv.apps.googleusercontent.com",
        //            ClientSecret = "ZvVlePyc2ikc8MzOhPmDyE_Z"
        //        },
        //        new[] { "email", "profile", "https://mail.google.com/" },
        //        "user",
        //        CancellationToken.None
        //        );

        //    var jwtPayload = GoogleJsonWebSignature.ValidateAsync(credential.Token.IdToken).Result;
        //    var accessToken = credential.Token.AccessToken;
        //    var username = jwtPayload.Email;
        //    results.Append(accessToken);
        //    results.Append(username);

        //    return results;
        //}


        //public static void SendMailWithXOAUTH2(string userEmail, string accessToken,string recepientEmail)
        //{
        //    try
        //    {
        //        SmtpServer oServer = new SmtpServer("smtp.gmail.com");

        //        oServer.ConnectType = SmtpConnectType.ConnectSSLAuto;
        //        oServer.Port = 587;
        //        oServer.AuthType = SmtpAuthType.XOAUTH2;
        //        oServer.User = userEmail;
        //        oServer.Password = accessToken;

        //        SmtpMail oMail = new SmtpMail("Try it");

        //        oMail.From = userEmail;
        //        oMail.To = recepientEmail;
        //        oMail.Subject = "Account Activation";
        //        oMail.TextBody = "Your account created succesfully! Please activate it by click in the following link.";

        //        SmtpClient oSmtp = new SmtpClient();
        //        oSmtp.SendMail(oServer, oMail);
        //    }
        //    catch(Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //}

        #region setupSMTPforGmail
        //public static string SetupEmail(string recipientEmail, string subject, string body)
        //{
        //    const string pass = "4425panos";
        //    const string providerEmail = "panagiotisgan@gmail.com";
        //    var client = new SmtpClient("smtp.gmail.com", 587)
        //    {
        //        Credentials = new NetworkCredential(providerEmail, pass),
        //        EnableSsl = true
        //    };

        //    try
        //    {
        //        client.Send(providerEmail, recipientEmail, subject, body);
        //    } 
        //    catch(Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }

        //    return "Email Sending Succesfully";
        //}
        #endregion
    }
}
