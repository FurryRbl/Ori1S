using System;
using System.Collections;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;

namespace System.Threading
{
	/// <summary>Defines a lock that supports single writers and multiple readers. </summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020006AA RID: 1706
	[ComVisible(true)]
	public sealed class ReaderWriterLock : CriticalFinalizerObject
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.ReaderWriterLock" /> class.</summary>
		// Token: 0x060040E5 RID: 16613 RVA: 0x000DFA98 File Offset: 0x000DDC98
		public ReaderWriterLock()
		{
			this.writer_queue = new LockQueue(this);
			this.reader_locks = new Hashtable();
			GC.SuppressFinalize(this);
		}

		// Token: 0x060040E6 RID: 16614 RVA: 0x000DFAD0 File Offset: 0x000DDCD0
		[MonoTODO]
		~ReaderWriterLock()
		{
		}

		/// <summary>Gets a value indicating whether the current thread holds a reader lock.</summary>
		/// <returns>true if the current thread holds a reader lock; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000C27 RID: 3111
		// (get) Token: 0x060040E7 RID: 16615 RVA: 0x000DFB08 File Offset: 0x000DDD08
		public bool IsReaderLockHeld
		{
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			get
			{
				bool result;
				lock (this)
				{
					result = this.reader_locks.ContainsKey(Thread.CurrentThreadId);
				}
				return result;
			}
		}

		/// <summary>Gets a value indicating whether the current thread holds the writer lock.</summary>
		/// <returns>true if the current thread holds the writer lock; otherwise, false.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000C28 RID: 3112
		// (get) Token: 0x060040E8 RID: 16616 RVA: 0x000DFB64 File Offset: 0x000DDD64
		public bool IsWriterLockHeld
		{
			[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
			get
			{
				bool result;
				lock (this)
				{
					result = (this.state < 0 && Thread.CurrentThreadId == this.writer_lock_owner);
				}
				return result;
			}
		}

		/// <summary>Gets the current sequence number.</summary>
		/// <returns>The current sequence number.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000C29 RID: 3113
		// (get) Token: 0x060040E9 RID: 16617 RVA: 0x000DFBC4 File Offset: 0x000DDDC4
		public int WriterSeqNum
		{
			get
			{
				int result;
				lock (this)
				{
					result = this.seq_num;
				}
				return result;
			}
		}

		/// <summary>Acquires a reader lock, using an <see cref="T:System.Int32" /> value for the time-out.</summary>
		/// <param name="millisecondsTimeout">The time-out in milliseconds. </param>
		/// <exception cref="T:System.ApplicationException">
		///   <paramref name="millisecondsTimeout" /> expires before the lock request is granted. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060040EA RID: 16618 RVA: 0x000DFC10 File Offset: 0x000DDE10
		public void AcquireReaderLock(int millisecondsTimeout)
		{
			this.AcquireReaderLock(millisecondsTimeout, 1);
		}

		// Token: 0x060040EB RID: 16619 RVA: 0x000DFC1C File Offset: 0x000DDE1C
		private void AcquireReaderLock(int millisecondsTimeout, int initialLockCount)
		{
			lock (this)
			{
				if (this.HasWriterLock())
				{
					this.AcquireWriterLock(millisecondsTimeout, initialLockCount);
				}
				else
				{
					object obj = this.reader_locks[Thread.CurrentThreadId];
					if (obj == null)
					{
						this.readers++;
						try
						{
							if (this.state < 0 || !this.writer_queue.IsEmpty)
							{
								while (Monitor.Wait(this, millisecondsTimeout))
								{
									if (this.state >= 0)
									{
										goto IL_89;
									}
								}
								throw new ApplicationException("Timeout expired");
							}
							IL_89:;
						}
						finally
						{
							this.readers--;
						}
						this.reader_locks[Thread.CurrentThreadId] = initialLockCount;
						this.state += initialLockCount;
					}
					else
					{
						this.reader_locks[Thread.CurrentThreadId] = (int)obj + 1;
						this.state++;
					}
				}
			}
		}

		/// <summary>Acquires a reader lock, using a <see cref="T:System.TimeSpan" /> value for the time-out.</summary>
		/// <param name="timeout">A TimeSpan specifying the time-out period. </param>
		/// <exception cref="T:System.ApplicationException">
		///   <paramref name="timeout" /> expires before the lock request is granted. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="timeout" /> specifies a negative value other than -1 milliseconds. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060040EC RID: 16620 RVA: 0x000DFD64 File Offset: 0x000DDF64
		public void AcquireReaderLock(TimeSpan timeout)
		{
			int millisecondsTimeout = this.CheckTimeout(timeout);
			this.AcquireReaderLock(millisecondsTimeout, 1);
		}

		/// <summary>Acquires the writer lock, using an <see cref="T:System.Int32" /> value for the time-out.</summary>
		/// <param name="millisecondsTimeout">The time-out in milliseconds. </param>
		/// <exception cref="T:System.ApplicationException">
		///   <paramref name="timeout" /> expires before the lock request is granted. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060040ED RID: 16621 RVA: 0x000DFD84 File Offset: 0x000DDF84
		public void AcquireWriterLock(int millisecondsTimeout)
		{
			this.AcquireWriterLock(millisecondsTimeout, 1);
		}

		// Token: 0x060040EE RID: 16622 RVA: 0x000DFD90 File Offset: 0x000DDF90
		private void AcquireWriterLock(int millisecondsTimeout, int initialLockCount)
		{
			lock (this)
			{
				if (this.HasWriterLock())
				{
					this.state--;
				}
				else
				{
					if (this.state != 0 || !this.writer_queue.IsEmpty)
					{
						while (this.writer_queue.Wait(millisecondsTimeout))
						{
							if (this.state == 0)
							{
								goto IL_68;
							}
						}
						throw new ApplicationException("Timeout expired");
					}
					IL_68:
					this.state = -initialLockCount;
					this.writer_lock_owner = Thread.CurrentThreadId;
					this.seq_num++;
				}
			}
		}

		/// <summary>Acquires the writer lock, using a <see cref="T:System.TimeSpan" /> value for the time-out.</summary>
		/// <param name="timeout">The TimeSpan specifying the time-out period. </param>
		/// <exception cref="T:System.ApplicationException">
		///   <paramref name="timeout" /> expires before the lock request is granted. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="timeout" /> specifies a negative value other than -1 milliseconds. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060040EF RID: 16623 RVA: 0x000DFE50 File Offset: 0x000DE050
		public void AcquireWriterLock(TimeSpan timeout)
		{
			int millisecondsTimeout = this.CheckTimeout(timeout);
			this.AcquireWriterLock(millisecondsTimeout, 1);
		}

		/// <summary>Indicates whether the writer lock has been granted to any thread since the sequence number was obtained.</summary>
		/// <returns>true if the writer lock has been granted to any thread since the sequence number was obtained; otherwise, false.</returns>
		/// <param name="seqNum">The sequence number. </param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060040F0 RID: 16624 RVA: 0x000DFE70 File Offset: 0x000DE070
		public bool AnyWritersSince(int seqNum)
		{
			bool result;
			lock (this)
			{
				result = (this.seq_num > seqNum);
			}
			return result;
		}

		/// <summary>Restores the lock status of the thread to what it was before <see cref="M:System.Threading.ReaderWriterLock.UpgradeToWriterLock(System.Int32)" /> was called.</summary>
		/// <param name="lockCookie">A <see cref="T:System.Threading.LockCookie" /> returned by <see cref="M:System.Threading.ReaderWriterLock.UpgradeToWriterLock(System.Int32)" />. </param>
		/// <exception cref="T:System.ApplicationException">The thread does not have the writer lock. </exception>
		/// <exception cref="T:System.NullReferenceException">The address of <paramref name="lockCookie" /> is a null pointer. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060040F1 RID: 16625 RVA: 0x000DFEC0 File Offset: 0x000DE0C0
		public void DowngradeFromWriterLock(ref LockCookie lockCookie)
		{
			lock (this)
			{
				if (!this.HasWriterLock())
				{
					throw new ApplicationException("The thread does not have the writer lock.");
				}
				this.state = lockCookie.ReaderLocks;
				this.reader_locks[Thread.CurrentThreadId] = this.state;
				if (this.readers > 0)
				{
					Monitor.PulseAll(this);
				}
			}
		}

		/// <summary>Releases the lock, regardless of the number of times the thread acquired the lock.</summary>
		/// <returns>A <see cref="T:System.Threading.LockCookie" /> value representing the released lock.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060040F2 RID: 16626 RVA: 0x000DFF54 File Offset: 0x000DE154
		public LockCookie ReleaseLock()
		{
			LockCookie lockCookie;
			lock (this)
			{
				lockCookie = this.GetLockCookie();
				if (lockCookie.WriterLocks != 0)
				{
					this.ReleaseWriterLock(lockCookie.WriterLocks);
				}
				else if (lockCookie.ReaderLocks != 0)
				{
					this.ReleaseReaderLock(lockCookie.ReaderLocks, lockCookie.ReaderLocks);
				}
			}
			return lockCookie;
		}

		/// <summary>Decrements the lock count.</summary>
		/// <exception cref="T:System.ApplicationException">The thread does not have any reader or writer locks. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060040F3 RID: 16627 RVA: 0x000DFFD8 File Offset: 0x000DE1D8
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public void ReleaseReaderLock()
		{
			lock (this)
			{
				if (!this.HasWriterLock())
				{
					if (this.state > 0)
					{
						object obj = this.reader_locks[Thread.CurrentThreadId];
						if (obj != null)
						{
							this.ReleaseReaderLock((int)obj, 1);
							return;
						}
					}
					throw new ApplicationException("The thread does not have any reader or writer locks.");
				}
				this.ReleaseWriterLock();
			}
		}

		// Token: 0x060040F4 RID: 16628 RVA: 0x000E0070 File Offset: 0x000DE270
		private void ReleaseReaderLock(int currentCount, int releaseCount)
		{
			int num = currentCount - releaseCount;
			if (num == 0)
			{
				this.reader_locks.Remove(Thread.CurrentThreadId);
			}
			else
			{
				this.reader_locks[Thread.CurrentThreadId] = num;
			}
			this.state -= releaseCount;
			if (this.state == 0 && !this.writer_queue.IsEmpty)
			{
				this.writer_queue.Pulse();
			}
		}

		/// <summary>Decrements the lock count on the writer lock.</summary>
		/// <exception cref="T:System.ApplicationException">The thread does not have the writer lock. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060040F5 RID: 16629 RVA: 0x000E00F0 File Offset: 0x000DE2F0
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public void ReleaseWriterLock()
		{
			lock (this)
			{
				if (!this.HasWriterLock())
				{
					throw new ApplicationException("The thread does not have the writer lock.");
				}
				this.ReleaseWriterLock(1);
			}
		}

		// Token: 0x060040F6 RID: 16630 RVA: 0x000E014C File Offset: 0x000DE34C
		private void ReleaseWriterLock(int releaseCount)
		{
			this.state += releaseCount;
			if (this.state == 0)
			{
				if (this.readers > 0)
				{
					Monitor.PulseAll(this);
				}
				else if (!this.writer_queue.IsEmpty)
				{
					this.writer_queue.Pulse();
				}
			}
		}

		/// <summary>Restores the lock status of the thread to what it was before calling <see cref="M:System.Threading.ReaderWriterLock.ReleaseLock" />.</summary>
		/// <param name="lockCookie">A <see cref="T:System.Threading.LockCookie" /> returned by <see cref="M:System.Threading.ReaderWriterLock.ReleaseLock" />. </param>
		/// <exception cref="T:System.NullReferenceException">The address of <paramref name="lockCookie" /> is a null pointer. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060040F7 RID: 16631 RVA: 0x000E01A4 File Offset: 0x000DE3A4
		public void RestoreLock(ref LockCookie lockCookie)
		{
			lock (this)
			{
				if (lockCookie.WriterLocks != 0)
				{
					this.AcquireWriterLock(-1, lockCookie.WriterLocks);
				}
				else if (lockCookie.ReaderLocks != 0)
				{
					this.AcquireReaderLock(-1, lockCookie.ReaderLocks);
				}
			}
		}

		/// <summary>Upgrades a reader lock to the writer lock, using an Int32 value for the time-out.</summary>
		/// <returns>A <see cref="T:System.Threading.LockCookie" /> value.</returns>
		/// <param name="millisecondsTimeout">The time-out in milliseconds. </param>
		/// <exception cref="T:System.ApplicationException">
		///   <paramref name="millisecondsTimeout" /> expires before the lock request is granted. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060040F8 RID: 16632 RVA: 0x000E0218 File Offset: 0x000DE418
		public LockCookie UpgradeToWriterLock(int millisecondsTimeout)
		{
			LockCookie lockCookie;
			lock (this)
			{
				lockCookie = this.GetLockCookie();
				if (lockCookie.WriterLocks != 0)
				{
					this.state--;
					return lockCookie;
				}
				if (lockCookie.ReaderLocks != 0)
				{
					this.ReleaseReaderLock(lockCookie.ReaderLocks, lockCookie.ReaderLocks);
				}
			}
			this.AcquireWriterLock(millisecondsTimeout);
			return lockCookie;
		}

		/// <summary>Upgrades a reader lock to the writer lock, using a TimeSpan value for the time-out.</summary>
		/// <returns>A <see cref="T:System.Threading.LockCookie" /> value.</returns>
		/// <param name="timeout">The TimeSpan specifying the time-out period. </param>
		/// <exception cref="T:System.ApplicationException">
		///   <paramref name="timeout" /> expires before the lock request is granted. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="timeout" /> specifies a negative value other than -1 milliseconds. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060040F9 RID: 16633 RVA: 0x000E02A8 File Offset: 0x000DE4A8
		public LockCookie UpgradeToWriterLock(TimeSpan timeout)
		{
			int millisecondsTimeout = this.CheckTimeout(timeout);
			return this.UpgradeToWriterLock(millisecondsTimeout);
		}

		// Token: 0x060040FA RID: 16634 RVA: 0x000E02C4 File Offset: 0x000DE4C4
		private LockCookie GetLockCookie()
		{
			LockCookie result = new LockCookie(Thread.CurrentThreadId);
			if (this.HasWriterLock())
			{
				result.WriterLocks = -this.state;
			}
			else
			{
				object obj = this.reader_locks[Thread.CurrentThreadId];
				if (obj != null)
				{
					result.ReaderLocks = (int)obj;
				}
			}
			return result;
		}

		// Token: 0x060040FB RID: 16635 RVA: 0x000E0328 File Offset: 0x000DE528
		private bool HasWriterLock()
		{
			return this.state < 0 && Thread.CurrentThreadId == this.writer_lock_owner;
		}

		// Token: 0x060040FC RID: 16636 RVA: 0x000E0348 File Offset: 0x000DE548
		private int CheckTimeout(TimeSpan timeout)
		{
			int num = (int)timeout.TotalMilliseconds;
			if (num < -1)
			{
				throw new ArgumentOutOfRangeException("timeout", "Number must be either non-negative or -1");
			}
			return num;
		}

		// Token: 0x04001BC5 RID: 7109
		private int seq_num = 1;

		// Token: 0x04001BC6 RID: 7110
		private int state;

		// Token: 0x04001BC7 RID: 7111
		private int readers;

		// Token: 0x04001BC8 RID: 7112
		private LockQueue writer_queue;

		// Token: 0x04001BC9 RID: 7113
		private Hashtable reader_locks;

		// Token: 0x04001BCA RID: 7114
		private int writer_lock_owner;
	}
}
