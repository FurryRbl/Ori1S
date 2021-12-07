using System;

namespace System.Net
{
	/// <summary>Provides the base authentication interface for retrieving credentials for Web client authentication.</summary>
	// Token: 0x02000327 RID: 807
	public interface ICredentials
	{
		/// <summary>Returns a <see cref="T:System.Net.NetworkCredential" /> object that is associated with the specified URI, and authentication type.</summary>
		/// <returns>The <see cref="T:System.Net.NetworkCredential" /> that is associated with the specified URI and authentication type, or, if no credentials are available, null.</returns>
		/// <param name="uri">The <see cref="T:System.Uri" /> that the client is providing authentication for. </param>
		/// <param name="authType">The type of authentication, as defined in the <see cref="P:System.Net.IAuthenticationModule.AuthenticationType" /> property. </param>
		// Token: 0x06001CA0 RID: 7328
		NetworkCredential GetCredential(System.Uri uri, string authType);
	}
}
