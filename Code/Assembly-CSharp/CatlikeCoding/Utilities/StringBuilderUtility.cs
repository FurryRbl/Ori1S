using System;
using System.Text;
using UnityEngine;

namespace CatlikeCoding.Utilities
{
	// Token: 0x02000763 RID: 1891
	public static class StringBuilderUtility
	{
		// Token: 0x06002C25 RID: 11301 RVA: 0x000BD2FC File Offset: 0x000BB4FC
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
			StringBuilderUtility.Reverse(s, length, s.Length - 1);
		}

		// Token: 0x06002C26 RID: 11302 RVA: 0x000BD350 File Offset: 0x000BB550
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
			StringBuilderUtility.Reverse(s, length, s.Length - 1);
		}

		// Token: 0x06002C27 RID: 11303 RVA: 0x000BD3C4 File Offset: 0x000BB5C4
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
					s.Append(StringBuilderUtility.groupSeparator);
					num = 0;
				}
				s.Append((char)(number % 10 + 48));
				number /= 10;
			}
			while (number > 0);
			StringBuilderUtility.Reverse(s, length, s.Length - 1);
		}

		// Token: 0x06002C28 RID: 11304 RVA: 0x000BD434 File Offset: 0x000BB634
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
					s.Append(StringBuilderUtility.groupSeparator);
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
					s.Append(StringBuilderUtility.groupSeparator);
					num2 = 0;
				}
				s.Append('0');
			}
			StringBuilderUtility.Reverse(s, length, s.Length - 1);
		}

		// Token: 0x06002C29 RID: 11305 RVA: 0x000BD4DC File Offset: 0x000BB6DC
		public static void AppendFloat(StringBuilder s, float number, int decimalCount)
		{
			int i = (int)number;
			StringBuilderUtility.AppendInt(s, i);
			if (decimalCount <= 0)
			{
				return;
			}
			s.Append(StringBuilderUtility.decimalSeparator);
			number -= (float)i;
			for (i = 0; i < decimalCount; i++)
			{
				number *= 10f;
			}
			i = Mathf.FloorToInt(number);
			StringBuilderUtility.AppendInt(s, (i >= 0) ? i : (-i), decimalCount);
		}

		// Token: 0x06002C2A RID: 11306 RVA: 0x000BD548 File Offset: 0x000BB748
		public static void AppendFloat(StringBuilder s, float number, int decimalCount, int digitCount)
		{
			int i = (int)number;
			StringBuilderUtility.AppendInt(s, i, digitCount);
			if (decimalCount <= 0)
			{
				return;
			}
			s.Append(StringBuilderUtility.decimalSeparator);
			number -= (float)i;
			for (i = 0; i < decimalCount; i++)
			{
				number *= 10f;
			}
			i = Mathf.FloorToInt(number);
			StringBuilderUtility.AppendInt(s, (i >= 0) ? i : (-i), decimalCount);
		}

		// Token: 0x06002C2B RID: 11307 RVA: 0x000BD5B4 File Offset: 0x000BB7B4
		public static void AppendFloatGrouped(StringBuilder s, float number, int decimalCount)
		{
			int i = (int)number;
			StringBuilderUtility.AppendIntGrouped(s, i);
			if (decimalCount <= 0)
			{
				return;
			}
			s.Append(StringBuilderUtility.decimalSeparator);
			number -= (float)i;
			for (i = 0; i < decimalCount; i++)
			{
				number *= 10f;
			}
			i = Mathf.FloorToInt(number);
			StringBuilderUtility.AppendInt(s, (i >= 0) ? i : (-i), decimalCount);
		}

		// Token: 0x06002C2C RID: 11308 RVA: 0x000BD620 File Offset: 0x000BB820
		public static void AppendFloatGrouped(StringBuilder s, float number, int decimalCount, int digitCount)
		{
			int i = (int)number;
			StringBuilderUtility.AppendIntGrouped(s, i, digitCount);
			if (decimalCount <= 0)
			{
				return;
			}
			s.Append(StringBuilderUtility.decimalSeparator);
			number -= (float)i;
			for (i = 0; i < decimalCount; i++)
			{
				number *= 10f;
			}
			i = Mathf.FloorToInt(number);
			StringBuilderUtility.AppendInt(s, (i >= 0) ? i : (-i), decimalCount);
		}

		// Token: 0x06002C2D RID: 11309 RVA: 0x000BD68C File Offset: 0x000BB88C
		public static void Reverse(StringBuilder s, int firstIndex, int lastIndex)
		{
			while (firstIndex < lastIndex)
			{
				char value = s[firstIndex];
				s[firstIndex++] = s[lastIndex];
				s[lastIndex--] = value;
			}
		}

		// Token: 0x040027EA RID: 10218
		public static char decimalSeparator = '.';

		// Token: 0x040027EB RID: 10219
		public static char groupSeparator = ',';

		// Token: 0x040027EC RID: 10220
		public static char padding = '0';
	}
}
