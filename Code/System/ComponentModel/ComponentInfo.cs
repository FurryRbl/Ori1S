using System;
using System.Collections;
using System.ComponentModel.Design;
using System.Reflection;

namespace System.ComponentModel
{
	// Token: 0x020001BC RID: 444
	internal class ComponentInfo : Info
	{
		// Token: 0x06000FAC RID: 4012 RVA: 0x000292AC File Offset: 0x000274AC
		public ComponentInfo(IComponent component) : base(component.GetType())
		{
			this._component = component;
		}

		// Token: 0x06000FAD RID: 4013 RVA: 0x000292C4 File Offset: 0x000274C4
		public override AttributeCollection GetAttributes()
		{
			return base.GetAttributes(this._component);
		}

		// Token: 0x06000FAE RID: 4014 RVA: 0x000292D4 File Offset: 0x000274D4
		public override EventDescriptorCollection GetEvents()
		{
			if (this._events != null)
			{
				return this._events;
			}
			bool flag = true;
			EventInfo[] events = this._component.GetType().GetEvents();
			Hashtable hashtable = new Hashtable();
			foreach (EventInfo eventInfo in events)
			{
				hashtable[eventInfo.Name] = new ReflectionEventDescriptor(eventInfo);
			}
			if (this._component.Site != null)
			{
				System.ComponentModel.Design.ITypeDescriptorFilterService typeDescriptorFilterService = (System.ComponentModel.Design.ITypeDescriptorFilterService)this._component.Site.GetService(typeof(System.ComponentModel.Design.ITypeDescriptorFilterService));
				if (typeDescriptorFilterService != null)
				{
					flag = typeDescriptorFilterService.FilterEvents(this._component, hashtable);
				}
			}
			ArrayList arrayList = new ArrayList();
			arrayList.AddRange(hashtable.Values);
			EventDescriptorCollection eventDescriptorCollection = new EventDescriptorCollection(arrayList);
			if (flag)
			{
				this._events = eventDescriptorCollection;
			}
			return eventDescriptorCollection;
		}

		// Token: 0x06000FAF RID: 4015 RVA: 0x000293B4 File Offset: 0x000275B4
		public override PropertyDescriptorCollection GetProperties()
		{
			if (this._properties != null)
			{
				return this._properties;
			}
			bool flag = true;
			PropertyInfo[] properties = this._component.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
			Hashtable hashtable = new Hashtable();
			for (int i = properties.Length - 1; i >= 0; i--)
			{
				hashtable[properties[i].Name] = new ReflectionPropertyDescriptor(properties[i]);
			}
			if (this._component.Site != null)
			{
				System.ComponentModel.Design.ITypeDescriptorFilterService typeDescriptorFilterService = (System.ComponentModel.Design.ITypeDescriptorFilterService)this._component.Site.GetService(typeof(System.ComponentModel.Design.ITypeDescriptorFilterService));
				if (typeDescriptorFilterService != null)
				{
					flag = typeDescriptorFilterService.FilterProperties(this._component, hashtable);
				}
			}
			PropertyDescriptor[] array = new PropertyDescriptor[hashtable.Values.Count];
			hashtable.Values.CopyTo(array, 0);
			PropertyDescriptorCollection propertyDescriptorCollection = new PropertyDescriptorCollection(array, true);
			if (flag)
			{
				this._properties = propertyDescriptorCollection;
			}
			return propertyDescriptorCollection;
		}

		// Token: 0x04000462 RID: 1122
		private IComponent _component;

		// Token: 0x04000463 RID: 1123
		private EventDescriptorCollection _events;

		// Token: 0x04000464 RID: 1124
		private PropertyDescriptorCollection _properties;
	}
}
