using System;

namespace System.Security.Cryptography.X509Certificates
{
	/// <summary>Provides a simple structure for storing X509 chain status and error information.</summary>
	// Token: 0x0200044A RID: 1098
	public struct X509ChainStatus
	{
		// Token: 0x060027AD RID: 10157 RVA: 0x0007CC8C File Offset: 0x0007AE8C
		internal X509ChainStatus(X509ChainStatusFlags flag)
		{
			this.status = flag;
			this.info = X509ChainStatus.GetInformation(flag);
		}

		/// <summary>Specifies the status of the X509 chain.</summary>
		/// <returns>An <see cref="T:System.Security.Cryptography.X509Certificates.X509ChainStatusFlags" /> value.</returns>
		// Token: 0x17000B25 RID: 2853
		// (get) Token: 0x060027AE RID: 10158 RVA: 0x0007CCA4 File Offset: 0x0007AEA4
		// (set) Token: 0x060027AF RID: 10159 RVA: 0x0007CCAC File Offset: 0x0007AEAC
		public X509ChainStatusFlags Status
		{
			get
			{
				return this.status;
			}
			set
			{
				this.status = value;
			}
		}

		/// <summary>Specifies a description of the <see cref="P:System.Security.Cryptography.X509Certificates.X509Chain.ChainStatus" /> value.</summary>
		/// <returns>A localizable string.</returns>
		// Token: 0x17000B26 RID: 2854
		// (get) Token: 0x060027B0 RID: 10160 RVA: 0x0007CCB8 File Offset: 0x0007AEB8
		// (set) Token: 0x060027B1 RID: 10161 RVA: 0x0007CCC0 File Offset: 0x0007AEC0
		public string StatusInformation
		{
			get
			{
				return this.info;
			}
			set
			{
				this.info = value;
			}
		}

		// Token: 0x060027B2 RID: 10162 RVA: 0x0007CCCC File Offset: 0x0007AECC
		internal static string GetInformation(X509ChainStatusFlags flags)
		{
			switch (flags)
			{
			case X509ChainStatusFlags.NoError:
				goto IL_FF;
			case X509ChainStatusFlags.NotTimeValid:
			case X509ChainStatusFlags.NotTimeNested:
			case X509ChainStatusFlags.Revoked:
			case X509ChainStatusFlags.NotSignatureValid:
				break;
			default:
				if (flags != X509ChainStatusFlags.NotValidForUsage && flags != X509ChainStatusFlags.UntrustedRoot && flags != X509ChainStatusFlags.RevocationStatusUnknown && flags != X509ChainStatusFlags.Cyclic && flags != X509ChainStatusFlags.InvalidExtension && flags != X509ChainStatusFlags.InvalidPolicyConstraints && flags != X509ChainStatusFlags.InvalidBasicConstraints && flags != X509ChainStatusFlags.InvalidNameConstraints && flags != X509ChainStatusFlags.HasNotSupportedNameConstraint && flags != X509ChainStatusFlags.HasNotDefinedNameConstraint && flags != X509ChainStatusFlags.HasNotPermittedNameConstraint && flags != X509ChainStatusFlags.HasExcludedNameConstraint && flags != X509ChainStatusFlags.PartialChain && flags != X509ChainStatusFlags.CtlNotTimeValid && flags != X509ChainStatusFlags.CtlNotSignatureValid && flags != X509ChainStatusFlags.CtlNotValidForUsage && flags != X509ChainStatusFlags.OfflineRevocation && flags != X509ChainStatusFlags.NoIssuanceChainPolicy)
				{
					goto IL_FF;
				}
				break;
			}
			return Locale.GetText(flags.ToString());
			IL_FF:
			return string.Empty;
		}

		// Token: 0x04001845 RID: 6213
		private X509ChainStatusFlags status;

		// Token: 0x04001846 RID: 6214
		private string info;
	}
}
