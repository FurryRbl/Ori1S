using System;
using System.Xml;

namespace Mono.Xml.XPath
{
	// Token: 0x02000048 RID: 72
	internal class XmlDocumentInsertionWriter : XmlWriter
	{
		// Token: 0x060002AE RID: 686 RVA: 0x00013C6C File Offset: 0x00011E6C
		public XmlDocumentInsertionWriter(XmlNode owner, XmlNode nextSibling)
		{
			this.parent = owner;
			if (this.parent == null)
			{
				throw new InvalidOperationException();
			}
			XmlNodeType nodeType = this.parent.NodeType;
			switch (nodeType)
			{
			case XmlNodeType.Document:
				this.current = ((XmlDocument)this.parent).CreateDocumentFragment();
				goto IL_A1;
			default:
				if (nodeType != XmlNodeType.Element)
				{
					throw new InvalidOperationException(string.Format("Insertion into {0} node is not allowed.", this.parent.NodeType));
				}
				break;
			case XmlNodeType.DocumentFragment:
				break;
			}
			this.current = this.parent.OwnerDocument.CreateDocumentFragment();
			IL_A1:
			this.nextSibling = nextSibling;
			this.state = WriteState.Content;
		}

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x060002AF RID: 687 RVA: 0x00013D28 File Offset: 0x00011F28
		// (remove) Token: 0x060002B0 RID: 688 RVA: 0x00013D44 File Offset: 0x00011F44
		internal event XmlWriterClosedEventHandler Closed;

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x060002B1 RID: 689 RVA: 0x00013D60 File Offset: 0x00011F60
		public override WriteState WriteState
		{
			get
			{
				return this.state;
			}
		}

		// Token: 0x060002B2 RID: 690 RVA: 0x00013D68 File Offset: 0x00011F68
		public override void Close()
		{
			while (this.current.ParentNode != null)
			{
				this.current = this.current.ParentNode;
			}
			this.parent.InsertBefore((XmlDocumentFragment)this.current, this.nextSibling);
			if (this.Closed != null)
			{
				this.Closed(this);
			}
		}

		// Token: 0x060002B3 RID: 691 RVA: 0x00013DD0 File Offset: 0x00011FD0
		public override void Flush()
		{
		}

		// Token: 0x060002B4 RID: 692 RVA: 0x00013DD4 File Offset: 0x00011FD4
		public override string LookupPrefix(string ns)
		{
			return this.current.GetPrefixOfNamespace(ns);
		}

		// Token: 0x060002B5 RID: 693 RVA: 0x00013DE4 File Offset: 0x00011FE4
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
			this.attribute = this.current.OwnerDocument.CreateAttribute(prefix, name, ns);
			this.state = WriteState.Attribute;
		}

		// Token: 0x060002B6 RID: 694 RVA: 0x00013E4C File Offset: 0x0001204C
		public override void WriteProcessingInstruction(string name, string value)
		{
			XmlProcessingInstruction newChild = this.current.OwnerDocument.CreateProcessingInstruction(name, value);
			this.current.AppendChild(newChild);
		}

		// Token: 0x060002B7 RID: 695 RVA: 0x00013E7C File Offset: 0x0001207C
		public override void WriteComment(string text)
		{
			XmlComment newChild = this.current.OwnerDocument.CreateComment(text);
			this.current.AppendChild(newChild);
		}

		// Token: 0x060002B8 RID: 696 RVA: 0x00013EA8 File Offset: 0x000120A8
		public override void WriteCData(string text)
		{
			XmlCDataSection newChild = this.current.OwnerDocument.CreateCDataSection(text);
			this.current.AppendChild(newChild);
		}

		// Token: 0x060002B9 RID: 697 RVA: 0x00013ED4 File Offset: 0x000120D4
		public override void WriteStartElement(string prefix, string name, string ns)
		{
			if (prefix == null && ns != null && ns.Length > 0)
			{
				prefix = this.LookupPrefix(ns);
			}
			XmlElement newChild = this.current.OwnerDocument.CreateElement(prefix, name, ns);
			this.current.AppendChild(newChild);
			this.current = newChild;
		}

		// Token: 0x060002BA RID: 698 RVA: 0x00013F2C File Offset: 0x0001212C
		public override void WriteEndElement()
		{
			this.current = this.current.ParentNode;
			if (this.current == null)
			{
				throw new InvalidOperationException("No element is opened.");
			}
		}

		// Token: 0x060002BB RID: 699 RVA: 0x00013F58 File Offset: 0x00012158
		public override void WriteFullEndElement()
		{
			XmlElement xmlElement = this.current as XmlElement;
			if (xmlElement != null)
			{
				xmlElement.IsEmpty = false;
			}
			this.WriteEndElement();
		}

		// Token: 0x060002BC RID: 700 RVA: 0x00013F84 File Offset: 0x00012184
		public override void WriteDocType(string name, string pubid, string systemId, string intsubset)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060002BD RID: 701 RVA: 0x00013F8C File Offset: 0x0001218C
		public override void WriteStartDocument()
		{
			throw new NotSupportedException();
		}

		// Token: 0x060002BE RID: 702 RVA: 0x00013F94 File Offset: 0x00012194
		public override void WriteStartDocument(bool standalone)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060002BF RID: 703 RVA: 0x00013F9C File Offset: 0x0001219C
		public override void WriteEndDocument()
		{
			throw new NotSupportedException();
		}

		// Token: 0x060002C0 RID: 704 RVA: 0x00013FA4 File Offset: 0x000121A4
		public override void WriteBase64(byte[] data, int start, int length)
		{
			this.WriteString(Convert.ToBase64String(data, start, length));
		}

		// Token: 0x060002C1 RID: 705 RVA: 0x00013FB4 File Offset: 0x000121B4
		public override void WriteRaw(char[] raw, int start, int length)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x00013FBC File Offset: 0x000121BC
		public override void WriteRaw(string raw)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x00013FC4 File Offset: 0x000121C4
		public override void WriteSurrogateCharEntity(char msb, char lsb)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x00013FCC File Offset: 0x000121CC
		public override void WriteCharEntity(char c)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x00013FD4 File Offset: 0x000121D4
		public override void WriteEntityRef(string entname)
		{
			if (this.state != WriteState.Attribute)
			{
				throw new InvalidOperationException("Current state is not inside attribute. Cannot write attribute value.");
			}
			this.attribute.AppendChild(this.attribute.OwnerDocument.CreateEntityReference(entname));
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x00014018 File Offset: 0x00012218
		public override void WriteChars(char[] data, int start, int length)
		{
			this.WriteString(new string(data, start, length));
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x00014028 File Offset: 0x00012228
		public override void WriteString(string text)
		{
			if (this.attribute != null)
			{
				XmlAttribute xmlAttribute = this.attribute;
				xmlAttribute.Value += text;
			}
			else
			{
				XmlText newChild = this.current.OwnerDocument.CreateTextNode(text);
				this.current.AppendChild(newChild);
			}
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x0001407C File Offset: 0x0001227C
		public override void WriteWhitespace(string text)
		{
			if (this.state != WriteState.Attribute)
			{
				this.current.AppendChild(this.current.OwnerDocument.CreateTextNode(text));
			}
			else if (this.attribute.ChildNodes.Count == 0)
			{
				this.attribute.AppendChild(this.attribute.OwnerDocument.CreateWhitespace(text));
			}
			else
			{
				XmlAttribute xmlAttribute = this.attribute;
				xmlAttribute.Value += text;
			}
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x00014108 File Offset: 0x00012308
		public override void WriteEndAttribute()
		{
			XmlElement xmlElement = (this.current as XmlElement) ?? ((this.nextSibling != null) ? null : (this.parent as XmlElement));
			if (this.state != WriteState.Attribute || xmlElement == null)
			{
				throw new InvalidOperationException("Current state is not inside attribute. Cannot close attribute.");
			}
			xmlElement.SetAttributeNode(this.attribute);
			this.attribute = null;
			this.state = WriteState.Content;
		}

		// Token: 0x0400021B RID: 539
		private XmlNode parent;

		// Token: 0x0400021C RID: 540
		private XmlNode current;

		// Token: 0x0400021D RID: 541
		private XmlNode nextSibling;

		// Token: 0x0400021E RID: 542
		private WriteState state;

		// Token: 0x0400021F RID: 543
		private XmlAttribute attribute;
	}
}
