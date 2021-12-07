using System;
using System.Collections;
using System.Reflection;

namespace System.ComponentModel
{
	// Token: 0x020001BD RID: 445
	internal class TypeInfo : Info
	{
		// Token: 0x06000FB0 RID: 4016 RVA: 0x0002949C File Offset: 0x0002769C
		public TypeInfo(Type t) : base(t)
		{
		}

		// Token: 0x06000FB1 RID: 4017 RVA: 0x000294A8 File Offset: 0x000276A8
		public override AttributeCollection GetAttributes()
		{
			return base.GetAttributes(null);
		}

		// Token: 0x06000FB2 RID: 4018 RVA: 0x000294B4 File Offset: 0x000276B4
		public override EventDescriptorCollection GetEvents()
		{
			if (this._events != null)
			{
				return this._events;
			}
			EventInfo[] events = base.InfoType.GetEvents();
			EventDescriptor[] array = new EventDescriptor[events.Length];
			for (int i = 0; i < events.Length; i++)
			{
				array[i] = new ReflectionEventDescriptor(events[i]);
			}
			this._events = new EventDescriptorCollection(array);
			return this._events;
		}

		// Token: 0x06000FB3 RID: 4019 RVA: 0x0002951C File Offset: 0x0002771C
		public override PropertyDescriptorCollection GetProperties()
		{
			if (this._properties != null)
			{
				return this._properties;
			}
			Hashtable hashtable = new Hashtable();
			ArrayList arrayList = new ArrayList();
			Type type = base.InfoType;
			while (type != null && type != typeof(object))
			{
				PropertyInfo[] properties = type.GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public);
				foreach (PropertyInfo propertyInfo in properties)
				{
					if (propertyInfo.GetIndexParameters().Length == 0 && propertyInfo.CanRead && !hashtable.ContainsKey(propertyInfo.Name))
					{
						arrayList.Add(new ReflectionPropertyDescriptor(propertyInfo));
						hashtable.Add(propertyInfo.Name, null);
					}
				}
				type = type.BaseType;
			}
			this._properties = new PropertyDescriptorCollection((PropertyDescriptor[])arrayList.ToArray(typeof(PropertyDescriptor)), true);
			return this._properties;
		}

		// Token: 0x04000465 RID: 1125
		private EventDescriptorCollection _events;

		// Token: 0x04000466 RID: 1126
		private PropertyDescriptorCollection _properties;
	}
}
