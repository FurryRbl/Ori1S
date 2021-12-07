using System;

namespace System.Diagnostics
{
	/// <summary>Holds instance data associated with a performance counter sample.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000230 RID: 560
	public class InstanceData
	{
		/// <summary>Initializes a new instance of the InstanceData class, using the specified sample and performance counter instance.</summary>
		/// <param name="instanceName">The name of an instance associated with the performance counter. </param>
		/// <param name="sample">A <see cref="T:System.Diagnostics.CounterSample" /> taken from the instance specified by the <paramref name="instanceName" /> parameter. </param>
		// Token: 0x0600132D RID: 4909 RVA: 0x00033220 File Offset: 0x00031420
		public InstanceData(string instanceName, CounterSample sample)
		{
			this.instanceName = instanceName;
			this.sample = sample;
		}

		/// <summary>Gets the instance name associated with this instance data.</summary>
		/// <returns>The name of an instance associated with the performance counter.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000476 RID: 1142
		// (get) Token: 0x0600132E RID: 4910 RVA: 0x00033238 File Offset: 0x00031438
		public string InstanceName
		{
			get
			{
				return this.instanceName;
			}
		}

		/// <summary>Gets the raw data value associated with the performance counter sample.</summary>
		/// <returns>The raw value read by the performance counter sample associated with the <see cref="P:System.Diagnostics.InstanceData.Sample" /> property.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000477 RID: 1143
		// (get) Token: 0x0600132F RID: 4911 RVA: 0x00033240 File Offset: 0x00031440
		public long RawValue
		{
			get
			{
				return this.sample.RawValue;
			}
		}

		/// <summary>Gets the performance counter sample that generated this data.</summary>
		/// <returns>A <see cref="T:System.Diagnostics.CounterSample" /> taken from the instance specified by the <see cref="P:System.Diagnostics.InstanceData.InstanceName" /> property.</returns>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000478 RID: 1144
		// (get) Token: 0x06001330 RID: 4912 RVA: 0x00033250 File Offset: 0x00031450
		public CounterSample Sample
		{
			get
			{
				return this.sample;
			}
		}

		// Token: 0x0400058A RID: 1418
		private string instanceName;

		// Token: 0x0400058B RID: 1419
		private CounterSample sample;
	}
}
