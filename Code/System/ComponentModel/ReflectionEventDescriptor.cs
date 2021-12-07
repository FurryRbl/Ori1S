using System;
using System.Reflection;

namespace System.ComponentModel
{
	// Token: 0x0200019E RID: 414
	internal class ReflectionEventDescriptor : EventDescriptor
	{
		// Token: 0x06000E9A RID: 3738 RVA: 0x00025908 File Offset: 0x00023B08
		public ReflectionEventDescriptor(EventInfo eventInfo) : base(eventInfo.Name, (Attribute[])eventInfo.GetCustomAttributes(true))
		{
			this._eventInfo = eventInfo;
			this._componentType = eventInfo.DeclaringType;
			this._eventType = eventInfo.EventHandlerType;
			this.add_method = eventInfo.GetAddMethod();
			this.remove_method = eventInfo.GetRemoveMethod();
		}

		// Token: 0x06000E9B RID: 3739 RVA: 0x00025964 File Offset: 0x00023B64
		public ReflectionEventDescriptor(Type componentType, EventDescriptor oldEventDescriptor, Attribute[] attrs) : base(oldEventDescriptor, attrs)
		{
			this._componentType = componentType;
			this._eventType = oldEventDescriptor.EventType;
			EventInfo @event = componentType.GetEvent(oldEventDescriptor.Name);
			this.add_method = @event.GetAddMethod();
			this.remove_method = @event.GetRemoveMethod();
		}

		// Token: 0x06000E9C RID: 3740 RVA: 0x000259B4 File Offset: 0x00023BB4
		public ReflectionEventDescriptor(Type componentType, string name, Type type, Attribute[] attrs) : base(name, attrs)
		{
			this._componentType = componentType;
			this._eventType = type;
			EventInfo @event = componentType.GetEvent(name);
			this.add_method = @event.GetAddMethod();
			this.remove_method = @event.GetRemoveMethod();
		}

		// Token: 0x06000E9D RID: 3741 RVA: 0x000259F8 File Offset: 0x00023BF8
		private EventInfo GetEventInfo()
		{
			if (this._eventInfo == null)
			{
				this._eventInfo = this._componentType.GetEvent(this.Name);
				if (this._eventInfo == null)
				{
					throw new ArgumentException("Accessor methods for the " + this.Name + " event are missing");
				}
			}
			return this._eventInfo;
		}

		// Token: 0x06000E9E RID: 3742 RVA: 0x00025A54 File Offset: 0x00023C54
		public override void AddEventHandler(object component, Delegate value)
		{
			this.add_method.Invoke(component, new object[]
			{
				value
			});
		}

		// Token: 0x06000E9F RID: 3743 RVA: 0x00025A70 File Offset: 0x00023C70
		public override void RemoveEventHandler(object component, Delegate value)
		{
			this.remove_method.Invoke(component, new object[]
			{
				value
			});
		}

		// Token: 0x1700035F RID: 863
		// (get) Token: 0x06000EA0 RID: 3744 RVA: 0x00025A8C File Offset: 0x00023C8C
		public override Type ComponentType
		{
			get
			{
				return this._componentType;
			}
		}

		// Token: 0x17000360 RID: 864
		// (get) Token: 0x06000EA1 RID: 3745 RVA: 0x00025A94 File Offset: 0x00023C94
		public override Type EventType
		{
			get
			{
				return this._eventType;
			}
		}

		// Token: 0x17000361 RID: 865
		// (get) Token: 0x06000EA2 RID: 3746 RVA: 0x00025A9C File Offset: 0x00023C9C
		public override bool IsMulticast
		{
			get
			{
				return this.GetEventInfo().IsMulticast;
			}
		}

		// Token: 0x04000419 RID: 1049
		private Type _eventType;

		// Token: 0x0400041A RID: 1050
		private Type _componentType;

		// Token: 0x0400041B RID: 1051
		private EventInfo _eventInfo;

		// Token: 0x0400041C RID: 1052
		private MethodInfo add_method;

		// Token: 0x0400041D RID: 1053
		private MethodInfo remove_method;
	}
}
