using System;

namespace System.IO.Pipes
{
	// Token: 0x02000073 RID: 115
	internal interface INamedPipeClient : IPipe
	{
		// Token: 0x060005C0 RID: 1472
		void Connect();

		// Token: 0x060005C1 RID: 1473
		void Connect(int timeout);

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x060005C2 RID: 1474
		int NumberOfServerInstances { get; }

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x060005C3 RID: 1475
		bool IsAsync { get; }
	}
}
