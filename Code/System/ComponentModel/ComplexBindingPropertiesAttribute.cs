using System;

namespace System.ComponentModel
{
	/// <summary>Specifies the data source and data member properties for a component that supports complex data binding. This class cannot be inherited.</summary>
	// Token: 0x020000DB RID: 219
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
	public sealed class ComplexBindingPropertiesAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.ComplexBindingPropertiesAttribute" /> class using the specified data source and data member. </summary>
		/// <param name="dataSource">The name of the property to be used as the data source.</param>
		/// <param name="dataMember">The name of the property to be used as the source for data.</param>
		// Token: 0x06000958 RID: 2392 RVA: 0x0001B3E0 File Offset: 0x000195E0
		public ComplexBindingPropertiesAttribute(string dataSource, string dataMember)
		{
			this.data_source = dataSource;
			this.data_member = dataMember;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.ComplexBindingPropertiesAttribute" /> class using the specified data source. </summary>
		/// <param name="dataSource">The name of the property to be used as the data source.</param>
		// Token: 0x06000959 RID: 2393 RVA: 0x0001B3F8 File Offset: 0x000195F8
		public ComplexBindingPropertiesAttribute(string dataSource)
		{
			this.data_source = dataSource;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.ComplexBindingPropertiesAttribute" /> class using no parameters. </summary>
		// Token: 0x0600095A RID: 2394 RVA: 0x0001B408 File Offset: 0x00019608
		public ComplexBindingPropertiesAttribute()
		{
		}

		/// <summary>Gets the name of the data member property for the component to which the <see cref="T:System.ComponentModel.ComplexBindingPropertiesAttribute" /> is bound.</summary>
		/// <returns>The name of the data member property for the component to which <see cref="T:System.ComponentModel.ComplexBindingPropertiesAttribute" /> is bound</returns>
		// Token: 0x1700021D RID: 541
		// (get) Token: 0x0600095C RID: 2396 RVA: 0x0001B41C File Offset: 0x0001961C
		public string DataMember
		{
			get
			{
				return this.data_member;
			}
		}

		/// <summary>Gets the name of the data source property for the component to which the <see cref="T:System.ComponentModel.ComplexBindingPropertiesAttribute" /> is bound.</summary>
		/// <returns>The name of the data source property for the component to which <see cref="T:System.ComponentModel.ComplexBindingPropertiesAttribute" /> is bound.</returns>
		// Token: 0x1700021E RID: 542
		// (get) Token: 0x0600095D RID: 2397 RVA: 0x0001B424 File Offset: 0x00019624
		public string DataSource
		{
			get
			{
				return this.data_source;
			}
		}

		/// <summary>Determines whether the specified <see cref="T:System.Object" /> is equal to the current <see cref="T:System.ComponentModel.ComplexBindingPropertiesAttribute" /> instance. </summary>
		/// <returns>true if the object is equal to the current instance; otherwise, false, indicating they are not equal.</returns>
		/// <param name="obj">The <see cref="T:System.Object" /> to compare with the current <see cref="T:System.ComponentModel.ComplexBindingPropertiesAttribute" /> instance </param>
		// Token: 0x0600095E RID: 2398 RVA: 0x0001B42C File Offset: 0x0001962C
		public override bool Equals(object obj)
		{
			ComplexBindingPropertiesAttribute complexBindingPropertiesAttribute = obj as ComplexBindingPropertiesAttribute;
			return complexBindingPropertiesAttribute != null && complexBindingPropertiesAttribute.DataMember == this.data_member && complexBindingPropertiesAttribute.DataSource == this.data_source;
		}

		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x0600095F RID: 2399 RVA: 0x0001B474 File Offset: 0x00019674
		public override int GetHashCode()
		{
			int hashCode = (this.data_source + this.data_member).GetHashCode();
			if (hashCode == 0)
			{
				return base.GetHashCode();
			}
			return hashCode;
		}

		// Token: 0x0400027F RID: 639
		private string data_source;

		// Token: 0x04000280 RID: 640
		private string data_member;

		/// <summary>Represents the default value for the <see cref="T:System.ComponentModel.ComplexBindingPropertiesAttribute" /> class.</summary>
		// Token: 0x04000281 RID: 641
		public static readonly ComplexBindingPropertiesAttribute Default = new ComplexBindingPropertiesAttribute();
	}
}
