﻿using System;

namespace System.Diagnostics
{
	/// <summary>Provides a set of utility functions for interpreting performance counter data.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000212 RID: 530
	public static class CounterSampleCalculator
	{
		/// <summary>Computes the calculated value of a single raw counter sample.</summary>
		/// <returns>A floating-point representation of the performance counter's calculated value.</returns>
		/// <param name="newSample">A <see cref="T:System.Diagnostics.CounterSample" /> that indicates the most recent sample the system has taken. </param>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x060011B4 RID: 4532 RVA: 0x0002EF0C File Offset: 0x0002D10C
		public static float ComputeCounterValue(CounterSample newSample)
		{
			PerformanceCounterType counterType = newSample.CounterType;
			if (counterType != PerformanceCounterType.NumberOfItemsHEX32 && counterType != PerformanceCounterType.NumberOfItemsHEX64 && counterType != PerformanceCounterType.NumberOfItems32 && counterType != PerformanceCounterType.NumberOfItems64 && counterType != PerformanceCounterType.RawFraction)
			{
				return 0f;
			}
			return (float)newSample.RawValue;
		}

		/// <summary>Computes the calculated value of two raw counter samples.</summary>
		/// <returns>A floating-point representation of the performance counter's calculated value.</returns>
		/// <param name="oldSample">A <see cref="T:System.Diagnostics.CounterSample" /> that indicates a previous sample the system has taken. </param>
		/// <param name="newSample">A <see cref="T:System.Diagnostics.CounterSample" /> that indicates the most recent sample the system has taken. </param>
		/// <exception cref="T:System.InvalidOperationException">
		///   <paramref name="oldSample" /> uses a counter type that is different from <paramref name="newSample" />. </exception>
		/// <exception cref="T:System.ComponentModel.Win32Exception">
		///   <paramref name="newSample" /> counter type has a Performance Data Helper (PDH) error. For more information, see "Checking PDH Interface Return Values" in the Win32 and COM Development section of this documentation.</exception>
		/// <filterpriority>1</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.EnvironmentPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Unrestricted="true" />
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode" />
		/// </PermissionSet>
		// Token: 0x060011B5 RID: 4533 RVA: 0x0002EF68 File Offset: 0x0002D168
		[MonoTODO("What's the algorithm?")]
		public static float ComputeCounterValue(CounterSample oldSample, CounterSample newSample)
		{
			if (newSample.CounterType != oldSample.CounterType)
			{
				throw new Exception("The counter samples must be of the same type");
			}
			PerformanceCounterType counterType = newSample.CounterType;
			if (counterType != PerformanceCounterType.NumberOfItemsHEX32 && counterType != PerformanceCounterType.NumberOfItemsHEX64 && counterType != PerformanceCounterType.NumberOfItems32 && counterType != PerformanceCounterType.NumberOfItems64)
			{
				if (counterType != PerformanceCounterType.CounterDelta32 && counterType != PerformanceCounterType.CounterDelta64)
				{
					if (counterType != PerformanceCounterType.CountPerTimeInterval32 && counterType != PerformanceCounterType.CountPerTimeInterval64)
					{
						if (counterType == PerformanceCounterType.RateOfCountsPerSecond32 || counterType == PerformanceCounterType.RateOfCountsPerSecond64)
						{
							return (float)(newSample.RawValue - oldSample.RawValue) / (float)(newSample.TimeStamp - oldSample.TimeStamp) * 10000000f;
						}
						if (counterType == PerformanceCounterType.RawFraction)
						{
							goto IL_118;
						}
						if (counterType != PerformanceCounterType.CounterTimer)
						{
							if (counterType == PerformanceCounterType.Timer100Ns)
							{
								return (float)(newSample.RawValue - oldSample.RawValue) / (float)(newSample.TimeStamp - oldSample.TimeStamp) * 100f;
							}
							if (counterType == PerformanceCounterType.CounterTimerInverse)
							{
								return (1f - (float)(newSample.RawValue - oldSample.RawValue) / (float)(newSample.TimeStamp100nSec - oldSample.TimeStamp100nSec)) * 100f;
							}
							if (counterType == PerformanceCounterType.Timer100NsInverse)
							{
								return (1f - (float)(newSample.RawValue - oldSample.RawValue) / (float)(newSample.TimeStamp - oldSample.TimeStamp)) * 100f;
							}
							if (counterType == PerformanceCounterType.CounterMultiTimer)
							{
								return (float)(newSample.RawValue - oldSample.RawValue) / (float)(newSample.TimeStamp - oldSample.TimeStamp) * 100f / (float)newSample.BaseValue;
							}
							if (counterType == PerformanceCounterType.CounterMultiTimer100Ns)
							{
								return (float)(newSample.RawValue - oldSample.RawValue) / (float)(newSample.TimeStamp100nSec - oldSample.TimeStamp100nSec) * 100f / (float)newSample.BaseValue;
							}
							if (counterType == PerformanceCounterType.CounterMultiTimerInverse)
							{
								return ((float)newSample.BaseValue - (float)(newSample.RawValue - oldSample.RawValue) / (float)(newSample.TimeStamp - oldSample.TimeStamp)) * 100f;
							}
							if (counterType == PerformanceCounterType.CounterMultiTimer100NsInverse)
							{
								return ((float)newSample.BaseValue - (float)(newSample.RawValue - oldSample.RawValue) / (float)(newSample.TimeStamp100nSec - oldSample.TimeStamp100nSec)) * 100f;
							}
							if (counterType == PerformanceCounterType.AverageTimer32)
							{
								return (float)(newSample.RawValue - oldSample.RawValue) / (float)newSample.SystemFrequency / (float)(newSample.BaseValue - oldSample.BaseValue);
							}
							if (counterType == PerformanceCounterType.ElapsedTime)
							{
								return 0f;
							}
							if (counterType != PerformanceCounterType.AverageCount64)
							{
								Console.WriteLine("Counter type {0} not handled", newSample.CounterType);
								return 0f;
							}
							return (float)(newSample.RawValue - oldSample.RawValue) / (float)(newSample.BaseValue - oldSample.BaseValue);
						}
					}
					return (float)(newSample.RawValue - oldSample.RawValue) / (float)(newSample.TimeStamp - oldSample.TimeStamp);
				}
				return (float)(newSample.RawValue - oldSample.RawValue);
			}
			IL_118:
			return (float)newSample.RawValue;
		}
	}
}
