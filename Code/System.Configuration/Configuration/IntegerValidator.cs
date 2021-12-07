using System;

namespace System.Configuration
{
	/// <summary>Provides validation of an <see cref="T:System.Int32" /> value.</summary>
	// Token: 0x0200004D RID: 77
	public class IntegerValidator : ConfigurationValidatorBase
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.IntegerValidator" /> class. </summary>
		/// <param name="minValue">An <see cref="T:System.Int32" /> object that specifies the minimum length of the integer value.</param>
		/// <param name="maxValue">An <see cref="T:System.Int32" /> object that specifies the maximum length of the integer value.</param>
		/// <param name="rangeIsExclusive">A <see cref="T:System.Boolean" /> value that specifies whether the validation range is exclusive.</param>
		/// <param name="resolution">An <see cref="T:System.Int32" /> object that specifies a value that must be matched.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="resolution" /> is less than 0.- or -<paramref name="minValue" /> is greater than <paramref name="maxValue" />.</exception>
		// Token: 0x060002B3 RID: 691 RVA: 0x0000860C File Offset: 0x0000680C
		public IntegerValidator(int minValue, int maxValue, bool rangeIsExclusive, int resolution)
		{
			this.minValue = minValue;
			this.maxValue = maxValue;
			this.rangeIsExclusive = rangeIsExclusive;
			this.resolution = resolution;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.IntegerValidator" /> class. </summary>
		/// <param name="minValue">An <see cref="T:System.Int32" /> object that specifies the minimum value.</param>
		/// <param name="maxValue">An <see cref="T:System.Int32" /> object that specifies the maximum value.</param>
		/// <param name="rangeIsExclusive">true to specify that the validation range is exclusive. Inclusive means the value to be validated must be within the specified range; exclusive means that it must be below the minimum or above the maximum.</param>
		// Token: 0x060002B4 RID: 692 RVA: 0x00008634 File Offset: 0x00006834
		public IntegerValidator(int minValue, int maxValue, bool rangeIsExclusive) : this(minValue, maxValue, rangeIsExclusive, 0)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.IntegerValidator" /> class. </summary>
		/// <param name="minValue">An <see cref="T:System.Int32" /> object that specifies the minimum value.</param>
		/// <param name="maxValue">An <see cref="T:System.Int32" /> object that specifies the maximum value.</param>
		// Token: 0x060002B5 RID: 693 RVA: 0x00008640 File Offset: 0x00006840
		public IntegerValidator(int minValue, int maxValue) : this(minValue, maxValue, false, 0)
		{
		}

		/// <summary>Determines whether the type of the object can be validated.</summary>
		/// <returns>true if the <paramref name="type" /> parameter matches an <see cref="T:System.Int32" /> value; otherwise, false. </returns>
		/// <param name="type">The type of the object.</param>
		// Token: 0x060002B6 RID: 694 RVA: 0x0000864C File Offset: 0x0000684C
		public override bool CanValidate(Type type)
		{
			return type == typeof(int);
		}

		/// <summary>Determines whether the value of an object is valid.</summary>
		/// <param name="value">The value to be validated.</param>
		// Token: 0x060002B7 RID: 695 RVA: 0x0000865C File Offset: 0x0000685C
		public override void Validate(object value)
		{
			int num = (int)value;
			if (!this.rangeIsExclusive)
			{
				if (num < this.minValue || num > this.maxValue)
				{
					throw new ArgumentException(string.Concat(new object[]
					{
						"The value must be in the range ",
						this.minValue,
						" - ",
						this.maxValue
					}));
				}
			}
			else if (num >= this.minValue && num <= this.maxValue)
			{
				throw new ArgumentException(string.Concat(new object[]
				{
					"The value must not be in the range ",
					this.minValue,
					" - ",
					this.maxValue
				}));
			}
			if (this.resolution != 0 && num % this.resolution != 0)
			{
				throw new ArgumentException("The value must have a resolution of " + this.resolution);
			}
		}

		// Token: 0x040000DC RID: 220
		private bool rangeIsExclusive;

		// Token: 0x040000DD RID: 221
		private int minValue;

		// Token: 0x040000DE RID: 222
		private int maxValue;

		// Token: 0x040000DF RID: 223
		private int resolution;
	}
}
