using System;

namespace System.Xml.Serialization
{
	// Token: 0x020002C2 RID: 706
	internal class XmlTypeMapMemberFlatList : XmlTypeMapMemberExpandable
	{
		// Token: 0x17000836 RID: 2102
		// (get) Token: 0x06001DBF RID: 7615 RVA: 0x0009C1A8 File Offset: 0x0009A3A8
		// (set) Token: 0x06001DC0 RID: 7616 RVA: 0x0009C1B0 File Offset: 0x0009A3B0
		public ListMap ListMap
		{
			get
			{
				return this._listMap;
			}
			set
			{
				this._listMap = value;
			}
		}

		// Token: 0x04000BC7 RID: 3015
		private ListMap _listMap;
	}
}
