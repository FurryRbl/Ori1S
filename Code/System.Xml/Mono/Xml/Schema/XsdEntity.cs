using System;
using System.Xml;
using System.Xml.Schema;

namespace Mono.Xml.Schema
{
	// Token: 0x020001CA RID: 458
	internal class XsdEntity : XsdName
	{
		// Token: 0x0600125C RID: 4700 RVA: 0x00051F7C File Offset: 0x0005017C
		internal XsdEntity()
		{
		}

		// Token: 0x17000558 RID: 1368
		// (get) Token: 0x0600125D RID: 4701 RVA: 0x00051F84 File Offset: 0x00050184
		public override XmlTokenizedType TokenizedType
		{
			get
			{
				return XmlTokenizedType.ENTITY;
			}
		}

		// Token: 0x17000559 RID: 1369
		// (get) Token: 0x0600125E RID: 4702 RVA: 0x00051F88 File Offset: 0x00050188
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.Entity;
			}
		}

		// Token: 0x1700055A RID: 1370
		// (get) Token: 0x0600125F RID: 4703 RVA: 0x00051F8C File Offset: 0x0005018C
		public override Type ValueType
		{
			get
			{
				return typeof(string);
			}
		}
	}
}
