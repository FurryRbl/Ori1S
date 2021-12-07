using System;
using System.Security.Principal;

namespace System.Security.AccessControl
{
	/// <summary>Represents a compound Access Control Entry (ACE).</summary>
	// Token: 0x02000565 RID: 1381
	public sealed class CompoundAce : KnownAce
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.AccessControl.CompoundAce" /> class.</summary>
		/// <param name="flags">Contains flags that specify information about the inheritance, inheritance propagation, and auditing conditions for the new Access Control Entry (ACE).</param>
		/// <param name="accessMask">The access mask for the ACE.</param>
		/// <param name="compoundAceType">A value from the <see cref="T:System.Security.AccessControl.CompoundAceType" /> enumeration.</param>
		/// <param name="sid">The <see cref="T:System.Security.Principal.SecurityIdentifier" /> associated with the new ACE.</param>
		// Token: 0x060035E0 RID: 13792 RVA: 0x000B198C File Offset: 0x000AFB8C
		public CompoundAce(AceFlags flags, int accessMask, CompoundAceType compoundAceType, SecurityIdentifier sid) : base(InheritanceFlags.None, PropagationFlags.None)
		{
			this.compound_ace_type = compoundAceType;
			base.AceFlags = flags;
			base.AccessMask = accessMask;
			base.SecurityIdentifier = sid;
		}

		/// <summary>Gets the length, in bytes, of the binary representation of the current <see cref="T:System.Security.AccessControl.CompoundAce" /> object. This length should be used before marshaling the ACL into a binary array with the <see cref="M:System.Security.AccessControl. CompoundAce.GetBinaryForm" /> method.</summary>
		/// <returns>The length, in bytes, of the binary representation of the current <see cref="T:System.Security.AccessControl. CompoundAce" /> object.</returns>
		// Token: 0x17000A1E RID: 2590
		// (get) Token: 0x060035E1 RID: 13793 RVA: 0x000B19C0 File Offset: 0x000AFBC0
		[MonoTODO]
		public override int BinaryLength
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets or sets the type of this <see cref="T:System.Security.AccessControl. CompoundAce" /> object.</summary>
		/// <returns>The type of this <see cref="T:System.Security.AccessControl. CompoundAce" /> object.</returns>
		// Token: 0x17000A1F RID: 2591
		// (get) Token: 0x060035E2 RID: 13794 RVA: 0x000B19C8 File Offset: 0x000AFBC8
		// (set) Token: 0x060035E3 RID: 13795 RVA: 0x000B19D0 File Offset: 0x000AFBD0
		public CompoundAceType CompoundAceType
		{
			get
			{
				return this.compound_ace_type;
			}
			set
			{
				this.compound_ace_type = value;
			}
		}

		/// <summary>Marshals the contents of the <see cref="T:System.Security.AccessControl.CompoundAce" /> object into the specified byte array beginning at the specified offset.</summary>
		/// <param name="binaryForm">The byte array into which the contents of the <see cref="T:System.Security.AccessControl. CompoundAce" /> is marshaled.</param>
		/// <param name="offset">The offset at which to start marshaling.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> is negative or too high to allow the entire <see cref="T:System.Security.AccessControl. CompoundAce" /> to be copied into <paramref name="array" />.</exception>
		// Token: 0x060035E4 RID: 13796 RVA: 0x000B19DC File Offset: 0x000AFBDC
		[MonoTODO]
		public override void GetBinaryForm(byte[] binaryForm, int offset)
		{
			throw new NotImplementedException();
		}

		// Token: 0x040016CB RID: 5835
		private CompoundAceType compound_ace_type;
	}
}
