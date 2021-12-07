using System;
using System.Security.Principal;

namespace System.Net
{
	/// <summary>Holds the user name and password from a basic authentication request.</summary>
	// Token: 0x02000314 RID: 788
	public class HttpListenerBasicIdentity : GenericIdentity
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.HttpListenerBasicIdentity" /> class using the specified user name and password.</summary>
		/// <param name="username">The user name.</param>
		/// <param name="password">The password.</param>
		// Token: 0x06001B6A RID: 7018 RVA: 0x0004E4D8 File Offset: 0x0004C6D8
		public HttpListenerBasicIdentity(string username, string password) : base(username, "Basic")
		{
			this.password = password;
		}

		/// <summary>Indicates the password from a basic authentication attempt.</summary>
		/// <returns>A <see cref="T:System.String" /> that holds the password.</returns>
		// Token: 0x170006A3 RID: 1699
		// (get) Token: 0x06001B6B RID: 7019 RVA: 0x0004E4F0 File Offset: 0x0004C6F0
		public virtual string Password
		{
			get
			{
				return this.password;
			}
		}

		// Token: 0x04001100 RID: 4352
		private string password;
	}
}
