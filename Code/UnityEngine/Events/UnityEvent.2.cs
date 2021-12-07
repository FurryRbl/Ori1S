using System;
using System.Reflection;
using UnityEngine.Scripting;
using UnityEngineInternal;

namespace UnityEngine.Events
{
	// Token: 0x0200031D RID: 797
	[Serializable]
	public abstract class UnityEvent<T0> : UnityEventBase
	{
		// Token: 0x06002791 RID: 10129 RVA: 0x000382CC File Offset: 0x000364CC
		[RequiredByNativeCode]
		public UnityEvent()
		{
		}

		// Token: 0x06002792 RID: 10130 RVA: 0x000382E0 File Offset: 0x000364E0
		public void AddListener(UnityAction<T0> call)
		{
			base.AddCall(UnityEvent<T0>.GetDelegate(call));
		}

		// Token: 0x06002793 RID: 10131 RVA: 0x000382F0 File Offset: 0x000364F0
		public void RemoveListener(UnityAction<T0> call)
		{
			base.RemoveListener(call.Target, call.GetMethodInfo());
		}

		// Token: 0x06002794 RID: 10132 RVA: 0x00038304 File Offset: 0x00036504
		protected override MethodInfo FindMethod_Impl(string name, object targetObj)
		{
			return UnityEventBase.GetValidMethodInfo(targetObj, name, new Type[]
			{
				typeof(T0)
			});
		}

		// Token: 0x06002795 RID: 10133 RVA: 0x00038320 File Offset: 0x00036520
		internal override BaseInvokableCall GetDelegate(object target, MethodInfo theFunction)
		{
			return new InvokableCall<T0>(target, theFunction);
		}

		// Token: 0x06002796 RID: 10134 RVA: 0x0003832C File Offset: 0x0003652C
		private static BaseInvokableCall GetDelegate(UnityAction<T0> action)
		{
			return new InvokableCall<T0>(action);
		}

		// Token: 0x06002797 RID: 10135 RVA: 0x00038334 File Offset: 0x00036534
		public void Invoke(T0 arg0)
		{
			this.m_InvokeArray[0] = arg0;
			base.Invoke(this.m_InvokeArray);
		}

		// Token: 0x04000C3C RID: 3132
		private readonly object[] m_InvokeArray = new object[1];
	}
}
