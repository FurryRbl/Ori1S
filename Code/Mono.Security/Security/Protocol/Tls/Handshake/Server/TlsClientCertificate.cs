using System;
using System.Collections;
using System.Security.Cryptography.X509Certificates;
using Mono.Security.X509;
using Mono.Security.X509.Extensions;

namespace Mono.Security.Protocol.Tls.Handshake.Server
{
	// Token: 0x020000B2 RID: 178
	internal class TlsClientCertificate : HandshakeMessage
	{
		// Token: 0x06000699 RID: 1689 RVA: 0x00024CC0 File Offset: 0x00022EC0
		public TlsClientCertificate(Context context, byte[] buffer) : base(context, HandshakeType.Certificate, buffer)
		{
		}

		// Token: 0x0600069A RID: 1690 RVA: 0x00024CCC File Offset: 0x00022ECC
		public override void Update()
		{
			foreach (Mono.Security.X509.X509Certificate x509Certificate in this.clientCertificates)
			{
				base.Context.ClientSettings.Certificates.Add(new System.Security.Cryptography.X509Certificates.X509Certificate(x509Certificate.RawData));
			}
		}

		// Token: 0x0600069B RID: 1691 RVA: 0x00024D50 File Offset: 0x00022F50
		protected override void ProcessAsSsl3()
		{
			this.ProcessAsTls1();
		}

		// Token: 0x0600069C RID: 1692 RVA: 0x00024D58 File Offset: 0x00022F58
		protected override void ProcessAsTls1()
		{
			int num = 0;
			int i = base.ReadInt24();
			this.clientCertificates = new Mono.Security.X509.X509CertificateCollection();
			while (i > num)
			{
				int num2 = base.ReadInt24();
				num += num2 + 3;
				byte[] data = base.ReadBytes(num2);
				this.clientCertificates.Add(new Mono.Security.X509.X509Certificate(data));
			}
			if (this.clientCertificates.Count > 0)
			{
				this.validateCertificates(this.clientCertificates);
			}
			else if ((base.Context as ServerContext).ClientCertificateRequired)
			{
				throw new TlsException(AlertDescription.NoCertificate);
			}
		}

		// Token: 0x0600069D RID: 1693 RVA: 0x00024DEC File Offset: 0x00022FEC
		private bool checkCertificateUsage(Mono.Security.X509.X509Certificate cert)
		{
			ServerContext serverContext = (ServerContext)base.Context;
			if (cert.Version < 3)
			{
				return true;
			}
			KeyUsages usage = KeyUsages.none;
			switch (serverContext.Negotiating.Cipher.ExchangeAlgorithmType)
			{
			case ExchangeAlgorithmType.DiffieHellman:
				usage = KeyUsages.keyAgreement;
				break;
			case ExchangeAlgorithmType.Fortezza:
				return false;
			case ExchangeAlgorithmType.RsaKeyX:
			case ExchangeAlgorithmType.RsaSign:
				usage = KeyUsages.digitalSignature;
				break;
			}
			KeyUsageExtension keyUsageExtension = null;
			ExtendedKeyUsageExtension extendedKeyUsageExtension = null;
			Mono.Security.X509.X509Extension x509Extension = cert.Extensions["2.5.29.15"];
			if (x509Extension != null)
			{
				keyUsageExtension = new KeyUsageExtension(x509Extension);
			}
			x509Extension = cert.Extensions["2.5.29.37"];
			if (x509Extension != null)
			{
				extendedKeyUsageExtension = new ExtendedKeyUsageExtension(x509Extension);
			}
			if (keyUsageExtension != null && extendedKeyUsageExtension != null)
			{
				return keyUsageExtension.Support(usage) && extendedKeyUsageExtension.KeyPurpose.Contains("1.3.6.1.5.5.7.3.2");
			}
			if (keyUsageExtension != null)
			{
				return keyUsageExtension.Support(usage);
			}
			if (extendedKeyUsageExtension != null)
			{
				return extendedKeyUsageExtension.KeyPurpose.Contains("1.3.6.1.5.5.7.3.2");
			}
			x509Extension = cert.Extensions["2.16.840.1.113730.1.1"];
			if (x509Extension != null)
			{
				NetscapeCertTypeExtension netscapeCertTypeExtension = new NetscapeCertTypeExtension(x509Extension);
				return netscapeCertTypeExtension.Support(NetscapeCertTypeExtension.CertTypes.SslClient);
			}
			return false;
		}

		// Token: 0x0600069E RID: 1694 RVA: 0x00024F24 File Offset: 0x00023124
		private void validateCertificates(Mono.Security.X509.X509CertificateCollection certificates)
		{
			ServerContext serverContext = (ServerContext)base.Context;
			AlertDescription description = AlertDescription.BadCertificate;
			System.Security.Cryptography.X509Certificates.X509Certificate x509Certificate = null;
			int[] certificateErrors = null;
			if (certificates.Count > 0)
			{
				Mono.Security.X509.X509Certificate x509Certificate2 = certificates[0];
				ArrayList arrayList = new ArrayList();
				if (!this.checkCertificateUsage(x509Certificate2))
				{
					arrayList.Add(-2146762490);
				}
				Mono.Security.X509.X509Chain x509Chain;
				if (certificates.Count > 1)
				{
					Mono.Security.X509.X509CertificateCollection x509CertificateCollection = new Mono.Security.X509.X509CertificateCollection(certificates);
					x509CertificateCollection.Remove(x509Certificate2);
					x509Chain = new Mono.Security.X509.X509Chain(x509CertificateCollection);
				}
				else
				{
					x509Chain = new Mono.Security.X509.X509Chain();
				}
				bool flag = false;
				try
				{
					flag = x509Chain.Build(x509Certificate2);
				}
				catch (Exception)
				{
					flag = false;
				}
				if (!flag)
				{
					Mono.Security.X509.X509ChainStatusFlags status = x509Chain.Status;
					if (status != Mono.Security.X509.X509ChainStatusFlags.NotTimeValid)
					{
						if (status != Mono.Security.X509.X509ChainStatusFlags.NotTimeNested)
						{
							if (status != Mono.Security.X509.X509ChainStatusFlags.NotSignatureValid)
							{
								if (status != Mono.Security.X509.X509ChainStatusFlags.UntrustedRoot)
								{
									if (status != Mono.Security.X509.X509ChainStatusFlags.InvalidBasicConstraints)
									{
										if (status != Mono.Security.X509.X509ChainStatusFlags.PartialChain)
										{
											description = AlertDescription.CertificateUnknown;
											arrayList.Add((int)x509Chain.Status);
										}
										else
										{
											description = AlertDescription.UnknownCA;
											arrayList.Add(-2146762486);
										}
									}
									else
									{
										arrayList.Add(-2146869223);
									}
								}
								else
								{
									description = AlertDescription.UnknownCA;
									arrayList.Add(-2146762487);
								}
							}
							else
							{
								arrayList.Add(-2146869232);
							}
						}
						else
						{
							arrayList.Add(-2146762494);
						}
					}
					else
					{
						description = AlertDescription.CertificateExpired;
						arrayList.Add(-2146762495);
					}
				}
				x509Certificate = new System.Security.Cryptography.X509Certificates.X509Certificate(x509Certificate2.RawData);
				certificateErrors = (int[])arrayList.ToArray(typeof(int));
			}
			else
			{
				certificateErrors = new int[0];
			}
			System.Security.Cryptography.X509Certificates.X509CertificateCollection x509CertificateCollection2 = new System.Security.Cryptography.X509Certificates.X509CertificateCollection();
			foreach (Mono.Security.X509.X509Certificate x509Certificate3 in certificates)
			{
				x509CertificateCollection2.Add(new System.Security.Cryptography.X509Certificates.X509Certificate(x509Certificate3.RawData));
			}
			if (!serverContext.SslStream.RaiseClientCertificateValidation(x509Certificate, certificateErrors))
			{
				throw new TlsException(description, "Invalid certificate received from client.");
			}
			base.Context.ClientSettings.ClientCertificate = x509Certificate;
		}

		// Token: 0x04000329 RID: 809
		private Mono.Security.X509.X509CertificateCollection clientCertificates;
	}
}
