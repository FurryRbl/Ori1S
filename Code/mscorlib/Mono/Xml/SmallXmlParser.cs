using System;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Text;

namespace Mono.Xml
{
	// Token: 0x020000ED RID: 237
	internal class SmallXmlParser
	{
		// Token: 0x06000C1F RID: 3103 RVA: 0x0003756C File Offset: 0x0003576C
		private Exception Error(string msg)
		{
			return new SmallXmlParserException(msg, this.line, this.column);
		}

		// Token: 0x06000C20 RID: 3104 RVA: 0x00037580 File Offset: 0x00035780
		private Exception UnexpectedEndError()
		{
			string[] array = new string[this.elementNames.Count];
			this.elementNames.CopyTo(array, 0);
			return this.Error(string.Format("Unexpected end of stream. Element stack content is {0}", string.Join(",", array)));
		}

		// Token: 0x06000C21 RID: 3105 RVA: 0x000375C8 File Offset: 0x000357C8
		private bool IsNameChar(char c, bool start)
		{
			if (c == '-' || c == '.')
			{
				return !start;
			}
			if (c == ':' || c == '_')
			{
				return true;
			}
			if (c > 'Ā')
			{
				if (c == 'ۥ' || c == 'ۦ' || c == 'ՙ')
				{
					return true;
				}
				if ('ʻ' <= c && c <= 'ˁ')
				{
					return true;
				}
			}
			switch (char.GetUnicodeCategory(c))
			{
			case UnicodeCategory.UppercaseLetter:
			case UnicodeCategory.LowercaseLetter:
			case UnicodeCategory.TitlecaseLetter:
			case UnicodeCategory.OtherLetter:
			case UnicodeCategory.LetterNumber:
				return true;
			case UnicodeCategory.ModifierLetter:
			case UnicodeCategory.NonSpacingMark:
			case UnicodeCategory.SpacingCombiningMark:
			case UnicodeCategory.EnclosingMark:
			case UnicodeCategory.DecimalDigitNumber:
				return !start;
			default:
				return false;
			}
		}

		// Token: 0x06000C22 RID: 3106 RVA: 0x00037694 File Offset: 0x00035894
		private bool IsWhitespace(int c)
		{
			switch (c)
			{
			case 9:
			case 10:
			case 13:
				break;
			default:
				if (c != 32)
				{
					return false;
				}
				break;
			}
			return true;
		}

		// Token: 0x06000C23 RID: 3107 RVA: 0x000376D0 File Offset: 0x000358D0
		public void SkipWhitespaces()
		{
			this.SkipWhitespaces(false);
		}

		// Token: 0x06000C24 RID: 3108 RVA: 0x000376DC File Offset: 0x000358DC
		private void HandleWhitespaces()
		{
			while (this.IsWhitespace(this.Peek()))
			{
				this.buffer.Append((char)this.Read());
			}
			if (this.Peek() != 60 && this.Peek() >= 0)
			{
				this.isWhitespace = false;
			}
		}

		// Token: 0x06000C25 RID: 3109 RVA: 0x00037734 File Offset: 0x00035934
		public void SkipWhitespaces(bool expected)
		{
			for (;;)
			{
				int num = this.Peek();
				switch (num)
				{
				case 9:
				case 10:
				case 13:
					break;
				default:
					if (num != 32)
					{
						goto Block_0;
					}
					break;
				}
				this.Read();
				if (expected)
				{
					expected = false;
				}
			}
			Block_0:
			if (expected)
			{
				throw this.Error("Whitespace is expected.");
			}
		}

		// Token: 0x06000C26 RID: 3110 RVA: 0x000377A0 File Offset: 0x000359A0
		private int Peek()
		{
			return this.reader.Peek();
		}

		// Token: 0x06000C27 RID: 3111 RVA: 0x000377B0 File Offset: 0x000359B0
		private int Read()
		{
			int num = this.reader.Read();
			if (num == 10)
			{
				this.resetColumn = true;
			}
			if (this.resetColumn)
			{
				this.line++;
				this.resetColumn = false;
				this.column = 1;
			}
			else
			{
				this.column++;
			}
			return num;
		}

		// Token: 0x06000C28 RID: 3112 RVA: 0x00037814 File Offset: 0x00035A14
		public void Expect(int c)
		{
			int num = this.Read();
			if (num < 0)
			{
				throw this.UnexpectedEndError();
			}
			if (num != c)
			{
				throw this.Error(string.Format("Expected '{0}' but got {1}", (char)c, (char)num));
			}
		}

		// Token: 0x06000C29 RID: 3113 RVA: 0x0003785C File Offset: 0x00035A5C
		private string ReadUntil(char until, bool handleReferences)
		{
			while (this.Peek() >= 0)
			{
				char c = (char)this.Read();
				if (c == until)
				{
					string result = this.buffer.ToString();
					this.buffer.Length = 0;
					return result;
				}
				if (handleReferences && c == '&')
				{
					this.ReadReference();
				}
				else
				{
					this.buffer.Append(c);
				}
			}
			throw this.UnexpectedEndError();
		}

		// Token: 0x06000C2A RID: 3114 RVA: 0x000378D4 File Offset: 0x00035AD4
		public string ReadName()
		{
			int num = 0;
			if (this.Peek() < 0 || !this.IsNameChar((char)this.Peek(), true))
			{
				throw this.Error("XML name start character is expected.");
			}
			for (int i = this.Peek(); i >= 0; i = this.Peek())
			{
				char c = (char)i;
				if (!this.IsNameChar(c, false))
				{
					break;
				}
				if (num == this.nameBuffer.Length)
				{
					char[] destinationArray = new char[num * 2];
					Array.Copy(this.nameBuffer, destinationArray, num);
					this.nameBuffer = destinationArray;
				}
				this.nameBuffer[num++] = c;
				this.Read();
			}
			if (num == 0)
			{
				throw this.Error("Valid XML name is expected.");
			}
			return new string(this.nameBuffer, 0, num);
		}

		// Token: 0x06000C2B RID: 3115 RVA: 0x0003799C File Offset: 0x00035B9C
		public void Parse(TextReader input, SmallXmlParser.IContentHandler handler)
		{
			this.reader = input;
			this.handler = handler;
			handler.OnStartParsing(this);
			while (this.Peek() >= 0)
			{
				this.ReadContent();
			}
			this.HandleBufferedContent();
			if (this.elementNames.Count > 0)
			{
				throw this.Error(string.Format("Insufficient close tag: {0}", this.elementNames.Peek()));
			}
			handler.OnEndParsing(this);
			this.Cleanup();
		}

		// Token: 0x06000C2C RID: 3116 RVA: 0x00037A18 File Offset: 0x00035C18
		private void Cleanup()
		{
			this.line = 1;
			this.column = 0;
			this.handler = null;
			this.reader = null;
			this.elementNames.Clear();
			this.xmlSpaces.Clear();
			this.attributes.Clear();
			this.buffer.Length = 0;
			this.xmlSpace = null;
			this.isWhitespace = false;
		}

		// Token: 0x06000C2D RID: 3117 RVA: 0x00037A7C File Offset: 0x00035C7C
		public void ReadContent()
		{
			if (this.IsWhitespace(this.Peek()))
			{
				if (this.buffer.Length == 0)
				{
					this.isWhitespace = true;
				}
				this.HandleWhitespaces();
			}
			if (this.Peek() != 60)
			{
				this.ReadCharacters();
				return;
			}
			this.Read();
			int num = this.Peek();
			if (num != 33)
			{
				if (num != 47)
				{
					string text;
					if (num != 63)
					{
						this.HandleBufferedContent();
						text = this.ReadName();
						while (this.Peek() != 62 && this.Peek() != 47)
						{
							this.ReadAttribute(this.attributes);
						}
						this.handler.OnStartElement(text, this.attributes);
						this.attributes.Clear();
						this.SkipWhitespaces();
						if (this.Peek() == 47)
						{
							this.Read();
							this.handler.OnEndElement(text);
						}
						else
						{
							this.elementNames.Push(text);
							this.xmlSpaces.Push(this.xmlSpace);
						}
						this.Expect(62);
						return;
					}
					this.HandleBufferedContent();
					this.Read();
					text = this.ReadName();
					this.SkipWhitespaces();
					string text2 = string.Empty;
					if (this.Peek() != 63)
					{
						for (;;)
						{
							text2 += this.ReadUntil('?', false);
							if (this.Peek() == 62)
							{
								break;
							}
							text2 += "?";
						}
					}
					this.handler.OnProcessingInstruction(text, text2);
					this.Expect(62);
					return;
				}
				else
				{
					this.HandleBufferedContent();
					if (this.elementNames.Count == 0)
					{
						throw this.UnexpectedEndError();
					}
					this.Read();
					string text = this.ReadName();
					this.SkipWhitespaces();
					string text3 = (string)this.elementNames.Pop();
					this.xmlSpaces.Pop();
					if (this.xmlSpaces.Count > 0)
					{
						this.xmlSpace = (string)this.xmlSpaces.Peek();
					}
					else
					{
						this.xmlSpace = null;
					}
					if (text != text3)
					{
						throw this.Error(string.Format("End tag mismatch: expected {0} but found {1}", text3, text));
					}
					this.handler.OnEndElement(text);
					this.Expect(62);
					return;
				}
			}
			else
			{
				this.Read();
				if (this.Peek() == 91)
				{
					this.Read();
					if (this.ReadName() != "CDATA")
					{
						throw this.Error("Invalid declaration markup");
					}
					this.Expect(91);
					this.ReadCDATASection();
					return;
				}
				else
				{
					if (this.Peek() == 45)
					{
						this.ReadComment();
						return;
					}
					if (this.ReadName() != "DOCTYPE")
					{
						throw this.Error("Invalid declaration markup.");
					}
					throw this.Error("This parser does not support document type.");
				}
			}
		}

		// Token: 0x06000C2E RID: 3118 RVA: 0x00037D54 File Offset: 0x00035F54
		private void HandleBufferedContent()
		{
			if (this.buffer.Length == 0)
			{
				return;
			}
			if (this.isWhitespace)
			{
				this.handler.OnIgnorableWhitespace(this.buffer.ToString());
			}
			else
			{
				this.handler.OnChars(this.buffer.ToString());
			}
			this.buffer.Length = 0;
			this.isWhitespace = false;
		}

		// Token: 0x06000C2F RID: 3119 RVA: 0x00037DC4 File Offset: 0x00035FC4
		private void ReadCharacters()
		{
			this.isWhitespace = false;
			for (;;)
			{
				int num = this.Peek();
				int num2 = num;
				if (num2 == -1)
				{
					break;
				}
				if (num2 != 38)
				{
					if (num2 == 60)
					{
						return;
					}
					this.buffer.Append((char)this.Read());
				}
				else
				{
					this.Read();
					this.ReadReference();
				}
			}
		}

		// Token: 0x06000C30 RID: 3120 RVA: 0x00037E30 File Offset: 0x00036030
		private void ReadReference()
		{
			if (this.Peek() != 35)
			{
				string text = this.ReadName();
				this.Expect(59);
				string text2 = text;
				switch (text2)
				{
				case "amp":
					this.buffer.Append('&');
					return;
				case "quot":
					this.buffer.Append('"');
					return;
				case "apos":
					this.buffer.Append('\'');
					return;
				case "lt":
					this.buffer.Append('<');
					return;
				case "gt":
					this.buffer.Append('>');
					return;
				}
				throw this.Error("General non-predefined entity reference is not supported in this parser.");
			}
			this.Read();
			this.ReadCharacterReference();
		}

		// Token: 0x06000C31 RID: 3121 RVA: 0x00037F64 File Offset: 0x00036164
		private int ReadCharacterReference()
		{
			int num = 0;
			if (this.Peek() == 120)
			{
				this.Read();
				for (int i = this.Peek(); i >= 0; i = this.Peek())
				{
					if (48 <= i && i <= 57)
					{
						num <<= 4 + i - 48;
					}
					else if (65 <= i && i <= 70)
					{
						num <<= 4 + i - 65 + 10;
					}
					else
					{
						if (97 > i || i > 102)
						{
							break;
						}
						num <<= 4 + i - 97 + 10;
					}
					this.Read();
				}
			}
			else
			{
				for (int j = this.Peek(); j >= 0; j = this.Peek())
				{
					if (48 > j || j > 57)
					{
						break;
					}
					num <<= 4 + j - 48;
					this.Read();
				}
			}
			return num;
		}

		// Token: 0x06000C32 RID: 3122 RVA: 0x00038064 File Offset: 0x00036264
		private void ReadAttribute(SmallXmlParser.AttrListImpl a)
		{
			this.SkipWhitespaces(true);
			if (this.Peek() == 47 || this.Peek() == 62)
			{
				return;
			}
			string text = this.ReadName();
			this.SkipWhitespaces();
			this.Expect(61);
			this.SkipWhitespaces();
			int num = this.Read();
			string value;
			if (num != 34)
			{
				if (num != 39)
				{
					throw this.Error("Invalid attribute value markup.");
				}
				value = this.ReadUntil('\'', true);
			}
			else
			{
				value = this.ReadUntil('"', true);
			}
			if (text == "xml:space")
			{
				this.xmlSpace = value;
			}
			a.Add(text, value);
		}

		// Token: 0x06000C33 RID: 3123 RVA: 0x00038114 File Offset: 0x00036314
		private void ReadCDATASection()
		{
			int num = 0;
			while (this.Peek() >= 0)
			{
				char c = (char)this.Read();
				if (c == ']')
				{
					num++;
				}
				else
				{
					if (c == '>' && num > 1)
					{
						for (int i = num; i > 2; i--)
						{
							this.buffer.Append(']');
						}
						return;
					}
					for (int j = 0; j < num; j++)
					{
						this.buffer.Append(']');
					}
					num = 0;
					this.buffer.Append(c);
				}
			}
			throw this.UnexpectedEndError();
		}

		// Token: 0x06000C34 RID: 3124 RVA: 0x000381B8 File Offset: 0x000363B8
		private void ReadComment()
		{
			this.Expect(45);
			this.Expect(45);
			for (;;)
			{
				if (this.Read() == 45)
				{
					if (this.Read() == 45)
					{
						break;
					}
				}
			}
			if (this.Read() != 62)
			{
				throw this.Error("'--' is not allowed inside comment markup.");
			}
		}

		// Token: 0x04000348 RID: 840
		private SmallXmlParser.IContentHandler handler;

		// Token: 0x04000349 RID: 841
		private TextReader reader;

		// Token: 0x0400034A RID: 842
		private Stack elementNames = new Stack();

		// Token: 0x0400034B RID: 843
		private Stack xmlSpaces = new Stack();

		// Token: 0x0400034C RID: 844
		private string xmlSpace;

		// Token: 0x0400034D RID: 845
		private StringBuilder buffer = new StringBuilder(200);

		// Token: 0x0400034E RID: 846
		private char[] nameBuffer = new char[30];

		// Token: 0x0400034F RID: 847
		private bool isWhitespace;

		// Token: 0x04000350 RID: 848
		private SmallXmlParser.AttrListImpl attributes = new SmallXmlParser.AttrListImpl();

		// Token: 0x04000351 RID: 849
		private int line = 1;

		// Token: 0x04000352 RID: 850
		private int column;

		// Token: 0x04000353 RID: 851
		private bool resetColumn;

		// Token: 0x020000EE RID: 238
		public interface IContentHandler
		{
			// Token: 0x06000C35 RID: 3125
			void OnStartParsing(SmallXmlParser parser);

			// Token: 0x06000C36 RID: 3126
			void OnEndParsing(SmallXmlParser parser);

			// Token: 0x06000C37 RID: 3127
			void OnStartElement(string name, SmallXmlParser.IAttrList attrs);

			// Token: 0x06000C38 RID: 3128
			void OnEndElement(string name);

			// Token: 0x06000C39 RID: 3129
			void OnProcessingInstruction(string name, string text);

			// Token: 0x06000C3A RID: 3130
			void OnChars(string text);

			// Token: 0x06000C3B RID: 3131
			void OnIgnorableWhitespace(string text);
		}

		// Token: 0x020000EF RID: 239
		public interface IAttrList
		{
			// Token: 0x170001BA RID: 442
			// (get) Token: 0x06000C3C RID: 3132
			int Length { get; }

			// Token: 0x170001BB RID: 443
			// (get) Token: 0x06000C3D RID: 3133
			bool IsEmpty { get; }

			// Token: 0x06000C3E RID: 3134
			string GetName(int i);

			// Token: 0x06000C3F RID: 3135
			string GetValue(int i);

			// Token: 0x06000C40 RID: 3136
			string GetValue(string name);

			// Token: 0x170001BC RID: 444
			// (get) Token: 0x06000C41 RID: 3137
			string[] Names { get; }

			// Token: 0x170001BD RID: 445
			// (get) Token: 0x06000C42 RID: 3138
			string[] Values { get; }
		}

		// Token: 0x020000F0 RID: 240
		private class AttrListImpl : SmallXmlParser.IAttrList
		{
			// Token: 0x170001BE RID: 446
			// (get) Token: 0x06000C44 RID: 3140 RVA: 0x0003823C File Offset: 0x0003643C
			public int Length
			{
				get
				{
					return this.attrNames.Count;
				}
			}

			// Token: 0x170001BF RID: 447
			// (get) Token: 0x06000C45 RID: 3141 RVA: 0x0003824C File Offset: 0x0003644C
			public bool IsEmpty
			{
				get
				{
					return this.attrNames.Count == 0;
				}
			}

			// Token: 0x06000C46 RID: 3142 RVA: 0x0003825C File Offset: 0x0003645C
			public string GetName(int i)
			{
				return (string)this.attrNames[i];
			}

			// Token: 0x06000C47 RID: 3143 RVA: 0x00038270 File Offset: 0x00036470
			public string GetValue(int i)
			{
				return (string)this.attrValues[i];
			}

			// Token: 0x06000C48 RID: 3144 RVA: 0x00038284 File Offset: 0x00036484
			public string GetValue(string name)
			{
				for (int i = 0; i < this.attrNames.Count; i++)
				{
					if ((string)this.attrNames[i] == name)
					{
						return (string)this.attrValues[i];
					}
				}
				return null;
			}

			// Token: 0x170001C0 RID: 448
			// (get) Token: 0x06000C49 RID: 3145 RVA: 0x000382DC File Offset: 0x000364DC
			public string[] Names
			{
				get
				{
					return (string[])this.attrNames.ToArray(typeof(string));
				}
			}

			// Token: 0x170001C1 RID: 449
			// (get) Token: 0x06000C4A RID: 3146 RVA: 0x000382F8 File Offset: 0x000364F8
			public string[] Values
			{
				get
				{
					return (string[])this.attrValues.ToArray(typeof(string));
				}
			}

			// Token: 0x06000C4B RID: 3147 RVA: 0x00038314 File Offset: 0x00036514
			internal void Clear()
			{
				this.attrNames.Clear();
				this.attrValues.Clear();
			}

			// Token: 0x06000C4C RID: 3148 RVA: 0x0003832C File Offset: 0x0003652C
			internal void Add(string name, string value)
			{
				this.attrNames.Add(name);
				this.attrValues.Add(value);
			}

			// Token: 0x04000355 RID: 853
			private ArrayList attrNames = new ArrayList();

			// Token: 0x04000356 RID: 854
			private ArrayList attrValues = new ArrayList();
		}
	}
}
