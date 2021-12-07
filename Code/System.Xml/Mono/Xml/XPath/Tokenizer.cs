using System;
using System.Collections;
using System.Globalization;
using System.Text;
using System.Xml;
using System.Xml.XPath;
using Mono.Xml.XPath.yyParser;

namespace Mono.Xml.XPath
{
	// Token: 0x020001AB RID: 427
	internal class Tokenizer : yyInput
	{
		// Token: 0x0600116B RID: 4459 RVA: 0x0004FE44 File Offset: 0x0004E044
		public Tokenizer(string strInput)
		{
			this.m_rgchInput = strInput;
			this.m_ich = 0;
			this.m_cch = strInput.Length;
			this.SkipWhitespace();
		}

		// Token: 0x0600116C RID: 4460 RVA: 0x0004FE84 File Offset: 0x0004E084
		static Tokenizer()
		{
			for (int i = 0; i < Tokenizer.s_rgTokenMap.Length; i += 2)
			{
				Tokenizer.s_mapTokens.Add(Tokenizer.s_rgTokenMap[i + 1], Tokenizer.s_rgTokenMap[i]);
			}
		}

		// Token: 0x0600116D RID: 4461 RVA: 0x000500B4 File Offset: 0x0004E2B4
		private char Peek(int iOffset)
		{
			if (this.m_ich + iOffset >= this.m_cch)
			{
				return '\0';
			}
			return this.m_rgchInput[this.m_ich + iOffset];
		}

		// Token: 0x0600116E RID: 4462 RVA: 0x000500EC File Offset: 0x0004E2EC
		private char Peek()
		{
			return this.Peek(0);
		}

		// Token: 0x0600116F RID: 4463 RVA: 0x000500F8 File Offset: 0x0004E2F8
		private char GetChar()
		{
			if (this.m_ich >= this.m_cch)
			{
				return '\0';
			}
			return this.m_rgchInput[this.m_ich++];
		}

		// Token: 0x06001170 RID: 4464 RVA: 0x00050134 File Offset: 0x0004E334
		private char PutBack()
		{
			if (this.m_ich == 0)
			{
				throw new XPathException("XPath parser returned an error status: invalid tokenizer state.");
			}
			return this.m_rgchInput[--this.m_ich];
		}

		// Token: 0x06001171 RID: 4465 RVA: 0x00050174 File Offset: 0x0004E374
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

		// Token: 0x06001172 RID: 4466 RVA: 0x000501B0 File Offset: 0x0004E3B0
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

		// Token: 0x06001173 RID: 4467 RVA: 0x00050244 File Offset: 0x0004E444
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

		// Token: 0x06001174 RID: 4468 RVA: 0x000502B8 File Offset: 0x0004E4B8
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

		// Token: 0x06001175 RID: 4469 RVA: 0x00050340 File Offset: 0x0004E540
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

		// Token: 0x06001176 RID: 4470 RVA: 0x00050508 File Offset: 0x0004E708
		private static bool IsWhitespace(char ch)
		{
			return ch == ' ' || ch == '\t' || ch == '\n' || ch == '\r';
		}

		// Token: 0x06001177 RID: 4471 RVA: 0x00050538 File Offset: 0x0004E738
		private static bool IsDigit(char ch)
		{
			return ch >= '0' && ch <= '9';
		}

		// Token: 0x06001178 RID: 4472 RVA: 0x00050550 File Offset: 0x0004E750
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

		// Token: 0x06001179 RID: 4473 RVA: 0x000508B4 File Offset: 0x0004EAB4
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

		// Token: 0x0600117A RID: 4474 RVA: 0x0005090C File Offset: 0x0004EB0C
		public int token()
		{
			return this.m_iToken;
		}

		// Token: 0x0600117B RID: 4475 RVA: 0x00050914 File Offset: 0x0004EB14
		public object value()
		{
			return this.m_objToken;
		}

		// Token: 0x17000512 RID: 1298
		// (get) Token: 0x0600117C RID: 4476 RVA: 0x0005091C File Offset: 0x0004EB1C
		private bool IsFirstToken
		{
			get
			{
				return this.m_iTokenPrev == 258;
			}
		}

		// Token: 0x0600117D RID: 4477 RVA: 0x0005092C File Offset: 0x0004EB2C
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

		// Token: 0x0600117E RID: 4478 RVA: 0x0005096C File Offset: 0x0004EB6C
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

		// Token: 0x0600117F RID: 4479 RVA: 0x000509AC File Offset: 0x0004EBAC
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

		// Token: 0x04000742 RID: 1858
		private const char EOL = '\0';

		// Token: 0x04000743 RID: 1859
		private string m_rgchInput;

		// Token: 0x04000744 RID: 1860
		private int m_ich;

		// Token: 0x04000745 RID: 1861
		private int m_cch;

		// Token: 0x04000746 RID: 1862
		private int m_iToken;

		// Token: 0x04000747 RID: 1863
		private int m_iTokenPrev = 258;

		// Token: 0x04000748 RID: 1864
		private object m_objToken;

		// Token: 0x04000749 RID: 1865
		private bool m_fPrevWasOperator;

		// Token: 0x0400074A RID: 1866
		private bool m_fThisIsOperator;

		// Token: 0x0400074B RID: 1867
		private static readonly Hashtable s_mapTokens = new Hashtable();

		// Token: 0x0400074C RID: 1868
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
