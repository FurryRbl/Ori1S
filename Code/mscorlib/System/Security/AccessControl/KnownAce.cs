using System;
using System.Security.Principal;

namespace System.Security.AccessControl
{
	/// <summary>Encapsulates all Access Control Entry (ACE) types currently defined by Microsoft Corporation. All <see cref="T:System.Security.AccessControl.KnownAce" /> objects contain a 32-bit access mask and a <see cref="T:System.Security.Principal.SecurityIdentifier" /> object.</summary>
	// Token: 0x0200057D RID: 1405
	public abstract class KnownAce : GenericAce
	{
		// Token: 0x06003681 RID: 13953 RVA: 0x000B2178 File Offset: 0x000B0378
		internal KnownAce(InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags) : base(inheritanceFlags, propagationFlags)
		{
		}

		/// <summary>Gets or sets the access mask for this <see cref="T:System.Security.AccessControl.KnownAce" /> object.</summary>
		/// <returns>The access mask for this <see cref="T:System.Security.AccessControl.KnownAce" /> object.</returns>
		// Token: 0x17000A43 RID: 2627
		// (get) Token: 0x06003682 RID: 13954 RVA: 0x000B2184 File Offset: 0x000B0384
		// (set) Token: 0x06003683 RID: 13955 RVA: 0x000B218C File Offset: 0x000B038C
		public int AccessMask
		{
			get
			{
				return this.access_mask;
			}
			set
			{
				this.access_mask = value;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Security.Principal.SecurityIdentifier" /> object associated with this <see cref="T:System.Security.AccessControl.KnownAce" /> object.</summary>
		/// <returns>The <see cref="T:System.Security.Principal.SecurityIdentifier" /> object associated with this <see cref="T:System.Security.AccessControl.KnownAce" /> object.</returns>
		// Token: 0x17000A44 RID: 2628
		// (get) Token: 0x06003684 RID: 13956 RVA: 0x000B2198 File Offset: 0x000B0398
		// (set) Token: 0x06003685 RID: 13957 RVA: 0x000B21A0 File Offset: 0x000B03A0
		public SecurityIdentifier SecurityIdentifier
		{
			get
			{
				return this.identifier;
			}
			set
			{
				this.identifier = value;
			}
		}

		// Token: 0x04001724 RID: 5924
		private int access_mask;

		// Token: 0x04001725 RID: 5925
		private SecurityIdentifier identifier;
	}
}
