using System;

namespace System.ComponentModel
{
	/// <summary>Top level mapping layer between a COM object and TypeDescriptor.</summary>
	// Token: 0x02000153 RID: 339
	[Obsolete("Use TypeDescriptionProvider and TypeDescriptor.ComObjectType instead")]
	public interface IComNativeDescriptorHandler
	{
		// Token: 0x06000C68 RID: 3176
		AttributeCollection GetAttributes(object component);

		// Token: 0x06000C69 RID: 3177
		string GetClassName(object component);

		// Token: 0x06000C6A RID: 3178
		TypeConverter GetConverter(object component);

		// Token: 0x06000C6B RID: 3179
		EventDescriptor GetDefaultEvent(object component);

		// Token: 0x06000C6C RID: 3180
		PropertyDescriptor GetDefaultProperty(object component);

		// Token: 0x06000C6D RID: 3181
		object GetEditor(object component, Type baseEditorType);

		// Token: 0x06000C6E RID: 3182
		EventDescriptorCollection GetEvents(object component);

		// Token: 0x06000C6F RID: 3183
		EventDescriptorCollection GetEvents(object component, Attribute[] attributes);

		// Token: 0x06000C70 RID: 3184
		string GetName(object component);

		// Token: 0x06000C71 RID: 3185
		PropertyDescriptorCollection GetProperties(object component, Attribute[] attributes);

		/// <summary>Retrieves the value of the property that has the specified dispatch identifier.</summary>
		/// <param name="component">The object to which the property belongs.</param>
		/// <param name="dispid">The dispatch identifier.</param>
		/// <param name="success">A <see cref="T:System.Boolean" />, passed by reference, that represents whether or not the property was retrieved. </param>
		// Token: 0x06000C72 RID: 3186
		object GetPropertyValue(object component, int dispid, ref bool success);

		/// <summary>Retrieves the value of the property that has the specified name.</summary>
		/// <param name="component">The object to which the property belongs.</param>
		/// <param name="propertyName">The name of the property.</param>
		/// <param name="success">A <see cref="T:System.Boolean" />, passed by reference, that represents whether or not the property was retrieved. </param>
		// Token: 0x06000C73 RID: 3187
		object GetPropertyValue(object component, string propertyName, ref bool success);
	}
}
