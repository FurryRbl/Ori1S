using System;
using System.Reflection;
using UnityEngine.Scripting;
using UnityEngineInternal;

namespace UnityEngine.Events
{
	// Token: 0x0200031F RID: 799
	[Serializable]
	public abstract class UnityEvent<T0, T1, T2> : UnityEventBase
	{
		// Token: 0x0600279F RID: 10143 RVA: 0x00038400 File Offset: 0x00036600
		[RequiredByNativeCode]
		public UnityEvent()
		{
		}

		// Token: 0x060027A0 RID: 10144 RVA: 0x00038414 File Offset: 0x00036614
		public void AddListener(UnityAction<T0, T1, T2> call)
		{
			base.AddCall(UnityEvent<T0, T1, T2>.GetDelegate(call));
		}

		// Token: 0x060027A1 RID: 10145 RVA: 0x00038424 File Offset: 0x00036624
		public void RemoveListener(UnityAction<T0, T1, T2> call)
		{
			base.RemoveListener(call.Target, call.GetMethodInfo());
		}

		// Token: 0x060027A2 RID: 10146 RVA: 0x00038438 File Offset: 0x00036638
		protected override MethodInfo FindMethod_Impl(string name, object targetObj)
		{
			return UnityEventBase.GetValidMethodInfo(targetObj, name, new Type[]
			{
				typeof(T0),
				typeof(T1),
				typeof(T2)
			});
		}

		// Token: 0x060027A3 RID: 10147 RVA: 0x0003847C File Offset: 0x0003667C
		internal override BaseInvokableCall GetDelegate(object target, MethodInfo theFunction)
		{
			return new InvokableCall<T0, T1, T2>(target, theFunction);
		}

		// Token: 0x060027A4 RID: 10148 RVA: 0x00038488 File Offset: 0x00036688
		private static BaseInvokableCall GetDelegate(UnityAction<T0, T1, T2> action)
		{
			return new InvokableCall<T0, T1, T2>(action);
		}

		// Token: 0x060027A5 RID: 10149 RVA: 0x00038490 File Offset: 0x00036690
		public void Invoke(T0 arg0, T1 arg1, T2 arg2)
		{
			this.m_InvokeArray[0] = arg0;
			this.m_InvokeArray[1] = arg1;
			this.m_InvokeArray[2] = arg2;
			base.Invoke(this.m_InvokeArray);
		}

		// Token: 0x04000C3E RID: 3134
		private readonly object[] m_InvokeArray = new object[3];
	}
}
