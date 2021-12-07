using System;
using System.Reflection;
using UnityEngine.Scripting;
using UnityEngineInternal;

namespace UnityEngine.Events
{
	// Token: 0x0200031C RID: 796
	[Serializable]
	public class UnityEvent : UnityEventBase
	{
		// Token: 0x0600278A RID: 10122 RVA: 0x00038260 File Offset: 0x00036460
		[RequiredByNativeCode]
		public UnityEvent()
		{
		}

		// Token: 0x0600278B RID: 10123 RVA: 0x00038274 File Offset: 0x00036474
		public void AddListener(UnityAction call)
		{
			base.AddCall(UnityEvent.GetDelegate(call));
		}

		// Token: 0x0600278C RID: 10124 RVA: 0x00038284 File Offset: 0x00036484
		public void RemoveListener(UnityAction call)
		{
			base.RemoveListener(call.Target, call.GetMethodInfo());
		}

		// Token: 0x0600278D RID: 10125 RVA: 0x00038298 File Offset: 0x00036498
		protected override MethodInfo FindMethod_Impl(string name, object targetObj)
		{
			return UnityEventBase.GetValidMethodInfo(targetObj, name, new Type[0]);
		}

		// Token: 0x0600278E RID: 10126 RVA: 0x000382A8 File Offset: 0x000364A8
		internal override BaseInvokableCall GetDelegate(object target, MethodInfo theFunction)
		{
			return new InvokableCall(target, theFunction);
		}

		// Token: 0x0600278F RID: 10127 RVA: 0x000382B4 File Offset: 0x000364B4
		private static BaseInvokableCall GetDelegate(UnityAction action)
		{
			return new InvokableCall(action);
		}

		// Token: 0x06002790 RID: 10128 RVA: 0x000382BC File Offset: 0x000364BC
		public void Invoke()
		{
			base.Invoke(this.m_InvokeArray);
		}

		// Token: 0x04000C3B RID: 3131
		private readonly object[] m_InvokeArray = new object[0];
	}
}
