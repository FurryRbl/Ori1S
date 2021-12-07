using System;
using System.Configuration;

namespace System.Net.Configuration
{
	/// <summary>Represents the performance counter element in the System.Net configuration file that determines whether the usage of performance counters is enabled. This class cannot be inherited.</summary>
	// Token: 0x020002DD RID: 733
	public sealed class PerformanceCountersElement : ConfigurationElement
	{
		// Token: 0x0600190F RID: 6415 RVA: 0x000451FC File Offset: 0x000433FC
		static PerformanceCountersElement()
		{
			PerformanceCountersElement.properties.Add(PerformanceCountersElement.enabledProp);
		}

		/// <summary>Gets or sets whether performance counters are enabled.</summary>
		/// <returns>true if performance counters are enabled; otherwise, false.</returns>
		// Token: 0x170005EF RID: 1519
		// (get) Token: 0x06001910 RID: 6416 RVA: 0x00045244 File Offset: 0x00043444
		// (set) Token: 0x06001911 RID: 6417 RVA: 0x00045258 File Offset: 0x00043458
		[ConfigurationProperty("enabled", DefaultValue = "False")]
		public bool Enabled
		{
			get
			{
				return (bool)base[PerformanceCountersElement.enabledProp];
			}
			set
			{
				base[PerformanceCountersElement.enabledProp] = value;
			}
		}

		// Token: 0x170005F0 RID: 1520
		// (get) Token: 0x06001912 RID: 6418 RVA: 0x0004526C File Offset: 0x0004346C
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return PerformanceCountersElement.properties;
			}
		}

		// Token: 0x04000FD4 RID: 4052
		private static ConfigurationProperty enabledProp = new ConfigurationProperty("enabled", typeof(bool), false);

		// Token: 0x04000FD5 RID: 4053
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();
	}
}
