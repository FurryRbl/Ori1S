using System;

namespace System.ComponentModel
{
	/// <summary>Specifies the default property for a component.</summary>
	// Token: 0x020000EF RID: 239
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class DefaultPropertyAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DefaultPropertyAttribute" /> class.</summary>
		/// <param name="name">The name of the default property for the component this attribute is bound to. </param>
		// Token: 0x060009D9 RID: 2521 RVA: 0x0001C798 File Offset: 0x0001A998
		public DefaultPropertyAttribute(string name)
		{
			this.property_name = name;
		}

		/// <summary>Gets the name of the default property for the component this attribute is bound to.</summary>
		/// <returns>The name of the default property for the component this attribute is bound to. The default value is null.</returns>
		// Token: 0x17000235 RID: 565
		// (get) Token: 0x060009DB RID: 2523 RVA: 0x0001C7B8 File Offset: 0x0001A9B8
		public string Name
		{
			get
			{
				return this.property_name;
			}
		}

		/// <summary>Returns whether the value of the given object is equal to the current <see cref="T:System.ComponentModel.DefaultPropertyAttribute" />.</summary>
		/// <returns>true if the value of the given object is equal to that of the current; otherwise, false.</returns>
		/// <param name="obj">The object to test the value equality of. </param>
		// Token: 0x060009DC RID: 2524 RVA: 0x0001C7C0 File Offset: 0x0001A9C0
		public override bool Equals(object o)
		{
			return o is DefaultPropertyAttribute && ((DefaultPropertyAttribute)o).Name == this.property_name;
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x060009DD RID: 2525 RVA: 0x0001C7E8 File Offset: 0x0001A9E8
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x0400029F RID: 671
		private string property_name;

		/// <summary>Specifies the default value for the <see cref="T:System.ComponentModel.DefaultPropertyAttribute" />, which is null. This static field is read-only.</summary>
		// Token: 0x040002A0 RID: 672
		public static readonly DefaultPropertyAttribute Default = new DefaultPropertyAttribute(null);
	}
}
