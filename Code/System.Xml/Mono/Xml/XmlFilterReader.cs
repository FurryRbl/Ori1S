using System;
using System.Xml;
using System.Xml.Schema;

namespace Mono.Xml
{
	// Token: 0x020000B2 RID: 178
	internal class XmlFilterReader : XmlReader, IXmlLineInfo
	{
		// Token: 0x06000606 RID: 1542 RVA: 0x000247B4 File Offset: 0x000229B4
		public XmlFilterReader(XmlReader reader, XmlReaderSettings settings)
		{
			this.reader = reader;
			this.settings = settings.Clone();
			this.lineInfo = (reader as IXmlLineInfo);
		}

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x06000607 RID: 1543 RVA: 0x000247DC File Offset: 0x000229DC
		public override bool CanReadBinaryContent
		{
			get
			{
				return this.reader.CanReadBinaryContent;
			}
		}

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x06000608 RID: 1544 RVA: 0x000247EC File Offset: 0x000229EC
		public override bool CanReadValueChunk
		{
			get
			{
				return this.reader.CanReadValueChunk;
			}
		}

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x06000609 RID: 1545 RVA: 0x000247FC File Offset: 0x000229FC
		public XmlReader Reader
		{
			get
			{
				return this.reader;
			}
		}

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x0600060A RID: 1546 RVA: 0x00024804 File Offset: 0x00022A04
		public int LineNumber
		{
			get
			{
				return (this.lineInfo == null) ? 0 : this.lineInfo.LineNumber;
			}
		}

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x0600060B RID: 1547 RVA: 0x00024824 File Offset: 0x00022A24
		public int LinePosition
		{
			get
			{
				return (this.lineInfo == null) ? 0 : this.lineInfo.LinePosition;
			}
		}

		// Token: 0x17000174 RID: 372
		// (get) Token: 0x0600060C RID: 1548 RVA: 0x00024844 File Offset: 0x00022A44
		public override XmlNodeType NodeType
		{
			get
			{
				return this.reader.NodeType;
			}
		}

		// Token: 0x17000175 RID: 373
		// (get) Token: 0x0600060D RID: 1549 RVA: 0x00024854 File Offset: 0x00022A54
		public override string Name
		{
			get
			{
				return this.reader.Name;
			}
		}

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x0600060E RID: 1550 RVA: 0x00024864 File Offset: 0x00022A64
		public override string LocalName
		{
			get
			{
				return this.reader.LocalName;
			}
		}

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x0600060F RID: 1551 RVA: 0x00024874 File Offset: 0x00022A74
		public override string NamespaceURI
		{
			get
			{
				return this.reader.NamespaceURI;
			}
		}

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x06000610 RID: 1552 RVA: 0x00024884 File Offset: 0x00022A84
		public override string Prefix
		{
			get
			{
				return this.reader.Prefix;
			}
		}

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x06000611 RID: 1553 RVA: 0x00024894 File Offset: 0x00022A94
		public override bool HasValue
		{
			get
			{
				return this.reader.HasValue;
			}
		}

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x06000612 RID: 1554 RVA: 0x000248A4 File Offset: 0x00022AA4
		public override int Depth
		{
			get
			{
				return this.reader.Depth;
			}
		}

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x06000613 RID: 1555 RVA: 0x000248B4 File Offset: 0x00022AB4
		public override string Value
		{
			get
			{
				return this.reader.Value;
			}
		}

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x06000614 RID: 1556 RVA: 0x000248C4 File Offset: 0x00022AC4
		public override string BaseURI
		{
			get
			{
				return this.reader.BaseURI;
			}
		}

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x06000615 RID: 1557 RVA: 0x000248D4 File Offset: 0x00022AD4
		public override bool IsEmptyElement
		{
			get
			{
				return this.reader.IsEmptyElement;
			}
		}

		// Token: 0x1700017E RID: 382
		// (get) Token: 0x06000616 RID: 1558 RVA: 0x000248E4 File Offset: 0x00022AE4
		public override bool IsDefault
		{
			get
			{
				return this.reader.IsDefault;
			}
		}

		// Token: 0x1700017F RID: 383
		// (get) Token: 0x06000617 RID: 1559 RVA: 0x000248F4 File Offset: 0x00022AF4
		public override char QuoteChar
		{
			get
			{
				return this.reader.QuoteChar;
			}
		}

		// Token: 0x17000180 RID: 384
		// (get) Token: 0x06000618 RID: 1560 RVA: 0x00024904 File Offset: 0x00022B04
		public override string XmlLang
		{
			get
			{
				return this.reader.XmlLang;
			}
		}

		// Token: 0x17000181 RID: 385
		// (get) Token: 0x06000619 RID: 1561 RVA: 0x00024914 File Offset: 0x00022B14
		public override XmlSpace XmlSpace
		{
			get
			{
				return this.reader.XmlSpace;
			}
		}

		// Token: 0x17000182 RID: 386
		// (get) Token: 0x0600061A RID: 1562 RVA: 0x00024924 File Offset: 0x00022B24
		public override int AttributeCount
		{
			get
			{
				return this.reader.AttributeCount;
			}
		}

		// Token: 0x17000183 RID: 387
		public override string this[int i]
		{
			get
			{
				return this.reader[i];
			}
		}

		// Token: 0x17000184 RID: 388
		public override string this[string name]
		{
			get
			{
				return this.reader[name];
			}
		}

		// Token: 0x17000185 RID: 389
		public override string this[string localName, string namespaceURI]
		{
			get
			{
				return this.reader[localName, namespaceURI];
			}
		}

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x0600061E RID: 1566 RVA: 0x00024964 File Offset: 0x00022B64
		public override bool EOF
		{
			get
			{
				return this.reader.EOF;
			}
		}

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x0600061F RID: 1567 RVA: 0x00024974 File Offset: 0x00022B74
		public override ReadState ReadState
		{
			get
			{
				return this.reader.ReadState;
			}
		}

		// Token: 0x17000188 RID: 392
		// (get) Token: 0x06000620 RID: 1568 RVA: 0x00024984 File Offset: 0x00022B84
		public override XmlNameTable NameTable
		{
			get
			{
				return this.reader.NameTable;
			}
		}

		// Token: 0x17000189 RID: 393
		// (get) Token: 0x06000621 RID: 1569 RVA: 0x00024994 File Offset: 0x00022B94
		public override IXmlSchemaInfo SchemaInfo
		{
			get
			{
				return this.reader.SchemaInfo;
			}
		}

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x06000622 RID: 1570 RVA: 0x000249A4 File Offset: 0x00022BA4
		public override XmlReaderSettings Settings
		{
			get
			{
				return this.settings;
			}
		}

		// Token: 0x06000623 RID: 1571 RVA: 0x000249AC File Offset: 0x00022BAC
		public override string GetAttribute(string name)
		{
			return this.reader.GetAttribute(name);
		}

		// Token: 0x06000624 RID: 1572 RVA: 0x000249BC File Offset: 0x00022BBC
		public override string GetAttribute(string localName, string namespaceURI)
		{
			return this.reader.GetAttribute(localName, namespaceURI);
		}

		// Token: 0x06000625 RID: 1573 RVA: 0x000249CC File Offset: 0x00022BCC
		public override string GetAttribute(int i)
		{
			return this.reader.GetAttribute(i);
		}

		// Token: 0x06000626 RID: 1574 RVA: 0x000249DC File Offset: 0x00022BDC
		public bool HasLineInfo()
		{
			return this.lineInfo != null && this.lineInfo.HasLineInfo();
		}

		// Token: 0x06000627 RID: 1575 RVA: 0x000249FC File Offset: 0x00022BFC
		public override bool MoveToAttribute(string name)
		{
			return this.reader.MoveToAttribute(name);
		}

		// Token: 0x06000628 RID: 1576 RVA: 0x00024A0C File Offset: 0x00022C0C
		public override bool MoveToAttribute(string localName, string namespaceURI)
		{
			return this.reader.MoveToAttribute(localName, namespaceURI);
		}

		// Token: 0x06000629 RID: 1577 RVA: 0x00024A1C File Offset: 0x00022C1C
		public override void MoveToAttribute(int i)
		{
			this.reader.MoveToAttribute(i);
		}

		// Token: 0x0600062A RID: 1578 RVA: 0x00024A2C File Offset: 0x00022C2C
		public override bool MoveToFirstAttribute()
		{
			return this.reader.MoveToFirstAttribute();
		}

		// Token: 0x0600062B RID: 1579 RVA: 0x00024A3C File Offset: 0x00022C3C
		public override bool MoveToNextAttribute()
		{
			return this.reader.MoveToNextAttribute();
		}

		// Token: 0x0600062C RID: 1580 RVA: 0x00024A4C File Offset: 0x00022C4C
		public override bool MoveToElement()
		{
			return this.reader.MoveToElement();
		}

		// Token: 0x0600062D RID: 1581 RVA: 0x00024A5C File Offset: 0x00022C5C
		public override void Close()
		{
			if (this.settings.CloseInput)
			{
				this.reader.Close();
			}
		}

		// Token: 0x0600062E RID: 1582 RVA: 0x00024A7C File Offset: 0x00022C7C
		public override bool Read()
		{
			if (!this.reader.Read())
			{
				return false;
			}
			if (this.reader.NodeType == XmlNodeType.DocumentType && this.settings.ProhibitDtd)
			{
				throw new XmlException("Document Type Definition (DTD) is prohibited in this XML reader.");
			}
			if (this.reader.NodeType == XmlNodeType.Whitespace && this.settings.IgnoreWhitespace)
			{
				return this.Read();
			}
			if (this.reader.NodeType == XmlNodeType.ProcessingInstruction && this.settings.IgnoreProcessingInstructions)
			{
				return this.Read();
			}
			return this.reader.NodeType != XmlNodeType.Comment || !this.settings.IgnoreComments || this.Read();
		}

		// Token: 0x0600062F RID: 1583 RVA: 0x00024B44 File Offset: 0x00022D44
		public override string ReadString()
		{
			return this.reader.ReadString();
		}

		// Token: 0x06000630 RID: 1584 RVA: 0x00024B54 File Offset: 0x00022D54
		public override string LookupNamespace(string prefix)
		{
			return this.reader.LookupNamespace(prefix);
		}

		// Token: 0x06000631 RID: 1585 RVA: 0x00024B64 File Offset: 0x00022D64
		public override void ResolveEntity()
		{
			this.reader.ResolveEntity();
		}

		// Token: 0x06000632 RID: 1586 RVA: 0x00024B74 File Offset: 0x00022D74
		public override bool ReadAttributeValue()
		{
			return this.reader.ReadAttributeValue();
		}

		// Token: 0x040003BD RID: 957
		private XmlReader reader;

		// Token: 0x040003BE RID: 958
		private XmlReaderSettings settings;

		// Token: 0x040003BF RID: 959
		private IXmlLineInfo lineInfo;
	}
}
