using System;
using System.Xml;
using System.Xml.Schema;

namespace Mono.Xml.Schema
{
	// Token: 0x020001DF RID: 479
	internal class XsdQName : XsdName
	{
		// Token: 0x060012ED RID: 4845 RVA: 0x000529A4 File Offset: 0x00050BA4
		internal XsdQName()
		{
		}

		// Token: 0x1700059C RID: 1436
		// (get) Token: 0x060012EE RID: 4846 RVA: 0x000529AC File Offset: 0x00050BAC
		public override XmlTokenizedType TokenizedType
		{
			get
			{
				return XmlTokenizedType.QName;
			}
		}

		// Token: 0x1700059D RID: 1437
		// (get) Token: 0x060012EF RID: 4847 RVA: 0x000529B0 File Offset: 0x00050BB0
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.QName;
			}
		}

		// Token: 0x1700059E RID: 1438
		// (get) Token: 0x060012F0 RID: 4848 RVA: 0x000529B4 File Offset: 0x00050BB4
		public override Type ValueType
		{
			get
			{
				return typeof(XmlQualifiedName);
			}
		}

		// Token: 0x060012F1 RID: 4849 RVA: 0x000529C0 File Offset: 0x00050BC0
		public override object ParseValue(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr)
		{
			if (nameTable == null)
			{
				throw new ArgumentNullException("name table");
			}
			if (nsmgr == null)
			{
				throw new ArgumentNullException("namespace manager");
			}
			XmlQualifiedName xmlQualifiedName = XmlQualifiedName.Parse(s, nsmgr, true);
			nameTable.Add(xmlQualifiedName.Name);
			nameTable.Add(xmlQualifiedName.Namespace);
			return xmlQualifiedName;
		}

		// Token: 0x060012F2 RID: 4850 RVA: 0x00052A14 File Offset: 0x00050C14
		internal override ValueType ParseValueType(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr)
		{
			return new QNameValueType(this.ParseValue(s, nameTable, nsmgr) as XmlQualifiedName);
		}
	}
}
