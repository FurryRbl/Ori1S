using System;

namespace System.Security.Cryptography.X509Certificates
{
	/// <summary>Represents the chain policy to be applied when building an X509 certificate chain. This class cannot be inherited.</summary>
	// Token: 0x02000449 RID: 1097
	public sealed class X509ChainPolicy
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509ChainPolicy" /> class. </summary>
		// Token: 0x0600279E RID: 10142 RVA: 0x0007CB40 File Offset: 0x0007AD40
		public X509ChainPolicy()
		{
			this.Reset();
		}

		/// <summary>Gets a collection of object identifiers (OIDs) specifying which application policies or enhanced key usages (EKUs) the certificate supports.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.OidCollection" />  object.</returns>
		// Token: 0x17000B1D RID: 2845
		// (get) Token: 0x0600279F RID: 10143 RVA: 0x0007CB50 File Offset: 0x0007AD50
		public OidCollection ApplicationPolicy
		{
			get
			{
				return this.apps;
			}
		}

		/// <summary>Gets a collection of object identifiers (OIDs) specifying which certificate policies the certificate supports.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.OidCollection" /> object.</returns>
		// Token: 0x17000B1E RID: 2846
		// (get) Token: 0x060027A0 RID: 10144 RVA: 0x0007CB58 File Offset: 0x0007AD58
		public OidCollection CertificatePolicy
		{
			get
			{
				return this.cert;
			}
		}

		/// <summary>Represents an additional collection of certificates that can be searched by the chaining engine when validating a certificate chain.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2Collection" /> object.</returns>
		// Token: 0x17000B1F RID: 2847
		// (get) Token: 0x060027A1 RID: 10145 RVA: 0x0007CB60 File Offset: 0x0007AD60
		public X509Certificate2Collection ExtraStore
		{
			get
			{
				return this.store;
			}
		}

		/// <summary>Gets or sets values for X509 revocation flags.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.X509Certificates.X509RevocationFlag" /> object.</returns>
		/// <exception cref="T:System.ArgumentException">The <see cref="T:System.Security.Cryptography.X509Certificates.X509RevocationFlag" /> value supplied is not a valid flag. </exception>
		// Token: 0x17000B20 RID: 2848
		// (get) Token: 0x060027A2 RID: 10146 RVA: 0x0007CB68 File Offset: 0x0007AD68
		// (set) Token: 0x060027A3 RID: 10147 RVA: 0x0007CB70 File Offset: 0x0007AD70
		public X509RevocationFlag RevocationFlag
		{
			get
			{
				return this.rflag;
			}
			set
			{
				if (value < X509RevocationFlag.EndCertificateOnly || value > X509RevocationFlag.ExcludeRoot)
				{
					throw new ArgumentException("RevocationFlag");
				}
				this.rflag = value;
			}
		}

		/// <summary>Gets or sets values for X509 certificate revocation mode.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.X509Certificates.X509RevocationMode" /> object.</returns>
		/// <exception cref="T:System.ArgumentException">The <see cref="T:System.Security.Cryptography.X509Certificates.X509RevocationMode" /> value supplied is not a valid flag. </exception>
		// Token: 0x17000B21 RID: 2849
		// (get) Token: 0x060027A4 RID: 10148 RVA: 0x0007CBA0 File Offset: 0x0007ADA0
		// (set) Token: 0x060027A5 RID: 10149 RVA: 0x0007CBA8 File Offset: 0x0007ADA8
		public X509RevocationMode RevocationMode
		{
			get
			{
				return this.mode;
			}
			set
			{
				if (value < X509RevocationMode.NoCheck || value > X509RevocationMode.Offline)
				{
					throw new ArgumentException("RevocationMode");
				}
				this.mode = value;
			}
		}

		/// <summary>Gets the time span that elapsed during online revocation verification or downloading the certificate revocation list (CRL).</summary>
		/// <returns>A <see cref="T:System.TimeSpan" /> object.</returns>
		// Token: 0x17000B22 RID: 2850
		// (get) Token: 0x060027A6 RID: 10150 RVA: 0x0007CBD8 File Offset: 0x0007ADD8
		// (set) Token: 0x060027A7 RID: 10151 RVA: 0x0007CBE0 File Offset: 0x0007ADE0
		public TimeSpan UrlRetrievalTimeout
		{
			get
			{
				return this.timeout;
			}
			set
			{
				this.timeout = value;
			}
		}

		/// <summary>Gets verification flags for the certificate.</summary>
		/// <returns>A value from the <see cref="T:System.Security.Cryptography.X509Certificates.X509VerificationFlags" /> enumeration.</returns>
		/// <exception cref="T:System.ArgumentException">The <see cref="T:System.Security.Cryptography.X509Certificates.X509VerificationFlags" /> value supplied is not a valid flag. <see cref="F:System.Security.Cryptography.X509Certificates.X509VerificationFlags.NoFlag" /> is the default value. </exception>
		// Token: 0x17000B23 RID: 2851
		// (get) Token: 0x060027A8 RID: 10152 RVA: 0x0007CBEC File Offset: 0x0007ADEC
		// (set) Token: 0x060027A9 RID: 10153 RVA: 0x0007CBF4 File Offset: 0x0007ADF4
		public X509VerificationFlags VerificationFlags
		{
			get
			{
				return this.vflags;
			}
			set
			{
				if ((value | X509VerificationFlags.AllFlags) != X509VerificationFlags.AllFlags)
				{
					throw new ArgumentException("VerificationFlags");
				}
				this.vflags = value;
			}
		}

		/// <summary>The time that the certificate was verified expressed in local time.</summary>
		/// <returns>A <see cref="T:System.DateTime" /> object.</returns>
		// Token: 0x17000B24 RID: 2852
		// (get) Token: 0x060027AA RID: 10154 RVA: 0x0007CC1C File Offset: 0x0007AE1C
		// (set) Token: 0x060027AB RID: 10155 RVA: 0x0007CC24 File Offset: 0x0007AE24
		public DateTime VerificationTime
		{
			get
			{
				return this.vtime;
			}
			set
			{
				this.vtime = value;
			}
		}

		/// <summary>Resets the <see cref="T:System.Security.Cryptography.X509Certificates.X509ChainPolicy" /> members to their default values.</summary>
		// Token: 0x060027AC RID: 10156 RVA: 0x0007CC30 File Offset: 0x0007AE30
		public void Reset()
		{
			this.apps = new OidCollection();
			this.cert = new OidCollection();
			this.store = new X509Certificate2Collection();
			this.rflag = X509RevocationFlag.ExcludeRoot;
			this.mode = X509RevocationMode.Online;
			this.timeout = TimeSpan.Zero;
			this.vflags = X509VerificationFlags.NoFlag;
			this.vtime = DateTime.Now;
		}

		// Token: 0x0400183D RID: 6205
		private OidCollection apps;

		// Token: 0x0400183E RID: 6206
		private OidCollection cert;

		// Token: 0x0400183F RID: 6207
		private X509Certificate2Collection store;

		// Token: 0x04001840 RID: 6208
		private X509RevocationFlag rflag;

		// Token: 0x04001841 RID: 6209
		private X509RevocationMode mode;

		// Token: 0x04001842 RID: 6210
		private TimeSpan timeout;

		// Token: 0x04001843 RID: 6211
		private X509VerificationFlags vflags;

		// Token: 0x04001844 RID: 6212
		private DateTime vtime;
	}
}
