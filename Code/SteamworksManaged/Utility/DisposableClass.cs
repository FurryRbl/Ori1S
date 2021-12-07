using System;

namespace ManagedSteam.Utility
{
	// Token: 0x02000082 RID: 130
	public class DisposableClass : IDisposable
	{
		// Token: 0x06000427 RID: 1063 RVA: 0x00007BF5 File Offset: 0x00005DF5
		public void Dispose()
		{
			this.CleanUpManagedResources();
			this.CleanUpNativeResources();
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000428 RID: 1064 RVA: 0x00007C09 File Offset: 0x00005E09
		protected virtual void CleanUpManagedResources()
		{
		}

		// Token: 0x06000429 RID: 1065 RVA: 0x00007C0B File Offset: 0x00005E0B
		protected virtual void CleanUpNativeResources()
		{
		}

		// Token: 0x0600042A RID: 1066 RVA: 0x00007C10 File Offset: 0x00005E10
		~DisposableClass()
		{
			this.CleanUpNativeResources();
		}
	}
}
