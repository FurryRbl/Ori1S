using System;
using System.Xml.Schema;

namespace Mono.Xml.Schema
{
	// Token: 0x020001BE RID: 446
	internal class XdtUntypedAtomic : XdtAnyAtomicType
	{
		// Token: 0x0600121B RID: 4635 RVA: 0x00051C18 File Offset: 0x0004FE18
		internal XdtUntypedAtomic()
		{
		}

		// Token: 0x17000531 RID: 1329
		// (get) Token: 0x0600121C RID: 4636 RVA: 0x00051C20 File Offset: 0x0004FE20
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.UntypedAtomic;
			}
		}
	}
}
