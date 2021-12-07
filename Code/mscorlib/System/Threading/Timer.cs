using System;
using System.Collections;
using System.Runtime.InteropServices;

namespace System.Threading
{
	/// <summary>Provides a mechanism for executing a method at specified intervals. This class cannot be inherited.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x020006B7 RID: 1719
	[ComVisible(true)]
	public sealed class Timer : MarshalByRefObject, IDisposable
	{
		/// <summary>Initializes a new instance of the Timer class, using a 32-bit signed integer to specify the time interval.</summary>
		/// <param name="callback">A <see cref="T:System.Threading.TimerCallback" /> delegate representing a method to be executed. </param>
		/// <param name="state">An object containing information to be used by the callback method, or null. </param>
		/// <param name="dueTime">The amount of time to delay before <paramref name="callback" /> is invoked, in milliseconds. Specify <see cref="F:System.Threading.Timeout.Infinite" /> to prevent the timer from starting. Specify zero (0) to start the timer immediately. </param>
		/// <param name="period">The time interval between invocations of <paramref name="callback" />, in milliseconds. Specify <see cref="F:System.Threading.Timeout.Infinite" /> to disable periodic signaling. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="dueTime" /> or <paramref name="period" /> parameter is negative and is not equal to <see cref="F:System.Threading.Timeout.Infinite" />. </exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="callback" /> parameter is null. </exception>
		// Token: 0x060041AF RID: 16815 RVA: 0x000E1598 File Offset: 0x000DF798
		public Timer(TimerCallback callback, object state, int dueTime, int period)
		{
			this.Init(callback, state, (long)dueTime, (long)period);
		}

		/// <summary>Initializes a new instance of the Timer class, using 64-bit signed integers to measure time intervals.</summary>
		/// <param name="callback">A <see cref="T:System.Threading.TimerCallback" /> delegate representing a method to be executed. </param>
		/// <param name="state">An object containing information to be used by the callback method, or null. </param>
		/// <param name="dueTime">The amount of time to delay before <paramref name="callback" /> is invoked, in milliseconds. Specify <see cref="F:System.Threading.Timeout.Infinite" /> to prevent the timer from starting. Specify zero (0) to start the timer immediately. </param>
		/// <param name="period">The time interval between invocations of <paramref name="callback" />, in milliseconds. Specify <see cref="F:System.Threading.Timeout.Infinite" /> to disable periodic signaling. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="dueTime" /> or <paramref name="period" /> parameter is negative and is not equal to <see cref="F:System.Threading.Timeout.Infinite" />. </exception>
		/// <exception cref="T:System.NotSupportedException">The <paramref name="dueTime" /> or <paramref name="period" /> parameter is greater than 4294967294. </exception>
		// Token: 0x060041B0 RID: 16816 RVA: 0x000E15B0 File Offset: 0x000DF7B0
		public Timer(TimerCallback callback, object state, long dueTime, long period)
		{
			this.Init(callback, state, dueTime, period);
		}

		/// <summary>Initializes a new instance of the Timer class, using <see cref="T:System.TimeSpan" /> values to measure time intervals.</summary>
		/// <param name="callback">A <see cref="T:System.Threading.TimerCallback" /> delegate representing a method to be executed. </param>
		/// <param name="state">An object containing information to be used by the callback method, or null. </param>
		/// <param name="dueTime">The <see cref="T:System.TimeSpan" /> representing the amount of time to delay before the <paramref name="callback" /> parameter invokes its methods. Specify negative one (-1) milliseconds to prevent the timer from starting. Specify zero (0) to start the timer immediately. </param>
		/// <param name="period">The time interval between invocations of the methods referenced by <paramref name="callback" />. Specify negative one (-1) milliseconds to disable periodic signaling. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The number of milliseconds in the value of <paramref name="dueTime" /> or <paramref name="period" /> is negative and not equal to <see cref="F:System.Threading.Timeout.Infinite" />, or is greater than <see cref="F:System.Int32.MaxValue" />. </exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="callback" /> parameter is null. </exception>
		// Token: 0x060041B1 RID: 16817 RVA: 0x000E15C4 File Offset: 0x000DF7C4
		public Timer(TimerCallback callback, object state, TimeSpan dueTime, TimeSpan period)
		{
			this.Init(callback, state, (long)dueTime.TotalMilliseconds, (long)period.TotalMilliseconds);
		}

		/// <summary>Initializes a new instance of the Timer class, using 32-bit unsigned integers to measure time intervals.</summary>
		/// <param name="callback">A <see cref="T:System.Threading.TimerCallback" /> delegate representing a method to be executed. </param>
		/// <param name="state">An object containing information to be used by the callback method, or null. </param>
		/// <param name="dueTime">The amount of time to delay before <paramref name="callback" /> is invoked, in milliseconds. Specify <see cref="F:System.Threading.Timeout.Infinite" /> to prevent the timer from starting. Specify zero (0) to start the timer immediately. </param>
		/// <param name="period">The time interval between invocations of <paramref name="callback" />, in milliseconds. Specify <see cref="F:System.Threading.Timeout.Infinite" /> to disable periodic signaling. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="dueTime" /> or <paramref name="period" /> parameter is negative and is not equal to <see cref="F:System.Threading.Timeout.Infinite" />. </exception>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="callback" /> parameter is null. </exception>
		// Token: 0x060041B2 RID: 16818 RVA: 0x000E15F0 File Offset: 0x000DF7F0
		[CLSCompliant(false)]
		public Timer(TimerCallback callback, object state, uint dueTime, uint period)
		{
			long dueTime2 = (long)((dueTime != uint.MaxValue) ? ((ulong)dueTime) : ulong.MaxValue);
			long period2 = (long)((period != uint.MaxValue) ? ((ulong)period) : ulong.MaxValue);
			this.Init(callback, state, dueTime2, period2);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Threading.Timer" /> class with an infinite period and an infinite due time, using the newly created <see cref="T:System.Threading.Timer" /> object as the state object. </summary>
		/// <param name="callback">A <see cref="T:System.Threading.TimerCallback" /> delegate representing a method to be executed.</param>
		// Token: 0x060041B3 RID: 16819 RVA: 0x000E1634 File Offset: 0x000DF834
		public Timer(TimerCallback callback)
		{
			this.Init(callback, this, -1L, -1L);
		}

		// Token: 0x060041B5 RID: 16821 RVA: 0x000E1654 File Offset: 0x000DF854
		private void Init(TimerCallback callback, object state, long dueTime, long period)
		{
			if (callback == null)
			{
				throw new ArgumentNullException("callback");
			}
			this.callback = callback;
			this.state = state;
			this.Change(dueTime, period, true);
		}

		/// <summary>Changes the start time and the interval between method invocations for a timer, using 32-bit signed integers to measure time intervals.</summary>
		/// <returns>true if the timer was successfully updated; otherwise, false.</returns>
		/// <param name="dueTime">The amount of time to delay before the invoking the callback method specified when the <see cref="T:System.Threading.Timer" /> was constructed, in milliseconds. Specify <see cref="F:System.Threading.Timeout.Infinite" /> to prevent the timer from restarting. Specify zero (0) to restart the timer immediately. </param>
		/// <param name="period">The time interval between invocations of the callback method specified when the <see cref="T:System.Threading.Timer" /> was constructed, in milliseconds. Specify <see cref="F:System.Threading.Timeout.Infinite" /> to disable periodic signaling. </param>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Threading.Timer" /> has already been disposed. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="dueTime" /> or <paramref name="period" /> parameter is negative and is not equal to <see cref="F:System.Threading.Timeout.Infinite" />. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060041B6 RID: 16822 RVA: 0x000E168C File Offset: 0x000DF88C
		public bool Change(int dueTime, int period)
		{
			return this.Change((long)dueTime, (long)period, false);
		}

		/// <summary>Changes the start time and the interval between method invocations for a timer, using <see cref="T:System.TimeSpan" /> values to measure time intervals.</summary>
		/// <returns>true if the timer was successfully updated; otherwise, false.</returns>
		/// <param name="dueTime">A <see cref="T:System.TimeSpan" /> representing the amount of time to delay before invoking the callback method specified when the <see cref="T:System.Threading.Timer" /> was constructed. Specify negative one (-1) milliseconds to prevent the timer from restarting. Specify zero (0) to restart the timer immediately. </param>
		/// <param name="period">The time interval between invocations of the callback method specified when the <see cref="T:System.Threading.Timer" /> was constructed. Specify negative one (-1) milliseconds to disable periodic signaling. </param>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Threading.Timer" /> has already been disposed. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="dueTime" /> or <paramref name="period" /> parameter, in milliseconds, is less than -1. </exception>
		/// <exception cref="T:System.NotSupportedException">The <paramref name="dueTime" /> or <paramref name="period" /> parameter, in milliseconds, is greater than 4294967294. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060041B7 RID: 16823 RVA: 0x000E169C File Offset: 0x000DF89C
		public bool Change(TimeSpan dueTime, TimeSpan period)
		{
			return this.Change((long)dueTime.TotalMilliseconds, (long)period.TotalMilliseconds, false);
		}

		/// <summary>Changes the start time and the interval between method invocations for a timer, using 32-bit unsigned integers to measure time intervals.</summary>
		/// <returns>true if the timer was successfully updated; otherwise, false.</returns>
		/// <param name="dueTime">The amount of time to delay before the invoking the callback method specified when the <see cref="T:System.Threading.Timer" /> was constructed, in milliseconds. Specify <see cref="F:System.Threading.Timeout.Infinite" /> to prevent the timer from restarting. Specify zero (0) to restart the timer immediately. </param>
		/// <param name="period">The time interval between invocations of the callback method specified when the <see cref="T:System.Threading.Timer" /> was constructed, in milliseconds. Specify <see cref="F:System.Threading.Timeout.Infinite" /> to disable periodic signaling. </param>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Threading.Timer" /> has already been disposed. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060041B8 RID: 16824 RVA: 0x000E16C0 File Offset: 0x000DF8C0
		[CLSCompliant(false)]
		public bool Change(uint dueTime, uint period)
		{
			long dueTime2 = (long)((dueTime != uint.MaxValue) ? ((ulong)dueTime) : ulong.MaxValue);
			long period2 = (long)((period != uint.MaxValue) ? ((ulong)period) : ulong.MaxValue);
			return this.Change(dueTime2, period2, false);
		}

		/// <summary>Releases all resources used by the current instance of <see cref="T:System.Threading.Timer" />.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060041B9 RID: 16825 RVA: 0x000E16F8 File Offset: 0x000DF8F8
		public void Dispose()
		{
			if (this.disposed)
			{
				return;
			}
			this.disposed = true;
			Timer.scheduler.Remove(this);
		}

		/// <summary>Changes the start time and the interval between method invocations for a timer, using 64-bit signed integers to measure time intervals.</summary>
		/// <returns>true if the timer was successfully updated; otherwise, false.</returns>
		/// <param name="dueTime">The amount of time to delay before the invoking the callback method specified when the <see cref="T:System.Threading.Timer" /> was constructed, in milliseconds. Specify <see cref="F:System.Threading.Timeout.Infinite" /> to prevent the timer from restarting. Specify zero (0) to restart the timer immediately. </param>
		/// <param name="period">The time interval between invocations of the callback method specified when the <see cref="T:System.Threading.Timer" /> was constructed, in milliseconds. Specify <see cref="F:System.Threading.Timeout.Infinite" /> to disable periodic signaling. </param>
		/// <exception cref="T:System.ObjectDisposedException">The <see cref="T:System.Threading.Timer" /> has already been disposed. </exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The <paramref name="dueTime" /> or <paramref name="period" /> parameter is less than -1. </exception>
		/// <exception cref="T:System.NotSupportedException">The <paramref name="dueTime" /> or <paramref name="period" /> parameter is greater than 4294967294. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060041BA RID: 16826 RVA: 0x000E1718 File Offset: 0x000DF918
		public bool Change(long dueTime, long period)
		{
			return this.Change(dueTime, period, false);
		}

		// Token: 0x060041BB RID: 16827 RVA: 0x000E1724 File Offset: 0x000DF924
		private bool Change(long dueTime, long period, bool first)
		{
			if (dueTime > (long)((ulong)-2))
			{
				throw new ArgumentOutOfRangeException("Due time too large");
			}
			if (period > (long)((ulong)-2))
			{
				throw new ArgumentOutOfRangeException("Period too large");
			}
			if (dueTime < -1L)
			{
				throw new ArgumentOutOfRangeException("dueTime");
			}
			if (period < -1L)
			{
				throw new ArgumentOutOfRangeException("period");
			}
			if (this.disposed)
			{
				return false;
			}
			this.due_time_ms = dueTime;
			this.period_ms = period;
			long new_next_run;
			if (dueTime == 0L)
			{
				new_next_run = 0L;
			}
			else if (dueTime < 0L)
			{
				new_next_run = long.MaxValue;
				if (first)
				{
					this.next_run = new_next_run;
					return true;
				}
			}
			else
			{
				new_next_run = dueTime * 10000L + DateTime.GetTimeMonotonic();
			}
			Timer.scheduler.Change(this, new_next_run);
			return true;
		}

		/// <summary>Releases all resources used by the current instance of <see cref="T:System.Threading.Timer" /> and signals when the timer has been disposed of.</summary>
		/// <returns>true if the function succeeds; otherwise, false.</returns>
		/// <param name="notifyObject">The <see cref="T:System.Threading.WaitHandle" /> to be signaled when the Timer has been disposed of. </param>
		/// <exception cref="T:System.ArgumentNullException">The <paramref name="notifyObject" /> parameter is null. </exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060041BC RID: 16828 RVA: 0x000E17EC File Offset: 0x000DF9EC
		public bool Dispose(WaitHandle notifyObject)
		{
			if (notifyObject == null)
			{
				throw new ArgumentNullException("notifyObject");
			}
			this.Dispose();
			NativeEventCalls.SetEvent_internal(notifyObject.Handle);
			return true;
		}

		// Token: 0x04001C1C RID: 7196
		private const long MaxValue = 4294967294L;

		// Token: 0x04001C1D RID: 7197
		private static Timer.Scheduler scheduler = Timer.Scheduler.Instance;

		// Token: 0x04001C1E RID: 7198
		private TimerCallback callback;

		// Token: 0x04001C1F RID: 7199
		private object state;

		// Token: 0x04001C20 RID: 7200
		private long due_time_ms;

		// Token: 0x04001C21 RID: 7201
		private long period_ms;

		// Token: 0x04001C22 RID: 7202
		private long next_run;

		// Token: 0x04001C23 RID: 7203
		private bool disposed;

		// Token: 0x020006B8 RID: 1720
		private sealed class TimerComparer : IComparer
		{
			// Token: 0x060041BE RID: 16830 RVA: 0x000E1828 File Offset: 0x000DFA28
			public int Compare(object x, object y)
			{
				Timer timer = x as Timer;
				if (timer == null)
				{
					return -1;
				}
				Timer timer2 = y as Timer;
				if (timer2 == null)
				{
					return 1;
				}
				long num = timer.next_run - timer2.next_run;
				if (num == 0L)
				{
					return (x != y) ? -1 : 0;
				}
				return (num <= 0L) ? -1 : 1;
			}
		}

		// Token: 0x020006B9 RID: 1721
		private sealed class Scheduler
		{
			// Token: 0x060041BF RID: 16831 RVA: 0x000E1888 File Offset: 0x000DFA88
			private Scheduler()
			{
				this.list = new SortedList(new Timer.TimerComparer(), 1024);
				new Thread(new ThreadStart(this.SchedulerThread))
				{
					IsBackground = true
				}.Start();
			}

			// Token: 0x17000C3C RID: 3132
			// (get) Token: 0x060041C1 RID: 16833 RVA: 0x000E18DC File Offset: 0x000DFADC
			public static Timer.Scheduler Instance
			{
				get
				{
					return Timer.Scheduler.instance;
				}
			}

			// Token: 0x060041C2 RID: 16834 RVA: 0x000E18E4 File Offset: 0x000DFAE4
			public void Remove(Timer timer)
			{
				if (timer.next_run == 0L || timer.next_run == 9223372036854775807L)
				{
					return;
				}
				lock (this)
				{
					this.InternalRemove(timer);
				}
			}

			// Token: 0x060041C3 RID: 16835 RVA: 0x000E194C File Offset: 0x000DFB4C
			public void Change(Timer timer, long new_next_run)
			{
				lock (this)
				{
					this.InternalRemove(timer);
					if (new_next_run == 9223372036854775807L)
					{
						timer.next_run = new_next_run;
					}
					else if (!timer.disposed)
					{
						timer.next_run = new_next_run;
						this.Add(timer);
						if (this.list.GetByIndex(0) == timer)
						{
							Monitor.Pulse(this);
						}
					}
				}
			}

			// Token: 0x060041C4 RID: 16836 RVA: 0x000E19E0 File Offset: 0x000DFBE0
			private void Add(Timer timer)
			{
				int num = this.list.IndexOfKey(timer);
				if (num != -1)
				{
					bool flag = long.MaxValue - timer.next_run > 20000L;
					Timer timer2;
					do
					{
						num++;
						if (flag)
						{
							timer.next_run += 1L;
						}
						else
						{
							timer.next_run -= 1L;
						}
						if (num >= this.list.Count)
						{
							break;
						}
						timer2 = (Timer)this.list.GetByIndex(num);
					}
					while (timer2.next_run == timer.next_run);
				}
				this.list.Add(timer, timer);
			}

			// Token: 0x060041C5 RID: 16837 RVA: 0x000E1AA4 File Offset: 0x000DFCA4
			private int InternalRemove(Timer timer)
			{
				int num = this.list.IndexOfKey(timer);
				if (num >= 0)
				{
					this.list.RemoveAt(num);
				}
				return num;
			}

			// Token: 0x060041C6 RID: 16838 RVA: 0x000E1AD4 File Offset: 0x000DFCD4
			private void SchedulerThread()
			{
				Thread.CurrentThread.Name = "Timer-Scheduler";
				ArrayList arrayList = new ArrayList(512);
				for (;;)
				{
					long timeMonotonic = DateTime.GetTimeMonotonic();
					lock (this)
					{
						int num = this.list.Count;
						for (int i = 0; i < num; i++)
						{
							Timer timer = (Timer)this.list.GetByIndex(i);
							if (timer.next_run > timeMonotonic)
							{
								break;
							}
							this.list.RemoveAt(i);
							num--;
							i--;
							ThreadPool.QueueUserWorkItem(new WaitCallback(timer.callback.Invoke), timer.state);
							long period_ms = timer.period_ms;
							long due_time_ms = timer.due_time_ms;
							bool flag = period_ms == -1L || ((period_ms == 0L || period_ms == -1L) && due_time_ms != -1L);
							if (flag)
							{
								timer.next_run = long.MaxValue;
							}
							else
							{
								timer.next_run = DateTime.GetTimeMonotonic() + 10000L * timer.period_ms;
								arrayList.Add(timer);
							}
						}
						num = arrayList.Count;
						for (int i = 0; i < num; i++)
						{
							Timer timer2 = (Timer)arrayList[i];
							this.Add(timer2);
						}
						arrayList.Clear();
						this.ShrinkIfNeeded(arrayList, 512);
						int capacity = this.list.Capacity;
						num = this.list.Count;
						if (capacity > 1024 && num > 0 && capacity / num > 3)
						{
							this.list.Capacity = num * 2;
						}
						long num2 = long.MaxValue;
						if (this.list.Count > 0)
						{
							num2 = ((Timer)this.list.GetByIndex(0)).next_run;
						}
						int num3 = -1;
						if (num2 != 9223372036854775807L)
						{
							long num4 = num2 - DateTime.GetTimeMonotonic();
							num3 = (int)(num4 / 10000L);
							if (num3 < 0)
							{
								num3 = 0;
							}
						}
						Monitor.Wait(this, num3);
					}
				}
			}

			// Token: 0x060041C7 RID: 16839 RVA: 0x000E1D28 File Offset: 0x000DFF28
			private void ShrinkIfNeeded(ArrayList list, int initial)
			{
				int capacity = list.Capacity;
				int count = list.Count;
				if (capacity > initial && count > 0 && capacity / count > 3)
				{
					list.Capacity = count * 2;
				}
			}

			// Token: 0x04001C24 RID: 7204
			private static Timer.Scheduler instance = new Timer.Scheduler();

			// Token: 0x04001C25 RID: 7205
			private SortedList list;
		}
	}
}
