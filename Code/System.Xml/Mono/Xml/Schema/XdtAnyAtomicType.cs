using System;
using System.Xml.Schema;

namespace Mono.Xml.Schema
{
	// Token: 0x020001BD RID: 445
	internal class XdtAnyAtomicType : XsdAnySimpleType
	{
		// Token: 0x06001219 RID: 4633 RVA: 0x00051C0C File Offset: 0x0004FE0C
		internal XdtAnyAtomicType()
		{
		}

		// Token: 0x17000530 RID: 1328
		// (get) Token: 0x0600121A RID: 4634 RVA: 0x00051C14 File Offset: 0x0004FE14
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.AnyAtomicType;
			}
		}
	}
}
