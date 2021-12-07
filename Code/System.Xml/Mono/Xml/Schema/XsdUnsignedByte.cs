using System;
using System.Xml;
using System.Xml.Schema;

namespace Mono.Xml.Schema
{
	// Token: 0x020001D7 RID: 471
	internal class XsdUnsignedByte : XsdUnsignedShort
	{
		// Token: 0x17000580 RID: 1408
		// (get) Token: 0x060012B2 RID: 4786 RVA: 0x00052540 File Offset: 0x00050740
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.UnsignedByte;
			}
		}

		// Token: 0x17000581 RID: 1409
		// (get) Token: 0x060012B3 RID: 4787 RVA: 0x00052544 File Offset: 0x00050744
		public override Type ValueType
		{
			get
			{
				return typeof(byte);
			}
		}

		// Token: 0x060012B4 RID: 4788 RVA: 0x00052550 File Offset: 0x00050750
		public override object ParseValue(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr)
		{
			return this.ParseValueType(s, nameTable, nsmgr);
		}

		// Token: 0x060012B5 RID: 4789 RVA: 0x0005255C File Offset: 0x0005075C
		internal override ValueType ParseValueType(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr)
		{
			return XmlConvert.ToByte(base.Normalize(s));
		}

		// Token: 0x060012B6 RID: 4790 RVA: 0x00052570 File Offset: 0x00050770
		internal override XsdOrdering Compare(object x, object y)
		{
			if (!(x is byte) || !(y is byte))
			{
				return XsdOrdering.Indeterminate;
			}
			if ((byte)x == (byte)y)
			{
				return XsdOrdering.Equal;
			}
			if ((byte)x < (byte)y)
			{
				return XsdOrdering.LessThan;
			}
			return XsdOrdering.GreaterThan;
		}
	}
}
