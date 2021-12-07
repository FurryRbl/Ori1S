using System;
using System.Collections;
using System.Globalization;
using System.IO;
using Mono.Xml;

namespace System.Xml
{
	// Token: 0x02000133 RID: 307
	internal class XmlParserInput
	{
		// Token: 0x06000E50 RID: 3664 RVA: 0x00047328 File Offset: 0x00045528
		public XmlParserInput(TextReader reader, string baseURI) : this(reader, baseURI, 1, 0)
		{
		}

		// Token: 0x06000E51 RID: 3665 RVA: 0x00047334 File Offset: 0x00045534
		public XmlParserInput(TextReader reader, string baseURI, int line, int column)
		{
			this.source = new XmlParserInput.XmlParserInputSource(reader, baseURI, false, line, column);
		}

		// Token: 0x06000E52 RID: 3666 RVA: 0x0004736C File Offset: 0x0004556C
		public void Close()
		{
			while (this.sourceStack.Count > 0)
			{
				((XmlParserInput.XmlParserInputSource)this.sourceStack.Pop()).Close();
			}
			this.source.Close();
		}

		// Token: 0x06000E53 RID: 3667 RVA: 0x000473B0 File Offset: 0x000455B0
		public void Expect(int expected)
		{
			int num = this.ReadChar();
			if (num != expected)
			{
				throw this.ReaderError(string.Format(CultureInfo.InvariantCulture, "expected '{0}' ({1:X}) but found '{2}' ({3:X})", new object[]
				{
					(char)expected,
					expected,
					(char)num,
					num
				}));
			}
		}

		// Token: 0x06000E54 RID: 3668 RVA: 0x00047410 File Offset: 0x00045610
		public void Expect(string expected)
		{
			int length = expected.Length;
			for (int i = 0; i < length; i++)
			{
				this.Expect((int)expected[i]);
			}
		}

		// Token: 0x06000E55 RID: 3669 RVA: 0x00047444 File Offset: 0x00045644
		public void PushPEBuffer(DTDParameterEntityDeclaration pe)
		{
			this.sourceStack.Push(this.source);
			this.source = new XmlParserInput.XmlParserInputSource(new StringReader(pe.ReplacementText), pe.ActualUri, true, 1, 0);
		}

		// Token: 0x06000E56 RID: 3670 RVA: 0x00047484 File Offset: 0x00045684
		private int ReadSourceChar()
		{
			int num = this.source.Read();
			while (num < 0 && this.sourceStack.Count > 0)
			{
				this.source = (this.sourceStack.Pop() as XmlParserInput.XmlParserInputSource);
				num = this.source.Read();
			}
			return num;
		}

		// Token: 0x06000E57 RID: 3671 RVA: 0x000474E0 File Offset: 0x000456E0
		public int PeekChar()
		{
			if (this.has_peek)
			{
				return this.peek_char;
			}
			this.peek_char = this.ReadSourceChar();
			if (this.peek_char >= 55296 && this.peek_char <= 56319)
			{
				this.peek_char = 65536 + (this.peek_char - 55296 << 10);
				int num = this.ReadSourceChar();
				if (num >= 56320 && num <= 57343)
				{
					this.peek_char += num - 56320;
				}
			}
			this.has_peek = true;
			return this.peek_char;
		}

		// Token: 0x06000E58 RID: 3672 RVA: 0x00047584 File Offset: 0x00045784
		public int ReadChar()
		{
			int result = this.PeekChar();
			this.has_peek = false;
			return result;
		}

		// Token: 0x17000413 RID: 1043
		// (get) Token: 0x06000E59 RID: 3673 RVA: 0x000475A0 File Offset: 0x000457A0
		public string BaseURI
		{
			get
			{
				return this.source.BaseURI;
			}
		}

		// Token: 0x17000414 RID: 1044
		// (get) Token: 0x06000E5A RID: 3674 RVA: 0x000475B0 File Offset: 0x000457B0
		public bool HasPEBuffer
		{
			get
			{
				return this.sourceStack.Count > 0;
			}
		}

		// Token: 0x17000415 RID: 1045
		// (get) Token: 0x06000E5B RID: 3675 RVA: 0x000475C0 File Offset: 0x000457C0
		public int LineNumber
		{
			get
			{
				return this.source.LineNumber;
			}
		}

		// Token: 0x17000416 RID: 1046
		// (get) Token: 0x06000E5C RID: 3676 RVA: 0x000475D0 File Offset: 0x000457D0
		public int LinePosition
		{
			get
			{
				return this.source.LinePosition;
			}
		}

		// Token: 0x17000417 RID: 1047
		// (get) Token: 0x06000E5D RID: 3677 RVA: 0x000475E0 File Offset: 0x000457E0
		// (set) Token: 0x06000E5E RID: 3678 RVA: 0x000475E8 File Offset: 0x000457E8
		public bool AllowTextDecl
		{
			get
			{
				return this.allowTextDecl;
			}
			set
			{
				this.allowTextDecl = value;
			}
		}

		// Token: 0x06000E5F RID: 3679 RVA: 0x000475F4 File Offset: 0x000457F4
		private XmlException ReaderError(string message)
		{
			return new XmlException(message, null, this.LineNumber, this.LinePosition);
		}

		// Token: 0x0400067A RID: 1658
		private Stack sourceStack = new Stack();

		// Token: 0x0400067B RID: 1659
		private XmlParserInput.XmlParserInputSource source;

		// Token: 0x0400067C RID: 1660
		private bool has_peek;

		// Token: 0x0400067D RID: 1661
		private int peek_char;

		// Token: 0x0400067E RID: 1662
		private bool allowTextDecl = true;

		// Token: 0x02000134 RID: 308
		private class XmlParserInputSource
		{
			// Token: 0x06000E60 RID: 3680 RVA: 0x00047614 File Offset: 0x00045814
			public XmlParserInputSource(TextReader reader, string baseUri, bool pe, int line, int column)
			{
				this.BaseURI = baseUri;
				this.reader = reader;
				this.isPE = pe;
				this.line = line;
				this.column = column;
			}

			// Token: 0x17000418 RID: 1048
			// (get) Token: 0x06000E61 RID: 3681 RVA: 0x00047644 File Offset: 0x00045844
			public int LineNumber
			{
				get
				{
					return this.line;
				}
			}

			// Token: 0x17000419 RID: 1049
			// (get) Token: 0x06000E62 RID: 3682 RVA: 0x0004764C File Offset: 0x0004584C
			public int LinePosition
			{
				get
				{
					return this.column;
				}
			}

			// Token: 0x06000E63 RID: 3683 RVA: 0x00047654 File Offset: 0x00045854
			public void Close()
			{
				this.reader.Close();
			}

			// Token: 0x06000E64 RID: 3684 RVA: 0x00047664 File Offset: 0x00045864
			public int Read()
			{
				if (this.state == 2)
				{
					return -1;
				}
				if (this.isPE && this.state == 0)
				{
					this.state = 1;
					return 32;
				}
				int num = this.reader.Read();
				if (num == 10)
				{
					this.line++;
					this.column = 1;
				}
				else if (num >= 0)
				{
					this.column++;
				}
				if (num < 0 && this.state == 1)
				{
					this.state = 2;
					return 32;
				}
				return num;
			}

			// Token: 0x0400067F RID: 1663
			public readonly string BaseURI;

			// Token: 0x04000680 RID: 1664
			private readonly TextReader reader;

			// Token: 0x04000681 RID: 1665
			public int state;

			// Token: 0x04000682 RID: 1666
			public bool isPE;

			// Token: 0x04000683 RID: 1667
			private int line;

			// Token: 0x04000684 RID: 1668
			private int column;
		}
	}
}
