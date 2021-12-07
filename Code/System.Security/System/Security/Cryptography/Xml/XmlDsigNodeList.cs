using System;
using System.Collections;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	// Token: 0x0200005C RID: 92
	internal class XmlDsigNodeList : XmlNodeList
	{
		// Token: 0x060002FD RID: 765 RVA: 0x0000DE0C File Offset: 0x0000C00C
		public XmlDsigNodeList(ArrayList rgNodes)
		{
			this._rgNodes = rgNodes;
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x060002FE RID: 766 RVA: 0x0000DE1C File Offset: 0x0000C01C
		public override int Count
		{
			get
			{
				return this._rgNodes.Count;
			}
		}

		// Token: 0x060002FF RID: 767 RVA: 0x0000DE2C File Offset: 0x0000C02C
		public override IEnumerator GetEnumerator()
		{
			return this._rgNodes.GetEnumerator();
		}

		// Token: 0x06000300 RID: 768 RVA: 0x0000DE3C File Offset: 0x0000C03C
		public override XmlNode Item(int index)
		{
			if (index < 0 || this._rgNodes.Count <= index)
			{
				return null;
			}
			return (XmlNode)this._rgNodes[index];
		}

		// Token: 0x04000148 RID: 328
		private ArrayList _rgNodes;
	}
}
