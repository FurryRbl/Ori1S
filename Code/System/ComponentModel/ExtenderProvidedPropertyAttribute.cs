using System;

namespace System.ComponentModel
{
	/// <summary>Specifies a property that is offered by an extender provider. This class cannot be inherited.</summary>
	// Token: 0x0200014C RID: 332
	[AttributeUsage(AttributeTargets.All)]
	public sealed class ExtenderProvidedPropertyAttribute : Attribute
	{
		// Token: 0x06000C3C RID: 3132 RVA: 0x0001FEF4 File Offset: 0x0001E0F4
		internal static ExtenderProvidedPropertyAttribute CreateAttribute(PropertyDescriptor extenderProperty, IExtenderProvider provider, Type receiverType)
		{
			return new ExtenderProvidedPropertyAttribute
			{
				extender = extenderProperty,
				receiver = receiverType,
				extenderProvider = provider
			};
		}

		/// <summary>Gets the property that is being provided.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptor" /> encapsulating the property that is being provided.</returns>
		// Token: 0x170002C0 RID: 704
		// (get) Token: 0x06000C3D RID: 3133 RVA: 0x0001FF20 File Offset: 0x0001E120
		public PropertyDescriptor ExtenderProperty
		{
			get
			{
				return this.extender;
			}
		}

		/// <summary>Gets the extender provider that is providing the property.</summary>
		/// <returns>The <see cref="T:System.ComponentModel.IExtenderProvider" /> that is providing the property.</returns>
		// Token: 0x170002C1 RID: 705
		// (get) Token: 0x06000C3E RID: 3134 RVA: 0x0001FF28 File Offset: 0x0001E128
		public IExtenderProvider Provider
		{
			get
			{
				return this.extenderProvider;
			}
		}

		/// <summary>Gets the type of object that can receive the property.</summary>
		/// <returns>A <see cref="T:System.Type" /> describing the type of object that can receive the property.</returns>
		// Token: 0x170002C2 RID: 706
		// (get) Token: 0x06000C3F RID: 3135 RVA: 0x0001FF30 File Offset: 0x0001E130
		public Type ReceiverType
		{
			get
			{
				return this.receiver;
			}
		}

		/// <summary>Provides an indication whether the value of this instance is the default value for the derived class.</summary>
		/// <returns>true if this instance is the default attribute for the class; otherwise, false.</returns>
		// Token: 0x06000C40 RID: 3136 RVA: 0x0001FF38 File Offset: 0x0001E138
		public override bool IsDefaultAttribute()
		{
			return this.extender == null && this.extenderProvider == null && this.receiver == null;
		}

		/// <summary>Determines whether the specified <see cref="T:System.Object" /> is equal to the current <see cref="T:System.Object" />.</summary>
		/// <returns>true if the specified <see cref="T:System.Object" /> is equal to the current <see cref="T:System.Object" />; otherwise, false.</returns>
		/// <param name="obj">An <see cref="T:System.Object" /> to compare with this instance or null. </param>
		// Token: 0x06000C41 RID: 3137 RVA: 0x0001FF68 File Offset: 0x0001E168
		public override bool Equals(object obj)
		{
			return obj is ExtenderProvidedPropertyAttribute && (obj == this || (((ExtenderProvidedPropertyAttribute)obj).ExtenderProperty.Equals(this.extender) && ((ExtenderProvidedPropertyAttribute)obj).Provider.Equals(this.extenderProvider) && ((ExtenderProvidedPropertyAttribute)obj).ReceiverType.Equals(this.receiver)));
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A 32-bit signed integer hash code.</returns>
		// Token: 0x06000C42 RID: 3138 RVA: 0x0001FFDC File Offset: 0x0001E1DC
		public override int GetHashCode()
		{
			return this.extender.GetHashCode() ^ this.extenderProvider.GetHashCode() ^ this.receiver.GetHashCode();
		}

		// Token: 0x0400036F RID: 879
		private PropertyDescriptor extender;

		// Token: 0x04000370 RID: 880
		private IExtenderProvider extenderProvider;

		// Token: 0x04000371 RID: 881
		private Type receiver;
	}
}
