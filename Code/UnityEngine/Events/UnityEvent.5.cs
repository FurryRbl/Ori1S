using System;
using System.Reflection;
using UnityEngine.Scripting;
using UnityEngineInternal;

namespace UnityEngine.Events
{
	// Token: 0x02000320 RID: 800
	[Serializable]
	public abstract class UnityEvent<T0, T1, T2, T3> : UnityEventBase
	{
		// Token: 0x060027A6 RID: 10150 RVA: 0x000384D4 File Offset: 0x000366D4
		[RequiredByNativeCode]
		public UnityEvent()
		{
		}

		// Token: 0x060027A7 RID: 10151 RVA: 0x000384E8 File Offset: 0x000366E8
		public void AddListener(UnityAction<T0, T1, T2, T3> call)
		{
			base.AddCall(UnityEvent<T0, T1, T2, T3>.GetDelegate(call));
		}

		// Token: 0x060027A8 RID: 10152 RVA: 0x000384F8 File Offset: 0x000366F8
		public void RemoveListener(UnityAction<T0, T1, T2, T3> call)
		{
			base.RemoveListener(call.Target, call.GetMethodInfo());
		}

		// Token: 0x060027A9 RID: 10153 RVA: 0x0003850C File Offset: 0x0003670C
		protected override MethodInfo FindMethod_Impl(string name, object targetObj)
		{
			return UnityEventBase.GetValidMethodInfo(targetObj, name, new Type[]
			{
				typeof(T0),
				typeof(T1),
				typeof(T2),
				typeof(T3)
			});
		}

		// Token: 0x060027AA RID: 10154 RVA: 0x0003855C File Offset: 0x0003675C
		internal override BaseInvokableCall GetDelegate(object target, MethodInfo theFunction)
		{
			return new InvokableCall<T0, T1, T2, T3>(target, theFunction);
		}

		// Token: 0x060027AB RID: 10155 RVA: 0x00038568 File Offset: 0x00036768
		private static BaseInvokableCall GetDelegate(UnityAction<T0, T1, T2, T3> action)
		{
			return new InvokableCall<T0, T1, T2, T3>(action);
		}

		// Token: 0x060027AC RID: 10156 RVA: 0x00038570 File Offset: 0x00036770
		public void Invoke(T0 arg0, T1 arg1, T2 arg2, T3 arg3)
		{
			this.m_InvokeArray[0] = arg0;
			this.m_InvokeArray[1] = arg1;
			this.m_InvokeArray[2] = arg2;
			this.m_InvokeArray[3] = arg3;
			base.Invoke(this.m_InvokeArray);
		}

		// Token: 0x04000C3F RID: 3135
		private readonly object[] m_InvokeArray = new object[4];
	}
}
