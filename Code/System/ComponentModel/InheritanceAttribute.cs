using System;

namespace System.ComponentModel
{
	/// <summary>Indicates whether the component associated with this attribute has been inherited from a base class. This class cannot be inherited.</summary>
	// Token: 0x0200015F RID: 351
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Event)]
	public sealed class InheritanceAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.InheritanceAttribute" /> class.</summary>
		// Token: 0x06000C9A RID: 3226 RVA: 0x0002024C File Offset: 0x0001E44C
		public InheritanceAttribute()
		{
			this.level = InheritanceLevel.NotInherited;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.InheritanceAttribute" /> class with the specified inheritance level.</summary>
		/// <param name="inheritanceLevel">An <see cref="T:System.ComponentModel.InheritanceLevel" /> that indicates the level of inheritance to set this attribute to. </param>
		// Token: 0x06000C9B RID: 3227 RVA: 0x0002025C File Offset: 0x0001E45C
		public InheritanceAttribute(InheritanceLevel inheritanceLevel)
		{
			this.level = inheritanceLevel;
		}

		/// <summary>Gets or sets the current inheritance level stored in this attribute.</summary>
		/// <returns>The <see cref="T:System.ComponentModel.InheritanceLevel" /> stored in this attribute.</returns>
		// Token: 0x170002DB RID: 731
		// (get) Token: 0x06000C9D RID: 3229 RVA: 0x0002029C File Offset: 0x0001E49C
		public InheritanceLevel InheritanceLevel
		{
			get
			{
				return this.level;
			}
		}

		/// <summary>Override to test for equality.</summary>
		/// <returns>true if the object is the same; otherwise, false.</returns>
		/// <param name="value">The object to test. </param>
		// Token: 0x06000C9E RID: 3230 RVA: 0x000202A4 File Offset: 0x0001E4A4
		public override bool Equals(object obj)
		{
			return obj is InheritanceAttribute && (obj == this || ((InheritanceAttribute)obj).InheritanceLevel == this.level);
		}

		/// <summary>Returns the hashcode for this object.</summary>
		/// <returns>A hash code for the current <see cref="T:System.ComponentModel.InheritanceAttribute" />.</returns>
		// Token: 0x06000C9F RID: 3231 RVA: 0x000202D0 File Offset: 0x0001E4D0
		public override int GetHashCode()
		{
			return this.level.GetHashCode();
		}

		/// <summary>Gets a value indicating whether the current value of the attribute is the default value for the attribute.</summary>
		/// <returns>true if the current value of the attribute is the default; otherwise, false.</returns>
		// Token: 0x06000CA0 RID: 3232 RVA: 0x000202E4 File Offset: 0x0001E4E4
		public override bool IsDefaultAttribute()
		{
			return this.level == InheritanceAttribute.Default.InheritanceLevel;
		}

		/// <summary>Converts this attribute to a string.</summary>
		/// <returns>A string that represents this <see cref="T:System.ComponentModel.InheritanceAttribute" />.</returns>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x06000CA1 RID: 3233 RVA: 0x000202F8 File Offset: 0x0001E4F8
		public override string ToString()
		{
			return this.level.ToString();
		}

		// Token: 0x04000377 RID: 887
		private InheritanceLevel level;

		/// <summary>Specifies that the default value for <see cref="T:System.ComponentModel.InheritanceAttribute" /> is <see cref="F:System.ComponentModel.InheritanceAttribute.NotInherited" />. This field is read-only.</summary>
		// Token: 0x04000378 RID: 888
		public static readonly InheritanceAttribute Default = new InheritanceAttribute();

		/// <summary>Specifies that the component is inherited. This field is read-only.</summary>
		// Token: 0x04000379 RID: 889
		public static readonly InheritanceAttribute Inherited = new InheritanceAttribute(InheritanceLevel.Inherited);

		/// <summary>Specifies that the component is inherited and is read-only. This field is read-only.</summary>
		// Token: 0x0400037A RID: 890
		public static readonly InheritanceAttribute InheritedReadOnly = new InheritanceAttribute(InheritanceLevel.InheritedReadOnly);

		/// <summary>Specifies that the component is not inherited. This field is read-only.</summary>
		// Token: 0x0400037B RID: 891
		public static readonly InheritanceAttribute NotInherited = new InheritanceAttribute(InheritanceLevel.NotInherited);
	}
}
