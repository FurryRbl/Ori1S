using System;
using System.Security.Principal;

namespace System.Security.AccessControl
{
	/// <summary>Represents an access rule for a cryptographic key. An access rule represents a combination of a user's identity, an access mask, and an access control type (allow or deny). An access rule object also contains information about the how the rule is inherited by child objects and how that inheritance is propagated.</summary>
	// Token: 0x02000568 RID: 1384
	public sealed class CryptoKeyAccessRule : AccessRule
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.AccessControl.CryptoKeyAccessRule" /> class using the specified values. </summary>
		/// <param name="identity">The identity to which the access rule applies. This parameter must be an object that can be cast as a <see cref="T:System.Security.Principal.SecurityIdentifier" />.</param>
		/// <param name="cryptoKeyRights">The cryptographic key operation to which this access rule controls access.</param>
		/// <param name="type">The valid access control type.</param>
		// Token: 0x060035E5 RID: 13797 RVA: 0x000B19E4 File Offset: 0x000AFBE4
		public CryptoKeyAccessRule(IdentityReference identity, CryptoKeyRights cryptoKeyRights, AccessControlType type) : base(identity, 0, false, InheritanceFlags.None, PropagationFlags.None, AccessControlType.Allow)
		{
			this.rights = cryptoKeyRights;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.AccessControl.CryptoKeyAccessRule" /> class using the specified values.</summary>
		/// <param name="identity">The identity to which the access rule applies.</param>
		/// <param name="cryptoKeyRights">The cryptographic key operation to which this access rule controls access.</param>
		/// <param name="type">The valid access control type.</param>
		// Token: 0x060035E6 RID: 13798 RVA: 0x000B19FC File Offset: 0x000AFBFC
		public CryptoKeyAccessRule(string identity, CryptoKeyRights cryptoKeyRights, AccessControlType type) : this(new SecurityIdentifier(identity), cryptoKeyRights, type)
		{
		}

		/// <summary>Gets the cryptographic key operation to which this access rule controls access.</summary>
		/// <returns>The cryptographic key operation to which this access rule controls access.</returns>
		// Token: 0x17000A20 RID: 2592
		// (get) Token: 0x060035E7 RID: 13799 RVA: 0x000B1A0C File Offset: 0x000AFC0C
		public CryptoKeyRights CryptoKeyRights
		{
			get
			{
				return this.rights;
			}
		}

		// Token: 0x040016E0 RID: 5856
		private CryptoKeyRights rights;
	}
}
