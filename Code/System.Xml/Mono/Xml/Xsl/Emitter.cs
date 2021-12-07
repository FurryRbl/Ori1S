using System;
using System.Text;

namespace Mono.Xml.Xsl
{
	// Token: 0x0200007D RID: 125
	internal abstract class Emitter
	{
		// Token: 0x0600042D RID: 1069
		public abstract void WriteStartDocument(Encoding encoding, StandaloneType standalone);

		// Token: 0x0600042E RID: 1070
		public abstract void WriteEndDocument();

		// Token: 0x0600042F RID: 1071
		public abstract void WriteDocType(string type, string publicId, string systemId);

		// Token: 0x06000430 RID: 1072
		public abstract void WriteStartElement(string prefix, string localName, string nsURI);

		// Token: 0x06000431 RID: 1073
		public abstract void WriteEndElement();

		// Token: 0x06000432 RID: 1074 RVA: 0x0001B654 File Offset: 0x00019854
		public virtual void WriteFullEndElement()
		{
			this.WriteEndElement();
		}

		// Token: 0x06000433 RID: 1075
		public abstract void WriteAttributeString(string prefix, string localName, string nsURI, string value);

		// Token: 0x06000434 RID: 1076
		public abstract void WriteComment(string text);

		// Token: 0x06000435 RID: 1077
		public abstract void WriteProcessingInstruction(string name, string text);

		// Token: 0x06000436 RID: 1078
		public abstract void WriteString(string text);

		// Token: 0x06000437 RID: 1079
		public abstract void WriteCDataSection(string text);

		// Token: 0x06000438 RID: 1080
		public abstract void WriteRaw(string data);

		// Token: 0x06000439 RID: 1081
		public abstract void Done();

		// Token: 0x0600043A RID: 1082 RVA: 0x0001B65C File Offset: 0x0001985C
		public virtual void WriteWhitespace(string text)
		{
			this.WriteString(text);
		}
	}
}
