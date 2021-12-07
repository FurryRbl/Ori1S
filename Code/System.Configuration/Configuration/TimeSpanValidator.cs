using System;

namespace System.Configuration
{
	/// <summary>Provides validation of a <see cref="T:System.TimeSpan" /> object.</summary>
	// Token: 0x0200007E RID: 126
	public class TimeSpanValidator : ConfigurationValidatorBase
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.TimeSpanValidator" /> class, based on supplied parameters.</summary>
		/// <param name="minValue">A <see cref="T:System.TimeSpan" /> object that specifies the minimum time allowed to pass validation.</param>
		/// <param name="maxValue">A <see cref="T:System.TimeSpan" /> object that specifies the maximum time allowed to pass validation.</param>
		// Token: 0x06000425 RID: 1061 RVA: 0x0000BBD8 File Offset: 0x00009DD8
		public TimeSpanValidator(TimeSpan minValue, TimeSpan maxValue) : this(minValue, maxValue, false, 0L)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.TimeSpanValidator" /> class, based on supplied parameters.</summary>
		/// <param name="minValue">A <see cref="T:System.TimeSpan" /> object that specifies the minimum time allowed to pass validation.</param>
		/// <param name="maxValue">A <see cref="T:System.TimeSpan" /> object that specifies the maximum time allowed to pass validation.</param>
		/// <param name="rangeIsExclusive">A <see cref="T:System.Boolean" /> value that specifies whether the validation range is exclusive.</param>
		// Token: 0x06000426 RID: 1062 RVA: 0x0000BBE8 File Offset: 0x00009DE8
		public TimeSpanValidator(TimeSpan minValue, TimeSpan maxValue, bool rangeIsExclusive) : this(minValue, maxValue, rangeIsExclusive, 0L)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.TimeSpanValidator" /> class, based on supplied parameters.</summary>
		/// <param name="minValue">A <see cref="T:System.TimeSpan" /> object that specifies the minimum time allowed to pass validation.</param>
		/// <param name="maxValue">A <see cref="T:System.TimeSpan" /> object that specifies the maximum time allowed to pass validation.</param>
		/// <param name="rangeIsExclusive">A <see cref="T:System.Boolean" /> value that specifies whether the validation range is exclusive.</param>
		/// <param name="resolutionInSeconds">An <see cref="T:System.Int64" /> value that specifies a number of seconds. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="resolutionInSeconds" /> is less than 0.- or -<paramref name="minValue" /> is greater than <paramref name="maxValue" />.</exception>
		// Token: 0x06000427 RID: 1063 RVA: 0x0000BBF8 File Offset: 0x00009DF8
		public TimeSpanValidator(TimeSpan minValue, TimeSpan maxValue, bool rangeIsExclusive, long resolutionInSeconds)
		{
			this.minValue = minValue;
			this.maxValue = maxValue;
			this.rangeIsExclusive = rangeIsExclusive;
			this.resolutionInSeconds = resolutionInSeconds;
		}

		/// <summary>Determines whether the type of the object can be validated.</summary>
		/// <returns>true if the <paramref name="type" /> parameter matches a <see cref="T:System.TimeSpan" /> value; otherwise, false. </returns>
		/// <param name="type"></param>
		// Token: 0x06000428 RID: 1064 RVA: 0x0000BC20 File Offset: 0x00009E20
		public override bool CanValidate(Type type)
		{
			return type == typeof(TimeSpan);
		}

		/// <summary>Determines whether the value of an object is valid.</summary>
		/// <param name="value">The value of an object.</param>
		// Token: 0x06000429 RID: 1065 RVA: 0x0000BC30 File Offset: 0x00009E30
		public override void Validate(object value)
		{
			TimeSpan t = (TimeSpan)value;
			if (!this.rangeIsExclusive)
			{
				if (t < this.minValue || t > this.maxValue)
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
			else if (t >= this.minValue && t <= this.maxValue)
			{
				throw new ArgumentException(string.Concat(new object[]
				{
					"The value must not be in the range ",
					this.minValue,
					" - ",
					this.maxValue
				}));
			}
			if (this.resolutionInSeconds != 0L && t.Ticks % (10000000L * this.resolutionInSeconds) != 0L)
			{
				throw new ArgumentException("The value must have a resolution of " + TimeSpan.FromTicks(10000000L * this.resolutionInSeconds));
			}
		}

		// Token: 0x04000149 RID: 329
		private bool rangeIsExclusive;

		// Token: 0x0400014A RID: 330
		private TimeSpan minValue;

		// Token: 0x0400014B RID: 331
		private TimeSpan maxValue;

		// Token: 0x0400014C RID: 332
		private long resolutionInSeconds;
	}
}
