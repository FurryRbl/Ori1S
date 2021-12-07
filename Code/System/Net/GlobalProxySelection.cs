using System;

namespace System.Net
{
	/// <summary>Contains a global default proxy instance for all HTTP requests.</summary>
	// Token: 0x0200030F RID: 783
	[Obsolete("Use WebRequest.DefaultProxy instead")]
	public class GlobalProxySelection
	{
		/// <summary>Gets or sets the global HTTP proxy.</summary>
		/// <returns>An <see cref="T:System.Net.IWebProxy" /> that every call to <see cref="M:System.Net.HttpWebRequest.GetResponse" /> uses.</returns>
		/// <exception cref="T:System.ArgumentNullException">The value specified for a set operation was null. </exception>
		/// <exception cref="T:System.Security.SecurityException">The caller does not have permission for the requested operation. </exception>
		// Token: 0x1700069C RID: 1692
		// (get) Token: 0x06001B4D RID: 6989 RVA: 0x0004DA80 File Offset: 0x0004BC80
		// (set) Token: 0x06001B4E RID: 6990 RVA: 0x0004DA88 File Offset: 0x0004BC88
		public static IWebProxy Select
		{
			get
			{
				return WebRequest.DefaultWebProxy;
			}
			set
			{
				WebRequest.DefaultWebProxy = value;
			}
		}

		/// <summary>Returns an empty proxy instance.</summary>
		/// <returns>An <see cref="T:System.Net.IWebProxy" /> that contains no information.</returns>
		// Token: 0x06001B4F RID: 6991 RVA: 0x0004DA90 File Offset: 0x0004BC90
		public static IWebProxy GetEmptyWebProxy()
		{
			return new GlobalProxySelection.EmptyWebProxy();
		}

		// Token: 0x02000310 RID: 784
		internal class EmptyWebProxy : IWebProxy
		{
			// Token: 0x06001B50 RID: 6992 RVA: 0x0004DA98 File Offset: 0x0004BC98
			internal EmptyWebProxy()
			{
			}

			// Token: 0x1700069D RID: 1693
			// (get) Token: 0x06001B51 RID: 6993 RVA: 0x0004DAA0 File Offset: 0x0004BCA0
			// (set) Token: 0x06001B52 RID: 6994 RVA: 0x0004DAA8 File Offset: 0x0004BCA8
			public ICredentials Credentials
			{
				get
				{
					return this.credentials;
				}
				set
				{
					this.credentials = value;
				}
			}

			// Token: 0x06001B53 RID: 6995 RVA: 0x0004DAB4 File Offset: 0x0004BCB4
			public System.Uri GetProxy(System.Uri destination)
			{
				return destination;
			}

			// Token: 0x06001B54 RID: 6996 RVA: 0x0004DAB8 File Offset: 0x0004BCB8
			public bool IsBypassed(System.Uri host)
			{
				return true;
			}

			// Token: 0x040010E5 RID: 4325
			private ICredentials credentials;
		}
	}
}
