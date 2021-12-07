using System;
using System.Xml;
using System.Xml.Schema;

namespace Mono.Xml.Schema
{
	// Token: 0x020001CB RID: 459
	internal class XsdEntities : XsdName
	{
		// Token: 0x06001260 RID: 4704 RVA: 0x00051F98 File Offset: 0x00050198
		internal XsdEntities()
		{
		}

		// Token: 0x1700055B RID: 1371
		// (get) Token: 0x06001261 RID: 4705 RVA: 0x00051FA0 File Offset: 0x000501A0
		public override XmlTokenizedType TokenizedType
		{
			get
			{
				return XmlTokenizedType.ENTITIES;
			}
		}

		// Token: 0x1700055C RID: 1372
		// (get) Token: 0x06001262 RID: 4706 RVA: 0x00051FA4 File Offset: 0x000501A4
		[MonoTODO]
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.Item;
			}
		}

		// Token: 0x1700055D RID: 1373
		// (get) Token: 0x06001263 RID: 4707 RVA: 0x00051FA8 File Offset: 0x000501A8
		public override Type ValueType
		{
			get
			{
				return typeof(string[]);
			}
		}

		// Token: 0x06001264 RID: 4708 RVA: 0x00051FB4 File Offset: 0x000501B4
		public override object ParseValue(string value, XmlNameTable nt, IXmlNamespaceResolver nsmgr)
		{
			return this.GetValidatedArray(value, nt);
		}

		// Token: 0x06001265 RID: 4709 RVA: 0x00051FC0 File Offset: 0x000501C0
		internal override ValueType ParseValueType(string s, XmlNameTable nameTable, IXmlNamespaceResolver nsmgr)
		{
			return new StringArrayValueType(this.GetValidatedArray(s, nameTable));
		}

		// Token: 0x06001266 RID: 4710 RVA: 0x00051FD4 File Offset: 0x000501D4
		private string[] GetValidatedArray(string value, XmlNameTable nt)
		{
			string[] array = base.ParseListValue(value, nt);
			for (int i = 0; i < array.Length; i++)
			{
				if (!XmlChar.IsName(array[i]))
				{
					throw new ArgumentException("Invalid entitiy name.");
				}
			}
			return array;
		}
	}
}
