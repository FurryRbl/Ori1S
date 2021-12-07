using System;

namespace System.ComponentModel
{
	/// <summary>Specifies the <see cref="T:System.ComponentModel.LicenseProvider" /> to use with a class. This class cannot be inherited.</summary>
	// Token: 0x02000177 RID: 375
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
	public sealed class LicenseProviderAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.LicenseProviderAttribute" /> class without a license provider.</summary>
		// Token: 0x06000CFD RID: 3325 RVA: 0x000209E4 File Offset: 0x0001EBE4
		public LicenseProviderAttribute()
		{
			this.Provider = null;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.LicenseProviderAttribute" /> class with the specified type.</summary>
		/// <param name="typeName">The fully qualified name of the license provider class. </param>
		// Token: 0x06000CFE RID: 3326 RVA: 0x000209F4 File Offset: 0x0001EBF4
		public LicenseProviderAttribute(string typeName)
		{
			this.Provider = Type.GetType(typeName, false);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.LicenseProviderAttribute" /> class with the specified type of license provider.</summary>
		/// <param name="type">A <see cref="T:System.Type" /> that represents the type of the license provider class. </param>
		// Token: 0x06000CFF RID: 3327 RVA: 0x00020A0C File Offset: 0x0001EC0C
		public LicenseProviderAttribute(Type type)
		{
			this.Provider = type;
		}

		/// <summary>Gets the license provider that must be used with the associated class.</summary>
		/// <returns>A <see cref="T:System.Type" /> that represents the type of the license provider. The default value is null.</returns>
		// Token: 0x170002F1 RID: 753
		// (get) Token: 0x06000D01 RID: 3329 RVA: 0x00020A28 File Offset: 0x0001EC28
		public Type LicenseProvider
		{
			get
			{
				return this.Provider;
			}
		}

		/// <summary>Indicates a unique ID for this attribute type.</summary>
		/// <returns>A unique ID for this attribute type.</returns>
		// Token: 0x170002F2 RID: 754
		// (get) Token: 0x06000D02 RID: 3330 RVA: 0x00020A30 File Offset: 0x0001EC30
		public override object TypeId
		{
			get
			{
				return base.ToString() + ((this.Provider == null) ? null : this.Provider.ToString());
			}
		}

		/// <summary>Indicates whether this instance and a specified object are equal.</summary>
		/// <returns>true if <paramref name="value" /> is equal to this instance; otherwise, false.</returns>
		/// <param name="value">Another object to compare to. </param>
		// Token: 0x06000D03 RID: 3331 RVA: 0x00020A5C File Offset: 0x0001EC5C
		public override bool Equals(object obj)
		{
			return obj is LicenseProviderAttribute && (obj == this || ((LicenseProviderAttribute)obj).LicenseProvider.Equals(this.Provider));
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A hash code for the current <see cref="T:System.ComponentModel.LicenseProviderAttribute" />.</returns>
		// Token: 0x06000D04 RID: 3332 RVA: 0x00020A98 File Offset: 0x0001EC98
		public override int GetHashCode()
		{
			return this.Provider.GetHashCode();
		}

		// Token: 0x04000386 RID: 902
		private Type Provider;

		/// <summary>Specifies the default value, which is no provider. This static field is read-only.</summary>
		// Token: 0x04000387 RID: 903
		public static readonly LicenseProviderAttribute Default = new LicenseProviderAttribute();
	}
}
