using System;
using System.Collections;
using System.Xml;
using System.Xml.Schema;
using System.Xml.XPath;

namespace Mono.Xml.XPath
{
	// Token: 0x0200003C RID: 60
	internal class DTMXPathDocumentBuilder2
	{
		// Token: 0x06000213 RID: 531 RVA: 0x000102F8 File Offset: 0x0000E4F8
		public DTMXPathDocumentBuilder2(string url) : this(url, XmlSpace.None, 200)
		{
		}

		// Token: 0x06000214 RID: 532 RVA: 0x00010308 File Offset: 0x0000E508
		public DTMXPathDocumentBuilder2(string url, XmlSpace space) : this(url, space, 200)
		{
		}

		// Token: 0x06000215 RID: 533 RVA: 0x00010318 File Offset: 0x0000E518
		public DTMXPathDocumentBuilder2(string url, XmlSpace space, int defaultCapacity)
		{
			this.parentStack = new int[10];
			base..ctor();
			XmlReader xmlReader = null;
			try
			{
				xmlReader = new XmlTextReader(url);
				this.Init(xmlReader, space, defaultCapacity);
			}
			finally
			{
				if (xmlReader != null)
				{
					xmlReader.Close();
				}
			}
		}

		// Token: 0x06000216 RID: 534 RVA: 0x00010378 File Offset: 0x0000E578
		public DTMXPathDocumentBuilder2(XmlReader reader) : this(reader, XmlSpace.None, 200)
		{
		}

		// Token: 0x06000217 RID: 535 RVA: 0x00010388 File Offset: 0x0000E588
		public DTMXPathDocumentBuilder2(XmlReader reader, XmlSpace space) : this(reader, space, 200)
		{
		}

		// Token: 0x06000218 RID: 536 RVA: 0x00010398 File Offset: 0x0000E598
		public DTMXPathDocumentBuilder2(XmlReader reader, XmlSpace space, int defaultCapacity)
		{
			this.parentStack = new int[10];
			base..ctor();
			this.Init(reader, space, defaultCapacity);
		}

		// Token: 0x06000219 RID: 537 RVA: 0x000103B8 File Offset: 0x0000E5B8
		private void Init(XmlReader reader, XmlSpace space, int defaultCapacity)
		{
			this.xmlReader = reader;
			this.validatingReader = (reader as XmlValidatingReader);
			this.lineInfo = (reader as IXmlLineInfo);
			this.xmlSpace = space;
			this.nameTable = reader.NameTable;
			this.nodeCapacity = defaultCapacity;
			this.attributeCapacity = this.nodeCapacity;
			this.nsCapacity = 10;
			this.idTable = new Hashtable();
			this.nodes = new DTMXPathLinkedNode2[this.nodeCapacity];
			this.attributes = new DTMXPathAttributeNode2[this.attributeCapacity];
			this.namespaces = new DTMXPathNamespaceNode2[this.nsCapacity];
			this.atomicStringPool = new string[20];
			this.nonAtomicStringPool = new string[20];
			this.Compile();
		}

		// Token: 0x0600021A RID: 538 RVA: 0x00010470 File Offset: 0x0000E670
		public DTMXPathDocument2 CreateDocument()
		{
			return new DTMXPathDocument2(this.nameTable, this.nodes, this.attributes, this.namespaces, this.atomicStringPool, this.nonAtomicStringPool, this.idTable);
		}

		// Token: 0x0600021B RID: 539 RVA: 0x000104A4 File Offset: 0x0000E6A4
		public void Compile()
		{
			this.atomicStringPool[0] = (this.nonAtomicStringPool[0] = string.Empty);
			this.atomicStringPool[1] = (this.nonAtomicStringPool[1] = null);
			this.atomicStringPool[2] = (this.nonAtomicStringPool[2] = "http://www.w3.org/XML/1998/namespace");
			this.atomicStringPool[3] = (this.nonAtomicStringPool[3] = "http://www.w3.org/2000/xmlns/");
			this.atomicIndex = (this.nonAtomicIndex = 4);
			this.AddNode(0, 0, 0, XPathNodeType.All, 0, false, 0, 0, 0, 0, 0, 0, 0, 0);
			this.nodeIndex++;
			this.AddAttribute(0, 0, 0, 0, 0, 0, 0);
			this.AddNsNode(0, 0, 0, 0);
			this.nsIndex++;
			this.AddNsNode(1, this.AtomicIndex("xml"), this.AtomicIndex("http://www.w3.org/XML/1998/namespace"), 0);
			this.AddNode(0, 0, 0, XPathNodeType.Root, this.AtomicIndex(this.xmlReader.BaseURI), false, 0, 0, 0, 0, 0, 1, 0, 0);
			this.nodeIndex = 1;
			this.lastNsInScope = 1;
			this.parentStack[0] = this.nodeIndex;
			if (this.xmlReader.ReadState == ReadState.Initial)
			{
				this.xmlReader.Read();
			}
			int depth = this.xmlReader.Depth;
			do
			{
				this.Read();
			}
			while (this.skipRead || (this.xmlReader.Read() && this.xmlReader.Depth >= depth));
			this.SetNodeArrayLength(this.nodeIndex + 1);
			this.SetAttributeArrayLength(this.attributeIndex + 1);
			this.SetNsArrayLength(this.nsIndex + 1);
			string[] destinationArray = new string[this.atomicIndex];
			Array.Copy(this.atomicStringPool, destinationArray, this.atomicIndex);
			this.atomicStringPool = destinationArray;
			destinationArray = new string[this.nonAtomicIndex];
			Array.Copy(this.nonAtomicStringPool, destinationArray, this.nonAtomicIndex);
			this.nonAtomicStringPool = destinationArray;
			this.xmlReader = null;
		}

		// Token: 0x0600021C RID: 540 RVA: 0x00010698 File Offset: 0x0000E898
		public void Read()
		{
			this.skipRead = false;
			int num = this.parentStack[this.parentStackIndex];
			int num2 = this.nodeIndex;
			switch (this.xmlReader.NodeType)
			{
			case XmlNodeType.Element:
			case XmlNodeType.Text:
			case XmlNodeType.CDATA:
			case XmlNodeType.ProcessingInstruction:
			case XmlNodeType.Comment:
			case XmlNodeType.Whitespace:
			case XmlNodeType.SignificantWhitespace:
			{
				string text = null;
				XPathNodeType xpathNodeType = XPathNodeType.Root;
				bool flag = false;
				switch (this.xmlReader.NodeType)
				{
				case XmlNodeType.None:
					break;
				case XmlNodeType.Element:
					xpathNodeType = XPathNodeType.Element;
					break;
				case XmlNodeType.Attribute:
				case XmlNodeType.EntityReference:
				case XmlNodeType.Entity:
				case XmlNodeType.Document:
				case XmlNodeType.DocumentType:
				case XmlNodeType.DocumentFragment:
				case XmlNodeType.Notation:
					return;
				case XmlNodeType.Text:
				case XmlNodeType.CDATA:
				case XmlNodeType.Whitespace:
				case XmlNodeType.SignificantWhitespace:
					this.skipRead = true;
					for (;;)
					{
						XmlNodeType nodeType = this.xmlReader.NodeType;
						if (nodeType == XmlNodeType.Text)
						{
							goto IL_1B2;
						}
						if (nodeType == XmlNodeType.CDATA)
						{
							if (xpathNodeType != XPathNodeType.Text)
							{
								text = string.Empty;
							}
							flag = true;
							goto IL_1B2;
						}
						if (nodeType != XmlNodeType.Whitespace)
						{
							if (nodeType != XmlNodeType.SignificantWhitespace)
							{
								break;
							}
							if (xpathNodeType == XPathNodeType.Root || xpathNodeType == XPathNodeType.Whitespace)
							{
								xpathNodeType = XPathNodeType.SignificantWhitespace;
							}
							text += this.xmlReader.Value;
						}
						else
						{
							XPathNodeType xpathNodeType2 = xpathNodeType;
							if (xpathNodeType2 == XPathNodeType.Root)
							{
								xpathNodeType = XPathNodeType.Whitespace;
							}
							if (!flag)
							{
								text += this.xmlReader.Value;
							}
						}
						IL_1D1:
						if (!this.xmlReader.Read())
						{
							break;
						}
						continue;
						IL_1B2:
						xpathNodeType = XPathNodeType.Text;
						text += this.xmlReader.Value;
						goto IL_1D1;
					}
					break;
				case XmlNodeType.ProcessingInstruction:
					text = this.xmlReader.Value;
					xpathNodeType = XPathNodeType.ProcessingInstruction;
					break;
				case XmlNodeType.Comment:
					text = this.xmlReader.Value;
					xpathNodeType = XPathNodeType.Comment;
					break;
				default:
					return;
				}
				if (xpathNodeType == XPathNodeType.Root || (xpathNodeType == XPathNodeType.Whitespace && this.xmlSpace != XmlSpace.Preserve))
				{
					return;
				}
				if (num == this.nodeIndex)
				{
					num2 = 0;
				}
				else
				{
					while (this.nodes[num2].Parent != num)
					{
						num2 = this.nodes[num2].Parent;
					}
				}
				this.nodeIndex++;
				if (num2 != 0)
				{
					this.nodes[num2].NextSibling = this.nodeIndex;
				}
				if (this.parentStack[this.parentStackIndex] == this.nodeIndex - 1)
				{
					this.nodes[num].FirstChild = this.nodeIndex;
				}
				if (xpathNodeType == XPathNodeType.Element)
				{
					this.ProcessElement(num, num2);
				}
				else
				{
					this.AddNode(num, 0, num2, xpathNodeType, this.AtomicIndex(this.xmlReader.BaseURI), this.xmlReader.IsEmptyElement, (!this.skipRead) ? this.AtomicIndex(this.xmlReader.LocalName) : 0, (!this.skipRead) ? this.AtomicIndex(this.xmlReader.NamespaceURI) : 0, this.AtomicIndex(this.xmlReader.Prefix), (text != null) ? this.NonAtomicIndex(text) : 0, this.AtomicIndex(this.xmlReader.XmlLang), this.nsIndex, (this.lineInfo == null) ? 0 : this.lineInfo.LineNumber, (this.lineInfo == null) ? 0 : this.lineInfo.LinePosition);
				}
				return;
			}
			case XmlNodeType.EndElement:
			{
				int target = this.parentStack[this.parentStackIndex];
				this.AdjustLastNsInScope(target);
				this.parentStackIndex--;
				return;
			}
			}
		}

		// Token: 0x0600021D RID: 541 RVA: 0x00010A64 File Offset: 0x0000EC64
		private void ProcessElement(int parent, int previousSibling)
		{
			this.WriteStartElement(parent, previousSibling);
			if (this.xmlReader.MoveToFirstAttribute())
			{
				do
				{
					string prefix = this.xmlReader.Prefix;
					string namespaceURI = this.xmlReader.NamespaceURI;
					if (namespaceURI == "http://www.w3.org/2000/xmlns/")
					{
						this.ProcessNamespace((prefix != null && !(prefix == string.Empty)) ? this.xmlReader.LocalName : string.Empty, this.xmlReader.Value);
					}
					else
					{
						this.ProcessAttribute(prefix, this.xmlReader.LocalName, namespaceURI, this.xmlReader.Value);
					}
				}
				while (this.xmlReader.MoveToNextAttribute());
				this.xmlReader.MoveToElement();
			}
			this.CloseStartElement();
		}

		// Token: 0x0600021E RID: 542 RVA: 0x00010B34 File Offset: 0x0000ED34
		private void PrepareStartElement(int previousSibling)
		{
			this.hasAttributes = false;
			this.hasLocalNs = false;
			this.attrIndexAtStart = this.attributeIndex;
			this.nsIndexAtStart = this.nsIndex;
			this.AdjustLastNsInScope(previousSibling);
		}

		// Token: 0x0600021F RID: 543 RVA: 0x00010B64 File Offset: 0x0000ED64
		private void AdjustLastNsInScope(int target)
		{
			while (this.namespaces[this.lastNsInScope].DeclaredElement == target)
			{
				this.lastNsInScope = this.namespaces[this.lastNsInScope].NextNamespace;
			}
		}

		// Token: 0x06000220 RID: 544 RVA: 0x00010BA4 File Offset: 0x0000EDA4
		private void WriteStartElement(int parent, int previousSibling)
		{
			this.PrepareStartElement(previousSibling);
			this.AddNode(parent, 0, previousSibling, XPathNodeType.Element, this.AtomicIndex(this.xmlReader.BaseURI), this.xmlReader.IsEmptyElement, this.AtomicIndex(this.xmlReader.LocalName), this.AtomicIndex(this.xmlReader.NamespaceURI), this.AtomicIndex(this.xmlReader.Prefix), 0, this.AtomicIndex(this.xmlReader.XmlLang), this.lastNsInScope, (this.lineInfo == null) ? 0 : this.lineInfo.LineNumber, (this.lineInfo == null) ? 0 : this.lineInfo.LinePosition);
		}

		// Token: 0x06000221 RID: 545 RVA: 0x00010C64 File Offset: 0x0000EE64
		private void CloseStartElement()
		{
			if (this.attrIndexAtStart != this.attributeIndex)
			{
				this.nodes[this.nodeIndex].FirstAttribute = this.attrIndexAtStart + 1;
			}
			if (this.nsIndexAtStart != this.nsIndex)
			{
				this.nodes[this.nodeIndex].FirstNamespace = this.nsIndex;
				if (!this.xmlReader.IsEmptyElement)
				{
					this.lastNsInScope = this.nsIndex;
				}
			}
			if (!this.nodes[this.nodeIndex].IsEmptyElement)
			{
				this.parentStackIndex++;
				if (this.parentStack.Length == this.parentStackIndex)
				{
					int[] destinationArray = new int[this.parentStackIndex * 2];
					Array.Copy(this.parentStack, destinationArray, this.parentStackIndex);
					this.parentStack = destinationArray;
				}
				this.parentStack[this.parentStackIndex] = this.nodeIndex;
			}
		}

		// Token: 0x06000222 RID: 546 RVA: 0x00010D60 File Offset: 0x0000EF60
		private void ProcessNamespace(string prefix, string ns)
		{
			int nextNs = (!this.hasLocalNs) ? this.nodes[this.nodeIndex].FirstNamespace : this.nsIndex;
			this.nsIndex++;
			this.AddNsNode(this.nodeIndex, this.AtomicIndex(prefix), this.AtomicIndex(ns), nextNs);
			this.hasLocalNs = true;
		}

		// Token: 0x06000223 RID: 547 RVA: 0x00010DCC File Offset: 0x0000EFCC
		private void ProcessAttribute(string prefix, string localName, string ns, string value)
		{
			this.attributeIndex++;
			this.AddAttribute(this.nodeIndex, this.AtomicIndex(localName), this.AtomicIndex(ns), (prefix == null) ? 0 : this.AtomicIndex(prefix), this.NonAtomicIndex(value), (this.lineInfo == null) ? 0 : this.lineInfo.LineNumber, (this.lineInfo == null) ? 0 : this.lineInfo.LinePosition);
			if (this.hasAttributes)
			{
				this.attributes[this.attributeIndex - 1].NextAttribute = this.attributeIndex;
			}
			else
			{
				this.hasAttributes = true;
			}
			if (this.validatingReader != null)
			{
				XmlSchemaDatatype xmlSchemaDatatype = this.validatingReader.SchemaType as XmlSchemaDatatype;
				if (xmlSchemaDatatype == null)
				{
					XmlSchemaType xmlSchemaType = this.validatingReader.SchemaType as XmlSchemaType;
					if (xmlSchemaType != null)
					{
						xmlSchemaDatatype = xmlSchemaType.Datatype;
					}
				}
				if (xmlSchemaDatatype != null && xmlSchemaDatatype.TokenizedType == XmlTokenizedType.ID)
				{
					this.idTable.Add(value, this.nodeIndex);
				}
			}
		}

		// Token: 0x06000224 RID: 548 RVA: 0x00010EF4 File Offset: 0x0000F0F4
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

		// Token: 0x06000225 RID: 549 RVA: 0x00010FA0 File Offset: 0x0000F1A0
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

		// Token: 0x06000226 RID: 550 RVA: 0x00011064 File Offset: 0x0000F264
		private void SetNodeArrayLength(int size)
		{
			DTMXPathLinkedNode2[] destinationArray = new DTMXPathLinkedNode2[size];
			Array.Copy(this.nodes, destinationArray, Math.Min(size, this.nodes.Length));
			this.nodes = destinationArray;
		}

		// Token: 0x06000227 RID: 551 RVA: 0x0001109C File Offset: 0x0000F29C
		private void SetAttributeArrayLength(int size)
		{
			DTMXPathAttributeNode2[] destinationArray = new DTMXPathAttributeNode2[size];
			Array.Copy(this.attributes, destinationArray, Math.Min(size, this.attributes.Length));
			this.attributes = destinationArray;
		}

		// Token: 0x06000228 RID: 552 RVA: 0x000110D4 File Offset: 0x0000F2D4
		private void SetNsArrayLength(int size)
		{
			DTMXPathNamespaceNode2[] destinationArray = new DTMXPathNamespaceNode2[size];
			Array.Copy(this.namespaces, destinationArray, Math.Min(size, this.namespaces.Length));
			this.namespaces = destinationArray;
		}

		// Token: 0x06000229 RID: 553 RVA: 0x0001110C File Offset: 0x0000F30C
		public void AddNode(int parent, int firstAttribute, int previousSibling, XPathNodeType nodeType, int baseUri, bool isEmptyElement, int localName, int ns, int prefix, int value, int xmlLang, int namespaceNode, int lineNumber, int linePosition)
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

		// Token: 0x0600022A RID: 554 RVA: 0x000112C4 File Offset: 0x0000F4C4
		public void AddAttribute(int ownerElement, int localName, int ns, int prefix, int value, int lineNumber, int linePosition)
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

		// Token: 0x0600022B RID: 555 RVA: 0x000113A8 File Offset: 0x0000F5A8
		public void AddNsNode(int declaredElement, int name, int ns, int nextNs)
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

		// Token: 0x040001BB RID: 443
		private XmlReader xmlReader;

		// Token: 0x040001BC RID: 444
		private XmlValidatingReader validatingReader;

		// Token: 0x040001BD RID: 445
		private XmlSpace xmlSpace;

		// Token: 0x040001BE RID: 446
		private XmlNameTable nameTable;

		// Token: 0x040001BF RID: 447
		private IXmlLineInfo lineInfo;

		// Token: 0x040001C0 RID: 448
		private int nodeCapacity;

		// Token: 0x040001C1 RID: 449
		private int attributeCapacity;

		// Token: 0x040001C2 RID: 450
		private int nsCapacity;

		// Token: 0x040001C3 RID: 451
		private DTMXPathLinkedNode2[] nodes;

		// Token: 0x040001C4 RID: 452
		private DTMXPathAttributeNode2[] attributes;

		// Token: 0x040001C5 RID: 453
		private DTMXPathNamespaceNode2[] namespaces;

		// Token: 0x040001C6 RID: 454
		private string[] atomicStringPool;

		// Token: 0x040001C7 RID: 455
		private int atomicIndex;

		// Token: 0x040001C8 RID: 456
		private string[] nonAtomicStringPool;

		// Token: 0x040001C9 RID: 457
		private int nonAtomicIndex;

		// Token: 0x040001CA RID: 458
		private Hashtable idTable;

		// Token: 0x040001CB RID: 459
		private int nodeIndex;

		// Token: 0x040001CC RID: 460
		private int attributeIndex;

		// Token: 0x040001CD RID: 461
		private int nsIndex;

		// Token: 0x040001CE RID: 462
		private bool hasAttributes;

		// Token: 0x040001CF RID: 463
		private bool hasLocalNs;

		// Token: 0x040001D0 RID: 464
		private int attrIndexAtStart;

		// Token: 0x040001D1 RID: 465
		private int nsIndexAtStart;

		// Token: 0x040001D2 RID: 466
		private int lastNsInScope;

		// Token: 0x040001D3 RID: 467
		private bool skipRead;

		// Token: 0x040001D4 RID: 468
		private int[] parentStack;

		// Token: 0x040001D5 RID: 469
		private int parentStackIndex;
	}
}
