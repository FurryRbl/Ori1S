using System;
using System.Collections;
using System.Xml;
using System.Xml.XPath;

namespace Mono.Xml.XPath
{
	// Token: 0x02000035 RID: 53
	internal class DTMXPathDocumentWriter : XmlWriter
	{
		// Token: 0x060001B3 RID: 435 RVA: 0x0000E524 File Offset: 0x0000C724
		public DTMXPathDocumentWriter(XmlNameTable nt, int defaultCapacity)
		{
			this.nameTable = ((nt != null) ? nt : new NameTable());
			this.nodeCapacity = defaultCapacity;
			this.attributeCapacity = this.nodeCapacity;
			this.nsCapacity = 10;
			this.idTable = new Hashtable();
			this.nodes = new DTMXPathLinkedNode[this.nodeCapacity];
			this.attributes = new DTMXPathAttributeNode[this.attributeCapacity];
			this.namespaces = new DTMXPathNamespaceNode[this.nsCapacity];
			this.Init();
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x0000E5BC File Offset: 0x0000C7BC
		public DTMXPathDocument CreateDocument()
		{
			if (!this.isClosed)
			{
				this.Close();
			}
			return new DTMXPathDocument(this.nameTable, this.nodes, this.attributes, this.namespaces, this.idTable);
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x0000E600 File Offset: 0x0000C800
		public void Init()
		{
			this.AddNode(0, 0, 0, XPathNodeType.All, string.Empty, false, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, 0, 0, 0);
			this.nodeIndex++;
			this.AddAttribute(0, null, null, null, null, 0, 0);
			this.AddNsNode(0, null, null, 0);
			this.nsIndex++;
			this.AddNsNode(1, "xml", "http://www.w3.org/XML/1998/namespace", 0);
			this.AddNode(0, 0, 0, XPathNodeType.Root, null, false, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, 1, 0, 0);
			this.nodeIndex = 1;
			this.lastNsInScope = 1;
			this.parentStack[0] = this.nodeIndex;
			this.state = WriteState.Content;
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x0000E6CC File Offset: 0x0000C8CC
		private int GetParentIndex()
		{
			return this.parentStack[this.parentStackIndex];
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x0000E6DC File Offset: 0x0000C8DC
		private int GetPreviousSiblingIndex()
		{
			int num = this.parentStack[this.parentStackIndex];
			if (num == this.nodeIndex)
			{
				return 0;
			}
			int parent = this.nodeIndex;
			while (this.nodes[parent].Parent != num)
			{
				parent = this.nodes[parent].Parent;
			}
			return parent;
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x0000E73C File Offset: 0x0000C93C
		private void UpdateTreeForAddition()
		{
			int parentIndex = this.GetParentIndex();
			this.prevSibling = this.GetPreviousSiblingIndex();
			this.nodeIndex++;
			if (this.prevSibling != 0)
			{
				this.nodes[this.prevSibling].NextSibling = this.nodeIndex;
			}
			if (parentIndex == this.nodeIndex - 1)
			{
				this.nodes[parentIndex].FirstChild = this.nodeIndex;
			}
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x0000E7B8 File Offset: 0x0000C9B8
		private void CloseStartElement()
		{
			if (this.attrIndexAtStart != this.attributeIndex)
			{
				this.nodes[this.nodeIndex].FirstAttribute = this.attrIndexAtStart + 1;
			}
			if (this.nsIndexAtStart != this.nsIndex)
			{
				this.nodes[this.nodeIndex].FirstNamespace = this.nsIndex;
				this.lastNsInScope = this.nsIndex;
			}
			this.parentStackIndex++;
			if (this.parentStack.Length == this.parentStackIndex)
			{
				int[] destinationArray = new int[this.parentStackIndex * 2];
				Array.Copy(this.parentStack, destinationArray, this.parentStackIndex);
				this.parentStack = destinationArray;
			}
			this.parentStack[this.parentStackIndex] = this.nodeIndex;
			this.state = WriteState.Content;
		}

		// Token: 0x060001BA RID: 442 RVA: 0x0000E890 File Offset: 0x0000CA90
		private void SetNodeArrayLength(int size)
		{
			DTMXPathLinkedNode[] destinationArray = new DTMXPathLinkedNode[size];
			Array.Copy(this.nodes, destinationArray, Math.Min(size, this.nodes.Length));
			this.nodes = destinationArray;
		}

		// Token: 0x060001BB RID: 443 RVA: 0x0000E8C8 File Offset: 0x0000CAC8
		private void SetAttributeArrayLength(int size)
		{
			DTMXPathAttributeNode[] destinationArray = new DTMXPathAttributeNode[size];
			Array.Copy(this.attributes, destinationArray, Math.Min(size, this.attributes.Length));
			this.attributes = destinationArray;
		}

		// Token: 0x060001BC RID: 444 RVA: 0x0000E900 File Offset: 0x0000CB00
		private void SetNsArrayLength(int size)
		{
			DTMXPathNamespaceNode[] destinationArray = new DTMXPathNamespaceNode[size];
			Array.Copy(this.namespaces, destinationArray, Math.Min(size, this.namespaces.Length));
			this.namespaces = destinationArray;
		}

		// Token: 0x060001BD RID: 445 RVA: 0x0000E938 File Offset: 0x0000CB38
		public void AddNode(int parent, int firstAttribute, int previousSibling, XPathNodeType nodeType, string baseUri, bool isEmptyElement, string localName, string ns, string prefix, string value, string xmlLang, int namespaceNode, int lineNumber, int linePosition)
		{
			if (this.nodes.Length < this.nodeIndex + 1)
			{
				this.nodeCapacity *= 4;
				this.SetNodeArrayLength(this.nodeCapacity);
			}
			this.nodes[this.nodeIndex].FirstChild = 0;
			this.nodes[this.nodeIndex].Parent = parent;
			this.nodes[this.nodeIndex].FirstAttribute = firstAttribute;
			this.nodes[this.nodeIndex].PreviousSibling = previousSibling;
			this.nodes[this.nodeIndex].NextSibling = 0;
			this.nodes[this.nodeIndex].NodeType = nodeType;
			this.nodes[this.nodeIndex].BaseURI = baseUri;
			this.nodes[this.nodeIndex].IsEmptyElement = isEmptyElement;
			this.nodes[this.nodeIndex].LocalName = localName;
			this.nodes[this.nodeIndex].NamespaceURI = ns;
			this.nodes[this.nodeIndex].Prefix = prefix;
			this.nodes[this.nodeIndex].Value = value;
			this.nodes[this.nodeIndex].XmlLang = xmlLang;
			this.nodes[this.nodeIndex].FirstNamespace = namespaceNode;
			this.nodes[this.nodeIndex].LineNumber = lineNumber;
			this.nodes[this.nodeIndex].LinePosition = linePosition;
		}

		// Token: 0x060001BE RID: 446 RVA: 0x0000EAF0 File Offset: 0x0000CCF0
		public void AddAttribute(int ownerElement, string localName, string ns, string prefix, string value, int lineNumber, int linePosition)
		{
			if (this.attributes.Length < this.attributeIndex + 1)
			{
				this.attributeCapacity *= 4;
				this.SetAttributeArrayLength(this.attributeCapacity);
			}
			this.attributes[this.attributeIndex].OwnerElement = ownerElement;
			this.attributes[this.attributeIndex].LocalName = localName;
			this.attributes[this.attributeIndex].NamespaceURI = ns;
			this.attributes[this.attributeIndex].Prefix = prefix;
			this.attributes[this.attributeIndex].Value = value;
			this.attributes[this.attributeIndex].LineNumber = lineNumber;
			this.attributes[this.attributeIndex].LinePosition = linePosition;
		}

		// Token: 0x060001BF RID: 447 RVA: 0x0000EBD4 File Offset: 0x0000CDD4
		public void AddNsNode(int declaredElement, string name, string ns, int nextNs)
		{
			if (this.namespaces.Length < this.nsIndex + 1)
			{
				this.nsCapacity *= 4;
				this.SetNsArrayLength(this.nsCapacity);
			}
			this.namespaces[this.nsIndex].DeclaredElement = declaredElement;
			this.namespaces[this.nsIndex].Name = name;
			this.namespaces[this.nsIndex].Namespace = ns;
			this.namespaces[this.nsIndex].NextNamespace = nextNs;
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x060001C0 RID: 448 RVA: 0x0000EC70 File Offset: 0x0000CE70
		public override string XmlLang
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x060001C1 RID: 449 RVA: 0x0000EC74 File Offset: 0x0000CE74
		public override XmlSpace XmlSpace
		{
			get
			{
				return XmlSpace.None;
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x060001C2 RID: 450 RVA: 0x0000EC78 File Offset: 0x0000CE78
		public override WriteState WriteState
		{
			get
			{
				return this.state;
			}
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x0000EC80 File Offset: 0x0000CE80
		public override void Close()
		{
			this.SetNodeArrayLength(this.nodeIndex + 1);
			this.SetAttributeArrayLength(this.attributeIndex + 1);
			this.SetNsArrayLength(this.nsIndex + 1);
			this.isClosed = true;
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x0000ECB4 File Offset: 0x0000CEB4
		public override void Flush()
		{
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x0000ECB8 File Offset: 0x0000CEB8
		public override string LookupPrefix(string ns)
		{
			for (int nextNamespace = this.nsIndex; nextNamespace != 0; nextNamespace = this.namespaces[nextNamespace].NextNamespace)
			{
				if (this.namespaces[nextNamespace].Namespace == ns)
				{
					return this.namespaces[nextNamespace].Name;
				}
			}
			return null;
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x0000ED18 File Offset: 0x0000CF18
		public override void WriteCData(string data)
		{
			this.AddTextNode(data);
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x0000ED24 File Offset: 0x0000CF24
		private void AddTextNode(string data)
		{
			switch (this.state)
			{
			case WriteState.Element:
				this.CloseStartElement();
				goto IL_4B;
			case WriteState.Content:
				goto IL_4B;
			}
			throw new InvalidOperationException("Invalid document state for CDATA section: " + this.state);
			IL_4B:
			if (this.nodes[this.nodeIndex].Parent == this.parentStack[this.parentStackIndex])
			{
				XPathNodeType nodeType = this.nodes[this.nodeIndex].NodeType;
				if (nodeType == XPathNodeType.Text || nodeType == XPathNodeType.SignificantWhitespace)
				{
					string text = this.nodes[this.nodeIndex].Value + data;
					this.nodes[this.nodeIndex].Value = text;
					if (this.IsWhitespace(text))
					{
						this.nodes[this.nodeIndex].NodeType = XPathNodeType.SignificantWhitespace;
					}
					else
					{
						this.nodes[this.nodeIndex].NodeType = XPathNodeType.Text;
					}
					return;
				}
			}
			int parentIndex = this.GetParentIndex();
			this.UpdateTreeForAddition();
			this.AddNode(parentIndex, 0, this.prevSibling, XPathNodeType.Text, null, false, null, string.Empty, string.Empty, data, null, 0, 0, 0);
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x0000EE70 File Offset: 0x0000D070
		private void CheckTopLevelNode()
		{
			switch (this.state)
			{
			case WriteState.Start:
			case WriteState.Prolog:
			case WriteState.Content:
				return;
			case WriteState.Element:
				this.CloseStartElement();
				return;
			}
			throw new InvalidOperationException("Invalid document state for CDATA section: " + this.state);
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x0000EED0 File Offset: 0x0000D0D0
		public override void WriteComment(string data)
		{
			this.CheckTopLevelNode();
			int parentIndex = this.GetParentIndex();
			this.UpdateTreeForAddition();
			this.AddNode(parentIndex, 0, this.prevSibling, XPathNodeType.Comment, null, false, null, string.Empty, string.Empty, data, null, 0, 0, 0);
		}

		// Token: 0x060001CA RID: 458 RVA: 0x0000EF14 File Offset: 0x0000D114
		public override void WriteProcessingInstruction(string name, string data)
		{
			this.CheckTopLevelNode();
			int parentIndex = this.GetParentIndex();
			this.UpdateTreeForAddition();
			this.AddNode(parentIndex, 0, this.prevSibling, XPathNodeType.ProcessingInstruction, null, false, name, string.Empty, string.Empty, data, null, 0, 0, 0);
		}

		// Token: 0x060001CB RID: 459 RVA: 0x0000EF58 File Offset: 0x0000D158
		public override void WriteWhitespace(string data)
		{
			this.CheckTopLevelNode();
			int parentIndex = this.GetParentIndex();
			this.UpdateTreeForAddition();
			this.AddNode(parentIndex, 0, this.prevSibling, XPathNodeType.Whitespace, null, false, null, string.Empty, string.Empty, data, null, 0, 0, 0);
		}

		// Token: 0x060001CC RID: 460 RVA: 0x0000EF9C File Offset: 0x0000D19C
		public override void WriteStartDocument()
		{
		}

		// Token: 0x060001CD RID: 461 RVA: 0x0000EFA0 File Offset: 0x0000D1A0
		public override void WriteStartDocument(bool standalone)
		{
		}

		// Token: 0x060001CE RID: 462 RVA: 0x0000EFA4 File Offset: 0x0000D1A4
		public override void WriteEndDocument()
		{
		}

		// Token: 0x060001CF RID: 463 RVA: 0x0000EFA8 File Offset: 0x0000D1A8
		public override void WriteStartElement(string prefix, string localName, string ns)
		{
			switch (this.state)
			{
			case WriteState.Start:
			case WriteState.Prolog:
			case WriteState.Content:
				goto IL_51;
			case WriteState.Element:
				this.CloseStartElement();
				goto IL_51;
			}
			throw new InvalidOperationException("Invalid document state for writing element: " + this.state);
			IL_51:
			int parentIndex = this.GetParentIndex();
			this.UpdateTreeForAddition();
			this.WriteStartElement(parentIndex, this.prevSibling, prefix, localName, ns);
			this.state = WriteState.Element;
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x0000F02C File Offset: 0x0000D22C
		private void WriteStartElement(int parent, int previousSibling, string prefix, string localName, string ns)
		{
			this.PrepareStartElement(previousSibling);
			this.AddNode(parent, 0, previousSibling, XPathNodeType.Element, null, false, localName, ns, prefix, string.Empty, null, this.lastNsInScope, 0, 0);
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x0000F060 File Offset: 0x0000D260
		private void PrepareStartElement(int previousSibling)
		{
			this.hasAttributes = false;
			this.hasLocalNs = false;
			this.attrIndexAtStart = this.attributeIndex;
			this.nsIndexAtStart = this.nsIndex;
			while (this.namespaces[this.lastNsInScope].DeclaredElement == previousSibling)
			{
				this.lastNsInScope = this.namespaces[this.lastNsInScope].NextNamespace;
			}
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x0000F0D0 File Offset: 0x0000D2D0
		public override void WriteEndElement()
		{
			this.WriteEndElement(false);
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x0000F0DC File Offset: 0x0000D2DC
		public override void WriteFullEndElement()
		{
			this.WriteEndElement(true);
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x0000F0E8 File Offset: 0x0000D2E8
		private void WriteEndElement(bool full)
		{
			switch (this.state)
			{
			case WriteState.Element:
				this.CloseStartElement();
				goto IL_4B;
			case WriteState.Content:
				goto IL_4B;
			}
			throw new InvalidOperationException("Invalid state for writing EndElement: " + this.state);
			IL_4B:
			this.parentStackIndex--;
			if (this.nodes[this.nodeIndex].NodeType == XPathNodeType.Element && !full)
			{
				this.nodes[this.nodeIndex].IsEmptyElement = true;
			}
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x0000F188 File Offset: 0x0000D388
		public override void WriteStartAttribute(string prefix, string localName, string ns)
		{
			if (this.state != WriteState.Element)
			{
				throw new InvalidOperationException("Invalid document state for attribute: " + this.state);
			}
			this.state = WriteState.Attribute;
			if (ns == "http://www.w3.org/2000/xmlns/")
			{
				this.ProcessNamespace((prefix != null && !(prefix == string.Empty)) ? localName : string.Empty, string.Empty);
			}
			else
			{
				this.ProcessAttribute(prefix, localName, ns, string.Empty);
			}
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x0000F214 File Offset: 0x0000D414
		private void ProcessNamespace(string prefix, string ns)
		{
			int nextNs = (!this.hasLocalNs) ? this.nodes[this.nodeIndex].FirstNamespace : this.nsIndex;
			this.nsIndex++;
			this.AddNsNode(this.nodeIndex, prefix, ns, nextNs);
			this.hasLocalNs = true;
			this.openNamespace = true;
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x0000F27C File Offset: 0x0000D47C
		private void ProcessAttribute(string prefix, string localName, string ns, string value)
		{
			this.attributeIndex++;
			this.AddAttribute(this.nodeIndex, localName, ns, (prefix == null) ? string.Empty : prefix, value, 0, 0);
			if (this.hasAttributes)
			{
				this.attributes[this.attributeIndex - 1].NextAttribute = this.attributeIndex;
			}
			else
			{
				this.hasAttributes = true;
			}
		}

		// Token: 0x060001D8 RID: 472 RVA: 0x0000F2F0 File Offset: 0x0000D4F0
		public override void WriteEndAttribute()
		{
			if (this.state != WriteState.Attribute)
			{
				throw new InvalidOperationException();
			}
			this.openNamespace = false;
			this.state = WriteState.Element;
		}

		// Token: 0x060001D9 RID: 473 RVA: 0x0000F320 File Offset: 0x0000D520
		public override void WriteString(string text)
		{
			if (this.WriteState == WriteState.Attribute)
			{
				if (this.openNamespace)
				{
					DTMXPathNamespaceNode[] array = this.namespaces;
					int num = this.nsIndex;
					array[num].Namespace = array[num].Namespace + text;
				}
				else
				{
					DTMXPathAttributeNode[] array2 = this.attributes;
					int num2 = this.attributeIndex;
					array2[num2].Value = array2[num2].Value + text;
				}
			}
			else
			{
				this.AddTextNode(text);
			}
		}

		// Token: 0x060001DA RID: 474 RVA: 0x0000F39C File Offset: 0x0000D59C
		public override void WriteRaw(string data)
		{
			this.WriteString(data);
		}

		// Token: 0x060001DB RID: 475 RVA: 0x0000F3A8 File Offset: 0x0000D5A8
		public override void WriteRaw(char[] data, int start, int len)
		{
			this.WriteString(new string(data, start, len));
		}

		// Token: 0x060001DC RID: 476 RVA: 0x0000F3B8 File Offset: 0x0000D5B8
		public override void WriteName(string name)
		{
			this.WriteString(name);
		}

		// Token: 0x060001DD RID: 477 RVA: 0x0000F3C4 File Offset: 0x0000D5C4
		public override void WriteNmToken(string name)
		{
			this.WriteString(name);
		}

		// Token: 0x060001DE RID: 478 RVA: 0x0000F3D0 File Offset: 0x0000D5D0
		public override void WriteBase64(byte[] buffer, int index, int count)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060001DF RID: 479 RVA: 0x0000F3D8 File Offset: 0x0000D5D8
		public override void WriteBinHex(byte[] buffer, int index, int count)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x0000F3E0 File Offset: 0x0000D5E0
		public override void WriteChars(char[] buffer, int index, int count)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x0000F3E8 File Offset: 0x0000D5E8
		public override void WriteCharEntity(char c)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x0000F3F0 File Offset: 0x0000D5F0
		public override void WriteDocType(string name, string pub, string sys, string intSubset)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x0000F3F8 File Offset: 0x0000D5F8
		public override void WriteEntityRef(string name)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x0000F400 File Offset: 0x0000D600
		public override void WriteQualifiedName(string localName, string ns)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060001E5 RID: 485 RVA: 0x0000F408 File Offset: 0x0000D608
		public override void WriteSurrogateCharEntity(char high, char low)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060001E6 RID: 486 RVA: 0x0000F410 File Offset: 0x0000D610
		private bool IsWhitespace(string data)
		{
			foreach (char c in data)
			{
				switch (c)
				{
				case '\t':
				case '\n':
				case '\r':
					break;
				default:
					if (c != ' ')
					{
						return false;
					}
					break;
				}
			}
			return true;
		}

		// Token: 0x04000171 RID: 369
		private XmlNameTable nameTable;

		// Token: 0x04000172 RID: 370
		private int nodeCapacity;

		// Token: 0x04000173 RID: 371
		private int attributeCapacity;

		// Token: 0x04000174 RID: 372
		private int nsCapacity;

		// Token: 0x04000175 RID: 373
		private DTMXPathLinkedNode[] nodes;

		// Token: 0x04000176 RID: 374
		private DTMXPathAttributeNode[] attributes;

		// Token: 0x04000177 RID: 375
		private DTMXPathNamespaceNode[] namespaces;

		// Token: 0x04000178 RID: 376
		private Hashtable idTable;

		// Token: 0x04000179 RID: 377
		private int nodeIndex;

		// Token: 0x0400017A RID: 378
		private int attributeIndex;

		// Token: 0x0400017B RID: 379
		private int nsIndex;

		// Token: 0x0400017C RID: 380
		private int[] parentStack = new int[10];

		// Token: 0x0400017D RID: 381
		private int parentStackIndex;

		// Token: 0x0400017E RID: 382
		private bool hasAttributes;

		// Token: 0x0400017F RID: 383
		private bool hasLocalNs;

		// Token: 0x04000180 RID: 384
		private int attrIndexAtStart;

		// Token: 0x04000181 RID: 385
		private int nsIndexAtStart;

		// Token: 0x04000182 RID: 386
		private int lastNsInScope;

		// Token: 0x04000183 RID: 387
		private int prevSibling;

		// Token: 0x04000184 RID: 388
		private WriteState state;

		// Token: 0x04000185 RID: 389
		private bool openNamespace;

		// Token: 0x04000186 RID: 390
		private bool isClosed;
	}
}
