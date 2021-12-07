using System;

namespace System.ComponentModel
{
	/// <summary>Specifies the default binding property for a component. This class cannot be inherited.</summary>
	// Token: 0x020000ED RID: 237
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class DefaultBindingPropertyAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DefaultBindingPropertyAttribute" /> class using no parameters. </summary>
		// Token: 0x060009CE RID: 2510 RVA: 0x0001C6DC File Offset: 0x0001A8DC
		public DefaultBindingPropertyAttribute()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.DefaultBindingPropertyAttribute" /> class using the specified property name.</summary>
		/// <param name="name">The name of the default binding property.</param>
		// Token: 0x060009CF RID: 2511 RVA: 0x0001C6E4 File Offset: 0x0001A8E4
		public DefaultBindingPropertyAttribute(string name)
		{
			this.name = name;
		}

		/// <summary>Determines whether the specified <see cref="T:System.Object" /> is equal to the current <see cref="T:System.ComponentModel.DefaultBindingPropertyAttribute" /> instance. </summary>
		/// <returns>true if the object is equal to the current instance; otherwise, false, indicating they are not equal.</returns>
		/// <param name="obj">The <see cref="T:System.Object" /> to compare with the current <see cref="T:System.ComponentModel.DefaultBindingPropertyAttribute" /> instance</param>
		// Token: 0x060009D1 RID: 2513 RVA: 0x0001C700 File Offset: 0x0001A900
		public override bool Equals(object obj)
		{
			DefaultBindingPropertyAttribute defaultBindingPropertyAttribute = obj as DefaultBindingPropertyAttribute;
			return obj != null && this.name == defaultBindingPropertyAttribute.Name;
		}

		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x060009D2 RID: 2514 RVA: 0x0001C730 File Offset: 0x0001A930
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		/// <summary>Gets the name of the default binding property for the component to which the <see cref="T:System.ComponentModel.DefaultBindingPropertyAttribute" /> is bound.</summary>
		/// <returns>The name of the default binding property for the component to which the <see cref="T:System.ComponentModel.DefaultBindingPropertyAttribute" /> is bound.</returns>
		// Token: 0x17000233 RID: 563
		// (get) Token: 0x060009D3 RID: 2515 RVA: 0x0001C738 File Offset: 0x0001A938
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		/// <summary>Represents the default value for the <see cref="T:System.ComponentModel.DefaultBindingPropertyAttribute" /> class.</summary>
		// Token: 0x0400029B RID: 667
		public static readonly DefaultBindingPropertyAttribute Default = new DefaultBindingPropertyAttribute();

		// Token: 0x0400029C RID: 668
		private string name;
	}
}
