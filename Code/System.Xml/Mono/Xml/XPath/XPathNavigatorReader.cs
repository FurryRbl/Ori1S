using System;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.Xml.XPath;

namespace Mono.Xml.XPath
{
	// Token: 0x0200004B RID: 75
	internal class XPathNavigatorReader : XmlReader
	{
		// Token: 0x06000314 RID: 788 RVA: 0x00014B78 File Offset: 0x00012D78
		public XPathNavigatorReader(XPathNavigator nav)
		{
			this.current = nav.Clone();
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x06000315 RID: 789 RVA: 0x00014BB0 File Offset: 0x00012DB0
		public override bool CanReadBinaryContent
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x06000316 RID: 790 RVA: 0x00014BB4 File Offset: 0x00012DB4
		public override bool CanReadValueChunk
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x06000317 RID: 791 RVA: 0x00014BB8 File Offset: 0x00012DB8
		public override XmlNodeType NodeType
		{
			get
			{
				if (this.ReadState != ReadState.Interactive)
				{
					return XmlNodeType.None;
				}
				if (this.endElement)
				{
					return XmlNodeType.EndElement;
				}
				if (this.attributeValueConsumed)
				{
					return XmlNodeType.Text;
				}
				switch (this.current.NodeType)
				{
				case XPathNodeType.Root:
					return XmlNodeType.None;
				case XPathNodeType.Element:
					return XmlNodeType.Element;
				case XPathNodeType.Attribute:
				case XPathNodeType.Namespace:
					return XmlNodeType.Attribute;
				case XPathNodeType.Text:
					return XmlNodeType.Text;
				case XPathNodeType.SignificantWhitespace:
					return XmlNodeType.SignificantWhitespace;
				case XPathNodeType.Whitespace:
					return XmlNodeType.Whitespace;
				case XPathNodeType.ProcessingInstruction:
					return XmlNodeType.ProcessingInstruction;
				case XPathNodeType.Comment:
					return XmlNodeType.Comment;
				default:
					throw new InvalidOperationException(string.Format("Current XPathNavigator status is {0} which is not acceptable to XmlReader.", this.current.NodeType));
				}
			}
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x06000318 RID: 792 RVA: 0x00014C5C File Offset: 0x00012E5C
		public override string Name
		{
			get
			{
				if (this.ReadState != ReadState.Interactive)
				{
					return string.Empty;
				}
				if (this.attributeValueConsumed)
				{
					return string.Empty;
				}
				if (this.current.NodeType == XPathNodeType.Namespace)
				{
					return (!(this.current.Name == string.Empty)) ? ("xmlns:" + this.current.Name) : "xmlns";
				}
				return this.current.Name;
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x06000319 RID: 793 RVA: 0x00014CE4 File Offset: 0x00012EE4
		public override string LocalName
		{
			get
			{
				if (this.ReadState != ReadState.Interactive)
				{
					return string.Empty;
				}
				if (this.attributeValueConsumed)
				{
					return string.Empty;
				}
				if (this.current.NodeType == XPathNodeType.Namespace && this.current.LocalName == string.Empty)
				{
					return "xmlns";
				}
				return this.current.LocalName;
			}
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x0600031A RID: 794 RVA: 0x00014D50 File Offset: 0x00012F50
		public override string NamespaceURI
		{
			get
			{
				if (this.ReadState != ReadState.Interactive)
				{
					return string.Empty;
				}
				if (this.attributeValueConsumed)
				{
					return string.Empty;
				}
				if (this.current.NodeType == XPathNodeType.Namespace)
				{
					return "http://www.w3.org/2000/xmlns/";
				}
				return this.current.NamespaceURI;
			}
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x0600031B RID: 795 RVA: 0x00014DA4 File Offset: 0x00012FA4
		public override string Prefix
		{
			get
			{
				if (this.ReadState != ReadState.Interactive)
				{
					return string.Empty;
				}
				if (this.attributeValueConsumed)
				{
					return string.Empty;
				}
				if (this.current.NodeType == XPathNodeType.Namespace && this.current.LocalName != string.Empty)
				{
					return "xmlns";
				}
				return this.current.Prefix;
			}
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x0600031C RID: 796 RVA: 0x00014E10 File Offset: 0x00013010
		public override bool HasValue
		{
			get
			{
				switch (this.current.NodeType)
				{
				case XPathNodeType.Attribute:
				case XPathNodeType.Namespace:
				case XPathNodeType.Text:
				case XPathNodeType.SignificantWhitespace:
				case XPathNodeType.Whitespace:
				case XPathNodeType.ProcessingInstruction:
				case XPathNodeType.Comment:
					return true;
				default:
					return false;
				}
			}
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x0600031D RID: 797 RVA: 0x00014E58 File Offset: 0x00013058
		public override int Depth
		{
			get
			{
				if (this.ReadState != ReadState.Interactive)
				{
					return 0;
				}
				if (this.NodeType == XmlNodeType.Attribute)
				{
					return this.depth + 1;
				}
				if (this.attributeValueConsumed)
				{
					return this.depth + 2;
				}
				return this.depth;
			}
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x0600031E RID: 798 RVA: 0x00014EA4 File Offset: 0x000130A4
		public override string Value
		{
			get
			{
				if (this.ReadState != ReadState.Interactive)
				{
					return string.Empty;
				}
				switch (this.current.NodeType)
				{
				case XPathNodeType.Root:
				case XPathNodeType.Element:
					return string.Empty;
				case XPathNodeType.Attribute:
				case XPathNodeType.Namespace:
				case XPathNodeType.Text:
				case XPathNodeType.SignificantWhitespace:
				case XPathNodeType.Whitespace:
				case XPathNodeType.ProcessingInstruction:
				case XPathNodeType.Comment:
					return this.current.Value;
				default:
					throw new InvalidOperationException("Current XPathNavigator status is {0} which is not acceptable to XmlReader.");
				}
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x0600031F RID: 799 RVA: 0x00014F1C File Offset: 0x0001311C
		public override string BaseURI
		{
			get
			{
				return this.current.BaseURI;
			}
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x06000320 RID: 800 RVA: 0x00014F2C File Offset: 0x0001312C
		public override bool IsEmptyElement
		{
			get
			{
				return this.ReadState == ReadState.Interactive && this.current.IsEmptyElement;
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x06000321 RID: 801 RVA: 0x00014F48 File Offset: 0x00013148
		public override bool IsDefault
		{
			get
			{
				IXmlSchemaInfo xmlSchemaInfo = this.current as IXmlSchemaInfo;
				return xmlSchemaInfo != null && xmlSchemaInfo.IsDefault;
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x06000322 RID: 802 RVA: 0x00014F70 File Offset: 0x00013170
		public override char QuoteChar
		{
			get
			{
				return '"';
			}
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x06000323 RID: 803 RVA: 0x00014F74 File Offset: 0x00013174
		public override IXmlSchemaInfo SchemaInfo
		{
			get
			{
				return this.current.SchemaInfo;
			}
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x06000324 RID: 804 RVA: 0x00014F84 File Offset: 0x00013184
		public override string XmlLang
		{
			get
			{
				return this.current.XmlLang;
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x06000325 RID: 805 RVA: 0x00014F94 File Offset: 0x00013194
		public override XmlSpace XmlSpace
		{
			get
			{
				return XmlSpace.None;
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x06000326 RID: 806 RVA: 0x00014F98 File Offset: 0x00013198
		public override int AttributeCount
		{
			get
			{
				return this.attributeCount;
			}
		}

		// Token: 0x06000327 RID: 807 RVA: 0x00014FA0 File Offset: 0x000131A0
		private int GetAttributeCount()
		{
			if (this.ReadState != ReadState.Interactive)
			{
				return 0;
			}
			int num = 0;
			if (this.current.MoveToFirstAttribute())
			{
				do
				{
					num++;
				}
				while (this.current.MoveToNextAttribute());
				this.current.MoveToParent();
			}
			if (this.current.MoveToFirstNamespace(XPathNamespaceScope.Local))
			{
				do
				{
					num++;
				}
				while (this.current.MoveToNextNamespace(XPathNamespaceScope.Local));
				this.current.MoveToParent();
			}
			return num;
		}

		// Token: 0x06000328 RID: 808 RVA: 0x00015020 File Offset: 0x00013220
		private void SplitName(string name, out string localName, out string ns)
		{
			if (name == "xmlns")
			{
				localName = "xmlns";
				ns = "http://www.w3.org/2000/xmlns/";
				return;
			}
			localName = name;
			ns = string.Empty;
			int num = name.IndexOf(':');
			if (num > 0)
			{
				localName = name.Substring(num + 1, name.Length - num - 1);
				ns = this.LookupNamespace(name.Substring(0, num));
				if (name.Substring(0, num) == "xmlns")
				{
					ns = "http://www.w3.org/2000/xmlns/";
				}
			}
		}

		// Token: 0x170000BA RID: 186
		public override string this[string name]
		{
			get
			{
				string localName;
				string namespaceURI;
				this.SplitName(name, out localName, out namespaceURI);
				return this[localName, namespaceURI];
			}
		}

		// Token: 0x170000BB RID: 187
		public override string this[string localName, string namespaceURI]
		{
			get
			{
				string attribute = this.current.GetAttribute(localName, namespaceURI);
				if (attribute != string.Empty)
				{
					return attribute;
				}
				XPathNavigator xpathNavigator = this.current.Clone();
				return (!xpathNavigator.MoveToAttribute(localName, namespaceURI)) ? null : string.Empty;
			}
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x0600032B RID: 811 RVA: 0x00015120 File Offset: 0x00013320
		public override bool EOF
		{
			get
			{
				return this.ReadState == ReadState.EndOfFile;
			}
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x0600032C RID: 812 RVA: 0x0001512C File Offset: 0x0001332C
		public override ReadState ReadState
		{
			get
			{
				if (this.eof)
				{
					return ReadState.EndOfFile;
				}
				if (this.closed)
				{
					return ReadState.Closed;
				}
				if (!this.started)
				{
					return ReadState.Initial;
				}
				return ReadState.Interactive;
			}
		}

		// Token: 0x170000BE RID: 190
		// (get) Token: 0x0600032D RID: 813 RVA: 0x00015164 File Offset: 0x00013364
		public override XmlNameTable NameTable
		{
			get
			{
				return this.current.NameTable;
			}
		}

		// Token: 0x0600032E RID: 814 RVA: 0x00015174 File Offset: 0x00013374
		public override string GetAttribute(string name)
		{
			string localName;
			string namespaceURI;
			this.SplitName(name, out localName, out namespaceURI);
			return this[localName, namespaceURI];
		}

		// Token: 0x0600032F RID: 815 RVA: 0x00015194 File Offset: 0x00013394
		public override string GetAttribute(string localName, string namespaceURI)
		{
			return this[localName, namespaceURI];
		}

		// Token: 0x06000330 RID: 816 RVA: 0x000151A0 File Offset: 0x000133A0
		public override string GetAttribute(int i)
		{
			return this[i];
		}

		// Token: 0x06000331 RID: 817 RVA: 0x000151AC File Offset: 0x000133AC
		private bool CheckAttributeMove(bool b)
		{
			if (b)
			{
				this.attributeValueConsumed = false;
			}
			return b;
		}

		// Token: 0x06000332 RID: 818 RVA: 0x000151BC File Offset: 0x000133BC
		public override bool MoveToAttribute(string name)
		{
			string localName;
			string namespaceURI;
			this.SplitName(name, out localName, out namespaceURI);
			return this.MoveToAttribute(localName, namespaceURI);
		}

		// Token: 0x06000333 RID: 819 RVA: 0x000151DC File Offset: 0x000133DC
		public override bool MoveToAttribute(string localName, string namespaceURI)
		{
			bool flag = namespaceURI == "http://www.w3.org/2000/xmlns/";
			XPathNavigator xpathNavigator = null;
			switch (this.current.NodeType)
			{
			case XPathNodeType.Element:
				break;
			case XPathNodeType.Attribute:
			case XPathNodeType.Namespace:
				xpathNavigator = this.current.Clone();
				this.MoveToElement();
				break;
			default:
				goto IL_F1;
			}
			if (this.MoveToFirstAttribute())
			{
				for (;;)
				{
					bool flag2;
					if (flag)
					{
						if (localName == "xmlns")
						{
							flag2 = (this.current.Name == string.Empty);
						}
						else
						{
							flag2 = (localName == this.current.Name);
						}
					}
					else
					{
						flag2 = (this.current.LocalName == localName && this.current.NamespaceURI == namespaceURI);
					}
					if (flag2)
					{
						break;
					}
					if (!this.MoveToNextAttribute())
					{
						goto Block_6;
					}
				}
				this.attributeValueConsumed = false;
				return true;
				Block_6:
				this.MoveToElement();
			}
			IL_F1:
			if (xpathNavigator != null)
			{
				this.current = xpathNavigator;
			}
			return false;
		}

		// Token: 0x06000334 RID: 820 RVA: 0x000152E8 File Offset: 0x000134E8
		public override bool MoveToFirstAttribute()
		{
			switch (this.current.NodeType)
			{
			case XPathNodeType.Element:
				break;
			case XPathNodeType.Attribute:
			case XPathNodeType.Namespace:
				this.current.MoveToParent();
				break;
			default:
				return false;
			}
			return this.CheckAttributeMove(this.current.MoveToFirstNamespace(XPathNamespaceScope.Local)) || this.CheckAttributeMove(this.current.MoveToFirstAttribute());
		}

		// Token: 0x06000335 RID: 821 RVA: 0x00015358 File Offset: 0x00013558
		public override bool MoveToNextAttribute()
		{
			switch (this.current.NodeType)
			{
			case XPathNodeType.Element:
				return this.MoveToFirstAttribute();
			case XPathNodeType.Attribute:
				return this.CheckAttributeMove(this.current.MoveToNextAttribute());
			case XPathNodeType.Namespace:
			{
				if (this.CheckAttributeMove(this.current.MoveToNextNamespace(XPathNamespaceScope.Local)))
				{
					return true;
				}
				XPathNavigator other = this.current.Clone();
				this.current.MoveToParent();
				if (this.CheckAttributeMove(this.current.MoveToFirstAttribute()))
				{
					return true;
				}
				this.current.MoveTo(other);
				return false;
			}
			default:
				return false;
			}
		}

		// Token: 0x06000336 RID: 822 RVA: 0x000153FC File Offset: 0x000135FC
		public override bool MoveToElement()
		{
			if (this.current.NodeType == XPathNodeType.Attribute || this.current.NodeType == XPathNodeType.Namespace)
			{
				this.attributeValueConsumed = false;
				return this.current.MoveToParent();
			}
			return false;
		}

		// Token: 0x06000337 RID: 823 RVA: 0x00015440 File Offset: 0x00013640
		public override void Close()
		{
			this.closed = true;
			this.eof = true;
		}

		// Token: 0x06000338 RID: 824 RVA: 0x00015450 File Offset: 0x00013650
		public override bool Read()
		{
			if (this.eof)
			{
				return false;
			}
			if (base.Binary != null)
			{
				base.Binary.Reset();
			}
			switch (this.ReadState)
			{
			case ReadState.Initial:
				this.started = true;
				this.root = this.current.Clone();
				if (this.current.NodeType == XPathNodeType.Root && !this.current.MoveToFirstChild())
				{
					this.endElement = false;
					this.eof = true;
					return false;
				}
				this.attributeCount = this.GetAttributeCount();
				return true;
			case ReadState.Interactive:
				if ((this.IsEmptyElement || this.endElement) && this.root.IsSamePosition(this.current))
				{
					this.eof = true;
					return false;
				}
				break;
			case ReadState.Error:
			case ReadState.EndOfFile:
			case ReadState.Closed:
				return false;
			}
			this.MoveToElement();
			if (this.endElement || !this.current.MoveToFirstChild())
			{
				if (!this.endElement && !this.current.IsEmptyElement && this.current.NodeType == XPathNodeType.Element)
				{
					this.endElement = true;
				}
				else if (!this.current.MoveToNext())
				{
					this.current.MoveToParent();
					if (this.current.NodeType == XPathNodeType.Root)
					{
						this.endElement = false;
						this.eof = true;
						return false;
					}
					this.endElement = (this.current.NodeType == XPathNodeType.Element);
					if (this.endElement)
					{
						this.depth--;
					}
				}
				else
				{
					this.endElement = false;
				}
			}
			else
			{
				this.depth++;
			}
			if (!this.endElement && this.current.NodeType == XPathNodeType.Element)
			{
				this.attributeCount = this.GetAttributeCount();
			}
			else
			{
				this.attributeCount = 0;
			}
			return true;
		}

		// Token: 0x06000339 RID: 825 RVA: 0x00015650 File Offset: 0x00013850
		public override string ReadString()
		{
			this.readStringBuffer.Length = 0;
			XmlNodeType nodeType = this.NodeType;
			switch (nodeType)
			{
			case XmlNodeType.Element:
				if (this.IsEmptyElement)
				{
					return string.Empty;
				}
				for (;;)
				{
					this.Read();
					XmlNodeType nodeType2 = this.NodeType;
					if (nodeType2 != XmlNodeType.Text && nodeType2 != XmlNodeType.CDATA && nodeType2 != XmlNodeType.Whitespace && nodeType2 != XmlNodeType.SignificantWhitespace)
					{
						break;
					}
					this.readStringBuffer.Append(this.Value);
				}
				goto IL_105;
			default:
				if (nodeType != XmlNodeType.Whitespace && nodeType != XmlNodeType.SignificantWhitespace)
				{
					return string.Empty;
				}
				break;
			case XmlNodeType.Text:
			case XmlNodeType.CDATA:
				break;
			}
			for (;;)
			{
				XmlNodeType nodeType2 = this.NodeType;
				if (nodeType2 != XmlNodeType.Text && nodeType2 != XmlNodeType.CDATA && nodeType2 != XmlNodeType.Whitespace && nodeType2 != XmlNodeType.SignificantWhitespace)
				{
					break;
				}
				this.readStringBuffer.Append(this.Value);
				this.Read();
			}
			IL_105:
			string result = this.readStringBuffer.ToString();
			this.readStringBuffer.Length = 0;
			return result;
		}

		// Token: 0x0600033A RID: 826 RVA: 0x0001577C File Offset: 0x0001397C
		public override string LookupNamespace(string prefix)
		{
			XPathNavigator xpathNavigator = this.current.Clone();
			string result;
			try
			{
				this.MoveToElement();
				if (this.current.NodeType != XPathNodeType.Element)
				{
					this.current.MoveToParent();
				}
				if (this.current.MoveToFirstNamespace())
				{
					while (!(this.current.LocalName == prefix))
					{
						if (!this.current.MoveToNextNamespace())
						{
							goto IL_77;
						}
					}
					return this.current.Value;
				}
				IL_77:
				result = null;
			}
			finally
			{
				this.current = xpathNavigator;
			}
			return result;
		}

		// Token: 0x0600033B RID: 827 RVA: 0x00015834 File Offset: 0x00013A34
		public override void ResolveEntity()
		{
			throw new InvalidOperationException();
		}

		// Token: 0x0600033C RID: 828 RVA: 0x0001583C File Offset: 0x00013A3C
		public override bool ReadAttributeValue()
		{
			if (this.NodeType != XmlNodeType.Attribute)
			{
				return false;
			}
			if (this.attributeValueConsumed)
			{
				return false;
			}
			this.attributeValueConsumed = true;
			return true;
		}

		// Token: 0x04000227 RID: 551
		private XPathNavigator root;

		// Token: 0x04000228 RID: 552
		private XPathNavigator current;

		// Token: 0x04000229 RID: 553
		private bool started;

		// Token: 0x0400022A RID: 554
		private bool closed;

		// Token: 0x0400022B RID: 555
		private bool endElement;

		// Token: 0x0400022C RID: 556
		private bool attributeValueConsumed;

		// Token: 0x0400022D RID: 557
		private StringBuilder readStringBuffer = new StringBuilder();

		// Token: 0x0400022E RID: 558
		private StringBuilder innerXmlBuilder = new StringBuilder();

		// Token: 0x0400022F RID: 559
		private int depth;

		// Token: 0x04000230 RID: 560
		private int attributeCount;

		// Token: 0x04000231 RID: 561
		private bool eof;
	}
}
