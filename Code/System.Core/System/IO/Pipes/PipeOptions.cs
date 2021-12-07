using System;

namespace System.IO.Pipes
{
	// Token: 0x02000075 RID: 117
	[Flags]
	[Serializable]
	public enum PipeOptions
	{
		// Token: 0x04000195 RID: 405
		None = 0,
		// Token: 0x04000196 RID: 406
		WriteThrough = 1,
		// Token: 0x04000197 RID: 407
		Asynchronous = 2
	}
}
