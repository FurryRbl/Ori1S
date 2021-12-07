using System;
using System.Reflection;

namespace UnityEngine.Events
{
	// Token: 0x02000316 RID: 790
	internal class CachedInvokableCall<T> : InvokableCall<T>
	{
		// Token: 0x0600274D RID: 10061 RVA: 0x00037734 File Offset: 0x00035934
		public CachedInvokableCall(Object target, MethodInfo theFunction, T argument) : base(target, theFunction)
		{
			this.m_Arg1[0] = argument;
		}

		// Token: 0x0600274E RID: 10062 RVA: 0x00037764 File Offset: 0x00035964
		public override void Invoke(object[] args)
		{
			base.Invoke(this.m_Arg1);
		}

		// Token: 0x04000C28 RID: 3112
		private readonly object[] m_Arg1 = new object[1];
	}
}
