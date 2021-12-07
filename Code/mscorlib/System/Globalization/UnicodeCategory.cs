using System;
using System.Runtime.InteropServices;

namespace System.Globalization
{
	/// <summary>Defines the Unicode category of a character.</summary>
	// Token: 0x0200022E RID: 558
	[ComVisible(true)]
	[Serializable]
	public enum UnicodeCategory
	{
		/// <summary>Indicates that the character is an uppercase letter. Signified by the Unicode designation "Lu" (letter, uppercase). The value is 0.</summary>
		// Token: 0x04000ABD RID: 2749
		UppercaseLetter,
		/// <summary>Indicates that the character is a lowercase letter. Signified by the Unicode designation "Ll" (letter, lowercase). The value is 1.</summary>
		// Token: 0x04000ABE RID: 2750
		LowercaseLetter,
		/// <summary>Indicates that the character is a titlecase letter. Signified by the Unicode designation "Lt" (letter, titlecase). The value is 2.</summary>
		// Token: 0x04000ABF RID: 2751
		TitlecaseLetter,
		/// <summary>Indicates that the character is a modifier letter, which is free-standing spacing character that indicates modifications of a preceding letter. Signified by the Unicode designation "Lm" (letter, modifier). The value is 3.</summary>
		// Token: 0x04000AC0 RID: 2752
		ModifierLetter,
		/// <summary>Indicates that the character is a letter that is not an uppercase letter, a lowercase letter, a titlecase letter, or a modifier letter. Signified by the Unicode designation "Lo" (letter, other). The value is 4.</summary>
		// Token: 0x04000AC1 RID: 2753
		OtherLetter,
		/// <summary>Indicates that the character is a nonspacing character, which indicates modifications of a base character. Signified by the Unicode designation "Mn" (mark, nonspacing). The value is 5.</summary>
		// Token: 0x04000AC2 RID: 2754
		NonSpacingMark,
		/// <summary>Indicates that the character is a spacing character, which indicates modifications of a base character and affects the width of the glyph for that base character. Signified by the Unicode designation "Mc" (mark, spacing combining). The value is 6.</summary>
		// Token: 0x04000AC3 RID: 2755
		SpacingCombiningMark,
		/// <summary>Indicates that the character is an enclosing mark, which is a nonspacing combining character that surrounds all previous characters up to and including a base character. Signified by the Unicode designation "Me" (mark, enclosing). The value is 7.</summary>
		// Token: 0x04000AC4 RID: 2756
		EnclosingMark,
		/// <summary>Indicates that the character is a decimal digit, that is, in the range 0 through 9. Signified by the Unicode designation "Nd" (number, decimal digit). The value is 8.</summary>
		// Token: 0x04000AC5 RID: 2757
		DecimalDigitNumber,
		/// <summary>Indicates that the character is a number represented by a letter, instead of a decimal digit, for example, the Roman numeral for five, which is "V". The indicator is signified by the Unicode designation "Nl" (number, letter). The value is 9.</summary>
		// Token: 0x04000AC6 RID: 2758
		LetterNumber,
		/// <summary>Indicates that the character is a number that is neither a decimal digit nor a letter number, for example, the fraction 1/2. The indicator is signified by the Unicode designation "No" (number, other). The value is 10.</summary>
		// Token: 0x04000AC7 RID: 2759
		OtherNumber,
		/// <summary>Indicates that the character is a space character, which has no glyph but is not a control or format character. Signified by the Unicode designation "Zs" (separator, space). The value is 11.</summary>
		// Token: 0x04000AC8 RID: 2760
		SpaceSeparator,
		/// <summary>Indicates that the character is used to separate lines of text. Signified by the Unicode designation "Zl" (separator, line). The value is 12.</summary>
		// Token: 0x04000AC9 RID: 2761
		LineSeparator,
		/// <summary>Indicates that the character is used to separate paragraphs. Signified by the Unicode designation "Zp" (separator, paragraph). The value is 13.</summary>
		// Token: 0x04000ACA RID: 2762
		ParagraphSeparator,
		/// <summary>Indicates that the character is a control code, with a Unicode value of U+007F or in the range U+0000 through U+001F or U+0080 through U+009F. Signified by the Unicode designation "Cc" (other, control). The value is 14.</summary>
		// Token: 0x04000ACB RID: 2763
		Control,
		/// <summary>Indicates that the character is a format character, which is not normally rendered but affects the layout of text or the operation of text processes. Signified by the Unicode designation "Cf" (other, format). The value is 15.</summary>
		// Token: 0x04000ACC RID: 2764
		Format,
		/// <summary>Indicates that the character is a high surrogate or a low surrogate. Surrogate code values are in the range U+D800 through U+DFFF. Signified by the Unicode designation "Cs" (other, surrogate). The value is 16.</summary>
		// Token: 0x04000ACD RID: 2765
		Surrogate,
		/// <summary>Indicates that the character is a private-use character, with a Unicode value in the range U+E000 through U+F8FF. Signified by the Unicode designation "Co" (other, private use). The value is 17.</summary>
		// Token: 0x04000ACE RID: 2766
		PrivateUse,
		/// <summary>Indicates that the character is a connector punctuation, which connects two characters. Signified by the Unicode designation "Pc" (punctuation, connector). The value is 18.</summary>
		// Token: 0x04000ACF RID: 2767
		ConnectorPunctuation,
		/// <summary>Indicates that the character is a dash or a hyphen. Signified by the Unicode designation "Pd" (punctuation, dash). The value is 19.</summary>
		// Token: 0x04000AD0 RID: 2768
		DashPunctuation,
		/// <summary>Indicates that the character is the opening character of one of the paired punctuation marks, such as parentheses, square brackets, and braces. Signified by the Unicode designation "Ps" (punctuation, open). The value is 20.</summary>
		// Token: 0x04000AD1 RID: 2769
		OpenPunctuation,
		/// <summary>Indicates that the character is the closing character of one of the paired punctuation marks, such as parentheses, square brackets, and braces. Signified by the Unicode designation "Pe" (punctuation, close). The value is 21.</summary>
		// Token: 0x04000AD2 RID: 2770
		ClosePunctuation,
		/// <summary>Indicates that the character is an opening or initial quotation mark. Signified by the Unicode designation "Pi" (punctuation, initial quote). The value is 22.</summary>
		// Token: 0x04000AD3 RID: 2771
		InitialQuotePunctuation,
		/// <summary>Indicates that the character is a closing or final quotation mark. Signified by the Unicode designation "Pf" (punctuation, final quote). The value is 23.</summary>
		// Token: 0x04000AD4 RID: 2772
		FinalQuotePunctuation,
		/// <summary>Indicates that the character is a punctuation that is not a connector punctuation, a dash punctuation, an open punctuation, a close punctuation, an initial quote punctuation, or a final quote punctuation. Signified by the Unicode designation "Po" (punctuation, other). The value is 24.</summary>
		// Token: 0x04000AD5 RID: 2773
		OtherPunctuation,
		/// <summary>Indicates that the character is a mathematical symbol, such as "+" or "= ". Signified by the Unicode designation "Sm" (symbol, math). The value is 25.</summary>
		// Token: 0x04000AD6 RID: 2774
		MathSymbol,
		/// <summary>Indicates that the character is a currency symbol. Signified by the Unicode designation "Sc" (symbol, currency). The value is 26.</summary>
		// Token: 0x04000AD7 RID: 2775
		CurrencySymbol,
		/// <summary>Indicates that the character is a modifier symbol, which indicates modifications of surrounding characters. For example, the fraction slash indicates that the number to the left is the numerator and the number to the right is the denominator. The indicator is signified by the Unicode designation "Sk" (symbol, modifier). The value is 27.</summary>
		// Token: 0x04000AD8 RID: 2776
		ModifierSymbol,
		/// <summary>Indicates that the character is a symbol that is not a mathematical symbol, a currency symbol or a modifier symbol. Signified by the Unicode designation "So" (symbol, other). The value is 28.</summary>
		// Token: 0x04000AD9 RID: 2777
		OtherSymbol,
		/// <summary>Indicates that the character is not assigned to any Unicode category. Signified by the Unicode designation "Cn" (other, not assigned). The value is 29.</summary>
		// Token: 0x04000ADA RID: 2778
		OtherNotAssigned
	}
}
