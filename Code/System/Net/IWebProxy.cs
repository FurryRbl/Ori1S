using System;

namespace System.Net
{
	/// <summary>Provides the base interface for implementation of proxy access for the <see cref="T:System.Net.WebRequest" /> class.</summary>
	// Token: 0x0200032E RID: 814
	public interface IWebProxy
	{
		/// <summary>The credentials to submit to the proxy server for authentication.</summary>
		/// <returns>An <see cref="T:System.Net.ICredentials" /> instance that contains the credentials that are needed to authenticate a request to the proxy server.</returns>
		// Token: 0x17000734 RID: 1844
		// (get) Token: 0x06001CF0 RID: 7408
		// (set) Token: 0x06001CF1 RID: 7409
		ICredentials Credentials { get; set; }

		/// <summary>Returns the URI of a proxy.</summary>
		/// <returns>A <see cref="T:System.Uri" /> instance that contains the URI of the proxy used to contact <paramref name="destination" />.</returns>
		/// <param name="destination">A <see cref="T:System.Uri" /> that specifies the requested Internet resource. </param>
		// Token: 0x06001CF2 RID: 7410
		System.Uri GetProxy(System.Uri destination);

		/// <summary>Indicates that the proxy should not be used for the specified host.</summary>
		/// <returns>true if the proxy server should not be used for <paramref name="host" />; otherwise, false.</returns>
		/// <param name="host">The <see cref="T:System.Uri" /> of the host to check for proxy use. </param>
		// Token: 0x06001CF3 RID: 7411
		bool IsBypassed(System.Uri host);
	}
}
