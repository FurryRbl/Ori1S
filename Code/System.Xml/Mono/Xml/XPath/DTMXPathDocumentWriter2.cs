using System;
using System.Collections;
using System.Xml;
using System.Xml.XPath;

namespace Mono.Xml.XPath
{
	// Token: 0x0200003D RID: 61
	internal class DTMXPathDocumentWriter2 : XmlWriter
	{
		// Token: 0x0600022C RID: 556 RVA: 0x00011444 File Offset: 0x0000F644
		public DTMXPathDocumentWriter2(XmlNameTable nt, int defaultCapacity)
		{
			this.nameTable = ((nt != null) ? nt : new NameTable());
			this.nodeCapacity = defaultCapacity;
			this.attributeCapacity = this.nodeCapacity;
			this.nsCapacity = 10;
			this.idTable = new Hashtable();
			this.nodes = new DTMXPathLinkedNode2[this.nodeCapacity];
			this.attributes = new DTMXPathAttributeNode2[this.attributeCapacity];
			this.namespaces = new DTMXPathNamespaceNode2[this.nsCapacity];
			this.atomicStringPool = new string[20];
			this.nonAtomicStringPool = new string[20];
			this.Init();
		}

		// Token: 0x0600022D RID: 557 RVA: 0x000114F4 File Offset: 0x0000F6F4
		public DTMXPathDocument2 CreateDocument()
		{
			if (!this.isClosed)
			{
				this.Close();
			}
			return new DTMXPathDocument2(this.nameTable, this.nodes, this.attributes, this.namespaces, this.atomicStringPool, this.nonAtomicStringPool, this.idTable);
		}

		// Token: 0x0600022E RID: 558 RVA: 0x00011544 File Offset: 0x0000F744
		public void Init()
		{
			this.atomicStringPool[0] = (this.nonAtomicStringPool[0] = string.Empty);
			this.atomicStringPool[1] = (this.nonAtomicStringPool[1] = null);
			this.atomicStringPool[2] = (this.nonAtomicStringPool[2] = "http://www.w3.org/XML/1998/namespace");
			this.atomicStringPool[3] = (this.nonAtomicStringPool[3] = "http://www.w3.org/2000/xmlns/");
			this.atomicIndex = (this.nonAtomicIndex = 4);
			this.AddNode(0, 0, 0, XPathNodeType.All, string.Empty, false, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, 0, 0, 0);
			this.nodeIndex++;
			this.AddAttribute(0, string.Empty, string.Empty, string.Empty, string.Empty, 0, 0);
			this.AddNsNode(0, string.Empty, string.Empty, 0);
			this.nsIndex++;
			this.AddNsNode(1, "xml", "http://www.w3.org/XML/1998/namespace", 0);
			this.AddNode(0, 0, 0, XPathNodeType.Root, string.Empty, false, string.Empty, string.Empty, string.Empty, string.Empty, string.Empty, 1, 0, 0);
			this.nodeIndex = 1;
			this.lastNsInScope = 1;
			this.parentStack[0] = this.nodeIndex;
			this.state = WriteState.Content;
		}

		// Token: 0x0600022F RID: 559 RVA: 0x00011698 File Offset: 0x0000F898
		private int GetParentIndex()
		{
			return this.parentStack[this.parentStackIndex];
		}

		// Token: 0x06000230 RID: 560 RVA: 0x000116A8 File Offset: 0x0000F8A8
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

		// Token: 0x06000231 RID: 561 RVA: 0x00011708 File Offset: 0x0000F908
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

		// Token: 0x06000232 RID: 562 RVA: 0x00011784 File Offset: 0x0000F984
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

		// Token: 0x06000233 RID: 563 RVA: 0x0001185C File Offset: 0x0000FA5C
		private int AtomicIndex(string s)
		{
			if (s == string.Empty)
			{
				return 0;
			}
			if (s == null)
			{
				return 1;
			}
			for (int i = 2; i < this.atomicIndex; i++)
			{
				if (object.ReferenceEquals(s, this.atomicStringPool[i]))
				{
					return i;
				}
			}
			if (this.atomicIndex == this.atomicStringPool.Length)
			{
				string[] destinationArray = new string[this.atomicIndex * 2];
				Array.Copy(this.atomicStringPool, destinationArray, this.atomicIndex);
				this.atomicStringPool = destinationArray;
			}
			this.atomicStringPool[this.atomicIndex] = s;
			return this.atomicIndex++;
		}

		// Token: 0x06000234 RID: 564 RVA: 0x00011908 File Offset: 0x0000FB08
		private int NonAtomicIndex(string s)
		{
			if (s == string.Empty)
			{
				return 0;
			}
			if (s == null)
			{
				return 1;
			}
			int i = 2;
			int num = (this.nonAtomicIndex >= 100) ? 100 : this.nonAtomicIndex;
			while (i < num)
			{
				if (s == this.nonAtomicStringPool[i])
				{
					return i;
				}
				i++;
			}
			if (this.nonAtomicIndex == this.nonAtomicStringPool.Length)
			{
				string[] destinationArray = new string[this.nonAtomicIndex * 2];
				Array.Copy(this.nonAtomicStringPool, destinationArray, this.nonAtomicIndex);
				this.nonAtomicStringPool = destinationArray;
			}
			this.nonAtomicStringPool[this.nonAtomicIndex] = s;
			return this.nonAtomicIndex++;
		}

		// Token: 0x06000235 RID: 565 RVA: 0x000119CC File Offset: 0x0000FBCC
		private void SetNodeArrayLength(int size)
		{
			DTMXPathLinkedNode2[] destinationArray = new DTMXPathLinkedNode2[size];
			Array.Copy(this.nodes, destinationArray, Math.Min(size, this.nodes.Length));
			this.nodes = destinationArray;
		}

		// Token: 0x06000236 RID: 566 RVA: 0x00011A04 File Offset: 0x0000FC04
		private void SetAttributeArrayLength(int size)
		{
			DTMXPathAttributeNode2[] destinationArray = new DTMXPathAttributeNode2[size];
			Array.Copy(this.attributes, destinationArray, Math.Min(size, this.attributes.Length));
			this.attributes = destinationArray;
		}

		// Token: 0x06000237 RID: 567 RVA: 0x00011A3C File Offset: 0x0000FC3C
		private void SetNsArrayLength(int size)
		{
			DTMXPathNamespaceNode2[] destinationArray = new DTMXPathNamespaceNode2[size];
			Array.Copy(this.namespaces, destinationArray, Math.Min(size, this.namespaces.Length));
			this.namespaces = destinationArray;
		}

		// Token: 0x06000238 RID: 568 RVA: 0x00011A74 File Offset: 0x0000FC74
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
			this.nodes[this.nodeIndex].BaseURI = this.AtomicIndex(baseUri);
			this.nodes[this.nodeIndex].IsEmptyElement = isEmptyElement;
			this.nodes[this.nodeIndex].LocalName = this.AtomicIndex(localName);
			this.nodes[this.nodeIndex].NamespaceURI = this.AtomicIndex(ns);
			this.nodes[this.nodeIndex].Prefix = this.AtomicIndex(prefix);
			this.nodes[this.nodeIndex].Value = this.NonAtomicIndex(value);
			this.nodes[this.nodeIndex].XmlLang = this.AtomicIndex(xmlLang);
			this.nodes[this.nodeIndex].FirstNamespace = namespaceNode;
			this.nodes[this.nodeIndex].LineNumber = lineNumber;
			this.nodes[this.nodeIndex].LinePosition = linePosition;
		}

		// Token: 0x06000239 RID: 569 RVA: 0x00011C50 File Offset: 0x0000FE50
		public void AddAttribute(int ownerElement, string localName, string ns, string prefix, string value, int lineNumber, int linePosition)
		{
			if (this.attributes.Length < this.attributeIndex + 1)
			{
				this.attributeCapacity *= 4;
				this.SetAttributeArrayLength(this.attributeCapacity);
			}
			this.attributes[this.attributeIndex].OwnerElement = ownerElement;
			this.attributes[this.attributeIndex].LocalName = this.AtomicIndex(localName);
			this.attributes[this.attributeIndex].NamespaceURI = this.AtomicIndex(ns);
			this.attributes[this.attributeIndex].Prefix = this.AtomicIndex(prefix);
			this.attributes[this.attributeIndex].Value = this.NonAtomicIndex(value);
			this.attributes[this.attributeIndex].LineNumber = lineNumber;
			this.attributes[this.attributeIndex].LinePosition = linePosition;
		}

		// Token: 0x0600023A RID: 570 RVA: 0x00011D4C File Offset: 0x0000FF4C
		public void AddNsNode(int declaredElement, string name, string ns, int nextNs)
		{
			if (this.namespaces.Length < this.nsIndex + 1)
			{
				this.nsCapacity *= 4;
				this.SetNsArrayLength(this.nsCapacity);
			}
			this.namespaces[this.nsIndex].DeclaredElement = declaredElement;
			this.namespaces[this.nsIndex].Name = this.AtomicIndex(name);
			this.namespaces[this.nsIndex].Namespace = this.AtomicIndex(ns);
			this.namespaces[this.nsIndex].NextNamespace = nextNs;
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x0600023B RID: 571 RVA: 0x00011DF4 File Offset: 0x0000FFF4
		public override string XmlLang
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x0600023C RID: 572 RVA: 0x00011DF8 File Offset: 0x0000FFF8
		public override XmlSpace XmlSpace
		{
			get
			{
				return XmlSpace.None;
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x0600023D RID: 573 RVA: 0x00011DFC File Offset: 0x0000FFFC
		public override WriteState WriteState
		{
			get
			{
				return this.state;
			}
		}

		// Token: 0x0600023E RID: 574 RVA: 0x00011E04 File Offset: 0x00010004
		public override void Close()
		{
			this.SetNodeArrayLength(this.nodeIndex + 1);
			this.SetAttributeArrayLength(this.attributeIndex + 1);
			this.SetNsArrayLength(this.nsIndex + 1);
			string[] destinationArray = new string[this.atomicIndex];
			Array.Copy(this.atomicStringPool, destinationArray, this.atomicIndex);
			this.atomicStringPool = destinationArray;
			destinationArray = new string[this.nonAtomicIndex];
			Array.Copy(this.nonAtomicStringPool, destinationArray, this.nonAtomicIndex);
			this.nonAtomicStringPool = destinationArray;
			this.isClosed = true;
		}

		// Token: 0x0600023F RID: 575 RVA: 0x00011E8C File Offset: 0x0001008C
		public override void Flush()
		{
		}

		// Token: 0x06000240 RID: 576 RVA: 0x00011E90 File Offset: 0x00010090
		public override string LookupPrefix(string ns)
		{
			for (int nextNamespace = this.nsIndex; nextNamespace != 0; nextNamespace = this.namespaces[nextNamespace].NextNamespace)
			{
				if (this.atomicStringPool[this.namespaces[nextNamespace].Namespace] == ns)
				{
					return this.atomicStringPool[this.namespaces[nextNamespace].Name];
				}
			}
			return null;
		}

		// Token: 0x06000241 RID: 577 RVA: 0x00011F00 File Offset: 0x00010100
		public override void WriteCData(string data)
		{
			this.AddTextNode(data);
		}

		// Token: 0x06000242 RID: 578 RVA: 0x00011F0C File Offset: 0x0001010C
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
					string text = this.nonAtomicStringPool[this.nodes[this.nodeIndex].Value] + data;
					this.nodes[this.nodeIndex].Value = this.NonAtomicIndex(text);
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
			this.AddNode(parentIndex, 0, this.prevSibling, XPathNodeType.Text, null, false, string.Empty, string.Empty, string.Empty, data, null, 0, 0, 0);
		}

		// Token: 0x06000243 RID: 579 RVA: 0x0001206C File Offset: 0x0001026C
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

		// Token: 0x06000244 RID: 580 RVA: 0x000120CC File Offset: 0x000102CC
		public override void WriteComment(string data)
		{
			this.CheckTopLevelNode();
			int parentIndex = this.GetParentIndex();
			this.UpdateTreeForAddition();
			this.AddNode(parentIndex, 0, this.prevSibling, XPathNodeType.Comment, null, false, string.Empty, string.Empty, string.Empty, data, null, 0, 0, 0);
		}

		// Token: 0x06000245 RID: 581 RVA: 0x00012114 File Offset: 0x00010314
		public override void WriteProcessingInstruction(string name, string data)
		{
			this.CheckTopLevelNode();
			int parentIndex = this.GetParentIndex();
			this.UpdateTreeForAddition();
			this.AddNode(parentIndex, 0, this.prevSibling, XPathNodeType.ProcessingInstruction, null, false, name, string.Empty, string.Empty, data, null, 0, 0, 0);
		}

		// Token: 0x06000246 RID: 582 RVA: 0x00012158 File Offset: 0x00010358
		public override void WriteWhitespace(string data)
		{
			this.CheckTopLevelNode();
			int parentIndex = this.GetParentIndex();
			this.UpdateTreeForAddition();
			this.AddNode(parentIndex, 0, this.prevSibling, XPathNodeType.Whitespace, null, false, string.Empty, string.Empty, string.Empty, data, null, 0, 0, 0);
		}

		// Token: 0x06000247 RID: 583 RVA: 0x000121A0 File Offset: 0x000103A0
		public override void WriteStartDocument()
		{
		}

		// Token: 0x06000248 RID: 584 RVA: 0x000121A4 File Offset: 0x000103A4
		public override void WriteStartDocument(bool standalone)
		{
		}

		// Token: 0x06000249 RID: 585 RVA: 0x000121A8 File Offset: 0x000103A8
		public override void WriteEndDocument()
		{
		}

		// Token: 0x0600024A RID: 586 RVA: 0x000121AC File Offset: 0x000103AC
		public override void WriteStartElement(string prefix, string localName, string ns)
		{
			if (ns == null)
			{
				ns = string.Empty;
			}
			else if (prefix == null && ns.Length > 0)
			{
				prefix = this.LookupPrefix(ns);
			}
			if (prefix == null)
			{
				prefix = string.Empty;
			}
			switch (this.state)
			{
			case WriteState.Start:
			case WriteState.Prolog:
			case WriteState.Content:
				goto IL_8B;
			case WriteState.Element:
				this.CloseStartElement();
				goto IL_8B;
			}
			throw new InvalidOperationException("Invalid document state for writing element: " + this.state);
			IL_8B:
			int parentIndex = this.GetParentIndex();
			this.UpdateTreeForAddition();
			this.WriteStartElement(parentIndex, this.prevSibling, prefix, localName, ns);
			this.state = WriteState.Element;
		}

		// Token: 0x0600024B RID: 587 RVA: 0x00012268 File Offset: 0x00010468
		private void WriteStartElement(int parent, int previousSibling, string prefix, string localName, string ns)
		{
			this.PrepareStartElement(previousSibling);
			this.AddNode(parent, 0, previousSibling, XPathNodeType.Element, null, false, localName, ns, prefix, string.Empty, null, this.lastNsInScope, 0, 0);
		}

		// Token: 0x0600024C RID: 588 RVA: 0x0001229C File Offset: 0x0001049C
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

		// Token: 0x0600024D RID: 589 RVA: 0x0001230C File Offset: 0x0001050C
		public override void WriteEndElement()
		{
			this.WriteEndElement(false);
		}

		// Token: 0x0600024E RID: 590 RVA: 0x00012318 File Offset: 0x00010518
		public override void WriteFullEndElement()
		{
			this.WriteEndElement(true);
		}

		// Token: 0x0600024F RID: 591 RVA: 0x00012324 File Offset: 0x00010524
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

		// Token: 0x06000250 RID: 592 RVA: 0x000123C4 File Offset: 0x000105C4
		public override void WriteStartAttribute(string prefix, string localName, string ns)
		{
			if (ns == null)
			{
				ns = string.Empty;
			}
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

		// Token: 0x06000251 RID: 593 RVA: 0x0001245C File Offset: 0x0001065C
		private void ProcessNamespace(string prefix, string ns)
		{
			int nextNs = (!this.hasLocalNs) ? this.nodes[this.nodeIndex].FirstNamespace : this.nsIndex;
			this.nsIndex++;
			this.AddNsNode(this.nodeIndex, prefix, ns, nextNs);
			this.hasLocalNs = true;
			this.openNamespace = true;
		}

		// Token: 0x06000252 RID: 594 RVA: 0x000124C4 File Offset: 0x000106C4
		private void ProcessAttribute(string prefix, string localName, string ns, string value)
		{
			if (prefix == null && ns.Length > 0)
			{
				prefix = this.LookupPrefix(ns);
			}
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

		// Token: 0x06000253 RID: 595 RVA: 0x00012554 File Offset: 0x00010754
		public override void WriteEndAttribute()
		{
			if (this.state != WriteState.Attribute)
			{
				throw new InvalidOperationException();
			}
			this.openNamespace = false;
			this.state = WriteState.Element;
		}

		// Token: 0x06000254 RID: 596 RVA: 0x00012584 File Offset: 0x00010784
		public override void WriteString(string text)
		{
			if (this.WriteState == WriteState.Attribute)
			{
				if (this.openNamespace)
				{
					string s = this.atomicStringPool[this.namespaces[this.nsIndex].Namespace] + text;
					this.namespaces[this.nsIndex].Namespace = this.AtomicIndex(s);
				}
				else
				{
					string s2 = this.nonAtomicStringPool[this.attributes[this.attributeIndex].Value] + text;
					this.attributes[this.attributeIndex].Value = this.NonAtomicIndex(s2);
				}
			}
			else
			{
				this.AddTextNode(text);
			}
		}

		// Token: 0x06000255 RID: 597 RVA: 0x0001263C File Offset: 0x0001083C
		public override void WriteRaw(string data)
		{
			this.WriteString(data);
		}

		// Token: 0x06000256 RID: 598 RVA: 0x00012648 File Offset: 0x00010848
		public override void WriteRaw(char[] data, int start, int len)
		{
			this.WriteString(new string(data, start, len));
		}

		// Token: 0x06000257 RID: 599 RVA: 0x00012658 File Offset: 0x00010858
		public override void WriteName(string name)
		{
			this.WriteString(name);
		}

		// Token: 0x06000258 RID: 600 RVA: 0x00012664 File Offset: 0x00010864
		public override void WriteNmToken(string name)
		{
			this.WriteString(name);
		}

		// Token: 0x06000259 RID: 601 RVA: 0x00012670 File Offset: 0x00010870
		public override void WriteBase64(byte[] buffer, int index, int count)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600025A RID: 602 RVA: 0x00012678 File Offset: 0x00010878
		public override void WriteBinHex(byte[] buffer, int index, int count)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600025B RID: 603 RVA: 0x00012680 File Offset: 0x00010880
		public override void WriteChars(char[] buffer, int index, int count)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600025C RID: 604 RVA: 0x00012688 File Offset: 0x00010888
		public override void WriteCharEntity(char c)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600025D RID: 605 RVA: 0x00012690 File Offset: 0x00010890
		public override void WriteDocType(string name, string pub, string sys, string intSubset)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600025E RID: 606 RVA: 0x00012698 File Offset: 0x00010898
		public override void WriteEntityRef(string name)
		{
			throw new NotSupportedException();
		}

		// Token: 0x0600025F RID: 607 RVA: 0x000126A0 File Offset: 0x000108A0
		public override void WriteQualifiedName(string localName, string ns)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000260 RID: 608 RVA: 0x000126A8 File Offset: 0x000108A8
		public override void WriteSurrogateCharEntity(char high, char low)
		{
			throw new NotSupportedException();
		}

		// Token: 0x06000261 RID: 609 RVA: 0x000126B0 File Offset: 0x000108B0
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

		// Token: 0x040001D6 RID: 470
		private XmlNameTable nameTable;

		// Token: 0x040001D7 RID: 471
		private int nodeCapacity;

		// Token: 0x040001D8 RID: 472
		private int attributeCapacity;

		// Token: 0x040001D9 RID: 473
		private int nsCapacity;

		// Token: 0x040001DA RID: 474
		private DTMXPathLinkedNode2[] nodes;

		// Token: 0x040001DB RID: 475
		private DTMXPathAttributeNode2[] attributes;

		// Token: 0x040001DC RID: 476
		private DTMXPathNamespaceNode2[] namespaces;

		// Token: 0x040001DD RID: 477
		private string[] atomicStringPool;

		// Token: 0x040001DE RID: 478
		private int atomicIndex;

		// Token: 0x040001DF RID: 479
		private string[] nonAtomicStringPool;

		// Token: 0x040001E0 RID: 480
		private int nonAtomicIndex;

		// Token: 0x040001E1 RID: 481
		private Hashtable idTable;

		// Token: 0x040001E2 RID: 482
		private int nodeIndex;

		// Token: 0x040001E3 RID: 483
		private int attributeIndex;

		// Token: 0x040001E4 RID: 484
		private int nsIndex;

		// Token: 0x040001E5 RID: 485
		private int[] parentStack = new int[10];

		// Token: 0x040001E6 RID: 486
		private int parentStackIndex;

		// Token: 0x040001E7 RID: 487
		private bool hasAttributes;

		// Token: 0x040001E8 RID: 488
		private bool hasLocalNs;

		// Token: 0x040001E9 RID: 489
		private int attrIndexAtStart;

		// Token: 0x040001EA RID: 490
		private int nsIndexAtStart;

		// Token: 0x040001EB RID: 491
		private int lastNsInScope;

		// Token: 0x040001EC RID: 492
		private int prevSibling;

		// Token: 0x040001ED RID: 493
		private WriteState state;

		// Token: 0x040001EE RID: 494
		private bool openNamespace;

		// Token: 0x040001EF RID: 495
		private bool isClosed;
	}
}
