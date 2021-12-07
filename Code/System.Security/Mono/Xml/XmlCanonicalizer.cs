using System;
using System.Collections;
using System.IO;
using System.Text;
using System.Xml;

namespace Mono.Xml
{
	// Token: 0x02000008 RID: 8
	internal class XmlCanonicalizer
	{
		// Token: 0x06000012 RID: 18 RVA: 0x00002A54 File Offset: 0x00000C54
		public XmlCanonicalizer(bool withComments, bool excC14N, Hashtable propagatedNamespaces)
		{
			this.res = new StringBuilder();
			this.comments = withComments;
			this.exclusive = excC14N;
			this.propagatedNss = propagatedNamespaces;
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002A88 File Offset: 0x00000C88
		private void Initialize()
		{
			this.state = XmlCanonicalizer.XmlCanonicalizerState.BeforeDocElement;
			this.visibleNamespaces = new ArrayList();
			this.prevVisibleNamespacesStart = 0;
			this.prevVisibleNamespacesEnd = 0;
			this.res.Length = 0;
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002AC4 File Offset: 0x00000CC4
		public Stream Canonicalize(XmlDocument doc)
		{
			if (doc == null)
			{
				throw new ArgumentNullException("doc");
			}
			this.Initialize();
			this.FillMissingPrefixes(doc, new XmlNamespaceManager(doc.NameTable), new ArrayList());
			this.WriteDocumentNode(doc);
			UTF8Encoding utf8Encoding = new UTF8Encoding();
			byte[] bytes = utf8Encoding.GetBytes(this.res.ToString());
			return new MemoryStream(bytes);
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002B24 File Offset: 0x00000D24
		public Stream Canonicalize(XmlNodeList nodes)
		{
			this.xnl = nodes;
			if (nodes == null || nodes.Count < 1)
			{
				return new MemoryStream();
			}
			XmlNode xmlNode = nodes[0];
			return this.Canonicalize((xmlNode.NodeType != XmlNodeType.Document) ? xmlNode.OwnerDocument : (xmlNode as XmlDocument));
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000016 RID: 22 RVA: 0x00002B7C File Offset: 0x00000D7C
		// (set) Token: 0x06000017 RID: 23 RVA: 0x00002B84 File Offset: 0x00000D84
		public string InclusiveNamespacesPrefixList
		{
			get
			{
				return this.inclusiveNamespacesPrefixList;
			}
			set
			{
				this.inclusiveNamespacesPrefixList = value;
			}
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002B90 File Offset: 0x00000D90
		private XmlAttribute CreateXmlns(XmlNode n)
		{
			XmlAttribute xmlAttribute = (n.Prefix.Length != 0) ? n.OwnerDocument.CreateAttribute("xmlns", n.Prefix, "http://www.w3.org/2000/xmlns/") : n.OwnerDocument.CreateAttribute("xmlns", "http://www.w3.org/2000/xmlns/");
			xmlAttribute.Value = n.NamespaceURI;
			return xmlAttribute;
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002BF0 File Offset: 0x00000DF0
		private void FillMissingPrefixes(XmlNode n, XmlNamespaceManager nsmgr, ArrayList tmpList)
		{
			if (n.Prefix.Length == 0 && this.propagatedNss != null)
			{
				foreach (object obj in this.propagatedNss)
				{
					DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
					if ((string)dictionaryEntry.Value == n.NamespaceURI)
					{
						n.Prefix = (string)dictionaryEntry.Key;
						break;
					}
				}
			}
			if (n.NodeType == XmlNodeType.Element && ((XmlElement)n).HasAttributes)
			{
				foreach (object obj2 in n.Attributes)
				{
					XmlAttribute xmlAttribute = (XmlAttribute)obj2;
					if (xmlAttribute.NamespaceURI == "http://www.w3.org/2000/xmlns/")
					{
						nsmgr.AddNamespace((xmlAttribute.Prefix.Length != 0) ? xmlAttribute.LocalName : string.Empty, xmlAttribute.Value);
					}
				}
				nsmgr.PushScope();
			}
			if (n.NamespaceURI.Length > 0 && nsmgr.LookupPrefix(n.NamespaceURI) == null)
			{
				tmpList.Add(this.CreateXmlns(n));
			}
			if (n.NodeType == XmlNodeType.Element && ((XmlElement)n).HasAttributes)
			{
				foreach (object obj3 in n.Attributes)
				{
					XmlAttribute xmlAttribute2 = (XmlAttribute)obj3;
					if (xmlAttribute2.NamespaceURI.Length > 0 && nsmgr.LookupNamespace(xmlAttribute2.Prefix) == null)
					{
						tmpList.Add(this.CreateXmlns(xmlAttribute2));
					}
				}
			}
			foreach (object obj4 in tmpList)
			{
				XmlAttribute attributeNode = (XmlAttribute)obj4;
				((XmlElement)n).SetAttributeNode(attributeNode);
			}
			tmpList.Clear();
			if (n.HasChildNodes)
			{
				for (XmlNode xmlNode = n.FirstChild; xmlNode != null; xmlNode = xmlNode.NextSibling)
				{
					if (xmlNode.NodeType == XmlNodeType.Element)
					{
						this.FillMissingPrefixes(xmlNode, nsmgr, tmpList);
					}
				}
			}
			nsmgr.PopScope();
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002EF8 File Offset: 0x000010F8
		private void WriteNode(XmlNode node)
		{
			bool visible = this.IsNodeVisible(node);
			switch (node.NodeType)
			{
			case XmlNodeType.Element:
				this.WriteElementNode(node, visible);
				break;
			case XmlNodeType.Attribute:
				throw new XmlException("Attribute node is impossible here", null);
			case XmlNodeType.Text:
			case XmlNodeType.CDATA:
			case XmlNodeType.SignificantWhitespace:
				this.WriteTextNode(node, visible);
				break;
			case XmlNodeType.EntityReference:
				for (int i = 0; i < node.ChildNodes.Count; i++)
				{
					this.WriteNode(node.ChildNodes[i]);
				}
				break;
			case XmlNodeType.ProcessingInstruction:
				this.WriteProcessingInstructionNode(node, visible);
				break;
			case XmlNodeType.Comment:
				this.WriteCommentNode(node, visible);
				break;
			case XmlNodeType.Document:
			case XmlNodeType.DocumentFragment:
				this.WriteDocumentNode(node);
				break;
			case XmlNodeType.Whitespace:
				if (this.state == XmlCanonicalizer.XmlCanonicalizerState.InsideDocElement)
				{
					this.WriteTextNode(node, visible);
				}
				break;
			case XmlNodeType.EndElement:
				throw new XmlException("EndElement node is impossible here", null);
			case XmlNodeType.EndEntity:
				throw new XmlException("EndEntity node is impossible here", null);
			}
		}

		// Token: 0x0600001B RID: 27 RVA: 0x0000301C File Offset: 0x0000121C
		private void WriteDocumentNode(XmlNode node)
		{
			this.state = XmlCanonicalizer.XmlCanonicalizerState.BeforeDocElement;
			for (XmlNode xmlNode = node.FirstChild; xmlNode != null; xmlNode = xmlNode.NextSibling)
			{
				this.WriteNode(xmlNode);
			}
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00003050 File Offset: 0x00001250
		private void WriteElementNode(XmlNode node, bool visible)
		{
			int num = this.prevVisibleNamespacesStart;
			int num2 = this.prevVisibleNamespacesEnd;
			int count = this.visibleNamespaces.Count;
			XmlCanonicalizer.XmlCanonicalizerState xmlCanonicalizerState = this.state;
			if (visible && this.state == XmlCanonicalizer.XmlCanonicalizerState.BeforeDocElement)
			{
				this.state = XmlCanonicalizer.XmlCanonicalizerState.InsideDocElement;
			}
			if (visible)
			{
				this.res.Append("<");
				this.res.Append(node.Name);
			}
			this.WriteNamespacesAxis(node, visible);
			this.WriteAttributesAxis(node);
			if (visible)
			{
				this.res.Append(">");
			}
			for (XmlNode xmlNode = node.FirstChild; xmlNode != null; xmlNode = xmlNode.NextSibling)
			{
				this.WriteNode(xmlNode);
			}
			if (visible)
			{
				this.res.Append("</");
				this.res.Append(node.Name);
				this.res.Append(">");
			}
			if (visible && xmlCanonicalizerState == XmlCanonicalizer.XmlCanonicalizerState.BeforeDocElement)
			{
				this.state = XmlCanonicalizer.XmlCanonicalizerState.AfterDocElement;
			}
			this.prevVisibleNamespacesStart = num;
			this.prevVisibleNamespacesEnd = num2;
			if (this.visibleNamespaces.Count > count)
			{
				this.visibleNamespaces.RemoveRange(count, this.visibleNamespaces.Count - count);
			}
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00003190 File Offset: 0x00001390
		private void WriteNamespacesAxis(XmlNode node, bool visible)
		{
			XmlDocument ownerDocument = node.OwnerDocument;
			bool flag = false;
			ArrayList arrayList = new ArrayList();
			XmlNode xmlNode = node;
			while (xmlNode != null && xmlNode != ownerDocument)
			{
				foreach (object obj in xmlNode.Attributes)
				{
					XmlAttribute xmlAttribute = (XmlAttribute)obj;
					if (this.IsNamespaceNode(xmlAttribute))
					{
						string text = string.Empty;
						if (xmlAttribute.Prefix == "xmlns")
						{
							text = xmlAttribute.LocalName;
						}
						if (!(text == "xml") || !(xmlAttribute.Value == "http://www.w3.org/XML/1998/namespace"))
						{
							string namespaceOfPrefix = node.GetNamespaceOfPrefix(text);
							if (!(namespaceOfPrefix != xmlAttribute.Value))
							{
								if (this.IsNodeVisible(xmlAttribute))
								{
									bool flag2 = this.IsNamespaceRendered(text, xmlAttribute.Value);
									if (!this.exclusive || this.IsVisiblyUtilized(node as XmlElement, xmlAttribute))
									{
										if (visible)
										{
											this.visibleNamespaces.Add(xmlAttribute);
										}
										if (!flag2)
										{
											arrayList.Add(xmlAttribute);
										}
										if (text == string.Empty)
										{
											flag = true;
										}
									}
								}
							}
						}
					}
				}
				xmlNode = xmlNode.ParentNode;
			}
			if (visible && !flag && !this.IsNamespaceRendered(string.Empty, string.Empty) && node.NamespaceURI == string.Empty)
			{
				this.res.Append(" xmlns=\"\"");
			}
			arrayList.Sort(new XmlDsigC14NTransformNamespacesComparer());
			foreach (object obj2 in arrayList)
			{
				XmlNode xmlNode2 = obj2 as XmlNode;
				if (xmlNode2 != null)
				{
					this.res.Append(" ");
					this.res.Append(xmlNode2.Name);
					this.res.Append("=\"");
					this.res.Append(xmlNode2.Value);
					this.res.Append("\"");
				}
			}
			if (visible)
			{
				this.prevVisibleNamespacesStart = this.prevVisibleNamespacesEnd;
				this.prevVisibleNamespacesEnd = this.visibleNamespaces.Count;
			}
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00003460 File Offset: 0x00001660
		private void WriteAttributesAxis(XmlNode node)
		{
			ArrayList arrayList = new ArrayList();
			foreach (object obj in node.Attributes)
			{
				XmlNode xmlNode = (XmlNode)obj;
				if (!this.IsNamespaceNode(xmlNode) && this.IsNodeVisible(xmlNode))
				{
					arrayList.Add(xmlNode);
				}
			}
			if (!this.exclusive && node.ParentNode != null && node.ParentNode.ParentNode != null && !this.IsNodeVisible(node.ParentNode.ParentNode))
			{
				for (XmlNode parentNode = node.ParentNode; parentNode != null; parentNode = parentNode.ParentNode)
				{
					if (parentNode.Attributes != null)
					{
						foreach (object obj2 in parentNode.Attributes)
						{
							XmlNode xmlNode2 = (XmlNode)obj2;
							if (!(xmlNode2.Prefix != "xml"))
							{
								if (node.Attributes.GetNamedItem(xmlNode2.LocalName, xmlNode2.NamespaceURI) == null)
								{
									bool flag = false;
									foreach (object obj3 in arrayList)
									{
										XmlNode xmlNode3 = obj3 as XmlNode;
										if (xmlNode3.Prefix == "xml" && xmlNode3.LocalName == xmlNode2.LocalName)
										{
											flag = true;
											break;
										}
									}
									if (!flag)
									{
										arrayList.Add(xmlNode2);
									}
								}
							}
						}
					}
				}
			}
			arrayList.Sort(new XmlDsigC14NTransformAttributesComparer());
			foreach (object obj4 in arrayList)
			{
				XmlNode xmlNode4 = obj4 as XmlNode;
				if (xmlNode4 != null)
				{
					this.res.Append(" ");
					this.res.Append(xmlNode4.Name);
					this.res.Append("=\"");
					this.res.Append(this.NormalizeString(xmlNode4.Value, XmlNodeType.Attribute));
					this.res.Append("\"");
				}
			}
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00003770 File Offset: 0x00001970
		private void WriteTextNode(XmlNode node, bool visible)
		{
			if (visible)
			{
				this.res.Append(this.NormalizeString(node.Value, node.NodeType));
			}
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000037A4 File Offset: 0x000019A4
		private void WriteCommentNode(XmlNode node, bool visible)
		{
			if (visible && this.comments)
			{
				if (this.state == XmlCanonicalizer.XmlCanonicalizerState.AfterDocElement)
				{
					this.res.Append("\n<!--");
				}
				else
				{
					this.res.Append("<!--");
				}
				this.res.Append(this.NormalizeString(node.Value, XmlNodeType.Comment));
				if (this.state == XmlCanonicalizer.XmlCanonicalizerState.BeforeDocElement)
				{
					this.res.Append("-->\n");
				}
				else
				{
					this.res.Append("-->");
				}
			}
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00003840 File Offset: 0x00001A40
		private void WriteProcessingInstructionNode(XmlNode node, bool visible)
		{
			if (visible)
			{
				if (this.state == XmlCanonicalizer.XmlCanonicalizerState.AfterDocElement)
				{
					this.res.Append("\n<?");
				}
				else
				{
					this.res.Append("<?");
				}
				this.res.Append(node.Name);
				if (node.Value.Length > 0)
				{
					this.res.Append(" ");
					this.res.Append(this.NormalizeString(node.Value, XmlNodeType.ProcessingInstruction));
				}
				if (this.state == XmlCanonicalizer.XmlCanonicalizerState.BeforeDocElement)
				{
					this.res.Append("?>\n");
				}
				else
				{
					this.res.Append("?>");
				}
			}
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00003908 File Offset: 0x00001B08
		private bool IsNodeVisible(XmlNode node)
		{
			if (this.xnl == null)
			{
				return true;
			}
			foreach (object obj in this.xnl)
			{
				XmlNode obj2 = (XmlNode)obj;
				if (node.Equals(obj2))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00003994 File Offset: 0x00001B94
		private bool IsVisiblyUtilized(XmlElement owner, XmlAttribute ns)
		{
			if (owner == null)
			{
				return false;
			}
			string b = (!(ns.LocalName == "xmlns")) ? ns.LocalName : string.Empty;
			if (owner.Prefix == b && owner.NamespaceURI == ns.Value)
			{
				return true;
			}
			if (!owner.HasAttributes)
			{
				return false;
			}
			foreach (object obj in owner.Attributes)
			{
				XmlAttribute xmlAttribute = (XmlAttribute)obj;
				if (!(xmlAttribute.Prefix == string.Empty))
				{
					if (!(xmlAttribute.Prefix != b) && !(xmlAttribute.NamespaceURI != ns.Value))
					{
						if (this.IsNodeVisible(xmlAttribute))
						{
							return true;
						}
					}
				}
			}
			return false;
		}

		// Token: 0x06000024 RID: 36 RVA: 0x00003AC0 File Offset: 0x00001CC0
		private bool IsNamespaceRendered(string prefix, string uri)
		{
			bool flag = prefix == string.Empty && uri == string.Empty;
			int num = (!flag) ? this.prevVisibleNamespacesStart : 0;
			for (int i = this.visibleNamespaces.Count - 1; i >= num; i--)
			{
				XmlNode xmlNode = this.visibleNamespaces[i] as XmlNode;
				if (xmlNode != null)
				{
					string a = string.Empty;
					if (xmlNode.Prefix == "xmlns")
					{
						a = xmlNode.LocalName;
					}
					if (a == prefix)
					{
						return xmlNode.Value == uri;
					}
				}
			}
			return flag;
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00003B74 File Offset: 0x00001D74
		private bool IsNamespaceNode(XmlNode node)
		{
			return node != null && node.NodeType == XmlNodeType.Attribute && node.NamespaceURI == "http://www.w3.org/2000/xmlns/";
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00003BA8 File Offset: 0x00001DA8
		private bool IsTextNode(XmlNodeType type)
		{
			return type == XmlNodeType.Text || type == XmlNodeType.CDATA || type == XmlNodeType.Whitespace || type == XmlNodeType.SignificantWhitespace;
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00003BE0 File Offset: 0x00001DE0
		private string NormalizeString(string input, XmlNodeType type)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (char c in input)
			{
				if (c == '<' && (type == XmlNodeType.Attribute || this.IsTextNode(type)))
				{
					stringBuilder.Append("&lt;");
				}
				else if (c == '>' && this.IsTextNode(type))
				{
					stringBuilder.Append("&gt;");
				}
				else if (c == '&' && (type == XmlNodeType.Attribute || this.IsTextNode(type)))
				{
					stringBuilder.Append("&amp;");
				}
				else if (c == '"' && type == XmlNodeType.Attribute)
				{
					stringBuilder.Append("&quot;");
				}
				else if (c == '\t' && type == XmlNodeType.Attribute)
				{
					stringBuilder.Append("&#x9;");
				}
				else if (c == '\n' && type == XmlNodeType.Attribute)
				{
					stringBuilder.Append("&#xA;");
				}
				else if (c == '\r')
				{
					stringBuilder.Append("&#xD;");
				}
				else
				{
					stringBuilder.Append(c);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x04000028 RID: 40
		private bool comments;

		// Token: 0x04000029 RID: 41
		private bool exclusive;

		// Token: 0x0400002A RID: 42
		private string inclusiveNamespacesPrefixList;

		// Token: 0x0400002B RID: 43
		private XmlNodeList xnl;

		// Token: 0x0400002C RID: 44
		private StringBuilder res;

		// Token: 0x0400002D RID: 45
		private XmlCanonicalizer.XmlCanonicalizerState state;

		// Token: 0x0400002E RID: 46
		private ArrayList visibleNamespaces;

		// Token: 0x0400002F RID: 47
		private int prevVisibleNamespacesStart;

		// Token: 0x04000030 RID: 48
		private int prevVisibleNamespacesEnd;

		// Token: 0x04000031 RID: 49
		private Hashtable propagatedNss;

		// Token: 0x02000009 RID: 9
		private enum XmlCanonicalizerState
		{
			// Token: 0x04000033 RID: 51
			BeforeDocElement,
			// Token: 0x04000034 RID: 52
			InsideDocElement,
			// Token: 0x04000035 RID: 53
			AfterDocElement
		}
	}
}
