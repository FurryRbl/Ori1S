using System;
using System.Collections;
using System.ComponentModel.Design;

namespace System.ComponentModel
{
	// Token: 0x020001BB RID: 443
	internal abstract class Info
	{
		// Token: 0x06000FA2 RID: 4002 RVA: 0x00028F2C File Offset: 0x0002712C
		public Info(Type infoType)
		{
			this._infoType = infoType;
		}

		// Token: 0x06000FA3 RID: 4003
		public abstract AttributeCollection GetAttributes();

		// Token: 0x06000FA4 RID: 4004
		public abstract EventDescriptorCollection GetEvents();

		// Token: 0x06000FA5 RID: 4005
		public abstract PropertyDescriptorCollection GetProperties();

		// Token: 0x17000381 RID: 897
		// (get) Token: 0x06000FA6 RID: 4006 RVA: 0x00028F3C File Offset: 0x0002713C
		public Type InfoType
		{
			get
			{
				return this._infoType;
			}
		}

		// Token: 0x06000FA7 RID: 4007 RVA: 0x00028F44 File Offset: 0x00027144
		public EventDescriptorCollection GetEvents(Attribute[] attributes)
		{
			EventDescriptorCollection events = this.GetEvents();
			if (attributes == null)
			{
				return events;
			}
			return events.Filter(attributes);
		}

		// Token: 0x06000FA8 RID: 4008 RVA: 0x00028F68 File Offset: 0x00027168
		public PropertyDescriptorCollection GetProperties(Attribute[] attributes)
		{
			PropertyDescriptorCollection properties = this.GetProperties();
			if (attributes == null)
			{
				return properties;
			}
			return properties.Filter(attributes);
		}

		// Token: 0x06000FA9 RID: 4009 RVA: 0x00028F8C File Offset: 0x0002718C
		public EventDescriptor GetDefaultEvent()
		{
			if (this._gotDefaultEvent)
			{
				return this._defaultEvent;
			}
			DefaultEventAttribute defaultEventAttribute = (DefaultEventAttribute)this.GetAttributes()[typeof(DefaultEventAttribute)];
			if (defaultEventAttribute == null || defaultEventAttribute.Name == null)
			{
				this._defaultEvent = null;
			}
			else
			{
				EventDescriptorCollection events = this.GetEvents();
				this._defaultEvent = events[defaultEventAttribute.Name];
			}
			this._gotDefaultEvent = true;
			return this._defaultEvent;
		}

		// Token: 0x06000FAA RID: 4010 RVA: 0x0002900C File Offset: 0x0002720C
		public PropertyDescriptor GetDefaultProperty()
		{
			if (this._gotDefaultProperty)
			{
				return this._defaultProperty;
			}
			DefaultPropertyAttribute defaultPropertyAttribute = (DefaultPropertyAttribute)this.GetAttributes()[typeof(DefaultPropertyAttribute)];
			if (defaultPropertyAttribute == null || defaultPropertyAttribute.Name == null)
			{
				this._defaultProperty = null;
			}
			else
			{
				PropertyDescriptorCollection properties = this.GetProperties();
				this._defaultProperty = properties[defaultPropertyAttribute.Name];
			}
			this._gotDefaultProperty = true;
			return this._defaultProperty;
		}

		// Token: 0x06000FAB RID: 4011 RVA: 0x0002908C File Offset: 0x0002728C
		protected AttributeCollection GetAttributes(IComponent comp)
		{
			if (this._attributes != null)
			{
				return this._attributes;
			}
			bool flag = true;
			ArrayList arrayList = new ArrayList();
			foreach (Attribute value in this._infoType.GetCustomAttributes(false))
			{
				arrayList.Add(value);
			}
			Type baseType = this._infoType.BaseType;
			while (baseType != null && baseType != typeof(object))
			{
				foreach (Attribute value2 in baseType.GetCustomAttributes(false))
				{
					arrayList.Add(value2);
				}
				baseType = baseType.BaseType;
			}
			foreach (Type componentType in this._infoType.GetInterfaces())
			{
				foreach (object obj in TypeDescriptor.GetAttributes(componentType))
				{
					Attribute value3 = (Attribute)obj;
					arrayList.Add(value3);
				}
			}
			Hashtable hashtable = new Hashtable();
			for (int l = arrayList.Count - 1; l >= 0; l--)
			{
				Attribute attribute = (Attribute)arrayList[l];
				hashtable[attribute.TypeId] = attribute;
			}
			if (comp != null && comp.Site != null)
			{
				System.ComponentModel.Design.ITypeDescriptorFilterService typeDescriptorFilterService = (System.ComponentModel.Design.ITypeDescriptorFilterService)comp.Site.GetService(typeof(System.ComponentModel.Design.ITypeDescriptorFilterService));
				if (typeDescriptorFilterService != null)
				{
					flag = typeDescriptorFilterService.FilterAttributes(comp, hashtable);
				}
			}
			Attribute[] array = new Attribute[hashtable.Values.Count];
			hashtable.Values.CopyTo(array, 0);
			AttributeCollection attributeCollection = new AttributeCollection(array);
			if (flag)
			{
				this._attributes = attributeCollection;
			}
			return attributeCollection;
		}

		// Token: 0x0400045C RID: 1116
		private Type _infoType;

		// Token: 0x0400045D RID: 1117
		private EventDescriptor _defaultEvent;

		// Token: 0x0400045E RID: 1118
		private bool _gotDefaultEvent;

		// Token: 0x0400045F RID: 1119
		private PropertyDescriptor _defaultProperty;

		// Token: 0x04000460 RID: 1120
		private bool _gotDefaultProperty;

		// Token: 0x04000461 RID: 1121
		private AttributeCollection _attributes;
	}
}
