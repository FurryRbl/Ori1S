using System;
using System.Reflection;
using UnityEngineInternal;

namespace UnityEngine.Events
{
	// Token: 0x02000311 RID: 785
	internal class InvokableCall : BaseInvokableCall
	{
		// Token: 0x0600272F RID: 10031 RVA: 0x000371B8 File Offset: 0x000353B8
		public InvokableCall(object target, MethodInfo theFunction) : base(target, theFunction)
		{
			this.Delegate = (UnityAction)System.Delegate.Combine(this.Delegate, (UnityAction)theFunction.CreateDelegate(typeof(UnityAction), target));
		}

		// Token: 0x06002730 RID: 10032 RVA: 0x000371FC File Offset: 0x000353FC
		public InvokableCall(UnityAction action)
		{
			this.Delegate = (UnityAction)System.Delegate.Combine(this.Delegate, action);
		}

		// Token: 0x14000011 RID: 17
		// (add) Token: 0x06002731 RID: 10033 RVA: 0x0003721C File Offset: 0x0003541C
		// (remove) Token: 0x06002732 RID: 10034 RVA: 0x00037238 File Offset: 0x00035438
		private event UnityAction Delegate;

		// Token: 0x06002733 RID: 10035 RVA: 0x00037254 File Offset: 0x00035454
		public override void Invoke(object[] args)
		{
			if (BaseInvokableCall.AllowInvoke(this.Delegate))
			{
				this.Delegate();
			}
		}

		// Token: 0x06002734 RID: 10036 RVA: 0x00037274 File Offset: 0x00035474
		public override bool Find(object targetObj, MethodInfo method)
		{
			return this.Delegate.Target == targetObj && this.Delegate.GetMethodInfo() == method;
		}
	}
}
