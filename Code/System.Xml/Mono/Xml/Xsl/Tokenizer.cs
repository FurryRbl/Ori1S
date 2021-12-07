using System;
using System.Collections;
using System.Globalization;
using System.Text;
using System.Xml;
using System.Xml.XPath;
using Mono.Xml.Xsl.yyParser;

namespace Mono.Xml.Xsl
{
	// Token: 0x02000012 RID: 18
	internal class Tokenizer : yyInput
	{
		// Token: 0x0600005D RID: 93 RVA: 0x00005DC4 File Offset: 0x00003FC4
		public Tokenizer(string strInput)
		{
			this.m_rgchInput = strInput;
			this.m_ich = 0;
			this.m_cch = strInput.Length;
			this.SkipWhitespace();
		}

		// Token: 0x0600005E RID: 94 RVA: 0x00005E04 File Offset: 0x00004004
		static Tokenizer()
		{
			for (int i = 0; i < Tokenizer.s_rgTokenMap.Length; i += 2)
			{
				Tokenizer.s_mapTokens.Add(Tokenizer.s_rgTokenMap[i + 1], Tokenizer.s_rgTokenMap[i]);
			}
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00006034 File Offset: 0x00004234
		private char Peek(int iOffset)
		{
			if (this.m_ich + iOffset >= this.m_cch)
			{
				return '\0';
			}
			return this.m_rgchInput[this.m_ich + iOffset];
		}

		// Token: 0x06000060 RID: 96 RVA: 0x0000606C File Offset: 0x0000426C
		private char Peek()
		{
			return this.Peek(0);
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00006078 File Offset: 0x00004278
		private char GetChar()
		{
			if (this.m_ich >= this.m_cch)
			{
				return '\0';
			}
			return this.m_rgchInput[this.m_ich++];
		}

		// Token: 0x06000062 RID: 98 RVA: 0x000060B4 File Offset: 0x000042B4
		private char PutBack()
		{
			if (this.m_ich == 0)
			{
				throw new XPathException("XPath parser returned an error status: invalid tokenizer state.");
			}
			return this.m_rgchInput[--this.m_ich];
		}

		// Token: 0x06000063 RID: 99 RVA: 0x000060F4 File Offset: 0x000042F4
		private bool SkipWhitespace()
		{
			if (!Tokenizer.IsWhitespace(this.Peek()))
			{
				return false;
			}
			while (Tokenizer.IsWhitespace(this.Peek()))
			{
				this.GetChar();
			}
			return true;
		}

		// Token: 0x06000064 RID: 100 RVA: 0x00006130 File Offset: 0x00004330
		private int ParseNumber()
		{
			StringBuilder stringBuilder = new StringBuilder();
			while (Tokenizer.IsDigit(this.Peek()))
			{
				stringBuilder.Append(this.GetChar());
			}
			if (this.Peek() == '.')
			{
				stringBuilder.Append(this.GetChar());
				while (Tokenizer.IsDigit(this.Peek()))
				{
					stringBuilder.Append(this.GetChar());
				}
			}
			this.m_objToken = double.Parse(stringBuilder.ToString(), NumberFormatInfo.InvariantInfo);
			return 331;
		}

		// Token: 0x06000065 RID: 101 RVA: 0x000061C4 File Offset: 0x000043C4
		private int ParseLiteral()
		{
			StringBuilder stringBuilder = new StringBuilder();
			char @char = this.GetChar();
			char c;
			while ((c = this.Peek()) != @char)
			{
				if (c == '\0')
				{
					throw new XPathException("unmatched " + @char + " in expression");
				}
				stringBuilder.Append(this.GetChar());
			}
			this.GetChar();
			this.m_objToken = stringBuilder.ToString();
			return 332;
		}

		// Token: 0x06000066 RID: 102 RVA: 0x00006238 File Offset: 0x00004438
		private string ReadIdentifier()
		{
			StringBuilder stringBuilder = new StringBuilder();
			char c = this.Peek();
			if (!char.IsLetter(c) && c != '_')
			{
				return null;
			}
			stringBuilder.Append(this.GetChar());
			while ((c = this.Peek()) == '_' || c == '-' || c == '.' || char.IsLetterOrDigit(c))
			{
				stringBuilder.Append(this.GetChar());
			}
			this.SkipWhitespace();
			return stringBuilder.ToString();
		}

		// Token: 0x06000067 RID: 103 RVA: 0x000062C0 File Offset: 0x000044C0
		private int ParseIdentifier()
		{
			string text = this.ReadIdentifier();
			object obj = Tokenizer.s_mapTokens[text];
			int num = (obj == null) ? 333 : ((int)obj);
			this.m_objToken = text;
			char c = this.Peek();
			if (c == ':')
			{
				if (this.Peek(1) == ':')
				{
					if (obj == null || !this.IsAxisName(num))
					{
						throw new XPathException("invalid axis name: '" + text + "'");
					}
					return num;
				}
				else
				{
					this.GetChar();
					this.SkipWhitespace();
					c = this.Peek();
					if (c == '*')
					{
						this.GetChar();
						this.m_objToken = new XmlQualifiedName(string.Empty, text);
						return 333;
					}
					string text2 = this.ReadIdentifier();
					if (text2 == null)
					{
						throw new XPathException(string.Concat(new object[]
						{
							"invalid QName: ",
							text,
							":",
							c
						}));
					}
					c = this.Peek();
					this.m_objToken = new XmlQualifiedName(text2, text);
					if (c == '(')
					{
						return 269;
					}
					return 333;
				}
			}
			else if (!this.IsFirstToken && !this.m_fPrevWasOperator)
			{
				if (obj == null || !this.IsOperatorName(num))
				{
					throw new XPathException("invalid operator name: '" + text + "'");
				}
				return num;
			}
			else
			{
				if (c != '(')
				{
					this.m_objToken = new XmlQualifiedName(text, string.Empty);
					return 333;
				}
				if (obj == null)
				{
					this.m_objToken = new XmlQualifiedName(text, string.Empty);
					return 269;
				}
				if (this.IsNodeType(num))
				{
					return num;
				}
				throw new XPathException("invalid function name: '" + text + "'");
			}
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00006488 File Offset: 0x00004688
		private static bool IsWhitespace(char ch)
		{
			return ch == ' ' || ch == '\t' || ch == '\n' || ch == '\r';
		}

		// Token: 0x06000069 RID: 105 RVA: 0x000064B8 File Offset: 0x000046B8
		private static bool IsDigit(char ch)
		{
			return ch >= '0' && ch <= '9';
		}

		// Token: 0x0600006A RID: 106 RVA: 0x000064D0 File Offset: 0x000046D0
		private int ParseToken()
		{
			char c = this.Peek();
			char c2 = c;
			switch (c2)
			{
			case '!':
				this.GetChar();
				if (this.Peek() == '=')
				{
					this.m_fThisIsOperator = true;
					this.GetChar();
					return 288;
				}
				break;
			case '"':
				return this.ParseLiteral();
			default:
				switch (c2)
				{
				case '[':
					this.m_fThisIsOperator = true;
					this.GetChar();
					return 270;
				default:
					if (c2 == '\0')
					{
						return 258;
					}
					if (c2 == '|')
					{
						this.m_fThisIsOperator = true;
						this.GetChar();
						return 286;
					}
					if (Tokenizer.IsDigit(c))
					{
						return this.ParseNumber();
					}
					if (char.IsLetter(c) || c == '_')
					{
						int num = this.ParseIdentifier();
						if (this.IsOperatorName(num))
						{
							this.m_fThisIsOperator = true;
						}
						return num;
					}
					break;
				case ']':
					this.GetChar();
					return 271;
				}
				break;
			case '$':
				this.GetChar();
				this.m_fThisIsOperator = true;
				return 285;
			case '\'':
				return this.ParseLiteral();
			case '(':
				this.m_fThisIsOperator = true;
				this.GetChar();
				return 272;
			case ')':
				this.GetChar();
				return 273;
			case '*':
				this.GetChar();
				if (!this.IsFirstToken && !this.m_fPrevWasOperator)
				{
					this.m_fThisIsOperator = true;
					return 330;
				}
				return 284;
			case '+':
				this.m_fThisIsOperator = true;
				this.GetChar();
				return 282;
			case ',':
				this.m_fThisIsOperator = true;
				this.GetChar();
				return 267;
			case '-':
				this.m_fThisIsOperator = true;
				this.GetChar();
				return 283;
			case '.':
				this.GetChar();
				if (this.Peek() == '.')
				{
					this.GetChar();
					return 263;
				}
				if (Tokenizer.IsDigit(this.Peek()))
				{
					this.PutBack();
					return this.ParseNumber();
				}
				return 262;
			case '/':
				this.m_fThisIsOperator = true;
				this.GetChar();
				if (this.Peek() == '/')
				{
					this.GetChar();
					return 260;
				}
				return 259;
			case ':':
				this.GetChar();
				if (this.Peek() == ':')
				{
					this.m_fThisIsOperator = true;
					this.GetChar();
					return 265;
				}
				return 257;
			case '<':
				this.m_fThisIsOperator = true;
				this.GetChar();
				if (this.Peek() == '=')
				{
					this.GetChar();
					return 290;
				}
				return 294;
			case '=':
				this.m_fThisIsOperator = true;
				this.GetChar();
				return 287;
			case '>':
				this.m_fThisIsOperator = true;
				this.GetChar();
				if (this.Peek() == '=')
				{
					this.GetChar();
					return 292;
				}
				return 295;
			case '@':
				this.m_fThisIsOperator = true;
				this.GetChar();
				return 268;
			}
			throw new XPathException("invalid token: '" + c + "'");
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00006834 File Offset: 0x00004A34
		public bool advance()
		{
			this.m_fThisIsOperator = false;
			this.m_objToken = null;
			this.m_iToken = this.ParseToken();
			this.SkipWhitespace();
			this.m_iTokenPrev = this.m_iToken;
			this.m_fPrevWasOperator = this.m_fThisIsOperator;
			return this.m_iToken != 258;
		}

		// Token: 0x0600006C RID: 108 RVA: 0x0000688C File Offset: 0x00004A8C
		public int token()
		{
			return this.m_iToken;
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00006894 File Offset: 0x00004A94
		public object value()
		{
			return this.m_objToken;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600006E RID: 110 RVA: 0x0000689C File Offset: 0x00004A9C
		private bool IsFirstToken
		{
			get
			{
				return this.m_iTokenPrev == 258;
			}
		}

		// Token: 0x0600006F RID: 111 RVA: 0x000068AC File Offset: 0x00004AAC
		private bool IsNodeType(int iToken)
		{
			switch (iToken)
			{
			case 322:
			case 324:
			case 326:
			case 328:
				return true;
			}
			return false;
		}

		// Token: 0x06000070 RID: 112 RVA: 0x000068EC File Offset: 0x00004AEC
		private bool IsOperatorName(int iToken)
		{
			switch (iToken)
			{
			case 274:
			case 276:
			case 278:
			case 280:
				return true;
			}
			return false;
		}

		// Token: 0x06000071 RID: 113 RVA: 0x0000692C File Offset: 0x00004B2C
		private bool IsAxisName(int iToken)
		{
			switch (iToken)
			{
			case 296:
			case 298:
			case 300:
			case 302:
			case 304:
			case 306:
			case 308:
			case 310:
			case 312:
			case 314:
			case 316:
			case 318:
			case 320:
				return true;
			}
			return false;
		}

		// Token: 0x040000AF RID: 175
		private const char EOL = '\0';

		// Token: 0x040000B0 RID: 176
		private string m_rgchInput;

		// Token: 0x040000B1 RID: 177
		private int m_ich;

		// Token: 0x040000B2 RID: 178
		private int m_cch;

		// Token: 0x040000B3 RID: 179
		private int m_iToken;

		// Token: 0x040000B4 RID: 180
		private int m_iTokenPrev = 258;

		// Token: 0x040000B5 RID: 181
		private object m_objToken;

		// Token: 0x040000B6 RID: 182
		private bool m_fPrevWasOperator;

		// Token: 0x040000B7 RID: 183
		private bool m_fThisIsOperator;

		// Token: 0x040000B8 RID: 184
		private static readonly Hashtable s_mapTokens = new Hashtable();

		// Token: 0x040000B9 RID: 185
		private static readonly object[] s_rgTokenMap = new object[]
		{
			274,
			"and",
			276,
			"or",
			278,
			"div",
			280,
			"mod",
			296,
			"ancestor",
			298,
			"ancestor-or-self",
			300,
			"attribute",
			302,
			"child",
			304,
			"descendant",
			306,
			"descendant-or-self",
			308,
			"following",
			310,
			"following-sibling",
			312,
			"namespace",
			314,
			"parent",
			316,
			"preceding",
			318,
			"preceding-sibling",
			320,
			"self",
			322,
			"comment",
			324,
			"text",
			326,
			"processing-instruction",
			328,
			"node"
		};
	}
}
