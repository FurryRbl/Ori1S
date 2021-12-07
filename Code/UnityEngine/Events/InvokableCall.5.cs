using System;
using System.Reflection;
using UnityEngineInternal;

namespace UnityEngine.Events
{
	// Token: 0x02000315 RID: 789
	internal class InvokableCall<T1, T2, T3, T4> : BaseInvokableCall
	{
		// Token: 0x06002747 RID: 10055 RVA: 0x000375FC File Offset: 0x000357FC
		public InvokableCall(object target, MethodInfo theFunction) : base(target, theFunction)
		{
			this.Delegate = (UnityAction<T1, T2, T3, T4>)theFunction.CreateDelegate(typeof(UnityAction<T1, T2, T3, T4>), target);
		}

		// Token: 0x06002748 RID: 10056 RVA: 0x00037630 File Offset: 0x00035830
		public InvokableCall(UnityAction<T1, T2, T3, T4> action)
		{
			this.Delegate = (UnityAction<T1, T2, T3, T4>)System.Delegate.Combine(this.Delegate, action);
		}

		// Token: 0x14000015 RID: 21
		// (add) Token: 0x06002749 RID: 10057 RVA: 0x00037650 File Offset: 0x00035850
		// (remove) Token: 0x0600274A RID: 10058 RVA: 0x0003766C File Offset: 0x0003586C
		protected event UnityAction<T1, T2, T3, T4> Delegate;

		// Token: 0x0600274B RID: 10059 RVA: 0x00037688 File Offset: 0x00035888
		public override void Invoke(object[] args)
		{
			if (args.Length != 4)
			{
				throw new ArgumentException("Passed argument 'args' is invalid size. Expected size is 1");
			}
			BaseInvokableCall.ThrowOnInvalidArg<T1>(args[0]);
			BaseInvokableCall.ThrowOnInvalidArg<T2>(args[1]);
			BaseInvokableCall.ThrowOnInvalidArg<T3>(args[2]);
			BaseInvokableCall.ThrowOnInvalidArg<T4>(args[3]);
			if (BaseInvokableCall.AllowInvoke(this.Delegate))
			{
				this.Delegate((T1)((object)args[0]), (T2)((object)args[1]), (T3)((object)args[2]), (T4)((object)args[3]));
			}
		}

		// Token: 0x0600274C RID: 10060 RVA: 0x00037704 File Offset: 0x00035904
		public override bool Find(object targetObj, MethodInfo method)
		{
			return this.Delegate.Target == targetObj && this.Delegate.GetMethodInfo() == method;
		}
	}
}
