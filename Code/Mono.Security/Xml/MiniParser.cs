using System;
using System.Collections;
using System.Globalization;
using System.Text;

namespace Mono.Xml
{
	// Token: 0x020000BD RID: 189
	[CLSCompliant(false)]
	public class MiniParser
	{
		// Token: 0x060006C5 RID: 1733 RVA: 0x00025D1C File Offset: 0x00023F1C
		public MiniParser()
		{
			this.twoCharBuff = new int[2];
			this.splitCData = false;
			this.Reset();
		}

		// Token: 0x060006C7 RID: 1735 RVA: 0x00025DBC File Offset: 0x00023FBC
		public void Reset()
		{
			this.line = 0;
			this.col = 0;
		}

		// Token: 0x060006C8 RID: 1736 RVA: 0x00025DCC File Offset: 0x00023FCC
		protected static bool StrEquals(string str, StringBuilder sb, int sbStart, int len)
		{
			if (len != str.Length)
			{
				return false;
			}
			for (int i = 0; i < len; i++)
			{
				if (str[i] != sb[sbStart + i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060006C9 RID: 1737 RVA: 0x00025E14 File Offset: 0x00024014
		protected void FatalErr(string descr)
		{
			throw new MiniParser.XMLError(descr, this.line, this.col);
		}

		// Token: 0x060006CA RID: 1738 RVA: 0x00025E28 File Offset: 0x00024028
		protected static int Xlat(int charCode, int state)
		{
			int num = state * MiniParser.INPUT_RANGE;
			int num2 = Math.Min(MiniParser.tbl.Length - num, MiniParser.INPUT_RANGE);
			while (--num2 >= 0)
			{
				ushort num3 = MiniParser.tbl[num];
				if (charCode == num3 >> 12)
				{
					return (int)(num3 & 4095);
				}
				num++;
			}
			return 4095;
		}

		// Token: 0x060006CB RID: 1739 RVA: 0x00025E84 File Offset: 0x00024084
		public void Parse(MiniParser.IReader reader, MiniParser.IHandler handler)
		{
			if (reader == null)
			{
				throw new ArgumentNullException("reader");
			}
			if (handler == null)
			{
				handler = new MiniParser.HandlerAdapter();
			}
			MiniParser.AttrListImpl attrListImpl = new MiniParser.AttrListImpl();
			string text = null;
			Stack stack = new Stack();
			string text2 = null;
			this.line = 1;
			this.col = 0;
			int num = 0;
			int num2 = 0;
			StringBuilder stringBuilder = new StringBuilder();
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			int num3 = 0;
			handler.OnStartParsing(this);
			for (;;)
			{
				this.col++;
				num = reader.Read();
				if (num == -1)
				{
					break;
				}
				int num4 = "<>/?=&'\"![ ]\t\r\n".IndexOf((char)num) & 15;
				if (num4 != 13)
				{
					if (num4 == 12)
					{
						num4 = 10;
					}
					if (num4 == 14)
					{
						this.col = 0;
						this.line++;
						num4 = 10;
					}
					int num5 = MiniParser.Xlat(num4, num2);
					num2 = (num5 & 255);
					if (num != 10 || (num2 != 14 && num2 != 15))
					{
						num5 >>= 8;
						if (num2 >= 128)
						{
							if (num2 == 255)
							{
								this.FatalErr("State dispatch error.");
							}
							else
							{
								this.FatalErr(MiniParser.errors[num2 ^ 128]);
							}
						}
						switch (num5)
						{
						case 0:
							goto IL_19F;
						case 1:
						{
							text2 = stringBuilder.ToString();
							stringBuilder = new StringBuilder();
							string text3 = null;
							if (stack.Count == 0 || text2 != (text3 = (stack.Pop() as string)))
							{
								if (text3 == null)
								{
									this.FatalErr("Tag stack underflow");
								}
								else
								{
									this.FatalErr(string.Format("Expected end tag '{0}' but found '{1}'", text2, text3));
								}
							}
							handler.OnEndElement(text2);
							break;
						}
						case 2:
							text2 = stringBuilder.ToString();
							stringBuilder = new StringBuilder();
							if (num == 47 || num == 62)
							{
								goto IL_19F;
							}
							break;
						case 3:
							text = stringBuilder.ToString();
							stringBuilder = new StringBuilder();
							break;
						case 4:
							if (text == null)
							{
								this.FatalErr("Internal error.");
							}
							attrListImpl.Add(text, stringBuilder.ToString());
							stringBuilder = new StringBuilder();
							text = null;
							break;
						case 5:
							handler.OnChars(stringBuilder.ToString());
							stringBuilder = new StringBuilder();
							break;
						case 6:
						{
							string text4 = "CDATA[";
							flag2 = false;
							flag3 = false;
							if (num == 45)
							{
								num = reader.Read();
								if (num != 45)
								{
									this.FatalErr("Invalid comment");
								}
								this.col++;
								flag2 = true;
								this.twoCharBuff[0] = -1;
								this.twoCharBuff[1] = -1;
							}
							else if (num != 91)
							{
								flag3 = true;
								num3 = 0;
							}
							else
							{
								for (int i = 0; i < text4.Length; i++)
								{
									if (reader.Read() != (int)text4[i])
									{
										this.col += i + 1;
										break;
									}
								}
								this.col += text4.Length;
								flag = true;
							}
							break;
						}
						case 7:
						{
							int num6 = 0;
							num = 93;
							while (num == 93)
							{
								num = reader.Read();
								num6++;
							}
							if (num != 62)
							{
								for (int j = 0; j < num6; j++)
								{
									stringBuilder.Append(']');
								}
								stringBuilder.Append((char)num);
								num2 = 18;
							}
							else
							{
								for (int k = 0; k < num6 - 2; k++)
								{
									stringBuilder.Append(']');
								}
								flag = false;
							}
							this.col += num6;
							break;
						}
						case 8:
							this.FatalErr(string.Format("Error {0}", num2));
							break;
						case 9:
							break;
						case 10:
							stringBuilder = new StringBuilder();
							if (num != 60)
							{
								goto IL_465;
							}
							break;
						case 11:
							goto IL_465;
						case 12:
							if (flag2)
							{
								if (num == 62 && this.twoCharBuff[0] == 45 && this.twoCharBuff[1] == 45)
								{
									flag2 = false;
									num2 = 0;
								}
								else
								{
									this.twoCharBuff[0] = this.twoCharBuff[1];
									this.twoCharBuff[1] = num;
								}
							}
							else if (flag3)
							{
								if (num == 60 || num == 62)
								{
									num3 ^= 1;
								}
								if (num == 62 && num3 != 0)
								{
									flag3 = false;
									num2 = 0;
								}
							}
							else
							{
								if (this.splitCData && stringBuilder.Length > 0 && flag)
								{
									handler.OnChars(stringBuilder.ToString());
									stringBuilder = new StringBuilder();
								}
								flag = false;
								stringBuilder.Append((char)num);
							}
							break;
						case 13:
						{
							num = reader.Read();
							int num7 = this.col + 1;
							if (num == 35)
							{
								int num8 = 10;
								int num9 = 0;
								int num10 = 0;
								num = reader.Read();
								num7++;
								if (num == 120)
								{
									num = reader.Read();
									num7++;
									num8 = 16;
								}
								NumberStyles style = (num8 != 16) ? NumberStyles.Integer : NumberStyles.HexNumber;
								for (;;)
								{
									int num11 = -1;
									if (char.IsNumber((char)num))
									{
										goto IL_5D9;
									}
									if ("abcdef".IndexOf(char.ToLower((char)num)) != -1)
									{
										goto Block_43;
									}
									IL_5F9:
									if (num11 == -1)
									{
										break;
									}
									num9 *= num8;
									num9 += num11;
									num10++;
									num = reader.Read();
									num7++;
									continue;
									Block_43:
									try
									{
										IL_5D9:
										num11 = int.Parse(new string((char)num, 1), style);
									}
									catch (FormatException)
									{
										num11 = -1;
									}
									goto IL_5F9;
								}
								if (num == 59 && num10 > 0)
								{
									stringBuilder.Append((char)num9);
								}
								else
								{
									this.FatalErr("Bad char ref");
								}
							}
							else
							{
								string text5 = "aglmopqstu";
								string text6 = "&'\"><";
								int num12 = 0;
								int num13 = 15;
								int num14 = 0;
								int length = stringBuilder.Length;
								for (;;)
								{
									if (num12 != 15)
									{
										num12 = (text5.IndexOf((char)num) & 15);
									}
									if (num12 == 15)
									{
										this.FatalErr(MiniParser.errors[7]);
									}
									stringBuilder.Append((char)num);
									int num15 = (int)"Ｕ㾏侏ཟｸ⊙ｏ"[num12];
									int num16 = num15 >> 4 & 15;
									int num17 = num15 & 15;
									int num18 = num15 >> 12;
									int num19 = num15 >> 8 & 15;
									num = reader.Read();
									num7++;
									num12 = 15;
									if (num16 != 15 && num == (int)text5[num16])
									{
										if (num18 < 14)
										{
											num13 = num18;
										}
										num14 = 12;
									}
									else if (num17 != 15 && num == (int)text5[num17])
									{
										if (num19 < 14)
										{
											num13 = num19;
										}
										num14 = 8;
									}
									else if (num == 59)
									{
										if (num13 != 15 && num14 != 0 && (num15 >> num14 & 15) == 14)
										{
											break;
										}
										continue;
									}
									num12 = 0;
								}
								int num20 = num7 - this.col - 1;
								if (num20 > 0 && num20 < 5 && (MiniParser.StrEquals("amp", stringBuilder, length, num20) || MiniParser.StrEquals("apos", stringBuilder, length, num20) || MiniParser.StrEquals("quot", stringBuilder, length, num20) || MiniParser.StrEquals("lt", stringBuilder, length, num20) || MiniParser.StrEquals("gt", stringBuilder, length, num20)))
								{
									stringBuilder.Length = length;
									stringBuilder.Append(text6[num13]);
								}
								else
								{
									this.FatalErr(MiniParser.errors[7]);
								}
							}
							this.col = num7;
							break;
						}
						default:
							this.FatalErr(string.Format("Unexpected action code - {0}.", num5));
							break;
						}
						continue;
						IL_19F:
						handler.OnStartElement(text2, attrListImpl);
						if (num != 47)
						{
							stack.Push(text2);
						}
						else
						{
							handler.OnEndElement(text2);
						}
						attrListImpl.Clear();
						continue;
						IL_465:
						stringBuilder.Append((char)num);
					}
				}
			}
			if (num2 != 0)
			{
				this.FatalErr("Unexpected EOF");
			}
			handler.OnEndParsing(this);
		}

		// Token: 0x04000331 RID: 817
		private static readonly int INPUT_RANGE = 13;

		// Token: 0x04000332 RID: 818
		private static readonly ushort[] tbl = new ushort[]
		{
			2305,
			43264,
			63616,
			10368,
			6272,
			14464,
			18560,
			22656,
			26752,
			34944,
			39040,
			47232,
			30848,
			2177,
			10498,
			6277,
			14595,
			18561,
			22657,
			26753,
			35088,
			39041,
			43137,
			47233,
			30849,
			64004,
			4352,
			43266,
			64258,
			2177,
			10369,
			14465,
			18561,
			22657,
			26753,
			34945,
			39041,
			47233,
			30849,
			14597,
			2307,
			10499,
			6403,
			18691,
			22787,
			26883,
			35075,
			39171,
			43267,
			47363,
			30979,
			63747,
			64260,
			8710,
			4615,
			41480,
			2177,
			14465,
			18561,
			22657,
			26753,
			34945,
			39041,
			47233,
			30849,
			6400,
			2307,
			10499,
			14595,
			18691,
			22787,
			26883,
			35075,
			39171,
			43267,
			47363,
			30979,
			63747,
			6400,
			2177,
			10369,
			14465,
			18561,
			22657,
			26753,
			34945,
			39041,
			43137,
			47233,
			30849,
			63617,
			2561,
			23818,
			11274,
			7178,
			15370,
			19466,
			27658,
			35850,
			39946,
			43783,
			48138,
			31754,
			64522,
			64265,
			8198,
			4103,
			43272,
			2177,
			14465,
			18561,
			22657,
			26753,
			34945,
			39041,
			47233,
			30849,
			64265,
			17163,
			43276,
			2178,
			10370,
			6274,
			14466,
			22658,
			26754,
			34946,
			39042,
			47234,
			30850,
			2317,
			23818,
			11274,
			7178,
			15370,
			19466,
			27658,
			35850,
			39946,
			44042,
			48138,
			31754,
			64522,
			26894,
			30991,
			43275,
			2180,
			10372,
			6276,
			14468,
			18564,
			22660,
			34948,
			39044,
			47236,
			63620,
			17163,
			43276,
			2178,
			10370,
			6274,
			14466,
			22658,
			26754,
			34946,
			39042,
			47234,
			30850,
			63618,
			9474,
			35088,
			2182,
			6278,
			14470,
			18566,
			22662,
			26758,
			39046,
			43142,
			47238,
			30854,
			63622,
			25617,
			23822,
			2830,
			11022,
			6926,
			15118,
			19214,
			35598,
			39694,
			43790,
			47886,
			31502,
			64270,
			29713,
			23823,
			2831,
			11023,
			6927,
			15119,
			19215,
			27407,
			35599,
			39695,
			43791,
			47887,
			64271,
			38418,
			6400,
			1555,
			9747,
			13843,
			17939,
			22035,
			26131,
			34323,
			42515,
			46611,
			30227,
			62995,
			8198,
			4103,
			43281,
			64265,
			2177,
			14465,
			18561,
			22657,
			26753,
			34945,
			39041,
			47233,
			30849,
			46858,
			3090,
			11282,
			7186,
			15378,
			19474,
			23570,
			27666,
			35858,
			39954,
			44050,
			31762,
			64530,
			3091,
			11283,
			7187,
			15379,
			19475,
			23571,
			27667,
			35859,
			39955,
			44051,
			48147,
			31763,
			64531,
			ushort.MaxValue,
			ushort.MaxValue
		};

		// Token: 0x04000333 RID: 819
		protected static string[] errors = new string[]
		{
			"Expected element",
			"Invalid character in tag",
			"No '='",
			"Invalid character entity",
			"Invalid attr value",
			"Empty tag",
			"No end tag",
			"Bad entity ref"
		};

		// Token: 0x04000334 RID: 820
		protected int line;

		// Token: 0x04000335 RID: 821
		protected int col;

		// Token: 0x04000336 RID: 822
		protected int[] twoCharBuff;

		// Token: 0x04000337 RID: 823
		protected bool splitCData;

		// Token: 0x020000BE RID: 190
		public interface IReader
		{
			// Token: 0x060006CC RID: 1740
			int Read();
		}

		// Token: 0x020000BF RID: 191
		public interface IAttrList
		{
			// Token: 0x170001A4 RID: 420
			// (get) Token: 0x060006CD RID: 1741
			int Length { get; }

			// Token: 0x170001A5 RID: 421
			// (get) Token: 0x060006CE RID: 1742
			bool IsEmpty { get; }

			// Token: 0x060006CF RID: 1743
			string GetName(int i);

			// Token: 0x060006D0 RID: 1744
			string GetValue(int i);

			// Token: 0x060006D1 RID: 1745
			string GetValue(string name);

			// Token: 0x060006D2 RID: 1746
			void ChangeValue(string name, string newValue);

			// Token: 0x170001A6 RID: 422
			// (get) Token: 0x060006D3 RID: 1747
			string[] Names { get; }

			// Token: 0x170001A7 RID: 423
			// (get) Token: 0x060006D4 RID: 1748
			string[] Values { get; }
		}

		// Token: 0x020000C0 RID: 192
		public interface IMutableAttrList : MiniParser.IAttrList
		{
			// Token: 0x060006D5 RID: 1749
			void Clear();

			// Token: 0x060006D6 RID: 1750
			void Add(string name, string value);

			// Token: 0x060006D7 RID: 1751
			void CopyFrom(MiniParser.IAttrList attrs);

			// Token: 0x060006D8 RID: 1752
			void Remove(int i);

			// Token: 0x060006D9 RID: 1753
			void Remove(string name);
		}

		// Token: 0x020000C1 RID: 193
		public interface IHandler
		{
			// Token: 0x060006DA RID: 1754
			void OnStartParsing(MiniParser parser);

			// Token: 0x060006DB RID: 1755
			void OnStartElement(string name, MiniParser.IAttrList attrs);

			// Token: 0x060006DC RID: 1756
			void OnEndElement(string name);

			// Token: 0x060006DD RID: 1757
			void OnChars(string ch);

			// Token: 0x060006DE RID: 1758
			void OnEndParsing(MiniParser parser);
		}

		// Token: 0x020000C2 RID: 194
		public class HandlerAdapter : MiniParser.IHandler
		{
			// Token: 0x060006E0 RID: 1760 RVA: 0x00026730 File Offset: 0x00024930
			public void OnStartParsing(MiniParser parser)
			{
			}

			// Token: 0x060006E1 RID: 1761 RVA: 0x00026734 File Offset: 0x00024934
			public void OnStartElement(string name, MiniParser.IAttrList attrs)
			{
			}

			// Token: 0x060006E2 RID: 1762 RVA: 0x00026738 File Offset: 0x00024938
			public void OnEndElement(string name)
			{
			}

			// Token: 0x060006E3 RID: 1763 RVA: 0x0002673C File Offset: 0x0002493C
			public void OnChars(string ch)
			{
			}

			// Token: 0x060006E4 RID: 1764 RVA: 0x00026740 File Offset: 0x00024940
			public void OnEndParsing(MiniParser parser)
			{
			}
		}

		// Token: 0x020000C3 RID: 195
		private enum CharKind : byte
		{
			// Token: 0x04000339 RID: 825
			LEFT_BR,
			// Token: 0x0400033A RID: 826
			RIGHT_BR,
			// Token: 0x0400033B RID: 827
			SLASH,
			// Token: 0x0400033C RID: 828
			PI_MARK,
			// Token: 0x0400033D RID: 829
			EQ,
			// Token: 0x0400033E RID: 830
			AMP,
			// Token: 0x0400033F RID: 831
			SQUOTE,
			// Token: 0x04000340 RID: 832
			DQUOTE,
			// Token: 0x04000341 RID: 833
			BANG,
			// Token: 0x04000342 RID: 834
			LEFT_SQBR,
			// Token: 0x04000343 RID: 835
			SPACE,
			// Token: 0x04000344 RID: 836
			RIGHT_SQBR,
			// Token: 0x04000345 RID: 837
			TAB,
			// Token: 0x04000346 RID: 838
			CR,
			// Token: 0x04000347 RID: 839
			EOL,
			// Token: 0x04000348 RID: 840
			CHARS,
			// Token: 0x04000349 RID: 841
			UNKNOWN = 31
		}

		// Token: 0x020000C4 RID: 196
		private enum ActionCode : byte
		{
			// Token: 0x0400034B RID: 843
			START_ELEM,
			// Token: 0x0400034C RID: 844
			END_ELEM,
			// Token: 0x0400034D RID: 845
			END_NAME,
			// Token: 0x0400034E RID: 846
			SET_ATTR_NAME,
			// Token: 0x0400034F RID: 847
			SET_ATTR_VAL,
			// Token: 0x04000350 RID: 848
			SEND_CHARS,
			// Token: 0x04000351 RID: 849
			START_CDATA,
			// Token: 0x04000352 RID: 850
			END_CDATA,
			// Token: 0x04000353 RID: 851
			ERROR,
			// Token: 0x04000354 RID: 852
			STATE_CHANGE,
			// Token: 0x04000355 RID: 853
			FLUSH_CHARS_STATE_CHANGE,
			// Token: 0x04000356 RID: 854
			ACC_CHARS_STATE_CHANGE,
			// Token: 0x04000357 RID: 855
			ACC_CDATA,
			// Token: 0x04000358 RID: 856
			PROC_CHAR_REF,
			// Token: 0x04000359 RID: 857
			UNKNOWN = 15
		}

		// Token: 0x020000C5 RID: 197
		public class AttrListImpl : MiniParser.IAttrList, MiniParser.IMutableAttrList
		{
			// Token: 0x060006E5 RID: 1765 RVA: 0x00026744 File Offset: 0x00024944
			public AttrListImpl() : this(0)
			{
			}

			// Token: 0x060006E6 RID: 1766 RVA: 0x00026750 File Offset: 0x00024950
			public AttrListImpl(int initialCapacity)
			{
				if (initialCapacity <= 0)
				{
					this.names = new ArrayList();
					this.values = new ArrayList();
				}
				else
				{
					this.names = new ArrayList(initialCapacity);
					this.values = new ArrayList(initialCapacity);
				}
			}

			// Token: 0x060006E7 RID: 1767 RVA: 0x000267A0 File Offset: 0x000249A0
			public AttrListImpl(MiniParser.IAttrList attrs) : this((attrs == null) ? 0 : attrs.Length)
			{
				if (attrs != null)
				{
					this.CopyFrom(attrs);
				}
			}

			// Token: 0x170001A8 RID: 424
			// (get) Token: 0x060006E8 RID: 1768 RVA: 0x000267C8 File Offset: 0x000249C8
			public int Length
			{
				get
				{
					return this.names.Count;
				}
			}

			// Token: 0x170001A9 RID: 425
			// (get) Token: 0x060006E9 RID: 1769 RVA: 0x000267D8 File Offset: 0x000249D8
			public bool IsEmpty
			{
				get
				{
					return this.Length != 0;
				}
			}

			// Token: 0x060006EA RID: 1770 RVA: 0x000267E8 File Offset: 0x000249E8
			public string GetName(int i)
			{
				string result = null;
				if (i >= 0 && i < this.Length)
				{
					result = (this.names[i] as string);
				}
				return result;
			}

			// Token: 0x060006EB RID: 1771 RVA: 0x00026820 File Offset: 0x00024A20
			public string GetValue(int i)
			{
				string result = null;
				if (i >= 0 && i < this.Length)
				{
					result = (this.values[i] as string);
				}
				return result;
			}

			// Token: 0x060006EC RID: 1772 RVA: 0x00026858 File Offset: 0x00024A58
			public string GetValue(string name)
			{
				return this.GetValue(this.names.IndexOf(name));
			}

			// Token: 0x060006ED RID: 1773 RVA: 0x0002686C File Offset: 0x00024A6C
			public void ChangeValue(string name, string newValue)
			{
				int num = this.names.IndexOf(name);
				if (num >= 0 && num < this.Length)
				{
					this.values[num] = newValue;
				}
			}

			// Token: 0x170001AA RID: 426
			// (get) Token: 0x060006EE RID: 1774 RVA: 0x000268A8 File Offset: 0x00024AA8
			public string[] Names
			{
				get
				{
					return this.names.ToArray(typeof(string)) as string[];
				}
			}

			// Token: 0x170001AB RID: 427
			// (get) Token: 0x060006EF RID: 1775 RVA: 0x000268C4 File Offset: 0x00024AC4
			public string[] Values
			{
				get
				{
					return this.values.ToArray(typeof(string)) as string[];
				}
			}

			// Token: 0x060006F0 RID: 1776 RVA: 0x000268E0 File Offset: 0x00024AE0
			public void Clear()
			{
				this.names.Clear();
				this.values.Clear();
			}

			// Token: 0x060006F1 RID: 1777 RVA: 0x000268F8 File Offset: 0x00024AF8
			public void Add(string name, string value)
			{
				this.names.Add(name);
				this.values.Add(value);
			}

			// Token: 0x060006F2 RID: 1778 RVA: 0x00026914 File Offset: 0x00024B14
			public void Remove(int i)
			{
				if (i >= 0)
				{
					this.names.RemoveAt(i);
					this.values.RemoveAt(i);
				}
			}

			// Token: 0x060006F3 RID: 1779 RVA: 0x00026938 File Offset: 0x00024B38
			public void Remove(string name)
			{
				this.Remove(this.names.IndexOf(name));
			}

			// Token: 0x060006F4 RID: 1780 RVA: 0x0002694C File Offset: 0x00024B4C
			public void CopyFrom(MiniParser.IAttrList attrs)
			{
				if (attrs != null && this == attrs)
				{
					this.Clear();
					int length = attrs.Length;
					for (int i = 0; i < length; i++)
					{
						this.Add(attrs.GetName(i), attrs.GetValue(i));
					}
				}
			}

			// Token: 0x0400035A RID: 858
			protected ArrayList names;

			// Token: 0x0400035B RID: 859
			protected ArrayList values;
		}

		// Token: 0x020000C6 RID: 198
		public class XMLError : Exception
		{
			// Token: 0x060006F5 RID: 1781 RVA: 0x0002699C File Offset: 0x00024B9C
			public XMLError() : this("Unknown")
			{
			}

			// Token: 0x060006F6 RID: 1782 RVA: 0x000269AC File Offset: 0x00024BAC
			public XMLError(string descr) : this(descr, -1, -1)
			{
			}

			// Token: 0x060006F7 RID: 1783 RVA: 0x000269B8 File Offset: 0x00024BB8
			public XMLError(string descr, int line, int column) : base(descr)
			{
				this.descr = descr;
				this.line = line;
				this.column = column;
			}

			// Token: 0x170001AC RID: 428
			// (get) Token: 0x060006F8 RID: 1784 RVA: 0x000269D8 File Offset: 0x00024BD8
			public int Line
			{
				get
				{
					return this.line;
				}
			}

			// Token: 0x170001AD RID: 429
			// (get) Token: 0x060006F9 RID: 1785 RVA: 0x000269E0 File Offset: 0x00024BE0
			public int Column
			{
				get
				{
					return this.column;
				}
			}

			// Token: 0x060006FA RID: 1786 RVA: 0x000269E8 File Offset: 0x00024BE8
			public override string ToString()
			{
				return string.Format("{0} @ (line = {1}, col = {2})", this.descr, this.line, this.column);
			}

			// Token: 0x0400035C RID: 860
			protected string descr;

			// Token: 0x0400035D RID: 861
			protected int line;

			// Token: 0x0400035E RID: 862
			protected int column;
		}
	}
}
