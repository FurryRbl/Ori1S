using System;

namespace System.Xml
{
	// Token: 0x020000B3 RID: 179
	internal class XmlNodeWriter : XmlWriter
	{
		// Token: 0x06000633 RID: 1587 RVA: 0x00024B84 File Offset: 0x00022D84
		public XmlNodeWriter() : this(true)
		{
		}

		// Token: 0x06000634 RID: 1588 RVA: 0x00024B90 File Offset: 0x00022D90
		public XmlNodeWriter(bool isDocumentEntity)
		{
			this.doc = new XmlDocument();
			this.state = XmlNodeType.None;
			this.isDocumentEntity = isDocumentEntity;
			if (!isDocumentEntity)
			{
				this.current = (this.fragment = this.doc.CreateDocumentFragment());
			}
		}

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x06000635 RID: 1589 RVA: 0x00024BDC File Offset: 0x00022DDC
		public XmlNode Document
		{
			get
			{
				return (!this.isDocumentEntity) ? this.fragment : this.doc;
			}
		}

		// Token: 0x1700018C RID: 396
		// (get) Token: 0x06000636 RID: 1590 RVA: 0x00024BFC File Offset: 0x00022DFC
		public override WriteState WriteState
		{
			get
			{
				if (this.isClosed)
				{
					return WriteState.Closed;
				}
				if (this.attribute != null)
				{
					return WriteState.Attribute;
				}
				XmlNodeType xmlNodeType = this.state;
				if (xmlNodeType == XmlNodeType.None)
				{
					return WriteState.Start;
				}
				if (xmlNodeType == XmlNodeType.DocumentType)
				{
					return WriteState.Element;
				}
				if (xmlNodeType != XmlNodeType.XmlDeclaration)
				{
					return WriteState.Content;
				}
				return WriteState.Prolog;
			}
		}

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x06000637 RID: 1591 RVA: 0x00024C4C File Offset: 0x00022E4C
		public override string XmlLang
		{
			get
			{
				for (XmlElement xmlElement = this.current as XmlElement; xmlElement != null; xmlElement = (xmlElement.ParentNode as XmlElement))
				{
					if (xmlElement.HasAttribute("xml:lang"))
					{
						return xmlElement.GetAttribute("xml:lang");
					}
				}
				return string.Empty;
			}
		}

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x06000638 RID: 1592 RVA: 0x00024CA0 File Offset: 0x00022EA0
		public override XmlSpace XmlSpace
		{
			get
			{
				XmlElement xmlElement = this.current as XmlElement;
				while (xmlElement != null)
				{
					string text = xmlElement.GetAttribute("xml:space");
					string text2 = text;
					switch (text2)
					{
					case "preserve":
						return XmlSpace.Preserve;
					case "default":
						return XmlSpace.Default;

						xmlElement = (xmlElement.ParentNode as XmlElement);
						continue;
					}
					throw new InvalidOperationException(string.Format("Invalid xml:space {0}.", text));
				}
				return XmlSpace.None;
			}
		}

		// Token: 0x06000639 RID: 1593 RVA: 0x00024D64 File Offset: 0x00022F64
		private void CheckState()
		{
			if (this.isClosed)
			{
				throw new InvalidOperationException();
			}
		}

		// Token: 0x0600063A RID: 1594 RVA: 0x00024D78 File Offset: 0x00022F78
		private void WritePossiblyTopLevelNode(XmlNode n, bool possiblyAttribute)
		{
			this.CheckState();
			if (!possiblyAttribute && this.attribute != null)
			{
				throw new InvalidOperationException(string.Format("Current state is not acceptable for {0}.", n.NodeType));
			}
			if (this.state != XmlNodeType.Element)
			{
				this.Document.AppendChild(n);
			}
			else if (this.attribute != null)
			{
				this.attribute.AppendChild(n);
			}
			else
			{
				this.current.AppendChild(n);
			}
			if (this.state == XmlNodeType.None)
			{
				this.state = XmlNodeType.XmlDeclaration;
			}
		}

		// Token: 0x0600063B RID: 1595 RVA: 0x00024E14 File Offset: 0x00023014
		public override void Close()
		{
			this.CheckState();
			this.isClosed = true;
		}

		// Token: 0x0600063C RID: 1596 RVA: 0x00024E24 File Offset: 0x00023024
		public override void Flush()
		{
		}

		// Token: 0x0600063D RID: 1597 RVA: 0x00024E28 File Offset: 0x00023028
		public override string LookupPrefix(string ns)
		{
			this.CheckState();
			if (this.current == null)
			{
				throw new InvalidOperationException();
			}
			return this.current.GetPrefixOfNamespace(ns);
		}

		// Token: 0x0600063E RID: 1598 RVA: 0x00024E50 File Offset: 0x00023050
		public override void WriteStartDocument()
		{
			this.WriteStartDocument(null);
		}

		// Token: 0x0600063F RID: 1599 RVA: 0x00024E5C File Offset: 0x0002305C
		public override void WriteStartDocument(bool standalone)
		{
			this.WriteStartDocument((!standalone) ? "no" : "yes");
		}

		// Token: 0x06000640 RID: 1600 RVA: 0x00024E7C File Offset: 0x0002307C
		private void WriteStartDocument(string sddecl)
		{
			this.CheckState();
			if (this.state != XmlNodeType.None)
			{
				throw new InvalidOperationException("Current state is not acceptable for xmldecl.");
			}
			this.doc.AppendChild(this.doc.CreateXmlDeclaration("1.0", null, sddecl));
			this.state = XmlNodeType.XmlDeclaration;
		}

		// Token: 0x06000641 RID: 1601 RVA: 0x00024ECC File Offset: 0x000230CC
		public override void WriteEndDocument()
		{
			this.CheckState();
			this.isClosed = true;
		}

		// Token: 0x06000642 RID: 1602 RVA: 0x00024EDC File Offset: 0x000230DC
		public override void WriteDocType(string name, string publicId, string systemId, string internalSubset)
		{
			this.CheckState();
			XmlNodeType xmlNodeType = this.state;
			if (xmlNodeType != XmlNodeType.None && xmlNodeType != XmlNodeType.XmlDeclaration)
			{
				throw new InvalidOperationException("Current state is not acceptable for doctype.");
			}
			this.doc.AppendChild(this.doc.CreateDocumentType(name, publicId, systemId, internalSubset));
			this.state = XmlNodeType.DocumentType;
		}

		// Token: 0x06000643 RID: 1603 RVA: 0x00024F40 File Offset: 0x00023140
		public override void WriteStartElement(string prefix, string name, string ns)
		{
			this.CheckState();
			if (this.isDocumentEntity && this.state == XmlNodeType.EndElement && this.doc.DocumentElement != null)
			{
				throw new InvalidOperationException("Current state is not acceptable for startElement.");
			}
			XmlElement newChild = this.doc.CreateElement(prefix, name, ns);
			if (this.current == null)
			{
				this.Document.AppendChild(newChild);
				this.state = XmlNodeType.Element;
			}
			else
			{
				this.current.AppendChild(newChild);
				this.state = XmlNodeType.Element;
			}
			this.current = newChild;
		}

		// Token: 0x06000644 RID: 1604 RVA: 0x00024FD4 File Offset: 0x000231D4
		public override void WriteEndElement()
		{
			this.WriteEndElementInternal(false);
		}

		// Token: 0x06000645 RID: 1605 RVA: 0x00024FE0 File Offset: 0x000231E0
		public override void WriteFullEndElement()
		{
			this.WriteEndElementInternal(true);
		}

		// Token: 0x06000646 RID: 1606 RVA: 0x00024FEC File Offset: 0x000231EC
		private void WriteEndElementInternal(bool forceFull)
		{
			this.CheckState();
			if (this.current == null)
			{
				throw new InvalidOperationException("Current state is not acceptable for endElement.");
			}
			if (!forceFull && this.current.FirstChild == null)
			{
				((XmlElement)this.current).IsEmpty = true;
			}
			if (this.isDocumentEntity && this.current.ParentNode == this.doc)
			{
				this.state = XmlNodeType.EndElement;
			}
			else
			{
				this.current = this.current.ParentNode;
			}
		}

		// Token: 0x06000647 RID: 1607 RVA: 0x0002507C File Offset: 0x0002327C
		public override void WriteStartAttribute(string prefix, string name, string ns)
		{
			this.CheckState();
			if (this.attribute != null)
			{
				throw new InvalidOperationException("There is an open attribute.");
			}
			if (!(this.current is XmlElement))
			{
				throw new InvalidOperationException("Current state is not acceptable for startAttribute.");
			}
			this.attribute = this.doc.CreateAttribute(prefix, name, ns);
			((XmlElement)this.current).SetAttributeNode(this.attribute);
		}

		// Token: 0x06000648 RID: 1608 RVA: 0x000250EC File Offset: 0x000232EC
		public override void WriteEndAttribute()
		{
			this.CheckState();
			if (this.attribute == null)
			{
				throw new InvalidOperationException("Current state is not acceptable for startAttribute.");
			}
			this.attribute = null;
		}

		// Token: 0x06000649 RID: 1609 RVA: 0x00025114 File Offset: 0x00023314
		public override void WriteCData(string data)
		{
			this.CheckState();
			if (this.current == null)
			{
				throw new InvalidOperationException("Current state is not acceptable for CDATAsection.");
			}
			this.current.AppendChild(this.doc.CreateCDataSection(data));
		}

		// Token: 0x0600064A RID: 1610 RVA: 0x00025158 File Offset: 0x00023358
		public override void WriteComment(string comment)
		{
			this.WritePossiblyTopLevelNode(this.doc.CreateComment(comment), false);
		}

		// Token: 0x0600064B RID: 1611 RVA: 0x00025170 File Offset: 0x00023370
		public override void WriteProcessingInstruction(string name, string value)
		{
			this.WritePossiblyTopLevelNode(this.doc.CreateProcessingInstruction(name, value), false);
		}

		// Token: 0x0600064C RID: 1612 RVA: 0x00025188 File Offset: 0x00023388
		public override void WriteEntityRef(string name)
		{
			this.WritePossiblyTopLevelNode(this.doc.CreateEntityReference(name), true);
		}

		// Token: 0x0600064D RID: 1613 RVA: 0x000251A0 File Offset: 0x000233A0
		public override void WriteCharEntity(char c)
		{
			this.WritePossiblyTopLevelNode(this.doc.CreateTextNode(new string(new char[]
			{
				c
			}, 0, 1)), true);
		}

		// Token: 0x0600064E RID: 1614 RVA: 0x000251C8 File Offset: 0x000233C8
		public override void WriteWhitespace(string ws)
		{
			this.WritePossiblyTopLevelNode(this.doc.CreateWhitespace(ws), true);
		}

		// Token: 0x0600064F RID: 1615 RVA: 0x000251E0 File Offset: 0x000233E0
		public override void WriteString(string data)
		{
			this.CheckState();
			if (this.current == null)
			{
				throw new InvalidOperationException("Current state is not acceptable for Text.");
			}
			if (this.attribute != null)
			{
				this.attribute.AppendChild(this.doc.CreateTextNode(data));
			}
			else
			{
				XmlText xmlText = this.current.LastChild as XmlText;
				if (xmlText == null)
				{
					this.current.AppendChild(this.doc.CreateTextNode(data));
				}
				else
				{
					xmlText.AppendData(data);
				}
			}
		}

		// Token: 0x06000650 RID: 1616 RVA: 0x0002526C File Offset: 0x0002346C
		public override void WriteName(string name)
		{
			this.WriteString(name);
		}

		// Token: 0x06000651 RID: 1617 RVA: 0x00025278 File Offset: 0x00023478
		public override void WriteNmToken(string nmtoken)
		{
			this.WriteString(nmtoken);
		}

		// Token: 0x06000652 RID: 1618 RVA: 0x00025284 File Offset: 0x00023484
		public override void WriteQualifiedName(string name, string ns)
		{
			string text = this.LookupPrefix(ns);
			if (text == null)
			{
				throw new ArgumentException(string.Format("Invalid namespace {0}", ns));
			}
			if (text != string.Empty)
			{
				this.WriteString(name);
			}
			else
			{
				this.WriteString(text + ":" + name);
			}
		}

		// Token: 0x06000653 RID: 1619 RVA: 0x000252E0 File Offset: 0x000234E0
		public override void WriteChars(char[] chars, int start, int len)
		{
			this.WriteString(new string(chars, start, len));
		}

		// Token: 0x06000654 RID: 1620 RVA: 0x000252F0 File Offset: 0x000234F0
		public override void WriteRaw(string data)
		{
			this.WriteString(data);
		}

		// Token: 0x06000655 RID: 1621 RVA: 0x000252FC File Offset: 0x000234FC
		public override void WriteRaw(char[] chars, int start, int len)
		{
			this.WriteChars(chars, start, len);
		}

		// Token: 0x06000656 RID: 1622 RVA: 0x00025308 File Offset: 0x00023508
		public override void WriteBase64(byte[] data, int start, int len)
		{
			this.WriteString(Convert.ToBase64String(data, start, len));
		}

		// Token: 0x06000657 RID: 1623 RVA: 0x00025318 File Offset: 0x00023518
		public override void WriteBinHex(byte[] data, int start, int len)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000658 RID: 1624 RVA: 0x00025320 File Offset: 0x00023520
		public override void WriteSurrogateCharEntity(char c1, char c2)
		{
			throw new NotImplementedException();
		}

		// Token: 0x040003C0 RID: 960
		private XmlDocument doc;

		// Token: 0x040003C1 RID: 961
		private bool isClosed;

		// Token: 0x040003C2 RID: 962
		private XmlNode current;

		// Token: 0x040003C3 RID: 963
		private XmlAttribute attribute;

		// Token: 0x040003C4 RID: 964
		private bool isDocumentEntity;

		// Token: 0x040003C5 RID: 965
		private XmlDocumentFragment fragment;

		// Token: 0x040003C6 RID: 966
		private XmlNodeType state;
	}
}
