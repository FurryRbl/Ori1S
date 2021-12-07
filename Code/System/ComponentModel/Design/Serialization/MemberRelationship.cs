using System;

namespace System.ComponentModel.Design.Serialization
{
	/// <summary>Represents a single relationship between an object and a member.</summary>
	// Token: 0x02000133 RID: 307
	public struct MemberRelationship
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.Serialization.MemberRelationship" /> class. </summary>
		/// <param name="owner">The object that owns <paramref name="member" />.</param>
		/// <param name="member">The member which is to be related to <paramref name="owner" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="owner" /> or <paramref name="member" /> is null.</exception>
		// Token: 0x06000B89 RID: 2953 RVA: 0x0001E388 File Offset: 0x0001C588
		public MemberRelationship(object owner, MemberDescriptor member)
		{
			this._owner = owner;
			this._member = member;
		}

		/// <summary>Gets a value indicating whether this relationship is equal to the <see cref="F:System.ComponentModel.Design.Serialization.MemberRelationship.Empty" /> relationship. </summary>
		/// <returns>true if this relationship is equal to the <see cref="F:System.ComponentModel.Design.Serialization.MemberRelationship.Empty" /> relationship; otherwise, false.</returns>
		// Token: 0x17000294 RID: 660
		// (get) Token: 0x06000B8B RID: 2955 RVA: 0x0001E3B4 File Offset: 0x0001C5B4
		public bool IsEmpty
		{
			get
			{
				return this._owner == null;
			}
		}

		/// <summary>Gets the owning object.</summary>
		/// <returns>The owning object that is passed in to the <see cref="M:System.ComponentModel.Design.Serialization.MemberRelationship.#ctor(System.Object,System.ComponentModel.MemberDescriptor)" />.</returns>
		// Token: 0x17000295 RID: 661
		// (get) Token: 0x06000B8C RID: 2956 RVA: 0x0001E3C0 File Offset: 0x0001C5C0
		public object Owner
		{
			get
			{
				return this._owner;
			}
		}

		/// <summary>Gets the related member.</summary>
		/// <returns>The member that is passed in to the <see cref="M:System.ComponentModel.Design.Serialization.MemberRelationship.#ctor(System.Object,System.ComponentModel.MemberDescriptor)" />.</returns>
		// Token: 0x17000296 RID: 662
		// (get) Token: 0x06000B8D RID: 2957 RVA: 0x0001E3C8 File Offset: 0x0001C5C8
		public MemberDescriptor Member
		{
			get
			{
				return this._member;
			}
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A hash code for the current <see cref="T:System.ComponentModel.Design.Serialization.MemberRelationship" />.</returns>
		// Token: 0x06000B8E RID: 2958 RVA: 0x0001E3D0 File Offset: 0x0001C5D0
		public override int GetHashCode()
		{
			if (this._owner != null && this._member != null)
			{
				return this._member.GetHashCode() ^ this._owner.GetHashCode();
			}
			return base.GetHashCode();
		}

		/// <summary>Determines whether two <see cref="T:System.ComponentModel.Design.Serialization.MemberRelationship" /> instances are equal.</summary>
		/// <returns>true if the specified <see cref="T:System.ComponentModel.Design.Serialization.MemberRelationship" /> is equal to the current <see cref="T:System.ComponentModel.Design.Serialization.MemberRelationship" />; otherwise, false.</returns>
		/// <param name="obj">The <see cref="T:System.ComponentModel.Design.Serialization.MemberRelationship" /> to compare with the current <see cref="T:System.ComponentModel.Design.Serialization.MemberRelationship" />.</param>
		// Token: 0x06000B8F RID: 2959 RVA: 0x0001E41C File Offset: 0x0001C61C
		public override bool Equals(object o)
		{
			return o is MemberRelationship && (MemberRelationship)o == this;
		}

		/// <summary>Tests whether two specified <see cref="T:System.ComponentModel.Design.Serialization.MemberRelationship" /> structures are equivalent.</summary>
		/// <returns>This operator returns true if the two <see cref="T:System.ComponentModel.Design.Serialization.MemberRelationship" /> structures are equal; otherwise, false.</returns>
		/// <param name="left">The <see cref="T:System.ComponentModel.Design.Serialization.MemberRelationship" /> structure that is to the left of the equality operator.</param>
		/// <param name="right">The <see cref="T:System.ComponentModel.Design.Serialization.MemberRelationship" /> structure that is to the right of the equality operator.</param>
		// Token: 0x06000B90 RID: 2960 RVA: 0x0001E43C File Offset: 0x0001C63C
		public static bool operator ==(MemberRelationship left, MemberRelationship right)
		{
			return left.Owner == right.Owner && left.Member == right.Member;
		}

		/// <summary>Tests whether two specified <see cref="T:System.ComponentModel.Design.Serialization.MemberRelationship" /> structures are different.</summary>
		/// <returns>This operator returns true if the two <see cref="T:System.ComponentModel.Design.Serialization.MemberRelationship" /> structures are different; otherwise, false.</returns>
		/// <param name="left">The <see cref="T:System.ComponentModel.Design.Serialization.MemberRelationship" /> structure that is to the left of the inequality operator.</param>
		/// <param name="right">The <see cref="T:System.ComponentModel.Design.Serialization.MemberRelationship" /> structure that is to the right of the inequality operator.</param>
		// Token: 0x06000B91 RID: 2961 RVA: 0x0001E474 File Offset: 0x0001C674
		public static bool operator !=(MemberRelationship left, MemberRelationship right)
		{
			return !(left == right);
		}

		/// <summary>Represents the empty member relationship. This field is read-only.</summary>
		// Token: 0x04000304 RID: 772
		public static readonly MemberRelationship Empty = default(MemberRelationship);

		// Token: 0x04000305 RID: 773
		private object _owner;

		// Token: 0x04000306 RID: 774
		private MemberDescriptor _member;
	}
}
