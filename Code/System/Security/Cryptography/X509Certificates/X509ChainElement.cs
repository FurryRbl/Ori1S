using System;

namespace System.Security.Cryptography.X509Certificates
{
	/// <summary>Represents an element of an X.509 chain.</summary>
	// Token: 0x02000447 RID: 1095
	public class X509ChainElement
	{
		// Token: 0x06002790 RID: 10128 RVA: 0x0007C7D4 File Offset: 0x0007A9D4
		internal X509ChainElement(X509Certificate2 certificate)
		{
			this.certificate = certificate;
			this.info = string.Empty;
		}

		/// <summary>Gets the X.509 certificate at a particular chain element.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.X509Certificates.X509Certificate2" /> object.</returns>
		// Token: 0x17000B17 RID: 2839
		// (get) Token: 0x06002791 RID: 10129 RVA: 0x0007C7F0 File Offset: 0x0007A9F0
		public X509Certificate2 Certificate
		{
			get
			{
				return this.certificate;
			}
		}

		/// <summary>Gets the error status of the current X.509 certificate in a chain.</summary>
		/// <returns>An array of <see cref="T:System.Security.Cryptography.X509Certificates.X509ChainStatus" /> objects.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000B18 RID: 2840
		// (get) Token: 0x06002792 RID: 10130 RVA: 0x0007C7F8 File Offset: 0x0007A9F8
		public X509ChainStatus[] ChainElementStatus
		{
			get
			{
				return this.status;
			}
		}

		/// <summary>Gets additional error information from an unmanaged certificate chain structure.</summary>
		/// <returns>A string representing the pwszExtendedErrorInfo member of the unmanaged CERT_CHAIN_ELEMENT structure in the Crypto API.</returns>
		// Token: 0x17000B19 RID: 2841
		// (get) Token: 0x06002793 RID: 10131 RVA: 0x0007C800 File Offset: 0x0007AA00
		public string Information
		{
			get
			{
				return this.info;
			}
		}

		// Token: 0x17000B1A RID: 2842
		// (get) Token: 0x06002794 RID: 10132 RVA: 0x0007C808 File Offset: 0x0007AA08
		// (set) Token: 0x06002795 RID: 10133 RVA: 0x0007C810 File Offset: 0x0007AA10
		internal X509ChainStatusFlags StatusFlags
		{
			get
			{
				return this.compressed_status_flags;
			}
			set
			{
				this.compressed_status_flags = value;
			}
		}

		// Token: 0x06002796 RID: 10134 RVA: 0x0007C81C File Offset: 0x0007AA1C
		private int Count(X509ChainStatusFlags flags)
		{
			int num = 0;
			int num2 = 0;
			int num3 = 1;
			while (num2++ < 32)
			{
				if ((flags & (X509ChainStatusFlags)num3) == (X509ChainStatusFlags)num3)
				{
					num++;
				}
				num3 <<= 1;
			}
			return num;
		}

		// Token: 0x06002797 RID: 10135 RVA: 0x0007C854 File Offset: 0x0007AA54
		private void Set(X509ChainStatus[] status, ref int position, X509ChainStatusFlags flags, X509ChainStatusFlags mask)
		{
			if ((flags & mask) != X509ChainStatusFlags.NoError)
			{
				status[position].Status = mask;
				status[position].StatusInformation = X509ChainStatus.GetInformation(mask);
				position++;
			}
		}

		// Token: 0x06002798 RID: 10136 RVA: 0x0007C894 File Offset: 0x0007AA94
		internal void UncompressFlags()
		{
			if (this.compressed_status_flags == X509ChainStatusFlags.NoError)
			{
				this.status = new X509ChainStatus[0];
			}
			else
			{
				int num = this.Count(this.compressed_status_flags);
				this.status = new X509ChainStatus[num];
				int num2 = 0;
				this.Set(this.status, ref num2, this.compressed_status_flags, X509ChainStatusFlags.UntrustedRoot);
				this.Set(this.status, ref num2, this.compressed_status_flags, X509ChainStatusFlags.NotTimeValid);
				this.Set(this.status, ref num2, this.compressed_status_flags, X509ChainStatusFlags.NotTimeNested);
				this.Set(this.status, ref num2, this.compressed_status_flags, X509ChainStatusFlags.Revoked);
				this.Set(this.status, ref num2, this.compressed_status_flags, X509ChainStatusFlags.NotSignatureValid);
				this.Set(this.status, ref num2, this.compressed_status_flags, X509ChainStatusFlags.NotValidForUsage);
				this.Set(this.status, ref num2, this.compressed_status_flags, X509ChainStatusFlags.RevocationStatusUnknown);
				this.Set(this.status, ref num2, this.compressed_status_flags, X509ChainStatusFlags.Cyclic);
				this.Set(this.status, ref num2, this.compressed_status_flags, X509ChainStatusFlags.InvalidExtension);
				this.Set(this.status, ref num2, this.compressed_status_flags, X509ChainStatusFlags.InvalidPolicyConstraints);
				this.Set(this.status, ref num2, this.compressed_status_flags, X509ChainStatusFlags.InvalidBasicConstraints);
				this.Set(this.status, ref num2, this.compressed_status_flags, X509ChainStatusFlags.InvalidNameConstraints);
				this.Set(this.status, ref num2, this.compressed_status_flags, X509ChainStatusFlags.HasNotSupportedNameConstraint);
				this.Set(this.status, ref num2, this.compressed_status_flags, X509ChainStatusFlags.HasNotDefinedNameConstraint);
				this.Set(this.status, ref num2, this.compressed_status_flags, X509ChainStatusFlags.HasNotPermittedNameConstraint);
				this.Set(this.status, ref num2, this.compressed_status_flags, X509ChainStatusFlags.HasExcludedNameConstraint);
				this.Set(this.status, ref num2, this.compressed_status_flags, X509ChainStatusFlags.PartialChain);
				this.Set(this.status, ref num2, this.compressed_status_flags, X509ChainStatusFlags.CtlNotTimeValid);
				this.Set(this.status, ref num2, this.compressed_status_flags, X509ChainStatusFlags.CtlNotSignatureValid);
				this.Set(this.status, ref num2, this.compressed_status_flags, X509ChainStatusFlags.CtlNotValidForUsage);
				this.Set(this.status, ref num2, this.compressed_status_flags, X509ChainStatusFlags.OfflineRevocation);
				this.Set(this.status, ref num2, this.compressed_status_flags, X509ChainStatusFlags.NoIssuanceChainPolicy);
			}
		}

		// Token: 0x04001838 RID: 6200
		private X509Certificate2 certificate;

		// Token: 0x04001839 RID: 6201
		private X509ChainStatus[] status;

		// Token: 0x0400183A RID: 6202
		private string info;

		// Token: 0x0400183B RID: 6203
		private X509ChainStatusFlags compressed_status_flags;
	}
}
