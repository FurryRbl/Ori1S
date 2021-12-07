using System;

namespace System.Net
{
	/// <summary>Provides the base authentication interface for Web client authentication modules.</summary>
	// Token: 0x02000325 RID: 805
	public interface IAuthenticationModule
	{
		/// <summary>Returns an instance of the <see cref="T:System.Net.Authorization" /> class in respose to an authentication challenge from a server.</summary>
		/// <returns>An <see cref="T:System.Net.Authorization" /> instance containing the authorization message for the request, or null if the challenge cannot be handled.</returns>
		/// <param name="challenge">The authentication challenge sent by the server. </param>
		/// <param name="request">The <see cref="T:System.Net.WebRequest" /> instance associated with the challenge. </param>
		/// <param name="credentials">The credentials associated with the challenge. </param>
		// Token: 0x06001C9B RID: 7323
		Authorization Authenticate(string challenge, WebRequest request, ICredentials credentials);

		/// <summary>Returns an instance of the <see cref="T:System.Net.Authorization" /> class for an authentication request to a server.</summary>
		/// <returns>An <see cref="T:System.Net.Authorization" /> instance containing the authorization message for the request.</returns>
		/// <param name="request">The <see cref="T:System.Net.WebRequest" /> instance associated with the authentication request. </param>
		/// <param name="credentials">The credentials associated with the authentication request. </param>
		// Token: 0x06001C9C RID: 7324
		Authorization PreAuthenticate(WebRequest request, ICredentials credentials);

		/// <summary>Gets the authentication type provided by this authentication module.</summary>
		/// <returns>A string indicating the authentication type provided by this authentication module.</returns>
		// Token: 0x17000720 RID: 1824
		// (get) Token: 0x06001C9D RID: 7325
		string AuthenticationType { get; }

		/// <summary>Gets a value indicating whether the authentication module supports preauthentication.</summary>
		/// <returns>true if the authorization module supports preauthentication; otherwise false.</returns>
		// Token: 0x17000721 RID: 1825
		// (get) Token: 0x06001C9E RID: 7326
		bool CanPreAuthenticate { get; }
	}
}
