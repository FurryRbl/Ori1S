using System;

namespace UnityEngine
{
	// Token: 0x02000293 RID: 659
	[Flags]
	public enum ComputeBufferType
	{
		// Token: 0x040009FF RID: 2559
		Default = 0,
		// Token: 0x04000A00 RID: 2560
		Raw = 1,
		// Token: 0x04000A01 RID: 2561
		Append = 2,
		// Token: 0x04000A02 RID: 2562
		Counter = 4,
		// Token: 0x04000A03 RID: 2563
		DrawIndirect = 256,
		// Token: 0x04000A04 RID: 2564
		GPUMemory = 512
	}
}
