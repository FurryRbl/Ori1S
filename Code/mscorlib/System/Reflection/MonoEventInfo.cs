using System;
using System.Runtime.CompilerServices;

namespace System.Reflection
{
	// Token: 0x020002A4 RID: 676
	internal struct MonoEventInfo
	{
		// Token: 0x06002268 RID: 8808
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void get_event_info(MonoEvent ev, out MonoEventInfo info);

		// Token: 0x06002269 RID: 8809 RVA: 0x0007C5A0 File Offset: 0x0007A7A0
		internal static MonoEventInfo GetEventInfo(MonoEvent ev)
		{
			MonoEventInfo result;
			MonoEventInfo.get_event_info(ev, out result);
			return result;
		}

		// Token: 0x04000CD9 RID: 3289
		public Type declaring_type;

		// Token: 0x04000CDA RID: 3290
		public Type reflected_type;

		// Token: 0x04000CDB RID: 3291
		public string name;

		// Token: 0x04000CDC RID: 3292
		public MethodInfo add_method;

		// Token: 0x04000CDD RID: 3293
		public MethodInfo remove_method;

		// Token: 0x04000CDE RID: 3294
		public MethodInfo raise_method;

		// Token: 0x04000CDF RID: 3295
		public EventAttributes attrs;

		// Token: 0x04000CE0 RID: 3296
		public MethodInfo[] other_methods;
	}
}
