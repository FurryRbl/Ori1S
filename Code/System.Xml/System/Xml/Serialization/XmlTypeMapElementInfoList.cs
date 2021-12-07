using System;
using System.Collections;

namespace System.Xml.Serialization
{
	// Token: 0x020002BC RID: 700
	internal class XmlTypeMapElementInfoList : ArrayList
	{
		// Token: 0x06001D83 RID: 7555 RVA: 0x0009B9B8 File Offset: 0x00099BB8
		public int IndexOfElement(string name, string namspace)
		{
			for (int i = 0; i < this.Count; i++)
			{
				XmlTypeMapElementInfo xmlTypeMapElementInfo = (XmlTypeMapElementInfo)base[i];
				if (xmlTypeMapElementInfo.ElementName == name && xmlTypeMapElementInfo.Namespace == namspace)
				{
					return i;
				}
			}
			return -1;
		}
	}
}
