using System;
using System.Globalization;
using System.Text;

namespace Mono.Xml.Xsl
{
	// Token: 0x0200008F RID: 143
	internal class DecimalFormatPattern
	{
		// Token: 0x060004DB RID: 1243 RVA: 0x0001E9D4 File Offset: 0x0001CBD4
		internal int ParsePattern(int start, string pattern, XslDecimalFormat format)
		{
			if (start == 0)
			{
				this.info = format.Info;
			}
			else
			{
				this.info = (format.Info.Clone() as NumberFormatInfo);
				this.info.NegativeSign = string.Empty;
			}
			int i;
			for (i = start; i < pattern.Length; i++)
			{
				if (pattern[i] == format.ZeroDigit || pattern[i] == format.Digit || pattern[i] == format.Info.CurrencySymbol[0])
				{
					break;
				}
			}
			this.Prefix = pattern.Substring(start, i - start);
			if (i == pattern.Length)
			{
				return i;
			}
			i = this.ParseNumber(i, pattern, format);
			int num = i;
			while (i < pattern.Length)
			{
				if (pattern[i] == format.ZeroDigit || pattern[i] == format.Digit || pattern[i] == format.PatternSeparator || pattern[i] == format.Info.CurrencySymbol[0])
				{
					break;
				}
				i++;
			}
			this.Suffix = pattern.Substring(num, i - num);
			return i;
		}

		// Token: 0x060004DC RID: 1244 RVA: 0x0001EB24 File Offset: 0x0001CD24
		private int ParseNumber(int start, string pattern, XslDecimalFormat format)
		{
			int i;
			for (i = start; i < pattern.Length; i++)
			{
				if (pattern[i] == format.Digit)
				{
					this.builder.Append('#');
				}
				else
				{
					if (pattern[i] != format.Info.NumberGroupSeparator[0])
					{
						break;
					}
					this.builder.Append(',');
				}
			}
			while (i < pattern.Length)
			{
				if (pattern[i] == format.ZeroDigit)
				{
					this.builder.Append('0');
				}
				else
				{
					if (pattern[i] != format.Info.NumberGroupSeparator[0])
					{
						break;
					}
					this.builder.Append(',');
				}
				i++;
			}
			if (i < pattern.Length)
			{
				if (pattern[i] == format.Info.NumberDecimalSeparator[0])
				{
					this.builder.Append('.');
					i++;
				}
				while (i < pattern.Length)
				{
					if (pattern[i] != format.ZeroDigit)
					{
						break;
					}
					i++;
					this.builder.Append('0');
				}
				while (i < pattern.Length)
				{
					if (pattern[i] != format.Digit)
					{
						break;
					}
					i++;
					this.builder.Append('#');
				}
			}
			if (i + 1 < pattern.Length && pattern[i] == 'E' && pattern[i + 1] == format.ZeroDigit)
			{
				i += 2;
				this.builder.Append("E0");
				while (i < pattern.Length)
				{
					if (pattern[i] != format.ZeroDigit)
					{
						break;
					}
					i++;
					this.builder.Append('0');
				}
			}
			if (i < pattern.Length)
			{
				if (pattern[i] == this.info.PercentSymbol[0])
				{
					this.builder.Append('%');
				}
				else if (pattern[i] == this.info.PerMilleSymbol[0])
				{
					this.builder.Append('‰');
				}
				else
				{
					if (pattern[i] == this.info.CurrencySymbol[0])
					{
						throw new ArgumentException("Currency symbol is not supported for number format pattern string.");
					}
					i--;
				}
				i++;
			}
			this.NumberPart = this.builder.ToString();
			return i;
		}

		// Token: 0x060004DD RID: 1245 RVA: 0x0001EE00 File Offset: 0x0001D000
		public string FormatNumber(double number)
		{
			this.builder.Length = 0;
			this.builder.Append(this.Prefix);
			this.builder.Append(number.ToString(this.NumberPart, this.info));
			this.builder.Append(this.Suffix);
			return this.builder.ToString();
		}

		// Token: 0x0400031A RID: 794
		public string Prefix = string.Empty;

		// Token: 0x0400031B RID: 795
		public string Suffix = string.Empty;

		// Token: 0x0400031C RID: 796
		public string NumberPart;

		// Token: 0x0400031D RID: 797
		private NumberFormatInfo info;

		// Token: 0x0400031E RID: 798
		private StringBuilder builder = new StringBuilder();
	}
}
