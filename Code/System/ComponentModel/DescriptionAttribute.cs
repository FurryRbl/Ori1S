using System;

namespace System.ComponentModel
{
	/// <summary>Specifies a description for a property or event.</summary>
	// Token: 0x020000F2 RID: 242
	[AttributeUsage(AttributeTargets.All)]
	public class DescriptionAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DescriptionAttribute" /> class with no parameters.</summary>
		// Token: 0x060009FA RID: 2554 RVA: 0x0001CA6C File Offset: 0x0001AC6C
		public DescriptionAttribute()
		{
			this.desc = string.Empty;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DescriptionAttribute" /> class with a description.</summary>
		/// <param name="description">The description text. </param>
		// Token: 0x060009FB RID: 2555 RVA: 0x0001CA80 File Offset: 0x0001AC80
		public DescriptionAttribute(string name)
		{
			this.desc = name;
		}

		/// <summary>Gets the description stored in this attribute.</summary>
		/// <returns>The description stored in this attribute.</returns>
		// Token: 0x1700023A RID: 570
		// (get) Token: 0x060009FD RID: 2557 RVA: 0x0001CA9C File Offset: 0x0001AC9C
		public virtual string Description
		{
			get
			{
				return this.DescriptionValue;
			}
		}

		/// <summary>Gets or sets the string stored as the description.</summary>
		/// <returns>The string stored as the description. The default value is an empty string ("").</returns>
		// Token: 0x1700023B RID: 571
		// (get) Token: 0x060009FE RID: 2558 RVA: 0x0001CAA4 File Offset: 0x0001ACA4
		// (set) Token: 0x060009FF RID: 2559 RVA: 0x0001CAAC File Offset: 0x0001ACAC
		protected string DescriptionValue
		{
			get
			{
				return this.desc;
			}
			set
			{
				this.desc = value;
			}
		}

		/// <summary>Returns whether the value of the given object is equal to the current <see cref="T:System.ComponentModel.DescriptionAttribute" />.</summary>
		/// <returns>true if the value of the given object is equal to that of the current; otherwise, false.</returns>
		/// <param name="obj">The object to test the value equality of. </param>
		// Token: 0x06000A00 RID: 2560 RVA: 0x0001CAB8 File Offset: 0x0001ACB8
		public override bool Equals(object obj)
		{
			return obj is DescriptionAttribute && (obj == this || ((DescriptionAttribute)obj).Description == this.desc);
		}

		// Token: 0x06000A01 RID: 2561 RVA: 0x0001CAF4 File Offset: 0x0001ACF4
		public override int GetHashCode()
		{
			return this.desc.GetHashCode();
		}

		/// <summary>Returns a value indicating whether this is the default <see cref="T:System.ComponentModel.DescriptionAttribute" /> instance.</summary>
		/// <returns>true, if this is the default <see cref="T:System.ComponentModel.DescriptionAttribute" /> instance; otherwise, false.</returns>
		// Token: 0x06000A02 RID: 2562 RVA: 0x0001CB04 File Offset: 0x0001AD04
		public override bool IsDefaultAttribute()
		{
			return this == DescriptionAttribute.Default;
		}

		// Token: 0x040002A6 RID: 678
		private string desc;

		/// <summary>Specifies the default value for the <see cref="T:System.ComponentModel.DescriptionAttribute" />, which is an empty string (""). This static field is read-only.</summary>
		// Token: 0x040002A7 RID: 679
		public static readonly DescriptionAttribute Default = new DescriptionAttribute();
	}
}
