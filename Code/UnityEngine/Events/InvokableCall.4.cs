using System;
using System.Reflection;
using UnityEngineInternal;

namespace UnityEngine.Events
{
	// Token: 0x02000314 RID: 788
	internal class InvokableCall<T1, T2, T3> : BaseInvokableCall
	{
		// Token: 0x06002741 RID: 10049 RVA: 0x000374D4 File Offset: 0x000356D4
		public InvokableCall(object target, MethodInfo theFunction) : base(target, theFunction)
		{
			this.Delegate = (UnityAction<T1, T2, T3>)theFunction.CreateDelegate(typeof(UnityAction<T1, T2, T3>), target);
		}

		// Token: 0x06002742 RID: 10050 RVA: 0x00037508 File Offset: 0x00035708
		public InvokableCall(UnityAction<T1, T2, T3> action)
		{
			this.Delegate = (UnityAction<T1, T2, T3>)System.Delegate.Combine(this.Delegate, action);
		}

		// Token: 0x14000014 RID: 20
		// (add) Token: 0x06002743 RID: 10051 RVA: 0x00037528 File Offset: 0x00035728
		// (remove) Token: 0x06002744 RID: 10052 RVA: 0x00037544 File Offset: 0x00035744
		protected event UnityAction<T1, T2, T3> Delegate;

		// Token: 0x06002745 RID: 10053 RVA: 0x00037560 File Offset: 0x00035760
		public override void Invoke(object[] args)
		{
			if (args.Length != 3)
			{
				throw new ArgumentException("Passed argument 'args' is invalid size. Expected size is 1");
			}
			BaseInvokableCall.ThrowOnInvalidArg<T1>(args[0]);
			BaseInvokableCall.ThrowOnInvalidArg<T2>(args[1]);
			BaseInvokableCall.ThrowOnInvalidArg<T3>(args[2]);
			if (BaseInvokableCall.AllowInvoke(this.Delegate))
			{
				this.Delegate((T1)((object)args[0]), (T2)((object)args[1]), (T3)((object)args[2]));
			}
		}

		// Token: 0x06002746 RID: 10054 RVA: 0x000375CC File Offset: 0x000357CC
		public override bool Find(object targetObj, MethodInfo method)
		{
			return this.Delegate.Target == targetObj && this.Delegate.GetMethodInfo() == method;
		}
	}
}
