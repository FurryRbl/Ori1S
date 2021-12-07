using System;

namespace System.ComponentModel
{
	/// <summary>Provides a simple default implementation of the <see cref="T:System.ComponentModel.ICustomTypeDescriptor" /> interface.</summary>
	// Token: 0x020000E6 RID: 230
	public abstract class CustomTypeDescriptor : ICustomTypeDescriptor
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.CustomTypeDescriptor" /> class.</summary>
		// Token: 0x0600099D RID: 2461 RVA: 0x0001BF54 File Offset: 0x0001A154
		protected CustomTypeDescriptor()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.ComponentModel.CustomTypeDescriptor" /> class using a parent custom type descriptor.</summary>
		/// <param name="parent">The parent custom type descriptor.</param>
		// Token: 0x0600099E RID: 2462 RVA: 0x0001BF5C File Offset: 0x0001A15C
		protected CustomTypeDescriptor(ICustomTypeDescriptor parent)
		{
			this._parent = parent;
		}

		/// <summary>Returns a collection of custom attributes for the type represented by this type descriptor.</summary>
		/// <returns>An <see cref="T:System.ComponentModel.AttributeCollection" /> containing the attributes for the type. The default is <see cref="F:System.ComponentModel.AttributeCollection.Empty" />.</returns>
		// Token: 0x0600099F RID: 2463 RVA: 0x0001BF6C File Offset: 0x0001A16C
		public virtual AttributeCollection GetAttributes()
		{
			if (this._parent != null)
			{
				return this._parent.GetAttributes();
			}
			return AttributeCollection.Empty;
		}

		/// <summary>Returns the fully qualified name of the class represented by this type descriptor.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the fully qualified class name of the type this type descriptor is describing. The default is null.</returns>
		// Token: 0x060009A0 RID: 2464 RVA: 0x0001BF8C File Offset: 0x0001A18C
		public virtual string GetClassName()
		{
			if (this._parent != null)
			{
				return this._parent.GetClassName();
			}
			return null;
		}

		/// <summary>Returns the name of the class represented by this type descriptor.</summary>
		/// <returns>A <see cref="T:System.String" /> containing the name of the component instance this type descriptor is describing. The default is null.</returns>
		// Token: 0x060009A1 RID: 2465 RVA: 0x0001BFA8 File Offset: 0x0001A1A8
		public virtual string GetComponentName()
		{
			if (this._parent != null)
			{
				return this._parent.GetComponentName();
			}
			return null;
		}

		/// <summary>Returns a type converter for the type represented by this type descriptor.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.TypeConverter" /> for the type represented by this type descriptor. The default is a newly created <see cref="T:System.ComponentModel.TypeConverter" />.</returns>
		// Token: 0x060009A2 RID: 2466 RVA: 0x0001BFC4 File Offset: 0x0001A1C4
		public virtual TypeConverter GetConverter()
		{
			if (this._parent != null)
			{
				return this._parent.GetConverter();
			}
			return new TypeConverter();
		}

		/// <summary>Returns the event descriptor for the default event of the object represented by this type descriptor.</summary>
		/// <returns>The <see cref="T:System.ComponentModel.EventDescriptor" /> for the default event on the object represented by this type descriptor. The default is null.</returns>
		// Token: 0x060009A3 RID: 2467 RVA: 0x0001BFE4 File Offset: 0x0001A1E4
		public virtual EventDescriptor GetDefaultEvent()
		{
			if (this._parent != null)
			{
				return this._parent.GetDefaultEvent();
			}
			return null;
		}

		/// <summary>Returns the property descriptor for the default property of the object represented by this type descriptor.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptor" /> for the default property on the object represented by this type descriptor. The default is null.</returns>
		// Token: 0x060009A4 RID: 2468 RVA: 0x0001C000 File Offset: 0x0001A200
		public virtual PropertyDescriptor GetDefaultProperty()
		{
			if (this._parent != null)
			{
				return this._parent.GetDefaultProperty();
			}
			return null;
		}

		/// <summary>Returns an editor of the specified type that is to be associated with the class represented by this type descriptor.</summary>
		/// <returns>An editor of the given type that is to be associated with the class represented by this type descriptor. The default is null.</returns>
		/// <param name="editorBaseType">The base type of the editor to retrieve.</param>
		// Token: 0x060009A5 RID: 2469 RVA: 0x0001C01C File Offset: 0x0001A21C
		public virtual object GetEditor(Type editorBaseType)
		{
			if (this._parent != null)
			{
				return this._parent.GetEditor(editorBaseType);
			}
			return null;
		}

		/// <summary>Returns a collection of event descriptors for the object represented by this type descriptor.</summary>
		/// <returns>An <see cref="T:System.ComponentModel.EventDescriptorCollection" /> containing the event descriptors for the object represented by this type descriptor. The default is <see cref="F:System.ComponentModel.EventDescriptorCollection.Empty" />.</returns>
		// Token: 0x060009A6 RID: 2470 RVA: 0x0001C038 File Offset: 0x0001A238
		public virtual EventDescriptorCollection GetEvents()
		{
			if (this._parent != null)
			{
				return this._parent.GetEvents();
			}
			return EventDescriptorCollection.Empty;
		}

		/// <summary>Returns a filtered collection of event descriptors for the object represented by this type descriptor.</summary>
		/// <returns>An <see cref="T:System.ComponentModel.EventDescriptorCollection" /> containing the event descriptions for the object represented by this type descriptor. The default is <see cref="F:System.ComponentModel.EventDescriptorCollection.Empty" />.</returns>
		/// <param name="attributes">An array of attributes to use as a filter. This can be null.</param>
		// Token: 0x060009A7 RID: 2471 RVA: 0x0001C058 File Offset: 0x0001A258
		public virtual EventDescriptorCollection GetEvents(Attribute[] attributes)
		{
			if (this._parent != null)
			{
				return this._parent.GetEvents(attributes);
			}
			return EventDescriptorCollection.Empty;
		}

		/// <summary>Returns a collection of property descriptors for the object represented by this type descriptor.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> containing the property descriptions for the object represented by this type descriptor. The default is <see cref="F:System.ComponentModel.PropertyDescriptorCollection.Empty" />.</returns>
		// Token: 0x060009A8 RID: 2472 RVA: 0x0001C078 File Offset: 0x0001A278
		public virtual PropertyDescriptorCollection GetProperties()
		{
			if (this._parent != null)
			{
				return this._parent.GetProperties();
			}
			return PropertyDescriptorCollection.Empty;
		}

		/// <summary>Returns a filtered collection of property descriptors for the object represented by this type descriptor.</summary>
		/// <returns>A <see cref="T:System.ComponentModel.PropertyDescriptorCollection" /> containing the property descriptions for the object represented by this type descriptor. The default is <see cref="F:System.ComponentModel.PropertyDescriptorCollection.Empty" />.</returns>
		/// <param name="attributes">An array of attributes to use as a filter. This can be null.</param>
		// Token: 0x060009A9 RID: 2473 RVA: 0x0001C098 File Offset: 0x0001A298
		public virtual PropertyDescriptorCollection GetProperties(Attribute[] attributes)
		{
			if (this._parent != null)
			{
				return this._parent.GetProperties(attributes);
			}
			return PropertyDescriptorCollection.Empty;
		}

		/// <summary>Returns an object that contains the property described by the specified property descriptor.</summary>
		/// <returns>An <see cref="T:System.Object" /> that owns the given property specified by the type descriptor. The default is null.</returns>
		/// <param name="pd">The property descriptor for which to retrieve the owning object.</param>
		// Token: 0x060009AA RID: 2474 RVA: 0x0001C0B8 File Offset: 0x0001A2B8
		public virtual object GetPropertyOwner(PropertyDescriptor pd)
		{
			if (this._parent != null)
			{
				return this._parent.GetPropertyOwner(pd);
			}
			return null;
		}

		// Token: 0x0400028A RID: 650
		private ICustomTypeDescriptor _parent;
	}
}
