using System;

namespace System.Configuration
{
	/// <summary>Provides validation of an <see cref="T:System.Int64" /> value.</summary>
	// Token: 0x02000058 RID: 88
	public class LongValidator : ConfigurationValidatorBase
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.LongValidator" /> class. </summary>
		/// <param name="minValue">An <see cref="T:System.Int64" /> value that specifies the minimum length of the long value.</param>
		/// <param name="maxValue">An <see cref="T:System.Int64" /> value that specifies the maximum length of the long value.</param>
		/// <param name="rangeIsExclusive">A <see cref="T:System.Boolean" /> value that specifies whether the validation range is exclusive.</param>
		/// <param name="resolution"></param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="resolution" /> is equal to or less than 0.- or -<paramref name="maxValue" /> is less than <paramref name="minValue" />.</exception>
		// Token: 0x0600031E RID: 798 RVA: 0x00009110 File Offset: 0x00007310
		public LongValidator(long minValue, long maxValue, bool rangeIsExclusive, long resolution)
		{
			this.minValue = minValue;
			this.maxValue = maxValue;
			this.rangeIsExclusive = rangeIsExclusive;
			this.resolution = resolution;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.LongValidator" /> class. </summary>
		/// <param name="minValue">An <see cref="T:System.Int64" /> value that specifies the minimum length of the long value.</param>
		/// <param name="maxValue">An <see cref="T:System.Int64" /> value that specifies the maximum length of the long value.</param>
		/// <param name="rangeIsExclusive">A <see cref="T:System.Boolean" /> value that specifies whether the validation range is exclusive.</param>
		// Token: 0x0600031F RID: 799 RVA: 0x00009138 File Offset: 0x00007338
		public LongValidator(long minValue, long maxValue, bool rangeIsExclusive) : this(minValue, maxValue, rangeIsExclusive, 0L)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.LongValidator" /> class. </summary>
		/// <param name="minValue">An <see cref="T:System.Int64" /> value that specifies the minimum length of the long value.</param>
		/// <param name="maxValue">An <see cref="T:System.Int64" /> value that specifies the maximum length of the long value.</param>
		// Token: 0x06000320 RID: 800 RVA: 0x00009148 File Offset: 0x00007348
		public LongValidator(long minValue, long maxValue) : this(minValue, maxValue, false, 0L)
		{
		}

		/// <summary>Determines whether the type of the object can be validated.</summary>
		/// <returns>true if the <paramref name="type" /> parameter matches an <see cref="T:System.Int64" /> value; otherwise, false. </returns>
		/// <param name="type">The type of object.</param>
		// Token: 0x06000321 RID: 801 RVA: 0x00009158 File Offset: 0x00007358
		public override bool CanValidate(Type type)
		{
			return type == typeof(long);
		}

		/// <summary>Determines whether the value of an object is valid.</summary>
		/// <param name="value">The value of an object.</param>
		// Token: 0x06000322 RID: 802 RVA: 0x00009168 File Offset: 0x00007368
		public override void Validate(object value)
		{
			long num = (long)value;
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
			if (this.resolution != 0L && num % this.resolution != 0L)
			{
				throw new ArgumentException("The value must have a resolution of " + this.resolution);
			}
		}

		// Token: 0x040000F3 RID: 243
		private bool rangeIsExclusive;

		// Token: 0x040000F4 RID: 244
		private long minValue;

		// Token: 0x040000F5 RID: 245
		private long maxValue;

		// Token: 0x040000F6 RID: 246
		private long resolution;
	}
}
