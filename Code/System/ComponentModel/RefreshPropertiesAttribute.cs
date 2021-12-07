using System;

namespace System.ComponentModel
{
	/// <summary>Indicates that the property grid should refresh when the associated property value changes. This class cannot be inherited.</summary>
	// Token: 0x020001A1 RID: 417
	[AttributeUsage(AttributeTargets.All)]
	public sealed class RefreshPropertiesAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.RefreshPropertiesAttribute" /> class.</summary>
		/// <param name="refresh">A <see cref="T:System.ComponentModel.RefreshProperties" /> value indicating the nature of the refresh.</param>
		// Token: 0x06000EB8 RID: 3768 RVA: 0x00026428 File Offset: 0x00024628
		public RefreshPropertiesAttribute(RefreshProperties refresh)
		{
			this.refresh = refresh;
		}

		/// <summary>Gets the refresh properties for the member.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.RefreshProperties" /> that indicates the current refresh properties for the member.</returns>
		// Token: 0x17000367 RID: 871
		// (get) Token: 0x06000EBA RID: 3770 RVA: 0x0002645C File Offset: 0x0002465C
		public RefreshProperties RefreshProperties
		{
			get
			{
				return this.refresh;
			}
		}

		/// <summary>Overrides the object's <see cref="Overload:System.Object.Equals" /> method.</summary>
		/// <returns>true if the specified object is the same; otherwise, false.</returns>
		/// <param name="value">The object to test for equality. </param>
		// Token: 0x06000EBB RID: 3771 RVA: 0x00026464 File Offset: 0x00024664
		public override bool Equals(object obj)
		{
			return obj is RefreshPropertiesAttribute && (obj == this || ((RefreshPropertiesAttribute)obj).RefreshProperties == this.refresh);
		}

		/// <summary>Returns the hash code for this object.</summary>
		/// <returns>The hash code for the object that the attribute belongs to.</returns>
		// Token: 0x06000EBC RID: 3772 RVA: 0x00026490 File Offset: 0x00024690
		public override int GetHashCode()
		{
			return this.refresh.GetHashCode();
		}

		/// <summary>Gets a value indicating whether the current value of the attribute is the default value for the attribute.</summary>
		/// <returns>true if the current value of the attribute is the default; otherwise, false.</returns>
		// Token: 0x06000EBD RID: 3773 RVA: 0x000264A4 File Offset: 0x000246A4
		public override bool IsDefaultAttribute()
		{
			return this == RefreshPropertiesAttribute.Default;
		}

		// Token: 0x04000426 RID: 1062
		private RefreshProperties refresh;

		/// <summary>Indicates that all properties are queried again and refreshed if the property value is changed. This field is read-only.</summary>
		// Token: 0x04000427 RID: 1063
		public static readonly RefreshPropertiesAttribute All = new RefreshPropertiesAttribute(RefreshProperties.All);

		/// <summary>Indicates that no other properties are refreshed if the property value is changed. This field is read-only.</summary>
		// Token: 0x04000428 RID: 1064
		public static readonly RefreshPropertiesAttribute Default = new RefreshPropertiesAttribute(RefreshProperties.None);

		/// <summary>Indicates that all properties are repainted if the property value is changed. This field is read-only.</summary>
		// Token: 0x04000429 RID: 1065
		public static readonly RefreshPropertiesAttribute Repaint = new RefreshPropertiesAttribute(RefreshProperties.Repaint);
	}
}
