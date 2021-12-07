using System;
using System.Runtime.ConstrainedExecution;

namespace System.Runtime
{
	/// <summary>Specifies the garbage collection settings for the current process. </summary>
	// Token: 0x0200031E RID: 798
	public static class GCSettings
	{
		/// <summary>Gets a value indicating whether server garbage collection is enabled.</summary>
		/// <returns>true if server garbage collection is enabled; otherwise, false.</returns>
		// Token: 0x17000742 RID: 1858
		// (get) Token: 0x0600288D RID: 10381 RVA: 0x00091C94 File Offset: 0x0008FE94
		[MonoTODO("Always returns false")]
		public static bool IsServerGC
		{
			get
			{
				return false;
			}
		}

		/// <summary>Gets or sets the current latency mode for garbage collection.</summary>
		/// <returns>One of the <see cref="T:System.Runtime.GCLatencyMode" /> values. </returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <see cref="T:System.Runtime.GCLatencyMode" /> is set to an invalid value.</exception>
		// Token: 0x17000743 RID: 1859
		// (get) Token: 0x0600288E RID: 10382 RVA: 0x00091C98 File Offset: 0x0008FE98
		// (set) Token: 0x0600288F RID: 10383 RVA: 0x00091C9C File Offset: 0x0008FE9C
		[MonoTODO("Always returns GCLatencyMode.Interactive and ignores set (.NET 2.0 SP1 member)")]
		public static GCLatencyMode LatencyMode
		{
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			get
			{
				return GCLatencyMode.Interactive;
			}
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			set
			{
			}
		}
	}
}
