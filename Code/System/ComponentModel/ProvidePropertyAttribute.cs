using System;

namespace System.ComponentModel
{
	/// <summary>Specifies the name of the property that an implementer of <see cref="T:System.ComponentModel.IExtenderProvider" /> offers to other components. This class cannot be inherited</summary>
	// Token: 0x0200019A RID: 410
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
	public sealed class ProvidePropertyAttribute : Attribute
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.ProvidePropertyAttribute" /> class with the name of the property and the type of its receiver.</summary>
		/// <param name="propertyName">The name of the property extending to an object of the specified type. </param>
		/// <param name="receiverTypeName">The name of the data type this property can extend. </param>
		// Token: 0x06000E7F RID: 3711 RVA: 0x00025458 File Offset: 0x00023658
		public ProvidePropertyAttribute(string propertyName, string receiverTypeName)
		{
			this.Property = propertyName;
			this.Receiver = receiverTypeName;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.ProvidePropertyAttribute" /> class with the name of the property and its <see cref="T:System.Type" />.</summary>
		/// <param name="propertyName">The name of the property extending to an object of the specified type. </param>
		/// <param name="receiverType">The <see cref="T:System.Type" /> of the data type of the object that can receive the property. </param>
		// Token: 0x06000E80 RID: 3712 RVA: 0x00025470 File Offset: 0x00023670
		public ProvidePropertyAttribute(string propertyName, Type receiverType)
		{
			this.Property = propertyName;
			this.Receiver = receiverType.AssemblyQualifiedName;
		}

		/// <summary>Gets the name of a property that this class provides.</summary>
		/// <returns>The name of a property that this class provides.</returns>
		// Token: 0x1700035A RID: 858
		// (get) Token: 0x06000E81 RID: 3713 RVA: 0x0002548C File Offset: 0x0002368C
		public string PropertyName
		{
			get
			{
				return this.Property;
			}
		}

		/// <summary>Gets the name of the data type this property can extend.</summary>
		/// <returns>The name of the data type this property can extend.</returns>
		// Token: 0x1700035B RID: 859
		// (get) Token: 0x06000E82 RID: 3714 RVA: 0x00025494 File Offset: 0x00023694
		public string ReceiverTypeName
		{
			get
			{
				return this.Receiver;
			}
		}

		/// <summary>Gets a unique identifier for this attribute.</summary>
		/// <returns>An <see cref="T:System.Object" /> that is a unique identifier for the attribute.</returns>
		// Token: 0x1700035C RID: 860
		// (get) Token: 0x06000E83 RID: 3715 RVA: 0x0002549C File Offset: 0x0002369C
		public override object TypeId
		{
			get
			{
				return base.TypeId + this.Property;
			}
		}

		/// <summary>Returns whether the value of the given object is equal to the current <see cref="T:System.ComponentModel.ProvidePropertyAttribute" />.</summary>
		/// <returns>true if the value of the given object is equal to that of the current; otherwise, false.</returns>
		/// <param name="obj">The object to test the value equality of. </param>
		// Token: 0x06000E84 RID: 3716 RVA: 0x000254B0 File Offset: 0x000236B0
		public override bool Equals(object obj)
		{
			return obj is ProvidePropertyAttribute && (obj == this || (((ProvidePropertyAttribute)obj).PropertyName == this.Property && ((ProvidePropertyAttribute)obj).ReceiverTypeName == this.Receiver));
		}

		/// <summary>Returns the hash code for this instance.</summary>
		/// <returns>A hash code for the current <see cref="T:System.ComponentModel.ProvidePropertyAttribute" />.</returns>
		// Token: 0x06000E85 RID: 3717 RVA: 0x00025508 File Offset: 0x00023708
		public override int GetHashCode()
		{
			return (this.Property + this.Receiver).GetHashCode();
		}

		// Token: 0x0400040E RID: 1038
		private string Property;

		// Token: 0x0400040F RID: 1039
		private string Receiver;
	}
}
