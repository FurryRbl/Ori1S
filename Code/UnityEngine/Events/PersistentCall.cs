using System;
using System.Reflection;
using UnityEngine.Serialization;

namespace UnityEngine.Events
{
	// Token: 0x02000318 RID: 792
	[Serializable]
	internal class PersistentCall
	{
		// Token: 0x170009AA RID: 2474
		// (get) Token: 0x06002750 RID: 10064 RVA: 0x00037790 File Offset: 0x00035990
		public Object target
		{
			get
			{
				return this.m_Target;
			}
		}

		// Token: 0x170009AB RID: 2475
		// (get) Token: 0x06002751 RID: 10065 RVA: 0x00037798 File Offset: 0x00035998
		public string methodName
		{
			get
			{
				return this.m_MethodName;
			}
		}

		// Token: 0x170009AC RID: 2476
		// (get) Token: 0x06002752 RID: 10066 RVA: 0x000377A0 File Offset: 0x000359A0
		// (set) Token: 0x06002753 RID: 10067 RVA: 0x000377A8 File Offset: 0x000359A8
		public PersistentListenerMode mode
		{
			get
			{
				return this.m_Mode;
			}
			set
			{
				this.m_Mode = value;
			}
		}

		// Token: 0x170009AD RID: 2477
		// (get) Token: 0x06002754 RID: 10068 RVA: 0x000377B4 File Offset: 0x000359B4
		public ArgumentCache arguments
		{
			get
			{
				return this.m_Arguments;
			}
		}

		// Token: 0x170009AE RID: 2478
		// (get) Token: 0x06002755 RID: 10069 RVA: 0x000377BC File Offset: 0x000359BC
		// (set) Token: 0x06002756 RID: 10070 RVA: 0x000377C4 File Offset: 0x000359C4
		public UnityEventCallState callState
		{
			get
			{
				return this.m_CallState;
			}
			set
			{
				this.m_CallState = value;
			}
		}

		// Token: 0x06002757 RID: 10071 RVA: 0x000377D0 File Offset: 0x000359D0
		public bool IsValid()
		{
			return this.target != null && !string.IsNullOrEmpty(this.methodName);
		}

		// Token: 0x06002758 RID: 10072 RVA: 0x00037800 File Offset: 0x00035A00
		public BaseInvokableCall GetRuntimeCall(UnityEventBase theEvent)
		{
			if (this.m_CallState == UnityEventCallState.Off || theEvent == null)
			{
				return null;
			}
			MethodInfo methodInfo = theEvent.FindMethod(this);
			if (methodInfo == null)
			{
				return null;
			}
			switch (this.m_Mode)
			{
			case PersistentListenerMode.EventDefined:
				return theEvent.GetDelegate(this.target, methodInfo);
			case PersistentListenerMode.Void:
				return new InvokableCall(this.target, methodInfo);
			case PersistentListenerMode.Object:
				return PersistentCall.GetObjectCall(this.target, methodInfo, this.m_Arguments);
			case PersistentListenerMode.Int:
				return new CachedInvokableCall<int>(this.target, methodInfo, this.m_Arguments.intArgument);
			case PersistentListenerMode.Float:
				return new CachedInvokableCall<float>(this.target, methodInfo, this.m_Arguments.floatArgument);
			case PersistentListenerMode.String:
				return new CachedInvokableCall<string>(this.target, methodInfo, this.m_Arguments.stringArgument);
			case PersistentListenerMode.Bool:
				return new CachedInvokableCall<bool>(this.target, methodInfo, this.m_Arguments.boolArgument);
			default:
				return null;
			}
		}

		// Token: 0x06002759 RID: 10073 RVA: 0x000378F0 File Offset: 0x00035AF0
		private static BaseInvokableCall GetObjectCall(Object target, MethodInfo method, ArgumentCache arguments)
		{
			Type type = typeof(Object);
			if (!string.IsNullOrEmpty(arguments.unityObjectArgumentAssemblyTypeName))
			{
				type = (Type.GetType(arguments.unityObjectArgumentAssemblyTypeName, false) ?? typeof(Object));
			}
			Type typeFromHandle = typeof(CachedInvokableCall<>);
			Type type2 = typeFromHandle.MakeGenericType(new Type[]
			{
				type
			});
			ConstructorInfo constructor = type2.GetConstructor(new Type[]
			{
				typeof(Object),
				typeof(MethodInfo),
				type
			});
			Object @object = arguments.unityObjectArgument;
			if (@object != null && !type.IsAssignableFrom(@object.GetType()))
			{
				@object = null;
			}
			return constructor.Invoke(new object[]
			{
				target,
				method,
				@object
			}) as BaseInvokableCall;
		}

		// Token: 0x0600275A RID: 10074 RVA: 0x000379C8 File Offset: 0x00035BC8
		public void RegisterPersistentListener(Object ttarget, string mmethodName)
		{
			this.m_Target = ttarget;
			this.m_MethodName = mmethodName;
		}

		// Token: 0x0600275B RID: 10075 RVA: 0x000379D8 File Offset: 0x00035BD8
		public void UnregisterPersistentListener()
		{
			this.m_MethodName = string.Empty;
			this.m_Target = null;
		}

		// Token: 0x04000C2D RID: 3117
		[SerializeField]
		[FormerlySerializedAs("instance")]
		private Object m_Target;

		// Token: 0x04000C2E RID: 3118
		[FormerlySerializedAs("methodName")]
		[SerializeField]
		private string m_MethodName;

		// Token: 0x04000C2F RID: 3119
		[FormerlySerializedAs("mode")]
		[SerializeField]
		private PersistentListenerMode m_Mode;

		// Token: 0x04000C30 RID: 3120
		[FormerlySerializedAs("arguments")]
		[SerializeField]
		private ArgumentCache m_Arguments = new ArgumentCache();

		// Token: 0x04000C31 RID: 3121
		[FormerlySerializedAs("m_Enabled")]
		[FormerlySerializedAs("enabled")]
		[SerializeField]
		private UnityEventCallState m_CallState = UnityEventCallState.RuntimeOnly;
	}
}
