using System;
using System.Security.Cryptography.X509Certificates;

namespace System.Net
{
	// Token: 0x020002F8 RID: 760
	internal class DefaultCertificatePolicy : ICertificatePolicy
	{
		// Token: 0x06001A05 RID: 6661 RVA: 0x0004803C File Offset: 0x0004623C
		public bool CheckValidationResult(ServicePoint point, X509Certificate certificate, WebRequest request, int certificateProblem)
		{
			return ServicePointManager.ServerCertificateValidationCallback != null || certificateProblem == -2146762495 || certificateProblem == 0;
		}
	}
}
