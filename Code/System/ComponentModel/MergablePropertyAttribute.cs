using System;

namespace System.ComponentModel
{
	/// <summary>Specifies that this property can be combined with properties belonging to other objects in a Properties window.</summary>
	// Token: 0x0200018C RID: 396
	[AttributeUsage(AttributeTargets.All)]
	public sealed class MergablePropertyAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.MergablePropertyAttribute" /> class.</summary>
		/// <param name="allowMerge">true if this property can be combined with properties belonging to other objects in a Properties window; otherwise, false. </param>
		// Token: 0x06000DDE RID: 3550 RVA: 0x00023E14 File Offset: 0x00022014
		public MergablePropertyAttribute(bool allowMerge)
		{
			this.mergable = allowMerge;
		}

		/// <summary>Gets a value indicating whether this property can be combined with properties belonging to other objects in a Properties window.</summary>
		/// <returns>true if this property can be combined with properties belonging to other objects in a Properties window; otherwise, false.</returns>
		// Token: 0x17000332 RID: 818
		// (get) Token: 0x06000DE0 RID: 3552 RVA: 0x00023E48 File Offset: 0x00022048
		public bool AllowMerge
		{
			get
			{
				return this.mergable;
			}
		}

		/// <summary>Indicates whether this instance and a specified object are equal.</summary>
		/// <returns>true if <paramref name="obj" /> is equal to this instance; otherwise, false.</returns>
		/// <param name="obj">Another object to compare to. </param>
		// Token: 0x06000DE1 RID: 3553 RVA: 0x00023E50 File Offset: 0x00022050
		public override bool Equals(object obj)
		{
			return obj is MergablePropertyAttribute && (obj == this || ((MergablePropertyAttribute)obj).AllowMerge == this.mergable);
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A hash code for the current <see cref="T:System.ComponentModel.MergablePropertyAttribute" />.</returns>
		// Token: 0x06000DE2 RID: 3554 RVA: 0x00023E7C File Offset: 0x0002207C
		public override int GetHashCode()
		{
			return this.mergable.GetHashCode();
		}

		/// <summary>Determines if this attribute is the default.</summary>
		/// <returns>true if the attribute is the default value for this attribute class; otherwise, false.</returns>
		// Token: 0x06000DE3 RID: 3555 RVA: 0x00023E8C File Offset: 0x0002208C
		public override bool IsDefaultAttribute()
		{
			return this.mergable == MergablePropertyAttribute.Default.AllowMerge;
		}

		// Token: 0x040003EB RID: 1003
		private bool mergable;

		/// <summary>Specifies the default value, which is <see cref="F:System.ComponentModel.MergablePropertyAttribute.Yes" />, that is a property can be combined with properties belonging to other objects in a Properties window. This static field is read-only.</summary>
		// Token: 0x040003EC RID: 1004
		public static readonly MergablePropertyAttribute Default = new MergablePropertyAttribute(true);

		/// <summary>Specifies that a property cannot be combined with properties belonging to other objects in a Properties window. This static field is read-only.</summary>
		// Token: 0x040003ED RID: 1005
		public static readonly MergablePropertyAttribute No = new MergablePropertyAttribute(false);

		/// <summary>Specifies that a property can be combined with properties belonging to other objects in a Properties window. This static field is read-only.</summary>
		// Token: 0x040003EE RID: 1006
		public static readonly MergablePropertyAttribute Yes = new MergablePropertyAttribute(true);
	}
}
