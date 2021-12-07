using System;

namespace System.ComponentModel
{
	/// <summary>Specifies the properties that support lookup-based binding. This class cannot be inherited.</summary>
	// Token: 0x02000183 RID: 387
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class LookupBindingPropertiesAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.LookupBindingPropertiesAttribute" /> class. </summary>
		/// <param name="dataSource">The name of the property to be used as the data source.</param>
		/// <param name="displayMember">The name of the property to be used for the display name.</param>
		/// <param name="valueMember">The name of the property to be used as the source for values.</param>
		/// <param name="lookupMember">The name of the property to be used for lookups.</param>
		// Token: 0x06000D3C RID: 3388 RVA: 0x00020F48 File Offset: 0x0001F148
		public LookupBindingPropertiesAttribute(string dataSource, string displayMember, string valueMember, string lookupMember)
		{
			this.data_source = dataSource;
			this.display_member = displayMember;
			this.value_member = valueMember;
			this.lookup_member = lookupMember;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.LookupBindingPropertiesAttribute" /> class using no parameters. </summary>
		// Token: 0x06000D3D RID: 3389 RVA: 0x00020F70 File Offset: 0x0001F170
		public LookupBindingPropertiesAttribute()
		{
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A hash code for the current <see cref="T:System.ComponentModel.LookupBindingPropertiesAttribute" />.</returns>
		// Token: 0x06000D3F RID: 3391 RVA: 0x00020F84 File Offset: 0x0001F184
		public override int GetHashCode()
		{
			return ((this.data_source == null) ? 1 : this.data_source.GetHashCode()) << 24 + ((this.display_member == null) ? 1 : this.display_member.GetHashCode()) << 16 + ((this.lookup_member == null) ? 1 : this.lookup_member.GetHashCode()) << 8 + ((this.value_member == null) ? 1 : this.value_member.GetHashCode());
		}

		/// <summary>Determines whether the specified <see cref="T:System.Object" /> is equal to the current <see cref="T:System.ComponentModel.LookupBindingPropertiesAttribute" /> instance. </summary>
		/// <returns>true if the object is equal to the current instance; otherwise, false, indicating they are not equal.</returns>
		/// <param name="obj">The <see cref="T:System.Object" /> to compare with the current <see cref="T:System.ComponentModel.LookupBindingPropertiesAttribute" /> instance </param>
		// Token: 0x06000D40 RID: 3392 RVA: 0x00021018 File Offset: 0x0001F218
		public override bool Equals(object obj)
		{
			LookupBindingPropertiesAttribute lookupBindingPropertiesAttribute = obj as LookupBindingPropertiesAttribute;
			return lookupBindingPropertiesAttribute != null && !(this.data_source != lookupBindingPropertiesAttribute.data_source) && !(this.display_member != lookupBindingPropertiesAttribute.display_member) && !(this.value_member != lookupBindingPropertiesAttribute.value_member) && !(this.lookup_member != lookupBindingPropertiesAttribute.lookup_member);
		}

		/// <summary>Gets the name of the data source property for the component to which the <see cref="T:System.ComponentModel.LookupBindingPropertiesAttribute" /> is bound.</summary>
		/// <returns>The data source property for the component to which the <see cref="T:System.ComponentModel.LookupBindingPropertiesAttribute" /> is bound.</returns>
		// Token: 0x17000303 RID: 771
		// (get) Token: 0x06000D41 RID: 3393 RVA: 0x00021094 File Offset: 0x0001F294
		public string DataSource
		{
			get
			{
				return this.data_source;
			}
		}

		/// <summary>Gets the name of the display member property for the component to which the <see cref="T:System.ComponentModel.LookupBindingPropertiesAttribute" /> is bound.</summary>
		/// <returns>The name of the display member property for the component to which the <see cref="T:System.ComponentModel.LookupBindingPropertiesAttribute" /> is bound.</returns>
		// Token: 0x17000304 RID: 772
		// (get) Token: 0x06000D42 RID: 3394 RVA: 0x0002109C File Offset: 0x0001F29C
		public string DisplayMember
		{
			get
			{
				return this.display_member;
			}
		}

		/// <summary>Gets the name of the lookup member for the component to which this attribute is bound.</summary>
		/// <returns>The name of the lookup member for the component to which the <see cref="T:System.ComponentModel.LookupBindingPropertiesAttribute" /> is bound.</returns>
		// Token: 0x17000305 RID: 773
		// (get) Token: 0x06000D43 RID: 3395 RVA: 0x000210A4 File Offset: 0x0001F2A4
		public string LookupMember
		{
			get
			{
				return this.lookup_member;
			}
		}

		/// <summary>Gets the name of the value member property for the component to which the <see cref="T:System.ComponentModel.LookupBindingPropertiesAttribute" /> is bound.</summary>
		/// <returns>The name of the value member property for the component to which the <see cref="T:System.ComponentModel.LookupBindingPropertiesAttribute" /> is bound.</returns>
		// Token: 0x17000306 RID: 774
		// (get) Token: 0x06000D44 RID: 3396 RVA: 0x000210AC File Offset: 0x0001F2AC
		public string ValueMember
		{
			get
			{
				return this.value_member;
			}
		}

		// Token: 0x040003A7 RID: 935
		private string data_source;

		// Token: 0x040003A8 RID: 936
		private string display_member;

		// Token: 0x040003A9 RID: 937
		private string value_member;

		// Token: 0x040003AA RID: 938
		private string lookup_member;

		/// <summary>Represents the default value for the <see cref="T:System.ComponentModel.LookupBindingPropertiesAttribute" /> class.</summary>
		// Token: 0x040003AB RID: 939
		public static readonly LookupBindingPropertiesAttribute Default = new LookupBindingPropertiesAttribute();
	}
}
