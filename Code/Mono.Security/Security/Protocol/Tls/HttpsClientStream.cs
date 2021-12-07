using System;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

namespace Mono.Security.Protocol.Tls
{
	// Token: 0x0200008D RID: 141
	internal class HttpsClientStream : SslClientStream
	{
		// Token: 0x0600051F RID: 1311 RVA: 0x0001E214 File Offset: 0x0001C414
		public HttpsClientStream(Stream stream, X509CertificateCollection clientCertificates, HttpWebRequest request, byte[] buffer) : base(stream, request.Address.Host, false, (SecurityProtocolType)ServicePointManager.SecurityProtocol, clientCertificates)
		{
			this._request = request;
			this._status = 0;
			if (buffer != null)
			{
				base.InputBuffer.Write(buffer, 0, buffer.Length);
			}
			base.CheckCertRevocationStatus = ServicePointManager.CheckCertificateRevocationList;
			base.ClientCertSelection += ((X509CertificateCollection clientCerts, X509Certificate serverCertificate, string targetHost, X509CertificateCollection serverRequestedCertificates) => (clientCerts != null && clientCerts.Count != 0) ? clientCerts[0] : null);
			base.PrivateKeySelection += delegate(X509Certificate certificate, string targetHost)
			{
				X509Certificate2 x509Certificate = certificate as X509Certificate2;
				return (x509Certificate != null) ? x509Certificate.PrivateKey : null;
			};
		}

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x06000520 RID: 1312 RVA: 0x0001E2B4 File Offset: 0x0001C4B4
		public bool TrustFailure
		{
			get
			{
				int status = this._status;
				return status == -2146762487 || status == -2146762486;
			}
		}

		// Token: 0x06000521 RID: 1313 RVA: 0x0001E2E8 File Offset: 0x0001C4E8
		internal override bool RaiseServerCertificateValidation(X509Certificate certificate, int[] certificateErrors)
		{
			bool flag = certificateErrors.Length > 0;
			this._status = ((!flag) ? 0 : certificateErrors[0]);
			if (ServicePointManager.CertificatePolicy != null)
			{
				ServicePoint servicePoint = this._request.ServicePoint;
				if (!ServicePointManager.CertificatePolicy.CheckValidationResult(servicePoint, certificate, this._request, this._status))
				{
					return false;
				}
				flag = true;
			}
			if (this.HaveRemoteValidation2Callback)
			{
				return flag;
			}
			RemoteCertificateValidationCallback serverCertificateValidationCallback = ServicePointManager.ServerCertificateValidationCallback;
			if (serverCertificateValidationCallback != null)
			{
				SslPolicyErrors sslPolicyErrors = SslPolicyErrors.None;
				foreach (int num in certificateErrors)
				{
					if (num == -2146762490)
					{
						sslPolicyErrors |= SslPolicyErrors.RemoteCertificateNotAvailable;
					}
					else if (num == -2146762481)
					{
						sslPolicyErrors |= SslPolicyErrors.RemoteCertificateNameMismatch;
					}
					else
					{
						sslPolicyErrors |= SslPolicyErrors.RemoteCertificateChainErrors;
					}
				}
				X509Certificate2 certificate2 = new X509Certificate2(certificate.GetRawCertData());
				X509Chain x509Chain = new X509Chain();
				if (!x509Chain.Build(certificate2))
				{
					sslPolicyErrors |= SslPolicyErrors.RemoteCertificateChainErrors;
				}
				return serverCertificateValidationCallback(this._request, certificate2, x509Chain, sslPolicyErrors);
			}
			return flag;
		}

		// Token: 0x04000294 RID: 660
		private HttpWebRequest _request;

		// Token: 0x04000295 RID: 661
		private int _status;
	}
}
