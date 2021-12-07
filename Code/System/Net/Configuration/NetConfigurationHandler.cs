using System;
using System.Configuration;
using System.Xml;

namespace System.Net.Configuration
{
	// Token: 0x020002DB RID: 731
	internal class NetConfigurationHandler : System.Configuration.IConfigurationSectionHandler
	{
		// Token: 0x06001904 RID: 6404 RVA: 0x00044F24 File Offset: 0x00043124
		public virtual object Create(object parent, object configContext, XmlNode section)
		{
			NetConfig netConfig = new NetConfig();
			if (section.Attributes != null && section.Attributes.Count != 0)
			{
				HandlersUtil.ThrowException("Unrecognized attribute", section);
			}
			XmlNodeList childNodes = section.ChildNodes;
			foreach (object obj in childNodes)
			{
				XmlNode xmlNode = (XmlNode)obj;
				XmlNodeType nodeType = xmlNode.NodeType;
				if (nodeType != XmlNodeType.Whitespace && nodeType != XmlNodeType.Comment)
				{
					if (nodeType != XmlNodeType.Element)
					{
						HandlersUtil.ThrowException("Only elements allowed", xmlNode);
					}
					string name = xmlNode.Name;
					if (name == "ipv6")
					{
						string a = HandlersUtil.ExtractAttributeValue("enabled", xmlNode, false);
						if (xmlNode.Attributes != null && xmlNode.Attributes.Count != 0)
						{
							HandlersUtil.ThrowException("Unrecognized attribute", xmlNode);
						}
						if (a == "true")
						{
							netConfig.ipv6Enabled = true;
						}
						else if (a != "false")
						{
							HandlersUtil.ThrowException("Invalid boolean value", xmlNode);
						}
					}
					else if (name == "httpWebRequest")
					{
						string text = HandlersUtil.ExtractAttributeValue("maximumResponseHeadersLength", xmlNode, true);
						HandlersUtil.ExtractAttributeValue("useUnsafeHeaderParsing", xmlNode, true);
						if (xmlNode.Attributes != null && xmlNode.Attributes.Count != 0)
						{
							HandlersUtil.ThrowException("Unrecognized attribute", xmlNode);
						}
						try
						{
							if (text != null)
							{
								int num = int.Parse(text.Trim());
								if (num < -1)
								{
									HandlersUtil.ThrowException("Must be -1 or >= 0", xmlNode);
								}
								netConfig.MaxResponseHeadersLength = num;
							}
						}
						catch
						{
							HandlersUtil.ThrowException("Invalid int value", xmlNode);
						}
					}
					else
					{
						HandlersUtil.ThrowException("Unexpected element", xmlNode);
					}
				}
			}
			return netConfig;
		}
	}
}
