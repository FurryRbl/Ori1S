using System;
using System.Text;
using System.Xml;

namespace Mono.Xml.Xsl
{
	// Token: 0x0200008B RID: 139
	internal class XmlWriterEmitter : Emitter
	{
		// Token: 0x060004B9 RID: 1209 RVA: 0x0001DED0 File Offset: 0x0001C0D0
		public XmlWriterEmitter(XmlWriter writer)
		{
			this.writer = writer;
		}

		// Token: 0x060004BA RID: 1210 RVA: 0x0001DEE0 File Offset: 0x0001C0E0
		public override void WriteStartDocument(Encoding encoding, StandaloneType standalone)
		{
			string text = string.Empty;
			if (standalone != StandaloneType.YES)
			{
				if (standalone == StandaloneType.NO)
				{
					text = " standalone=\"no\"";
				}
			}
			else
			{
				text = " standalone=\"yes\"";
			}
			if (encoding == null)
			{
				this.writer.WriteProcessingInstruction("xml", "version=\"1.0\"" + text);
			}
			else
			{
				this.writer.WriteProcessingInstruction("xml", "version=\"1.0\" encoding=\"" + encoding.WebName + "\"" + text);
			}
		}

		// Token: 0x060004BB RID: 1211 RVA: 0x0001DF6C File Offset: 0x0001C16C
		public override void WriteEndDocument()
		{
		}

		// Token: 0x060004BC RID: 1212 RVA: 0x0001DF70 File Offset: 0x0001C170
		public override void WriteDocType(string type, string publicId, string systemId)
		{
			if (systemId == null)
			{
				return;
			}
			this.writer.WriteDocType(type, publicId, systemId, null);
		}

		// Token: 0x060004BD RID: 1213 RVA: 0x0001DF88 File Offset: 0x0001C188
		public override void WriteStartElement(string prefix, string localName, string nsURI)
		{
			this.writer.WriteStartElement(prefix, localName, nsURI);
		}

		// Token: 0x060004BE RID: 1214 RVA: 0x0001DF98 File Offset: 0x0001C198
		public override void WriteEndElement()
		{
			this.writer.WriteEndElement();
		}

		// Token: 0x060004BF RID: 1215 RVA: 0x0001DFA8 File Offset: 0x0001C1A8
		public override void WriteFullEndElement()
		{
			this.writer.WriteFullEndElement();
		}

		// Token: 0x060004C0 RID: 1216 RVA: 0x0001DFB8 File Offset: 0x0001C1B8
		public override void WriteAttributeString(string prefix, string localName, string nsURI, string value)
		{
			this.writer.WriteAttributeString(prefix, localName, nsURI, value);
		}

		// Token: 0x060004C1 RID: 1217 RVA: 0x0001DFCC File Offset: 0x0001C1CC
		public override void WriteComment(string text)
		{
			while (text.IndexOf("--") >= 0)
			{
				text = text.Replace("--", "- -");
			}
			if (text.EndsWith("-"))
			{
				text += ' ';
			}
			this.writer.WriteComment(text);
		}

		// Token: 0x060004C2 RID: 1218 RVA: 0x0001E02C File Offset: 0x0001C22C
		public override void WriteProcessingInstruction(string name, string text)
		{
			while (text.IndexOf("?>") >= 0)
			{
				text = text.Replace("?>", "? >");
			}
			this.writer.WriteProcessingInstruction(name, text);
		}

		// Token: 0x060004C3 RID: 1219 RVA: 0x0001E064 File Offset: 0x0001C264
		public override void WriteString(string text)
		{
			this.writer.WriteString(text);
		}

		// Token: 0x060004C4 RID: 1220 RVA: 0x0001E074 File Offset: 0x0001C274
		public override void WriteRaw(string data)
		{
			this.writer.WriteRaw(data);
		}

		// Token: 0x060004C5 RID: 1221 RVA: 0x0001E084 File Offset: 0x0001C284
		public override void WriteCDataSection(string text)
		{
			int num = text.IndexOf("]]>");
			if (num >= 0)
			{
				this.writer.WriteCData(text.Substring(0, num + 2));
				this.WriteCDataSection(text.Substring(num + 2));
			}
			else
			{
				this.writer.WriteCData(text);
			}
		}

		// Token: 0x060004C6 RID: 1222 RVA: 0x0001E0DC File Offset: 0x0001C2DC
		public override void WriteWhitespace(string value)
		{
			this.writer.WriteWhitespace(value);
		}

		// Token: 0x060004C7 RID: 1223 RVA: 0x0001E0EC File Offset: 0x0001C2EC
		public override void Done()
		{
			this.writer.Flush();
		}

		// Token: 0x0400030B RID: 779
		private XmlWriter writer;
	}
}
