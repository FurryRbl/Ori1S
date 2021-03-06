using System;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;

namespace System
{
	/// <summary>Provides constants and static methods for trigonometric, logarithmic, and other common mathematical functions.</summary>
	/// <filterpriority>1</filterpriority>
	// Token: 0x0200014D RID: 333
	public static class Math
	{
		/// <summary>Returns the absolute value of a <see cref="T:System.Decimal" /> number.</summary>
		/// <returns>A <see cref="T:System.Decimal" />, x, such that 0 ≤ x ≤<see cref="F:System.Decimal.MaxValue" />.</returns>
		/// <param name="value">A number that is greater than or equal to <see cref="F:System.Decimal.MinValue" />, but less than or equal to <see cref="F:System.Decimal.MaxValue" />. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060011D0 RID: 4560 RVA: 0x000473CC File Offset: 0x000455CC
		public static decimal Abs(decimal value)
		{
			return (!(value < 0m)) ? value : (-value);
		}

		/// <summary>Returns the absolute value of a double-precision floating-point number.</summary>
		/// <returns>A double-precision floating-point number, x, such that 0 ≤ x ≤<see cref="F:System.Double.MaxValue" />.</returns>
		/// <param name="value">A number that is greater than or equal to <see cref="F:System.Double.MinValue" />, but less than or equal to <see cref="F:System.Double.MaxValue" />.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060011D1 RID: 4561 RVA: 0x000473EC File Offset: 0x000455EC
		public static double Abs(double value)
		{
			return (value >= 0.0) ? value : (-value);
		}

		/// <summary>Returns the absolute value of a single-precision floating-point number.</summary>
		/// <returns>A single-precision floating-point number, x, such that 0 ≤ x ≤<see cref="F:System.Single.MaxValue" />.</returns>
		/// <param name="value">A number that is greater than or equal to <see cref="F:System.Single.MinValue" />, but less than or equal to <see cref="F:System.Single.MaxValue" />.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060011D2 RID: 4562 RVA: 0x00047408 File Offset: 0x00045608
		public static float Abs(float value)
		{
			return (value >= 0f) ? value : (-value);
		}

		/// <summary>Returns the absolute value of a 32-bit signed integer.</summary>
		/// <returns>A 32-bit signed integer, x, such that 0 ≤ x ≤<see cref="F:System.Int32.MaxValue" />.</returns>
		/// <param name="value">A number that is greater than <see cref="F:System.Int32.MinValue" />, but less than or equal to <see cref="F:System.Int32.MaxValue" />.</param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> equals <see cref="F:System.Int32.MinValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060011D3 RID: 4563 RVA: 0x00047420 File Offset: 0x00045620
		public static int Abs(int value)
		{
			if (value == -2147483648)
			{
				throw new OverflowException(Locale.GetText("Value is too small."));
			}
			return (value >= 0) ? value : (-value);
		}

		/// <summary>Returns the absolute value of a 64-bit signed integer.</summary>
		/// <returns>A 64-bit signed integer, x, such that 0 ≤ x ≤<see cref="F:System.Int64.MaxValue" />.</returns>
		/// <param name="value">A number that is greater than <see cref="F:System.Int64.MinValue" />, but less than or equal to <see cref="F:System.Int64.MaxValue" />.</param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> equals <see cref="F:System.Int64.MinValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060011D4 RID: 4564 RVA: 0x00047458 File Offset: 0x00045658
		public static long Abs(long value)
		{
			if (value == -9223372036854775808L)
			{
				throw new OverflowException(Locale.GetText("Value is too small."));
			}
			return (value >= 0L) ? value : (-value);
		}

		/// <summary>Returns the absolute value of an 8-bit signed integer.</summary>
		/// <returns>An 8-bit signed integer, x, such that 0 ≤ x ≤<see cref="F:System.SByte.MaxValue" />.</returns>
		/// <param name="value">A number that is greater than <see cref="F:System.SByte.MinValue" />, but less than or equal to <see cref="F:System.SByte.MaxValue" />.</param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> equals <see cref="F:System.SByte.MinValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060011D5 RID: 4565 RVA: 0x0004748C File Offset: 0x0004568C
		[CLSCompliant(false)]
		public static sbyte Abs(sbyte value)
		{
			if ((int)value == -128)
			{
				throw new OverflowException(Locale.GetText("Value is too small."));
			}
			return (sbyte)(((int)value >= 0) ? ((int)value) : (-(sbyte)((int)value)));
		}

		/// <summary>Returns the absolute value of a 16-bit signed integer.</summary>
		/// <returns>A 16-bit signed integer, x, such that 0 ≤ x ≤<see cref="F:System.Int16.MaxValue" />.</returns>
		/// <param name="value">A number that is greater than <see cref="F:System.Int16.MinValue" />, but less than or equal to <see cref="F:System.Int16.MaxValue" />.</param>
		/// <exception cref="T:System.OverflowException">
		///   <paramref name="value" /> equals <see cref="F:System.Int16.MinValue" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060011D6 RID: 4566 RVA: 0x000474C8 File Offset: 0x000456C8
		public static short Abs(short value)
		{
			if (value == -32768)
			{
				throw new OverflowException(Locale.GetText("Value is too small."));
			}
			return (value >= 0) ? value : (-value);
		}

		/// <summary>Returns the smallest integral value that is greater than or equal to the specified decimal number.</summary>
		/// <returns>The smallest integer that is greater than or equal to <paramref name="d" />. Note that the method returns a <see cref="T:System.Decimal" /> type instead of an integral type.</returns>
		/// <param name="d">A decimal number. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060011D7 RID: 4567 RVA: 0x000474F8 File Offset: 0x000456F8
		public static decimal Ceiling(decimal d)
		{
			decimal num = Math.Floor(d);
			if (num != d)
			{
				num = ++num;
			}
			return num;
		}

		/// <summary>Returns the smallest integral value that is greater than or equal to the specified double-precision floating-point number.</summary>
		/// <returns>The smallest integral value that is greater than or equal to <paramref name="a" />. If <paramref name="a" /> is equal to <see cref="F:System.Double.NaN" />, <see cref="F:System.Double.NegativeInfinity" />, or <see cref="F:System.Double.PositiveInfinity" />, that value is returned. Note that this method returns a <see cref="T:System.Double" /> type instead of an integral type.</returns>
		/// <param name="a">A double-precision floating-point number. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060011D8 RID: 4568 RVA: 0x00047520 File Offset: 0x00045720
		public static double Ceiling(double a)
		{
			double num = Math.Floor(a);
			if (num != a)
			{
				num += 1.0;
			}
			return num;
		}

		/// <summary>Produces the full product of two 32-bit numbers.</summary>
		/// <returns>The <see cref="T:System.Int64" /> containing the product of the specified numbers.</returns>
		/// <param name="a">The first <see cref="T:System.Int32" /> to multiply. </param>
		/// <param name="b">The second <see cref="T:System.Int32" /> to multiply. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060011D9 RID: 4569 RVA: 0x00047548 File Offset: 0x00045748
		public static long BigMul(int a, int b)
		{
			return (long)a * (long)b;
		}

		/// <summary>Calculates the quotient of two 32-bit signed integers and also returns the remainder in an output parameter.</summary>
		/// <returns>The quotient of the specified numbers.</returns>
		/// <param name="a">The dividend. </param>
		/// <param name="b">The divisor. </param>
		/// <param name="result">The remainder. </param>
		/// <exception cref="T:System.DivideByZeroException">
		///   <paramref name="b" /> is zero.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060011DA RID: 4570 RVA: 0x00047550 File Offset: 0x00045750
		public static int DivRem(int a, int b, out int result)
		{
			result = a % b;
			return a / b;
		}

		/// <summary>Calculates the quotient of two 64-bit signed integers and also returns the remainder in an output parameter.</summary>
		/// <returns>The quotient of the specified numbers.</returns>
		/// <param name="a">The dividend. </param>
		/// <param name="b">The divisor. </param>
		/// <param name="result">The remainder. </param>
		/// <exception cref="T:System.DivideByZeroException">
		///   <paramref name="b" /> is zero.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060011DB RID: 4571 RVA: 0x0004755C File Offset: 0x0004575C
		public static long DivRem(long a, long b, out long result)
		{
			result = a % b;
			return a / b;
		}

		/// <summary>Returns the largest integer less than or equal to the specified double-precision floating-point number.</summary>
		/// <returns>The largest integer less than or equal to <paramref name="d" />. If <paramref name="d" /> is equal to <see cref="F:System.Double.NaN" />, <see cref="F:System.Double.NegativeInfinity" />, or <see cref="F:System.Double.PositiveInfinity" />, that value is returned.</returns>
		/// <param name="d">A double-precision floating-point number. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060011DC RID: 4572
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern double Floor(double d);

		/// <summary>Returns the remainder resulting from the division of a specified number by another specified number.</summary>
		/// <returns>A number equal to <paramref name="x" /> - (<paramref name="y" /> Q), where Q is the quotient of <paramref name="x" /> / <paramref name="y" /> rounded to the nearest integer (if <paramref name="x" /> / <paramref name="y" /> falls halfway between two integers, the even integer is returned).If <paramref name="x" /> - (<paramref name="y" /> Q) is zero, the value +0 is returned if <paramref name="x" /> is positive, or -0 if <paramref name="x" /> is negative.If <paramref name="y" /> = 0, <see cref="F:System.Double.NaN" /> is returned.</returns>
		/// <param name="x">A dividend. </param>
		/// <param name="y">A divisor. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060011DD RID: 4573 RVA: 0x00047568 File Offset: 0x00045768
		public static double IEEERemainder(double x, double y)
		{
			if (y == 0.0)
			{
				return double.NaN;
			}
			double num = x - y * Math.Round(x / y);
			if (num != 0.0)
			{
				return num;
			}
			return (x <= 0.0) ? BitConverter.Int64BitsToDouble(long.MinValue) : 0.0;
		}

		/// <summary>Returns the logarithm of a specified number in a specified base.</summary>
		/// <returns>In the following table +Infinity denotes <see cref="F:System.Double.PositiveInfinity" />, -Infinity denotes <see cref="F:System.Double.NegativeInfinity" />, and NaN denotes <see cref="F:System.Double.NaN" />.<paramref name="a" /><paramref name="newBase" />Return Value<paramref name="a" />&gt; 0(0 &lt;<paramref name="newBase" />&lt; 1) -or-(<paramref name="newBase" />&gt; 1)lognewBase(a)<paramref name="a" />&lt; 0(any value)NaN(any value)<paramref name="newBase" />&lt; 0NaN<paramref name="a" /> != 1<paramref name="newBase" /> = 0NaN<paramref name="a" /> != 1<paramref name="newBase" /> = +InfinityNaN<paramref name="a" /> = NaN(any value)NaN(any value)<paramref name="newBase" /> = NaNNaN(any value)<paramref name="newBase" /> = 1NaN<paramref name="a" /> = 00 &lt;<paramref name="newBase" />&lt; 1 +Infinity<paramref name="a" /> = 0<paramref name="newBase" />&gt; 1-Infinity<paramref name="a" /> =  +Infinity0 &lt;<paramref name="newBase" />&lt; 1-Infinity<paramref name="a" /> =  +Infinity<paramref name="newBase" />&gt; 1+Infinity<paramref name="a" /> = 1<paramref name="newBase" /> = 00<paramref name="a" /> = 1<paramref name="newBase" /> = +Infinity0</returns>
		/// <param name="a">A number whose logarithm is to be found. </param>
		/// <param name="newBase">The base of the logarithm. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060011DE RID: 4574 RVA: 0x000475D8 File Offset: 0x000457D8
		public static double Log(double a, double newBase)
		{
			double num = Math.Log(a) / Math.Log(newBase);
			return (num != 0.0) ? num : 0.0;
		}

		/// <summary>Returns the larger of two 8-bit unsigned integers.</summary>
		/// <returns>Parameter <paramref name="val1" /> or <paramref name="val2" />, whichever is larger.</returns>
		/// <param name="val1">The first of two 8-bit unsigned integers to compare. </param>
		/// <param name="val2">The second of two 8-bit unsigned integers to compare. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060011DF RID: 4575 RVA: 0x00047614 File Offset: 0x00045814
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public static byte Max(byte val1, byte val2)
		{
			return (val1 <= val2) ? val2 : val1;
		}

		/// <summary>Returns the larger of two decimal numbers.</summary>
		/// <returns>Parameter <paramref name="val1" /> or <paramref name="val2" />, whichever is larger.</returns>
		/// <param name="val1">The first of two <see cref="T:System.Decimal" /> numbers to compare. </param>
		/// <param name="val2">The second of two <see cref="T:System.Decimal" /> numbers to compare. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060011E0 RID: 4576 RVA: 0x00047624 File Offset: 0x00045824
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public static decimal Max(decimal val1, decimal val2)
		{
			return (!(val1 > val2)) ? val2 : val1;
		}

		/// <summary>Returns the larger of two double-precision floating-point numbers.</summary>
		/// <returns>Parameter <paramref name="val1" /> or <paramref name="val2" />, whichever is larger. If <paramref name="val1" />, <paramref name="val2" />, or both <paramref name="val1" /> and <paramref name="val2" /> are equal to <see cref="F:System.Double.NaN" />, <see cref="F:System.Double.NaN" /> is returned.</returns>
		/// <param name="val1">The first of two double-precision floating-point numbers to compare. </param>
		/// <param name="val2">The second of two double-precision floating-point numbers to compare. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060011E1 RID: 4577 RVA: 0x0004763C File Offset: 0x0004583C
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public static double Max(double val1, double val2)
		{
			if (double.IsNaN(val1) || double.IsNaN(val2))
			{
				return double.NaN;
			}
			return (val1 <= val2) ? val2 : val1;
		}

		/// <summary>Returns the larger of two single-precision floating-point numbers.</summary>
		/// <returns>Parameter <paramref name="val1" /> or <paramref name="val2" />, whichever is larger. If <paramref name="val1" />, or <paramref name="val2" />, or both <paramref name="val1" /> and <paramref name="val2" /> are equal to <see cref="F:System.Single.NaN" />, <see cref="F:System.Single.NaN" /> is returned.</returns>
		/// <param name="val1">The first of two single-precision floating-point numbers to compare. </param>
		/// <param name="val2">The second of two single-precision floating-point numbers to compare. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060011E2 RID: 4578 RVA: 0x00047678 File Offset: 0x00045878
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public static float Max(float val1, float val2)
		{
			if (float.IsNaN(val1) || float.IsNaN(val2))
			{
				return float.NaN;
			}
			return (val1 <= val2) ? val2 : val1;
		}

		/// <summary>Returns the larger of two 32-bit signed integers.</summary>
		/// <returns>Parameter <paramref name="val1" /> or <paramref name="val2" />, whichever is larger.</returns>
		/// <param name="val1">The first of two 32-bit signed integers to compare. </param>
		/// <param name="val2">The second of two 32-bit signed integers to compare. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060011E3 RID: 4579 RVA: 0x000476B0 File Offset: 0x000458B0
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public static int Max(int val1, int val2)
		{
			return (val1 <= val2) ? val2 : val1;
		}

		/// <summary>Returns the larger of two 64-bit signed integers.</summary>
		/// <returns>Parameter <paramref name="val1" /> or <paramref name="val2" />, whichever is larger.</returns>
		/// <param name="val1">The first of two 64-bit signed integers to compare. </param>
		/// <param name="val2">The second of two 64-bit signed integers to compare. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060011E4 RID: 4580 RVA: 0x000476C0 File Offset: 0x000458C0
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public static long Max(long val1, long val2)
		{
			return (val1 <= val2) ? val2 : val1;
		}

		/// <summary>Returns the larger of two 8-bit signed integers.</summary>
		/// <returns>Parameter <paramref name="val1" /> or <paramref name="val2" />, whichever is larger.</returns>
		/// <param name="val1">The first of two 8-bit signed integers to compare. </param>
		/// <param name="val2">The second of two 8-bit signed integers to compare. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060011E5 RID: 4581 RVA: 0x000476D0 File Offset: 0x000458D0
		[CLSCompliant(false)]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public static sbyte Max(sbyte val1, sbyte val2)
		{
			return ((int)val1 <= (int)val2) ? val2 : val1;
		}

		/// <summary>Returns the larger of two 16-bit signed integers.</summary>
		/// <returns>Parameter <paramref name="val1" /> or <paramref name="val2" />, whichever is larger.</returns>
		/// <param name="val1">The first of two 16-bit signed integers to compare. </param>
		/// <param name="val2">The second of two 16-bit signed integers to compare. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060011E6 RID: 4582 RVA: 0x000476E4 File Offset: 0x000458E4
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public static short Max(short val1, short val2)
		{
			return (val1 <= val2) ? val2 : val1;
		}

		/// <summary>Returns the larger of two 32-bit unsigned integers.</summary>
		/// <returns>Parameter <paramref name="val1" /> or <paramref name="val2" />, whichever is larger.</returns>
		/// <param name="val1">The first of two 32-bit unsigned integers to compare. </param>
		/// <param name="val2">The second of two 32-bit unsigned integers to compare. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060011E7 RID: 4583 RVA: 0x000476F4 File Offset: 0x000458F4
		[CLSCompliant(false)]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public static uint Max(uint val1, uint val2)
		{
			return (val1 <= val2) ? val2 : val1;
		}

		/// <summary>Returns the larger of two 64-bit unsigned integers.</summary>
		/// <returns>Parameter <paramref name="val1" /> or <paramref name="val2" />, whichever is larger.</returns>
		/// <param name="val1">The first of two 64-bit unsigned integers to compare. </param>
		/// <param name="val2">The second of two 64-bit unsigned integers to compare. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060011E8 RID: 4584 RVA: 0x00047704 File Offset: 0x00045904
		[CLSCompliant(false)]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public static ulong Max(ulong val1, ulong val2)
		{
			return (val1 <= val2) ? val2 : val1;
		}

		/// <summary>Returns the larger of two 16-bit unsigned integers.</summary>
		/// <returns>Parameter <paramref name="val1" /> or <paramref name="val2" />, whichever is larger.</returns>
		/// <param name="val1">The first of two 16-bit unsigned integers to compare. </param>
		/// <param name="val2">The second of two 16-bit unsigned integers to compare. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060011E9 RID: 4585 RVA: 0x00047714 File Offset: 0x00045914
		[CLSCompliant(false)]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public static ushort Max(ushort val1, ushort val2)
		{
			return (val1 <= val2) ? val2 : val1;
		}

		/// <summary>Returns the smaller of two 8-bit unsigned integers.</summary>
		/// <returns>Parameter <paramref name="val1" /> or <paramref name="val2" />, whichever is smaller.</returns>
		/// <param name="val1">The first of two 8-bit unsigned integers to compare. </param>
		/// <param name="val2">The second of two 8-bit unsigned integers to compare. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060011EA RID: 4586 RVA: 0x00047724 File Offset: 0x00045924
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public static byte Min(byte val1, byte val2)
		{
			return (val1 >= val2) ? val2 : val1;
		}

		/// <summary>Returns the smaller of two decimal numbers.</summary>
		/// <returns>Parameter <paramref name="val1" /> or <paramref name="val2" />, whichever is smaller.</returns>
		/// <param name="val1">The first of two <see cref="T:System.Decimal" /> numbers to compare. </param>
		/// <param name="val2">The second of two <see cref="T:System.Decimal" /> numbers to compare. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060011EB RID: 4587 RVA: 0x00047734 File Offset: 0x00045934
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public static decimal Min(decimal val1, decimal val2)
		{
			return (!(val1 < val2)) ? val2 : val1;
		}

		/// <summary>Returns the smaller of two double-precision floating-point numbers.</summary>
		/// <returns>Parameter <paramref name="val1" /> or <paramref name="val2" />, whichever is smaller. If <paramref name="val1" />, <paramref name="val2" />, or both <paramref name="val1" /> and <paramref name="val2" /> are equal to <see cref="F:System.Double.NaN" />, <see cref="F:System.Double.NaN" /> is returned.</returns>
		/// <param name="val1">The first of two double-precision floating-point numbers to compare. </param>
		/// <param name="val2">The second of two double-precision floating-point numbers to compare. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060011EC RID: 4588 RVA: 0x0004774C File Offset: 0x0004594C
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public static double Min(double val1, double val2)
		{
			if (double.IsNaN(val1) || double.IsNaN(val2))
			{
				return double.NaN;
			}
			return (val1 >= val2) ? val2 : val1;
		}

		/// <summary>Returns the smaller of two single-precision floating-point numbers.</summary>
		/// <returns>Parameter <paramref name="val1" /> or <paramref name="val2" />, whichever is smaller. If <paramref name="val1" />, <paramref name="val2" />, or both <paramref name="val1" /> and <paramref name="val2" /> are equal to <see cref="F:System.Single.NaN" />, <see cref="F:System.Single.NaN" /> is returned.</returns>
		/// <param name="val1">The first of two single-precision floating-point numbers to compare. </param>
		/// <param name="val2">The second of two single-precision floating-point numbers to compare. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060011ED RID: 4589 RVA: 0x00047788 File Offset: 0x00045988
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public static float Min(float val1, float val2)
		{
			if (float.IsNaN(val1) || float.IsNaN(val2))
			{
				return float.NaN;
			}
			return (val1 >= val2) ? val2 : val1;
		}

		/// <summary>Returns the smaller of two 32-bit signed integers.</summary>
		/// <returns>Parameter <paramref name="val1" /> or <paramref name="val2" />, whichever is smaller.</returns>
		/// <param name="val1">The first of two 32-bit signed integers to compare. </param>
		/// <param name="val2">The second of two 32-bit signed integers to compare. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060011EE RID: 4590 RVA: 0x000477C0 File Offset: 0x000459C0
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public static int Min(int val1, int val2)
		{
			return (val1 >= val2) ? val2 : val1;
		}

		/// <summary>Returns the smaller of two 64-bit signed integers.</summary>
		/// <returns>Parameter <paramref name="val1" /> or <paramref name="val2" />, whichever is smaller.</returns>
		/// <param name="val1">The first of two 64-bit signed integers to compare. </param>
		/// <param name="val2">The second of two 64-bit signed integers to compare. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060011EF RID: 4591 RVA: 0x000477D0 File Offset: 0x000459D0
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public static long Min(long val1, long val2)
		{
			return (val1 >= val2) ? val2 : val1;
		}

		/// <summary>Returns the smaller of two 8-bit signed integers.</summary>
		/// <returns>Parameter <paramref name="val1" /> or <paramref name="val2" />, whichever is smaller.</returns>
		/// <param name="val1">The first of two 8-bit signed integers to compare. </param>
		/// <param name="val2">The second of two 8-bit signed integers to compare. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060011F0 RID: 4592 RVA: 0x000477E0 File Offset: 0x000459E0
		[CLSCompliant(false)]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public static sbyte Min(sbyte val1, sbyte val2)
		{
			return ((int)val1 >= (int)val2) ? val2 : val1;
		}

		/// <summary>Returns the smaller of two 16-bit signed integers.</summary>
		/// <returns>Parameter <paramref name="val1" /> or <paramref name="val2" />, whichever is smaller.</returns>
		/// <param name="val1">The first of two 16-bit signed integers to compare. </param>
		/// <param name="val2">The second of two 16-bit signed integers to compare. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060011F1 RID: 4593 RVA: 0x000477F4 File Offset: 0x000459F4
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public static short Min(short val1, short val2)
		{
			return (val1 >= val2) ? val2 : val1;
		}

		/// <summary>Returns the smaller of two 32-bit unsigned integers.</summary>
		/// <returns>Parameter <paramref name="val1" /> or <paramref name="val2" />, whichever is smaller.</returns>
		/// <param name="val1">The first of two 32-bit unsigned integers to compare. </param>
		/// <param name="val2">The second of two 32-bit unsigned integers to compare. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060011F2 RID: 4594 RVA: 0x00047804 File Offset: 0x00045A04
		[CLSCompliant(false)]
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public static uint Min(uint val1, uint val2)
		{
			return (val1 >= val2) ? val2 : val1;
		}

		/// <summary>Returns the smaller of two 64-bit unsigned integers.</summary>
		/// <returns>Parameter <paramref name="val1" /> or <paramref name="val2" />, whichever is smaller.</returns>
		/// <param name="val1">The first of two 64-bit unsigned integers to compare. </param>
		/// <param name="val2">The second of two 64-bit unsigned integers to compare. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060011F3 RID: 4595 RVA: 0x00047814 File Offset: 0x00045A14
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[CLSCompliant(false)]
		public static ulong Min(ulong val1, ulong val2)
		{
			return (val1 >= val2) ? val2 : val1;
		}

		/// <summary>Returns the smaller of two 16-bit unsigned integers.</summary>
		/// <returns>Parameter <paramref name="val1" /> or <paramref name="val2" />, whichever is smaller.</returns>
		/// <param name="val1">The first of two 16-bit unsigned integers to compare. </param>
		/// <param name="val2">The second of two 16-bit unsigned integers to compare. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060011F4 RID: 4596 RVA: 0x00047824 File Offset: 0x00045A24
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[CLSCompliant(false)]
		public static ushort Min(ushort val1, ushort val2)
		{
			return (val1 >= val2) ? val2 : val1;
		}

		/// <summary>Rounds a decimal value to the nearest integral value.</summary>
		/// <returns>The integer nearest parameter <paramref name="d" />. If the fractional component of <paramref name="d" /> is halfway between two integers, one of which is even and the other odd, the even number is returned. Note that this method returns a <see cref="T:System.Decimal" /> instead of an integral type.</returns>
		/// <param name="d">A decimal number to be rounded. </param>
		/// <exception cref="T:System.OverflowException">The result is outside the range of a <see cref="T:System.Decimal" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060011F5 RID: 4597 RVA: 0x00047834 File Offset: 0x00045A34
		public static decimal Round(decimal d)
		{
			decimal num = decimal.Floor(d);
			decimal d2 = d - num;
			if ((d2 == 0.5m && 2.0m * (num / 2.0m - decimal.Floor(num / 2.0m)) != 0m) || d2 > 0.5m)
			{
				num = ++num;
			}
			return num;
		}

		/// <summary>Rounds a decimal value to a specified number of fractional digits.</summary>
		/// <returns>The number nearest to <paramref name="d" /> that contains a number of fractional digits equal to <paramref name="decimals" />. </returns>
		/// <param name="d">A decimal number to be rounded. </param>
		/// <param name="decimals">The number of decimal places in the return value. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="decimals" /> is less than 0 or greater than 28. </exception>
		/// <exception cref="T:System.OverflowException">The result is outside the range of a <see cref="T:System.Decimal" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060011F6 RID: 4598 RVA: 0x000478D0 File Offset: 0x00045AD0
		public static decimal Round(decimal d, int decimals)
		{
			return decimal.Round(d, decimals);
		}

		/// <summary>Rounds a decimal value to the nearest integer. A parameter specifies how to round the value if it is midway between two other numbers.</summary>
		/// <returns>The integer nearest <paramref name="d" />. If <paramref name="d" /> is halfway between two numbers, one of which is even and the other odd, then <paramref name="mode" /> determines which of the two is returned. </returns>
		/// <param name="d">A decimal number to be rounded. </param>
		/// <param name="mode">Specification for how to round <paramref name="d" /> if it is midway between two other numbers.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="mode" /> is not a valid value of <see cref="T:System.MidpointRounding" />.</exception>
		/// <exception cref="T:System.OverflowException">The result is outside the range of a <see cref="T:System.Decimal" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060011F7 RID: 4599 RVA: 0x000478DC File Offset: 0x00045ADC
		public static decimal Round(decimal d, MidpointRounding mode)
		{
			if (mode != MidpointRounding.ToEven && mode != MidpointRounding.AwayFromZero)
			{
				throw new ArgumentException("The value '" + mode + "' is not valid for this usage of the type MidpointRounding.", "mode");
			}
			if (mode == MidpointRounding.ToEven)
			{
				return Math.Round(d);
			}
			return Math.RoundAwayFromZero(d);
		}

		// Token: 0x060011F8 RID: 4600 RVA: 0x0004792C File Offset: 0x00045B2C
		private static decimal RoundAwayFromZero(decimal d)
		{
			decimal num = decimal.Floor(d);
			decimal d2 = d - num;
			if (num >= 0m && d2 >= 0.5m)
			{
				num = ++num;
			}
			else if (num < 0m && d2 > 0.5m)
			{
				num = ++num;
			}
			return num;
		}

		/// <summary>Rounds a decimal value to a specified number of fractional digits. A parameter specifies how to round the value if it is midway between two other numbers.</summary>
		/// <returns>The number nearest to <paramref name="d" /> that contains a number of fractional digits equal to <paramref name="decimals" />. If the number of fractional digits in <paramref name="d" /> is less than <paramref name="decimals" />, <paramref name="d" /> is returned unchanged.</returns>
		/// <param name="d">A decimal number to be rounded. </param>
		/// <param name="decimals">The number of decimal places in the return value. </param>
		/// <param name="mode">Specification for how to round <paramref name="d" /> if it is midway between two other numbers.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="decimals" /> is less than 0 or greater than 28. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="mode" /> is not a valid value of <see cref="T:System.MidpointRounding" />.</exception>
		/// <exception cref="T:System.OverflowException">The result is outside the range of a <see cref="T:System.Decimal" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060011F9 RID: 4601 RVA: 0x000479A8 File Offset: 0x00045BA8
		public static decimal Round(decimal d, int decimals, MidpointRounding mode)
		{
			return decimal.Round(d, decimals, mode);
		}

		/// <summary>Rounds a double-precision floating-point value to the nearest integral value.</summary>
		/// <returns>The integer nearest <paramref name="a" />. If the fractional component of <paramref name="a" /> is halfway between two integers, one of which is even and the other odd, then the even number is returned. Note that this method returns a <see cref="T:System.Double" /> instead of an integral type.</returns>
		/// <param name="a">A double-precision floating-point number to be rounded. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060011FA RID: 4602
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern double Round(double a);

		/// <summary>Rounds a double-precision floating-point value to a specified number of fractional digits.</summary>
		/// <returns>The number nearest to <paramref name="value" /> that contains a number of fractional digits equal to <paramref name="digits" />.</returns>
		/// <param name="value">A double-precision floating-point number to be rounded. </param>
		/// <param name="digits">The number of fractional digits in the return value. </param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="digits" /> is less than 0 or greater than 15. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060011FB RID: 4603 RVA: 0x000479B4 File Offset: 0x00045BB4
		public static double Round(double value, int digits)
		{
			if (digits < 0 || digits > 15)
			{
				throw new ArgumentOutOfRangeException(Locale.GetText("Value is too small or too big."));
			}
			return Math.Round2(value, digits, false);
		}

		// Token: 0x060011FC RID: 4604
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern double Round2(double value, int digits, bool away_from_zero);

		/// <summary>Rounds a double-precision floating-point value to the nearest integer. A parameter specifies how to round the value if it is midway between two other numbers.</summary>
		/// <returns>The integer nearest <paramref name="value" />. If <paramref name="value" /> is halfway between two integers, one of which is even and the other odd, then <paramref name="mode" /> determines which of the two is returned.</returns>
		/// <param name="value">A double-precision floating-point number to be rounded. </param>
		/// <param name="mode">Specification for how to round <paramref name="value" /> if it is midway between two other numbers.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="mode" /> is not a valid value of <see cref="T:System.MidpointRounding" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060011FD RID: 4605 RVA: 0x000479E0 File Offset: 0x00045BE0
		public static double Round(double value, MidpointRounding mode)
		{
			if (mode != MidpointRounding.ToEven && mode != MidpointRounding.AwayFromZero)
			{
				throw new ArgumentException("The value '" + mode + "' is not valid for this usage of the type MidpointRounding.", "mode");
			}
			if (mode == MidpointRounding.ToEven)
			{
				return Math.Round(value);
			}
			if (value > 0.0)
			{
				return Math.Floor(value + 0.5);
			}
			return Math.Ceiling(value - 0.5);
		}

		/// <summary>Rounds a double-precision floating-point value to the specified number of fractional digits. A parameter specifies how to round the value if it is midway between two other numbers.</summary>
		/// <returns>The number nearest to <paramref name="value" /> that has a number of fractional digits equal to <paramref name="digits" />. If the number of fractional digits in <paramref name="value" /> is less than <paramref name="digits" />, <paramref name="value" /> is returned unchanged.</returns>
		/// <param name="value">A double-precision floating-point number to be rounded. </param>
		/// <param name="digits">The number of fractional digits in the return value. </param>
		/// <param name="mode">Specification for how to round <paramref name="value" /> if it is midway between two other numbers.</param>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="digits" /> is less than 0 or greater than 15. </exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="mode" /> is not a valid value of <see cref="T:System.MidpointRounding" />.</exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060011FE RID: 4606 RVA: 0x00047A58 File Offset: 0x00045C58
		public static double Round(double value, int digits, MidpointRounding mode)
		{
			if (mode != MidpointRounding.ToEven && mode != MidpointRounding.AwayFromZero)
			{
				throw new ArgumentException("The value '" + mode + "' is not valid for this usage of the type MidpointRounding.", "mode");
			}
			if (mode == MidpointRounding.ToEven)
			{
				return Math.Round(value, digits);
			}
			return Math.Round2(value, digits, true);
		}

		/// <summary>Calculates the integral part of a specified double-precision floating-point number. </summary>
		/// <returns>The integral part of <paramref name="d" />; that is, the number that remains after any fractional digits have been discarded, or one of the values listed in the following table. <paramref name="d" />Return value<see cref="F:System.Double.NaN" /><see cref="F:System.Double.NaN" /><see cref="F:System.Double.NegativeInfinity" /><see cref="F:System.Double.NegativeInfinity" /><see cref="F:System.Double.PositiveInfinity" /><see cref="F:System.Double.PositiveInfinity" /></returns>
		/// <param name="d">A number to truncate.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x060011FF RID: 4607 RVA: 0x00047AA8 File Offset: 0x00045CA8
		public static double Truncate(double d)
		{
			if (d > 0.0)
			{
				return Math.Floor(d);
			}
			if (d < 0.0)
			{
				return Math.Ceiling(d);
			}
			return d;
		}

		/// <summary>Calculates the integral part of a specified decimal number. </summary>
		/// <returns>The integral part of <paramref name="d" />; that is, the number that remains after any fractional digits have been discarded.</returns>
		/// <param name="d">A number to truncate.</param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001200 RID: 4608 RVA: 0x00047AD8 File Offset: 0x00045CD8
		public static decimal Truncate(decimal d)
		{
			return decimal.Truncate(d);
		}

		/// <summary>Returns the largest integer less than or equal to the specified decimal number.</summary>
		/// <returns>The largest integer less than or equal to <paramref name="d" />. </returns>
		/// <param name="d">A decimal number. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001201 RID: 4609 RVA: 0x00047AE0 File Offset: 0x00045CE0
		public static decimal Floor(decimal d)
		{
			return decimal.Floor(d);
		}

		/// <summary>Returns a value indicating the sign of a decimal number.</summary>
		/// <returns>A number indicating the sign of <paramref name="value" />.Number Description -1 <paramref name="value" /> is less than zero. 0 <paramref name="value" /> is equal to zero. 1 <paramref name="value" /> is greater than zero. </returns>
		/// <param name="value">A signed <see cref="T:System.Decimal" /> number. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001202 RID: 4610 RVA: 0x00047AE8 File Offset: 0x00045CE8
		public static int Sign(decimal value)
		{
			if (value > 0m)
			{
				return 1;
			}
			return (!(value == 0m)) ? -1 : 0;
		}

		/// <summary>Returns a value indicating the sign of a double-precision floating-point number.</summary>
		/// <returns>A number indicating the sign of <paramref name="value" />.Number Description -1 <paramref name="value" /> is less than zero. 0 <paramref name="value" /> is equal to zero. 1 <paramref name="value" /> is greater than zero. </returns>
		/// <param name="value">A signed number. </param>
		/// <exception cref="T:System.ArithmeticException">
		///   <paramref name="value" /> is equal to <see cref="F:System.Double.NaN" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001203 RID: 4611 RVA: 0x00047B18 File Offset: 0x00045D18
		public static int Sign(double value)
		{
			if (double.IsNaN(value))
			{
				throw new ArithmeticException("NAN");
			}
			if (value > 0.0)
			{
				return 1;
			}
			return (value != 0.0) ? -1 : 0;
		}

		/// <summary>Returns a value indicating the sign of a single-precision floating-point number.</summary>
		/// <returns>A number indicating the sign of <paramref name="value" />.Number Description -1 <paramref name="value" /> is less than zero. 0 <paramref name="value" /> is equal to zero. 1 <paramref name="value" /> is greater than zero. </returns>
		/// <param name="value">A signed number. </param>
		/// <exception cref="T:System.ArithmeticException">
		///   <paramref name="value" /> is equal to <see cref="F:System.Single.NaN" />. </exception>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001204 RID: 4612 RVA: 0x00047B58 File Offset: 0x00045D58
		public static int Sign(float value)
		{
			if (float.IsNaN(value))
			{
				throw new ArithmeticException("NAN");
			}
			if (value > 0f)
			{
				return 1;
			}
			return (value != 0f) ? -1 : 0;
		}

		/// <summary>Returns a value indicating the sign of a 32-bit signed integer.</summary>
		/// <returns>A number indicating the sign of <paramref name="value" />.Number Description -1 <paramref name="value" /> is less than zero. 0 <paramref name="value" /> is equal to zero. 1 <paramref name="value" /> is greater than zero. </returns>
		/// <param name="value">A signed number. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001205 RID: 4613 RVA: 0x00047B90 File Offset: 0x00045D90
		public static int Sign(int value)
		{
			if (value > 0)
			{
				return 1;
			}
			return (value != 0) ? -1 : 0;
		}

		/// <summary>Returns a value indicating the sign of a 64-bit signed integer.</summary>
		/// <returns>A number indicating the sign of <paramref name="value" />.Number Description -1 <paramref name="value" /> is less than zero. 0 <paramref name="value" /> is equal to zero. 1 <paramref name="value" /> is greater than zero. </returns>
		/// <param name="value">A signed number. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001206 RID: 4614 RVA: 0x00047BA8 File Offset: 0x00045DA8
		public static int Sign(long value)
		{
			if (value > 0L)
			{
				return 1;
			}
			return (value != 0L) ? -1 : 0;
		}

		/// <summary>Returns a value indicating the sign of an 8-bit signed integer.</summary>
		/// <returns>A number indicating the sign of <paramref name="value" />.Number Description -1 <paramref name="value" /> is less than zero. 0 <paramref name="value" /> is equal to zero. 1 <paramref name="value" /> is greater than zero. </returns>
		/// <param name="value">A signed number. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001207 RID: 4615 RVA: 0x00047BC4 File Offset: 0x00045DC4
		[CLSCompliant(false)]
		public static int Sign(sbyte value)
		{
			if ((int)value > 0)
			{
				return 1;
			}
			return ((int)value != 0) ? -1 : 0;
		}

		/// <summary>Returns a value indicating the sign of a 16-bit signed integer.</summary>
		/// <returns>A number indicating the sign of <paramref name="value" />.Number Description -1 <paramref name="value" /> is less than zero. 0 <paramref name="value" /> is equal to zero. 1 <paramref name="value" /> is greater than zero. </returns>
		/// <param name="value">A signed number. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001208 RID: 4616 RVA: 0x00047BE0 File Offset: 0x00045DE0
		public static int Sign(short value)
		{
			if (value > 0)
			{
				return 1;
			}
			return (value != 0) ? -1 : 0;
		}

		/// <summary>Returns the sine of the specified angle.</summary>
		/// <returns>The sine of <paramref name="a" />. If <paramref name="a" /> is equal to <see cref="F:System.Double.NaN" />, <see cref="F:System.Double.NegativeInfinity" />, or <see cref="F:System.Double.PositiveInfinity" />, this method returns <see cref="F:System.Double.NaN" />.</returns>
		/// <param name="a">An angle, measured in radians. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001209 RID: 4617
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern double Sin(double a);

		/// <summary>Returns the cosine of the specified angle.</summary>
		/// <returns>The cosine of <paramref name="d" />. If <paramref name="d" /> is equal to <see cref="F:System.Double.NaN" />, <see cref="F:System.Double.NegativeInfinity" />, or <see cref="F:System.Double.PositiveInfinity" />, this method returns <see cref="F:System.Double.NaN" />. </returns>
		/// <param name="d">An angle, measured in radians. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600120A RID: 4618
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern double Cos(double d);

		/// <summary>Returns the tangent of the specified angle.</summary>
		/// <returns>The tangent of <paramref name="a" />. If <paramref name="a" /> is equal to <see cref="F:System.Double.NaN" />, <see cref="F:System.Double.NegativeInfinity" />, or <see cref="F:System.Double.PositiveInfinity" />, this method returns <see cref="F:System.Double.NaN" />.</returns>
		/// <param name="a">An angle, measured in radians. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600120B RID: 4619
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern double Tan(double a);

		/// <summary>Returns the hyperbolic sine of the specified angle.</summary>
		/// <returns>The hyperbolic sine of <paramref name="value" />. If <paramref name="value" /> is equal to <see cref="F:System.Double.NegativeInfinity" />, <see cref="F:System.Double.PositiveInfinity" />, or <see cref="F:System.Double.NaN" />, this method returns a <see cref="T:System.Double" /> equal to <paramref name="value" />.</returns>
		/// <param name="value">An angle, measured in radians. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600120C RID: 4620
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern double Sinh(double value);

		/// <summary>Returns the hyperbolic cosine of the specified angle.</summary>
		/// <returns>The hyperbolic cosine of <paramref name="value" />. If <paramref name="value" /> is equal to <see cref="F:System.Double.NegativeInfinity" /> or <see cref="F:System.Double.PositiveInfinity" />, <see cref="F:System.Double.PositiveInfinity" /> is returned. If <paramref name="value" /> is equal to <see cref="F:System.Double.NaN" />, <see cref="F:System.Double.NaN" /> is returned.</returns>
		/// <param name="value">An angle, measured in radians. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600120D RID: 4621
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern double Cosh(double value);

		/// <summary>Returns the hyperbolic tangent of the specified angle.</summary>
		/// <returns>The hyperbolic tangent of <paramref name="value" />. If <paramref name="value" /> is equal to <see cref="F:System.Double.NegativeInfinity" />, this method returns -1. If value is equal to <see cref="F:System.Double.PositiveInfinity" />, this method returns 1. If <paramref name="value" /> is equal to <see cref="F:System.Double.NaN" />, this method returns <see cref="F:System.Double.NaN" />.</returns>
		/// <param name="value">An angle, measured in radians. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600120E RID: 4622
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern double Tanh(double value);

		/// <summary>Returns the angle whose cosine is the specified number.</summary>
		/// <returns>An angle, θ, measured in radians, such that 0 ≤θ≤π-or- <see cref="F:System.Double.NaN" /> if <paramref name="d" /> &lt; -1 or <paramref name="d" /> &gt; 1 or <paramref name="d" /> equals <see cref="F:System.Double.NaN" />.</returns>
		/// <param name="d">A number representing a cosine, where <paramref name="d" /> must be greater than or equal to -1, but less than or equal to 1. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0600120F RID: 4623
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern double Acos(double d);

		/// <summary>Returns the angle whose sine is the specified number.</summary>
		/// <returns>An angle, θ, measured in radians, such that -π/2 ≤θ≤π/2 -or- <see cref="F:System.Double.NaN" /> if <paramref name="d" /> &lt; -1 or <paramref name="d" /> &gt; 1 or <paramref name="d" /> equals <see cref="F:System.Double.NaN" />.</returns>
		/// <param name="d">A number representing a sine, where <paramref name="d" /> must be greater than or equal to -1. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001210 RID: 4624
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern double Asin(double d);

		/// <summary>Returns the angle whose tangent is the specified number.</summary>
		/// <returns>An angle, θ, measured in radians, such that -π/2 ≤θ≤π/2.-or- <see cref="F:System.Double.NaN" /> if <paramref name="d" /> equals <see cref="F:System.Double.NaN" />, -π/2 rounded to double precision (-1.5707963267949) if <paramref name="d" /> equals <see cref="F:System.Double.NegativeInfinity" />, or π/2 rounded to double precision (1.5707963267949) if <paramref name="d" /> equals <see cref="F:System.Double.PositiveInfinity" />.</returns>
		/// <param name="d">A number representing a tangent. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001211 RID: 4625
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern double Atan(double d);

		/// <summary>Returns the angle whose tangent is the quotient of two specified numbers.</summary>
		/// <returns>An angle, θ, measured in radians, such that -π≤θ≤π, and tan(θ) = <paramref name="y" /> / <paramref name="x" />, where (<paramref name="x" />, <paramref name="y" />) is a point in the Cartesian plane. Observe the following: For (<paramref name="x" />, <paramref name="y" />) in quadrant 1, 0 &lt; θ &lt; π/2.For (<paramref name="x" />, <paramref name="y" />) in quadrant 2, π/2 &lt; θ≤π.For (<paramref name="x" />, <paramref name="y" />) in quadrant 3, -π &lt; θ &lt; -π/2.For (<paramref name="x" />, <paramref name="y" />) in quadrant 4, -π/2 &lt; θ &lt; 0.For points on the boundaries of the quadrants, the return value is the following:If y is 0 and x is not negative, θ = 0.If y is 0 and x is negative, θ = π.If y is positive and x is 0, θ = π/2.If y is negative and x is 0, θ = -π/2.If <paramref name="x" /> or <paramref name="y" /> is <see cref="F:System.Double.NaN" />, or if <paramref name="x" /> and <paramref name="y" /> are either <see cref="F:System.Double.PositiveInfinity" /> or <see cref="F:System.Double.NegativeInfinity" />, the method returns <see cref="F:System.Double.NaN" />.</returns>
		/// <param name="y">The y coordinate of a point. </param>
		/// <param name="x">The x coordinate of a point. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001212 RID: 4626
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern double Atan2(double y, double x);

		/// <summary>Returns e raised to the specified power.</summary>
		/// <returns>The number e raised to the power <paramref name="d" />. If <paramref name="d" /> equals <see cref="F:System.Double.NaN" /> or <see cref="F:System.Double.PositiveInfinity" />, that value is returned. If <paramref name="d" /> equals <see cref="F:System.Double.NegativeInfinity" />, 0 is returned.</returns>
		/// <param name="d">A number specifying a power. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001213 RID: 4627
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern double Exp(double d);

		/// <summary>Returns the natural (base e) logarithm of a specified number.</summary>
		/// <returns>Sign of <paramref name="d" />Returns Positive The natural logarithm of <paramref name="d" />; that is, ln <paramref name="d" />, or log e<paramref name="d" />Zero <see cref="F:System.Double.NegativeInfinity" />Negative <see cref="F:System.Double.NaN" />If <paramref name="d" /> is equal to <see cref="F:System.Double.NaN" />, returns <see cref="F:System.Double.NaN" />. If <paramref name="d" /> is equal to <see cref="F:System.Double.PositiveInfinity" />, returns <see cref="F:System.Double.PositiveInfinity" />.</returns>
		/// <param name="d">A number whose logarithm is to be found. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001214 RID: 4628
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern double Log(double d);

		/// <summary>Returns the base 10 logarithm of a specified number.</summary>
		/// <returns>Sign of <paramref name="d" />Returns Positive The base 10 log of <paramref name="d" />; that is, log 10<paramref name="d" />. Zero <see cref="F:System.Double.NegativeInfinity" />Negative <see cref="F:System.Double.NaN" />If <paramref name="d" /> is equal to <see cref="F:System.Double.NaN" />, this method returns <see cref="F:System.Double.NaN" />. If <paramref name="d" /> is equal to <see cref="F:System.Double.PositiveInfinity" />, this method returns <see cref="F:System.Double.PositiveInfinity" />.</returns>
		/// <param name="d">A number whose logarithm is to be found. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001215 RID: 4629
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern double Log10(double d);

		/// <summary>Returns a specified number raised to the specified power.</summary>
		/// <returns>The number <paramref name="x" /> raised to the power <paramref name="y" />.</returns>
		/// <param name="x">A double-precision floating-point number to be raised to a power. </param>
		/// <param name="y">A double-precision floating-point number that specifies a power. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001216 RID: 4630
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern double Pow(double x, double y);

		/// <summary>Returns the square root of a specified number.</summary>
		/// <returns>Value of <paramref name="d" />Returns Zero, or positive The positive square root of <paramref name="d" />. Negative <see cref="F:System.Double.NaN" />If <paramref name="d" /> is equal to <see cref="F:System.Double.NaN" /> or <see cref="F:System.Double.PositiveInfinity" />, that value is returned.</returns>
		/// <param name="d">A number. </param>
		/// <filterpriority>1</filterpriority>
		// Token: 0x06001217 RID: 4631
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern double Sqrt(double d);

		/// <summary>Represents the natural logarithmic base, specified by the constant, e.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0400052B RID: 1323
		public const double E = 2.718281828459045;

		/// <summary>Represents the ratio of the circumference of a circle to its diameter, specified by the constant, π.</summary>
		/// <filterpriority>1</filterpriority>
		// Token: 0x0400052C RID: 1324
		public const double PI = 3.141592653589793;
	}
}
