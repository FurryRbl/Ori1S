using System;
using System.Collections;
using System.Globalization;
using System.Reflection;
using System.Runtime.InteropServices;

namespace Mono.Globalization.Unicode
{
	// Token: 0x0200007F RID: 127
	internal class MSCompatUnicodeTable
	{
		// Token: 0x06000790 RID: 1936 RVA: 0x000180E4 File Offset: 0x000162E4
		unsafe static MSCompatUnicodeTable()
		{
			IntPtr resource = MSCompatUnicodeTable.GetResource("collation.core.bin");
			if (resource == IntPtr.Zero)
			{
				return;
			}
			byte* ptr = (byte*)((void*)resource);
			resource = MSCompatUnicodeTable.GetResource("collation.tailoring.bin");
			if (resource == IntPtr.Zero)
			{
				return;
			}
			byte* ptr2 = (byte*)((void*)resource);
			if (ptr == null || ptr2 == null)
			{
				return;
			}
			if (*ptr != 3 || *ptr2 != 3)
			{
				return;
			}
			uint num = 1U;
			uint num2 = MSCompatUnicodeTable.UInt32FromBytePtr(ptr, num);
			num += 4U;
			MSCompatUnicodeTable.ignorableFlags = ptr + num;
			num += num2;
			num2 = MSCompatUnicodeTable.UInt32FromBytePtr(ptr, num);
			num += 4U;
			MSCompatUnicodeTable.categories = ptr + num;
			num += num2;
			num2 = MSCompatUnicodeTable.UInt32FromBytePtr(ptr, num);
			num += 4U;
			MSCompatUnicodeTable.level1 = ptr + num;
			num += num2;
			num2 = MSCompatUnicodeTable.UInt32FromBytePtr(ptr, num);
			num += 4U;
			MSCompatUnicodeTable.level2 = ptr + num;
			num += num2;
			num2 = MSCompatUnicodeTable.UInt32FromBytePtr(ptr, num);
			num += 4U;
			MSCompatUnicodeTable.level3 = ptr + num;
			num += num2;
			num = 1U;
			uint num3 = MSCompatUnicodeTable.UInt32FromBytePtr(ptr2, num);
			num += 4U;
			MSCompatUnicodeTable.tailoringInfos = new TailoringInfo[num3];
			int num4 = 0;
			while ((long)num4 < (long)((ulong)num3))
			{
				int lcid = (int)MSCompatUnicodeTable.UInt32FromBytePtr(ptr2, num);
				num += 4U;
				int tailoringIndex = (int)MSCompatUnicodeTable.UInt32FromBytePtr(ptr2, num);
				num += 4U;
				int tailoringCount = (int)MSCompatUnicodeTable.UInt32FromBytePtr(ptr2, num);
				num += 4U;
				TailoringInfo tailoringInfo = new TailoringInfo(lcid, tailoringIndex, tailoringCount, ptr2[(UIntPtr)(num++)] != 0);
				MSCompatUnicodeTable.tailoringInfos[num4] = tailoringInfo;
				num4++;
			}
			num += 2U;
			num3 = MSCompatUnicodeTable.UInt32FromBytePtr(ptr2, num);
			num += 4U;
			MSCompatUnicodeTable.tailoringArr = new char[num3];
			int num5 = 0;
			while ((long)num5 < (long)((ulong)num3))
			{
				MSCompatUnicodeTable.tailoringArr[num5] = (char)((int)ptr2[num] + ((int)ptr2[num + 1U] << 8));
				num5++;
				num += 2U;
			}
			MSCompatUnicodeTable.isReady = true;
		}

		// Token: 0x06000791 RID: 1937 RVA: 0x000182C8 File Offset: 0x000164C8
		public static TailoringInfo GetTailoringInfo(int lcid)
		{
			for (int i = 0; i < MSCompatUnicodeTable.tailoringInfos.Length; i++)
			{
				if (MSCompatUnicodeTable.tailoringInfos[i].LCID == lcid)
				{
					return MSCompatUnicodeTable.tailoringInfos[i];
				}
			}
			return null;
		}

		// Token: 0x06000792 RID: 1938 RVA: 0x00018308 File Offset: 0x00016508
		public unsafe static void BuildTailoringTables(CultureInfo culture, TailoringInfo t, ref Contraction[] contractions, ref Level2Map[] diacriticals)
		{
			ArrayList arrayList = new ArrayList();
			ArrayList arrayList2 = new ArrayList();
			fixed (char* ptr = ref (MSCompatUnicodeTable.tailoringArr != null && MSCompatUnicodeTable.tailoringArr.Length != 0) ? ref MSCompatUnicodeTable.tailoringArr[0] : ref *null)
			{
				int i = t.TailoringIndex;
				int num = i + t.TailoringCount;
				while (i < num)
				{
					int num2 = i + 1;
					switch (ptr[i])
					{
					case '\u0001':
					{
						i++;
						while (ptr[num2] != '\0')
						{
							num2++;
						}
						char[] array = new char[num2 - i];
						Marshal.Copy((IntPtr)((void*)(ptr + i)), array, 0, num2 - i);
						byte[] array2 = new byte[4];
						for (int j = 0; j < 4; j++)
						{
							array2[j] = (byte)ptr[num2 + 1 + j];
						}
						arrayList.Add(new Contraction(array, null, array2));
						i = num2 + 6;
						break;
					}
					case '\u0002':
						arrayList2.Add(new Level2Map((byte)ptr[i + 1], (byte)ptr[i + 2]));
						i += 3;
						break;
					case '\u0003':
					{
						i++;
						while (ptr[num2] != '\0')
						{
							num2++;
						}
						char[] array = new char[num2 - i];
						Marshal.Copy((IntPtr)((void*)(ptr + i)), array, 0, num2 - i);
						num2++;
						int num3 = num2;
						while (ptr[num3] != '\0')
						{
							num3++;
						}
						string replacement = new string(ptr, num2, num3 - num2);
						arrayList.Add(new Contraction(array, replacement, null));
						i = num3 + 1;
						break;
					}
					default:
						throw new NotImplementedException(string.Format("Mono INTERNAL ERROR (Should not happen): Collation tailoring table is broken for culture {0} ({1}) at 0x{2:X}", culture.LCID, culture.Name, i));
					}
				}
			}
			arrayList.Sort(ContractionComparer.Instance);
			arrayList2.Sort(Level2MapComparer.Instance);
			contractions = (arrayList.ToArray(typeof(Contraction)) as Contraction[]);
			diacriticals = (arrayList2.ToArray(typeof(Level2Map)) as Level2Map[]);
		}

		// Token: 0x06000793 RID: 1939 RVA: 0x00018538 File Offset: 0x00016738
		private unsafe static void SetCJKReferences(string name, ref CodePointIndexer cjkIndexer, ref byte* catTable, ref byte* lv1Table, ref CodePointIndexer lv2Indexer, ref byte* lv2Table)
		{
			switch (name)
			{
			case "zh-CHS":
				catTable = MSCompatUnicodeTable.cjkCHScategory;
				lv1Table = MSCompatUnicodeTable.cjkCHSlv1;
				cjkIndexer = MSCompatUnicodeTableUtil.CjkCHS;
				break;
			case "zh-CHT":
				catTable = MSCompatUnicodeTable.cjkCHTcategory;
				lv1Table = MSCompatUnicodeTable.cjkCHTlv1;
				cjkIndexer = MSCompatUnicodeTableUtil.Cjk;
				break;
			case "ja":
				catTable = MSCompatUnicodeTable.cjkJAcategory;
				lv1Table = MSCompatUnicodeTable.cjkJAlv1;
				cjkIndexer = MSCompatUnicodeTableUtil.Cjk;
				break;
			case "ko":
				catTable = MSCompatUnicodeTable.cjkKOcategory;
				lv1Table = MSCompatUnicodeTable.cjkKOlv1;
				lv2Table = MSCompatUnicodeTable.cjkKOlv2;
				cjkIndexer = MSCompatUnicodeTableUtil.Cjk;
				lv2Indexer = MSCompatUnicodeTableUtil.Cjk;
				break;
			}
		}

		// Token: 0x06000794 RID: 1940 RVA: 0x0001863C File Offset: 0x0001683C
		public unsafe static byte Category(int cp)
		{
			return MSCompatUnicodeTable.categories[MSCompatUnicodeTableUtil.Category.ToIndex(cp)];
		}

		// Token: 0x06000795 RID: 1941 RVA: 0x00018650 File Offset: 0x00016850
		public unsafe static byte Level1(int cp)
		{
			return MSCompatUnicodeTable.level1[MSCompatUnicodeTableUtil.Level1.ToIndex(cp)];
		}

		// Token: 0x06000796 RID: 1942 RVA: 0x00018664 File Offset: 0x00016864
		public unsafe static byte Level2(int cp)
		{
			return MSCompatUnicodeTable.level2[MSCompatUnicodeTableUtil.Level2.ToIndex(cp)];
		}

		// Token: 0x06000797 RID: 1943 RVA: 0x00018678 File Offset: 0x00016878
		public unsafe static byte Level3(int cp)
		{
			return MSCompatUnicodeTable.level3[MSCompatUnicodeTableUtil.Level3.ToIndex(cp)];
		}

		// Token: 0x06000798 RID: 1944 RVA: 0x0001868C File Offset: 0x0001688C
		public static bool IsSortable(string s)
		{
			foreach (char cp in s)
			{
				if (!MSCompatUnicodeTable.IsSortable((int)cp))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000799 RID: 1945 RVA: 0x000186C8 File Offset: 0x000168C8
		public static bool IsSortable(int cp)
		{
			return !MSCompatUnicodeTable.IsIgnorable(cp) || cp == 0 || cp == 1600 || cp == 65279 || (6155 <= cp && cp <= 6158) || (8204 <= cp && cp <= 8207) || (8234 <= cp && cp <= 8238) || (8298 <= cp && cp <= 8303) || (8204 <= cp && cp <= 8207) || (65529 <= cp && cp <= 65533);
		}

		// Token: 0x0600079A RID: 1946 RVA: 0x00018794 File Offset: 0x00016994
		public static bool IsIgnorable(int cp)
		{
			return MSCompatUnicodeTable.IsIgnorable(cp, 1);
		}

		// Token: 0x0600079B RID: 1947 RVA: 0x000187A0 File Offset: 0x000169A0
		public unsafe static bool IsIgnorable(int cp, byte flag)
		{
			if (cp == 0)
			{
				return false;
			}
			if ((flag & 1) != 0)
			{
				UnicodeCategory unicodeCategory = char.GetUnicodeCategory((char)cp);
				if (unicodeCategory == UnicodeCategory.OtherNotAssigned)
				{
					return true;
				}
				if (55424 <= cp && cp < 56192)
				{
					return true;
				}
			}
			int num = MSCompatUnicodeTableUtil.Ignorable.ToIndex(cp);
			return num >= 0 && (MSCompatUnicodeTable.ignorableFlags[num] & flag) != 0;
		}

		// Token: 0x0600079C RID: 1948 RVA: 0x00018810 File Offset: 0x00016A10
		public static bool IsIgnorableSymbol(int cp)
		{
			return MSCompatUnicodeTable.IsIgnorable(cp, 2);
		}

		// Token: 0x0600079D RID: 1949 RVA: 0x0001881C File Offset: 0x00016A1C
		public static bool IsIgnorableNonSpacing(int cp)
		{
			return MSCompatUnicodeTable.IsIgnorable(cp, 4);
		}

		// Token: 0x0600079E RID: 1950 RVA: 0x00018828 File Offset: 0x00016A28
		public static int ToKanaTypeInsensitive(int i)
		{
			return (12353 > i || i > 12436) ? i : (i + 96);
		}

		// Token: 0x0600079F RID: 1951 RVA: 0x00018858 File Offset: 0x00016A58
		public static int ToWidthCompat(int i)
		{
			if (i < 8592)
			{
				return i;
			}
			if (i > 65280)
			{
				if (i <= 65374)
				{
					return i - 65280 + 32;
				}
				switch (i)
				{
				case 65504:
					return 162;
				case 65505:
					return 163;
				case 65506:
					return 172;
				case 65507:
					return 175;
				case 65508:
					return 166;
				case 65509:
					return 165;
				case 65510:
					return 8361;
				}
			}
			if (i > 13054)
			{
				return i;
			}
			if (i <= 8595)
			{
				return 56921 + i;
			}
			if (i < 9474)
			{
				return i;
			}
			if (i <= 9675)
			{
				if (i == 9474)
				{
					return 65512;
				}
				if (i == 9632)
				{
					return 65517;
				}
				if (i != 9675)
				{
					return i;
				}
				return 65518;
			}
			else
			{
				if (i < 12288)
				{
					return i;
				}
				if (i < 12593)
				{
					switch (i)
					{
					case 12288:
						return 32;
					case 12289:
						return 65380;
					case 12290:
						return 65377;
					default:
						if (i == 12300)
						{
							return 65378;
						}
						if (i == 12301)
						{
							return 65379;
						}
						if (i != 12539)
						{
							return i;
						}
						return 65381;
					}
				}
				else
				{
					if (i < 12644)
					{
						return i - 12592 + 65440;
					}
					if (i == 12644)
					{
						return 65440;
					}
					return i;
				}
			}
		}

		// Token: 0x060007A0 RID: 1952 RVA: 0x00018A08 File Offset: 0x00016C08
		public static bool HasSpecialWeight(char c)
		{
			if (c < 'ぁ')
			{
				return false;
			}
			if ('ｦ' <= c && c < 'ﾞ')
			{
				return true;
			}
			if ('㌀' <= c)
			{
				return false;
			}
			if (c < 'ゝ')
			{
				return c < '゙';
			}
			if (c < '㄀')
			{
				return c != '・';
			}
			return c >= '㋐' && c < '㋿';
		}

		// Token: 0x060007A1 RID: 1953 RVA: 0x00018A90 File Offset: 0x00016C90
		public static byte GetJapaneseDashType(char c)
		{
			switch (c)
			{
			case 'ー':
				return 5;
			case 'ヽ':
			case 'ヾ':
				break;
			default:
				if (c != 'ゝ' && c != 'ゞ' && c != 'ｰ')
				{
					return 3;
				}
				break;
			}
			return 4;
		}

		// Token: 0x060007A2 RID: 1954 RVA: 0x00018AE4 File Offset: 0x00016CE4
		public static bool IsHalfWidthKana(char c)
		{
			return 'ｦ' <= c && c <= 'ﾝ';
		}

		// Token: 0x060007A3 RID: 1955 RVA: 0x00018B00 File Offset: 0x00016D00
		public static bool IsHiragana(char c)
		{
			return 'ぁ' <= c && c <= 'ゔ';
		}

		// Token: 0x060007A4 RID: 1956 RVA: 0x00018B1C File Offset: 0x00016D1C
		public static bool IsJapaneseSmallLetter(char c)
		{
			if ('ｧ' <= c && c <= 'ｯ')
			{
				return true;
			}
			if ('぀' < c && c < 'ヺ')
			{
				switch (c)
				{
				case 'ぁ':
				case 'ぃ':
				case 'ぅ':
				case 'ぇ':
				case 'ぉ':
					break;
				default:
					switch (c)
					{
					case 'ァ':
					case 'ィ':
					case 'ゥ':
					case 'ェ':
					case 'ォ':
						break;
					default:
						switch (c)
						{
						case 'ゃ':
						case 'ゅ':
						case 'ょ':
							break;
						default:
							switch (c)
							{
							case 'ャ':
							case 'ュ':
							case 'ョ':
								break;
							default:
								if (c != 'ヵ' && c != 'ヶ' && c != 'っ' && c != 'ゎ' && c != 'ッ' && c != 'ヮ')
								{
									return false;
								}
								break;
							}
							break;
						}
						break;
					}
					break;
				}
				return true;
			}
			return false;
		}

		// Token: 0x170000DB RID: 219
		// (get) Token: 0x060007A5 RID: 1957 RVA: 0x00018C44 File Offset: 0x00016E44
		public static bool IsReady
		{
			get
			{
				return MSCompatUnicodeTable.isReady;
			}
		}

		// Token: 0x060007A6 RID: 1958 RVA: 0x00018C4C File Offset: 0x00016E4C
		private static IntPtr GetResource(string name)
		{
			int num;
			Module module;
			return Assembly.GetExecutingAssembly().GetManifestResourceInternal(name, out num, out module);
		}

		// Token: 0x060007A7 RID: 1959 RVA: 0x00018C68 File Offset: 0x00016E68
		private unsafe static uint UInt32FromBytePtr(byte* raw, uint idx)
		{
			return (uint)((int)raw[idx] + ((int)raw[idx + 1U] << 8) + ((int)raw[idx + 2U] << 16) + ((int)raw[idx + 3U] << 24));
		}

		// Token: 0x060007A8 RID: 1960 RVA: 0x00018C90 File Offset: 0x00016E90
		public unsafe static void FillCJK(string culture, ref CodePointIndexer cjkIndexer, ref byte* catTable, ref byte* lv1Table, ref CodePointIndexer lv2Indexer, ref byte* lv2Table)
		{
			object obj = MSCompatUnicodeTable.forLock;
			lock (obj)
			{
				MSCompatUnicodeTable.FillCJKCore(culture, ref cjkIndexer, ref catTable, ref lv1Table, ref lv2Indexer, ref lv2Table);
				MSCompatUnicodeTable.SetCJKReferences(culture, ref cjkIndexer, ref catTable, ref lv1Table, ref lv2Indexer, ref lv2Table);
			}
		}

		// Token: 0x060007A9 RID: 1961 RVA: 0x00018CEC File Offset: 0x00016EEC
		private unsafe static void FillCJKCore(string culture, ref CodePointIndexer cjkIndexer, ref byte* catTable, ref byte* lv1Table, ref CodePointIndexer cjkLv2Indexer, ref byte* lv2Table)
		{
			if (!MSCompatUnicodeTable.IsReady)
			{
				return;
			}
			string text = null;
			switch (culture)
			{
			case "zh-CHS":
				text = "cjkCHS";
				catTable = MSCompatUnicodeTable.cjkCHScategory;
				lv1Table = MSCompatUnicodeTable.cjkCHSlv1;
				break;
			case "zh-CHT":
				text = "cjkCHT";
				catTable = MSCompatUnicodeTable.cjkCHTcategory;
				lv1Table = MSCompatUnicodeTable.cjkCHTlv1;
				break;
			case "ja":
				text = "cjkJA";
				catTable = MSCompatUnicodeTable.cjkJAcategory;
				lv1Table = MSCompatUnicodeTable.cjkJAlv1;
				break;
			case "ko":
				text = "cjkKO";
				catTable = MSCompatUnicodeTable.cjkKOcategory;
				lv1Table = MSCompatUnicodeTable.cjkKOlv1;
				break;
			}
			if (text == null || lv1Table != 0)
			{
				return;
			}
			uint num2 = 0U;
			string name = string.Format("collation.{0}.bin", text);
			IntPtr resource = MSCompatUnicodeTable.GetResource(name);
			if (resource == IntPtr.Zero)
			{
				return;
			}
			byte* ptr = (byte*)((void*)resource);
			num2 += 1U;
			uint num3 = MSCompatUnicodeTable.UInt32FromBytePtr(ptr, num2);
			num2 += 4U;
			catTable = ptr + num2;
			lv1Table = ptr + num2 + num3;
			switch (culture)
			{
			case "zh-CHS":
				MSCompatUnicodeTable.cjkCHScategory = catTable;
				MSCompatUnicodeTable.cjkCHSlv1 = lv1Table;
				break;
			case "zh-CHT":
				MSCompatUnicodeTable.cjkCHTcategory = catTable;
				MSCompatUnicodeTable.cjkCHTlv1 = lv1Table;
				break;
			case "ja":
				MSCompatUnicodeTable.cjkJAcategory = catTable;
				MSCompatUnicodeTable.cjkJAlv1 = lv1Table;
				break;
			case "ko":
				MSCompatUnicodeTable.cjkKOcategory = catTable;
				MSCompatUnicodeTable.cjkKOlv1 = lv1Table;
				break;
			}
			if (text != "cjkKO")
			{
				return;
			}
			resource = MSCompatUnicodeTable.GetResource("collation.cjkKOlv2.bin");
			if (resource == IntPtr.Zero)
			{
				return;
			}
			ptr = (byte*)((void*)resource);
			num2 = 5U;
			MSCompatUnicodeTable.cjkKOlv2 = ptr + num2;
			lv2Table = MSCompatUnicodeTable.cjkKOlv2;
		}

		// Token: 0x04000129 RID: 297
		private const int ResourceVersionSize = 1;

		// Token: 0x0400012A RID: 298
		public static int MaxExpansionLength = 3;

		// Token: 0x0400012B RID: 299
		private unsafe static readonly byte* ignorableFlags;

		// Token: 0x0400012C RID: 300
		private unsafe static readonly byte* categories;

		// Token: 0x0400012D RID: 301
		private unsafe static readonly byte* level1;

		// Token: 0x0400012E RID: 302
		private unsafe static readonly byte* level2;

		// Token: 0x0400012F RID: 303
		private unsafe static readonly byte* level3;

		// Token: 0x04000130 RID: 304
		private unsafe static byte* cjkCHScategory;

		// Token: 0x04000131 RID: 305
		private unsafe static byte* cjkCHTcategory;

		// Token: 0x04000132 RID: 306
		private unsafe static byte* cjkJAcategory;

		// Token: 0x04000133 RID: 307
		private unsafe static byte* cjkKOcategory;

		// Token: 0x04000134 RID: 308
		private unsafe static byte* cjkCHSlv1;

		// Token: 0x04000135 RID: 309
		private unsafe static byte* cjkCHTlv1;

		// Token: 0x04000136 RID: 310
		private unsafe static byte* cjkJAlv1;

		// Token: 0x04000137 RID: 311
		private unsafe static byte* cjkKOlv1;

		// Token: 0x04000138 RID: 312
		private unsafe static byte* cjkKOlv2;

		// Token: 0x04000139 RID: 313
		private static readonly char[] tailoringArr;

		// Token: 0x0400013A RID: 314
		private static readonly TailoringInfo[] tailoringInfos;

		// Token: 0x0400013B RID: 315
		private static object forLock = new object();

		// Token: 0x0400013C RID: 316
		public static readonly bool isReady;
	}
}
