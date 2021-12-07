using System;
using System.Net;
using Mono.Security.Protocol.Ntlm;

namespace Mono.Http
{
	// Token: 0x0200001F RID: 31
	internal class NtlmSession
	{
		// Token: 0x06000125 RID: 293 RVA: 0x0000A070 File Offset: 0x00008270
		public System.Net.Authorization Authenticate(string challenge, System.Net.WebRequest webRequest, System.Net.ICredentials credentials)
		{
			System.Net.HttpWebRequest httpWebRequest = webRequest as System.Net.HttpWebRequest;
			if (httpWebRequest == null)
			{
				return null;
			}
			System.Net.NetworkCredential credential = credentials.GetCredential(httpWebRequest.RequestUri, "NTLM");
			if (credential == null)
			{
				return null;
			}
			string userName = credential.UserName;
			string text = credential.Domain;
			string text2 = credential.Password;
			if (userName == null || userName == string.Empty)
			{
				return null;
			}
			text = ((text == null || text.Length <= 0) ? httpWebRequest.Headers["Host"] : text);
			bool complete = false;
			if (this.message == null)
			{
				this.message = new Type1Message
				{
					Domain = text
				};
			}
			else if (this.message.Type == 1)
			{
				if (challenge == null)
				{
					this.message = null;
					return null;
				}
				Type2Message type2Message = new Type2Message(Convert.FromBase64String(challenge));
				if (text2 == null)
				{
					text2 = string.Empty;
				}
				this.message = new Type3Message
				{
					Domain = text,
					Username = userName,
					Challenge = type2Message.Nonce,
					Password = text2
				};
				complete = true;
			}
			else if (challenge == null || challenge == string.Empty)
			{
				this.message = new Type1Message
				{
					Domain = text
				};
			}
			else
			{
				complete = true;
			}
			string token = "NTLM " + Convert.ToBase64String(this.message.GetBytes());
			return new System.Net.Authorization(token, complete);
		}

		// Token: 0x0400005D RID: 93
		private MessageBase message;
	}
}
