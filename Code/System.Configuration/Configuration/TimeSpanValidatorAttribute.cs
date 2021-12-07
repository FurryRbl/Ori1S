using System;

namespace System.Configuration
{
	/// <summary>Declaratively instructs the .NET Framework to perform time validation on a configuration property. This class cannot be inherited.</summary>
	// Token: 0x0200007F RID: 127
	[AttributeUsage(AttributeTargets.Property)]
	public sealed class TimeSpanValidatorAttribute : ConfigurationValidatorAttribute
	{
		/// <summary>Gets or sets the relative maximum <see cref="T:System.TimeSpan" /> value.</summary>
		/// <returns>The allowed maximum <see cref="T:System.TimeSpan" /> value. </returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The selected value represents less than <see cref="P:System.Configuration.TimeSpanValidatorAttribute.MinValue" />.</exception>
		// Token: 0x17000131 RID: 305
		// (get) Token: 0x0600042B RID: 1067 RVA: 0x0000BD80 File Offset: 0x00009F80
		// (set) Token: 0x0600042C RID: 1068 RVA: 0x0000BD88 File Offset: 0x00009F88
		public string MaxValueString
		{
			get
			{
				return this.maxValueString;
			}
			set
			{
				this.maxValueString = value;
				this.instance = null;
			}
		}

		/// <summary>Gets or sets the relative minimum <see cref="T:System.TimeSpan" /> value.</summary>
		/// <returns>The minimum allowed <see cref="T:System.TimeSpan" /> value. </returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The selected value represents more than <see cref="P:System.Configuration.TimeSpanValidatorAttribute.MaxValue" />.</exception>
		// Token: 0x17000132 RID: 306
		// (get) Token: 0x0600042D RID: 1069 RVA: 0x0000BD98 File Offset: 0x00009F98
		// (set) Token: 0x0600042E RID: 1070 RVA: 0x0000BDA0 File Offset: 0x00009FA0
		public string MinValueString
		{
			get
			{
				return this.minValueString;
			}
			set
			{
				this.minValueString = value;
				this.instance = null;
			}
		}

		/// <summary>Gets the absolute maximum <see cref="T:System.TimeSpan" /> value.</summary>
		/// <returns>The allowed maximum <see cref="T:System.TimeSpan" /> value. </returns>
		// Token: 0x17000133 RID: 307
		// (get) Token: 0x0600042F RID: 1071 RVA: 0x0000BDB0 File Offset: 0x00009FB0
		public TimeSpan MaxValue
		{
			get
			{
				return TimeSpan.Parse(this.maxValueString);
			}
		}

		/// <summary>Gets the absolute minimum <see cref="T:System.TimeSpan" /> value.</summary>
		/// <returns>The allowed minimum <see cref="T:System.TimeSpan" /> value. </returns>
		// Token: 0x17000134 RID: 308
		// (get) Token: 0x06000430 RID: 1072 RVA: 0x0000BDC0 File Offset: 0x00009FC0
		public TimeSpan MinValue
		{
			get
			{
				return TimeSpan.Parse(this.minValueString);
			}
		}

		/// <summary>Gets or sets a value that indicates whether to include or exclude the integers in the range as defined by <see cref="P:System.Configuration.TimeSpanValidatorAttribute.MinValueString" /> and <see cref="P:System.Configuration.TimeSpanValidatorAttribute.MaxValueString" />.</summary>
		/// <returns>true if the value must be excluded; otherwise, false. The default is false.</returns>
		// Token: 0x17000135 RID: 309
		// (get) Token: 0x06000431 RID: 1073 RVA: 0x0000BDD0 File Offset: 0x00009FD0
		// (set) Token: 0x06000432 RID: 1074 RVA: 0x0000BDD8 File Offset: 0x00009FD8
		public bool ExcludeRange
		{
			get
			{
				return this.excludeRange;
			}
			set
			{
				this.excludeRange = value;
				this.instance = null;
			}
		}

		/// <summary>Gets an instance of the <see cref="T:System.Configuration.TimeSpanValidator" /> class.</summary>
		/// <returns>The <see cref="T:System.Configuration.ConfigurationValidatorBase" /> validator instance. </returns>
		// Token: 0x17000136 RID: 310
		// (get) Token: 0x06000433 RID: 1075 RVA: 0x0000BDE8 File Offset: 0x00009FE8
		public override ConfigurationValidatorBase ValidatorInstance
		{
			get
			{
				if (this.instance == null)
				{
					this.instance = new TimeSpanValidator(this.MinValue, this.MaxValue, this.excludeRange);
				}
				return this.instance;
			}
		}

		/// <summary>Gets the absolute maximum value allowed.</summary>
		// Token: 0x0400014D RID: 333
		public const string TimeSpanMaxValue = "10675199.02:48:05.4775807";

		/// <summary>Gets the absolute minimum value allowed.</summary>
		// Token: 0x0400014E RID: 334
		public const string TimeSpanMinValue = "-10675199.02:48:05.4775808";

		// Token: 0x0400014F RID: 335
		private bool excludeRange;

		// Token: 0x04000150 RID: 336
		private string maxValueString = "10675199.02:48:05.4775807";

		// Token: 0x04000151 RID: 337
		private string minValueString = "-10675199.02:48:05.4775808";

		// Token: 0x04000152 RID: 338
		private ConfigurationValidatorBase instance;
	}
}
