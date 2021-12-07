using System;
using System.Runtime.CompilerServices;

namespace System.Reflection
{
	// Token: 0x020002AA RID: 682
	internal struct MonoPropertyInfo
	{
		// Token: 0x060022CD RID: 8909
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void get_property_info(MonoProperty prop, ref MonoPropertyInfo info, PInfo req_info);

		// Token: 0x060022CE RID: 8910
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern Type[] GetTypeModifiers(MonoProperty prop, bool optional);

		// Token: 0x04000CF3 RID: 3315
		public Type parent;

		// Token: 0x04000CF4 RID: 3316
		public string name;

		// Token: 0x04000CF5 RID: 3317
		public MethodInfo get_method;

		// Token: 0x04000CF6 RID: 3318
		public MethodInfo set_method;

		// Token: 0x04000CF7 RID: 3319
		public PropertyAttributes attrs;
	}
}
