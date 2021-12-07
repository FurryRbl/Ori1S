using System;
using System.Reflection;

namespace UnityEngineInternal
{
	// Token: 0x0200033D RID: 829
	internal static class NetFxCoreExtensions
	{
		// Token: 0x0600284F RID: 10319 RVA: 0x0003A044 File Offset: 0x00038244
		public static Delegate CreateDelegate(this MethodInfo self, Type delegateType, object target)
		{
			return Delegate.CreateDelegate(delegateType, target, self);
		}

		// Token: 0x06002850 RID: 10320 RVA: 0x0003A050 File Offset: 0x00038250
		public static MethodInfo GetMethodInfo(this Delegate self)
		{
			return self.Method;
		}
	}
}
