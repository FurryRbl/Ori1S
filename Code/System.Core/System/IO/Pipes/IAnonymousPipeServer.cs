using System;
using Microsoft.Win32.SafeHandles;

namespace System.IO.Pipes
{
	// Token: 0x02000072 RID: 114
	internal interface IAnonymousPipeServer : IPipe
	{
		// Token: 0x17000089 RID: 137
		// (get) Token: 0x060005BE RID: 1470
		SafePipeHandle ClientHandle { get; }

		// Token: 0x060005BF RID: 1471
		void DisposeLocalCopyOfClientHandle();
	}
}
