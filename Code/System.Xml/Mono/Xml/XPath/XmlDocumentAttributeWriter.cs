using System;
using System.Xml;

namespace Mono.Xml.XPath
{
	// Token: 0x02000049 RID: 73
	internal class XmlDocumentAttributeWriter : XmlWriter
	{
		// Token: 0x060002CA RID: 714 RVA: 0x0001417C File Offset: 0x0001237C
		public XmlDocumentAttributeWriter(XmlNode owner)
		{
			this.element = (owner as XmlElement);
			if (this.element == null)
			{
				throw new ArgumentException("To write attributes, current node must be an element.");
			}
			this.state = WriteState.Content;
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x060002CB RID: 715 RVA: 0x000141B0 File Offset: 0x000123B0
		public override WriteState WriteState
		{
			get
			{
				return this.state;
			}
		}

		// Token: 0x060002CC RID: 716 RVA: 0x000141B8 File Offset: 0x000123B8
		public override void Close()
		{
		}

		// Token: 0x060002CD RID: 717 RVA: 0x000141BC File Offset: 0x000123BC
		public override void Flush()
		{
		}

		// Token: 0x060002CE RID: 718 RVA: 0x000141C0 File Offset: 0x000123C0
		public override string LookupPrefix(string ns)
		{
			return this.element.GetPrefixOfNamespace(ns);
		}

		// Token: 0x060002CF RID: 719 RVA: 0x000141D0 File Offset: 0x000123D0
		public override void WriteStartAttribute(string prefix, string name, string ns)
		{
			if (this.state != WriteState.Content)
			{
				throw new InvalidOperationException("Current state is not inside element. Cannot start attribute.");
			}
			if (prefix == null && ns != null && ns.Length > 0)
			{
				prefix = this.LookupPrefix(ns);
			}
			this.attribute = this.element.OwnerDocument.CreateAttribute(prefix, name, ns);
			this.state = WriteState.Attribute;
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x00014238 File Offset: 0x00012438
		public override void WriteProcessingInstruction(string name, string value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x00014240 File Offset: 0x00012440
		public override void WriteComment(string text)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x00014248 File Offset: 0x00012448
		public override void WriteCData(string text)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060002D3 RID: 723 RVA: 0x00014250 File Offset: 0x00012450
		public override void WriteStartElement(string prefix, string name, string ns)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060002D4 RID: 724 RVA: 0x00014258 File Offset: 0x00012458
		public override void WriteEndElement()
		{
			throw new NotSupportedException();
		}

		// Token: 0x060002D5 RID: 725 RVA: 0x00014260 File Offset: 0x00012460
		public override void WriteFullEndElement()
		{
			throw new NotSupportedException();
		}

		// Token: 0x060002D6 RID: 726 RVA: 0x00014268 File Offset: 0x00012468
		public override void WriteDocType(string name, string pubid, string systemId, string intsubset)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060002D7 RID: 727 RVA: 0x00014270 File Offset: 0x00012470
		public override void WriteStartDocument()
		{
			throw new NotSupportedException();
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x00014278 File Offset: 0x00012478
		public override void WriteStartDocument(bool standalone)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x00014280 File Offset: 0x00012480
		public override void WriteEndDocument()
		{
			throw new NotSupportedException();
		}

		// Token: 0x060002DA RID: 730 RVA: 0x00014288 File Offset: 0x00012488
		public override void WriteBase64(byte[] data, int start, int length)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060002DB RID: 731 RVA: 0x00014290 File Offset: 0x00012490
		public override void WriteRaw(char[] raw, int start, int length)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060002DC RID: 732 RVA: 0x00014298 File Offset: 0x00012498
		public override void WriteRaw(string raw)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060002DD RID: 733 RVA: 0x000142A0 File Offset: 0x000124A0
		public override void WriteSurrogateCharEntity(char msb, char lsb)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060002DE RID: 734 RVA: 0x000142A8 File Offset: 0x000124A8
		public override void WriteCharEntity(char c)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060002DF RID: 735 RVA: 0x000142B0 File Offset: 0x000124B0
		public override void WriteEntityRef(string entname)
		{
			if (this.state != WriteState.Attribute)
			{
				throw new InvalidOperationException("Current state is not inside attribute. Cannot write attribute value.");
			}
			this.attribute.AppendChild(this.attribute.OwnerDocument.CreateEntityReference(entname));
		}

		// Token: 0x060002E0 RID: 736 RVA: 0x000142F4 File Offset: 0x000124F4
		public override void WriteChars(char[] data, int start, int length)
		{
			this.WriteString(new string(data, start, length));
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x00014304 File Offset: 0x00012504
		public override void WriteString(string text)
		{
			if (this.state != WriteState.Attribute)
			{
				throw new InvalidOperationException("Current state is not inside attribute. Cannot write attribute value.");
			}
			XmlAttribute xmlAttribute = this.attribute;
			xmlAttribute.Value += text;
		}

		// Token: 0x060002E2 RID: 738 RVA: 0x00014340 File Offset: 0x00012540
		public override void WriteWhitespace(string text)
		{
			if (this.state != WriteState.Attribute)
			{
				throw new InvalidOperationException("Current state is not inside attribute. Cannot write attribute value.");
			}
			if (this.attribute.ChildNodes.Count == 0)
			{
				this.attribute.AppendChild(this.attribute.OwnerDocument.CreateWhitespace(text));
			}
			else
			{
				XmlAttribute xmlAttribute = this.attribute;
				xmlAttribute.Value += text;
			}
		}

		// Token: 0x060002E3 RID: 739 RVA: 0x000143B4 File Offset: 0x000125B4
		public override void WriteEndAttribute()
		{
			if (this.state != WriteState.Attribute)
			{
				throw new InvalidOperationException("Current state is not inside attribute. Cannot close attribute.");
			}
			this.element.SetAttributeNode(this.attribute);
			this.attribute = null;
			this.state = WriteState.Content;
		}

		// Token: 0x04000221 RID: 545
		private XmlElement element;

		// Token: 0x04000222 RID: 546
		private WriteState state;

		// Token: 0x04000223 RID: 547
		private XmlAttribute attribute;
	}
}
