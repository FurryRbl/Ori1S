using System;

namespace System.Runtime
{
	/// <summary>Adjusts the time that the garbage collector intrudes in your application.   </summary>
	// Token: 0x0200031D RID: 797
	[Serializable]
	public enum GCLatencyMode
	{
		/// <summary>Disables garbage collection concurrency and reclaims objects in a batch call. This is the most intrusive mode.</summary>
		// Token: 0x04001080 RID: 4224
		Batch,
		/// <summary>Enables garbage collection concurrency and reclaims objects while the application is running. This is the default mode for garbage collection on a workstation and is less intrusive. It balances responsiveness with throughput.</summary>
		// Token: 0x04001081 RID: 4225
		Interactive,
		/// <summary>Enables garbage collection that is more conservative in reclaiming objects. Full Collections occur only if the system is under memory pressure while generation 0 and generation 1 collections might occur more frequently. This is the least intrusive mode.</summary>
		// Token: 0x04001082 RID: 4226
		LowLatency
	}
}
