using System;
using System.Security.Cryptography.X509Certificates;

namespace System.Net
{
	/// <summary>Validates a server certificate.</summary>
	// Token: 0x02000326 RID: 806
	public interface ICertificatePolicy
	{
		/// <summary>Validates a server certificate.</summary>
		/// <returns>true if the certificate should be honored; otherwise, false.</returns>
		/// <param name="srvPoint">The <see cref="T:System.Net.ServicePoint" /> that will use the certificate. </param>
		/// <param name="certificate">The certificate to validate. </param>
		/// <param name="request">The request that received the certificate. </param>
		/// <param name="certificateProblem">The problem that was encountered when using the certificate. </param>
		// Token: 0x06001C9F RID: 7327
		bool CheckValidationResult(ServicePoint srvPoint, X509Certificate certificate, WebRequest request, int certificateProblem);
	}
}
