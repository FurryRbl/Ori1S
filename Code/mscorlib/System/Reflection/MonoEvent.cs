using System;
using System.Runtime.Serialization;

namespace System.Reflection
{
	// Token: 0x020002A5 RID: 677
	[Serializable]
	internal sealed class MonoEvent : EventInfo, ISerializable
	{
		// Token: 0x170005EA RID: 1514
		// (get) Token: 0x0600226B RID: 8811 RVA: 0x0007C5C0 File Offset: 0x0007A7C0
		public override EventAttributes Attributes
		{
			get
			{
				return MonoEventInfo.GetEventInfo(this).attrs;
			}
		}

		// Token: 0x0600226C RID: 8812 RVA: 0x0007C5DC File Offset: 0x0007A7DC
		public override MethodInfo GetAddMethod(bool nonPublic)
		{
			MonoEventInfo eventInfo = MonoEventInfo.GetEventInfo(this);
			if (nonPublic || (eventInfo.add_method != null && eventInfo.add_method.IsPublic))
			{
				return eventInfo.add_method;
			}
			return null;
		}

		// Token: 0x0600226D RID: 8813 RVA: 0x0007C61C File Offset: 0x0007A81C
		public override MethodInfo GetRaiseMethod(bool nonPublic)
		{
			MonoEventInfo eventInfo = MonoEventInfo.GetEventInfo(this);
			if (nonPublic || (eventInfo.raise_method != null && eventInfo.raise_method.IsPublic))
			{
				return eventInfo.raise_method;
			}
			return null;
		}

		// Token: 0x0600226E RID: 8814 RVA: 0x0007C65C File Offset: 0x0007A85C
		public override MethodInfo GetRemoveMethod(bool nonPublic)
		{
			MonoEventInfo eventInfo = MonoEventInfo.GetEventInfo(this);
			if (nonPublic || (eventInfo.remove_method != null && eventInfo.remove_method.IsPublic))
			{
				return eventInfo.remove_method;
			}
			return null;
		}

		// Token: 0x0600226F RID: 8815 RVA: 0x0007C69C File Offset: 0x0007A89C
		public override MethodInfo[] GetOtherMethods(bool nonPublic)
		{
			MonoEventInfo eventInfo = MonoEventInfo.GetEventInfo(this);
			if (nonPublic)
			{
				return eventInfo.other_methods;
			}
			int num = 0;
			foreach (MethodInfo methodInfo in eventInfo.other_methods)
			{
				if (methodInfo.IsPublic)
				{
					num++;
				}
			}
			if (num == eventInfo.other_methods.Length)
			{
				return eventInfo.other_methods;
			}
			MethodInfo[] array = new MethodInfo[num];
			num = 0;
			foreach (MethodInfo methodInfo2 in eventInfo.other_methods)
			{
				if (methodInfo2.IsPublic)
				{
					array[num++] = methodInfo2;
				}
			}
			return array;
		}

		// Token: 0x170005EB RID: 1515
		// (get) Token: 0x06002270 RID: 8816 RVA: 0x0007C758 File Offset: 0x0007A958
		public override Type DeclaringType
		{
			get
			{
				return MonoEventInfo.GetEventInfo(this).declaring_type;
			}
		}

		// Token: 0x170005EC RID: 1516
		// (get) Token: 0x06002271 RID: 8817 RVA: 0x0007C774 File Offset: 0x0007A974
		public override Type ReflectedType
		{
			get
			{
				return MonoEventInfo.GetEventInfo(this).reflected_type;
			}
		}

		// Token: 0x170005ED RID: 1517
		// (get) Token: 0x06002272 RID: 8818 RVA: 0x0007C790 File Offset: 0x0007A990
		public override string Name
		{
			get
			{
				return MonoEventInfo.GetEventInfo(this).name;
			}
		}

		// Token: 0x06002273 RID: 8819 RVA: 0x0007C7AC File Offset: 0x0007A9AC
		public override string ToString()
		{
			return this.EventHandlerType + " " + this.Name;
		}

		// Token: 0x06002274 RID: 8820 RVA: 0x0007C7C4 File Offset: 0x0007A9C4
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			return MonoCustomAttrs.IsDefined(this, attributeType, inherit);
		}

		// Token: 0x06002275 RID: 8821 RVA: 0x0007C7D0 File Offset: 0x0007A9D0
		public override object[] GetCustomAttributes(bool inherit)
		{
			return MonoCustomAttrs.GetCustomAttributes(this, inherit);
		}

		// Token: 0x06002276 RID: 8822 RVA: 0x0007C7DC File Offset: 0x0007A9DC
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			return MonoCustomAttrs.GetCustomAttributes(this, attributeType, inherit);
		}

		// Token: 0x06002277 RID: 8823 RVA: 0x0007C7E8 File Offset: 0x0007A9E8
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			MemberInfoSerializationHolder.Serialize(info, this.Name, this.ReflectedType, this.ToString(), MemberTypes.Event);
		}

		// Token: 0x04000CE1 RID: 3297
		private IntPtr klass;

		// Token: 0x04000CE2 RID: 3298
		private IntPtr handle;
	}
}
