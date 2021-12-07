using System;

namespace System.Xml.Serialization
{
	// Token: 0x020002C1 RID: 705
	internal class XmlTypeMapMemberExpandable : XmlTypeMapMemberElement
	{
		// Token: 0x17000835 RID: 2101
		// (get) Token: 0x06001DBC RID: 7612 RVA: 0x0009C18C File Offset: 0x0009A38C
		// (set) Token: 0x06001DBD RID: 7613 RVA: 0x0009C194 File Offset: 0x0009A394
		public int FlatArrayIndex
		{
			get
			{
				return this._flatArrayIndex;
			}
			set
			{
				this._flatArrayIndex = value;
			}
		}

		// Token: 0x04000BC6 RID: 3014
		private int _flatArrayIndex;
	}
}
