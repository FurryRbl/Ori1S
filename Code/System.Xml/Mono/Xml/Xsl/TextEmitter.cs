using System;
using System.IO;
using System.Text;

namespace Mono.Xml.Xsl
{
	// Token: 0x02000089 RID: 137
	internal class TextEmitter : Emitter
	{
		// Token: 0x0600049D RID: 1181 RVA: 0x0001DD9C File Offset: 0x0001BF9C
		public TextEmitter(TextWriter writer)
		{
			this.writer = writer;
		}

		// Token: 0x0600049E RID: 1182 RVA: 0x0001DDAC File Offset: 0x0001BFAC
		public override void WriteStartDocument(Encoding encoding, StandaloneType standalone)
		{
		}

		// Token: 0x0600049F RID: 1183 RVA: 0x0001DDB0 File Offset: 0x0001BFB0
		public override void WriteEndDocument()
		{
		}

		// Token: 0x060004A0 RID: 1184 RVA: 0x0001DDB4 File Offset: 0x0001BFB4
		public override void WriteDocType(string type, string publicId, string systemId)
		{
		}

		// Token: 0x060004A1 RID: 1185 RVA: 0x0001DDB8 File Offset: 0x0001BFB8
		public override void WriteStartElement(string prefix, string localName, string nsURI)
		{
		}

		// Token: 0x060004A2 RID: 1186 RVA: 0x0001DDBC File Offset: 0x0001BFBC
		public override void WriteEndElement()
		{
		}

		// Token: 0x060004A3 RID: 1187 RVA: 0x0001DDC0 File Offset: 0x0001BFC0
		public override void WriteAttributeString(string prefix, string localName, string nsURI, string value)
		{
		}

		// Token: 0x060004A4 RID: 1188 RVA: 0x0001DDC4 File Offset: 0x0001BFC4
		public override void WriteComment(string text)
		{
		}

		// Token: 0x060004A5 RID: 1189 RVA: 0x0001DDC8 File Offset: 0x0001BFC8
		public override void WriteProcessingInstruction(string name, string text)
		{
		}

		// Token: 0x060004A6 RID: 1190 RVA: 0x0001DDCC File Offset: 0x0001BFCC
		public override void WriteString(string text)
		{
			this.writer.Write(text);
		}

		// Token: 0x060004A7 RID: 1191 RVA: 0x0001DDDC File Offset: 0x0001BFDC
		public override void WriteRaw(string data)
		{
			this.writer.Write(data);
		}

		// Token: 0x060004A8 RID: 1192 RVA: 0x0001DDEC File Offset: 0x0001BFEC
		public override void WriteCDataSection(string text)
		{
			this.writer.Write(text);
		}

		// Token: 0x060004A9 RID: 1193 RVA: 0x0001DDFC File Offset: 0x0001BFFC
		public override void Done()
		{
		}

		// Token: 0x04000307 RID: 775
		private TextWriter writer;
	}
}
