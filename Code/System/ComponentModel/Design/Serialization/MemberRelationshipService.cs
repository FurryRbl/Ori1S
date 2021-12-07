using System;
using System.Collections;

namespace System.ComponentModel.Design.Serialization
{
	/// <summary>Provides the base class for relating one member to another.</summary>
	// Token: 0x02000134 RID: 308
	public abstract class MemberRelationshipService
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.Design.Serialization.MemberRelationshipService" /> class. </summary>
		// Token: 0x06000B92 RID: 2962 RVA: 0x0001E480 File Offset: 0x0001C680
		protected MemberRelationshipService()
		{
			this._relations = new Hashtable();
		}

		/// <summary>Gets a value indicating whether the given relationship is supported.</summary>
		/// <returns>true if a relationship between the given two objects is supported; otherwise, false.</returns>
		/// <param name="source">The source relationship.</param>
		/// <param name="relationship">The relationship to set into the source.</param>
		// Token: 0x06000B93 RID: 2963
		public abstract bool SupportsRelationship(MemberRelationship source, MemberRelationship relationship);

		/// <summary>Gets a relationship to the given source relationship.</summary>
		/// <returns>A relationship to <paramref name="source" />, or <see cref="F:System.ComponentModel.Design.Serialization.MemberRelationship.Empty" /> if no relationship exists.</returns>
		/// <param name="source">The source relationship.</param>
		// Token: 0x06000B94 RID: 2964 RVA: 0x0001E494 File Offset: 0x0001C694
		protected virtual MemberRelationship GetRelationship(MemberRelationship source)
		{
			if (source.IsEmpty)
			{
				throw new ArgumentNullException("source");
			}
			MemberRelationshipService.MemberRelationshipWeakEntry memberRelationshipWeakEntry = this._relations[new MemberRelationshipService.MemberRelationshipWeakEntry(source)] as MemberRelationshipService.MemberRelationshipWeakEntry;
			if (memberRelationshipWeakEntry != null)
			{
				return new MemberRelationship(memberRelationshipWeakEntry.Owner, memberRelationshipWeakEntry.Member);
			}
			return MemberRelationship.Empty;
		}

		/// <summary>Creates a relationship between the source object and target relationship.</summary>
		/// <param name="source">The source relationship.</param>
		/// <param name="relationship">The relationship to set into the source.</param>
		/// <exception cref="T:System.ArgumentException">The relationship is not supported by the service.</exception>
		// Token: 0x06000B95 RID: 2965 RVA: 0x0001E4F4 File Offset: 0x0001C6F4
		protected virtual void SetRelationship(MemberRelationship source, MemberRelationship relationship)
		{
			if (source.IsEmpty)
			{
				throw new ArgumentNullException("source");
			}
			if (!relationship.IsEmpty && !this.SupportsRelationship(source, relationship))
			{
				throw new ArgumentException("Relationship not supported.");
			}
			this._relations[new MemberRelationshipService.MemberRelationshipWeakEntry(source)] = new MemberRelationshipService.MemberRelationshipWeakEntry(relationship);
		}

		/// <summary>Establishes a relationship between a source and target object.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.Design.Serialization.MemberRelationship" /> structure encapsulating the relationship between a source and target object, or null if there is no relationship.</returns>
		/// <param name="sourceOwner">The owner of a source relationship.</param>
		/// <param name="sourceMember">The member of a source relationship.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="sourceOwner" /> or <paramref name="sourceMember" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="sourceOwner" /> or <paramref name="sourceMember" /> is empty, or the relationship is not supported by the service.</exception>
		// Token: 0x17000297 RID: 663
		public MemberRelationship this[object owner, MemberDescriptor member]
		{
			get
			{
				return this.GetRelationship(new MemberRelationship(owner, member));
			}
			set
			{
				this.SetRelationship(new MemberRelationship(owner, member), value);
			}
		}

		/// <summary>Establishes a relationship between a source and target object.</summary>
		/// <returns>The current relationship associated with <paramref name="source" />, or <see cref="F:System.ComponentModel.Design.Serialization.MemberRelationship.Empty" /> if there is no relationship.</returns>
		/// <param name="source">The source relationship. This is the left-hand side of a relationship assignment.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="source" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="source" /> is empty, or the relationship is not supported by the service.</exception>
		// Token: 0x17000298 RID: 664
		public MemberRelationship this[MemberRelationship source]
		{
			get
			{
				return this.GetRelationship(source);
			}
			set
			{
				this.SetRelationship(source, value);
			}
		}

		// Token: 0x04000307 RID: 775
		private Hashtable _relations;

		// Token: 0x02000135 RID: 309
		private class MemberRelationshipWeakEntry
		{
			// Token: 0x06000B9A RID: 2970 RVA: 0x0001E58C File Offset: 0x0001C78C
			public MemberRelationshipWeakEntry(MemberRelationship relation)
			{
				this._ownerWeakRef = new WeakReference(relation.Owner);
				this._member = relation.Member;
			}

			// Token: 0x17000299 RID: 665
			// (get) Token: 0x06000B9B RID: 2971 RVA: 0x0001E5B4 File Offset: 0x0001C7B4
			public object Owner
			{
				get
				{
					if (this._ownerWeakRef.IsAlive)
					{
						return this._ownerWeakRef.Target;
					}
					return null;
				}
			}

			// Token: 0x1700029A RID: 666
			// (get) Token: 0x06000B9C RID: 2972 RVA: 0x0001E5D4 File Offset: 0x0001C7D4
			public MemberDescriptor Member
			{
				get
				{
					return this._member;
				}
			}

			// Token: 0x06000B9D RID: 2973 RVA: 0x0001E5DC File Offset: 0x0001C7DC
			public override int GetHashCode()
			{
				if (this.Owner != null && this._member != null)
				{
					return this._member.GetHashCode() ^ this._ownerWeakRef.Target.GetHashCode();
				}
				return base.GetHashCode();
			}

			// Token: 0x06000B9E RID: 2974 RVA: 0x0001E624 File Offset: 0x0001C824
			public override bool Equals(object o)
			{
				return o is MemberRelationshipService.MemberRelationshipWeakEntry && (MemberRelationshipService.MemberRelationshipWeakEntry)o == this;
			}

			// Token: 0x06000B9F RID: 2975 RVA: 0x0001E640 File Offset: 0x0001C840
			public static bool operator ==(MemberRelationshipService.MemberRelationshipWeakEntry left, MemberRelationshipService.MemberRelationshipWeakEntry right)
			{
				return left.Owner == right.Owner && left.Member == right.Member;
			}

			// Token: 0x06000BA0 RID: 2976 RVA: 0x0001E674 File Offset: 0x0001C874
			public static bool operator !=(MemberRelationshipService.MemberRelationshipWeakEntry left, MemberRelationshipService.MemberRelationshipWeakEntry right)
			{
				return !(left == right);
			}

			// Token: 0x04000308 RID: 776
			private WeakReference _ownerWeakRef;

			// Token: 0x04000309 RID: 777
			private MemberDescriptor _member;
		}
	}
}
