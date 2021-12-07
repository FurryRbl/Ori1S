using System;
using System.Collections;
using System.Xml;

namespace Mono.Xml
{
	// Token: 0x0200000B RID: 11
	internal class XmlDsigC14NTransformNamespacesComparer : IComparer
	{
		// Token: 0x0600002B RID: 43 RVA: 0x00003DE0 File Offset: 0x00001FE0
		public int Compare(object x, object y)
		{
			XmlNode xmlNode = x as XmlNode;
			XmlNode xmlNode2 = y as XmlNode;
			if (xmlNode == xmlNode2)
			{
				return 0;
			}
			if (xmlNode == null)
			{
				return -1;
			}
			if (xmlNode2 == null)
			{
				return 1;
			}
			if (xmlNode.Prefix == string.Empty)
			{
				return -1;
			}
			if (xmlNode2.Prefix == string.Empty)
			{
				return 1;
			}
			return string.Compare(xmlNode.LocalName, xmlNode2.LocalName);
		}
	}
}
