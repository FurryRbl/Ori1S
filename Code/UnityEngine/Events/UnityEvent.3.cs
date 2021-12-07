using System;
using System.Reflection;
using UnityEngine.Scripting;
using UnityEngineInternal;

namespace UnityEngine.Events
{
	// Token: 0x0200031E RID: 798
	[Serializable]
	public abstract class UnityEvent<T0, T1> : UnityEventBase
	{
		// Token: 0x06002798 RID: 10136 RVA: 0x00038350 File Offset: 0x00036550
		[RequiredByNativeCode]
		public UnityEvent()
		{
		}

		// Token: 0x06002799 RID: 10137 RVA: 0x00038364 File Offset: 0x00036564
		public void AddListener(UnityAction<T0, T1> call)
		{
			base.AddCall(UnityEvent<T0, T1>.GetDelegate(call));
		}

		// Token: 0x0600279A RID: 10138 RVA: 0x00038374 File Offset: 0x00036574
		public void RemoveListener(UnityAction<T0, T1> call)
		{
			base.RemoveListener(call.Target, call.GetMethodInfo());
		}

		// Token: 0x0600279B RID: 10139 RVA: 0x00038388 File Offset: 0x00036588
		protected override MethodInfo FindMethod_Impl(string name, object targetObj)
		{
			return UnityEventBase.GetValidMethodInfo(targetObj, name, new Type[]
			{
				typeof(T0),
				typeof(T1)
			});
		}

		// Token: 0x0600279C RID: 10140 RVA: 0x000383B4 File Offset: 0x000365B4
		internal override BaseInvokableCall GetDelegate(object target, MethodInfo theFunction)
		{
			return new InvokableCall<T0, T1>(target, theFunction);
		}

		// Token: 0x0600279D RID: 10141 RVA: 0x000383C0 File Offset: 0x000365C0
		private static BaseInvokableCall GetDelegate(UnityAction<T0, T1> action)
		{
			return new InvokableCall<T0, T1>(action);
		}

		// Token: 0x0600279E RID: 10142 RVA: 0x000383C8 File Offset: 0x000365C8
		public void Invoke(T0 arg0, T1 arg1)
		{
			this.m_InvokeArray[0] = arg0;
			this.m_InvokeArray[1] = arg1;
			base.Invoke(this.m_InvokeArray);
		}

		// Token: 0x04000C3D RID: 3133
		private readonly object[] m_InvokeArray = new object[2];
	}
}
