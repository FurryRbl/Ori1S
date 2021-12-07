using System;

namespace System.ComponentModel
{
	/// <summary>Specifies whether a property should be localized. This class cannot be inherited.</summary>
	// Token: 0x02000182 RID: 386
	[AttributeUsage(AttributeTargets.All)]
	public sealed class LocalizableAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.LocalizableAttribute" /> class.</summary>
		/// <param name="isLocalizable">true if a property should be localized; otherwise, false. </param>
		// Token: 0x06000D36 RID: 3382 RVA: 0x00020EBC File Offset: 0x0001F0BC
		public LocalizableAttribute(bool localizable)
		{
			this.localizable = localizable;
		}

		/// <summary>Gets a value indicating whether a property should be localized.</summary>
		/// <returns>true if a property should be localized; otherwise, false.</returns>
		// Token: 0x17000302 RID: 770
		// (get) Token: 0x06000D38 RID: 3384 RVA: 0x00020EF0 File Offset: 0x0001F0F0
		public bool IsLocalizable
		{
			get
			{
				return this.localizable;
			}
		}

		/// <summary>Returns whether the value of the given object is equal to the current <see cref="T:System.ComponentModel.LocalizableAttribute" />.</summary>
		/// <returns>true if the value of the given object is equal to that of the current; otherwise, false.</returns>
		/// <param name="obj">The object to test the value equality of. </param>
		// Token: 0x06000D39 RID: 3385 RVA: 0x00020EF8 File Offset: 0x0001F0F8
		public override bool Equals(object obj)
		{
			return obj is LocalizableAttribute && (obj == this || ((LocalizableAttribute)obj).IsLocalizable == this.localizable);
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A hash code for the current <see cref="T:System.ComponentModel.LocalizableAttribute" />.</returns>
		// Token: 0x06000D3A RID: 3386 RVA: 0x00020F24 File Offset: 0x0001F124
		public override int GetHashCode()
		{
			return this.localizable.GetHashCode();
		}

		/// <summary>Determines if this attribute is the default.</summary>
		/// <returns>true if the attribute is the default value for this attribute class; otherwise, false.</returns>
		// Token: 0x06000D3B RID: 3387 RVA: 0x00020F34 File Offset: 0x0001F134
		public override bool IsDefaultAttribute()
		{
			return this.localizable == LocalizableAttribute.Default.IsLocalizable;
		}

		// Token: 0x040003A3 RID: 931
		private bool localizable;

		/// <summary>Specifies the default value, which is <see cref="F:System.ComponentModel.LocalizableAttribute.No" />. This static field is read-only.</summary>
		// Token: 0x040003A4 RID: 932
		public static readonly LocalizableAttribute Default = new LocalizableAttribute(false);

		/// <summary>Specifies that a property should not be localized. This static field is read-only.</summary>
		// Token: 0x040003A5 RID: 933
		public static readonly LocalizableAttribute No = new LocalizableAttribute(false);

		/// <summary>Specifies that a property should be localized. This static field is read-only.</summary>
		// Token: 0x040003A6 RID: 934
		public static readonly LocalizableAttribute Yes = new LocalizableAttribute(true);
	}
}
