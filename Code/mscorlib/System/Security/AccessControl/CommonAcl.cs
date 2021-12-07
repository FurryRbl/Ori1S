using System;
using System.Collections.Generic;
using System.Security.Principal;

namespace System.Security.AccessControl
{
	/// <summary>Represents an access control list (ACL) and is the base class for the <see cref="T:System.Security.AccessControl.DiscretionaryAcl" /> and <see cref="T:System.Security.AccessControl.SystemAcl" /> classes.</summary>
	// Token: 0x02000562 RID: 1378
	public abstract class CommonAcl : GenericAcl
	{
		// Token: 0x060035AE RID: 13742 RVA: 0x000B1540 File Offset: 0x000AF740
		internal CommonAcl(bool isContainer, bool isDS, byte revision) : this(isContainer, isDS, revision, 10)
		{
		}

		// Token: 0x060035AF RID: 13743 RVA: 0x000B1550 File Offset: 0x000AF750
		internal CommonAcl(bool isContainer, bool isDS, byte revision, int capacity)
		{
			this.is_container = isContainer;
			this.is_ds = isDS;
			this.revision = revision;
			this.list = new List<GenericAce>(capacity);
		}

		/// <summary>Gets the length, in bytes, of the binary representation of the current <see cref="T:System.Security.AccessControl.CommonAcl" /> object. This length should be used before marshaling the access control list (ACL) into a binary array by using the <see cref="M:System.Security.AccessControl.CommonAcl.GetBinaryForm" /> method.</summary>
		/// <returns>The length, in bytes, of the binary representation of the current <see cref="T:System.Security.AccessControl.CommonAcl" /> object.</returns>
		// Token: 0x17000A0E RID: 2574
		// (get) Token: 0x060035B0 RID: 13744 RVA: 0x000B1588 File Offset: 0x000AF788
		[MonoTODO]
		public sealed override int BinaryLength
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Gets the number of access control entries (ACEs) in the current <see cref="T:System.Security.AccessControl.CommonAcl" /> object.</summary>
		/// <returns>The number of ACEs in the current <see cref="T:System.Security.AccessControl.CommonAcl" /> object.</returns>
		// Token: 0x17000A0F RID: 2575
		// (get) Token: 0x060035B1 RID: 13745 RVA: 0x000B1590 File Offset: 0x000AF790
		public sealed override int Count
		{
			get
			{
				return this.list.Count;
			}
		}

		/// <summary>Gets a Boolean value that specifies whether the access control entries (ACEs) in the current <see cref="T:System.Security.AccessControl.CommonAcl" /> object are in canonical order.</summary>
		/// <returns>true if the ACEs in the current <see cref="T:System.Security.AccessControl.CommonAcl" /> object are in canonical order; otherwise, false.</returns>
		// Token: 0x17000A10 RID: 2576
		// (get) Token: 0x060035B2 RID: 13746 RVA: 0x000B15A0 File Offset: 0x000AF7A0
		[MonoTODO]
		public bool IsCanonical
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		/// <summary>Sets whether the <see cref="T:System.Security.AccessControl.CommonAcl" /> object is a container. </summary>
		/// <returns>true if the current <see cref="T:System.Security.AccessControl.CommonAcl" /> object is a container.</returns>
		// Token: 0x17000A11 RID: 2577
		// (get) Token: 0x060035B3 RID: 13747 RVA: 0x000B15A8 File Offset: 0x000AF7A8
		public bool IsContainer
		{
			get
			{
				return this.is_container;
			}
		}

		/// <summary>Sets whether the current <see cref="T:System.Security.AccessControl.CommonAcl" /> object is a directory object access control list (ACL).</summary>
		/// <returns>true if the current <see cref="T:System.Security.AccessControl.CommonAcl" /> object is a directory object ACL.</returns>
		// Token: 0x17000A12 RID: 2578
		// (get) Token: 0x060035B4 RID: 13748 RVA: 0x000B15B0 File Offset: 0x000AF7B0
		public bool IsDS
		{
			get
			{
				return this.is_ds;
			}
		}

		/// <summary>Gets or sets the <see cref="T:System.Security.AccessControl.CommonAce" /> at the specified index.</summary>
		/// <returns>The <see cref="T:System.Security.AccessControl.CommonAce" /> at the specified index.</returns>
		/// <param name="index">The zero-based index of the <see cref="T:System.Security.AccessControl.CommonAce" /> to get or set.</param>
		// Token: 0x17000A13 RID: 2579
		public sealed override GenericAce this[int index]
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

		/// <summary>Gets the revision level of the <see cref="T:System.Security.AccessControl.CommonAcl" />.</summary>
		/// <returns>A byte value that specifies the revision level of the <see cref="T:System.Security.AccessControl.CommonAcl" />.</returns>
		// Token: 0x17000A14 RID: 2580
		// (get) Token: 0x060035B7 RID: 13751 RVA: 0x000B15D8 File Offset: 0x000AF7D8
		public sealed override byte Revision
		{
			get
			{
				return this.revision;
			}
		}

		/// <summary>Marshals the contents of the <see cref="T:System.Security.AccessControl.CommonAcl" /> object into the specified byte array beginning at the specified offset.</summary>
		/// <param name="binaryForm">The byte array into which the contents of the <see cref="T:System.Security.AccessControl.CommonAcl" /> is marshaled.</param>
		/// <param name="offset">The offset at which to start marshaling.</param>
		// Token: 0x060035B8 RID: 13752 RVA: 0x000B15E0 File Offset: 0x000AF7E0
		[MonoTODO]
		public sealed override void GetBinaryForm(byte[] binaryForm, int offset)
		{
			throw new NotImplementedException();
		}

		/// <summary>Removes all access control entries (ACEs) contained by this <see cref="T:System.Security.AccessControl.CommonAcl" /> object that are associated with the specified <see cref="T:System.Security.Principal.SecurityIdentifier" /> object.</summary>
		/// <param name="sid">The <see cref="T:System.Security.Principal.SecurityIdentifier" /> object to check for.</param>
		// Token: 0x060035B9 RID: 13753 RVA: 0x000B15E8 File Offset: 0x000AF7E8
		[MonoTODO]
		public void Purge(SecurityIdentifier sid)
		{
			throw new NotImplementedException();
		}

		/// <summary>Removes all inherited access control entries (ACEs) from this <see cref="T:System.Security.AccessControl.CommonAcl" /> object.</summary>
		// Token: 0x060035BA RID: 13754 RVA: 0x000B15F0 File Offset: 0x000AF7F0
		[MonoTODO]
		public void RemoveInheritedAces()
		{
			throw new NotImplementedException();
		}

		// Token: 0x040016BD RID: 5821
		private const int default_capacity = 10;

		// Token: 0x040016BE RID: 5822
		private bool is_container;

		// Token: 0x040016BF RID: 5823
		private bool is_ds;

		// Token: 0x040016C0 RID: 5824
		private byte revision;

		// Token: 0x040016C1 RID: 5825
		private List<GenericAce> list;
	}
}
