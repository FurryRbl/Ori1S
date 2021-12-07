using System;
using System.Reflection;
using UnityEngineInternal;

namespace UnityEngine.Events
{
	// Token: 0x02000312 RID: 786
	internal class InvokableCall<T1> : BaseInvokableCall
	{
		// Token: 0x06002735 RID: 10037 RVA: 0x000372A4 File Offset: 0x000354A4
		public InvokableCall(object target, MethodInfo theFunction) : base(target, theFunction)
		{
			this.Delegate = (UnityAction<T1>)System.Delegate.Combine(this.Delegate, (UnityAction<T1>)theFunction.CreateDelegate(typeof(UnityAction<T1>), target));
		}

		// Token: 0x06002736 RID: 10038 RVA: 0x000372E8 File Offset: 0x000354E8
		public InvokableCall(UnityAction<T1> action)
		{
			this.Delegate = (UnityAction<T1>)System.Delegate.Combine(this.Delegate, action);
		}

		// Token: 0x14000012 RID: 18
		// (add) Token: 0x06002737 RID: 10039 RVA: 0x00037308 File Offset: 0x00035508
		// (remove) Token: 0x06002738 RID: 10040 RVA: 0x00037324 File Offset: 0x00035524
		protected event UnityAction<T1> Delegate;

		// Token: 0x06002739 RID: 10041 RVA: 0x00037340 File Offset: 0x00035540
		public override void Invoke(object[] args)
		{
			if (args.Length != 1)
			{
				throw new ArgumentException("Passed argument 'args' is invalid size. Expected size is 1");
			}
			BaseInvokableCall.ThrowOnInvalidArg<T1>(args[0]);
			if (BaseInvokableCall.AllowInvoke(this.Delegate))
			{
				this.Delegate((T1)((object)args[0]));
			}
		}

		// Token: 0x0600273A RID: 10042 RVA: 0x0003738C File Offset: 0x0003558C
		public override bool Find(object targetObj, MethodInfo method)
		{
			return this.Delegate.Target == targetObj && this.Delegate.GetMethodInfo() == method;
		}
	}
}
