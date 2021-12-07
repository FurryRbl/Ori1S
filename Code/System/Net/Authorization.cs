using System;

namespace System.Net
{
	/// <summary>Contains an authentication message for an Internet server.</summary>
	// Token: 0x020002BB RID: 699
	public class Authorization
	{
		/// <summary>Creates a new instance of the <see cref="T:System.Net.Authorization" /> class with the specified authorization message.</summary>
		/// <param name="token">The encrypted authorization message expected by the server. </param>
		// Token: 0x06001833 RID: 6195 RVA: 0x00042A28 File Offset: 0x00040C28
		public Authorization(string token) : this(token, true)
		{
		}

		/// <summary>Creates a new instance of the <see cref="T:System.Net.Authorization" /> class with the specified authorization message and completion status.</summary>
		/// <param name="token">The encrypted authorization message expected by the server. </param>
		/// <param name="finished">The completion status of the authorization attempt. true if the authorization attempt is complete; otherwise, false. </param>
		// Token: 0x06001834 RID: 6196 RVA: 0x00042A34 File Offset: 0x00040C34
		public Authorization(string token, bool complete) : this(token, complete, null)
		{
		}

		/// <summary>Creates a new instance of the <see cref="T:System.Net.Authorization" /> class with the specified authorization message, completion status, and connection group identifier.</summary>
		/// <param name="token">The encrypted authorization message expected by the server. </param>
		/// <param name="finished">The completion status of the authorization attempt. true if the authorization attempt is complete; otherwise, false. </param>
		/// <param name="connectionGroupId">A unique identifier that can be used to create private client-server connections that are bound only to this authentication scheme. </param>
		// Token: 0x06001835 RID: 6197 RVA: 0x00042A40 File Offset: 0x00040C40
		public Authorization(string token, bool complete, string connectionGroupId)
		{
			this.token = token;
			this.complete = complete;
			this.connectionGroupId = connectionGroupId;
		}

		/// <summary>Gets the message returned to the server in response to an authentication challenge.</summary>
		/// <returns>The message that will be returned to the server in response to an authentication challenge.</returns>
		// Token: 0x170005AD RID: 1453
		// (get) Token: 0x06001836 RID: 6198 RVA: 0x00042A60 File Offset: 0x00040C60
		public string Message
		{
			get
			{
				return this.token;
			}
		}

		/// <summary>Gets the completion status of the authorization.</summary>
		/// <returns>true if the authentication process is complete; otherwise, false.</returns>
		// Token: 0x170005AE RID: 1454
		// (get) Token: 0x06001837 RID: 6199 RVA: 0x00042A68 File Offset: 0x00040C68
		public bool Complete
		{
			get
			{
				return this.complete;
			}
		}

		/// <summary>Gets a unique identifier for user-specific connections.</summary>
		/// <returns>A unique string that associates a connection with an authenticating entity.</returns>
		// Token: 0x170005AF RID: 1455
		// (get) Token: 0x06001838 RID: 6200 RVA: 0x00042A70 File Offset: 0x00040C70
		public string ConnectionGroupId
		{
			get
			{
				return this.connectionGroupId;
			}
		}

		/// <summary>Gets or sets the prefix for Uniform Resource Identifiers (URIs) that can be authenticated with the <see cref="P:System.Net.Authorization.Message" /> property.</summary>
		/// <returns>An array of strings that contains URI prefixes.</returns>
		// Token: 0x170005B0 RID: 1456
		// (get) Token: 0x06001839 RID: 6201 RVA: 0x00042A78 File Offset: 0x00040C78
		// (set) Token: 0x0600183A RID: 6202 RVA: 0x00042A80 File Offset: 0x00040C80
		public string[] ProtectionRealm
		{
			get
			{
				return this.protectionRealm;
			}
			set
			{
				this.protectionRealm = value;
			}
		}

		// Token: 0x170005B1 RID: 1457
		// (get) Token: 0x0600183B RID: 6203 RVA: 0x00042A8C File Offset: 0x00040C8C
		// (set) Token: 0x0600183C RID: 6204 RVA: 0x00042A94 File Offset: 0x00040C94
		internal IAuthenticationModule Module
		{
			get
			{
				return this.module;
			}
			set
			{
				this.module = value;
			}
		}

		// Token: 0x0600183D RID: 6205 RVA: 0x00042AA0 File Offset: 0x00040CA0
		private static Exception GetMustImplement()
		{
			return new NotImplementedException();
		}

		/// <summary>Gets or sets a <see cref="T:System.Boolean" /> value that indicates whether mutual authentication occurred.</summary>
		/// <returns>true if both client and server were authenticated; otherwise, false.</returns>
		// Token: 0x170005B2 RID: 1458
		// (get) Token: 0x0600183E RID: 6206 RVA: 0x00042AA8 File Offset: 0x00040CA8
		// (set) Token: 0x0600183F RID: 6207 RVA: 0x00042AB0 File Offset: 0x00040CB0
		[MonoTODO]
		public bool MutuallyAuthenticated
		{
			get
			{
				throw Authorization.GetMustImplement();
			}
			set
			{
				throw Authorization.GetMustImplement();
			}
		}

		// Token: 0x04000F74 RID: 3956
		private string token;

		// Token: 0x04000F75 RID: 3957
		private bool complete;

		// Token: 0x04000F76 RID: 3958
		private string connectionGroupId;

		// Token: 0x04000F77 RID: 3959
		private string[] protectionRealm;

		// Token: 0x04000F78 RID: 3960
		private IAuthenticationModule module;
	}
}
