using System;

namespace System.Runtime.ConstrainedExecution
{
	/// <summary>Specifies a reliability contract.</summary>
	// Token: 0x02000349 RID: 841
	[Serializable]
	public enum Consistency
	{
		/// <summary>In the face of exceptional conditions, the common language runtime (CLR) makes no guarantees regarding state consistency in the current application domain.</summary>
		// Token: 0x040010A2 RID: 4258
		MayCorruptAppDomain = 1,
		/// <summary>In the face of exceptional conditions, the method is guaranteed to limit state corruption to the current instance.</summary>
		// Token: 0x040010A3 RID: 4259
		MayCorruptInstance,
		/// <summary>In the face of exceptional conditions, the CLR makes no guarantees regarding state consistency; that is, the condition might corrupt the process.</summary>
		// Token: 0x040010A4 RID: 4260
		MayCorruptProcess = 0,
		/// <summary>In the face of exceptional conditions, the method is guaranteed not to corrupt state. </summary>
		// Token: 0x040010A5 RID: 4261
		WillNotCorruptState = 3
	}
}
