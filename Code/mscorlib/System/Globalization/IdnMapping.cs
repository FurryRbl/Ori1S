using System;
using System.Text;

namespace System.Globalization
{
	/// <summary>Supports the use of non-ASCII characters for Internet domain names. This class cannot be inherited.</summary>
	// Token: 0x0200021A RID: 538
	public sealed class IdnMapping
	{
		/// <summary>Gets or sets a value indicating whether unassigned Unicode code points are used in operations performed by members of the current <see cref="T:System.Globalization.IdnMapping" /> object.</summary>
		/// <returns>true if unassigned code points are used in operations; otherwise, false.</returns>
		// Token: 0x17000470 RID: 1136
		// (get) Token: 0x06001B2F RID: 6959 RVA: 0x00064C20 File Offset: 0x00062E20
		// (set) Token: 0x06001B30 RID: 6960 RVA: 0x00064C28 File Offset: 0x00062E28
		public bool AllowUnassigned
		{
			get
			{
				return this.allow_unassigned;
			}
			set
			{
				this.allow_unassigned = value;
			}
		}

		/// <summary>Gets or sets a value indicating whether standard or nonstandard naming conventions are used in operations performed by members of the current <see cref="T:System.Globalization.IdnMapping" /> object.</summary>
		/// <returns>true if nonstandard naming conventions are used in operations; otherwise, false.</returns>
		// Token: 0x17000471 RID: 1137
		// (get) Token: 0x06001B31 RID: 6961 RVA: 0x00064C34 File Offset: 0x00062E34
		// (set) Token: 0x06001B32 RID: 6962 RVA: 0x00064C3C File Offset: 0x00062E3C
		public bool UseStd3AsciiRules
		{
			get
			{
				return this.use_std3;
			}
			set
			{
				this.use_std3 = value;
			}
		}

		/// <summary>Indicates whether a specified object and this <see cref="T:System.Globalization.IdnMapping" /> object are equal.</summary>
		/// <returns>true if the <paramref name="obj" /> parameter is derived from <see cref="T:System.Globalization.IdnMapping" /> and its <see cref="P:System.Globalization.IdnMapping.AllowUnassigned" /> and <see cref="P:System.Globalization.IdnMapping.UseStd3AsciiRules" /> properties are equal; otherwise, false. </returns>
		/// <param name="obj">An object.</param>
		// Token: 0x06001B33 RID: 6963 RVA: 0x00064C48 File Offset: 0x00062E48
		public override bool Equals(object obj)
		{
			IdnMapping idnMapping = obj as IdnMapping;
			return idnMapping != null && this.allow_unassigned == idnMapping.allow_unassigned && this.use_std3 == idnMapping.use_std3;
		}

		/// <summary>Returns a hash code for this <see cref="T:System.Globalization.IdnMapping" /> object.</summary>
		/// <returns>One of four 32-bit signed constants derived from the properties of a <see cref="T:System.Globalization.IdnMapping" /> object.  The return value has no special meaning and is not suitable for use in a hash code algorithm.</returns>
		// Token: 0x06001B34 RID: 6964 RVA: 0x00064C84 File Offset: 0x00062E84
		public override int GetHashCode()
		{
			return ((!this.allow_unassigned) ? 0 : 2) + ((!this.use_std3) ? 0 : 1);
		}

		/// <summary>Encodes a string of one or more domain name labels that consist of Unicode characters to a string of Unicode characters in the US-ASCII character range.</summary>
		/// <returns>The equivalent of the string specified by the <paramref name="unicode" /> parameter, consisting of displayable Unicode characters in the US-ASCII character range (U+0020 to U+007E) and formatted according to the Internationalizing Domain Names in Applications (IDNA) standard.</returns>
		/// <param name="unicode">An input string to convert, which consists of one or more domain name labels delimited with label separators.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="unicode" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="unicode" /> is invalid based on the <see cref="P:System.Globalization.IdnMapping.AllowUnassigned" /> and <see cref="P:System.Globalization.IdnMapping.UseStd3AsciiRules" /> properties, and the IDNA standard.-or-A label contains one or more of the Unicode control characters from U+0001 through U+001F, or U+007F.</exception>
		// Token: 0x06001B35 RID: 6965 RVA: 0x00064CAC File Offset: 0x00062EAC
		public string GetAscii(string unicode)
		{
			if (unicode == null)
			{
				throw new ArgumentNullException("unicode");
			}
			return this.GetAscii(unicode, 0, unicode.Length);
		}

		/// <summary>Encodes a substring of one or more domain name labels that consist of Unicode characters to a string of Unicode characters in the US-ASCII character range.  </summary>
		/// <returns>The equivalent of the substring specified by the <paramref name="unicode" /> and <paramref name="index" /> parameters, consisting of displayable Unicode characters in the US-ASCII character range (U+0020 to U+007E) and formatted according to the Internationalizing Domain Names in Applications (IDNA) standard.</returns>
		/// <param name="unicode">An input string to convert, which consists of one or more domain name labels delimited with label separators.</param>
		/// <param name="index">A zero-based offset into <paramref name="unicode" /> that specifies the start of the substring. The conversion operation continues to the end of <paramref name="unicode" />.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="unicode" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> or <paramref name="count" /> is less than zero.-or-<paramref name="index" /> is greater than the length of <paramref name="unicode" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="unicode" /> is invalid based on the <see cref="P:System.Globalization.IdnMapping.AllowUnassigned" /> and <see cref="P:System.Globalization.IdnMapping.UseStd3AsciiRules" /> properties, and the IDNA standard.-or-A label contains one or more of the Unicode control characters from U+0001 through U+001F, or U+007F.</exception>
		// Token: 0x06001B36 RID: 6966 RVA: 0x00064CD0 File Offset: 0x00062ED0
		public string GetAscii(string unicode, int index)
		{
			if (unicode == null)
			{
				throw new ArgumentNullException("unicode");
			}
			return this.GetAscii(unicode, index, unicode.Length - index);
		}

		/// <summary>Encodes a substring of one or more domain name labels that consist of Unicode characters to a string of Unicode characters in the US-ASCII character range. The string is formatted according to the Internationalizing Domain Names in Applications (IDNA) standard. </summary>
		/// <returns>The equivalent of the substring specified by the <paramref name="unicode" />, <paramref name="index" />, and <paramref name="count" /> parameters, consisting of displayable Unicode characters in the US-ASCII character range (U+0020 to U+007E) and formatted according to the IDNA standard.</returns>
		/// <param name="unicode">An input string to convert, which consists of one or more domain name labels delimited with label separators.</param>
		/// <param name="index">A zero-based offset into <paramref name="unicode" /> that specifies the start of the substring.</param>
		/// <param name="count">The number of characters to convert in the substring that starts at the position specified by <paramref name="unicode" /> and <paramref name="index" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="unicode" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> or <paramref name="count" /> is less than zero.-or-<paramref name="index" /> is greater than the length of <paramref name="unicode" />.-or-<paramref name="index" /> is greater than the length of <paramref name="unicode" /> minus <paramref name="count" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="unicode" /> is invalid based on the <see cref="P:System.Globalization.IdnMapping.AllowUnassigned" /> and <see cref="P:System.Globalization.IdnMapping.UseStd3AsciiRules" /> properties, and the IDNA standard.-or-A label contains one or more of the Unicode control characters from U+0001 through U+001F, or U+007F.</exception>
		// Token: 0x06001B37 RID: 6967 RVA: 0x00064CF4 File Offset: 0x00062EF4
		public string GetAscii(string unicode, int index, int count)
		{
			if (unicode == null)
			{
				throw new ArgumentNullException("unicode");
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index must be non-negative value");
			}
			if (count < 0 || index + count > unicode.Length)
			{
				throw new ArgumentOutOfRangeException("index + count must point inside the argument unicode string");
			}
			return this.Convert(unicode, index, count, true);
		}

		// Token: 0x06001B38 RID: 6968 RVA: 0x00064D50 File Offset: 0x00062F50
		private string Convert(string input, int index, int count, bool toAscii)
		{
			string text = input.Substring(index, count);
			for (int i = 0; i < text.Length; i++)
			{
				if (text[i] >= '\u0080')
				{
					text = text.ToLower(CultureInfo.InvariantCulture);
					break;
				}
			}
			string[] array = text.Split(new char[]
			{
				'.',
				'。',
				'．',
				'｡'
			});
			int num = 0;
			for (int j = 0; j < array.Length; j++)
			{
				if (array[j].Length != 0 || j + 1 != array.Length)
				{
					if (toAscii)
					{
						array[j] = this.ToAscii(array[j], num);
					}
					else
					{
						array[j] = this.ToUnicode(array[j], num);
					}
				}
				num += array[j].Length;
			}
			return string.Join(".", array);
		}

		// Token: 0x06001B39 RID: 6969 RVA: 0x00064E34 File Offset: 0x00063034
		private string ToAscii(string s, int offset)
		{
			for (int i = 0; i < s.Length; i++)
			{
				if (s[i] < ' ' || s[i] == '\u007f')
				{
					throw new ArgumentException(string.Format("Not allowed character was found, at {0}", offset + i));
				}
				if (s[i] >= '\u0080')
				{
					s = this.NamePrep(s, offset);
					break;
				}
			}
			if (this.use_std3)
			{
				this.VerifyStd3AsciiRules(s, offset);
			}
			int j = 0;
			while (j < s.Length)
			{
				if (s[j] >= '\u0080')
				{
					if (s.StartsWith("xn--", StringComparison.OrdinalIgnoreCase))
					{
						throw new ArgumentException(string.Format("The input string must not start with ACE (xn--), at {0}", offset + j));
					}
					s = this.puny.Encode(s, offset);
					s = "xn--" + s;
					break;
				}
				else
				{
					j++;
				}
			}
			this.VerifyLength(s, offset);
			return s;
		}

		// Token: 0x06001B3A RID: 6970 RVA: 0x00064F3C File Offset: 0x0006313C
		private void VerifyLength(string s, int offset)
		{
			if (s.Length == 0)
			{
				throw new ArgumentException(string.Format("A label in the input string resulted in an invalid zero-length string, at {0}", offset));
			}
			if (s.Length > 63)
			{
				throw new ArgumentException(string.Format("A label in the input string exceeded the length in ASCII representation, at {0}", offset));
			}
		}

		// Token: 0x06001B3B RID: 6971 RVA: 0x00064F90 File Offset: 0x00063190
		private string NamePrep(string s, int offset)
		{
			s = s.Normalize(NormalizationForm.FormKC);
			this.VerifyProhibitedCharacters(s, offset);
			if (!this.allow_unassigned)
			{
				for (int i = 0; i < s.Length; i++)
				{
					if (char.GetUnicodeCategory(s, i) == UnicodeCategory.OtherNotAssigned)
					{
						throw new ArgumentException(string.Format("Use of unassigned Unicode characer is prohibited in this IdnMapping, at {0}", offset + i));
					}
				}
			}
			return s;
		}

		// Token: 0x06001B3C RID: 6972 RVA: 0x00064FF8 File Offset: 0x000631F8
		private void VerifyProhibitedCharacters(string s, int offset)
		{
			int i = 0;
			while (i < s.Length)
			{
				switch (char.GetUnicodeCategory(s, i))
				{
				case UnicodeCategory.SpaceSeparator:
					if (s[i] >= '\u0080')
					{
						goto IL_164;
					}
					break;
				case UnicodeCategory.LineSeparator:
				case UnicodeCategory.ParagraphSeparator:
				case UnicodeCategory.Format:
					goto IL_80;
				case UnicodeCategory.Control:
					if (s[i] == '\0' || s[i] >= '\u0080')
					{
						goto IL_164;
					}
					break;
				case UnicodeCategory.Surrogate:
				case UnicodeCategory.PrivateUse:
					goto IL_164;
				default:
					goto IL_80;
				}
				IL_17C:
				i++;
				continue;
				IL_80:
				char c = s[i];
				if (('﷟' > c || c > '﷯') && ((c & '￿') != '￾' && ('￹' > c || c > '�')) && ('⿰' > c || c > '⿻') && ('‪' > c || c > '‮') && ('⁪' > c || c > '⁯'))
				{
					char c2 = c;
					if (c2 != '̀' && c2 != '́' && c2 != '‎' && c2 != '‏' && c2 != '\u2028' && c2 != '\u2029')
					{
						goto IL_17C;
					}
				}
				IL_164:
				throw new ArgumentException(string.Format("Not allowed character was in the input string, at {0}", offset + i));
			}
		}

		// Token: 0x06001B3D RID: 6973 RVA: 0x00065194 File Offset: 0x00063394
		private void VerifyStd3AsciiRules(string s, int offset)
		{
			if (s.Length > 0 && s[0] == '-')
			{
				throw new ArgumentException(string.Format("'-' is not allowed at head of a sequence in STD3 mode, found at {0}", offset));
			}
			if (s.Length > 0 && s[s.Length - 1] == '-')
			{
				throw new ArgumentException(string.Format("'-' is not allowed at tail of a sequence in STD3 mode, found at {0}", offset + s.Length - 1));
			}
			for (int i = 0; i < s.Length; i++)
			{
				char c = s[i];
				if (c != '-')
				{
					if (c <= '/' || (':' <= c && c <= '@') || ('[' <= c && c <= '`') || ('{' <= c && c <= '\u007f'))
					{
						throw new ArgumentException(string.Format("Not allowed character in STD3 mode, found at {0}", offset + i));
					}
				}
			}
		}

		/// <summary>Decodes a string of one or more domain name labels encoded according to the Internationalizing Domain Names in Applications (IDNA) standard to a string of Unicode characters. </summary>
		/// <returns>The Unicode equivalent of the IDNA substring specified by the <paramref name="ascii" /> parameter.</returns>
		/// <param name="ascii">One or more labels in the US-ASCII character range (U+0020 to U+007E) encoded according to the IDNA standard. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="ascii" /> is null.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="ascii" /> is invalid based on the <see cref="P:System.Globalization.IdnMapping.AllowUnassigned" /> and <see cref="P:System.Globalization.IdnMapping.UseStd3AsciiRules" /> properties, and the IDNA standard.</exception>
		// Token: 0x06001B3E RID: 6974 RVA: 0x00065290 File Offset: 0x00063490
		public string GetUnicode(string ascii)
		{
			if (ascii == null)
			{
				throw new ArgumentNullException("ascii");
			}
			return this.GetUnicode(ascii, 0, ascii.Length);
		}

		/// <summary>Decodes a substring of one or more domain name labels encoded according to the Internationalizing Domain Names in Applications (IDNA) standard to a string of Unicode characters. </summary>
		/// <returns>The Unicode equivalent of the IDNA substring specified by the <paramref name="ascii" /> and <paramref name="index" /> parameters.</returns>
		/// <param name="ascii">One or more labels in the US-ASCII character range (U+0020 to U+007E) encoded according to the IDNA standard. </param>
		/// <param name="index">A zero-based offset into <paramref name="ascii" /> that specifies the start of the substring. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="ascii" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> is less than zero.-or-<paramref name="index" /> is greater than the length of <paramref name="ascii" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="ascii" /> is invalid based on the <see cref="P:System.Globalization.IdnMapping.AllowUnassigned" /> and <see cref="P:System.Globalization.IdnMapping.UseStd3AsciiRules" /> properties, and the IDNA standard.</exception>
		// Token: 0x06001B3F RID: 6975 RVA: 0x000652B4 File Offset: 0x000634B4
		public string GetUnicode(string ascii, int index)
		{
			if (ascii == null)
			{
				throw new ArgumentNullException("ascii");
			}
			return this.GetUnicode(ascii, index, ascii.Length - index);
		}

		/// <summary>Decodes a substring of one or more domain name labels encoded according to the Internationalizing Domain Names in Applications (IDNA) standard to a string of Unicode characters. </summary>
		/// <returns>The Unicode equivalent of the IDNA substring specified by the <paramref name="ascii" />, <paramref name="index" />, and <paramref name="count" /> parameters.</returns>
		/// <param name="ascii">One or more labels in the US-ASCII character range (U+0020 to U+007E) encoded according to the IDNA standard. </param>
		/// <param name="index">A zero-based offset into <paramref name="ascii" /> that specifies the start of the substring. </param>
		/// <param name="count">The number of characters to convert in the substring that starts at the position specified by <paramref name="ascii" /> and <paramref name="index" />. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="ascii" /> is null.</exception>
		/// <exception cref="T:System.ArgumentOutOfRangeException">
		///   <paramref name="index" /> or <paramref name="count" /> is less than zero.-or-<paramref name="index" /> is greater than the length of <paramref name="ascii" />.-or-<paramref name="index" /> is greater than the length of <paramref name="ascii" /> minus <paramref name="count" />.</exception>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="ascii" /> is invalid based on the <see cref="P:System.Globalization.IdnMapping.AllowUnassigned" /> and <see cref="P:System.Globalization.IdnMapping.UseStd3AsciiRules" /> properties, and the IDNA standard.</exception>
		// Token: 0x06001B40 RID: 6976 RVA: 0x000652D8 File Offset: 0x000634D8
		public string GetUnicode(string ascii, int index, int count)
		{
			if (ascii == null)
			{
				throw new ArgumentNullException("ascii");
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index must be non-negative value");
			}
			if (count < 0 || index + count > ascii.Length)
			{
				throw new ArgumentOutOfRangeException("index + count must point inside the argument ascii string");
			}
			return this.Convert(ascii, index, count, false);
		}

		// Token: 0x06001B41 RID: 6977 RVA: 0x00065334 File Offset: 0x00063534
		private string ToUnicode(string s, int offset)
		{
			for (int i = 0; i < s.Length; i++)
			{
				if (s[i] >= '\u0080')
				{
					s = this.NamePrep(s, offset);
					break;
				}
			}
			if (!s.StartsWith("xn--", StringComparison.OrdinalIgnoreCase))
			{
				return s;
			}
			s = s.ToLower(CultureInfo.InvariantCulture);
			string strA = s;
			s = s.Substring(4);
			s = this.puny.Decode(s, offset);
			string result = s;
			s = this.ToAscii(s, offset);
			if (string.Compare(strA, s, StringComparison.OrdinalIgnoreCase) != 0)
			{
				throw new ArgumentException(string.Format("ToUnicode() failed at verifying the result, at label part from {0}", offset));
			}
			return result;
		}

		// Token: 0x04000A2F RID: 2607
		private bool allow_unassigned;

		// Token: 0x04000A30 RID: 2608
		private bool use_std3;

		// Token: 0x04000A31 RID: 2609
		private Punycode puny = new Punycode();
	}
}
