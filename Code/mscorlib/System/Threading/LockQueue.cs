using System;

namespace System.Threading
{
	// Token: 0x020006A3 RID: 1699
	internal class LockQueue
	{
		// Token: 0x060040A7 RID: 16551 RVA: 0x000DF2F4 File Offset: 0x000DD4F4
		public LockQueue(ReaderWriterLock rwlock)
		{
			this.rwlock = rwlock;
		}

		// Token: 0x060040A8 RID: 16552 RVA: 0x000DF304 File Offset: 0x000DD504
		public bool Wait(int timeout)
		{
			bool flag = false;
			bool result;
			try
			{
				lock (this)
				{
					this.lockCount++;
					Monitor.Exit(this.rwlock);
					flag = true;
					result = Monitor.Wait(this, timeout);
				}
			}
			finally
			{
				if (flag)
				{
					Monitor.Enter(this.rwlock);
					this.lockCount--;
				}
			}
			return result;
		}

		// Token: 0x17000C21 RID: 3105
		// (get) Token: 0x060040A9 RID: 16553 RVA: 0x000DF3AC File Offset: 0x000DD5AC
		public bool IsEmpty
		{
			get
			{
				bool result;
				lock (this)
				{
					result = (this.lockCount == 0);
				}
				return result;
			}
		}

		// Token: 0x060040AA RID: 16554 RVA: 0x000DF3FC File Offset: 0x000DD5FC
		public void Pulse()
		{
			lock (this)
			{
				Monitor.Pulse(this);
			}
		}

		// Token: 0x060040AB RID: 16555 RVA: 0x000DF440 File Offset: 0x000DD640
		public void PulseAll()
		{
			lock (this)
			{
				Monitor.PulseAll(this);
			}
		}

		// Token: 0x04001BB7 RID: 7095
		private ReaderWriterLock rwlock;

		// Token: 0x04001BB8 RID: 7096
		private int lockCount;
	}
}
