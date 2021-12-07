using System;
using System.ComponentModel;

namespace System.Diagnostics
{
	/// <summary>Defines the counter type, name, and Help string for a custom counter.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x02000211 RID: 529
	[System.ComponentModel.TypeConverter("System.Diagnostics.Design.CounterCreationDataConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[Serializable]
	public class CounterCreationData
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.CounterCreationData" /> class, to a counter of type NumberOfItems32, and with empty name and help strings.</summary>
		// Token: 0x060011AC RID: 4524 RVA: 0x0002EE2C File Offset: 0x0002D02C
		public CounterCreationData()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Diagnostics.CounterCreationData" /> class, to a counter of the specified type, using the specified counter name and Help strings.</summary>
		/// <param name="counterName">The name of the counter, which must be unique within its category. </param>
		/// <param name="counterHelp">The text that describes the counter's behavior. </param>
		/// <param name="counterType">A <see cref="T:System.Diagnostics.PerformanceCounterType" /> that identifies the counter's behavior. </param>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">You have specified a value for <paramref name="counterType" /> that is not a member of the <see cref="T:System.Diagnostics.PerformanceCounterType" /> enumeration. </exception>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="counterHelp" /> is null. </exception>
		// Token: 0x060011AD RID: 4525 RVA: 0x0002EE40 File Offset: 0x0002D040
		public CounterCreationData(string counterName, string counterHelp, PerformanceCounterType counterType)
		{
			this.CounterName = counterName;
			this.CounterHelp = counterHelp;
			this.CounterType = counterType;
		}

		/// <summary>Gets or sets the custom counter's description.</summary>
		/// <returns>The text that describes the counter's behavior.</returns>
		/// <exception cref="T:System.ArgumentNullException">The specified value is null.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000405 RID: 1029
		// (get) Token: 0x060011AE RID: 4526 RVA: 0x0002EE74 File Offset: 0x0002D074
		// (set) Token: 0x060011AF RID: 4527 RVA: 0x0002EE7C File Offset: 0x0002D07C
		[MonitoringDescription("Description of this counter.")]
		[System.ComponentModel.DefaultValue("")]
		public string CounterHelp
		{
			get
			{
				return this.help;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.help = value;
			}
		}

		/// <summary>Gets or sets the name of the custom counter.</summary>
		/// <returns>A name for the counter, which is unique in its category.</returns>
		/// <exception cref="T:System.ArgumentNullException">The specified value is null.</exception>
		/// <exception cref="T:System.ArgumentException">The specified value is not between 1 and 80 characters long or contains double quotes, control characters or leading or trailing spaces.</exception>
		/// <filterpriority>2</filterpriority>
		// Token: 0x17000406 RID: 1030
		// (get) Token: 0x060011B0 RID: 4528 RVA: 0x0002EE98 File Offset: 0x0002D098
		// (set) Token: 0x060011B1 RID: 4529 RVA: 0x0002EEA0 File Offset: 0x0002D0A0
		[MonitoringDescription("Name of this counter.")]
		[System.ComponentModel.DefaultValue("")]
		[System.ComponentModel.TypeConverter("System.Diagnostics.Design.StringValueConverter, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public string CounterName
		{
			get
			{
				return this.name;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (value == string.Empty)
				{
					throw new ArgumentException("value");
				}
				this.name = value;
			}
		}

		/// <summary>Gets or sets the performance counter type of the custom counter.</summary>
		/// <returns>A <see cref="T:System.Diagnostics.PerformanceCounterType" /> that defines the behavior of the performance counter.</returns>
		/// <exception cref="T:System.ComponentModel.InvalidEnumArgumentException">You have specified a type that is not a member of the <see cref="T:System.Diagnostics.PerformanceCounterType" /> enumeration. </exception>
		/// <filterpriority>2</filterpriority>
		/// <PermissionSet>
		///   <IPermission class="System.Security.Permissions.SecurityPermission, mscorlib, Version=2.0.3600.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" version="1" Flags="UnmanagedCode, ControlEvidence" />
		/// </PermissionSet>
		// Token: 0x17000407 RID: 1031
		// (get) Token: 0x060011B2 RID: 4530 RVA: 0x0002EED8 File Offset: 0x0002D0D8
		// (set) Token: 0x060011B3 RID: 4531 RVA: 0x0002EEE0 File Offset: 0x0002D0E0
		[MonitoringDescription("Type of this counter.")]
		[System.ComponentModel.DefaultValue(typeof(PerformanceCounterType), "NumberOfItems32")]
		public PerformanceCounterType CounterType
		{
			get
			{
				return this.type;
			}
			set
			{
				if (!Enum.IsDefined(typeof(PerformanceCounterType), value))
				{
					throw new System.ComponentModel.InvalidEnumArgumentException();
				}
				this.type = value;
			}
		}

		// Token: 0x0400050B RID: 1291
		private string help = string.Empty;

		// Token: 0x0400050C RID: 1292
		private string name;

		// Token: 0x0400050D RID: 1293
		private PerformanceCounterType type;
	}
}
