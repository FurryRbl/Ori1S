using System;

namespace UnityEngine.Networking
{
	// Token: 0x0200024C RID: 588
	public enum QosType
	{
		// Token: 0x04000950 RID: 2384
		Unreliable,
		// Token: 0x04000951 RID: 2385
		UnreliableFragmented,
		// Token: 0x04000952 RID: 2386
		UnreliableSequenced,
		// Token: 0x04000953 RID: 2387
		Reliable,
		// Token: 0x04000954 RID: 2388
		ReliableFragmented,
		// Token: 0x04000955 RID: 2389
		ReliableSequenced,
		// Token: 0x04000956 RID: 2390
		StateUpdate,
		// Token: 0x04000957 RID: 2391
		ReliableStateUpdate,
		// Token: 0x04000958 RID: 2392
		AllCostDelivery
	}
}
