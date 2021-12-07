using System;

namespace System.Diagnostics
{
	/// <summary>Defines a structure that holds the raw data for a performance counter.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000213 RID: 531
	public struct CounterSample
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.CounterSample" /> structure and sets the <see cref="P:System.Diagnostics.CounterSample.CounterTimeStamp" /> property to 0 (zero).</summary>
		/// <param name="rawValue">The numeric value associated with the performance counter sample. </param>
		/// <param name="baseValue">An optional, base raw value for the counter, to use only if the sample is based on multiple counters. </param>
		/// <param name="counterFrequency">The frequency with which the counter is read. </param>
		/// <param name="systemFrequency">The frequency with which the system reads from the counter. </param>
		/// <param name="timeStamp">The raw time stamp. </param>
		/// <param name="timeStamp100nSec">The raw, high-fidelity time stamp. </param>
		/// <param name="counterType">A <see cref="T:System.Diagnostics.PerformanceCounterType" /> object that indicates the type of the counter for which this sample is a snapshot. </param>
		// Token: 0x060011B6 RID: 4534 RVA: 0x0002F2A8 File Offset: 0x0002D4A8
		public CounterSample(long rawValue, long baseValue, long counterFrequency, long systemFrequency, long timeStamp, long timeStamp100nSec, PerformanceCounterType counterType)
		{
			this = new CounterSample(rawValue, baseValue, counterFrequency, systemFrequency, timeStamp, timeStamp100nSec, counterType, 0L);
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.CounterSample" /> structure and sets the <see cref="P:System.Diagnostics.CounterSample.CounterTimeStamp" /> property to the value that is passed in.</summary>
		/// <param name="rawValue">The numeric value associated with the performance counter sample. </param>
		/// <param name="baseValue">An optional, base raw value for the counter, to use only if the sample is based on multiple counters. </param>
		/// <param name="counterFrequency">The frequency with which the counter is read. </param>
		/// <param name="systemFrequency">The frequency with which the system reads from the counter. </param>
		/// <param name="timeStamp">The raw time stamp. </param>
		/// <param name="timeStamp100nSec">The raw, high-fidelity time stamp. </param>
		/// <param name="counterType">A <see cref="T:System.Diagnostics.PerformanceCounterType" /> object that indicates the type of the counter for which this sample is a snapshot. </param>
		/// <param name="counterTimeStamp">The time at which the sample was taken. </param>
		// Token: 0x060011B7 RID: 4535 RVA: 0x0002F2C8 File Offset: 0x0002D4C8
		public CounterSample(long rawValue, long baseValue, long counterFrequency, long systemFrequency, long timeStamp, long timeStamp100nSec, PerformanceCounterType counterType, long counterTimeStamp)
		{
			this.rawValue = rawValue;
			this.baseValue = baseValue;
			this.counterFrequency = counterFrequency;
			this.systemFrequency = systemFrequency;
			this.timeStamp = timeStamp;
			this.timeStamp100nSec = timeStamp100nSec;
			this.counterType = counterType;
			this.counterTimeStamp = counterTimeStamp;
		}

		/// <summary>Gets an optional, base raw value for the counter.</summary>
		/// <returns>The base raw value, which is used only if the sample is based on multiple counters.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000408 RID: 1032
		// (get) Token: 0x060011B9 RID: 4537 RVA: 0x0002F334 File Offset: 0x0002D534
		public long BaseValue
		{
			get
			{
				return this.baseValue;
			}
		}

		/// <summary>Gets the raw counter frequency.</summary>
		/// <returns>The frequency with which the counter is read.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000409 RID: 1033
		// (get) Token: 0x060011BA RID: 4538 RVA: 0x0002F33C File Offset: 0x0002D53C
		public long CounterFrequency
		{
			get
			{
				return this.counterFrequency;
			}
		}

		/// <summary>Gets the counter's time stamp.</summary>
		/// <returns>The time at which the sample was taken.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700040A RID: 1034
		// (get) Token: 0x060011BB RID: 4539 RVA: 0x0002F344 File Offset: 0x0002D544
		public long CounterTimeStamp
		{
			get
			{
				return this.counterTimeStamp;
			}
		}

		/// <summary>Gets the performance counter type.</summary>
		/// <returns>A <see cref="T:System.Diagnostics.PerformanceCounterType" /> object that indicates the type of the counter for which this sample is a snapshot.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700040B RID: 1035
		// (get) Token: 0x060011BC RID: 4540 RVA: 0x0002F34C File Offset: 0x0002D54C
		public PerformanceCounterType CounterType
		{
			get
			{
				return this.counterType;
			}
		}

		/// <summary>Gets the raw value of the counter.</summary>
		/// <returns>The numeric value that is associated with the performance counter sample.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700040C RID: 1036
		// (get) Token: 0x060011BD RID: 4541 RVA: 0x0002F354 File Offset: 0x0002D554
		public long RawValue
		{
			get
			{
				return this.rawValue;
			}
		}

		/// <summary>Gets the raw system frequency.</summary>
		/// <returns>The frequency with which the system reads from the counter.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700040D RID: 1037
		// (get) Token: 0x060011BE RID: 4542 RVA: 0x0002F35C File Offset: 0x0002D55C
		public long SystemFrequency
		{
			get
			{
				return this.systemFrequency;
			}
		}

		/// <summary>Gets the raw time stamp.</summary>
		/// <returns>The system time stamp.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700040E RID: 1038
		// (get) Token: 0x060011BF RID: 4543 RVA: 0x0002F364 File Offset: 0x0002D564
		public long TimeStamp
		{
			get
			{
				return this.timeStamp;
			}
		}

		/// <summary>Gets the raw, high-fidelity time stamp.</summary>
		/// <returns>The system time stamp, represented within 0.1 millisecond.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x1700040F RID: 1039
		// (get) Token: 0x060011C0 RID: 4544 RVA: 0x0002F36C File Offset: 0x0002D56C
		public long TimeStamp100nSec
		{
			get
			{
				return this.timeStamp100nSec;
			}
		}

		/// <summary>Calculates the performance data of the counter, using a single sample point. This method is generally used for uncalculated performance counter types.</summary>
		/// <returns>The calculated performance value.</returns>
		/// <param name="counterSample">The <see cref="T:System.Diagnostics.CounterSample" /> structure to use as a base point for calculating performance data. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x060011C1 RID: 4545 RVA: 0x0002F374 File Offset: 0x0002D574
		public static float Calculate(CounterSample counterSample)
		{
			return CounterSampleCalculator.ComputeCounterValue(counterSample);
		}

		/// <summary>Calculates the performance data of the counter, using two sample points. This method is generally used for calculated performance counter types, such as averages.</summary>
		/// <returns>The calculated performance value.</returns>
		/// <param name="counterSample">The <see cref="T:System.Diagnostics.CounterSample" /> structure to use as a base point for calculating performance data. </param>
		/// <param name="nextCounterSample">The <see cref="T:System.Diagnostics.CounterSample" /> structure to use as an ending point for calculating performance data. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x060011C2 RID: 4546 RVA: 0x0002F37C File Offset: 0x0002D57C
		public static float Calculate(CounterSample counterSample, CounterSample nextCounterSample)
		{
			return CounterSampleCalculator.ComputeCounterValue(counterSample, nextCounterSample);
		}

		/// <summary>Indicates whether the specified structure is a <see cref="T:System.Diagnostics.CounterSample" /> structure and is identical to the current <see cref="T:System.Diagnostics.CounterSample" /> structure.</summary>
		/// <returns>true if <paramref name="o" /> is a <see cref="T:System.Diagnostics.CounterSample" /> structure and is identical to the current instance; otherwise, false. </returns>
		/// <param name="o">The <see cref="T:System.Diagnostics.CounterSample" /> structure to be compared with the current structure.</param>
		// Token: 0x060011C3 RID: 4547 RVA: 0x0002F388 File Offset: 0x0002D588
		public override bool Equals(object obj)
		{
			return obj is CounterSample && this.Equals((CounterSample)obj);
		}

		/// <summary>Indicates whether the specified <see cref="T:System.Diagnostics.CounterSample" /> structure is equal to the current <see cref="T:System.Diagnostics.CounterSample" /> structure.</summary>
		/// <returns>true if <paramref name="sample" /> is equal to the current instance; otherwise, false. </returns>
		/// <param name="sample">The <see cref="T:System.Diagnostics.CounterSample" /> structure to be compared with this instance.</param>
		// Token: 0x060011C4 RID: 4548 RVA: 0x0002F3A4 File Offset: 0x0002D5A4
		public bool Equals(CounterSample other)
		{
			return this.rawValue == other.rawValue && this.baseValue == other.counterFrequency && this.counterFrequency == other.counterFrequency && this.systemFrequency == other.systemFrequency && this.timeStamp == other.timeStamp && this.timeStamp100nSec == other.timeStamp100nSec && this.counterTimeStamp == other.counterTimeStamp && this.counterType == other.counterType;
		}

		/// <summary>Gets a hash code for the current counter sample.</summary>
		/// <returns>A hash code for the current counter sample.</returns>
		// Token: 0x060011C5 RID: 4549 RVA: 0x0002F444 File Offset: 0x0002D644
		public override int GetHashCode()
		{
			return (int)(this.rawValue << 28 ^ (this.baseValue << 24 ^ (this.counterFrequency << 20 ^ (this.systemFrequency << 16 ^ (this.timeStamp << 8 ^ (this.timeStamp100nSec << 4 ^ (this.counterTimeStamp ^ (long)this.counterType)))))));
		}

		/// <summary>Returns a value that indicates whether two <see cref="T:System.Diagnostics.CounterSample" /> structures are equal.</summary>
		/// <returns>true if <paramref name="a" /> and <paramref name="b" /> are equal; otherwise, false.</returns>
		/// <param name="a">A <see cref="T:System.Diagnostics.CounterSample" /> structure.</param>
		/// <param name="b">Another <see cref="T:System.Diagnostics.CounterSample" /> structure to be compared to the structure specified by the <paramref name="a" /> parameter.</param>
		// Token: 0x060011C6 RID: 4550 RVA: 0x0002F49C File Offset: 0x0002D69C
		public static bool operator ==(CounterSample obj1, CounterSample obj2)
		{
			return obj1.Equals(obj2);
		}

		/// <summary>Returns a value that indicates whether two <see cref="T:System.Diagnostics.CounterSample" /> structures are not equal.</summary>
		/// <returns>true if <paramref name="a" /> and <paramref name="b" /> are not equal; otherwise, false</returns>
		/// <param name="a">A <see cref="T:System.Diagnostics.CounterSample" /> structure.</param>
		/// <param name="b">Another <see cref="T:System.Diagnostics.CounterSample" /> structure to be compared to the structure specified by the <paramref name="a" /> parameter.</param>
		// Token: 0x060011C7 RID: 4551 RVA: 0x0002F4A8 File Offset: 0x0002D6A8
		public static bool operator !=(CounterSample obj1, CounterSample obj2)
		{
			return !obj1.Equals(obj2);
		}

		// Token: 0x0400050E RID: 1294
		private long rawValue;

		// Token: 0x0400050F RID: 1295
		private long baseValue;

		// Token: 0x04000510 RID: 1296
		private long counterFrequency;

		// Token: 0x04000511 RID: 1297
		private long systemFrequency;

		// Token: 0x04000512 RID: 1298
		private long timeStamp;

		// Token: 0x04000513 RID: 1299
		private long timeStamp100nSec;

		// Token: 0x04000514 RID: 1300
		private long counterTimeStamp;

		// Token: 0x04000515 RID: 1301
		private PerformanceCounterType counterType;

		/// <summary>Defines an empty, uninitialized performance counter sample of type NumberOfItems32.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x04000516 RID: 1302
		public static CounterSample Empty = new CounterSample(0L, 0L, 0L, 0L, 0L, 0L, PerformanceCounterType.NumberOfItems32, 0L);
	}
}
