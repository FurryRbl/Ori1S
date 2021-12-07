using System;

namespace System.ComponentModel
{
	/// <summary>Specifies whether the property this attribute is bound to is read-only or read/write. This class cannot be inherited</summary>
	// Token: 0x0200019B RID: 411
	[AttributeUsage(AttributeTargets.All)]
	public sealed class ReadOnlyAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.ReadOnlyAttribute" /> class.</summary>
		/// <param name="isReadOnly">true to show that the property this attribute is bound to is read-only; false to show that the property is read/write. </param>
		// Token: 0x06000E86 RID: 3718 RVA: 0x00025520 File Offset: 0x00023720
		public ReadOnlyAttribute(bool read_only)
		{
			this.read_only = read_only;
		}

		/// <summary>Gets a value indicating whether the property this attribute is bound to is read-only.</summary>
		/// <returns>true if the property this attribute is bound to is read-only; false if the property is read/write.</returns>
		// Token: 0x1700035D RID: 861
		// (get) Token: 0x06000E88 RID: 3720 RVA: 0x00025554 File Offset: 0x00023754
		public bool IsReadOnly
		{
			get
			{
				return this.read_only;
			}
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A hash code for the current <see cref="T:System.ComponentModel.ReadOnlyAttribute" />.</returns>
		// Token: 0x06000E89 RID: 3721 RVA: 0x0002555C File Offset: 0x0002375C
		public override int GetHashCode()
		{
			return this.read_only.GetHashCode();
		}

		/// <summary>Indicates whether this instance and a specified object are equal.</summary>
		/// <returns>true if <paramref name="value" /> is equal to this instance; otherwise, false.</returns>
		/// <param name="value">Another object to compare to. </param>
		// Token: 0x06000E8A RID: 3722 RVA: 0x0002556C File Offset: 0x0002376C
		public override bool Equals(object o)
		{
			return o is ReadOnlyAttribute && ((ReadOnlyAttribute)o).IsReadOnly.Equals(this.read_only);
		}

		/// <summary>Determines if this attribute is the default.</summary>
		/// <returns>true if the attribute is the default value for this attribute class; otherwise, false.</returns>
		// Token: 0x06000E8B RID: 3723 RVA: 0x000255A0 File Offset: 0x000237A0
		public override bool IsDefaultAttribute()
		{
			return this.Equals(ReadOnlyAttribute.Default);
		}

		// Token: 0x04000410 RID: 1040
		private bool read_only;

		/// <summary>Specifies that the property this attribute is bound to is read/write and can be modified. This static field is read-only.</summary>
		// Token: 0x04000411 RID: 1041
		public static readonly ReadOnlyAttribute No = new ReadOnlyAttribute(false);

		/// <summary>Specifies that the property this attribute is bound to is read-only and cannot be modified in the server explorer. This static field is read-only.</summary>
		// Token: 0x04000412 RID: 1042
		public static readonly ReadOnlyAttribute Yes = new ReadOnlyAttribute(true);

		/// <summary>Specifies the default value for the <see cref="T:System.ComponentModel.ReadOnlyAttribute" />, which is <see cref="F:System.ComponentModel.ReadOnlyAttribute.No" /> (that is, the property this attribute is bound to is read/write). This static field is read-only.</summary>
		// Token: 0x04000413 RID: 1043
		public static readonly ReadOnlyAttribute Default = new ReadOnlyAttribute(false);
	}
}
