using System;
using System.Runtime.InteropServices;

namespace System.Globalization
{
	/// <summary>Defines the string comparison options to use with <see cref="T:System.Globalization.CompareInfo" />.</summary>
	// Token: 0x0200020D RID: 525
	[Flags]
	[ComVisible(true)]
	[Serializable]
	public enum CompareOptions
	{
		/// <summary>Indicates the default option settings for string comparisons.</summary>
		// Token: 0x04000989 RID: 2441
		None = 0,
		/// <summary>Indicates that the string comparison must ignore case.</summary>
		// Token: 0x0400098A RID: 2442
		IgnoreCase = 1,
		/// <summary>Indicates that the string comparison must ignore nonspacing combining characters, such as diacritics. The Unicode Standard defines combining characters as characters that are combined with base characters to produce a new character. Nonspacing combining characters do not occupy a spacing position by themselves when rendered. For more information on nonspacing combining characters, see The Unicode Standard at the Unicode home page.</summary>
		// Token: 0x0400098B RID: 2443
		IgnoreNonSpace = 2,
		/// <summary>Indicates that the string comparison must ignore symbols, such as white-space characters, punctuation, currency symbols, the percent sign, mathematical symbols, the ampersand, and so on.</summary>
		// Token: 0x0400098C RID: 2444
		IgnoreSymbols = 4,
		/// <summary>Indicates that the string comparison must ignore the Kana type. Kana type refers to Japanese hiragana and katakana characters, which represent phonetic sounds in the Japanese language. Hiragana is used for native Japanese expressions and words, while katakana is used for words borrowed from other languages, such as "computer" or "Internet". A phonetic sound can be expressed in both hiragana and katakana. If this value is selected, the hiragana character for one sound is considered equal to the katakana character for the same sound.</summary>
		// Token: 0x0400098D RID: 2445
		IgnoreKanaType = 8,
		/// <summary>Indicates that the string comparison must ignore the character width. For example, Japanese katakana characters can be written as full-width or half-width. If this value is selected, the katakana characters written as full-width are considered equal to the same characters written as half-width.</summary>
		// Token: 0x0400098E RID: 2446
		IgnoreWidth = 16,
		/// <summary>Indicates that the string comparison must use the string sort algorithm. In a string sort, the hyphen and the apostrophe, as well as other nonalphanumeric symbols, come before alphanumeric characters.</summary>
		// Token: 0x0400098F RID: 2447
		StringSort = 536870912,
		/// <summary>Indicates that the string comparison must use the Unicode values of each character, leading to a fast comparison but one that is culture-insensitive. A string starting with "U+xxxx" comes before a string starting with "U+yyyy", if xxxx is less than yyyy. This value cannot be combined with other <see cref="T:System.Globalization.CompareOptions" /> values and must be used alone.</summary>
		// Token: 0x04000990 RID: 2448
		Ordinal = 1073741824,
		/// <summary>String comparison must ignore case, then perform an ordinal comparison. This technique is equivalent to converting the string to uppercase using the invariant culture and then performing an ordinal comparison on the result.</summary>
		// Token: 0x04000991 RID: 2449
		OrdinalIgnoreCase = 268435456
	}
}
