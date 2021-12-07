using System;

namespace System.ComponentModel
{
	/// <summary>Specifies the display name for a property, event, or public void method which takes no arguments. </summary>
	// Token: 0x0200013F RID: 319
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Event)]
	public class DisplayNameAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DisplayNameAttribute" /> class.</summary>
		// Token: 0x06000BCD RID: 3021 RVA: 0x0001EF54 File Offset: 0x0001D154
		public DisplayNameAttribute()
		{
			this.attributeDisplayName = string.Empty;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DisplayNameAttribute" /> class using the display name.</summary>
		/// <param name="displayName">The display name.</param>
		// Token: 0x06000BCE RID: 3022 RVA: 0x0001EF68 File Offset: 0x0001D168
		public DisplayNameAttribute(string displayName)
		{
			this.attributeDisplayName = displayName;
		}

		/// <summary>Determines if this attribute is the default.</summary>
		/// <returns>true if the attribute is the default value for this attribute class; otherwise, false.</returns>
		// Token: 0x06000BD0 RID: 3024 RVA: 0x0001EF84 File Offset: 0x0001D184
		public override bool IsDefaultAttribute()
		{
			return this.attributeDisplayName != null && this.attributeDisplayName.Length == 0;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A hash code for the current <see cref="T:System.ComponentModel.DisplayNameAttribute" />.</returns>
		// Token: 0x06000BD1 RID: 3025 RVA: 0x0001EFA4 File Offset: 0x0001D1A4
		public override int GetHashCode()
		{
			return this.attributeDisplayName.GetHashCode();
		}

		/// <summary>Determines whether two <see cref="T:System.ComponentModel.DisplayNameAttribute" /> instances are equal.</summary>
		/// <returns>true if the value of the given object is equal to that of the current object; otherwise, false.</returns>
		/// <param name="obj">The <see cref="T:System.ComponentModel.DisplayNameAttribute" /> to test the value equality of.</param>
		// Token: 0x06000BD2 RID: 3026 RVA: 0x0001EFB4 File Offset: 0x0001D1B4
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			DisplayNameAttribute displayNameAttribute = obj as DisplayNameAttribute;
			return displayNameAttribute != null && displayNameAttribute.DisplayName == this.attributeDisplayName;
		}

		/// <summary>Gets the display name for a property, event, or public void method that takes no arguments stored in this attribute.</summary>
		/// <returns>The display name.</returns>
		// Token: 0x170002A6 RID: 678
		// (get) Token: 0x06000BD3 RID: 3027 RVA: 0x0001EFEC File Offset: 0x0001D1EC
		public virtual string DisplayName
		{
			get
			{
				return this.attributeDisplayName;
			}
		}

		/// <summary>Gets or sets the display name.</summary>
		/// <returns>The display name.</returns>
		// Token: 0x170002A7 RID: 679
		// (get) Token: 0x06000BD4 RID: 3028 RVA: 0x0001EFF4 File Offset: 0x0001D1F4
		// (set) Token: 0x06000BD5 RID: 3029 RVA: 0x0001EFFC File Offset: 0x0001D1FC
		protected string DisplayNameValue
		{
			get
			{
				return this.attributeDisplayName;
			}
			set
			{
				this.attributeDisplayName = value;
			}
		}

		/// <summary>Specifies the default value for the <see cref="T:System.ComponentModel.DisplayNameAttribute" />. This field is read-only.</summary>
		// Token: 0x0400035A RID: 858
		public static readonly DisplayNameAttribute Default = new DisplayNameAttribute();

		// Token: 0x0400035B RID: 859
		private string attributeDisplayName;
	}
}
