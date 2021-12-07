using System;
using System.Security.Principal;
using System.Text;

namespace System.Net
{
	/// <summary>Provides access to the request and response objects used by the <see cref="T:System.Net.HttpListener" /> class. This class cannot be inherited.</summary>
	// Token: 0x02000315 RID: 789
	public sealed class HttpListenerContext
	{
		// Token: 0x06001B6C RID: 7020 RVA: 0x0004E4F8 File Offset: 0x0004C6F8
		internal HttpListenerContext(HttpConnection cnc)
		{
			this.cnc = cnc;
			this.request = new HttpListenerRequest(this);
			this.response = new HttpListenerResponse(this);
		}

		// Token: 0x170006A4 RID: 1700
		// (get) Token: 0x06001B6D RID: 7021 RVA: 0x0004E538 File Offset: 0x0004C738
		// (set) Token: 0x06001B6E RID: 7022 RVA: 0x0004E540 File Offset: 0x0004C740
		internal int ErrorStatus
		{
			get
			{
				return this.err_status;
			}
			set
			{
				this.err_status = value;
			}
		}

		// Token: 0x170006A5 RID: 1701
		// (get) Token: 0x06001B6F RID: 7023 RVA: 0x0004E54C File Offset: 0x0004C74C
		// (set) Token: 0x06001B70 RID: 7024 RVA: 0x0004E554 File Offset: 0x0004C754
		internal string ErrorMessage
		{
			get
			{
				return this.error;
			}
			set
			{
				this.error = value;
			}
		}

		// Token: 0x170006A6 RID: 1702
		// (get) Token: 0x06001B71 RID: 7025 RVA: 0x0004E560 File Offset: 0x0004C760
		internal bool HaveError
		{
			get
			{
				return this.error != null;
			}
		}

		// Token: 0x170006A7 RID: 1703
		// (get) Token: 0x06001B72 RID: 7026 RVA: 0x0004E570 File Offset: 0x0004C770
		internal HttpConnection Connection
		{
			get
			{
				return this.cnc;
			}
		}

		/// <summary>Gets the <see cref="T:System.Net.HttpListenerRequest" /> that represents a client's request for a resource.</summary>
		/// <returns>An <see cref="T:System.Net.HttpListenerRequest" /> object that represents the client request.</returns>
		// Token: 0x170006A8 RID: 1704
		// (get) Token: 0x06001B73 RID: 7027 RVA: 0x0004E578 File Offset: 0x0004C778
		public HttpListenerRequest Request
		{
			get
			{
				return this.request;
			}
		}

		/// <summary>Gets the <see cref="T:System.Net.HttpListenerResponse" /> object that will be sent to the client in response to the client's request. </summary>
		/// <returns>An <see cref="T:System.Net.HttpListenerResponse" /> object used to send a response back to the client.</returns>
		// Token: 0x170006A9 RID: 1705
		// (get) Token: 0x06001B74 RID: 7028 RVA: 0x0004E580 File Offset: 0x0004C780
		public HttpListenerResponse Response
		{
			get
			{
				return this.response;
			}
		}

		/// <summary>Gets an object used to obtain identity, authentication information, and security roles for the client whose request is represented by this <see cref="T:System.Net.HttpListenerContext" /> object. </summary>
		/// <returns>An <see cref="T:System.Security.Principal.IPrincipal" /> object that describes the client, or null if the <see cref="T:System.Net.HttpListener" /> that supplied this <see cref="T:System.Net.HttpListenerContext" /> does not require authentication.</returns>
		// Token: 0x170006AA RID: 1706
		// (get) Token: 0x06001B75 RID: 7029 RVA: 0x0004E588 File Offset: 0x0004C788
		public IPrincipal User
		{
			get
			{
				return this.user;
			}
		}

		// Token: 0x06001B76 RID: 7030 RVA: 0x0004E590 File Offset: 0x0004C790
		internal void ParseAuthentication(AuthenticationSchemes expectedSchemes)
		{
			if (expectedSchemes == AuthenticationSchemes.Anonymous)
			{
				return;
			}
			string text = this.request.Headers["Authorization"];
			if (text == null || text.Length < 2)
			{
				return;
			}
			string[] array = text.Split(new char[]
			{
				' '
			}, 2);
			if (string.Compare(array[0], "basic", true) == 0)
			{
				this.user = this.ParseBasicAuthentication(array[1]);
			}
		}

		// Token: 0x06001B77 RID: 7031 RVA: 0x0004E608 File Offset: 0x0004C808
		internal IPrincipal ParseBasicAuthentication(string authData)
		{
			IPrincipal result;
			try
			{
				string text = Encoding.Default.GetString(Convert.FromBase64String(authData));
				int num = text.IndexOf(':');
				string password = text.Substring(num + 1);
				text = text.Substring(0, num);
				num = text.IndexOf('\\');
				string username;
				if (num > 0)
				{
					username = text.Substring(num);
				}
				else
				{
					username = text;
				}
				HttpListenerBasicIdentity identity = new HttpListenerBasicIdentity(username, password);
				result = new GenericPrincipal(identity, new string[0]);
			}
			catch (Exception)
			{
				result = null;
			}
			return result;
		}

		// Token: 0x04001101 RID: 4353
		private HttpListenerRequest request;

		// Token: 0x04001102 RID: 4354
		private HttpListenerResponse response;

		// Token: 0x04001103 RID: 4355
		private IPrincipal user;

		// Token: 0x04001104 RID: 4356
		private HttpConnection cnc;

		// Token: 0x04001105 RID: 4357
		private string error;

		// Token: 0x04001106 RID: 4358
		private int err_status = 400;

		// Token: 0x04001107 RID: 4359
		internal HttpListener Listener;
	}
}
