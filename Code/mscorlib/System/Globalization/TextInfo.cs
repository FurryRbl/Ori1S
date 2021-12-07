using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Text;

namespace System.Globalization
{
	/// <summary>Defines text properties and behaviors, such as casing, that are specific to a writing system. </summary>
	// Token: 0x0200022A RID: 554
	[ComVisible(true)]
	[MonoTODO("IDeserializationCallback isn't implemented.")]
	[Serializable]
	public class TextInfo : ICloneable, IDeserializationCallback
	{
		// Token: 0x06001C6D RID: 7277 RVA: 0x00068C20 File Offset: 0x00066E20
		internal unsafe TextInfo(CultureInfo ci, int lcid, void* data, bool read_only)
		{
			this.m_isReadOnly = read_only;
			this.m_win32LangID = lcid;
			this.ci = ci;
			if (data != null)
			{
				this.data = *(TextInfo.Data*)data;
			}
			else
			{
				this.data = default(TextInfo.Data);
				this.data.list_sep = 44;
			}
			CultureInfo cultureInfo = ci;
			while (cultureInfo.Parent != null && cultureInfo.Parent.LCID != 127 && cultureInfo.Parent != cultureInfo)
			{
				cultureInfo = cultureInfo.Parent;
			}
			if (cultureInfo != null)
			{
				int lcid2 = cultureInfo.LCID;
				if (lcid2 == 31 || lcid2 == 44)
				{
					this.handleDotI = true;
				}
			}
		}

		// Token: 0x06001C6E RID: 7278 RVA: 0x00068CE4 File Offset: 0x00066EE4
		private TextInfo(TextInfo textInfo)
		{
			this.m_win32LangID = textInfo.m_win32LangID;
			this.m_nDataItem = textInfo.m_nDataItem;
			this.m_useUserOverride = textInfo.m_useUserOverride;
			this.m_listSeparator = textInfo.ListSeparator;
			this.customCultureName = textInfo.CultureName;
			this.ci = textInfo.ci;
			this.handleDotI = textInfo.handleDotI;
			this.data = textInfo.data;
		}

		/// <summary>Raises the deserialization event when deserialization is complete.</summary>
		/// <param name="sender">The source of the deserialization event. </param>
		// Token: 0x06001C6F RID: 7279 RVA: 0x00068D58 File Offset: 0x00066F58
		[MonoTODO]
		void IDeserializationCallback.OnDeserialization(object sender)
		{
		}

		/// <summary>Gets the American National Standards Institute (ANSI) code page used by the writing system represented by the current <see cref="T:System.Globalization.TextInfo" />.</summary>
		/// <returns>The ANSI code page used by the writing system represented by the current <see cref="T:System.Globalization.TextInfo" />.</returns>
		// Token: 0x170004C8 RID: 1224
		// (get) Token: 0x06001C70 RID: 7280 RVA: 0x00068D5C File Offset: 0x00066F5C
		public virtual int ANSICodePage
		{
			get
			{
				return this.data.ansi;
			}
		}

		/// <summary>Gets the Extended Binary Coded Decimal Interchange Code (EBCDIC) code page used by the writing system represented by the current <see cref="T:System.Globalization.TextInfo" />.</summary>
		/// <returns>The EBCDIC code page used by the writing system represented by the current <see cref="T:System.Globalization.TextInfo" />.</returns>
		// Token: 0x170004C9 RID: 1225
		// (get) Token: 0x06001C71 RID: 7281 RVA: 0x00068D78 File Offset: 0x00066F78
		public virtual int EBCDICCodePage
		{
			get
			{
				return this.data.ebcdic;
			}
		}

		/// <summary>Gets the culture identifier for the culture associated with the current <see cref="T:System.Globalization.TextInfo" /> object.</summary>
		/// <returns>A number that identifies the culture from which the current <see cref="T:System.Globalization.TextInfo" /> object was created.</returns>
		// Token: 0x170004CA RID: 1226
		// (get) Token: 0x06001C72 RID: 7282 RVA: 0x00068D94 File Offset: 0x00066F94
		[ComVisible(false)]
		public int LCID
		{
			get
			{
				return this.m_win32LangID;
			}
		}

		/// <summary>Gets or sets the string that separates items in a list.</summary>
		/// <returns>The string that separates items in a list.</returns>
		/// <exception cref="T:System.ArgumentNullException">The value in a set operation is null.</exception>
		/// <exception cref="T:System.InvalidOperationException">In a set operation, the current <see cref="T:System.Globalization.TextInfo" /> object is read-only.</exception>
		// Token: 0x170004CB RID: 1227
		// (get) Token: 0x06001C73 RID: 7283 RVA: 0x00068D9C File Offset: 0x00066F9C
		// (set) Token: 0x06001C74 RID: 7284 RVA: 0x00068DD8 File Offset: 0x00066FD8
		public virtual string ListSeparator
		{
			get
			{
				if (this.m_listSeparator == null)
				{
					this.m_listSeparator = ((char)this.data.list_sep).ToString();
				}
				return this.m_listSeparator;
			}
			[ComVisible(false)]
			set
			{
				this.m_listSeparator = value;
			}
		}

		/// <summary>Gets the Macintosh code page used by the writing system represented by the current <see cref="T:System.Globalization.TextInfo" />.</summary>
		/// <returns>The Macintosh code page used by the writing system represented by the current <see cref="T:System.Globalization.TextInfo" />.</returns>
		// Token: 0x170004CC RID: 1228
		// (get) Token: 0x06001C75 RID: 7285 RVA: 0x00068DE4 File Offset: 0x00066FE4
		public virtual int MacCodePage
		{
			get
			{
				return this.data.mac;
			}
		}

		/// <summary>Gets the original equipment manufacturer (OEM) code page used by the writing system represented by the current <see cref="T:System.Globalization.TextInfo" />.</summary>
		/// <returns>The OEM code page used by the writing system represented by the current <see cref="T:System.Globalization.TextInfo" />.</returns>
		// Token: 0x170004CD RID: 1229
		// (get) Token: 0x06001C76 RID: 7286 RVA: 0x00068E00 File Offset: 0x00067000
		public virtual int OEMCodePage
		{
			get
			{
				return this.data.oem;
			}
		}

		/// <summary>Gets the name of the culture associated with the current <see cref="T:System.Globalization.TextInfo" /> object.</summary>
		/// <returns>The name of a culture. </returns>
		// Token: 0x170004CE RID: 1230
		// (get) Token: 0x06001C77 RID: 7287 RVA: 0x00068E1C File Offset: 0x0006701C
		[ComVisible(false)]
		public string CultureName
		{
			get
			{
				if (this.customCultureName == null)
				{
					this.customCultureName = this.ci.Name;
				}
				return this.customCultureName;
			}
		}

		/// <summary>Gets a value indicating whether the current <see cref="T:System.Globalization.TextInfo" /> object is read-only.</summary>
		/// <returns>true if the current <see cref="T:System.Globalization.TextInfo" /> object is read-only; otherwise, false.</returns>
		// Token: 0x170004CF RID: 1231
		// (get) Token: 0x06001C78 RID: 7288 RVA: 0x00068E4C File Offset: 0x0006704C
		[ComVisible(false)]
		public bool IsReadOnly
		{
			get
			{
				return this.m_isReadOnly;
			}
		}

		/// <summary>Gets a value indicating whether the current <see cref="T:System.Globalization.TextInfo" /> object represents a writing system where text flows from right to left.</summary>
		/// <returns>true if text flows from right to left; otherwise, false.</returns>
		// Token: 0x170004D0 RID: 1232
		// (get) Token: 0x06001C79 RID: 7289 RVA: 0x00068E54 File Offset: 0x00067054
		[ComVisible(false)]
		public bool IsRightToLeft
		{
			get
			{
				int win32LangID = this.m_win32LangID;
				return win32LangID == 1 || win32LangID == 13 || win32LangID == 32 || win32LangID == 41 || win32LangID == 90 || win32LangID == 101 || win32LangID == 1025 || win32LangID == 1037 || win32LangID == 1056 || win32LangID == 1065 || win32LangID == 1114 || win32LangID == 1125 || win32LangID == 2049 || win32LangID == 3073 || win32LangID == 4097 || win32LangID == 5121 || win32LangID == 6145 || win32LangID == 7169 || win32LangID == 8193 || win32LangID == 9217 || win32LangID == 10241 || win32LangID == 11265 || win32LangID == 12289 || win32LangID == 13313 || win32LangID == 14337 || win32LangID == 15361 || win32LangID == 16385;
			}
		}

		/// <summary>Determines whether the specified object represents the same writing system as the current <see cref="T:System.Globalization.TextInfo" /> object.</summary>
		/// <returns>true if <paramref name="obj" /> represents the same writing system as the current <see cref="T:System.Globalization.TextInfo" />; otherwise, false.</returns>
		/// <param name="obj">The object to compare with the current <see cref="T:System.Globalization.TextInfo" />. </param>
		// Token: 0x06001C7A RID: 7290 RVA: 0x00068F88 File Offset: 0x00067188
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			TextInfo textInfo = obj as TextInfo;
			return textInfo != null && textInfo.m_win32LangID == this.m_win32LangID && textInfo.ci == this.ci;
		}

		/// <summary>Serves as a hash function for the current <see cref="T:System.Globalization.TextInfo" />, suitable for hashing algorithms and data structures, such as a hash table.</summary>
		/// <returns>A hash code for the current <see cref="T:System.Globalization.TextInfo" />.</returns>
		// Token: 0x06001C7B RID: 7291 RVA: 0x00068FD4 File Offset: 0x000671D4
		public override int GetHashCode()
		{
			return this.m_win32LangID;
		}

		/// <summary>Returns a string that represents the current <see cref="T:System.Globalization.TextInfo" />.</summary>
		/// <returns>A string that represents the current <see cref="T:System.Globalization.TextInfo" />.</returns>
		// Token: 0x06001C7C RID: 7292 RVA: 0x00068FDC File Offset: 0x000671DC
		public override string ToString()
		{
			return "TextInfo - " + this.m_win32LangID;
		}

		/// <summary>Converts the specified string to titlecase.</summary>
		/// <returns>The specified string converted to titlecase.</returns>
		/// <param name="str">The string to convert to titlecase. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="str" /> is null. </exception>
		// Token: 0x06001C7D RID: 7293 RVA: 0x00068FF4 File Offset: 0x000671F4
		public string ToTitleCase(string str)
		{
			if (str == null)
			{
				throw new ArgumentNullException("str");
			}
			StringBuilder stringBuilder = null;
			int i = 0;
			int num = 0;
			while (i < str.Length)
			{
				if (char.IsLetter(str[i++]))
				{
					i--;
					char c = this.ToTitleCase(str[i]);
					bool flag = true;
					if (c == str[i])
					{
						flag = false;
						bool flag2 = true;
						int num2 = i;
						while (++i < str.Length)
						{
							if (char.IsWhiteSpace(str[i]))
							{
								break;
							}
							c = this.ToTitleCase(str[i]);
							if (c != str[i])
							{
								flag2 = false;
								break;
							}
						}
						if (flag2)
						{
							continue;
						}
						i = num2;
						while (++i < str.Length)
						{
							if (char.IsWhiteSpace(str[i]))
							{
								break;
							}
							if (this.ToLower(str[i]) != str[i])
							{
								flag = true;
								i = num2;
								break;
							}
						}
					}
					if (flag)
					{
						if (stringBuilder == null)
						{
							stringBuilder = new StringBuilder(str.Length);
						}
						stringBuilder.Append(str, num, i - num);
						stringBuilder.Append(this.ToTitleCase(str[i]));
						num = i + 1;
						while (++i < str.Length)
						{
							if (char.IsWhiteSpace(str[i]))
							{
								break;
							}
							stringBuilder.Append(this.ToLower(str[i]));
						}
						num = i;
					}
				}
			}
			if (stringBuilder != null)
			{
				stringBuilder.Append(str, num, str.Length - num);
			}
			return (stringBuilder == null) ? str : stringBuilder.ToString();
		}

		/// <summary>Converts the specified character to lowercase.</summary>
		/// <returns>The specified character converted to lowercase.</returns>
		/// <param name="c">The character to convert to lowercase. </param>
		// Token: 0x06001C7E RID: 7294 RVA: 0x000691C4 File Offset: 0x000673C4
		public virtual char ToLower(char c)
		{
			if (c < '@' || ('`' < c && c < '\u0080'))
			{
				return c;
			}
			if ('A' <= c && c <= 'Z' && (!this.handleDotI || c != 'I'))
			{
				return c + ' ';
			}
			if (this.ci == null || this.ci.LCID == 127)
			{
				return char.ToLowerInvariant(c);
			}
			switch (c)
			{
			case 'ǅ':
				return 'ǆ';
			default:
				switch (c)
				{
				case 'ϒ':
					return 'υ';
				case 'ϓ':
					return 'ύ';
				case 'ϔ':
					return 'ϋ';
				default:
					if (c != 'I')
					{
						if (c == 'İ')
						{
							return 'i';
						}
						if (c == 'ǋ')
						{
							return 'ǌ';
						}
						if (c == 'ǲ')
						{
							return 'ǳ';
						}
					}
					else if (this.handleDotI)
					{
						return 'ı';
					}
					return char.ToLowerInvariant(c);
				}
				break;
			case 'ǈ':
				return 'ǉ';
			}
		}

		/// <summary>Converts the specified character to uppercase.</summary>
		/// <returns>The specified character converted to uppercase.</returns>
		/// <param name="c">The character to convert to uppercase. </param>
		// Token: 0x06001C7F RID: 7295 RVA: 0x000692E8 File Offset: 0x000674E8
		public virtual char ToUpper(char c)
		{
			if (c < '`')
			{
				return c;
			}
			if ('a' <= c && c <= 'z' && (!this.handleDotI || c != 'i'))
			{
				return c - ' ';
			}
			if (this.ci == null || this.ci.LCID == 127)
			{
				return char.ToUpperInvariant(c);
			}
			switch (c)
			{
			case 'ϐ':
				return 'Β';
			case 'ϑ':
				return 'Θ';
			default:
				switch (c)
				{
				case 'ǅ':
					return 'Ǆ';
				default:
					if (c == 'ϰ')
					{
						return 'Κ';
					}
					if (c != 'ϱ')
					{
						if (c != 'i')
						{
							if (c == 'ı')
							{
								return 'I';
							}
							if (c == 'ǋ')
							{
								return 'Ǌ';
							}
							if (c == 'ǲ')
							{
								return 'Ǳ';
							}
							if (c == 'ΐ')
							{
								return 'Ϊ';
							}
							if (c == 'ΰ')
							{
								return 'Ϋ';
							}
						}
						else if (this.handleDotI)
						{
							return 'İ';
						}
						return char.ToUpperInvariant(c);
					}
					return 'Ρ';
				case 'ǈ':
					return 'Ǉ';
				}
				break;
			case 'ϕ':
				return 'Φ';
			case 'ϖ':
				return 'Π';
			}
		}

		// Token: 0x06001C80 RID: 7296 RVA: 0x00069454 File Offset: 0x00067654
		private char ToTitleCase(char c)
		{
			switch (c)
			{
			case 'Ǆ':
			case 'ǅ':
			case 'ǆ':
				return 'ǅ';
			case 'Ǉ':
			case 'ǈ':
			case 'ǉ':
				return 'ǈ';
			case 'Ǌ':
			case 'ǋ':
			case 'ǌ':
				return 'ǋ';
			default:
				switch (c)
				{
				case 'Ǳ':
				case 'ǲ':
				case 'ǳ':
					return 'ǲ';
				default:
					if (('ⅰ' <= c && c <= 'ⅿ') || ('ⓐ' <= c && c <= 'ⓩ'))
					{
						return c;
					}
					return this.ToUpper(c);
				}
				break;
			}
		}

		/// <summary>Converts the specified string to lowercase.</summary>
		/// <returns>The specified string converted to lowercase.</returns>
		/// <param name="str">The string to convert to lowercase. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="str" /> is null. </exception>
		// Token: 0x06001C81 RID: 7297 RVA: 0x00069500 File Offset: 0x00067700
		public unsafe virtual string ToLower(string str)
		{
			if (str == null)
			{
				throw new ArgumentNullException("str");
			}
			if (str.Length == 0)
			{
				return string.Empty;
			}
			string text = string.InternalAllocateStr(str.Length);
			fixed (string text2 = str)
			{
				fixed (char* ptr = text2 + RuntimeHelpers.OffsetToStringData / 2)
				{
					fixed (string text3 = text)
					{
						fixed (char* ptr2 = text3 + RuntimeHelpers.OffsetToStringData / 2)
						{
							char* ptr3 = ptr2;
							char* ptr4 = ptr;
							for (int i = 0; i < str.Length; i++)
							{
								*ptr3 = this.ToLower(*ptr4);
								ptr4++;
								ptr3++;
							}
							text2 = null;
							text3 = null;
							return text;
						}
					}
				}
			}
		}

		/// <summary>Converts the specified string to uppercase.</summary>
		/// <returns>The specified string converted to uppercase.</returns>
		/// <param name="str">The string to convert to uppercase. </param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="str" /> is null. </exception>
		// Token: 0x06001C82 RID: 7298 RVA: 0x00069594 File Offset: 0x00067794
		public unsafe virtual string ToUpper(string str)
		{
			if (str == null)
			{
				throw new ArgumentNullException("str");
			}
			if (str.Length == 0)
			{
				return string.Empty;
			}
			string text = string.InternalAllocateStr(str.Length);
			fixed (string text2 = str)
			{
				fixed (char* ptr = text2 + RuntimeHelpers.OffsetToStringData / 2)
				{
					fixed (string text3 = text)
					{
						fixed (char* ptr2 = text3 + RuntimeHelpers.OffsetToStringData / 2)
						{
							char* ptr3 = ptr2;
							char* ptr4 = ptr;
							for (int i = 0; i < str.Length; i++)
							{
								*ptr3 = this.ToUpper(*ptr4);
								ptr4++;
								ptr3++;
							}
							text2 = null;
							text3 = null;
							return text;
						}
					}
				}
			}
		}

		/// <summary>Returns a read-only version of the specified <see cref="T:System.Globalization.TextInfo" /> object.</summary>
		/// <returns>The <see cref="T:System.Globalization.TextInfo" /> object specified by the <paramref name="textInfo" /> parameter, if <paramref name="textInfo" /> is read-only.-or-A read-only memberwise clone of the <see cref="T:System.Globalization.TextInfo" /> object specified by <paramref name="textInfo" />, if <paramref name="textInfo" /> is not read-only.</returns>
		/// <param name="textInfo">A <see cref="T:System.Globalization.TextInfo" /> object.</param>
		/// <exception cref="T:System.ArgumentNullException">
		///   <paramref name="textInfo" /> is null.</exception>
		// Token: 0x06001C83 RID: 7299 RVA: 0x00069628 File Offset: 0x00067828
		[ComVisible(false)]
		public static TextInfo ReadOnly(TextInfo textInfo)
		{
			if (textInfo == null)
			{
				throw new ArgumentNullException("textInfo");
			}
			return new TextInfo(textInfo)
			{
				m_isReadOnly = true
			};
		}

		/// <summary>Creates a new object that is a copy of the current <see cref="T:System.Globalization.TextInfo" /> object.</summary>
		/// <returns>A new instance of <see cref="T:System.Object" /> that is the memberwise clone of the current <see cref="T:System.Globalization.TextInfo" /> object.</returns>
		// Token: 0x06001C84 RID: 7300 RVA: 0x00069658 File Offset: 0x00067858
		[ComVisible(false)]
		public virtual object Clone()
		{
			return new TextInfo(this);
		}

		// Token: 0x04000AA4 RID: 2724
		private string m_listSeparator;

		// Token: 0x04000AA5 RID: 2725
		private bool m_isReadOnly;

		// Token: 0x04000AA6 RID: 2726
		private string customCultureName;

		// Token: 0x04000AA7 RID: 2727
		[NonSerialized]
		private int m_nDataItem;

		// Token: 0x04000AA8 RID: 2728
		private bool m_useUserOverride;

		// Token: 0x04000AA9 RID: 2729
		private int m_win32LangID;

		// Token: 0x04000AAA RID: 2730
		[NonSerialized]
		private readonly CultureInfo ci;

		// Token: 0x04000AAB RID: 2731
		[NonSerialized]
		private readonly bool handleDotI;

		// Token: 0x04000AAC RID: 2732
		[NonSerialized]
		private readonly TextInfo.Data data;

		// Token: 0x0200022B RID: 555
		private struct Data
		{
			// Token: 0x04000AAD RID: 2733
			public int ansi;

			// Token: 0x04000AAE RID: 2734
			public int ebcdic;

			// Token: 0x04000AAF RID: 2735
			public int mac;

			// Token: 0x04000AB0 RID: 2736
			public int oem;

			// Token: 0x04000AB1 RID: 2737
			public byte list_sep;
		}
	}
}
