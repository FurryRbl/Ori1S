using System;
using System.Security.Permissions;

namespace System.Threading
{
	// Token: 0x02000065 RID: 101
	[PermissionSet(SecurityAction.LinkDemand, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\nversion=\"1\">\n<IPermission class=\"System.Security.Permissions.HostProtectionPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\nversion=\"1\"\nResources=\"None\"/>\n</PermissionSet>\n")]
	public class ReaderWriterLockSlim : IDisposable
	{
		// Token: 0x06000560 RID: 1376 RVA: 0x00018708 File Offset: 0x00016908
		public ReaderWriterLockSlim()
		{
		}

		// Token: 0x06000561 RID: 1377 RVA: 0x0001871C File Offset: 0x0001691C
		public ReaderWriterLockSlim(LockRecursionPolicy recursionPolicy)
		{
			this.recursionPolicy = recursionPolicy;
			if (recursionPolicy != LockRecursionPolicy.NoRecursion)
			{
				throw new NotImplementedException("recursionPolicy != NoRecursion not currently implemented");
			}
		}

		// Token: 0x06000563 RID: 1379 RVA: 0x00018764 File Offset: 0x00016964
		public void EnterReadLock()
		{
			this.TryEnterReadLock(-1);
		}

		// Token: 0x06000564 RID: 1380 RVA: 0x00018770 File Offset: 0x00016970
		public bool TryEnterReadLock(int millisecondsTimeout)
		{
			if (millisecondsTimeout < -1)
			{
				throw new ArgumentOutOfRangeException("millisecondsTimeout");
			}
			if (this.read_locks == null)
			{
				throw new ObjectDisposedException(null);
			}
			if (Thread.CurrentThread == this.write_thread)
			{
				throw new LockRecursionException("Read lock cannot be acquired while write lock is held");
			}
			this.EnterMyLock();
			ReaderWriterLockSlim.LockDetails readLockDetails = this.GetReadLockDetails(Thread.CurrentThread.ManagedThreadId, true);
			if (readLockDetails.ReadLocks != 0)
			{
				this.ExitMyLock();
				throw new LockRecursionException("Recursive read lock can only be aquired in SupportsRecursion mode");
			}
			readLockDetails.ReadLocks++;
			while (this.owners < 0 || this.numWriteWaiters != 0U)
			{
				if (millisecondsTimeout == 0)
				{
					this.ExitMyLock();
					return false;
				}
				if (this.readEvent == null)
				{
					this.LazyCreateEvent(ref this.readEvent, false);
				}
				else if (!this.WaitOnEvent(this.readEvent, ref this.numReadWaiters, millisecondsTimeout))
				{
					return false;
				}
			}
			this.owners++;
			this.ExitMyLock();
			return true;
		}

		// Token: 0x06000565 RID: 1381 RVA: 0x0001887C File Offset: 0x00016A7C
		public bool TryEnterReadLock(TimeSpan timeout)
		{
			return this.TryEnterReadLock(ReaderWriterLockSlim.CheckTimeout(timeout));
		}

		// Token: 0x06000566 RID: 1382 RVA: 0x0001888C File Offset: 0x00016A8C
		public void ExitReadLock()
		{
			this.EnterMyLock();
			if (this.owners < 1)
			{
				this.ExitMyLock();
				throw new SynchronizationLockException("Releasing lock and no read lock taken");
			}
			this.owners--;
			this.GetReadLockDetails(Thread.CurrentThread.ManagedThreadId, false).ReadLocks--;
			this.ExitAndWakeUpAppropriateWaiters();
		}

		// Token: 0x06000567 RID: 1383 RVA: 0x000188F0 File Offset: 0x00016AF0
		public void EnterWriteLock()
		{
			this.TryEnterWriteLock(-1);
		}

		// Token: 0x06000568 RID: 1384 RVA: 0x000188FC File Offset: 0x00016AFC
		public bool TryEnterWriteLock(int millisecondsTimeout)
		{
			if (millisecondsTimeout < -1)
			{
				throw new ArgumentOutOfRangeException("millisecondsTimeout");
			}
			if (this.read_locks == null)
			{
				throw new ObjectDisposedException(null);
			}
			if (this.IsWriteLockHeld)
			{
				throw new LockRecursionException();
			}
			this.EnterMyLock();
			ReaderWriterLockSlim.LockDetails readLockDetails = this.GetReadLockDetails(Thread.CurrentThread.ManagedThreadId, false);
			if (readLockDetails != null && readLockDetails.ReadLocks > 0)
			{
				this.ExitMyLock();
				throw new LockRecursionException("Write lock cannot be acquired while read lock is held");
			}
			while (this.owners != 0)
			{
				if (this.owners == 1 && this.upgradable_thread == Thread.CurrentThread)
				{
					this.owners = -1;
					this.write_thread = Thread.CurrentThread;
					IL_178:
					this.ExitMyLock();
					return true;
				}
				if (millisecondsTimeout == 0)
				{
					this.ExitMyLock();
					return false;
				}
				if (this.upgradable_thread == Thread.CurrentThread)
				{
					if (this.upgradeEvent == null)
					{
						this.LazyCreateEvent(ref this.upgradeEvent, false);
					}
					else
					{
						if (this.numUpgradeWaiters > 0U)
						{
							this.ExitMyLock();
							throw new ApplicationException("Upgrading lock to writer lock already in process, deadlock");
						}
						if (!this.WaitOnEvent(this.upgradeEvent, ref this.numUpgradeWaiters, millisecondsTimeout))
						{
							return false;
						}
					}
				}
				else if (this.writeEvent == null)
				{
					this.LazyCreateEvent(ref this.writeEvent, true);
				}
				else if (!this.WaitOnEvent(this.writeEvent, ref this.numWriteWaiters, millisecondsTimeout))
				{
					return false;
				}
			}
			this.owners = -1;
			this.write_thread = Thread.CurrentThread;
			goto IL_178;
		}

		// Token: 0x06000569 RID: 1385 RVA: 0x00018A88 File Offset: 0x00016C88
		public bool TryEnterWriteLock(TimeSpan timeout)
		{
			return this.TryEnterWriteLock(ReaderWriterLockSlim.CheckTimeout(timeout));
		}

		// Token: 0x0600056A RID: 1386 RVA: 0x00018A98 File Offset: 0x00016C98
		public void ExitWriteLock()
		{
			this.EnterMyLock();
			if (this.owners != -1)
			{
				this.ExitMyLock();
				throw new SynchronizationLockException("Calling ExitWriterLock when no write lock is held");
			}
			if (this.upgradable_thread == Thread.CurrentThread)
			{
				this.owners = 1;
			}
			else
			{
				this.owners = 0;
			}
			this.write_thread = null;
			this.ExitAndWakeUpAppropriateWaiters();
		}

		// Token: 0x0600056B RID: 1387 RVA: 0x00018AF8 File Offset: 0x00016CF8
		public void EnterUpgradeableReadLock()
		{
			this.TryEnterUpgradeableReadLock(-1);
		}

		// Token: 0x0600056C RID: 1388 RVA: 0x00018B04 File Offset: 0x00016D04
		public bool TryEnterUpgradeableReadLock(int millisecondsTimeout)
		{
			if (millisecondsTimeout < -1)
			{
				throw new ArgumentOutOfRangeException("millisecondsTimeout");
			}
			if (this.read_locks == null)
			{
				throw new ObjectDisposedException(null);
			}
			if (this.IsUpgradeableReadLockHeld)
			{
				throw new LockRecursionException();
			}
			if (this.IsWriteLockHeld)
			{
				throw new LockRecursionException();
			}
			this.EnterMyLock();
			while (this.owners != 0 || this.numWriteWaiters != 0U || this.upgradable_thread != null)
			{
				if (millisecondsTimeout == 0)
				{
					this.ExitMyLock();
					return false;
				}
				if (this.readEvent == null)
				{
					this.LazyCreateEvent(ref this.readEvent, false);
				}
				else if (!this.WaitOnEvent(this.readEvent, ref this.numReadWaiters, millisecondsTimeout))
				{
					return false;
				}
			}
			this.owners++;
			this.upgradable_thread = Thread.CurrentThread;
			this.ExitMyLock();
			return true;
		}

		// Token: 0x0600056D RID: 1389 RVA: 0x00018BF0 File Offset: 0x00016DF0
		public bool TryEnterUpgradeableReadLock(TimeSpan timeout)
		{
			return this.TryEnterUpgradeableReadLock(ReaderWriterLockSlim.CheckTimeout(timeout));
		}

		// Token: 0x0600056E RID: 1390 RVA: 0x00018C00 File Offset: 0x00016E00
		public void ExitUpgradeableReadLock()
		{
			this.EnterMyLock();
			this.owners--;
			this.upgradable_thread = null;
			this.ExitAndWakeUpAppropriateWaiters();
		}

		// Token: 0x0600056F RID: 1391 RVA: 0x00018C24 File Offset: 0x00016E24
		public void Dispose()
		{
			this.read_locks = null;
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000570 RID: 1392 RVA: 0x00018C30 File Offset: 0x00016E30
		public bool IsReadLockHeld
		{
			get
			{
				return this.RecursiveReadCount != 0;
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x06000571 RID: 1393 RVA: 0x00018C40 File Offset: 0x00016E40
		public bool IsWriteLockHeld
		{
			get
			{
				return this.RecursiveWriteCount != 0;
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x06000572 RID: 1394 RVA: 0x00018C50 File Offset: 0x00016E50
		public bool IsUpgradeableReadLockHeld
		{
			get
			{
				return this.RecursiveUpgradeCount != 0;
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x06000573 RID: 1395 RVA: 0x00018C60 File Offset: 0x00016E60
		public int CurrentReadCount
		{
			get
			{
				return this.owners & 268435455;
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x06000574 RID: 1396 RVA: 0x00018C70 File Offset: 0x00016E70
		public int RecursiveReadCount
		{
			get
			{
				this.EnterMyLock();
				ReaderWriterLockSlim.LockDetails readLockDetails = this.GetReadLockDetails(Thread.CurrentThread.ManagedThreadId, false);
				int result = (readLockDetails != null) ? readLockDetails.ReadLocks : 0;
				this.ExitMyLock();
				return result;
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x06000575 RID: 1397 RVA: 0x00018CB0 File Offset: 0x00016EB0
		public int RecursiveUpgradeCount
		{
			get
			{
				return (this.upgradable_thread != Thread.CurrentThread) ? 0 : 1;
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x06000576 RID: 1398 RVA: 0x00018CCC File Offset: 0x00016ECC
		public int RecursiveWriteCount
		{
			get
			{
				return (this.write_thread != Thread.CurrentThread) ? 0 : 1;
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x06000577 RID: 1399 RVA: 0x00018CE8 File Offset: 0x00016EE8
		public int WaitingReadCount
		{
			get
			{
				return (int)this.numReadWaiters;
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x06000578 RID: 1400 RVA: 0x00018CF0 File Offset: 0x00016EF0
		public int WaitingUpgradeCount
		{
			get
			{
				return (int)this.numUpgradeWaiters;
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x06000579 RID: 1401 RVA: 0x00018CF8 File Offset: 0x00016EF8
		public int WaitingWriteCount
		{
			get
			{
				return (int)this.numWriteWaiters;
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x0600057A RID: 1402 RVA: 0x00018D00 File Offset: 0x00016F00
		public LockRecursionPolicy RecursionPolicy
		{
			get
			{
				return this.recursionPolicy;
			}
		}

		// Token: 0x0600057B RID: 1403 RVA: 0x00018D08 File Offset: 0x00016F08
		private void EnterMyLock()
		{
			if (Interlocked.CompareExchange(ref this.myLock, 1, 0) != 0)
			{
				this.EnterMyLockSpin();
			}
		}

		// Token: 0x0600057C RID: 1404 RVA: 0x00018D24 File Offset: 0x00016F24
		private void EnterMyLockSpin()
		{
			int num = 0;
			for (;;)
			{
				if (num < 3 && ReaderWriterLockSlim.smp)
				{
					Thread.SpinWait(20);
				}
				else
				{
					Thread.Sleep(0);
				}
				if (Interlocked.CompareExchange(ref this.myLock, 1, 0) == 0)
				{
					break;
				}
				num++;
			}
		}

		// Token: 0x0600057D RID: 1405 RVA: 0x00018D78 File Offset: 0x00016F78
		private void ExitMyLock()
		{
			this.myLock = 0;
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x0600057E RID: 1406 RVA: 0x00018D84 File Offset: 0x00016F84
		private bool MyLockHeld
		{
			get
			{
				return this.myLock != 0;
			}
		}

		// Token: 0x0600057F RID: 1407 RVA: 0x00018D94 File Offset: 0x00016F94
		private void ExitAndWakeUpAppropriateWaiters()
		{
			if (this.owners == 1 && this.numUpgradeWaiters != 0U)
			{
				this.ExitMyLock();
				this.upgradeEvent.Set();
			}
			else if (this.owners == 0 && this.numWriteWaiters > 0U)
			{
				this.ExitMyLock();
				this.writeEvent.Set();
			}
			else if (this.owners >= 0 && this.numReadWaiters != 0U)
			{
				this.ExitMyLock();
				this.readEvent.Set();
			}
			else
			{
				this.ExitMyLock();
			}
		}

		// Token: 0x06000580 RID: 1408 RVA: 0x00018E34 File Offset: 0x00017034
		private void LazyCreateEvent(ref EventWaitHandle waitEvent, bool makeAutoResetEvent)
		{
			this.ExitMyLock();
			EventWaitHandle eventWaitHandle;
			if (makeAutoResetEvent)
			{
				eventWaitHandle = new AutoResetEvent(false);
			}
			else
			{
				eventWaitHandle = new ManualResetEvent(false);
			}
			this.EnterMyLock();
			if (waitEvent == null)
			{
				waitEvent = eventWaitHandle;
			}
		}

		// Token: 0x06000581 RID: 1409 RVA: 0x00018E70 File Offset: 0x00017070
		private bool WaitOnEvent(EventWaitHandle waitEvent, ref uint numWaiters, int millisecondsTimeout)
		{
			waitEvent.Reset();
			numWaiters += 1U;
			bool flag = false;
			this.ExitMyLock();
			try
			{
				flag = waitEvent.WaitOne(millisecondsTimeout, false);
			}
			finally
			{
				this.EnterMyLock();
				numWaiters -= 1U;
				if (!flag)
				{
					this.ExitMyLock();
				}
			}
			return flag;
		}

		// Token: 0x06000582 RID: 1410 RVA: 0x00018ED8 File Offset: 0x000170D8
		private static int CheckTimeout(TimeSpan timeout)
		{
			int result;
			try
			{
				result = checked((int)timeout.TotalMilliseconds);
			}
			catch (OverflowException)
			{
				throw new ArgumentOutOfRangeException("timeout");
			}
			return result;
		}

		// Token: 0x06000583 RID: 1411 RVA: 0x00018F28 File Offset: 0x00017128
		private ReaderWriterLockSlim.LockDetails GetReadLockDetails(int threadId, bool create)
		{
			int i;
			ReaderWriterLockSlim.LockDetails lockDetails;
			for (i = 0; i < this.read_locks.Length; i++)
			{
				lockDetails = this.read_locks[i];
				if (lockDetails == null)
				{
					break;
				}
				if (lockDetails.ThreadId == threadId)
				{
					return lockDetails;
				}
			}
			if (!create)
			{
				return null;
			}
			if (i == this.read_locks.Length)
			{
				Array.Resize<ReaderWriterLockSlim.LockDetails>(ref this.read_locks, this.read_locks.Length * 2);
			}
			lockDetails = (this.read_locks[i] = new ReaderWriterLockSlim.LockDetails());
			lockDetails.ThreadId = threadId;
			return lockDetails;
		}

		// Token: 0x04000167 RID: 359
		private static readonly bool smp = Environment.ProcessorCount > 1;

		// Token: 0x04000168 RID: 360
		private int myLock;

		// Token: 0x04000169 RID: 361
		private int owners;

		// Token: 0x0400016A RID: 362
		private Thread upgradable_thread;

		// Token: 0x0400016B RID: 363
		private Thread write_thread;

		// Token: 0x0400016C RID: 364
		private uint numWriteWaiters;

		// Token: 0x0400016D RID: 365
		private uint numReadWaiters;

		// Token: 0x0400016E RID: 366
		private uint numUpgradeWaiters;

		// Token: 0x0400016F RID: 367
		private EventWaitHandle writeEvent;

		// Token: 0x04000170 RID: 368
		private EventWaitHandle readEvent;

		// Token: 0x04000171 RID: 369
		private EventWaitHandle upgradeEvent;

		// Token: 0x04000172 RID: 370
		private readonly LockRecursionPolicy recursionPolicy;

		// Token: 0x04000173 RID: 371
		private ReaderWriterLockSlim.LockDetails[] read_locks = new ReaderWriterLockSlim.LockDetails[8];

		// Token: 0x02000066 RID: 102
		private sealed class LockDetails
		{
			// Token: 0x04000174 RID: 372
			public int ThreadId;

			// Token: 0x04000175 RID: 373
			public int ReadLocks;
		}
	}
}
