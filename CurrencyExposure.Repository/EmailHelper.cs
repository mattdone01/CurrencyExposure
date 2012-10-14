﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using SendGrid;
using SendGrid.Transport;

namespace CurrencyExposure.Repository
{
	public class EmailHelper
	{
		private readonly string _username;
        private readonly string _password;
        private readonly string _from;

		public EmailHelper(string username, string password, string from)
		{
			_username = username;
			_password = password;
			_from = from;
		}

		public void SendEmail(string recipient, string html, string subject)
		{
			SendEmail(new List<string> { recipient }, html, subject);
		}

		public void SendEmail(IEnumerable<string> recipients, string html, string subject)
		{
			Task.Factory.StartNew(() => SendEmailAsync(recipients, html, subject));
		}

		private void SendEmailAsync(IEnumerable<string> recipients, string html, string subject)
		{
			//create a new message object
			var message = Mail.GetInstance();

			//set the message recipients
			foreach (string recipient in recipients)
			{
				message.AddTo(recipient);
			}

			//set the sender
			message.From = new MailAddress(_from);

			//set the message body
			message.Html = html;

			//set the message subject
			message.Subject = subject;

			//create an instance of the SMTP transport mechanism
			var transportInstance = SMTP.GetInstance(new NetworkCredential(_username, _password));

			//send the mail
			transportInstance.Deliver(message);
		}
	}
}
