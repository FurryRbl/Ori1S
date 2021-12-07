using System;

namespace System.Configuration
{
	/// <summary>Declaratively instructs the .NET Framework to perform integer validation on a configuration property. This class cannot be inherited.</summary>
	// Token: 0x0200004E RID: 78
	[AttributeUsage(AttributeTargets.Property)]
	public sealed class IntegerValidatorAttribute : ConfigurationValidatorAttribute
	{
		/// <summary>Gets or sets a value that indicates whether to include or exclude the integers in the range defined by the <see cref="P:System.Configuration.IntegerValidatorAttribute.MinValue" /> and <see cref="P:System.Configuration.IntegerValidatorAttribute.MaxValue" /> property values.</summary>
		/// <returns>true if the value must be excluded; otherwise, false. The default is false.</returns>
		// Token: 0x170000CC RID: 204
		// (get) Token: 0x060002B9 RID: 697 RVA: 0x00008768 File Offset: 0x00006968
		// (set) Token: 0x060002BA RID: 698 RVA: 0x00008770 File Offset: 0x00006970
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
		/// <returns>An integer that indicates the allowed maximum value.</returns>
		/// <exception cref="T:System.ArgumentOutOfRangeException">The selected value is less than <see cref="P:System.Configuration.IntegerValidatorAttribute.MinValue" />.</exception>
		// Token: 0x170000CD RID: 205
		// (get) Token: 0x060002BB RID: 699 RVA: 0x00008780 File Offset: 0x00006980
		// (set) Token: 0x060002BC RID: 700 RVA: 0x00008788 File Offset: 0x00006988
		public int MaxValue
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
		/// <exception cref="T:System.ArgumentOutOfRangeException">The selected value is greater than <see cref="P:System.Configuration.IntegerValidatorAttribute.MaxValue" />.</exception>
		// Token: 0x170000CE RID: 206
		// (get) Token: 0x060002BD RID: 701 RVA: 0x00008798 File Offset: 0x00006998
		// (set) Token: 0x060002BE RID: 702 RVA: 0x000087A0 File Offset: 0x000069A0
		public int MinValue
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

		/// <summary>Gets an instance of the <see cref="T:System.Configuration.IntegerValidator" /> class.</summary>
		/// <returns>The <see cref="T:System.Configuration.ConfigurationValidatorBase" /> validator instance.</returns>
		// Token: 0x170000CF RID: 207
		// (get) Token: 0x060002BF RID: 703 RVA: 0x000087B0 File Offset: 0x000069B0
		public override ConfigurationValidatorBase ValidatorInstance
		{
			get
			{
				if (this.instance == null)
				{
					this.instance = new IntegerValidator(this.minValue, this.maxValue, this.excludeRange);
				}
				return this.instance;
			}
		}

		// Token: 0x040000E0 RID: 224
		private bool excludeRange;

		// Token: 0x040000E1 RID: 225
		private int maxValue;

		// Token: 0x040000E2 RID: 226
		private int minValue;

		// Token: 0x040000E3 RID: 227
		private ConfigurationValidatorBase instance;
	}
}
