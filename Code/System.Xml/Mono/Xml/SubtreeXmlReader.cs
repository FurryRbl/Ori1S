using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Schema;

namespace Mono.Xml
{
	// Token: 0x020000B1 RID: 177
	internal class SubtreeXmlReader : XmlReader, IXmlLineInfo, IXmlNamespaceResolver
	{
		// Token: 0x060005E0 RID: 1504 RVA: 0x0002420C File Offset: 0x0002240C
		public SubtreeXmlReader(XmlReader reader)
		{
			this.Reader = reader;
			this.li = (reader as IXmlLineInfo);
			this.nsResolver = (reader as IXmlNamespaceResolver);
			this.initial = true;
			this.startDepth = reader.Depth;
			if (reader.ReadState == ReadState.Initial)
			{
				this.startDepth = -1;
			}
		}

		// Token: 0x060005E1 RID: 1505 RVA: 0x00024264 File Offset: 0x00022464
		IDictionary<string, string> IXmlNamespaceResolver.GetNamespacesInScope(XmlNamespaceScope scope)
		{
			IDictionary<string, string> result;
			if (this.nsResolver != null)
			{
				IDictionary<string, string> namespacesInScope = this.nsResolver.GetNamespacesInScope(scope);
				result = namespacesInScope;
			}
			else
			{
				result = new Dictionary<string, string>();
			}
			return result;
		}

		// Token: 0x060005E2 RID: 1506 RVA: 0x00024294 File Offset: 0x00022494
		string IXmlNamespaceResolver.LookupPrefix(string ns)
		{
			return (this.nsResolver == null) ? string.Empty : this.nsResolver.LookupPrefix(ns);
		}

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x060005E3 RID: 1507 RVA: 0x000242B8 File Offset: 0x000224B8
		public override int AttributeCount
		{
			get
			{
				return (!this.initial) ? this.Reader.AttributeCount : 0;
			}
		}

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x060005E4 RID: 1508 RVA: 0x000242D8 File Offset: 0x000224D8
		public override bool CanReadBinaryContent
		{
			get
			{
				return this.Reader.CanReadBinaryContent;
			}
		}

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x060005E5 RID: 1509 RVA: 0x000242E8 File Offset: 0x000224E8
		public override bool CanReadValueChunk
		{
			get
			{
				return this.Reader.CanReadValueChunk;
			}
		}

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x060005E6 RID: 1510 RVA: 0x000242F8 File Offset: 0x000224F8
		public override int Depth
		{
			get
			{
				return this.Reader.Depth - this.startDepth;
			}
		}

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x060005E7 RID: 1511 RVA: 0x0002430C File Offset: 0x0002250C
		public override string BaseURI
		{
			get
			{
				return this.Reader.BaseURI;
			}
		}

		// Token: 0x17000160 RID: 352
		// (get) Token: 0x060005E8 RID: 1512 RVA: 0x0002431C File Offset: 0x0002251C
		public override bool EOF
		{
			get
			{
				return this.eof || this.Reader.EOF;
			}
		}

		// Token: 0x17000161 RID: 353
		// (get) Token: 0x060005E9 RID: 1513 RVA: 0x00024338 File Offset: 0x00022538
		public override bool IsEmptyElement
		{
			get
			{
				return this.Reader.IsEmptyElement;
			}
		}

		// Token: 0x17000162 RID: 354
		// (get) Token: 0x060005EA RID: 1514 RVA: 0x00024348 File Offset: 0x00022548
		public int LineNumber
		{
			get
			{
				return (!this.initial) ? ((this.li == null) ? 0 : this.li.LineNumber) : 0;
			}
		}

		// Token: 0x17000163 RID: 355
		// (get) Token: 0x060005EB RID: 1515 RVA: 0x00024378 File Offset: 0x00022578
		public int LinePosition
		{
			get
			{
				return (!this.initial) ? ((this.li == null) ? 0 : this.li.LinePosition) : 0;
			}
		}

		// Token: 0x17000164 RID: 356
		// (get) Token: 0x060005EC RID: 1516 RVA: 0x000243A8 File Offset: 0x000225A8
		public override bool HasValue
		{
			get
			{
				return !this.initial && !this.eof && this.Reader.HasValue;
			}
		}

		// Token: 0x17000165 RID: 357
		// (get) Token: 0x060005ED RID: 1517 RVA: 0x000243D4 File Offset: 0x000225D4
		public override string LocalName
		{
			get
			{
				return (!this.initial && !this.eof) ? this.Reader.LocalName : string.Empty;
			}
		}

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x060005EE RID: 1518 RVA: 0x00024404 File Offset: 0x00022604
		public override string Name
		{
			get
			{
				return (!this.initial && !this.eof) ? this.Reader.Name : string.Empty;
			}
		}

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x060005EF RID: 1519 RVA: 0x00024434 File Offset: 0x00022634
		public override XmlNameTable NameTable
		{
			get
			{
				return this.Reader.NameTable;
			}
		}

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x060005F0 RID: 1520 RVA: 0x00024444 File Offset: 0x00022644
		public override string NamespaceURI
		{
			get
			{
				return (!this.initial && !this.eof) ? this.Reader.NamespaceURI : string.Empty;
			}
		}

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x060005F1 RID: 1521 RVA: 0x00024474 File Offset: 0x00022674
		public override XmlNodeType NodeType
		{
			get
			{
				return (!this.initial && !this.eof) ? this.Reader.NodeType : XmlNodeType.None;
			}
		}

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x060005F2 RID: 1522 RVA: 0x000244A0 File Offset: 0x000226A0
		public override string Prefix
		{
			get
			{
				return (!this.initial && !this.eof) ? this.Reader.Prefix : string.Empty;
			}
		}

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x060005F3 RID: 1523 RVA: 0x000244D0 File Offset: 0x000226D0
		public override ReadState ReadState
		{
			get
			{
				return (!this.initial) ? ((!this.eof) ? this.Reader.ReadState : ReadState.EndOfFile) : ReadState.Initial;
			}
		}

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x060005F4 RID: 1524 RVA: 0x00024500 File Offset: 0x00022700
		public override IXmlSchemaInfo SchemaInfo
		{
			get
			{
				return this.Reader.SchemaInfo;
			}
		}

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x060005F5 RID: 1525 RVA: 0x00024510 File Offset: 0x00022710
		public override XmlReaderSettings Settings
		{
			get
			{
				return this.Reader.Settings;
			}
		}

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x060005F6 RID: 1526 RVA: 0x00024520 File Offset: 0x00022720
		public override string Value
		{
			get
			{
				return (!this.initial) ? this.Reader.Value : string.Empty;
			}
		}

		// Token: 0x060005F7 RID: 1527 RVA: 0x00024550 File Offset: 0x00022750
		public override void Close()
		{
			while (this.Read())
			{
			}
		}

		// Token: 0x060005F8 RID: 1528 RVA: 0x00024564 File Offset: 0x00022764
		public override string GetAttribute(int i)
		{
			return (!this.initial) ? this.Reader.GetAttribute(i) : null;
		}

		// Token: 0x060005F9 RID: 1529 RVA: 0x00024584 File Offset: 0x00022784
		public override string GetAttribute(string name)
		{
			return (!this.initial) ? this.Reader.GetAttribute(name) : null;
		}

		// Token: 0x060005FA RID: 1530 RVA: 0x000245A4 File Offset: 0x000227A4
		public override string GetAttribute(string local, string ns)
		{
			return (!this.initial) ? this.Reader.GetAttribute(local, ns) : null;
		}

		// Token: 0x060005FB RID: 1531 RVA: 0x000245C4 File Offset: 0x000227C4
		public bool HasLineInfo()
		{
			return this.li != null && this.li.HasLineInfo();
		}

		// Token: 0x060005FC RID: 1532 RVA: 0x000245E4 File Offset: 0x000227E4
		public override string LookupNamespace(string prefix)
		{
			return this.Reader.LookupNamespace(prefix);
		}

		// Token: 0x060005FD RID: 1533 RVA: 0x000245F4 File Offset: 0x000227F4
		public override bool MoveToFirstAttribute()
		{
			return !this.initial && this.Reader.MoveToFirstAttribute();
		}

		// Token: 0x060005FE RID: 1534 RVA: 0x00024614 File Offset: 0x00022814
		public override bool MoveToNextAttribute()
		{
			return !this.initial && this.Reader.MoveToNextAttribute();
		}

		// Token: 0x060005FF RID: 1535 RVA: 0x00024634 File Offset: 0x00022834
		public override void MoveToAttribute(int i)
		{
			if (!this.initial)
			{
				this.Reader.MoveToAttribute(i);
			}
		}

		// Token: 0x06000600 RID: 1536 RVA: 0x00024650 File Offset: 0x00022850
		public override bool MoveToAttribute(string name)
		{
			return !this.initial && this.Reader.MoveToAttribute(name);
		}

		// Token: 0x06000601 RID: 1537 RVA: 0x00024670 File Offset: 0x00022870
		public override bool MoveToAttribute(string local, string ns)
		{
			return !this.initial && this.Reader.MoveToAttribute(local, ns);
		}

		// Token: 0x06000602 RID: 1538 RVA: 0x00024690 File Offset: 0x00022890
		public override bool MoveToElement()
		{
			return !this.initial && this.Reader.MoveToElement();
		}

		// Token: 0x06000603 RID: 1539 RVA: 0x000246B0 File Offset: 0x000228B0
		public override bool Read()
		{
			if (this.initial)
			{
				this.initial = false;
				return true;
			}
			if (!this.read)
			{
				this.read = true;
				this.Reader.MoveToElement();
				bool flag = !this.Reader.IsEmptyElement && this.Reader.Read();
				if (!flag)
				{
					this.eof = true;
				}
				return flag;
			}
			this.Reader.MoveToElement();
			if (this.Reader.Depth > this.startDepth && this.Reader.Read())
			{
				return true;
			}
			this.eof = true;
			return false;
		}

		// Token: 0x06000604 RID: 1540 RVA: 0x0002475C File Offset: 0x0002295C
		public override bool ReadAttributeValue()
		{
			return !this.initial && !this.eof && this.Reader.ReadAttributeValue();
		}

		// Token: 0x06000605 RID: 1541 RVA: 0x00024784 File Offset: 0x00022984
		public override void ResolveEntity()
		{
			if (this.initial || this.eof)
			{
				return;
			}
			this.Reader.ResolveEntity();
		}

		// Token: 0x040003B6 RID: 950
		private int startDepth;

		// Token: 0x040003B7 RID: 951
		private bool eof;

		// Token: 0x040003B8 RID: 952
		private bool initial;

		// Token: 0x040003B9 RID: 953
		private bool read;

		// Token: 0x040003BA RID: 954
		private XmlReader Reader;

		// Token: 0x040003BB RID: 955
		private IXmlLineInfo li;

		// Token: 0x040003BC RID: 956
		private IXmlNamespaceResolver nsResolver;
	}
}
