using System;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Security.Permissions;

namespace System.Security.Policy
{
	/// <summary>Provides the Authenticode X.509v3 digital signature of a code assembly as evidence for policy evaluation. This class cannot be inherited.</summary>
	// Token: 0x0200064F RID: 1615
	[ComVisible(true)]
	[Serializable]
	public sealed class Publisher : IBuiltInEvidence, IIdentityPermissionFactory
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Policy.Publisher" /> class with the Authenticode X.509v3 certificate containing the publisher's public key.</summary>
		/// <param name="cert">An <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" /> that contains the software publisher's public key. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="cert" /> parameter is null. </exception>
		// Token: 0x06003D71 RID: 15729 RVA: 0x000D3E3C File Offset: 0x000D203C
		public Publisher(X509Certificate cert)
		{
			if (cert == null)
			{
				throw new ArgumentNullException("cert");
			}
			if (cert.GetHashCode() == 0)
			{
				throw new ArgumentException("cert");
			}
			this.m_cert = cert;
		}

		// Token: 0x06003D72 RID: 15730 RVA: 0x000D3E80 File Offset: 0x000D2080
		int IBuiltInEvidence.GetRequiredSize(bool verbose)
		{
			return ((!verbose) ? 1 : 3) + this.m_cert.GetRawCertData().Length;
		}

		// Token: 0x06003D73 RID: 15731 RVA: 0x000D3EA0 File Offset: 0x000D20A0
		[MonoTODO("IBuiltInEvidence")]
		int IBuiltInEvidence.InitFromBuffer(char[] buffer, int position)
		{
			return 0;
		}

		// Token: 0x06003D74 RID: 15732 RVA: 0x000D3EA4 File Offset: 0x000D20A4
		[MonoTODO("IBuiltInEvidence")]
		int IBuiltInEvidence.OutputToBuffer(char[] buffer, int position, bool verbose)
		{
			return 0;
		}

		/// <summary>Gets the publisher's Authenticode X.509v3 certificate.</summary>
		/// <returns>The publisher's <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate" />.</returns>
		// Token: 0x17000B9E RID: 2974
		// (get) Token: 0x06003D75 RID: 15733 RVA: 0x000D3EA8 File Offset: 0x000D20A8
		public X509Certificate Certificate
		{
			get
			{
				if (this.m_cert.GetHashCode() == 0)
				{
					throw new ArgumentException("m_cert");
				}
				return this.m_cert;
			}
		}

		/// <summary>Creates an equivalent copy of the <see cref="T:System.Security.Policy.Publisher" />.</summary>
		/// <returns>A new, identical copy of the <see cref="T:System.Security.Policy.Publisher" />.</returns>
		// Token: 0x06003D76 RID: 15734 RVA: 0x000D3ECC File Offset: 0x000D20CC
		public object Copy()
		{
			return new Publisher(this.m_cert);
		}

		/// <summary>Creates an identity permission that corresponds to the current instance of the <see cref="T:System.Security.Policy.Publisher" /> class.</summary>
		/// <returns>A <see cref="T:System.Security.Permissions.PublisherIdentityPermission" /> for the specified <see cref="T:System.Security.Policy.Publisher" />.</returns>
		/// <param name="evidence">The <see cref="T:System.Security.Policy.Evidence" /> from which to construct the identity permission. </param>
		// Token: 0x06003D77 RID: 15735 RVA: 0x000D3EDC File Offset: 0x000D20DC
		public IPermission CreateIdentityPermission(Evidence evidence)
		{
			return new PublisherIdentityPermission(this.m_cert);
		}

		/// <summary>Compares the current <see cref="T:System.Security.Policy.Publisher" /> to the specified object for equivalence.</summary>
		/// <returns>true if the two instances of the <see cref="T:System.Security.Policy.Publisher" /> class are equal; otherwise, false.</returns>
		/// <param name="o">The <see cref="T:System.Security.Policy.Publisher" /> to test for equivalence with the current object. </param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="o" /> parameter is not a <see cref="T:System.Security.Policy.Publisher" /> object. </exception>
		// Token: 0x06003D78 RID: 15736 RVA: 0x000D3EEC File Offset: 0x000D20EC
		public override bool Equals(object o)
		{
			Publisher publisher = o as Publisher;
			if (publisher == null)
			{
				throw new ArgumentException("o", Locale.GetText("not a Publisher instance."));
			}
			return this.m_cert.Equals(publisher.Certificate);
		}

		/// <summary>Gets the hash code of the current <see cref="P:System.Security.Policy.Publisher.Certificate" />.</summary>
		/// <returns>The hash code of the current <see cref="P:System.Security.Policy.Publisher.Certificate" />.</returns>
		// Token: 0x06003D79 RID: 15737 RVA: 0x000D3F2C File Offset: 0x000D212C
		public override int GetHashCode()
		{
			return this.m_cert.GetHashCode();
		}

		/// <summary>Returns a string representation of the current <see cref="T:System.Security.Policy.Publisher" />.</summary>
		/// <returns>A representation of the current <see cref="T:System.Security.Policy.Publisher" />.</returns>
		// Token: 0x06003D7A RID: 15738 RVA: 0x000D3F3C File Offset: 0x000D213C
		public override string ToString()
		{
			SecurityElement securityElement = new SecurityElement("System.Security.Policy.Publisher");
			securityElement.AddAttribute("version", "1");
			SecurityElement securityElement2 = new SecurityElement("X509v3Certificate");
			string rawCertDataString = this.m_cert.GetRawCertDataString();
			if (rawCertDataString != null)
			{
				securityElement2.Text = rawCertDataString;
			}
			securityElement.AddChild(securityElement2);
			return securityElement.ToString();
		}

		// Token: 0x04001A91 RID: 6801
		private X509Certificate m_cert;
	}
}
