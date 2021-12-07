using System;
using System.Reflection;
using UnityEngineInternal;

namespace UnityEngine.Events
{
	// Token: 0x02000313 RID: 787
	internal class InvokableCall<T1, T2> : BaseInvokableCall
	{
		// Token: 0x0600273B RID: 10043 RVA: 0x000373BC File Offset: 0x000355BC
		public InvokableCall(object target, MethodInfo theFunction) : base(target, theFunction)
		{
			this.Delegate = (UnityAction<T1, T2>)theFunction.CreateDelegate(typeof(UnityAction<T1, T2>), target);
		}

		// Token: 0x0600273C RID: 10044 RVA: 0x000373F0 File Offset: 0x000355F0
		public InvokableCall(UnityAction<T1, T2> action)
		{
			this.Delegate = (UnityAction<T1, T2>)System.Delegate.Combine(this.Delegate, action);
		}

		// Token: 0x14000013 RID: 19
		// (add) Token: 0x0600273D RID: 10045 RVA: 0x00037410 File Offset: 0x00035610
		// (remove) Token: 0x0600273E RID: 10046 RVA: 0x0003742C File Offset: 0x0003562C
		protected event UnityAction<T1, T2> Delegate;

		// Token: 0x0600273F RID: 10047 RVA: 0x00037448 File Offset: 0x00035648
		public override void Invoke(object[] args)
		{
			if (args.Length != 2)
			{
				throw new ArgumentException("Passed argument 'args' is invalid size. Expected size is 1");
			}
			BaseInvokableCall.ThrowOnInvalidArg<T1>(args[0]);
			BaseInvokableCall.ThrowOnInvalidArg<T2>(args[1]);
			if (BaseInvokableCall.AllowInvoke(this.Delegate))
			{
				this.Delegate((T1)((object)args[0]), (T2)((object)args[1]));
			}
		}

		// Token: 0x06002740 RID: 10048 RVA: 0x000374A4 File Offset: 0x000356A4
		public override bool Find(object targetObj, MethodInfo method)
		{
			return this.Delegate.Target == targetObj && this.Delegate.GetMethodInfo() == method;
		}
	}
}
