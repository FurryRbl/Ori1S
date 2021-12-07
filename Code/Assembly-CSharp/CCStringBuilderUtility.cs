using System;
using System.Text;
using UnityEngine;

// Token: 0x0200022C RID: 556
public static class CCStringBuilderUtility
{
	// Token: 0x060012C7 RID: 4807 RVA: 0x00056468 File Offset: 0x00054668
	public static void AppendInt(StringBuilder s, int number)
	{
		if (number < 0)
		{
			s.Append('-');
			number = -number;
		}
		int length = s.Length;
		do
		{
			s.Append((char)(number % 10 + 48));
			number /= 10;
		}
		while (number > 0);
		CCStringBuilderUtility.Reverse(s, length, s.Length - 1);
	}

	// Token: 0x060012C8 RID: 4808 RVA: 0x000564BC File Offset: 0x000546BC
	public static void AppendInt(StringBuilder s, int number, int digitCount)
	{
		if (number < 0)
		{
			s.Append('-');
			number = -number;
		}
		int length = s.Length;
		int num = 0;
		do
		{
			s.Append((char)(number % 10 + 48));
			number /= 10;
			num++;
		}
		while (number > 0);
		while (num++ < digitCount)
		{
			s.Append('0');
		}
		CCStringBuilderUtility.Reverse(s, length, s.Length - 1);
	}

	// Token: 0x060012C9 RID: 4809 RVA: 0x00056530 File Offset: 0x00054730
	public static void AppendIntGrouped(StringBuilder s, int number)
	{
		if (number < 0)
		{
			s.Append('-');
			number = -number;
		}
		int length = s.Length;
		int num = -1;
		do
		{
			if (++num == 3)
			{
				s.Append(CCStringBuilderUtility.groupSeparator);
				num = 0;
			}
			s.Append((char)(number % 10 + 48));
			number /= 10;
		}
		while (number > 0);
		CCStringBuilderUtility.Reverse(s, length, s.Length - 1);
	}

	// Token: 0x060012CA RID: 4810 RVA: 0x000565A0 File Offset: 0x000547A0
	public static void AppendIntGrouped(StringBuilder s, int number, int digitCount)
	{
		if (number < 0)
		{
			s.Append('-');
			number = -number;
		}
		int length = s.Length;
		int num = 0;
		int num2 = -1;
		do
		{
			if (++num2 == 3)
			{
				s.Append(CCStringBuilderUtility.groupSeparator);
				num2 = 0;
			}
			s.Append((char)(number % 10 + 48));
			number /= 10;
			num++;
		}
		while (number > 0);
		while (num++ < digitCount)
		{
			if (++num2 == 3)
			{
				s.Append(CCStringBuilderUtility.groupSeparator);
				num2 = 0;
			}
			s.Append('0');
		}
		CCStringBuilderUtility.Reverse(s, length, s.Length - 1);
	}

	// Token: 0x060012CB RID: 4811 RVA: 0x00056648 File Offset: 0x00054848
	public static void AppendFloat(StringBuilder s, float number, int decimalCount)
	{
		int i = (int)number;
		CCStringBuilderUtility.AppendInt(s, i);
		if (decimalCount <= 0)
		{
			return;
		}
		s.Append(CCStringBuilderUtility.decimalSeparator);
		number -= (float)i;
		for (i = 0; i < decimalCount; i++)
		{
			number *= 10f;
		}
		i = Mathf.FloorToInt(number);
		CCStringBuilderUtility.AppendInt(s, (i >= 0) ? i : (-i), decimalCount);
	}

	// Token: 0x060012CC RID: 4812 RVA: 0x000566B4 File Offset: 0x000548B4
	public static void AppendFloat(StringBuilder s, float number, int decimalCount, int digitCount)
	{
		int i = (int)number;
		CCStringBuilderUtility.AppendInt(s, i, digitCount);
		if (decimalCount <= 0)
		{
			return;
		}
		s.Append(CCStringBuilderUtility.decimalSeparator);
		number -= (float)i;
		for (i = 0; i < decimalCount; i++)
		{
			number *= 10f;
		}
		i = Mathf.FloorToInt(number);
		CCStringBuilderUtility.AppendInt(s, (i >= 0) ? i : (-i), decimalCount);
	}

	// Token: 0x060012CD RID: 4813 RVA: 0x00056720 File Offset: 0x00054920
	public static void AppendFloatGrouped(StringBuilder s, float number, int decimalCount)
	{
		int i = (int)number;
		CCStringBuilderUtility.AppendIntGrouped(s, i);
		if (decimalCount <= 0)
		{
			return;
		}
		s.Append(CCStringBuilderUtility.decimalSeparator);
		number -= (float)i;
		for (i = 0; i < decimalCount; i++)
		{
			number *= 10f;
		}
		i = Mathf.FloorToInt(number);
		CCStringBuilderUtility.AppendInt(s, (i >= 0) ? i : (-i), decimalCount);
	}

	// Token: 0x060012CE RID: 4814 RVA: 0x0005678C File Offset: 0x0005498C
	public static void AppendFloatGrouped(StringBuilder s, float number, int decimalCount, int digitCount)
	{
		int i = (int)number;
		CCStringBuilderUtility.AppendIntGrouped(s, i, digitCount);
		if (decimalCount <= 0)
		{
			return;
		}
		s.Append(CCStringBuilderUtility.decimalSeparator);
		number -= (float)i;
		for (i = 0; i < decimalCount; i++)
		{
			number *= 10f;
		}
		i = Mathf.FloorToInt(number);
		CCStringBuilderUtility.AppendInt(s, (i >= 0) ? i : (-i), decimalCount);
	}

	// Token: 0x060012CF RID: 4815 RVA: 0x000567F8 File Offset: 0x000549F8
	public static void Reverse(StringBuilder s, int firstIndex, int lastIndex)
	{
		while (firstIndex < lastIndex)
		{
			char value = s[firstIndex];
			s[firstIndex++] = s[lastIndex];
			s[lastIndex--] = value;
		}
	}

	// Token: 0x0400104C RID: 4172
	public static char decimalSeparator = '.';

	// Token: 0x0400104D RID: 4173
	public static char groupSeparator = ',';

	// Token: 0x0400104E RID: 4174
	public static char padding = '0';
}
