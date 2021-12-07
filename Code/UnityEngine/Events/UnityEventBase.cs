using System;
using System.Reflection;
using UnityEngine.Serialization;

namespace UnityEngine.Events
{
	// Token: 0x0200031B RID: 795
	[Serializable]
	public abstract class UnityEventBase : ISerializationCallbackReceiver
	{
		// Token: 0x06002776 RID: 10102 RVA: 0x00037E94 File Offset: 0x00036094
		protected UnityEventBase()
		{
			this.m_Calls = new InvokableCallList();
			this.m_PersistentCalls = new PersistentCallGroup();
			this.m_TypeName = base.GetType().AssemblyQualifiedName;
		}

		// Token: 0x06002777 RID: 10103 RVA: 0x00037ED8 File Offset: 0x000360D8
		void ISerializationCallbackReceiver.OnBeforeSerialize()
		{
		}

		// Token: 0x06002778 RID: 10104 RVA: 0x00037EDC File Offset: 0x000360DC
		void ISerializationCallbackReceiver.OnAfterDeserialize()
		{
			this.DirtyPersistentCalls();
			this.m_TypeName = base.GetType().AssemblyQualifiedName;
		}

		// Token: 0x06002779 RID: 10105
		protected abstract MethodInfo FindMethod_Impl(string name, object targetObj);

		// Token: 0x0600277A RID: 10106
		internal abstract BaseInvokableCall GetDelegate(object target, MethodInfo theFunction);

		// Token: 0x0600277B RID: 10107 RVA: 0x00037EF8 File Offset: 0x000360F8
		internal MethodInfo FindMethod(PersistentCall call)
		{
			Type argumentType = typeof(Object);
			if (!string.IsNullOrEmpty(call.arguments.unityObjectArgumentAssemblyTypeName))
			{
				argumentType = (Type.GetType(call.arguments.unityObjectArgumentAssemblyTypeName, false) ?? typeof(Object));
			}
			return this.FindMethod(call.methodName, call.target, call.mode, argumentType);
		}

		// Token: 0x0600277C RID: 10108 RVA: 0x00037F64 File Offset: 0x00036164
		internal MethodInfo FindMethod(string name, object listener, PersistentListenerMode mode, Type argumentType)
		{
			switch (mode)
			{
			case PersistentListenerMode.EventDefined:
				return this.FindMethod_Impl(name, listener);
			case PersistentListenerMode.Void:
				return UnityEventBase.GetValidMethodInfo(listener, name, new Type[0]);
			case PersistentListenerMode.Object:
				return UnityEventBase.GetValidMethodInfo(listener, name, new Type[]
				{
					argumentType ?? typeof(Object)
				});
			case PersistentListenerMode.Int:
				return UnityEventBase.GetValidMethodInfo(listener, name, new Type[]
				{
					typeof(int)
				});
			case PersistentListenerMode.Float:
				return UnityEventBase.GetValidMethodInfo(listener, name, new Type[]
				{
					typeof(float)
				});
			case PersistentListenerMode.String:
				return UnityEventBase.GetValidMethodInfo(listener, name, new Type[]
				{
					typeof(string)
				});
			case PersistentListenerMode.Bool:
				return UnityEventBase.GetValidMethodInfo(listener, name, new Type[]
				{
					typeof(bool)
				});
			default:
				return null;
			}
		}

		// Token: 0x0600277D RID: 10109 RVA: 0x00038044 File Offset: 0x00036244
		public int GetPersistentEventCount()
		{
			return this.m_PersistentCalls.Count;
		}

		// Token: 0x0600277E RID: 10110 RVA: 0x00038054 File Offset: 0x00036254
		public Object GetPersistentTarget(int index)
		{
			PersistentCall listener = this.m_PersistentCalls.GetListener(index);
			return (listener == null) ? null : listener.target;
		}

		// Token: 0x0600277F RID: 10111 RVA: 0x00038080 File Offset: 0x00036280
		public string GetPersistentMethodName(int index)
		{
			PersistentCall listener = this.m_PersistentCalls.GetListener(index);
			return (listener == null) ? string.Empty : listener.methodName;
		}

		// Token: 0x06002780 RID: 10112 RVA: 0x000380B0 File Offset: 0x000362B0
		private void DirtyPersistentCalls()
		{
			this.m_Calls.ClearPersistent();
			this.m_CallsDirty = true;
		}

		// Token: 0x06002781 RID: 10113 RVA: 0x000380C4 File Offset: 0x000362C4
		private void RebuildPersistentCallsIfNeeded()
		{
			if (this.m_CallsDirty)
			{
				this.m_PersistentCalls.Initialize(this.m_Calls, this);
				this.m_CallsDirty = false;
			}
		}

		// Token: 0x06002782 RID: 10114 RVA: 0x000380F8 File Offset: 0x000362F8
		public void SetPersistentListenerState(int index, UnityEventCallState state)
		{
			PersistentCall listener = this.m_PersistentCalls.GetListener(index);
			if (listener != null)
			{
				listener.callState = state;
			}
			this.DirtyPersistentCalls();
		}

		// Token: 0x06002783 RID: 10115 RVA: 0x00038128 File Offset: 0x00036328
		protected void AddListener(object targetObj, MethodInfo method)
		{
			this.m_Calls.AddListener(this.GetDelegate(targetObj, method));
		}

		// Token: 0x06002784 RID: 10116 RVA: 0x00038140 File Offset: 0x00036340
		internal void AddCall(BaseInvokableCall call)
		{
			this.m_Calls.AddListener(call);
		}

		// Token: 0x06002785 RID: 10117 RVA: 0x00038150 File Offset: 0x00036350
		protected void RemoveListener(object targetObj, MethodInfo method)
		{
			this.m_Calls.RemoveListener(targetObj, method);
		}

		// Token: 0x06002786 RID: 10118 RVA: 0x00038160 File Offset: 0x00036360
		public void RemoveAllListeners()
		{
			this.m_Calls.Clear();
		}

		// Token: 0x06002787 RID: 10119 RVA: 0x00038170 File Offset: 0x00036370
		protected void Invoke(object[] parameters)
		{
			this.RebuildPersistentCallsIfNeeded();
			this.m_Calls.Invoke(parameters);
		}

		// Token: 0x06002788 RID: 10120 RVA: 0x00038184 File Offset: 0x00036384
		public override string ToString()
		{
			return base.ToString() + " " + base.GetType().FullName;
		}

		// Token: 0x06002789 RID: 10121 RVA: 0x000381AC File Offset: 0x000363AC
		public static MethodInfo GetValidMethodInfo(object obj, string functionName, Type[] argumentTypes)
		{
			Type type = obj.GetType();
			while (type != typeof(object) && type != null)
			{
				MethodInfo method = type.GetMethod(functionName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, argumentTypes, null);
				if (method != null)
				{
					ParameterInfo[] parameters = method.GetParameters();
					bool flag = true;
					int num = 0;
					foreach (ParameterInfo parameterInfo in parameters)
					{
						Type type2 = argumentTypes[num];
						Type parameterType = parameterInfo.ParameterType;
						flag = (type2.IsPrimitive == parameterType.IsPrimitive);
						if (!flag)
						{
							break;
						}
						num++;
					}
					if (flag)
					{
						return method;
					}
				}
				type = type.BaseType;
			}
			return null;
		}

		// Token: 0x04000C37 RID: 3127
		private InvokableCallList m_Calls;

		// Token: 0x04000C38 RID: 3128
		[SerializeField]
		[FormerlySerializedAs("m_PersistentListeners")]
		private PersistentCallGroup m_PersistentCalls;

		// Token: 0x04000C39 RID: 3129
		[SerializeField]
		private string m_TypeName;

		// Token: 0x04000C3A RID: 3130
		private bool m_CallsDirty = true;
	}
}
