using System;
using System.Globalization;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Xsl;

namespace Mono.Xml.Xsl
{
	// Token: 0x0200008D RID: 141
	internal class XslDecimalFormat
	{
		// Token: 0x060004CD RID: 1229 RVA: 0x0001E37C File Offset: 0x0001C57C
		private XslDecimalFormat()
		{
		}

		// Token: 0x060004CE RID: 1230 RVA: 0x0001E3A8 File Offset: 0x0001C5A8
		public XslDecimalFormat(Compiler c)
		{
			XPathNavigator input = c.Input;
			IXmlLineInfo xmlLineInfo = input as IXmlLineInfo;
			if (xmlLineInfo != null)
			{
				this.lineNumber = xmlLineInfo.LineNumber;
				this.linePosition = xmlLineInfo.LinePosition;
			}
			this.baseUri = input.BaseURI;
			if (input.MoveToFirstAttribute())
			{
				for (;;)
				{
					if (!(input.NamespaceURI != string.Empty))
					{
						string localName = input.LocalName;
						switch (localName)
						{
						case "decimal-separator":
							if (input.Value.Length != 1)
							{
								goto Block_7;
							}
							this.info.NumberDecimalSeparator = input.Value;
							break;
						case "grouping-separator":
							if (input.Value.Length != 1)
							{
								goto Block_8;
							}
							this.info.NumberGroupSeparator = input.Value;
							break;
						case "infinity":
							this.info.PositiveInfinitySymbol = input.Value;
							break;
						case "minus-sign":
							if (input.Value.Length != 1)
							{
								goto Block_9;
							}
							this.info.NegativeSign = input.Value;
							break;
						case "NaN":
							this.info.NaNSymbol = input.Value;
							break;
						case "percent":
							if (input.Value.Length != 1)
							{
								goto Block_10;
							}
							this.info.PercentSymbol = input.Value;
							break;
						case "per-mille":
							if (input.Value.Length != 1)
							{
								goto Block_11;
							}
							this.info.PerMilleSymbol = input.Value;
							break;
						case "digit":
							if (input.Value.Length != 1)
							{
								goto Block_12;
							}
							this.digit = input.Value[0];
							break;
						case "zero-digit":
							if (input.Value.Length != 1)
							{
								goto Block_13;
							}
							this.zeroDigit = input.Value[0];
							break;
						case "pattern-separator":
							if (input.Value.Length != 1)
							{
								goto Block_14;
							}
							this.patternSeparator = input.Value[0];
							break;
						}
					}
					if (!input.MoveToNextAttribute())
					{
						goto Block_15;
					}
				}
				Block_7:
				throw new XsltCompileException("XSLT decimal-separator value must be exact one character", null, input);
				Block_8:
				throw new XsltCompileException("XSLT grouping-separator value must be exact one character", null, input);
				Block_9:
				throw new XsltCompileException("XSLT minus-sign value must be exact one character", null, input);
				Block_10:
				throw new XsltCompileException("XSLT percent value must be exact one character", null, input);
				Block_11:
				throw new XsltCompileException("XSLT per-mille value must be exact one character", null, input);
				Block_12:
				throw new XsltCompileException("XSLT digit value must be exact one character", null, input);
				Block_13:
				throw new XsltCompileException("XSLT zero-digit value must be exact one character", null, input);
				Block_14:
				throw new XsltCompileException("XSLT pattern-separator value must be exact one character", null, input);
				Block_15:
				input.MoveToParent();
				this.info.NegativeInfinitySymbol = this.info.NegativeSign + this.info.PositiveInfinitySymbol;
			}
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x060004D0 RID: 1232 RVA: 0x0001E748 File Offset: 0x0001C948
		public char Digit
		{
			get
			{
				return this.digit;
			}
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x060004D1 RID: 1233 RVA: 0x0001E750 File Offset: 0x0001C950
		public char ZeroDigit
		{
			get
			{
				return this.zeroDigit;
			}
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x060004D2 RID: 1234 RVA: 0x0001E758 File Offset: 0x0001C958
		public NumberFormatInfo Info
		{
			get
			{
				return this.info;
			}
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x060004D3 RID: 1235 RVA: 0x0001E760 File Offset: 0x0001C960
		public char PatternSeparator
		{
			get
			{
				return this.patternSeparator;
			}
		}

		// Token: 0x060004D4 RID: 1236 RVA: 0x0001E768 File Offset: 0x0001C968
		public void CheckSameAs(XslDecimalFormat other)
		{
			if (this.digit != other.digit || this.patternSeparator != other.patternSeparator || this.zeroDigit != other.zeroDigit || this.info.NumberDecimalSeparator != other.info.NumberDecimalSeparator || this.info.NumberGroupSeparator != other.info.NumberGroupSeparator || this.info.PositiveInfinitySymbol != other.info.PositiveInfinitySymbol || this.info.NegativeSign != other.info.NegativeSign || this.info.NaNSymbol != other.info.NaNSymbol || this.info.PercentSymbol != other.info.PercentSymbol || this.info.PerMilleSymbol != other.info.PerMilleSymbol)
			{
				throw new XsltCompileException(null, other.baseUri, other.lineNumber, other.linePosition);
			}
		}

		// Token: 0x060004D5 RID: 1237 RVA: 0x0001E8A4 File Offset: 0x0001CAA4
		public string FormatNumber(double number, string pattern)
		{
			return this.ParsePatternSet(pattern).FormatNumber(number);
		}

		// Token: 0x060004D6 RID: 1238 RVA: 0x0001E8B4 File Offset: 0x0001CAB4
		private DecimalFormatPatternSet ParsePatternSet(string pattern)
		{
			return new DecimalFormatPatternSet(pattern, this);
		}

		// Token: 0x0400030F RID: 783
		private NumberFormatInfo info = new NumberFormatInfo();

		// Token: 0x04000310 RID: 784
		private char digit = '#';

		// Token: 0x04000311 RID: 785
		private char zeroDigit = '0';

		// Token: 0x04000312 RID: 786
		private char patternSeparator = ';';

		// Token: 0x04000313 RID: 787
		private string baseUri;

		// Token: 0x04000314 RID: 788
		private int lineNumber;

		// Token: 0x04000315 RID: 789
		private int linePosition;

		// Token: 0x04000316 RID: 790
		public static readonly XslDecimalFormat Default = new XslDecimalFormat();
	}
}
