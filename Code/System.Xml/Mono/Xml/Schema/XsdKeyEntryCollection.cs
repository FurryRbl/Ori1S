using System;
using System.Collections;

namespace Mono.Xml.Schema
{
	// Token: 0x02000023 RID: 35
	internal class XsdKeyEntryCollection : CollectionBase
	{
		// Token: 0x060000D5 RID: 213 RVA: 0x000084D0 File Offset: 0x000066D0
		public void Add(XsdKeyEntry entry)
		{
			base.List.Add(entry);
		}

		// Token: 0x1700002F RID: 47
		public XsdKeyEntry this[int i]
		{
			get
			{
				return (XsdKeyEntry)base.List[i];
			}
			set
			{
				base.List[i] = value;
			}
		}
	}
}
