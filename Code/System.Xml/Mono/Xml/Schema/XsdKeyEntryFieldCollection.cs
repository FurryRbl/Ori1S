using System;
using System.Collections;

namespace Mono.Xml.Schema
{
	// Token: 0x02000021 RID: 33
	internal class XsdKeyEntryFieldCollection : CollectionBase
	{
		// Token: 0x1700002D RID: 45
		public XsdKeyEntryField this[int i]
		{
			get
			{
				return (XsdKeyEntryField)base.List[i];
			}
			set
			{
				base.List[i] = value;
			}
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00008194 File Offset: 0x00006394
		public int Add(XsdKeyEntryField value)
		{
			return base.List.Add(value);
		}
	}
}
