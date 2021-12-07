using System;

namespace System.Net.Mail
{
	/// <summary>Specifies the outcome of sending e-mail by using the <see cref="T:System.Net.Mail.SmtpClient" /> class.</summary>
	// Token: 0x0200034D RID: 845
	public enum SmtpStatusCode
	{
		/// <summary>The commands were sent in the incorrect sequence.</summary>
		// Token: 0x04001295 RID: 4757
		BadCommandSequence = 503,
		/// <summary>The specified user is not local, but the receiving SMTP service accepted the message and attempted to deliver it. This status code is defined in RFC 1123, which is available at http://www.ietf.org.</summary>
		// Token: 0x04001296 RID: 4758
		CannotVerifyUserWillAttemptDelivery = 252,
		/// <summary>The client was not authenticated or is not allowed to send mail using the specified SMTP host.</summary>
		// Token: 0x04001297 RID: 4759
		ClientNotPermitted = 454,
		/// <summary>The SMTP service does not implement the specified command.</summary>
		// Token: 0x04001298 RID: 4760
		CommandNotImplemented = 502,
		/// <summary>The SMTP service does not implement the specified command parameter.</summary>
		// Token: 0x04001299 RID: 4761
		CommandParameterNotImplemented = 504,
		/// <summary>The SMTP service does not recognize the specified command.</summary>
		// Token: 0x0400129A RID: 4762
		CommandUnrecognized = 500,
		/// <summary>The message is too large to be stored in the destination mailbox.</summary>
		// Token: 0x0400129B RID: 4763
		ExceededStorageAllocation = 552,
		/// <summary>The transaction could not occur. You receive this error when the specified SMTP host cannot be found.</summary>
		// Token: 0x0400129C RID: 4764
		GeneralFailure = -1,
		/// <summary>A Help message was returned by the service.</summary>
		// Token: 0x0400129D RID: 4765
		HelpMessage = 214,
		/// <summary>The SMTP service does not have sufficient storage to complete the request.</summary>
		// Token: 0x0400129E RID: 4766
		InsufficientStorage = 452,
		/// <summary>The SMTP service cannot complete the request. This error can occur if the client's IP address cannot be resolved (that is, a reverse lookup failed). You can also receive this error if the client domain has been identified as an open relay or source for unsolicited e-mail (spam). For details, see RFC 2505, which is available at http://www.ietf.org.</summary>
		// Token: 0x0400129F RID: 4767
		LocalErrorInProcessing = 451,
		/// <summary>The destination mailbox is in use.</summary>
		// Token: 0x040012A0 RID: 4768
		MailboxBusy = 450,
		/// <summary>The syntax used to specify the destination mailbox is incorrect.</summary>
		// Token: 0x040012A1 RID: 4769
		MailboxNameNotAllowed = 553,
		/// <summary>The destination mailbox was not found or could not be accessed.</summary>
		// Token: 0x040012A2 RID: 4770
		MailboxUnavailable = 550,
		/// <summary>The email was successfully sent to the SMTP service.</summary>
		// Token: 0x040012A3 RID: 4771
		Ok = 250,
		/// <summary>The SMTP service is closing the transmission channel.</summary>
		// Token: 0x040012A4 RID: 4772
		ServiceClosingTransmissionChannel = 221,
		/// <summary>The SMTP service is not available; the server is closing the transmission channel.</summary>
		// Token: 0x040012A5 RID: 4773
		ServiceNotAvailable = 421,
		/// <summary>The SMTP service is ready.</summary>
		// Token: 0x040012A6 RID: 4774
		ServiceReady = 220,
		/// <summary>The SMTP service is ready to receive the e-mail content.</summary>
		// Token: 0x040012A7 RID: 4775
		StartMailInput = 354,
		/// <summary>The syntax used to specify a command or parameter is incorrect.</summary>
		// Token: 0x040012A8 RID: 4776
		SyntaxError = 501,
		/// <summary>A system status or system Help reply.</summary>
		// Token: 0x040012A9 RID: 4777
		SystemStatus = 211,
		/// <summary>The transaction failed.</summary>
		// Token: 0x040012AA RID: 4778
		TransactionFailed = 554,
		/// <summary>The user mailbox is not located on the receiving server. You should resend using the supplied address information.</summary>
		// Token: 0x040012AB RID: 4779
		UserNotLocalTryAlternatePath = 551,
		/// <summary>The user mailbox is not located on the receiving server; the server forwards the e-mail.</summary>
		// Token: 0x040012AC RID: 4780
		UserNotLocalWillForward = 251,
		/// <summary>The SMTP server is configured to accept only TLS connections, and the SMTP client is attempting to connect by using a non-TLS connection. The solution is for the user to set EnableSsl=true on the SMTP Client.</summary>
		// Token: 0x040012AD RID: 4781
		MustIssueStartTlsFirst = 530
	}
}
