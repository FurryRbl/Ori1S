using System;

namespace System.Configuration
{
	/// <summary>Declaratively instructs the .NET Framework to perform long-integer validation on a configuration property. This class cannot be inherited.</summary>
	// Token: 0x02000059 RID: 89
	[AttributeUsage(AttributeTargets.Property)]
	public sealed class LongValidatorAttribute : ConfigurationValidatorAttribute
	{
		/// <summary>Gets or sets a value that indicates whether to include or exclude the integers in the range defined by the <see cref="P:System.Configuration.LongValidatorAttribute.MinValue" /> and <see cref="P:System.Configuration.LongValidatorAttribute.MaxValue" /> property values.</summary>
		/// <returns>true if the value must be excluded; otherwise, false. The default is false.</returns>
		// Token: 0x170000DF RID: 223
		// (get) Token: 0x06000324 RID: 804 RVA: 0x00009274 File Offset: 0x00007474
		// (set) Token: 0x06000325 RID: 805 RVA: 0x0000927C File Offset: 0x0000747C
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

		/// <summary>Gets or sets the maximum value allowed for the property.</summary>
		/// <returns>A long integer that indicates the allowed maximum value.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The selected value is less than <see cref="P:System.Configuration.LongValidatorAttribute.MinValue" />.</exception>
		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x06000326 RID: 806 RVA: 0x0000928C File Offset: 0x0000748C
		// (set) Token: 0x06000327 RID: 807 RVA: 0x00009294 File Offset: 0x00007494
		public long MaxValue
		{
			get
			{
				return this.maxValue;
			}
			set
			{
				this.maxValue = value;
				this.instance = null;
			}
		}

		/// <summary>Gets or sets the minimum value allowed for the property.</summary>
		/// <returns>An integer that indicates the allowed minimum value.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The selected value is greater than <see cref="P:System.Configuration.LongValidatorAttribute.MaxValue" />.</exception>
		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x06000328 RID: 808 RVA: 0x000092A4 File Offset: 0x000074A4
		// (set) Token: 0x06000329 RID: 809 RVA: 0x000092AC File Offset: 0x000074AC
		public long MinValue
		{
			get
			{
				return this.minValue;
			}
			set
			{
				this.minValue = value;
				this.instance = null;
			}
		}

		/// <summary>Gets an instance of the <see cref="T:System.Configuration.LongValidator" /> class.</summary>
		/// <returns>The <see cref="T:System.Configuration.ConfigurationValidatorBase" /> validator instance.</returns>
		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x0600032A RID: 810 RVA: 0x000092BC File Offset: 0x000074BC
		public override ConfigurationValidatorBase ValidatorInstance
		{
			get
			{
				if (this.instance == null)
				{
					this.instance = new LongValidator(this.minValue, this.maxValue, this.excludeRange);
				}
				return this.instance;
			}
		}

		// Token: 0x040000F7 RID: 247
		private bool excludeRange;

		// Token: 0x040000F8 RID: 248
		private long maxValue;

		// Token: 0x040000F9 RID: 249
		private long minValue;

		// Token: 0x040000FA RID: 250
		private ConfigurationValidatorBase instance;
	}
}
