using System;
using System.Collections.Generic;

namespace System.Security.AccessControl
{
	/// <summary>Represents an Access Control List (ACL).</summary>
	// Token: 0x0200058B RID: 1419
	public sealed class RawAcl : GenericAcl
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Security.AccessControl.RawAcl" /> class with the specified revision level.</summary>
		/// <param name="revision">The revision level of the new Access Control List (ACL).</param>
		/// <param name="capacity">The number of Access Control Entries (ACEs) this <see cref="T:System.Security.AccessControl.RawAcl" /> object can contain. This number is to be used only as a hint.</param>
		// Token: 0x060036F4 RID: 14068 RVA: 0x000B26B8 File Offset: 0x000B08B8
		public RawAcl(byte revision, int capacity)
		{
			this.revision = revision;
			this.list = new List<GenericAce>(capacity);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Security.AccessControl.RawAcl" /> class from the specified binary form.</summary>
		/// <param name="binaryForm">An array of byte values that represent an Access Control List (ACL).</param>
		/// <param name="offset">The offset in the <paramref name="binaryForm" /> parameter at which to begin unmarshaling data.</param>
		// Token: 0x060036F5 RID: 14069 RVA: 0x000B26D4 File Offset: 0x000B08D4
		public RawAcl(byte[] binaryForm, int offset) : this(0, 10)
		{
		}

		/// <summary>Gets the length, in bytes, of the binary representation of the current <see cref="T:System.Security.AccessControl.RawAcl" /> object. This length should be used before marshaling the ACL into a binary array with the <see cref="M:System.Security.AccessControl.RawAcl.GetBinaryForm" /> method.</summary>
		/// <returns>The length, in bytes, of the binary representation of the current <see cref="T:System.Security.AccessControl.RawAcl" /> object.</returns>
		// Token: 0x17000A65 RID: 2661
		// (get) Token: 0x060036F6 RID: 14070 RVA: 0x000B26E0 File Offset: 0x000B08E0
		[MonoTODO]
		public override int BinaryLength
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the number of access control entries (ACEs) in the current <see cref="T:System.Security.AccessControl.RawAcl" /> object.</summary>
		/// <returns>The number of ACEs in the current <see cref="T:System.Security.AccessControl.RawAcl" /> object.</returns>
		// Token: 0x17000A66 RID: 2662
		// (get) Token: 0x060036F7 RID: 14071 RVA: 0x000B26E8 File Offset: 0x000B08E8
		public override int Count
		{
			get
			{
				return this.list.Count;
			}
		}

		/// <summary>Gets or sets the Access Control Entry (ACE) at the specified index.</summary>
		/// <returns>The ACE at the specified index.</returns>
		/// <param name="index">The zero-based index of the ACE to get or set.</param>
		// Token: 0x17000A67 RID: 2663
		public override GenericAce this[int index]
		{
			get
			{
				return this.list[index];
			}
			set
			{
				this.list[index] = value;
			}
		}

		/// <summary>Gets the revision level of the <see cref="T:System.Security.AccessControl.RawAcl" />.</summary>
		/// <returns>A byte value that specifies the revision level of the <see cref="T:System.Security.AccessControl.RawAcl" />.</returns>
		// Token: 0x17000A68 RID: 2664
		// (get) Token: 0x060036FA RID: 14074 RVA: 0x000B2718 File Offset: 0x000B0918
		public override byte Revision
		{
			get
			{
				return this.revision;
			}
		}

		/// <summary>Marshals the contents of the <see cref="T:System.Security.AccessControl.RawAcl" /> object into the specified byte array beginning at the specified offset.</summary>
		/// <param name="binaryForm">The byte array into which the contents of the <see cref="T:System.Security.AccessControl.RawAcl" /> is marshaled.</param>
		/// <param name="offset">The offset at which to start marshaling.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> is negative or too high to allow the entire <see cref="T:System.Security.AccessControl.RawAcl" /> to be copied into <paramref name="array" />.</exception>
		// Token: 0x060036FB RID: 14075 RVA: 0x000B2720 File Offset: 0x000B0920
		[MonoTODO]
		public override void GetBinaryForm(byte[] binaryForm, int offset)
		{
			throw new NotImplementedException();
		}

		/// <summary>Inserts the specified Access Control Entry (ACE) at the specified index.</summary>
		/// <param name="index">The position at which to add the new ACE. Specify the value of the <see cref="P:System.Security.AccessControl.RawAcl.Count" /> property to insert an ACE at the end of the <see cref="T:System.Security.AccessControl.RawAcl" /> object.</param>
		/// <param name="ace">The ACE to insert.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="offset" /> is negative or too high to allow the entire <see cref="T:System.Security.AccessControl.GenericAcl" /> to be copied into <paramref name="array" />.</exception>
		// Token: 0x060036FC RID: 14076 RVA: 0x000B2728 File Offset: 0x000B0928
		public void InsertAce(int index, GenericAce ace)
		{
			if (ace == null)
			{
				throw new ArgumentNullException("ace");
			}
			this.list.Insert(index, ace);
		}

		/// <summary>Removes the Access Control Entry (ACE) at the specified location.</summary>
		/// <param name="index">The zero-based index of the ACE to remove.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The value of the <paramref name="index" /> parameter is higher than the value of the <see cref="P:System.Security.AccessControl.RawAcl.Count" /> property minus one or is negative.</exception>
		// Token: 0x060036FD RID: 14077 RVA: 0x000B275C File Offset: 0x000B095C
		public void RemoveAce(int index)
		{
			this.list.RemoveAt(index);
		}

		// Token: 0x04001748 RID: 5960
		private byte revision;

		// Token: 0x04001749 RID: 5961
		private List<GenericAce> list;
	}
}
