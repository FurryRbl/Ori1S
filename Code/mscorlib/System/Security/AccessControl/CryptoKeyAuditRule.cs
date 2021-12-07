using System;
using System.Security.Principal;

namespace System.Security.AccessControl
{
	/// <summary>Represents an audit rule for a cryptographic key. An audit rule represents a combination of a user's identity and an access mask. An audit rule also contains information about the how the rule is inherited by child objects, how that inheritance is propagated, and for what conditions it is audited.</summary>
	// Token: 0x02000569 RID: 1385
	public sealed class CryptoKeyAuditRule : AuditRule
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.AccessControl.CryptoKeyAuditRule" /> class using the specified values. </summary>
		/// <param name="identity">The identity to which the audit rule applies. This parameter must be an object that can be cast as a <see cref="T:System.Security.Principal.SecurityIdentifier" />.</param>
		/// <param name="cryptoKeyRights">The cryptographic key operation for which this audit rule generates audits.</param>
		/// <param name="flags">The conditions that generate audits.</param>
		// Token: 0x060035E8 RID: 13800 RVA: 0x000B1A14 File Offset: 0x000AFC14
		public CryptoKeyAuditRule(IdentityReference identity, CryptoKeyRights cryptoKeyRights, AuditFlags flags) : base(identity, 0, false, InheritanceFlags.None, PropagationFlags.None, flags)
		{
			this.rights = cryptoKeyRights;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.AccessControl.CryptoKeyAuditRule" /> class using the specified values. </summary>
		/// <param name="identity">The identity to which the audit rule applies.</param>
		/// <param name="cryptoKeyRights">The cryptographic key operation for which this audit rule generates audits.</param>
		/// <param name="flags">The conditions that generate audits.</param>
		// Token: 0x060035E9 RID: 13801 RVA: 0x000B1A2C File Offset: 0x000AFC2C
		public CryptoKeyAuditRule(string identity, CryptoKeyRights cryptoKeyRights, AuditFlags flags) : this(new SecurityIdentifier(identity), cryptoKeyRights, flags)
		{
		}

		/// <summary>Gets the cryptographic key operation for which this audit rule generates audits.</summary>
		/// <returns>The cryptographic key operation for which this audit rule generates audits.</returns>
		// Token: 0x17000A21 RID: 2593
		// (get) Token: 0x060035EA RID: 13802 RVA: 0x000B1A3C File Offset: 0x000AFC3C
		public CryptoKeyRights CryptoKeyRights
		{
			get
			{
				return this.rights;
			}
		}

		// Token: 0x040016E1 RID: 5857
		private CryptoKeyRights rights;
	}
}
