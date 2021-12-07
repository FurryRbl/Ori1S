using System;
using System.Configuration;
using System.Xml;

namespace System.Net.Configuration
{
	// Token: 0x020002D0 RID: 720
	internal class HandlersUtil
	{
		// Token: 0x060018BD RID: 6333 RVA: 0x000440B4 File Offset: 0x000422B4
		private HandlersUtil()
		{
		}

		// Token: 0x060018BE RID: 6334 RVA: 0x000440BC File Offset: 0x000422BC
		internal static string ExtractAttributeValue(string attKey, XmlNode node)
		{
			return HandlersUtil.ExtractAttributeValue(attKey, node, false);
		}

		// Token: 0x060018BF RID: 6335 RVA: 0x000440C8 File Offset: 0x000422C8
		internal static string ExtractAttributeValue(string attKey, XmlNode node, bool optional)
		{
			if (node.Attributes == null)
			{
				if (optional)
				{
					return null;
				}
				HandlersUtil.ThrowException("Required attribute not found: " + attKey, node);
			}
			XmlNode xmlNode = node.Attributes.RemoveNamedItem(attKey);
			if (xmlNode == null)
			{
				if (optional)
				{
					return null;
				}
				HandlersUtil.ThrowException("Required attribute not found: " + attKey, node);
			}
			string value = xmlNode.Value;
			if (value == string.Empty)
			{
				string str = (!optional) ? "Required" : "Optional";
				HandlersUtil.ThrowException(str + " attribute is empty: " + attKey, node);
			}
			return value;
		}

		// Token: 0x060018C0 RID: 6336 RVA: 0x00044168 File Offset: 0x00042368
		internal static void ThrowException(string msg, XmlNode node)
		{
			if (node != null && node.Name != string.Empty)
			{
				msg = msg + " (node name: " + node.Name + ") ";
			}
			throw new System.Configuration.ConfigurationException(msg, node);
		}
	}
}
