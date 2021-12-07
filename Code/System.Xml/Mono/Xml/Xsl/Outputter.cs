using System;

namespace Mono.Xml.Xsl
{
	// Token: 0x02000084 RID: 132
	internal abstract class Outputter
	{
		// Token: 0x06000472 RID: 1138 RVA: 0x0001D864 File Offset: 0x0001BA64
		public void WriteStartElement(string localName, string nsURI)
		{
			this.WriteStartElement(null, localName, nsURI);
		}

		// Token: 0x06000473 RID: 1139
		public abstract void WriteStartElement(string prefix, string localName, string nsURI);

		// Token: 0x06000474 RID: 1140
		public abstract void WriteEndElement();

		// Token: 0x06000475 RID: 1141 RVA: 0x0001D870 File Offset: 0x0001BA70
		public virtual void WriteFullEndElement()
		{
			this.WriteEndElement();
		}

		// Token: 0x06000476 RID: 1142 RVA: 0x0001D878 File Offset: 0x0001BA78
		public void WriteAttributeString(string localName, string value)
		{
			this.WriteAttributeString(string.Empty, localName, string.Empty, value);
		}

		// Token: 0x06000477 RID: 1143
		public abstract void WriteAttributeString(string prefix, string localName, string nsURI, string value);

		// Token: 0x06000478 RID: 1144
		public abstract void WriteNamespaceDecl(string prefix, string nsUri);

		// Token: 0x06000479 RID: 1145
		public abstract void WriteComment(string text);

		// Token: 0x0600047A RID: 1146
		public abstract void WriteProcessingInstruction(string name, string text);

		// Token: 0x0600047B RID: 1147
		public abstract void WriteString(string text);

		// Token: 0x0600047C RID: 1148
		public abstract void WriteRaw(string data);

		// Token: 0x0600047D RID: 1149
		public abstract void WriteWhitespace(string text);

		// Token: 0x0600047E RID: 1150
		public abstract void Done();

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x0600047F RID: 1151
		public abstract bool CanProcessAttributes { get; }

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x06000480 RID: 1152
		// (set) Token: 0x06000481 RID: 1153
		public abstract bool InsideCDataSection { get; set; }
	}
}
