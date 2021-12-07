using System;

namespace Mono.Xml.Xsl
{
	// Token: 0x0200008E RID: 142
	internal class DecimalFormatPatternSet
	{
		// Token: 0x060004D7 RID: 1239 RVA: 0x0001E8C0 File Offset: 0x0001CAC0
		public DecimalFormatPatternSet(string pattern, XslDecimalFormat decimalFormat)
		{
			this.Parse(pattern, decimalFormat);
		}

		// Token: 0x060004D8 RID: 1240 RVA: 0x0001E8D0 File Offset: 0x0001CAD0
		private void Parse(string pattern, XslDecimalFormat format)
		{
			if (pattern.Length == 0)
			{
				throw new ArgumentException("Invalid number format pattern string.");
			}
			this.positivePattern = new DecimalFormatPattern();
			this.negativePattern = this.positivePattern;
			int num = this.positivePattern.ParsePattern(0, pattern, format);
			if (num < pattern.Length)
			{
				if (pattern[num] != format.PatternSeparator)
				{
					return;
				}
				num++;
				this.negativePattern = new DecimalFormatPattern();
				num = this.negativePattern.ParsePattern(num, pattern, format);
				if (num < pattern.Length)
				{
					throw new ArgumentException("Number format pattern string ends with extraneous part.");
				}
			}
		}

		// Token: 0x060004D9 RID: 1241 RVA: 0x0001E970 File Offset: 0x0001CB70
		public string FormatNumber(double number)
		{
			if (number >= 0.0)
			{
				return this.positivePattern.FormatNumber(number);
			}
			return this.negativePattern.FormatNumber(number);
		}

		// Token: 0x04000318 RID: 792
		private DecimalFormatPattern positivePattern;

		// Token: 0x04000319 RID: 793
		private DecimalFormatPattern negativePattern;
	}
}
