using System;

namespace System.Xml.Schema
{
	// Token: 0x0200024F RID: 591
	internal class XmlSchemaReader : XmlReader, IXmlLineInfo
	{
		// Token: 0x060017E4 RID: 6116 RVA: 0x00079A88 File Offset: 0x00077C88
		public XmlSchemaReader(XmlReader reader, ValidationEventHandler handler)
		{
			this.reader = reader;
			this.handler = handler;
			if (reader is IXmlLineInfo)
			{
				IXmlLineInfo xmlLineInfo = (IXmlLineInfo)reader;
				this.hasLineInfo = xmlLineInfo.HasLineInfo();
			}
		}

		// Token: 0x1700071D RID: 1821
		// (get) Token: 0x060017E5 RID: 6117 RVA: 0x00079AC8 File Offset: 0x00077CC8
		public string FullName
		{
			get
			{
				return this.NamespaceURI + ":" + this.LocalName;
			}
		}

		// Token: 0x1700071E RID: 1822
		// (get) Token: 0x060017E6 RID: 6118 RVA: 0x00079AE0 File Offset: 0x00077CE0
		public XmlReader Reader
		{
			get
			{
				return this.reader;
			}
		}

		// Token: 0x060017E7 RID: 6119 RVA: 0x00079AE8 File Offset: 0x00077CE8
		public void RaiseInvalidElementError()
		{
			string text = "Element " + this.FullName + " is invalid in this context.\n";
			if (this.hasLineInfo)
			{
				string text2 = text;
				text = string.Concat(new object[]
				{
					text2,
					"The error occured on (",
					((IXmlLineInfo)this.reader).LineNumber,
					",",
					((IXmlLineInfo)this.reader).LinePosition,
					")"
				});
			}
			XmlSchemaObject.error(this.handler, text, null);
			this.SkipToEnd();
		}

		// Token: 0x060017E8 RID: 6120 RVA: 0x00079B84 File Offset: 0x00077D84
		public bool ReadNextElement()
		{
			this.MoveToElement();
			while (this.Read())
			{
				if (this.NodeType == XmlNodeType.Element || this.NodeType == XmlNodeType.EndElement)
				{
					if (!(this.reader.NamespaceURI != "http://www.w3.org/2001/XMLSchema"))
					{
						return true;
					}
					this.RaiseInvalidElementError();
				}
			}
			return false;
		}

		// Token: 0x060017E9 RID: 6121 RVA: 0x00079BEC File Offset: 0x00077DEC
		public void SkipToEnd()
		{
			this.MoveToElement();
			if (this.IsEmptyElement || this.NodeType != XmlNodeType.Element)
			{
				return;
			}
			if (this.NodeType == XmlNodeType.Element)
			{
				int depth = this.Depth;
				while (this.Read())
				{
					if (this.Depth == depth)
					{
						break;
					}
				}
			}
		}

		// Token: 0x060017EA RID: 6122 RVA: 0x00079C4C File Offset: 0x00077E4C
		public bool HasLineInfo()
		{
			return this.hasLineInfo;
		}

		// Token: 0x1700071F RID: 1823
		// (get) Token: 0x060017EB RID: 6123 RVA: 0x00079C54 File Offset: 0x00077E54
		public int LineNumber
		{
			get
			{
				return (!this.hasLineInfo) ? 0 : ((IXmlLineInfo)this.reader).LineNumber;
			}
		}

		// Token: 0x17000720 RID: 1824
		// (get) Token: 0x060017EC RID: 6124 RVA: 0x00079C78 File Offset: 0x00077E78
		public int LinePosition
		{
			get
			{
				return (!this.hasLineInfo) ? 0 : ((IXmlLineInfo)this.reader).LinePosition;
			}
		}

		// Token: 0x17000721 RID: 1825
		// (get) Token: 0x060017ED RID: 6125 RVA: 0x00079C9C File Offset: 0x00077E9C
		public override int AttributeCount
		{
			get
			{
				return this.reader.AttributeCount;
			}
		}

		// Token: 0x17000722 RID: 1826
		// (get) Token: 0x060017EE RID: 6126 RVA: 0x00079CAC File Offset: 0x00077EAC
		public override string BaseURI
		{
			get
			{
				return this.reader.BaseURI;
			}
		}

		// Token: 0x17000723 RID: 1827
		// (get) Token: 0x060017EF RID: 6127 RVA: 0x00079CBC File Offset: 0x00077EBC
		public override bool CanResolveEntity
		{
			get
			{
				return this.reader.CanResolveEntity;
			}
		}

		// Token: 0x17000724 RID: 1828
		// (get) Token: 0x060017F0 RID: 6128 RVA: 0x00079CCC File Offset: 0x00077ECC
		public override int Depth
		{
			get
			{
				return this.reader.Depth;
			}
		}

		// Token: 0x17000725 RID: 1829
		// (get) Token: 0x060017F1 RID: 6129 RVA: 0x00079CDC File Offset: 0x00077EDC
		public override bool EOF
		{
			get
			{
				return this.reader.EOF;
			}
		}

		// Token: 0x17000726 RID: 1830
		// (get) Token: 0x060017F2 RID: 6130 RVA: 0x00079CEC File Offset: 0x00077EEC
		public override bool HasAttributes
		{
			get
			{
				return this.reader.HasAttributes;
			}
		}

		// Token: 0x17000727 RID: 1831
		// (get) Token: 0x060017F3 RID: 6131 RVA: 0x00079CFC File Offset: 0x00077EFC
		public override bool HasValue
		{
			get
			{
				return this.reader.HasValue;
			}
		}

		// Token: 0x17000728 RID: 1832
		// (get) Token: 0x060017F4 RID: 6132 RVA: 0x00079D0C File Offset: 0x00077F0C
		public override bool IsDefault
		{
			get
			{
				return this.reader.IsDefault;
			}
		}

		// Token: 0x17000729 RID: 1833
		// (get) Token: 0x060017F5 RID: 6133 RVA: 0x00079D1C File Offset: 0x00077F1C
		public override bool IsEmptyElement
		{
			get
			{
				return this.reader.IsEmptyElement;
			}
		}

		// Token: 0x1700072A RID: 1834
		public override string this[int i]
		{
			get
			{
				return this.reader[i];
			}
		}

		// Token: 0x1700072B RID: 1835
		public override string this[string name]
		{
			get
			{
				return this.reader[name];
			}
		}

		// Token: 0x1700072C RID: 1836
		public override string this[string name, string namespaceURI]
		{
			get
			{
				return this.reader[name, namespaceURI];
			}
		}

		// Token: 0x1700072D RID: 1837
		// (get) Token: 0x060017F9 RID: 6137 RVA: 0x00079D5C File Offset: 0x00077F5C
		public override string LocalName
		{
			get
			{
				return this.reader.LocalName;
			}
		}

		// Token: 0x1700072E RID: 1838
		// (get) Token: 0x060017FA RID: 6138 RVA: 0x00079D6C File Offset: 0x00077F6C
		public override string Name
		{
			get
			{
				return this.reader.Name;
			}
		}

		// Token: 0x1700072F RID: 1839
		// (get) Token: 0x060017FB RID: 6139 RVA: 0x00079D7C File Offset: 0x00077F7C
		public override string NamespaceURI
		{
			get
			{
				return this.reader.NamespaceURI;
			}
		}

		// Token: 0x17000730 RID: 1840
		// (get) Token: 0x060017FC RID: 6140 RVA: 0x00079D8C File Offset: 0x00077F8C
		public override XmlNameTable NameTable
		{
			get
			{
				return this.reader.NameTable;
			}
		}

		// Token: 0x17000731 RID: 1841
		// (get) Token: 0x060017FD RID: 6141 RVA: 0x00079D9C File Offset: 0x00077F9C
		public override XmlNodeType NodeType
		{
			get
			{
				return this.reader.NodeType;
			}
		}

		// Token: 0x17000732 RID: 1842
		// (get) Token: 0x060017FE RID: 6142 RVA: 0x00079DAC File Offset: 0x00077FAC
		public override string Prefix
		{
			get
			{
				return this.reader.Prefix;
			}
		}

		// Token: 0x17000733 RID: 1843
		// (get) Token: 0x060017FF RID: 6143 RVA: 0x00079DBC File Offset: 0x00077FBC
		public override char QuoteChar
		{
			get
			{
				return this.reader.QuoteChar;
			}
		}

		// Token: 0x17000734 RID: 1844
		// (get) Token: 0x06001800 RID: 6144 RVA: 0x00079DCC File Offset: 0x00077FCC
		public override ReadState ReadState
		{
			get
			{
				return this.reader.ReadState;
			}
		}

		// Token: 0x17000735 RID: 1845
		// (get) Token: 0x06001801 RID: 6145 RVA: 0x00079DDC File Offset: 0x00077FDC
		public override string Value
		{
			get
			{
				return this.reader.Value;
			}
		}

		// Token: 0x17000736 RID: 1846
		// (get) Token: 0x06001802 RID: 6146 RVA: 0x00079DEC File Offset: 0x00077FEC
		public override string XmlLang
		{
			get
			{
				return this.reader.XmlLang;
			}
		}

		// Token: 0x17000737 RID: 1847
		// (get) Token: 0x06001803 RID: 6147 RVA: 0x00079DFC File Offset: 0x00077FFC
		public override XmlSpace XmlSpace
		{
			get
			{
				return this.reader.XmlSpace;
			}
		}

		// Token: 0x06001804 RID: 6148 RVA: 0x00079E0C File Offset: 0x0007800C
		public override void Close()
		{
			this.reader.Close();
		}

		// Token: 0x06001805 RID: 6149 RVA: 0x00079E1C File Offset: 0x0007801C
		public override bool Equals(object obj)
		{
			return this.reader.Equals(obj);
		}

		// Token: 0x06001806 RID: 6150 RVA: 0x00079E2C File Offset: 0x0007802C
		public override string GetAttribute(int i)
		{
			return this.reader.GetAttribute(i);
		}

		// Token: 0x06001807 RID: 6151 RVA: 0x00079E3C File Offset: 0x0007803C
		public override string GetAttribute(string name)
		{
			return this.reader.GetAttribute(name);
		}

		// Token: 0x06001808 RID: 6152 RVA: 0x00079E4C File Offset: 0x0007804C
		public override string GetAttribute(string name, string namespaceURI)
		{
			return this.reader.GetAttribute(name, namespaceURI);
		}

		// Token: 0x06001809 RID: 6153 RVA: 0x00079E5C File Offset: 0x0007805C
		public override int GetHashCode()
		{
			return this.reader.GetHashCode();
		}

		// Token: 0x0600180A RID: 6154 RVA: 0x00079E6C File Offset: 0x0007806C
		public override bool IsStartElement()
		{
			return this.reader.IsStartElement();
		}

		// Token: 0x0600180B RID: 6155 RVA: 0x00079E7C File Offset: 0x0007807C
		public override bool IsStartElement(string localname, string ns)
		{
			return this.reader.IsStartElement(localname, ns);
		}

		// Token: 0x0600180C RID: 6156 RVA: 0x00079E8C File Offset: 0x0007808C
		public override bool IsStartElement(string name)
		{
			return this.reader.IsStartElement(name);
		}

		// Token: 0x0600180D RID: 6157 RVA: 0x00079E9C File Offset: 0x0007809C
		public override string LookupNamespace(string prefix)
		{
			return this.reader.LookupNamespace(prefix);
		}

		// Token: 0x0600180E RID: 6158 RVA: 0x00079EAC File Offset: 0x000780AC
		public override void MoveToAttribute(int i)
		{
			this.reader.MoveToAttribute(i);
		}

		// Token: 0x0600180F RID: 6159 RVA: 0x00079EBC File Offset: 0x000780BC
		public override bool MoveToAttribute(string name)
		{
			return this.reader.MoveToAttribute(name);
		}

		// Token: 0x06001810 RID: 6160 RVA: 0x00079ECC File Offset: 0x000780CC
		public override bool MoveToAttribute(string name, string ns)
		{
			return this.reader.MoveToAttribute(name, ns);
		}

		// Token: 0x06001811 RID: 6161 RVA: 0x00079EDC File Offset: 0x000780DC
		public override XmlNodeType MoveToContent()
		{
			return this.reader.MoveToContent();
		}

		// Token: 0x06001812 RID: 6162 RVA: 0x00079EEC File Offset: 0x000780EC
		public override bool MoveToElement()
		{
			return this.reader.MoveToElement();
		}

		// Token: 0x06001813 RID: 6163 RVA: 0x00079EFC File Offset: 0x000780FC
		public override bool MoveToFirstAttribute()
		{
			return this.reader.MoveToFirstAttribute();
		}

		// Token: 0x06001814 RID: 6164 RVA: 0x00079F0C File Offset: 0x0007810C
		public override bool MoveToNextAttribute()
		{
			return this.reader.MoveToNextAttribute();
		}

		// Token: 0x06001815 RID: 6165 RVA: 0x00079F1C File Offset: 0x0007811C
		public override bool Read()
		{
			return this.reader.Read();
		}

		// Token: 0x06001816 RID: 6166 RVA: 0x00079F2C File Offset: 0x0007812C
		public override bool ReadAttributeValue()
		{
			return this.reader.ReadAttributeValue();
		}

		// Token: 0x06001817 RID: 6167 RVA: 0x00079F3C File Offset: 0x0007813C
		public override string ReadElementString()
		{
			return this.reader.ReadElementString();
		}

		// Token: 0x06001818 RID: 6168 RVA: 0x00079F4C File Offset: 0x0007814C
		public override string ReadElementString(string localname, string ns)
		{
			return this.reader.ReadElementString(localname, ns);
		}

		// Token: 0x06001819 RID: 6169 RVA: 0x00079F5C File Offset: 0x0007815C
		public override string ReadElementString(string name)
		{
			return this.reader.ReadElementString(name);
		}

		// Token: 0x0600181A RID: 6170 RVA: 0x00079F6C File Offset: 0x0007816C
		public override void ReadEndElement()
		{
			this.reader.ReadEndElement();
		}

		// Token: 0x0600181B RID: 6171 RVA: 0x00079F7C File Offset: 0x0007817C
		public override string ReadInnerXml()
		{
			return this.reader.ReadInnerXml();
		}

		// Token: 0x0600181C RID: 6172 RVA: 0x00079F8C File Offset: 0x0007818C
		public override string ReadOuterXml()
		{
			return this.reader.ReadOuterXml();
		}

		// Token: 0x0600181D RID: 6173 RVA: 0x00079F9C File Offset: 0x0007819C
		public override void ReadStartElement()
		{
			this.reader.ReadStartElement();
		}

		// Token: 0x0600181E RID: 6174 RVA: 0x00079FAC File Offset: 0x000781AC
		public override void ReadStartElement(string localname, string ns)
		{
			this.reader.ReadStartElement(localname, ns);
		}

		// Token: 0x0600181F RID: 6175 RVA: 0x00079FBC File Offset: 0x000781BC
		public override void ReadStartElement(string name)
		{
			this.reader.ReadStartElement(name);
		}

		// Token: 0x06001820 RID: 6176 RVA: 0x00079FCC File Offset: 0x000781CC
		public override string ReadString()
		{
			return this.reader.ReadString();
		}

		// Token: 0x06001821 RID: 6177 RVA: 0x00079FDC File Offset: 0x000781DC
		public override void ResolveEntity()
		{
			this.reader.ResolveEntity();
		}

		// Token: 0x06001822 RID: 6178 RVA: 0x00079FEC File Offset: 0x000781EC
		public override void Skip()
		{
			this.reader.Skip();
		}

		// Token: 0x06001823 RID: 6179 RVA: 0x00079FFC File Offset: 0x000781FC
		public override string ToString()
		{
			return this.reader.ToString();
		}

		// Token: 0x040009BC RID: 2492
		private XmlReader reader;

		// Token: 0x040009BD RID: 2493
		private ValidationEventHandler handler;

		// Token: 0x040009BE RID: 2494
		private bool hasLineInfo;
	}
}
