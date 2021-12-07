using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Mono.Security.X509;
using Mono.Security.X509.Extensions;

namespace System.Security.Cryptography.X509Certificates
{
	/// <summary>Represents a chain-building engine for <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> certificates.</summary>
	// Token: 0x02000445 RID: 1093
	public class X509Chain
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509Chain" /> class.</summary>
		// Token: 0x06002760 RID: 10080 RVA: 0x0007B30C File Offset: 0x0007950C
		public X509Chain() : this(false)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509Chain" /> class specifying a value that indicates whether the machine context should be used.</summary>
		/// <param name="useMachineContext">true to use the machine context; false to use the current user context. </param>
		// Token: 0x06002761 RID: 10081 RVA: 0x0007B318 File Offset: 0x00079518
		public X509Chain(bool useMachineContext)
		{
			this.location = ((!useMachineContext) ? StoreLocation.CurrentUser : StoreLocation.LocalMachine);
			this.elements = new X509ChainElementCollection();
			this.policy = new X509ChainPolicy();
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.Cryptography.X509Certificates.X509Chain" /> class using an <see cref="T:System.IntPtr" /> handle to an X.509 chain.</summary>
		/// <param name="chainContext">An <see cref="T:System.IntPtr" /> handle to an X.509 chain.</param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="chainContext" /> parameter is null.</exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The <paramref name="chainContext" /> parameter points to an invalid context.</exception>
		// Token: 0x06002762 RID: 10082 RVA: 0x0007B34C File Offset: 0x0007954C
		[MonoTODO("Mono's X509Chain is fully managed. All handles are invalid.")]
		public X509Chain(IntPtr chainContext)
		{
			throw new NotSupportedException();
		}

		/// <summary>Gets a handle to an X.509 chain.</summary>
		/// <returns>An <see cref="T:System.IntPtr" /> handle to an X.509 chain.</returns>
		// Token: 0x17000B0C RID: 2828
		// (get) Token: 0x06002764 RID: 10084 RVA: 0x0007B36C File Offset: 0x0007956C
		[MonoTODO("Mono's X509Chain is fully managed. Always returns IntPtr.Zero.")]
		public IntPtr ChainContext
		{
			get
			{
				return IntPtr.Zero;
			}
		}

		/// <summary>Gets a collection of <see cref="T:System.Security.Cryptography.X509Certificates.X509ChainElement" /> objects.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.X509Certificates.X509ChainElementCollection" /> object.</returns>
		// Token: 0x17000B0D RID: 2829
		// (get) Token: 0x06002765 RID: 10085 RVA: 0x0007B374 File Offset: 0x00079574
		public X509ChainElementCollection ChainElements
		{
			get
			{
				return this.elements;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Security.Cryptography.X509Certificates.X509ChainPolicy" /> to use when building an X.509 certificate chain.</summary>
		/// <returns>The <see cref="T:System.Security.Cryptography.X509Certificates.X509ChainPolicy" /> object associated with this X.509 chain.</returns>
		/// <exception cref="T:System.ArgumentNullException">The value being set for this property is null.</exception>
		// Token: 0x17000B0E RID: 2830
		// (get) Token: 0x06002766 RID: 10086 RVA: 0x0007B37C File Offset: 0x0007957C
		// (set) Token: 0x06002767 RID: 10087 RVA: 0x0007B384 File Offset: 0x00079584
		public X509ChainPolicy ChainPolicy
		{
			get
			{
				return this.policy;
			}
			set
			{
				this.policy = value;
			}
		}

		/// <summary>Gets the status of each element in an <see cref="T:System.Security.Cryptography.X509Certificates.X509Chain" /> object.</summary>
		/// <returns>An array of <see cref="T:System.Security.Cryptography.X509Certificates.X509ChainStatus" /> objects.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000B0F RID: 2831
		// (get) Token: 0x06002768 RID: 10088 RVA: 0x0007B390 File Offset: 0x00079590
		public X509ChainStatus[] ChainStatus
		{
			get
			{
				if (this.status == null)
				{
					return X509Chain.Empty;
				}
				return this.status;
			}
		}

		/// <summary>Builds an X.509 chain using the policy specified in <see cref="T:System.Security.Cryptography.X509Certificates.X509ChainPolicy" />.</summary>
		/// <returns>true if the X.509 certificate is valid; otherwise, false.</returns>
		/// <param name="certificate">An <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> object.</param>
		/// <exception cref="T:System.ArgumentException">The <paramref name="certificate" /> is not a valid certificate or is null. </exception>
		/// <exception cref="T:System.Security.Cryptography.CryptographicException">The <paramref name="certificate" /> is unreadable. </exception>
		// Token: 0x06002769 RID: 10089 RVA: 0x0007B3AC File Offset: 0x000795AC
		[MonoTODO("Not totally RFC3280 compliant, but neither is MS implementation...")]
		public bool Build(X509Certificate2 certificate)
		{
			if (certificate == null)
			{
				throw new ArgumentException("certificate");
			}
			this.Reset();
			X509ChainStatusFlags x509ChainStatusFlags;
			try
			{
				x509ChainStatusFlags = this.BuildChainFrom(certificate);
				this.ValidateChain(x509ChainStatusFlags);
			}
			catch (CryptographicException innerException)
			{
				throw new ArgumentException("certificate", innerException);
			}
			X509ChainStatusFlags x509ChainStatusFlags2 = X509ChainStatusFlags.NoError;
			ArrayList arrayList = new ArrayList();
			foreach (X509ChainElement x509ChainElement in this.elements)
			{
				foreach (X509ChainStatus x509ChainStatus in x509ChainElement.ChainElementStatus)
				{
					if ((x509ChainStatusFlags2 & x509ChainStatus.Status) != x509ChainStatus.Status)
					{
						arrayList.Add(x509ChainStatus);
						x509ChainStatusFlags2 |= x509ChainStatus.Status;
					}
				}
			}
			if (x509ChainStatusFlags != X509ChainStatusFlags.NoError)
			{
				arrayList.Insert(0, new X509ChainStatus(x509ChainStatusFlags));
			}
			this.status = (X509ChainStatus[])arrayList.ToArray(typeof(X509ChainStatus));
			if (this.status.Length == 0 || this.ChainPolicy.VerificationFlags == X509VerificationFlags.AllFlags)
			{
				return true;
			}
			bool flag = true;
			foreach (X509ChainStatus x509ChainStatus2 in this.status)
			{
				X509ChainStatusFlags x509ChainStatusFlags3 = x509ChainStatus2.Status;
				if (x509ChainStatusFlags3 != X509ChainStatusFlags.NotTimeValid)
				{
					if (x509ChainStatusFlags3 != X509ChainStatusFlags.NotTimeNested)
					{
						if (x509ChainStatusFlags3 != X509ChainStatusFlags.UntrustedRoot)
						{
							if (x509ChainStatusFlags3 != X509ChainStatusFlags.InvalidExtension)
							{
								if (x509ChainStatusFlags3 != X509ChainStatusFlags.InvalidPolicyConstraints)
								{
									if (x509ChainStatusFlags3 == X509ChainStatusFlags.InvalidBasicConstraints)
									{
										flag &= ((this.ChainPolicy.VerificationFlags & X509VerificationFlags.IgnoreInvalidBasicConstraints) != X509VerificationFlags.NoFlag);
										goto IL_316;
									}
									if (x509ChainStatusFlags3 == X509ChainStatusFlags.InvalidNameConstraints || x509ChainStatusFlags3 == X509ChainStatusFlags.HasNotSupportedNameConstraint || x509ChainStatusFlags3 == X509ChainStatusFlags.HasNotPermittedNameConstraint || x509ChainStatusFlags3 == X509ChainStatusFlags.HasExcludedNameConstraint)
									{
										flag &= ((this.ChainPolicy.VerificationFlags & X509VerificationFlags.IgnoreInvalidName) != X509VerificationFlags.NoFlag);
										goto IL_316;
									}
									if (x509ChainStatusFlags3 == X509ChainStatusFlags.PartialChain)
									{
										goto IL_1FC;
									}
									if (x509ChainStatusFlags3 == X509ChainStatusFlags.CtlNotTimeValid)
									{
										flag &= ((this.ChainPolicy.VerificationFlags & X509VerificationFlags.IgnoreCtlNotTimeValid) != X509VerificationFlags.NoFlag);
										goto IL_316;
									}
									if (x509ChainStatusFlags3 == X509ChainStatusFlags.CtlNotSignatureValid)
									{
										goto IL_316;
									}
									if (x509ChainStatusFlags3 == X509ChainStatusFlags.CtlNotValidForUsage)
									{
										flag &= ((this.ChainPolicy.VerificationFlags & X509VerificationFlags.IgnoreWrongUsage) != X509VerificationFlags.NoFlag);
										goto IL_316;
									}
									if (x509ChainStatusFlags3 != X509ChainStatusFlags.NoIssuanceChainPolicy)
									{
										flag = false;
										goto IL_316;
									}
								}
								flag &= ((this.ChainPolicy.VerificationFlags & X509VerificationFlags.IgnoreInvalidPolicy) != X509VerificationFlags.NoFlag);
								goto IL_316;
							}
							flag &= ((this.ChainPolicy.VerificationFlags & X509VerificationFlags.IgnoreWrongUsage) != X509VerificationFlags.NoFlag);
							goto IL_316;
						}
						IL_1FC:
						flag &= ((this.ChainPolicy.VerificationFlags & X509VerificationFlags.AllowUnknownCertificateAuthority) != X509VerificationFlags.NoFlag);
					}
					else
					{
						flag &= ((this.ChainPolicy.VerificationFlags & X509VerificationFlags.IgnoreNotTimeNested) != X509VerificationFlags.NoFlag);
					}
				}
				else
				{
					flag &= ((this.ChainPolicy.VerificationFlags & X509VerificationFlags.IgnoreNotTimeValid) != X509VerificationFlags.NoFlag);
				}
				IL_316:
				if (!flag)
				{
					return false;
				}
			}
			return true;
		}

		/// <summary>Clears the current <see cref="T:System.Security.Cryptography.X509Certificates.X509Chain" /> object.</summary>
		// Token: 0x0600276A RID: 10090 RVA: 0x0007B708 File Offset: 0x00079908
		public void Reset()
		{
			if (this.status != null && this.status.Length != 0)
			{
				this.status = null;
			}
			if (this.elements.Count > 0)
			{
				this.elements.Clear();
			}
			if (this.roots != null)
			{
				this.roots.Close();
				this.roots = null;
			}
			if (this.cas != null)
			{
				this.cas.Close();
				this.cas = null;
			}
			this.collection = null;
			this.bce_restriction = null;
			this.working_public_key = null;
		}

		/// <summary>Creates an <see cref="T:System.Security.Cryptography.X509Certificates.X509Chain" /> object after querying for the mapping defined in the CryptoConfig file, and maps the chain to that mapping.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.X509Certificates.X509Chain" /> object.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x0600276B RID: 10091 RVA: 0x0007B7A0 File Offset: 0x000799A0
		public static X509Chain Create()
		{
			return (X509Chain)CryptoConfig.CreateFromName("X509Chain");
		}

		// Token: 0x17000B10 RID: 2832
		// (get) Token: 0x0600276C RID: 10092 RVA: 0x0007B7B4 File Offset: 0x000799B4
		private X509Store Roots
		{
			get
			{
				if (this.roots == null)
				{
					this.roots = new X509Store(StoreName.Root, this.location);
					this.roots.Open(OpenFlags.ReadOnly);
				}
				return this.roots;
			}
		}

		// Token: 0x17000B11 RID: 2833
		// (get) Token: 0x0600276D RID: 10093 RVA: 0x0007B7E8 File Offset: 0x000799E8
		private X509Store CertificateAuthorities
		{
			get
			{
				if (this.cas == null)
				{
					this.cas = new X509Store(StoreName.CertificateAuthority, this.location);
					this.cas.Open(OpenFlags.ReadOnly);
				}
				return this.cas;
			}
		}

		// Token: 0x17000B12 RID: 2834
		// (get) Token: 0x0600276E RID: 10094 RVA: 0x0007B81C File Offset: 0x00079A1C
		private X509Certificate2Collection CertificateCollection
		{
			get
			{
				if (this.collection == null)
				{
					this.collection = new X509Certificate2Collection(this.ChainPolicy.ExtraStore);
					if (this.Roots.Certificates.Count > 0)
					{
						this.collection.AddRange(this.Roots.Certificates);
					}
					if (this.CertificateAuthorities.Certificates.Count > 0)
					{
						this.collection.AddRange(this.CertificateAuthorities.Certificates);
					}
				}
				return this.collection;
			}
		}

		// Token: 0x0600276F RID: 10095 RVA: 0x0007B8A8 File Offset: 0x00079AA8
		private X509ChainStatusFlags BuildChainFrom(X509Certificate2 certificate)
		{
			this.elements.Add(certificate);
			while (!this.IsChainComplete(certificate))
			{
				certificate = this.FindParent(certificate);
				if (certificate == null)
				{
					return X509ChainStatusFlags.PartialChain;
				}
				if (this.elements.Contains(certificate))
				{
					return X509ChainStatusFlags.Cyclic;
				}
				this.elements.Add(certificate);
			}
			if (!this.Roots.Certificates.Contains(certificate))
			{
				this.elements[this.elements.Count - 1].StatusFlags |= X509ChainStatusFlags.UntrustedRoot;
			}
			return X509ChainStatusFlags.NoError;
		}

		// Token: 0x06002770 RID: 10096 RVA: 0x0007B948 File Offset: 0x00079B48
		private X509Certificate2 SelectBestFromCollection(X509Certificate2 child, X509Certificate2Collection c)
		{
			int count = c.Count;
			if (count == 0)
			{
				return null;
			}
			if (count == 1)
			{
				return c[0];
			}
			X509Certificate2Collection x509Certificate2Collection = c.Find(X509FindType.FindByTimeValid, this.ChainPolicy.VerificationTime, false);
			int count2 = x509Certificate2Collection.Count;
			if (count2 != 0)
			{
				if (count2 == 1)
				{
					return x509Certificate2Collection[0];
				}
			}
			else
			{
				x509Certificate2Collection = c;
			}
			string authorityKeyIdentifier = this.GetAuthorityKeyIdentifier(child);
			if (string.IsNullOrEmpty(authorityKeyIdentifier))
			{
				return x509Certificate2Collection[0];
			}
			foreach (X509Certificate2 x509Certificate in x509Certificate2Collection)
			{
				string subjectKeyIdentifier = this.GetSubjectKeyIdentifier(x509Certificate);
				if (authorityKeyIdentifier == subjectKeyIdentifier)
				{
					return x509Certificate;
				}
			}
			return x509Certificate2Collection[0];
		}

		// Token: 0x06002771 RID: 10097 RVA: 0x0007BA1C File Offset: 0x00079C1C
		private X509Certificate2 FindParent(X509Certificate2 certificate)
		{
			X509Certificate2Collection x509Certificate2Collection = this.CertificateCollection.Find(X509FindType.FindBySubjectDistinguishedName, certificate.Issuer, false);
			string authorityKeyIdentifier = this.GetAuthorityKeyIdentifier(certificate);
			if (authorityKeyIdentifier != null && authorityKeyIdentifier.Length > 0)
			{
				x509Certificate2Collection.AddRange(this.CertificateCollection.Find(X509FindType.FindBySubjectKeyIdentifier, authorityKeyIdentifier, false));
			}
			X509Certificate2 x509Certificate = this.SelectBestFromCollection(certificate, x509Certificate2Collection);
			return (!certificate.Equals(x509Certificate)) ? x509Certificate : null;
		}

		// Token: 0x06002772 RID: 10098 RVA: 0x0007BA88 File Offset: 0x00079C88
		private bool IsChainComplete(X509Certificate2 certificate)
		{
			if (!this.IsSelfIssued(certificate))
			{
				return false;
			}
			if (certificate.Version < 3)
			{
				return true;
			}
			string subjectKeyIdentifier = this.GetSubjectKeyIdentifier(certificate);
			if (string.IsNullOrEmpty(subjectKeyIdentifier))
			{
				return true;
			}
			string authorityKeyIdentifier = this.GetAuthorityKeyIdentifier(certificate);
			return string.IsNullOrEmpty(authorityKeyIdentifier) || authorityKeyIdentifier == subjectKeyIdentifier;
		}

		// Token: 0x06002773 RID: 10099 RVA: 0x0007BAE4 File Offset: 0x00079CE4
		private bool IsSelfIssued(X509Certificate2 certificate)
		{
			return certificate.Issuer == certificate.Subject;
		}

		// Token: 0x06002774 RID: 10100 RVA: 0x0007BAF8 File Offset: 0x00079CF8
		private void ValidateChain(X509ChainStatusFlags flag)
		{
			int num = this.elements.Count - 1;
			X509Certificate2 certificate = this.elements[num].Certificate;
			if ((flag & X509ChainStatusFlags.PartialChain) == X509ChainStatusFlags.NoError)
			{
				this.Process(num);
				if (num == 0)
				{
					this.elements[0].UncompressFlags();
					return;
				}
				num--;
			}
			this.working_public_key = certificate.PublicKey.Key;
			this.working_issuer_name = certificate.IssuerName;
			this.max_path_length = num;
			for (int i = num; i > 0; i--)
			{
				this.Process(i);
				this.PrepareForNextCertificate(i);
			}
			this.Process(0);
			this.CheckRevocationOnChain(flag);
			this.WrapUp();
		}

		// Token: 0x06002775 RID: 10101 RVA: 0x0007BBAC File Offset: 0x00079DAC
		private void Process(int n)
		{
			X509ChainElement x509ChainElement = this.elements[n];
			X509Certificate2 certificate = x509ChainElement.Certificate;
			if (n != this.elements.Count - 1 && certificate.MonoCertificate.KeyAlgorithm == "1.2.840.10040.4.1" && certificate.MonoCertificate.KeyAlgorithmParameters == null)
			{
				X509Certificate2 certificate2 = this.elements[n + 1].Certificate;
				certificate.MonoCertificate.KeyAlgorithmParameters = certificate2.MonoCertificate.KeyAlgorithmParameters;
			}
			bool flag = this.working_public_key == null;
			if (!this.IsSignedWith(certificate, (!flag) ? this.working_public_key : certificate.PublicKey.Key) && (flag || n != this.elements.Count - 1 || this.IsSelfIssued(certificate)))
			{
				x509ChainElement.StatusFlags |= X509ChainStatusFlags.NotSignatureValid;
			}
			if (this.ChainPolicy.VerificationTime < certificate.NotBefore || this.ChainPolicy.VerificationTime > certificate.NotAfter)
			{
				x509ChainElement.StatusFlags |= X509ChainStatusFlags.NotTimeValid;
			}
			if (flag)
			{
				return;
			}
			if (!X500DistinguishedName.AreEqual(certificate.IssuerName, this.working_issuer_name))
			{
				x509ChainElement.StatusFlags |= X509ChainStatusFlags.InvalidNameConstraints;
			}
			if (this.IsSelfIssued(certificate) || n != 0)
			{
			}
		}

		// Token: 0x06002776 RID: 10102 RVA: 0x0007BD20 File Offset: 0x00079F20
		private void PrepareForNextCertificate(int n)
		{
			X509ChainElement x509ChainElement = this.elements[n];
			X509Certificate2 certificate = x509ChainElement.Certificate;
			this.working_issuer_name = certificate.SubjectName;
			this.working_public_key = certificate.PublicKey.Key;
			X509BasicConstraintsExtension x509BasicConstraintsExtension = (X509BasicConstraintsExtension)certificate.Extensions["2.5.29.19"];
			if (x509BasicConstraintsExtension != null)
			{
				if (!x509BasicConstraintsExtension.CertificateAuthority)
				{
					x509ChainElement.StatusFlags |= X509ChainStatusFlags.InvalidBasicConstraints;
				}
			}
			else if (certificate.Version >= 3)
			{
				x509ChainElement.StatusFlags |= X509ChainStatusFlags.InvalidBasicConstraints;
			}
			if (!this.IsSelfIssued(certificate))
			{
				if (this.max_path_length > 0)
				{
					this.max_path_length--;
				}
				else if (this.bce_restriction != null)
				{
					this.bce_restriction.StatusFlags |= X509ChainStatusFlags.InvalidBasicConstraints;
				}
			}
			if (x509BasicConstraintsExtension != null && x509BasicConstraintsExtension.HasPathLengthConstraint && x509BasicConstraintsExtension.PathLengthConstraint < this.max_path_length)
			{
				this.max_path_length = x509BasicConstraintsExtension.PathLengthConstraint;
				this.bce_restriction = x509ChainElement;
			}
			X509KeyUsageExtension x509KeyUsageExtension = (X509KeyUsageExtension)certificate.Extensions["2.5.29.15"];
			if (x509KeyUsageExtension != null)
			{
				X509KeyUsageFlags x509KeyUsageFlags = X509KeyUsageFlags.KeyCertSign;
				if ((x509KeyUsageExtension.KeyUsages & x509KeyUsageFlags) != x509KeyUsageFlags)
				{
					x509ChainElement.StatusFlags |= X509ChainStatusFlags.NotValidForUsage;
				}
			}
			this.ProcessCertificateExtensions(x509ChainElement);
		}

		// Token: 0x06002777 RID: 10103 RVA: 0x0007BE84 File Offset: 0x0007A084
		private void WrapUp()
		{
			X509ChainElement x509ChainElement = this.elements[0];
			X509Certificate2 certificate = x509ChainElement.Certificate;
			if (this.IsSelfIssued(certificate))
			{
			}
			this.ProcessCertificateExtensions(x509ChainElement);
			for (int i = this.elements.Count - 1; i >= 0; i--)
			{
				this.elements[i].UncompressFlags();
			}
		}

		// Token: 0x06002778 RID: 10104 RVA: 0x0007BEE8 File Offset: 0x0007A0E8
		private void ProcessCertificateExtensions(X509ChainElement element)
		{
			foreach (X509Extension x509Extension in element.Certificate.Extensions)
			{
				if (x509Extension.Critical)
				{
					string value = x509Extension.Oid.Value;
					if (value != null)
					{
						if (X509Chain.<>f__switch$map17 == null)
						{
							X509Chain.<>f__switch$map17 = new Dictionary<string, int>(2)
							{
								{
									"2.5.29.15",
									0
								},
								{
									"2.5.29.19",
									0
								}
							};
						}
						int num;
						if (X509Chain.<>f__switch$map17.TryGetValue(value, out num))
						{
							if (num == 0)
							{
								continue;
							}
						}
					}
					element.StatusFlags |= X509ChainStatusFlags.InvalidExtension;
				}
			}
		}

		// Token: 0x06002779 RID: 10105 RVA: 0x0007BFA4 File Offset: 0x0007A1A4
		private bool IsSignedWith(X509Certificate2 signed, AsymmetricAlgorithm pubkey)
		{
			if (pubkey == null)
			{
				return false;
			}
			X509Certificate monoCertificate = signed.MonoCertificate;
			return monoCertificate.VerifySignature(pubkey);
		}

		// Token: 0x0600277A RID: 10106 RVA: 0x0007BFC8 File Offset: 0x0007A1C8
		private string GetSubjectKeyIdentifier(X509Certificate2 certificate)
		{
			X509SubjectKeyIdentifierExtension x509SubjectKeyIdentifierExtension = (X509SubjectKeyIdentifierExtension)certificate.Extensions["2.5.29.14"];
			return (x509SubjectKeyIdentifierExtension != null) ? x509SubjectKeyIdentifierExtension.SubjectKeyIdentifier : string.Empty;
		}

		// Token: 0x0600277B RID: 10107 RVA: 0x0007C004 File Offset: 0x0007A204
		private string GetAuthorityKeyIdentifier(X509Certificate2 certificate)
		{
			return this.GetAuthorityKeyIdentifier(certificate.MonoCertificate.Extensions["2.5.29.35"]);
		}

		// Token: 0x0600277C RID: 10108 RVA: 0x0007C02C File Offset: 0x0007A22C
		private string GetAuthorityKeyIdentifier(X509Crl crl)
		{
			return this.GetAuthorityKeyIdentifier(crl.Extensions["2.5.29.35"]);
		}

		// Token: 0x0600277D RID: 10109 RVA: 0x0007C044 File Offset: 0x0007A244
		private string GetAuthorityKeyIdentifier(X509Extension ext)
		{
			if (ext == null)
			{
				return string.Empty;
			}
			AuthorityKeyIdentifierExtension authorityKeyIdentifierExtension = new AuthorityKeyIdentifierExtension(ext);
			byte[] identifier = authorityKeyIdentifierExtension.Identifier;
			if (identifier == null)
			{
				return string.Empty;
			}
			StringBuilder stringBuilder = new StringBuilder();
			foreach (byte b in identifier)
			{
				stringBuilder.Append(b.ToString("X02"));
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600277E RID: 10110 RVA: 0x0007C0B8 File Offset: 0x0007A2B8
		private void CheckRevocationOnChain(X509ChainStatusFlags flag)
		{
			bool flag2 = (flag & X509ChainStatusFlags.PartialChain) != X509ChainStatusFlags.NoError;
			bool online;
			switch (this.ChainPolicy.RevocationMode)
			{
			case X509RevocationMode.NoCheck:
				return;
			case X509RevocationMode.Online:
				online = true;
				break;
			case X509RevocationMode.Offline:
				online = false;
				break;
			default:
				throw new InvalidOperationException(Locale.GetText("Invalid revocation mode."));
			}
			bool flag3 = flag2;
			for (int i = this.elements.Count - 1; i >= 0; i--)
			{
				bool flag4 = true;
				switch (this.ChainPolicy.RevocationFlag)
				{
				case X509RevocationFlag.EndCertificateOnly:
					flag4 = (i == 0);
					break;
				case X509RevocationFlag.EntireChain:
					flag4 = true;
					break;
				case X509RevocationFlag.ExcludeRoot:
					flag4 = (i != this.elements.Count - 1);
					break;
				}
				X509ChainElement x509ChainElement = this.elements[i];
				if (!flag3)
				{
					flag3 |= ((x509ChainElement.StatusFlags & X509ChainStatusFlags.NotSignatureValid) != X509ChainStatusFlags.NoError);
				}
				if (flag3)
				{
					x509ChainElement.StatusFlags |= X509ChainStatusFlags.RevocationStatusUnknown;
					x509ChainElement.StatusFlags |= X509ChainStatusFlags.OfflineRevocation;
				}
				else if (flag4 && !flag2 && !this.IsSelfIssued(x509ChainElement.Certificate))
				{
					x509ChainElement.StatusFlags |= this.CheckRevocation(x509ChainElement.Certificate, i + 1, online);
					flag3 |= ((x509ChainElement.StatusFlags & X509ChainStatusFlags.Revoked) != X509ChainStatusFlags.NoError);
				}
			}
		}

		// Token: 0x0600277F RID: 10111 RVA: 0x0007C230 File Offset: 0x0007A430
		private X509ChainStatusFlags CheckRevocation(X509Certificate2 certificate, int ca, bool online)
		{
			X509ChainStatusFlags x509ChainStatusFlags = X509ChainStatusFlags.RevocationStatusUnknown;
			X509ChainElement x509ChainElement = this.elements[ca];
			X509Certificate2 certificate2 = x509ChainElement.Certificate;
			while (this.IsSelfIssued(certificate2) && ca < this.elements.Count - 1)
			{
				x509ChainStatusFlags = this.CheckRevocation(certificate, certificate2, online);
				if (x509ChainStatusFlags != X509ChainStatusFlags.RevocationStatusUnknown)
				{
					break;
				}
				ca++;
				x509ChainElement = this.elements[ca];
				certificate2 = x509ChainElement.Certificate;
			}
			if (x509ChainStatusFlags == X509ChainStatusFlags.RevocationStatusUnknown)
			{
				x509ChainStatusFlags = this.CheckRevocation(certificate, certificate2, online);
			}
			return x509ChainStatusFlags;
		}

		// Token: 0x06002780 RID: 10112 RVA: 0x0007C2BC File Offset: 0x0007A4BC
		private X509ChainStatusFlags CheckRevocation(X509Certificate2 certificate, X509Certificate2 ca_cert, bool online)
		{
			X509KeyUsageExtension x509KeyUsageExtension = (X509KeyUsageExtension)ca_cert.Extensions["2.5.29.15"];
			if (x509KeyUsageExtension != null)
			{
				X509KeyUsageFlags x509KeyUsageFlags = X509KeyUsageFlags.CrlSign;
				if ((x509KeyUsageExtension.KeyUsages & x509KeyUsageFlags) != x509KeyUsageFlags)
				{
					return X509ChainStatusFlags.RevocationStatusUnknown;
				}
			}
			X509Crl x509Crl = this.FindCrl(ca_cert);
			if (x509Crl != null || online)
			{
			}
			if (x509Crl == null)
			{
				return X509ChainStatusFlags.RevocationStatusUnknown;
			}
			if (!x509Crl.VerifySignature(ca_cert.PublicKey.Key))
			{
				return X509ChainStatusFlags.RevocationStatusUnknown;
			}
			X509Crl.X509CrlEntry crlEntry = x509Crl.GetCrlEntry(certificate.MonoCertificate);
			if (crlEntry != null)
			{
				if (!this.ProcessCrlEntryExtensions(crlEntry))
				{
					return X509ChainStatusFlags.Revoked;
				}
				if (crlEntry.RevocationDate <= this.ChainPolicy.VerificationTime)
				{
					return X509ChainStatusFlags.Revoked;
				}
			}
			if (x509Crl.NextUpdate < this.ChainPolicy.VerificationTime)
			{
				return X509ChainStatusFlags.RevocationStatusUnknown | X509ChainStatusFlags.OfflineRevocation;
			}
			if (!this.ProcessCrlExtensions(x509Crl))
			{
				return X509ChainStatusFlags.RevocationStatusUnknown;
			}
			return X509ChainStatusFlags.NoError;
		}

		// Token: 0x06002781 RID: 10113 RVA: 0x0007C3A4 File Offset: 0x0007A5A4
		private X509Crl FindCrl(X509Certificate2 caCertificate)
		{
			string b = caCertificate.SubjectName.Decode(X500DistinguishedNameFlags.None);
			string subjectKeyIdentifier = this.GetSubjectKeyIdentifier(caCertificate);
			foreach (object obj in this.CertificateAuthorities.Store.Crls)
			{
				X509Crl x509Crl = (X509Crl)obj;
				if (x509Crl.IssuerName == b && (subjectKeyIdentifier.Length == 0 || subjectKeyIdentifier == this.GetAuthorityKeyIdentifier(x509Crl)))
				{
					return x509Crl;
				}
			}
			foreach (object obj2 in this.Roots.Store.Crls)
			{
				X509Crl x509Crl2 = (X509Crl)obj2;
				if (x509Crl2.IssuerName == b && (subjectKeyIdentifier.Length == 0 || subjectKeyIdentifier == this.GetAuthorityKeyIdentifier(x509Crl2)))
				{
					return x509Crl2;
				}
			}
			return null;
		}

		// Token: 0x06002782 RID: 10114 RVA: 0x0007C50C File Offset: 0x0007A70C
		private bool ProcessCrlExtensions(X509Crl crl)
		{
			foreach (object obj in crl.Extensions)
			{
				X509Extension x509Extension = (X509Extension)obj;
				if (x509Extension.Critical)
				{
					string oid = x509Extension.Oid;
					if (oid != null)
					{
						if (X509Chain.<>f__switch$map18 == null)
						{
							X509Chain.<>f__switch$map18 = new Dictionary<string, int>(2)
							{
								{
									"2.5.29.20",
									0
								},
								{
									"2.5.29.35",
									0
								}
							};
						}
						int num;
						if (X509Chain.<>f__switch$map18.TryGetValue(oid, out num))
						{
							if (num == 0)
							{
								continue;
							}
						}
					}
					return false;
				}
			}
			return true;
		}

		// Token: 0x06002783 RID: 10115 RVA: 0x0007C5F0 File Offset: 0x0007A7F0
		private bool ProcessCrlEntryExtensions(X509Crl.X509CrlEntry entry)
		{
			foreach (object obj in entry.Extensions)
			{
				X509Extension x509Extension = (X509Extension)obj;
				if (x509Extension.Critical)
				{
					string oid = x509Extension.Oid;
					if (oid != null)
					{
						if (X509Chain.<>f__switch$map19 == null)
						{
							X509Chain.<>f__switch$map19 = new Dictionary<string, int>(1)
							{
								{
									"2.5.29.21",
									0
								}
							};
						}
						int num;
						if (X509Chain.<>f__switch$map19.TryGetValue(oid, out num))
						{
							if (num == 0)
							{
								continue;
							}
						}
					}
					return false;
				}
			}
			return true;
		}

		// Token: 0x04001828 RID: 6184
		private StoreLocation location;

		// Token: 0x04001829 RID: 6185
		private X509ChainElementCollection elements;

		// Token: 0x0400182A RID: 6186
		private X509ChainPolicy policy;

		// Token: 0x0400182B RID: 6187
		private X509ChainStatus[] status;

		// Token: 0x0400182C RID: 6188
		private static X509ChainStatus[] Empty = new X509ChainStatus[0];

		// Token: 0x0400182D RID: 6189
		private int max_path_length;

		// Token: 0x0400182E RID: 6190
		private X500DistinguishedName working_issuer_name;

		// Token: 0x0400182F RID: 6191
		private AsymmetricAlgorithm working_public_key;

		// Token: 0x04001830 RID: 6192
		private X509ChainElement bce_restriction;

		// Token: 0x04001831 RID: 6193
		private X509Store roots;

		// Token: 0x04001832 RID: 6194
		private X509Store cas;

		// Token: 0x04001833 RID: 6195
		private X509Certificate2Collection collection;
	}
}
