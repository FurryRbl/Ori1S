using System;
using System.Collections;
using System.Xml;

namespace Mono.Xml
{
	// Token: 0x0200000A RID: 10
	internal class XmlDsigC14NTransformAttributesComparer : IComparer
	{
		// Token: 0x06000029 RID: 41 RVA: 0x00003D20 File Offset: 0x00001F20
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
			if (xmlNode.Prefix == xmlNode2.Prefix)
			{
				return string.Compare(xmlNode.LocalName, xmlNode2.LocalName);
			}
			if (xmlNode.Prefix == string.Empty)
			{
				return -1;
			}
			if (xmlNode2.Prefix == string.Empty)
			{
				return 1;
			}
			int num = string.Compare(xmlNode.NamespaceURI, xmlNode2.NamespaceURI);
			if (num == 0)
			{
				num = string.Compare(xmlNode.LocalName, xmlNode2.LocalName);
			}
			return num;
		}
	}
}
