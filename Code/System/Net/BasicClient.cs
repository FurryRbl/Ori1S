using System;

namespace System.Net
{
	// Token: 0x020002BC RID: 700
	internal class BasicClient : IAuthenticationModule
	{
		// Token: 0x06001841 RID: 6209 RVA: 0x00042AC0 File Offset: 0x00040CC0
		public Authorization Authenticate(string challenge, WebRequest webRequest, ICredentials credentials)
		{
			if (credentials == null || challenge == null)
			{
				return null;
			}
			string text = challenge.Trim();
			if (text.ToLower().IndexOf("basic") == -1)
			{
				return null;
			}
			return BasicClient.InternalAuthenticate(webRequest, credentials);
		}

		// Token: 0x06001842 RID: 6210 RVA: 0x00042B04 File Offset: 0x00040D04
		private static byte[] GetBytes(string str)
		{
			int i = str.Length;
			byte[] array = new byte[i];
			for (i--; i >= 0; i--)
			{
				array[i] = (byte)str[i];
			}
			return array;
		}

		// Token: 0x06001843 RID: 6211 RVA: 0x00042B40 File Offset: 0x00040D40
		private static Authorization InternalAuthenticate(WebRequest webRequest, ICredentials credentials)
		{
			HttpWebRequest httpWebRequest = webRequest as HttpWebRequest;
			if (httpWebRequest == null || credentials == null)
			{
				return null;
			}
			NetworkCredential credential = credentials.GetCredential(httpWebRequest.AuthUri, "basic");
			if (credential == null)
			{
				return null;
			}
			string userName = credential.UserName;
			if (userName == null || userName == string.Empty)
			{
				return null;
			}
			string password = credential.Password;
			string domain = credential.Domain;
			byte[] bytes;
			if (domain == null || domain == string.Empty || domain.Trim() == string.Empty)
			{
				bytes = BasicClient.GetBytes(userName + ":" + password);
			}
			else
			{
				bytes = BasicClient.GetBytes(string.Concat(new string[]
				{
					domain,
					"\\",
					userName,
					":",
					password
				}));
			}
			string token = "Basic " + Convert.ToBase64String(bytes);
			return new Authorization(token);
		}

		// Token: 0x06001844 RID: 6212 RVA: 0x00042C3C File Offset: 0x00040E3C
		public Authorization PreAuthenticate(WebRequest webRequest, ICredentials credentials)
		{
			return BasicClient.InternalAuthenticate(webRequest, credentials);
		}

		// Token: 0x170005B3 RID: 1459
		// (get) Token: 0x06001845 RID: 6213 RVA: 0x00042C48 File Offset: 0x00040E48
		public string AuthenticationType
		{
			get
			{
				return "Basic";
			}
		}

		// Token: 0x170005B4 RID: 1460
		// (get) Token: 0x06001846 RID: 6214 RVA: 0x00042C50 File Offset: 0x00040E50
		public bool CanPreAuthenticate
		{
			get
			{
				return true;
			}
		}
	}
}
