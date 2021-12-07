using System;

namespace System.Security.AccessControl
{
	/// <summary>Represents an Access Control Entry (ACE) that is not defined by one of the members of the <see cref="T:System.Security.AccessControl.AceType" /> enumeration.</summary>
	// Token: 0x0200056C RID: 1388
	public sealed class CustomAce : GenericAce
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.AccessControl.CustomAce" /> class.</summary>
		/// <param name="type">Type of the new Access Control Entry (ACE). This value must be greater than <see cref="F:System.Security.AccessControl.AceType.MaxDefinedAceType" />.</param>
		/// <param name="flags">Flags that specify information about the inheritance, inheritance propagation, and auditing conditions for the new ACE.</param>
		/// <param name="opaque">An array of byte values that contains the data for the new ACE. This value can be null. The length of this array must not be greater than the value of the <see cref="F:System.Security.AccessControl.CustomAce.MaxOpaqueLength" /> field, and must be a multiple of four.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value of the <paramref name="type" /> parameter is not greater than <see cref="F:System.Security.AccessControl.AceType.MaxDefinedAceType" /> or the length of the <paramref name="opaque" /> array is either greater than the value of the <see cref="F:System.Security.AccessControl.CustomAce.MaxOpaqueLength" /> field or not a multiple of four.</exception>
		// Token: 0x060035FD RID: 13821 RVA: 0x000B1AE8 File Offset: 0x000AFCE8
		public CustomAce(AceType type, AceFlags flags, byte[] opaque) : base(type)
		{
			base.AceFlags = flags;
			this.SetOpaque(opaque);
		}

		/// <summary>Gets the length, in bytes, of the binary representation of the current <see cref="T:System.Security.AccessControl.CustomAce" /> object. This length should be used before marshaling the ACL into a binary array with the <see cref="M:System.Security.AccessControl.CustomAce.GetBinaryForm" /> method.</summary>
		/// <returns>The length, in bytes, of the binary representation of the current <see cref="T:System.Security.AccessControl.CustomAce" /> object.</returns>
		// Token: 0x17000A25 RID: 2597
		// (get) Token: 0x060035FE RID: 13822 RVA: 0x000B1B00 File Offset: 0x000AFD00
		[MonoTODO]
		public override int BinaryLength
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the length of the opaque data associated with this <see cref="T:System.Security.AccessControl.CustomAce" /> object.</summary>
		/// <returns>The length of the opaque callback data.</returns>
		// Token: 0x17000A26 RID: 2598
		// (get) Token: 0x060035FF RID: 13823 RVA: 0x000B1B08 File Offset: 0x000AFD08
		public int OpaqueLength
		{
			get
			{
				return this.opaque.Length;
			}
		}

		/// <summary>Marshals the contents of the <see cref="T:System.Security.AccessControl.CustomAce" /> object into the specified byte array beginning at the specified offset.</summary>
		/// <param name="binaryForm">The byte array into which the contents of the <see cref="T:System.Security.AccessControl.CustomAce" /> is marshaled.</param>
		/// <param name="offset">The offset at which to start marshaling.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> is negative or too high to allow the entire <see cref="T:System.Security.AccessControl.CustomAce" /> to be copied into <paramref name="array" />.</exception>
		// Token: 0x06003600 RID: 13824 RVA: 0x000B1B14 File Offset: 0x000AFD14
		[MonoTODO]
		public override void GetBinaryForm(byte[] binaryForm, int offset)
		{
			throw new NotImplementedException();
		}

		/// <summary>Returns the opaque data associated with this <see cref="T:System.Security.AccessControl.CustomAce" /> object. </summary>
		/// <returns>An array of byte values that represents the opaque data associated with this <see cref="T:System.Security.AccessControl.CustomAce" /> object.</returns>
		// Token: 0x06003601 RID: 13825 RVA: 0x000B1B1C File Offset: 0x000AFD1C
		public byte[] GetOpaque()
		{
			return (byte[])this.opaque.Clone();
		}

		/// <summary>Sets the opaque callback data associated with this <see cref="T:System.Security.AccessControl.CustomAce" /> object.</summary>
		/// <param name="opaque">An array of byte values that represents the opaque callback data for this <see cref="T:System.Security.AccessControl.CustomAce" /> object.</param>
		// Token: 0x06003602 RID: 13826 RVA: 0x000B1B30 File Offset: 0x000AFD30
		public void SetOpaque(byte[] opaque)
		{
			if (opaque == null)
			{
				throw new ArgumentNullException("opaque");
			}
			this.opaque = (byte[])opaque.Clone();
		}

		// Token: 0x040016F3 RID: 5875
		private byte[] opaque;

		/// <summary>Returns the maximum allowed length of an opaque data blob for this <see cref="T:System.Security.AccessControl.CustomAce" /> object.</summary>
		// Token: 0x040016F4 RID: 5876
		[MonoTODO]
		public static readonly int MaxOpaqueLength;
	}
}
