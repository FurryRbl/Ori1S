using System;
using System.Xml;
using System.Xml.Schema;

namespace Mono.Xml.Schema
{
	// Token: 0x020001C9 RID: 457
	internal class XsdIDRefs : XsdName
	{
		// Token: 0x06001255 RID: 4693 RVA: 0x00051F0C File Offset: 0x0005010C
		internal XsdIDRefs()
		{
		}

		// Token: 0x17000555 RID: 1365
		// (get) Token: 0x06001256 RID: 4694 RVA: 0x00051F14 File Offset: 0x00050114
		public override XmlTokenizedType TokenizedType
		{
			get
			{
				return XmlTokenizedType.IDREFS;
			}
		}

		// Token: 0x17000556 RID: 1366
		// (get) Token: 0x06001257 RID: 4695 RVA: 0x00051F18 File Offset: 0x00050118
		[MonoTODO]
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.Item;
			}
		}

		// Token: 0x17000557 RID: 1367
		// (get) Token: 0x06001258 RID: 4696 RVA: 0x00051F1C File Offset: 0x0005011C
		public override Type ValueType
		{
			get
			{
				return typeof(string[]);
			}
		}

		// Token: 0x06001259 RID: 4697 RVA: 0x00051F28 File Offset: 0x00050128
		public override object ParseValue(string value, XmlNameTable nt, IXmlNamespaceResolver nsmgr)
		{
			return this.GetValidatedArray(value, nt);
		}

		// Token: 0x0600125A RID: 4698 RVA: 0x00051F34 File Offset: 0x00050134
		internal override ValueType ParseValueType(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr)
		{
			return new StringArrayValueType(this.GetValidatedArray(s, nameTable));
		}

		// Token: 0x0600125B RID: 4699 RVA: 0x00051F48 File Offset: 0x00050148
		private string[] GetValidatedArray(string value, XmlNameTable nt)
		{
			string[] array = base.ParseListValue(value, nt);
			for (int i = 0; i < array.Length; i++)
			{
				XmlConvert.VerifyNCName(array[i]);
			}
			return array;
		}
	}
}
