using UnityEngine;
using System.Collections;
using System;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

public class mono_gmail : MonoBehaviour {

	private string recipient;
	private string sender;

	void OnMouseDown ()
	{
		MailMessage mail = new MailMessage();

		mail.From = new MailAddress("QubZ@gmail.com");
		mail.To.Add(recipient);
		mail.Subject = sender + " has sent you a game referal: QubZ";
		mail.Body = "Your self proclaimed friend of yours, " + sender + ", has sent you a game referal. QubZ";

		SmtpClient smtpServer = new SmtpClient("smtp.gmail.com");
		smtpServer.Port = 587;
		smtpServer.Credentials = new System.Net.NetworkCredential("youraddress@gmail.com", "yourpassword") as ICredentialsByHost;
		smtpServer.EnableSsl = true;
		ServicePointManager.ServerCertificateValidationCallback = 
			delegate(object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) 
		{ return true; };
		smtpServer.Send(mail);
		Debug.Log("success");

	}
}