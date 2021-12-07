using System;

namespace System.IO
{
	// Token: 0x02000283 RID: 643
	internal interface IFileWatcher
	{
		// Token: 0x060016AF RID: 5807
		void StartDispatching(FileSystemWatcher fsw);

		// Token: 0x060016B0 RID: 5808
		void StopDispatching(FileSystemWatcher fsw);
	}
}
