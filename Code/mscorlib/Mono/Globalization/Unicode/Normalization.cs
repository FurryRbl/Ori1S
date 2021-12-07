using System;
using System.Runtime.CompilerServices;
using System.Text;

namespace Mono.Globalization.Unicode
{
	// Token: 0x02000089 RID: 137
	internal class Normalization
	{
		// Token: 0x06000807 RID: 2055 RVA: 0x0001D4C0 File Offset: 0x0001B6C0
		unsafe static Normalization()
		{
			object obj = Normalization.forLock;
			lock (obj)
			{
				IntPtr value;
				IntPtr value2;
				IntPtr value3;
				IntPtr value4;
				IntPtr value5;
				IntPtr value6;
				Normalization.load_normalization_resource(out value, out value2, out value3, out value4, out value5, out value6);
				Normalization.props = (byte*)((void*)value);
				Normalization.mappedChars = (int*)((void*)value2);
				Normalization.charMapIndex = (short*)((void*)value3);
				Normalization.helperIndex = (short*)((void*)value4);
				Normalization.mapIdxToComposite = (ushort*)((void*)value5);
				Normalization.combiningClass = (byte*)((void*)value6);
			}
			Normalization.isReady = true;
		}

		// Token: 0x06000808 RID: 2056 RVA: 0x0001D56C File Offset: 0x0001B76C
		private unsafe static uint PropValue(int cp)
		{
			return (uint)Normalization.props[NormalizationTableUtil.PropIdx(cp)];
		}

		// Token: 0x06000809 RID: 2057 RVA: 0x0001D57C File Offset: 0x0001B77C
		private unsafe static int CharMapIdx(int cp)
		{
			return (int)Normalization.charMapIndex[NormalizationTableUtil.MapIdx(cp)];
		}

		// Token: 0x0600080A RID: 2058 RVA: 0x0001D590 File Offset: 0x0001B790
		private unsafe static int GetNormalizedStringLength(int ch)
		{
			int num = (int)Normalization.charMapIndex[NormalizationTableUtil.MapIdx(ch)];
			int num2 = num;
			while (Normalization.mappedChars[num2] != 0)
			{
				num2++;
			}
			return num2 - num;
		}

		// Token: 0x0600080B RID: 2059 RVA: 0x0001D5CC File Offset: 0x0001B7CC
		private unsafe static byte GetCombiningClass(int c)
		{
			return Normalization.combiningClass[NormalizationTableUtil.Combining.ToIndex(c)];
		}

		// Token: 0x0600080C RID: 2060 RVA: 0x0001D5E0 File Offset: 0x0001B7E0
		private unsafe static int GetPrimaryCompositeFromMapIndex(int src)
		{
			return (int)Normalization.mapIdxToComposite[NormalizationTableUtil.Composite.ToIndex(src)];
		}

		// Token: 0x0600080D RID: 2061 RVA: 0x0001D5F8 File Offset: 0x0001B7F8
		private unsafe static int GetPrimaryCompositeHelperIndex(int cp)
		{
			return (int)Normalization.helperIndex[NormalizationTableUtil.Helper.ToIndex(cp)];
		}

		// Token: 0x0600080E RID: 2062 RVA: 0x0001D610 File Offset: 0x0001B810
		private unsafe static int GetPrimaryCompositeCharIndex(object chars, int start)
		{
			string text = chars as string;
			StringBuilder stringBuilder = chars as StringBuilder;
			char c = (text == null) ? stringBuilder[start] : text[start];
			int num = (stringBuilder == null) ? text.Length : stringBuilder.Length;
			int num2 = Normalization.GetPrimaryCompositeHelperIndex((int)c);
			if (num2 == 0)
			{
				return 0;
			}
			while (Normalization.mappedChars[num2] == (int)c)
			{
				int num3 = 0;
				int num4 = 1;
				int num5 = 1;
				for (;;)
				{
					int num6 = num3;
					if (Normalization.mappedChars[num2 + num4] == 0)
					{
						return num2;
					}
					if (start + num4 >= num)
					{
						return 0;
					}
					bool flag = false;
					char c2;
					do
					{
						c2 = ((text == null) ? stringBuilder[start + num5] : text[start + num5]);
						num3 = (int)Normalization.GetCombiningClass((int)c2);
						if (Normalization.mappedChars[num2 + num4] == (int)c2)
						{
							goto Block_7;
						}
						if (num3 < num6)
						{
							break;
						}
					}
					while (++num5 + start < num && num3 != 0);
					IL_105:
					if (!flag)
					{
						if (num6 >= num3)
						{
							break;
						}
						num5--;
						if (Normalization.mappedChars[num2 + num4] != (int)c2)
						{
							break;
						}
					}
					num4++;
					num5++;
					continue;
					Block_7:
					flag = true;
					goto IL_105;
				}
				while (Normalization.mappedChars[num4] != 0)
				{
					num4++;
				}
				num2 += num4 + 1;
			}
			return 0;
		}

		// Token: 0x0600080F RID: 2063 RVA: 0x0001D7A4 File Offset: 0x0001B9A4
		private static string Compose(string source, int checkType)
		{
			StringBuilder stringBuilder = null;
			Normalization.Decompose(source, ref stringBuilder, checkType);
			if (stringBuilder == null)
			{
				stringBuilder = Normalization.Combine(source, 0, checkType);
			}
			else
			{
				Normalization.Combine(stringBuilder, 0, checkType);
			}
			return (stringBuilder == null) ? source : stringBuilder.ToString();
		}

		// Token: 0x06000810 RID: 2064 RVA: 0x0001D7EC File Offset: 0x0001B9EC
		private static StringBuilder Combine(string source, int start, int checkType)
		{
			for (int i = 0; i < source.Length; i++)
			{
				if (Normalization.QuickCheck(source[i], checkType) != NormalizationCheck.Yes)
				{
					StringBuilder stringBuilder = new StringBuilder(source.Length + source.Length / 10);
					stringBuilder.Append(source);
					Normalization.Combine(stringBuilder, i, checkType);
					return stringBuilder;
				}
			}
			return null;
		}

		// Token: 0x06000811 RID: 2065 RVA: 0x0001D850 File Offset: 0x0001BA50
		private static bool CanBePrimaryComposite(int i)
		{
			if (i >= 13312 && i <= 40891)
			{
				return Normalization.GetPrimaryCompositeHelperIndex(i) != 0;
			}
			return (Normalization.PropValue(i) & 128U) != 0U;
		}

		// Token: 0x06000812 RID: 2066 RVA: 0x0001D888 File Offset: 0x0001BA88
		private unsafe static void Combine(StringBuilder sb, int start, int checkType)
		{
			for (int i = start; i < sb.Length; i++)
			{
				if (Normalization.QuickCheck(sb[i], checkType) != NormalizationCheck.Yes)
				{
					int num = i;
					while (i > 0)
					{
						if (Normalization.GetCombiningClass((int)sb[i]) == 0)
						{
							break;
						}
						i--;
					}
					int num2 = 0;
					while (i < num)
					{
						num2 = Normalization.GetPrimaryCompositeMapIndex(sb, (int)sb[i], i);
						if (num2 > 0)
						{
							break;
						}
						i++;
					}
					if (num2 == 0)
					{
						i = num;
					}
					else
					{
						int primaryCompositeFromMapIndex = Normalization.GetPrimaryCompositeFromMapIndex(num2);
						int normalizedStringLength = Normalization.GetNormalizedStringLength(primaryCompositeFromMapIndex);
						if (primaryCompositeFromMapIndex == 0 || normalizedStringLength == 0)
						{
							throw new SystemException("Internal error: should not happen. Input: " + sb);
						}
						int j = 0;
						sb.Insert(i++, (char)primaryCompositeFromMapIndex);
						while (j < normalizedStringLength)
						{
							if ((int)sb[i] == Normalization.mappedChars[num2 + j])
							{
								sb.Remove(i, 1);
								j++;
							}
							else
							{
								i++;
							}
						}
						i = num - 1;
					}
				}
			}
		}

		// Token: 0x06000813 RID: 2067 RVA: 0x0001D9A8 File Offset: 0x0001BBA8
		private static int GetPrimaryCompositeMapIndex(object o, int cur, int bufferPos)
		{
			if ((Normalization.PropValue(cur) & 64U) != 0U)
			{
				return 0;
			}
			if (Normalization.GetCombiningClass(cur) != 0)
			{
				return 0;
			}
			return Normalization.GetPrimaryCompositeCharIndex(o, bufferPos);
		}

		// Token: 0x06000814 RID: 2068 RVA: 0x0001D9DC File Offset: 0x0001BBDC
		private static string Decompose(string source, int checkType)
		{
			StringBuilder stringBuilder = null;
			Normalization.Decompose(source, ref stringBuilder, checkType);
			return (stringBuilder == null) ? source : stringBuilder.ToString();
		}

		// Token: 0x06000815 RID: 2069 RVA: 0x0001DA08 File Offset: 0x0001BC08
		private static void Decompose(string source, ref StringBuilder sb, int checkType)
		{
			int[] array = null;
			int num = 0;
			for (int i = 0; i < source.Length; i++)
			{
				if (Normalization.QuickCheck(source[i], checkType) == NormalizationCheck.No)
				{
					Normalization.DecomposeChar(ref sb, ref array, source, i, ref num);
				}
			}
			if (sb != null)
			{
				sb.Append(source, num, source.Length - num);
			}
			Normalization.ReorderCanonical(source, ref sb, 1);
		}

		// Token: 0x06000816 RID: 2070 RVA: 0x0001DA70 File Offset: 0x0001BC70
		private static void ReorderCanonical(string src, ref StringBuilder sb, int start)
		{
			if (sb == null)
			{
				for (int i = 1; i < src.Length; i++)
				{
					int num = (int)Normalization.GetCombiningClass((int)src[i]);
					if (num != 0)
					{
						if ((int)Normalization.GetCombiningClass((int)src[i - 1]) > num)
						{
							sb = new StringBuilder(src.Length);
							sb.Append(src, 0, src.Length);
							Normalization.ReorderCanonical(src, ref sb, i);
							return;
						}
					}
				}
				return;
			}
			for (int j = start; j < sb.Length; j++)
			{
				int num2 = (int)Normalization.GetCombiningClass((int)sb[j]);
				if (num2 != 0)
				{
					if ((int)Normalization.GetCombiningClass((int)sb[j - 1]) > num2)
					{
						char value = sb[j - 1];
						sb[j - 1] = sb[j];
						sb[j] = value;
						j--;
					}
				}
			}
		}

		// Token: 0x06000817 RID: 2071 RVA: 0x0001DB60 File Offset: 0x0001BD60
		private static void DecomposeChar(ref StringBuilder sb, ref int[] buf, string s, int i, ref int start)
		{
			if (sb == null)
			{
				sb = new StringBuilder(s.Length + 100);
			}
			sb.Append(s, start, i - start);
			if (buf == null)
			{
				buf = new int[19];
			}
			Normalization.GetCanonical((int)s[i], buf, 0);
			int num = 0;
			while (buf[num] != 0)
			{
				if (buf[num] < 65535)
				{
					sb.Append((char)buf[num]);
				}
				else
				{
					sb.Append((char)(buf[num] >> 10));
					sb.Append((char)((buf[num] & 4095) + 56320));
				}
				num++;
			}
			start = i + 1;
		}

		// Token: 0x06000818 RID: 2072 RVA: 0x0001DC24 File Offset: 0x0001BE24
		public static NormalizationCheck QuickCheck(char c, int type)
		{
			switch (type)
			{
			case 1:
				if ('가' <= c && c <= '힣')
				{
					return NormalizationCheck.No;
				}
				return ((Normalization.PropValue((int)c) & 1U) == 0U) ? NormalizationCheck.Yes : NormalizationCheck.No;
			case 2:
			{
				uint num = Normalization.PropValue((int)c);
				return ((num & 16U) == 0U) ? (((num & 32U) == 0U) ? NormalizationCheck.Yes : NormalizationCheck.Maybe) : NormalizationCheck.No;
			}
			case 3:
				if ('가' <= c && c <= '힣')
				{
					return NormalizationCheck.No;
				}
				return ((Normalization.PropValue((int)c) & 2U) == 0U) ? NormalizationCheck.Yes : NormalizationCheck.No;
			default:
			{
				uint num = Normalization.PropValue((int)c);
				return ((num & 4U) != 0U) ? NormalizationCheck.No : (((num & 8U) != 0U) ? NormalizationCheck.Maybe : NormalizationCheck.Yes);
			}
			}
		}

		// Token: 0x06000819 RID: 2073 RVA: 0x0001DCF4 File Offset: 0x0001BEF4
		private static bool GetCanonicalHangul(int s, int[] buf, int bufIdx)
		{
			int num = s - 44032;
			if (num < 0 || num >= 11172)
			{
				return false;
			}
			int num2 = 4352 + num / 588;
			int num3 = 4449 + num % 588 / 28;
			int num4 = 4519 + num % 28;
			buf[bufIdx++] = num2;
			buf[bufIdx++] = num3;
			if (num4 != 4519)
			{
				buf[bufIdx++] = num4;
			}
			buf[bufIdx] = 0;
			return true;
		}

		// Token: 0x0600081A RID: 2074 RVA: 0x0001DD74 File Offset: 0x0001BF74
		public unsafe static void GetCanonical(int c, int[] buf, int bufIdx)
		{
			if (!Normalization.GetCanonicalHangul(c, buf, bufIdx))
			{
				int num = Normalization.CharMapIdx(c);
				while (Normalization.mappedChars[num] != 0)
				{
					buf[bufIdx++] = Normalization.mappedChars[num];
					num++;
				}
				buf[bufIdx] = 0;
			}
		}

		// Token: 0x0600081B RID: 2075 RVA: 0x0001DDC4 File Offset: 0x0001BFC4
		public static bool IsNormalized(string source, int type)
		{
			int num = -1;
			for (int i = 0; i < source.Length; i++)
			{
				int num2 = (int)Normalization.GetCombiningClass((int)source[i]);
				if (num2 != 0 && num2 < num)
				{
					return false;
				}
				num = num2;
				switch (Normalization.QuickCheck(source[i], type))
				{
				case NormalizationCheck.No:
					return false;
				case NormalizationCheck.Maybe:
				{
					switch (type)
					{
					case 0:
					case 2:
						return source == Normalization.Normalize(source, type);
					}
					int num3 = i;
					while (i > 0)
					{
						if (Normalization.GetCombiningClass((int)source[i]) == 0)
						{
							break;
						}
						i--;
					}
					while (i < num3)
					{
						if (Normalization.GetPrimaryCompositeCharIndex(source, i) != 0)
						{
							return false;
						}
						i++;
					}
					break;
				}
				}
			}
			return true;
		}

		// Token: 0x0600081C RID: 2076 RVA: 0x0001DEAC File Offset: 0x0001C0AC
		public static string Normalize(string source, int type)
		{
			switch (type)
			{
			case 1:
			case 3:
				return Normalization.Decompose(source, type);
			default:
				return Normalization.Compose(source, type);
			}
		}

		// Token: 0x170000DE RID: 222
		// (get) Token: 0x0600081D RID: 2077 RVA: 0x0001DEE4 File Offset: 0x0001C0E4
		public static bool IsReady
		{
			get
			{
				return Normalization.isReady;
			}
		}

		// Token: 0x0600081E RID: 2078
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void load_normalization_resource(out IntPtr props, out IntPtr mappedChars, out IntPtr charMapIndex, out IntPtr helperIndex, out IntPtr mapIdxToComposite, out IntPtr combiningClass);

		// Token: 0x04000189 RID: 393
		public const int NoNfd = 1;

		// Token: 0x0400018A RID: 394
		public const int NoNfkd = 2;

		// Token: 0x0400018B RID: 395
		public const int NoNfc = 4;

		// Token: 0x0400018C RID: 396
		public const int MaybeNfc = 8;

		// Token: 0x0400018D RID: 397
		public const int NoNfkc = 16;

		// Token: 0x0400018E RID: 398
		public const int MaybeNfkc = 32;

		// Token: 0x0400018F RID: 399
		public const int FullCompositionExclusion = 64;

		// Token: 0x04000190 RID: 400
		public const int IsUnsafe = 128;

		// Token: 0x04000191 RID: 401
		private const int HangulSBase = 44032;

		// Token: 0x04000192 RID: 402
		private const int HangulLBase = 4352;

		// Token: 0x04000193 RID: 403
		private const int HangulVBase = 4449;

		// Token: 0x04000194 RID: 404
		private const int HangulTBase = 4519;

		// Token: 0x04000195 RID: 405
		private const int HangulLCount = 19;

		// Token: 0x04000196 RID: 406
		private const int HangulVCount = 21;

		// Token: 0x04000197 RID: 407
		private const int HangulTCount = 28;

		// Token: 0x04000198 RID: 408
		private const int HangulNCount = 588;

		// Token: 0x04000199 RID: 409
		private const int HangulSCount = 11172;

		// Token: 0x0400019A RID: 410
		private unsafe static byte* props;

		// Token: 0x0400019B RID: 411
		private unsafe static int* mappedChars;

		// Token: 0x0400019C RID: 412
		private unsafe static short* charMapIndex;

		// Token: 0x0400019D RID: 413
		private unsafe static short* helperIndex;

		// Token: 0x0400019E RID: 414
		private unsafe static ushort* mapIdxToComposite;

		// Token: 0x0400019F RID: 415
		private unsafe static byte* combiningClass;

		// Token: 0x040001A0 RID: 416
		private static object forLock = new object();

		// Token: 0x040001A1 RID: 417
		public static readonly bool isReady;
	}
}
