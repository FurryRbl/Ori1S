using System;
using System.Collections;

namespace System.Xml
{
	// Token: 0x0200010A RID: 266
	internal class XmlNodeArrayList : XmlNodeList
	{
		// Token: 0x06000A9E RID: 2718 RVA: 0x00037E18 File Offset: 0x00036018
		public XmlNodeArrayList(ArrayList rgNodes)
		{
			this._rgNodes = rgNodes;
		}

		// Token: 0x170002FC RID: 764
		// (get) Token: 0x06000A9F RID: 2719 RVA: 0x00037E28 File Offset: 0x00036028
		public override int Count
		{
			get
			{
				return this._rgNodes.Count;
			}
		}

		// Token: 0x06000AA0 RID: 2720 RVA: 0x00037E38 File Offset: 0x00036038
		public override IEnumerator GetEnumerator()
		{
			return this._rgNodes.GetEnumerator();
		}

		// Token: 0x06000AA1 RID: 2721 RVA: 0x00037E48 File Offset: 0x00036048
		public override XmlNode Item(int index)
		{
			if (index < 0 || this._rgNodes.Count <= index)
			{
				return null;
			}
			return (XmlNode)this._rgNodes[index];
		}

		// Token: 0x0400053C RID: 1340
		private ArrayList _rgNodes;
	}
}
