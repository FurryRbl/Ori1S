using System;

namespace System.IO
{
	// Token: 0x0200027E RID: 638
	internal class NullFileWatcher : IFileWatcher
	{
		// Token: 0x06001672 RID: 5746 RVA: 0x0003CF64 File Offset: 0x0003B164
		public void StartDispatching(FileSystemWatcher fsw)
		{
		}

		// Token: 0x06001673 RID: 5747 RVA: 0x0003CF68 File Offset: 0x0003B168
		public void StopDispatching(FileSystemWatcher fsw)
		{
		}

		// Token: 0x06001674 RID: 5748 RVA: 0x0003CF6C File Offset: 0x0003B16C
		public static bool GetInstance(out IFileWatcher watcher)
		{
			if (NullFileWatcher.instance != null)
			{
				watcher = NullFileWatcher.instance;
				return true;
			}
			IFileWatcher fileWatcher;
			watcher = (fileWatcher = new NullFileWatcher());
			NullFileWatcher.instance = fileWatcher;
			return true;
		}

		// Token: 0x04000723 RID: 1827
		private static IFileWatcher instance;
	}
}
