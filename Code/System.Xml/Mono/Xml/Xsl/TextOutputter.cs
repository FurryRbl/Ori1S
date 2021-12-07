using System;
using System.IO;

namespace Mono.Xml.Xsl
{
	// Token: 0x0200008A RID: 138
	internal class TextOutputter : Outputter
	{
		// Token: 0x060004AA RID: 1194 RVA: 0x0001DE00 File Offset: 0x0001C000
		public TextOutputter(TextWriter w, bool ignoreNestedText)
		{
			this._writer = w;
			this._ignoreNestedText = ignoreNestedText;
		}

		// Token: 0x060004AB RID: 1195 RVA: 0x0001DE18 File Offset: 0x0001C018
		public override void WriteStartElement(string prefix, string localName, string nsURI)
		{
			if (this._ignoreNestedText)
			{
				this._depth++;
			}
		}

		// Token: 0x060004AC RID: 1196 RVA: 0x0001DE34 File Offset: 0x0001C034
		public override void WriteEndElement()
		{
			if (this._ignoreNestedText)
			{
				this._depth--;
			}
		}

		// Token: 0x060004AD RID: 1197 RVA: 0x0001DE50 File Offset: 0x0001C050
		public override void WriteAttributeString(string prefix, string localName, string nsURI, string value)
		{
		}

		// Token: 0x060004AE RID: 1198 RVA: 0x0001DE54 File Offset: 0x0001C054
		public override void WriteNamespaceDecl(string prefix, string nsUri)
		{
		}

		// Token: 0x060004AF RID: 1199 RVA: 0x0001DE58 File Offset: 0x0001C058
		public override void WriteComment(string text)
		{
		}

		// Token: 0x060004B0 RID: 1200 RVA: 0x0001DE5C File Offset: 0x0001C05C
		public override void WriteProcessingInstruction(string name, string text)
		{
		}

		// Token: 0x060004B1 RID: 1201 RVA: 0x0001DE60 File Offset: 0x0001C060
		public override void WriteString(string text)
		{
			this.WriteImpl(text);
		}

		// Token: 0x060004B2 RID: 1202 RVA: 0x0001DE6C File Offset: 0x0001C06C
		public override void WriteRaw(string data)
		{
			this.WriteImpl(data);
		}

		// Token: 0x060004B3 RID: 1203 RVA: 0x0001DE78 File Offset: 0x0001C078
		public override void WriteWhitespace(string text)
		{
			this.WriteImpl(text);
		}

		// Token: 0x060004B4 RID: 1204 RVA: 0x0001DE84 File Offset: 0x0001C084
		private void WriteImpl(string text)
		{
			if (!this._ignoreNestedText || this._depth == 0)
			{
				this._writer.Write(text);
			}
		}

		// Token: 0x060004B5 RID: 1205 RVA: 0x0001DEB4 File Offset: 0x0001C0B4
		public override void Done()
		{
			this._writer.Flush();
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x060004B6 RID: 1206 RVA: 0x0001DEC4 File Offset: 0x0001C0C4
		public override bool CanProcessAttributes
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x060004B7 RID: 1207 RVA: 0x0001DEC8 File Offset: 0x0001C0C8
		// (set) Token: 0x060004B8 RID: 1208 RVA: 0x0001DECC File Offset: 0x0001C0CC
		public override bool InsideCDataSection
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		// Token: 0x04000308 RID: 776
		private TextWriter _writer;

		// Token: 0x04000309 RID: 777
		private int _depth;

		// Token: 0x0400030A RID: 778
		private bool _ignoreNestedText;
	}
}
