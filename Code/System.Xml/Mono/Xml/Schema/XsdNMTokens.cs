using System;
using System.Xml;
using System.Xml.Schema;

namespace Mono.Xml.Schema
{
	// Token: 0x020001C4 RID: 452
	internal class XsdNMTokens : XsdNMToken
	{
		// Token: 0x06001238 RID: 4664 RVA: 0x00051D24 File Offset: 0x0004FF24
		internal XsdNMTokens()
		{
		}

		// Token: 0x17000546 RID: 1350
		// (get) Token: 0x06001239 RID: 4665 RVA: 0x00051D2C File Offset: 0x0004FF2C
		public override XmlTokenizedType TokenizedType
		{
			get
			{
				return XmlTokenizedType.NMTOKENS;
			}
		}

		// Token: 0x17000547 RID: 1351
		// (get) Token: 0x0600123A RID: 4666 RVA: 0x00051D30 File Offset: 0x0004FF30
		[MonoTODO]
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.Item;
			}
		}

		// Token: 0x17000548 RID: 1352
		// (get) Token: 0x0600123B RID: 4667 RVA: 0x00051D34 File Offset: 0x0004FF34
		public override Type ValueType
		{
			get
			{
				return typeof(string[]);
			}
		}

		// Token: 0x0600123C RID: 4668 RVA: 0x00051D40 File Offset: 0x0004FF40
		public override object ParseValue(string value, XmlNameTable nt, IXmlNamespaceResolver nsmgr)
		{
			return this.GetValidatedArray(value, nt);
		}

		// Token: 0x0600123D RID: 4669 RVA: 0x00051D4C File Offset: 0x0004FF4C
		internal override ValueType ParseValueType(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr)
		{
			return new StringArrayValueType(this.GetValidatedArray(s, nameTable));
		}

		// Token: 0x0600123E RID: 4670 RVA: 0x00051D60 File Offset: 0x0004FF60
		private string[] GetValidatedArray(string value, XmlNameTable nt)
		{
			string[] array = base.ParseListValue(value, nt);
			for (int i = 0; i < array.Length; i++)
			{
				if (!XmlChar.IsNmToken(array[i]))
				{
					throw new ArgumentException("Invalid name token.");
				}
			}
			return array;
		}
	}
}
