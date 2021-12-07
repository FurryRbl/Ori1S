using System;
using System.Runtime.CompilerServices;

namespace System.Diagnostics
{
	/// <summary>Provides a set of methods and properties that you can use to accurately measure elapsed time.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x02000253 RID: 595
	public class Stopwatch
	{
		/// <summary>Gets the current number of ticks in the timer mechanism.</summary>
		/// <returns>A long integer representing the tick counter value of the underlying timer mechanism.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060014E7 RID: 5351
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern long GetTimestamp();

		/// <summary>Initializes a new <see cref="T:System.Diagnostics.Stopwatch" /> instance, sets the elapsed time property to zero, and starts measuring elapsed time.</summary>
		/// <returns>A <see cref="T:System.Diagnostics.Stopwatch" /> that has just begun measuring elapsed time.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060014E8 RID: 5352 RVA: 0x00037508 File Offset: 0x00035708
		public static Stopwatch StartNew()
		{
			Stopwatch stopwatch = new Stopwatch();
			stopwatch.Start();
			return stopwatch;
		}

		/// <summary>Gets the total elapsed time measured by the current instance.</summary>
		/// <returns>A read-only <see cref="T:System.TimeSpan" /> representing the total elapsed time measured by the current instance.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000501 RID: 1281
		// (get) Token: 0x060014E9 RID: 5353 RVA: 0x00037524 File Offset: 0x00035724
		public TimeSpan Elapsed
		{
			get
			{
				if (Stopwatch.IsHighResolution)
				{
					return TimeSpan.FromTicks(this.ElapsedTicks / (Stopwatch.Frequency / 10000000L));
				}
				return TimeSpan.FromTicks(this.ElapsedTicks);
			}
		}

		/// <summary>Gets the total elapsed time measured by the current instance, in milliseconds.</summary>
		/// <returns>A read-only long integer representing the total number of milliseconds measured by the current instance.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000502 RID: 1282
		// (get) Token: 0x060014EA RID: 5354 RVA: 0x00037560 File Offset: 0x00035760
		public long ElapsedMilliseconds
		{
			get
			{
				if (Stopwatch.IsHighResolution)
				{
					return this.ElapsedTicks / (Stopwatch.Frequency / 1000L);
				}
				return checked((long)this.Elapsed.TotalMilliseconds);
			}
		}

		/// <summary>Gets the total elapsed time measured by the current instance, in timer ticks.</summary>
		/// <returns>A read-only long integer representing the total number of timer ticks measured by the current instance.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000503 RID: 1283
		// (get) Token: 0x060014EB RID: 5355 RVA: 0x0003759C File Offset: 0x0003579C
		public long ElapsedTicks
		{
			get
			{
				return (!this.is_running) ? this.elapsed : (Stopwatch.GetTimestamp() - this.started + this.elapsed);
			}
		}

		/// <summary>Gets a value indicating whether the <see cref="T:System.Diagnostics.Stopwatch" /> timer is running.</summary>
		/// <returns>true if the <see cref="T:System.Diagnostics.Stopwatch" /> instance is currently running and measuring elapsed time for an interval; otherwise, false.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x17000504 RID: 1284
		// (get) Token: 0x060014EC RID: 5356 RVA: 0x000375C8 File Offset: 0x000357C8
		public bool IsRunning
		{
			get
			{
				return this.is_running;
			}
		}

		/// <summary>Stops time interval measurement and resets the elapsed time to zero.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060014ED RID: 5357 RVA: 0x000375D0 File Offset: 0x000357D0
		public void Reset()
		{
			this.elapsed = 0L;
			this.is_running = false;
		}

		/// <summary>Starts, or resumes, measuring elapsed time for an interval.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060014EE RID: 5358 RVA: 0x000375E4 File Offset: 0x000357E4
		public void Start()
		{
			if (this.is_running)
			{
				return;
			}
			this.started = Stopwatch.GetTimestamp();
			this.is_running = true;
		}

		/// <summary>Stops measuring elapsed time for an interval.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060014EF RID: 5359 RVA: 0x00037604 File Offset: 0x00035804
		public void Stop()
		{
			if (!this.is_running)
			{
				return;
			}
			this.elapsed += Stopwatch.GetTimestamp() - this.started;
			this.is_running = false;
		}

		/// <summary>Gets the frequency of the timer as the number of ticks per second. This field is read-only.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x04000658 RID: 1624
		public static readonly long Frequency = 10000000L;

		/// <summary>Indicates whether the timer is based on a high-resolution performance counter. This field is read-only.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x04000659 RID: 1625
		public static readonly bool IsHighResolution = true;

		// Token: 0x0400065A RID: 1626
		private long elapsed;

		// Token: 0x0400065B RID: 1627
		private long started;

		// Token: 0x0400065C RID: 1628
		private bool is_running;
	}
}
