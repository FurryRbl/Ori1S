using System;
using System.ComponentModel;

namespace System.Diagnostics
{
	/// <summary>Specifies the formula used to calculate the <see cref="M:System.Diagnostics.PerformanceCounter.NextValue" /> method for a <see cref="T:System.Diagnostics.PerformanceCounter" /> instance.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000240 RID: 576
	[System.ComponentModel.TypeConverter(typeof(AlphabeticalEnumConverter))]
	public enum PerformanceCounterType
	{
		/// <summary>An instantaneous counter that shows the most recently observed value in hexadecimal format. Used, for example, to maintain a simple count of items or operations.</summary>
		// Token: 0x040005BD RID: 1469
		NumberOfItemsHEX32,
		/// <summary>An instantaneous counter that shows the most recently observed value. Used, for example, to maintain a simple count of a very large number of items or operations. It is the same as NumberOfItemsHEX32 except that it uses larger fields to accommodate larger values.</summary>
		// Token: 0x040005BE RID: 1470
		NumberOfItemsHEX64 = 256,
		/// <summary>An instantaneous counter that shows the most recently observed value. Used, for example, to maintain a simple count of items or operations.</summary>
		// Token: 0x040005BF RID: 1471
		NumberOfItems32 = 65536,
		/// <summary>An instantaneous counter that shows the most recently observed value. Used, for example, to maintain a simple count of a very large number of items or operations. It is the same as NumberOfItems32 except that it uses larger fields to accommodate larger values.</summary>
		// Token: 0x040005C0 RID: 1472
		NumberOfItems64 = 65792,
		/// <summary>A difference counter that shows the change in the measured attribute between the two most recent sample intervals.</summary>
		// Token: 0x040005C1 RID: 1473
		CounterDelta32 = 4195328,
		/// <summary>A difference counter that shows the change in the measured attribute between the two most recent sample intervals. It is the same as the CounterDelta32 counter type except that is uses larger fields to accomodate larger values.</summary>
		// Token: 0x040005C2 RID: 1474
		CounterDelta64 = 4195584,
		/// <summary>An average counter that shows the average number of operations completed in one second. When a counter of this type samples the data, each sampling interrupt returns one or zero. The counter data is the number of ones that were sampled. It measures time in units of ticks of the system performance timer.</summary>
		// Token: 0x040005C3 RID: 1475
		SampleCounter = 4260864,
		/// <summary>An average counter designed to monitor the average length of a queue to a resource over time. It shows the difference between the queue lengths observed during the last two sample intervals divided by the duration of the interval. This type of counter is typically used to track the number of items that are queued or waiting.</summary>
		// Token: 0x040005C4 RID: 1476
		CountPerTimeInterval32 = 4523008,
		/// <summary>An average counter that monitors the average length of a queue to a resource over time. Counters of this type display the difference between the queue lengths observed during the last two sample intervals, divided by the duration of the interval. This counter type is the same as CountPerTimeInterval32 except that it uses larger fields to accommodate larger values. This type of counter is typically used to track a high-volume or very large number of items that are queued or waiting.</summary>
		// Token: 0x040005C5 RID: 1477
		CountPerTimeInterval64 = 4523264,
		/// <summary>A difference counter that shows the average number of operations completed during each second of the sample interval. Counters of this type measure time in ticks of the system clock.</summary>
		// Token: 0x040005C6 RID: 1478
		RateOfCountsPerSecond32 = 272696320,
		/// <summary>A difference counter that shows the average number of operations completed during each second of the sample interval. Counters of this type measure time in ticks of the system clock. This counter type is the same as the RateOfCountsPerSecond32 type, but it uses larger fields to accommodate larger values to track a high-volume number of items or operations per second, such as a byte-transmission rate.</summary>
		// Token: 0x040005C7 RID: 1479
		RateOfCountsPerSecond64 = 272696576,
		/// <summary>An instantaneous percentage counter that shows the ratio of a subset to its set as a percentage. For example, it compares the number of bytes in use on a disk to the total number of bytes on the disk. Counters of this type display the current percentage only, not an average over time.</summary>
		// Token: 0x040005C8 RID: 1480
		RawFraction = 537003008,
		/// <summary>A percentage counter that shows the average time that a component is active as a percentage of the total sample time.</summary>
		// Token: 0x040005C9 RID: 1481
		CounterTimer = 541132032,
		/// <summary>A percentage counter that shows the active time of a component as a percentage of the total elapsed time of the sample interval. It measures time in units of 100 nanoseconds (ns). Counters of this type are designed to measure the activity of one component at a time.</summary>
		// Token: 0x040005CA RID: 1482
		Timer100Ns = 542180608,
		/// <summary>A percentage counter that shows the average ratio of hits to all operations during the last two sample intervals.</summary>
		// Token: 0x040005CB RID: 1483
		SampleFraction = 549585920,
		/// <summary>A percentage counter that displays the average percentage of active time observed during sample interval. The value of these counters is calculated by monitoring the percentage of time that the service was inactive and then subtracting that value from 100 percent.</summary>
		// Token: 0x040005CC RID: 1484
		CounterTimerInverse = 557909248,
		/// <summary>A percentage counter that shows the average percentage of active time observed during the sample interval.</summary>
		// Token: 0x040005CD RID: 1485
		Timer100NsInverse = 558957824,
		/// <summary>A percentage counter that displays the active time of one or more components as a percentage of the total time of the sample interval. Because the numerator records the active time of components operating simultaneously, the resulting percentage can exceed 100 percent.</summary>
		// Token: 0x040005CE RID: 1486
		CounterMultiTimer = 574686464,
		/// <summary>A percentage counter that shows the active time of one or more components as a percentage of the total time of the sample interval. It measures time in 100 nanosecond (ns) units.</summary>
		// Token: 0x040005CF RID: 1487
		CounterMultiTimer100Ns = 575735040,
		/// <summary>A percentage counter that shows the active time of one or more components as a percentage of the total time of the sample interval. It derives the active time by measuring the time that the components were not active and subtracting the result from 100 percent by the number of objects monitored.</summary>
		// Token: 0x040005D0 RID: 1488
		CounterMultiTimerInverse = 591463680,
		/// <summary>A percentage counter that shows the active time of one or more components as a percentage of the total time of the sample interval. Counters of this type measure time in 100 nanosecond (ns) units. They derive the active time by measuring the time that the components were not active and subtracting the result from multiplying 100 percent by the number of objects monitored.</summary>
		// Token: 0x040005D1 RID: 1489
		CounterMultiTimer100NsInverse = 592512256,
		/// <summary>An average counter that measures the time it takes, on average, to complete a process or operation. Counters of this type display a ratio of the total elapsed time of the sample interval to the number of processes or operations completed during that time. This counter type measures time in ticks of the system clock.</summary>
		// Token: 0x040005D2 RID: 1490
		AverageTimer32 = 805438464,
		/// <summary>A difference timer that shows the total time between when the component or process started and the time when this value is calculated.</summary>
		// Token: 0x040005D3 RID: 1491
		ElapsedTime = 807666944,
		/// <summary>An average counter that shows how many items are processed, on average, during an operation. Counters of this type display a ratio of the items processed to the number of operations completed. The ratio is calculated by comparing the number of items processed during the last interval to the number of operations completed during the last interval.</summary>
		// Token: 0x040005D4 RID: 1492
		AverageCount64 = 1073874176,
		/// <summary>A base counter that stores the number of sampling interrupts taken and is used as a denominator in the sampling fraction. The sampling fraction is the number of samples that were 1 (or true) for a sample interrupt. Check that this value is greater than zero before using it as the denominator in a calculation of SampleFraction.</summary>
		// Token: 0x040005D5 RID: 1493
		SampleBase = 1073939457,
		/// <summary>A base counter that is used in the calculation of time or count averages, such as AverageTimer32 and AverageCount64. Stores the denominator for calculating a counter to present "time per operation" or "count per operation".</summary>
		// Token: 0x040005D6 RID: 1494
		AverageBase,
		/// <summary>A base counter that stores the denominator of a counter that presents a general arithmetic fraction. Check that this value is greater than zero before using it as the denominator in a RawFraction value calculation.</summary>
		// Token: 0x040005D7 RID: 1495
		RawBase,
		/// <summary>A base counter that indicates the number of items sampled. It is used as the denominator in the calculations to get an average among the items sampled when taking timings of multiple, but similar items. Used with CounterMultiTimer, CounterMultiTimerInverse, CounterMultiTimer100Ns, and CounterMultiTimer100NsInverse.</summary>
		// Token: 0x040005D8 RID: 1496
		CounterMultiBase = 1107494144
	}
}
