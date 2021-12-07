using System;

namespace System.IO.Pipes
{
	// Token: 0x02000074 RID: 116
	internal interface INamedPipeServer : IPipe
	{
		// Token: 0x060005C4 RID: 1476
		void Disconnect();

		// Token: 0x060005C5 RID: 1477
		void WaitForConnection();
	}
}
