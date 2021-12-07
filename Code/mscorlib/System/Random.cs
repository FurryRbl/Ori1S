using System;
using System.Runtime.InteropServices;

namespace System
{
	/// <summary>Represents a pseudo-random number generator, a device that produces a sequence of numbers that meet certain statistical requirements for randomness.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x0200016C RID: 364
	[ComVisible(true)]
	[Serializable]
	public class Random
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Random" /> class, using a time-dependent default seed value.</summary>
		/// <exception cref="T:System.OverflowException">The seed value derived from the system clock is <see cref="F:System.Int32.MinValue" />, which causes an overflow when its absolute value is calculated. </exception>
		// Token: 0x06001359 RID: 4953 RVA: 0x0004D698 File Offset: 0x0004B898
		public Random() : this(Environment.TickCount)
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Random" /> class, using the specified seed value.</summary>
		/// <param name="Seed">A number used to calculate a starting value for the pseudo-random number sequence. If a negative number is specified, the absolute value of the number is used. </param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="Seed" /> is <see cref="F:System.Int32.MinValue" />, which causes an overflow when its absolute value is calculated. </exception>
		// Token: 0x0600135A RID: 4954 RVA: 0x0004D6A8 File Offset: 0x0004B8A8
		public Random(int Seed)
		{
			int num = 161803398 - Math.Abs(Seed);
			this.SeedArray[55] = num;
			int num2 = 1;
			for (int i = 1; i < 55; i++)
			{
				int num3 = 21 * i % 55;
				this.SeedArray[num3] = num2;
				num2 = num - num2;
				if (num2 < 0)
				{
					num2 += int.MaxValue;
				}
				num = this.SeedArray[num3];
			}
			for (int j = 1; j < 5; j++)
			{
				for (int k = 1; k < 56; k++)
				{
					this.SeedArray[k] -= this.SeedArray[1 + (k + 30) % 55];
					if (this.SeedArray[k] < 0)
					{
						this.SeedArray[k] += int.MaxValue;
					}
				}
			}
			this.inext = 0;
			this.inextp = 31;
		}

		/// <summary>Returns a random number between 0.0 and 1.0.</summary>
		/// <returns>A double-precision floating point number greater than or equal to 0.0, and less than 1.0.</returns>
		// Token: 0x0600135B RID: 4955 RVA: 0x0004D7A4 File Offset: 0x0004B9A4
		protected virtual double Sample()
		{
			if (++this.inext >= 56)
			{
				this.inext = 1;
			}
			if (++this.inextp >= 56)
			{
				this.inextp = 1;
			}
			int num = this.SeedArray[this.inext] - this.SeedArray[this.inextp];
			if (num < 0)
			{
				num += int.MaxValue;
			}
			this.SeedArray[this.inext] = num;
			return (double)num * 4.656612875245797E-10;
		}

		/// <summary>Returns a nonnegative random number.</summary>
		/// <returns>A 32-bit signed integer greater than or equal to zero and less than <see cref="F:System.Int32.MaxValue" />.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600135C RID: 4956 RVA: 0x0004D834 File Offset: 0x0004BA34
		public virtual int Next()
		{
			return (int)(this.Sample() * 2147483647.0);
		}

		/// <summary>Returns a nonnegative random number less than the specified maximum.</summary>
		/// <returns>A 32-bit signed integer greater than or equal to zero, and less than <paramref name="maxValue" />; that is, the range of return values ordinarily includes zero but not <paramref name="maxValue" />. However, if <paramref name="maxValue" /> equals zero, <paramref name="maxValue" /> is returned.</returns>
		/// <param name="maxValue">The exclusive upper bound of the random number to be generated. <paramref name="maxValue" /> must be greater than or equal to zero. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="maxValue" /> is less than zero. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600135D RID: 4957 RVA: 0x0004D848 File Offset: 0x0004BA48
		public virtual int Next(int maxValue)
		{
			if (maxValue < 0)
			{
				throw new ArgumentOutOfRangeException(Locale.GetText("Max value is less than min value."));
			}
			return (int)(this.Sample() * (double)maxValue);
		}

		/// <summary>Returns a random number within a specified range.</summary>
		/// <returns>A 32-bit signed integer greater than or equal to <paramref name="minValue" /> and less than <paramref name="maxValue" />; that is, the range of return values includes <paramref name="minValue" /> but not <paramref name="maxValue" />. If <paramref name="minValue" /> equals <paramref name="maxValue" />, <paramref name="minValue" /> is returned.</returns>
		/// <param name="minValue">The inclusive lower bound of the random number returned. </param>
		/// <param name="maxValue">The exclusive upper bound of the random number returned. <paramref name="maxValue" /> must be greater than or equal to <paramref name="minValue" />. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="minValue" /> is greater than <paramref name="maxValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600135E RID: 4958 RVA: 0x0004D86C File Offset: 0x0004BA6C
		public virtual int Next(int minValue, int maxValue)
		{
			if (minValue > maxValue)
			{
				throw new ArgumentOutOfRangeException(Locale.GetText("Min value is greater than max value."));
			}
			uint num = (uint)(maxValue - minValue);
			if (num <= 1U)
			{
				return minValue;
			}
			return (int)((ulong)((uint)(this.Sample() * num)) + (ulong)((long)minValue));
		}

		/// <summary>Fills the elements of a specified array of bytes with random numbers.</summary>
		/// <param name="buffer">An array of bytes to contain random numbers. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="buffer" /> is null. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600135F RID: 4959 RVA: 0x0004D8B0 File Offset: 0x0004BAB0
		public virtual void NextBytes(byte[] buffer)
		{
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			for (int i = 0; i < buffer.Length; i++)
			{
				buffer[i] = (byte)(this.Sample() * 256.0);
			}
		}

		/// <summary>Returns a random number between 0.0 and 1.0.</summary>
		/// <returns>A double-precision floating point number greater than or equal to 0.0, and less than 1.0.</returns>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001360 RID: 4960 RVA: 0x0004D8F8 File Offset: 0x0004BAF8
		public virtual double NextDouble()
		{
			return this.Sample();
		}

		// Token: 0x0400059E RID: 1438
		private const int MBIG = 2147483647;

		// Token: 0x0400059F RID: 1439
		private const int MSEED = 161803398;

		// Token: 0x040005A0 RID: 1440
		private const int MZ = 0;

		// Token: 0x040005A1 RID: 1441
		private int inext;

		// Token: 0x040005A2 RID: 1442
		private int inextp;

		// Token: 0x040005A3 RID: 1443
		private int[] SeedArray = new int[56];
	}
}
