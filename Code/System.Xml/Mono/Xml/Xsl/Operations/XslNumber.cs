using System;
using System.Collections;
using System.Globalization;
using System.Text;
using System.Xml.XPath;
using System.Xml.Xsl;
using Mono.Xml.XPath;

namespace Mono.Xml.Xsl.Operations
{
	// Token: 0x02000062 RID: 98
	internal class XslNumber : XslCompiledElement
	{
		// Token: 0x0600037E RID: 894 RVA: 0x00017DE8 File Offset: 0x00015FE8
		public XslNumber(Compiler c) : base(c)
		{
		}

		// Token: 0x0600037F RID: 895 RVA: 0x00017DF4 File Offset: 0x00015FF4
		public static double Round(double n)
		{
			double num = Math.Floor(n);
			return (n - num < 0.5) ? num : (num + 1.0);
		}

		// Token: 0x06000380 RID: 896 RVA: 0x00017E2C File Offset: 0x0001602C
		protected override void Compile(Compiler c)
		{
			if (c.Debugger != null)
			{
				c.Debugger.DebugCompile(base.DebugInput);
			}
			c.CheckExtraAttributes("number", new string[]
			{
				"level",
				"count",
				"from",
				"value",
				"format",
				"lang",
				"letter-value",
				"grouping-separator",
				"grouping-size"
			});
			string attribute = c.GetAttribute("level");
			switch (attribute)
			{
			case "single":
				this.level = XslNumberingLevel.Single;
				goto IL_12C;
			case "multiple":
				this.level = XslNumberingLevel.Multiple;
				goto IL_12C;
			case "any":
				this.level = XslNumberingLevel.Any;
				goto IL_12C;
			}
			this.level = XslNumberingLevel.Single;
			IL_12C:
			this.count = c.CompilePattern(c.GetAttribute("count"), c.Input);
			this.from = c.CompilePattern(c.GetAttribute("from"), c.Input);
			this.value = c.CompileExpression(c.GetAttribute("value"));
			this.format = c.ParseAvtAttribute("format");
			this.lang = c.ParseAvtAttribute("lang");
			this.letterValue = c.ParseAvtAttribute("letter-value");
			this.groupingSeparator = c.ParseAvtAttribute("grouping-separator");
			this.groupingSize = c.ParseAvtAttribute("grouping-size");
		}

		// Token: 0x06000381 RID: 897 RVA: 0x0001800C File Offset: 0x0001620C
		public override void Evaluate(XslTransformProcessor p)
		{
			if (p.Debugger != null)
			{
				p.Debugger.DebugExecute(p, base.DebugInput);
			}
			string text = this.GetFormat(p);
			if (text != string.Empty)
			{
				p.Out.WriteString(text);
			}
		}

		// Token: 0x06000382 RID: 898 RVA: 0x0001805C File Offset: 0x0001625C
		private XslNumber.XslNumberFormatter GetNumberFormatter(XslTransformProcessor p)
		{
			string text = "1";
			string text2 = null;
			string text3 = null;
			char c = '\0';
			decimal d = 0m;
			if (this.format != null)
			{
				text = this.format.Evaluate(p);
			}
			if (this.lang != null)
			{
				text2 = this.lang.Evaluate(p);
			}
			if (this.letterValue != null)
			{
				text3 = this.letterValue.Evaluate(p);
			}
			if (this.groupingSeparator != null)
			{
				c = this.groupingSeparator.Evaluate(p)[0];
			}
			if (this.groupingSize != null)
			{
				d = decimal.Parse(this.groupingSize.Evaluate(p), CultureInfo.InvariantCulture);
			}
			if (d > 2147483647m || d < 1m)
			{
				d = 0m;
			}
			return new XslNumber.XslNumberFormatter(text, text2, text3, c, (int)d);
		}

		// Token: 0x06000383 RID: 899 RVA: 0x00018148 File Offset: 0x00016348
		private string GetFormat(XslTransformProcessor p)
		{
			XslNumber.XslNumberFormatter numberFormatter = this.GetNumberFormatter(p);
			if (this.value != null)
			{
				double num = p.EvaluateNumber(this.value);
				return numberFormatter.Format(num);
			}
			switch (this.level)
			{
			case XslNumberingLevel.Single:
			{
				int num2 = this.NumberSingle(p);
				return numberFormatter.Format((double)num2, num2 != 0);
			}
			case XslNumberingLevel.Multiple:
				return numberFormatter.Format(this.NumberMultiple(p));
			case XslNumberingLevel.Any:
			{
				int num2 = this.NumberAny(p);
				return numberFormatter.Format((double)num2, num2 != 0);
			}
			default:
				throw new XsltException("Should not get here", null, p.CurrentNode);
			}
		}

		// Token: 0x06000384 RID: 900 RVA: 0x000181EC File Offset: 0x000163EC
		private int[] NumberMultiple(XslTransformProcessor p)
		{
			ArrayList arrayList = new ArrayList();
			XPathNavigator xpathNavigator = p.CurrentNode.Clone();
			bool flag = false;
			while (!this.MatchesFrom(xpathNavigator, p))
			{
				if (this.MatchesCount(xpathNavigator, p))
				{
					int num = 1;
					while (xpathNavigator.MoveToPrevious())
					{
						if (this.MatchesCount(xpathNavigator, p))
						{
							num++;
						}
					}
					arrayList.Add(num);
				}
				if (!xpathNavigator.MoveToParent())
				{
					IL_70:
					if (!flag)
					{
						return new int[0];
					}
					int[] array = new int[arrayList.Count];
					int num2 = arrayList.Count;
					for (int i = 0; i < arrayList.Count; i++)
					{
						array[--num2] = (int)arrayList[i];
					}
					return array;
				}
			}
			flag = true;
			goto IL_70;
		}

		// Token: 0x06000385 RID: 901 RVA: 0x000182C0 File Offset: 0x000164C0
		private int NumberAny(XslTransformProcessor p)
		{
			int num = 0;
			XPathNavigator xpathNavigator = p.CurrentNode.Clone();
			xpathNavigator.MoveToRoot();
			bool flag = this.from == null;
			for (;;)
			{
				if (this.from != null && this.MatchesFrom(xpathNavigator, p))
				{
					flag = true;
					num = 0;
				}
				else if (flag && this.MatchesCount(xpathNavigator, p))
				{
					num++;
				}
				if (xpathNavigator.IsSamePosition(p.CurrentNode))
				{
					break;
				}
				if (!xpathNavigator.MoveToFirstChild())
				{
					while (!xpathNavigator.MoveToNext())
					{
						if (!xpathNavigator.MoveToParent())
						{
							return 0;
						}
					}
				}
			}
			return num;
		}

		// Token: 0x06000386 RID: 902 RVA: 0x00018364 File Offset: 0x00016564
		private int NumberSingle(XslTransformProcessor p)
		{
			XPathNavigator xpathNavigator = p.CurrentNode.Clone();
			while (!this.MatchesCount(xpathNavigator, p))
			{
				if (this.from != null && this.MatchesFrom(xpathNavigator, p))
				{
					return 0;
				}
				if (!xpathNavigator.MoveToParent())
				{
					return 0;
				}
			}
			if (this.from != null)
			{
				XPathNavigator xpathNavigator2 = xpathNavigator.Clone();
				if (this.MatchesFrom(xpathNavigator2, p))
				{
					return 0;
				}
				bool flag = false;
				while (xpathNavigator2.MoveToParent())
				{
					if (this.MatchesFrom(xpathNavigator2, p))
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					return 0;
				}
			}
			int num = 1;
			while (xpathNavigator.MoveToPrevious())
			{
				if (this.MatchesCount(xpathNavigator, p))
				{
					num++;
				}
			}
			return num;
		}

		// Token: 0x06000387 RID: 903 RVA: 0x0001842C File Offset: 0x0001662C
		private bool MatchesCount(XPathNavigator item, XslTransformProcessor p)
		{
			if (this.count == null)
			{
				return item.NodeType == p.CurrentNode.NodeType && item.LocalName == p.CurrentNode.LocalName && item.NamespaceURI == p.CurrentNode.NamespaceURI;
			}
			return p.Matches(this.count, item);
		}

		// Token: 0x06000388 RID: 904 RVA: 0x0001849C File Offset: 0x0001669C
		private bool MatchesFrom(XPathNavigator item, XslTransformProcessor p)
		{
			if (this.from == null)
			{
				return item.NodeType == XPathNodeType.Root;
			}
			return p.Matches(this.from, item);
		}

		// Token: 0x04000271 RID: 625
		private XslNumberingLevel level;

		// Token: 0x04000272 RID: 626
		private Pattern count;

		// Token: 0x04000273 RID: 627
		private Pattern from;

		// Token: 0x04000274 RID: 628
		private XPathExpression value;

		// Token: 0x04000275 RID: 629
		private XslAvt format;

		// Token: 0x04000276 RID: 630
		private XslAvt lang;

		// Token: 0x04000277 RID: 631
		private XslAvt letterValue;

		// Token: 0x04000278 RID: 632
		private XslAvt groupingSeparator;

		// Token: 0x04000279 RID: 633
		private XslAvt groupingSize;

		// Token: 0x02000063 RID: 99
		private class XslNumberFormatter
		{
			// Token: 0x06000389 RID: 905 RVA: 0x000184CC File Offset: 0x000166CC
			public XslNumberFormatter(string format, string lang, string letterValue, char groupingSeparator, int groupingSize)
			{
				if (format == null || format == string.Empty)
				{
					this.fmtList.Add(XslNumber.XslNumberFormatter.FormatItem.GetItem(null, "1", groupingSeparator, groupingSize));
				}
				else
				{
					XslNumber.XslNumberFormatter.NumberFormatterScanner numberFormatterScanner = new XslNumber.XslNumberFormatter.NumberFormatterScanner(format);
					this.firstSep = numberFormatterScanner.Advance(false);
					string text = numberFormatterScanner.Advance(true);
					if (text == null)
					{
						string sep = this.firstSep;
						this.firstSep = null;
						this.fmtList.Add(XslNumber.XslNumberFormatter.FormatItem.GetItem(sep, "1", groupingSeparator, groupingSize));
					}
					else
					{
						this.fmtList.Add(XslNumber.XslNumberFormatter.FormatItem.GetItem(".", text, groupingSeparator, groupingSize));
						string sep;
						for (;;)
						{
							sep = numberFormatterScanner.Advance(false);
							text = numberFormatterScanner.Advance(true);
							if (text == null)
							{
								break;
							}
							this.fmtList.Add(XslNumber.XslNumberFormatter.FormatItem.GetItem(sep, text, groupingSeparator, groupingSize));
							if (text == null)
							{
								return;
							}
						}
						this.lastSep = sep;
					}
				}
			}

			// Token: 0x0600038A RID: 906 RVA: 0x000185D0 File Offset: 0x000167D0
			public string Format(double value)
			{
				return this.Format(value, true);
			}

			// Token: 0x0600038B RID: 907 RVA: 0x000185DC File Offset: 0x000167DC
			public string Format(double value, bool formatContent)
			{
				StringBuilder stringBuilder = new StringBuilder();
				if (this.firstSep != null)
				{
					stringBuilder.Append(this.firstSep);
				}
				if (formatContent)
				{
					((XslNumber.XslNumberFormatter.FormatItem)this.fmtList[0]).Format(stringBuilder, value);
				}
				if (this.lastSep != null)
				{
					stringBuilder.Append(this.lastSep);
				}
				return stringBuilder.ToString();
			}

			// Token: 0x0600038C RID: 908 RVA: 0x00018644 File Offset: 0x00016844
			public string Format(int[] values)
			{
				StringBuilder stringBuilder = new StringBuilder();
				if (this.firstSep != null)
				{
					stringBuilder.Append(this.firstSep);
				}
				int num = 0;
				int num2 = this.fmtList.Count - 1;
				if (values.Length > 0)
				{
					if (this.fmtList.Count > 0)
					{
						XslNumber.XslNumberFormatter.FormatItem formatItem = (XslNumber.XslNumberFormatter.FormatItem)this.fmtList[num];
						formatItem.Format(stringBuilder, (double)values[0]);
					}
					if (num < num2)
					{
						num++;
					}
				}
				for (int i = 1; i < values.Length; i++)
				{
					XslNumber.XslNumberFormatter.FormatItem formatItem2 = (XslNumber.XslNumberFormatter.FormatItem)this.fmtList[num];
					stringBuilder.Append(formatItem2.sep);
					int num3 = values[i];
					formatItem2.Format(stringBuilder, (double)num3);
					if (num < num2)
					{
						num++;
					}
				}
				if (this.lastSep != null)
				{
					stringBuilder.Append(this.lastSep);
				}
				return stringBuilder.ToString();
			}

			// Token: 0x0400027B RID: 635
			private string firstSep;

			// Token: 0x0400027C RID: 636
			private string lastSep;

			// Token: 0x0400027D RID: 637
			private ArrayList fmtList = new ArrayList();

			// Token: 0x02000064 RID: 100
			private class NumberFormatterScanner
			{
				// Token: 0x0600038D RID: 909 RVA: 0x00018734 File Offset: 0x00016934
				public NumberFormatterScanner(string fmt)
				{
					this.fmt = fmt;
					this.len = fmt.Length;
				}

				// Token: 0x0600038E RID: 910 RVA: 0x00018750 File Offset: 0x00016950
				public string Advance(bool alphaNum)
				{
					int num = this.pos;
					while (this.pos < this.len && char.IsLetterOrDigit(this.fmt, this.pos) == alphaNum)
					{
						this.pos++;
					}
					if (this.pos == num)
					{
						return null;
					}
					return this.fmt.Substring(num, this.pos - num);
				}

				// Token: 0x0400027E RID: 638
				private int pos;

				// Token: 0x0400027F RID: 639
				private int len;

				// Token: 0x04000280 RID: 640
				private string fmt;
			}

			// Token: 0x02000065 RID: 101
			private abstract class FormatItem
			{
				// Token: 0x0600038F RID: 911 RVA: 0x000187C4 File Offset: 0x000169C4
				public FormatItem(string sep)
				{
					this.sep = sep;
				}

				// Token: 0x06000390 RID: 912
				public abstract void Format(StringBuilder b, double num);

				// Token: 0x06000391 RID: 913 RVA: 0x000187D4 File Offset: 0x000169D4
				public static XslNumber.XslNumberFormatter.FormatItem GetItem(string sep, string item, char gpSep, int gpSize)
				{
					char c = item[0];
					if (c == '0' || c == '1')
					{
						int i;
						for (i = 1; i < item.Length; i++)
						{
							if (!char.IsDigit(item, i))
							{
								break;
							}
						}
						return new XslNumber.XslNumberFormatter.DigitItem(sep, i, gpSep, gpSize);
					}
					if (c == 'A')
					{
						return new XslNumber.XslNumberFormatter.AlphaItem(sep, true);
					}
					if (c == 'I')
					{
						return new XslNumber.XslNumberFormatter.RomanItem(sep, true);
					}
					if (c == 'a')
					{
						return new XslNumber.XslNumberFormatter.AlphaItem(sep, false);
					}
					if (c != 'i')
					{
						return new XslNumber.XslNumberFormatter.DigitItem(sep, 1, gpSep, gpSize);
					}
					return new XslNumber.XslNumberFormatter.RomanItem(sep, false);
				}

				// Token: 0x04000281 RID: 641
				public readonly string sep;
			}

			// Token: 0x02000066 RID: 102
			private class AlphaItem : XslNumber.XslNumberFormatter.FormatItem
			{
				// Token: 0x06000392 RID: 914 RVA: 0x0001887C File Offset: 0x00016A7C
				public AlphaItem(string sep, bool uc) : base(sep)
				{
					this.uc = uc;
				}

				// Token: 0x06000394 RID: 916 RVA: 0x000188C8 File Offset: 0x00016AC8
				public override void Format(StringBuilder b, double num)
				{
					XslNumber.XslNumberFormatter.AlphaItem.alphaSeq(b, num, (!this.uc) ? XslNumber.XslNumberFormatter.AlphaItem.lcl : XslNumber.XslNumberFormatter.AlphaItem.ucl);
				}

				// Token: 0x06000395 RID: 917 RVA: 0x000188EC File Offset: 0x00016AEC
				private static void alphaSeq(StringBuilder b, double n, char[] alphabet)
				{
					n = XslNumber.Round(n);
					if (n == 0.0)
					{
						return;
					}
					if (n > (double)alphabet.Length)
					{
						XslNumber.XslNumberFormatter.AlphaItem.alphaSeq(b, Math.Floor((n - 1.0) / (double)alphabet.Length), alphabet);
					}
					b.Append(alphabet[((int)n - 1) % alphabet.Length]);
				}

				// Token: 0x04000282 RID: 642
				private bool uc;

				// Token: 0x04000283 RID: 643
				private static readonly char[] ucl = new char[]
				{
					'A',
					'B',
					'C',
					'D',
					'E',
					'F',
					'G',
					'H',
					'I',
					'J',
					'K',
					'L',
					'M',
					'N',
					'O',
					'P',
					'Q',
					'R',
					'S',
					'T',
					'U',
					'V',
					'W',
					'X',
					'Y',
					'Z'
				};

				// Token: 0x04000284 RID: 644
				private static readonly char[] lcl = new char[]
				{
					'a',
					'b',
					'c',
					'd',
					'e',
					'f',
					'g',
					'h',
					'i',
					'j',
					'k',
					'l',
					'm',
					'n',
					'o',
					'p',
					'q',
					'r',
					's',
					't',
					'u',
					'v',
					'w',
					'x',
					'y',
					'z'
				};
			}

			// Token: 0x02000067 RID: 103
			private class RomanItem : XslNumber.XslNumberFormatter.FormatItem
			{
				// Token: 0x06000396 RID: 918 RVA: 0x00018948 File Offset: 0x00016B48
				public RomanItem(string sep, bool uc) : base(sep)
				{
					this.uc = uc;
				}

				// Token: 0x06000398 RID: 920 RVA: 0x00018A6C File Offset: 0x00016C6C
				public override void Format(StringBuilder b, double num)
				{
					if (num < 1.0 || num > 4999.0)
					{
						b.Append(num);
						return;
					}
					num = XslNumber.Round(num);
					for (int i = 0; i < XslNumber.XslNumberFormatter.RomanItem.decValues.Length; i++)
					{
						while ((double)XslNumber.XslNumberFormatter.RomanItem.decValues[i] <= num)
						{
							if (this.uc)
							{
								b.Append(XslNumber.XslNumberFormatter.RomanItem.ucrDigits[i]);
							}
							else
							{
								b.Append(XslNumber.XslNumberFormatter.RomanItem.lcrDigits[i]);
							}
							num -= (double)XslNumber.XslNumberFormatter.RomanItem.decValues[i];
						}
						if (num == 0.0)
						{
							break;
						}
					}
				}

				// Token: 0x04000285 RID: 645
				private bool uc;

				// Token: 0x04000286 RID: 646
				private static readonly string[] ucrDigits = new string[]
				{
					"M",
					"CM",
					"D",
					"CD",
					"C",
					"XC",
					"L",
					"XL",
					"X",
					"IX",
					"V",
					"IV",
					"I"
				};

				// Token: 0x04000287 RID: 647
				private static readonly string[] lcrDigits = new string[]
				{
					"m",
					"cm",
					"d",
					"cd",
					"c",
					"xc",
					"l",
					"xl",
					"x",
					"ix",
					"v",
					"iv",
					"i"
				};

				// Token: 0x04000288 RID: 648
				private static readonly int[] decValues = new int[]
				{
					1000,
					900,
					500,
					400,
					100,
					90,
					50,
					40,
					10,
					9,
					5,
					4,
					1
				};
			}

			// Token: 0x02000068 RID: 104
			private class DigitItem : XslNumber.XslNumberFormatter.FormatItem
			{
				// Token: 0x06000399 RID: 921 RVA: 0x00018B20 File Offset: 0x00016D20
				public DigitItem(string sep, int len, char gpSep, int gpSize) : base(sep)
				{
					this.nfi = new NumberFormatInfo();
					this.nfi.NumberDecimalDigits = 0;
					this.nfi.NumberGroupSizes = new int[1];
					if (gpSep != '\0' && gpSize > 0)
					{
						this.nfi.NumberGroupSeparator = gpSep.ToString();
						this.nfi.NumberGroupSizes = new int[]
						{
							gpSize
						};
					}
					this.decimalSectionLength = len;
				}

				// Token: 0x0600039A RID: 922 RVA: 0x00018B9C File Offset: 0x00016D9C
				public override void Format(StringBuilder b, double num)
				{
					string text = num.ToString("N", this.nfi);
					int num2 = this.decimalSectionLength;
					if (num2 > 1)
					{
						if (this.numberBuilder == null)
						{
							this.numberBuilder = new StringBuilder();
						}
						for (int i = num2; i > text.Length; i--)
						{
							this.numberBuilder.Append('0');
						}
						this.numberBuilder.Append((text.Length > num2) ? text.Substring(text.Length - num2, num2) : text);
						text = this.numberBuilder.ToString();
						this.numberBuilder.Length = 0;
					}
					b.Append(text);
				}

				// Token: 0x04000289 RID: 649
				private NumberFormatInfo nfi;

				// Token: 0x0400028A RID: 650
				private int decimalSectionLength;

				// Token: 0x0400028B RID: 651
				private StringBuilder numberBuilder;
			}
		}
	}
}
