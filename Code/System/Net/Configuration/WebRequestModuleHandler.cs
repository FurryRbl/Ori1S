using System;
using System.Configuration;
using System.Xml;

namespace System.Net.Configuration
{
	// Token: 0x020002EC RID: 748
	internal class WebRequestModuleHandler : System.Configuration.IConfigurationSectionHandler
	{
		// Token: 0x06001986 RID: 6534 RVA: 0x00045ED4 File Offset: 0x000440D4
		public virtual object Create(object parent, object configContext, XmlNode section)
		{
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
					if (name == "clear")
					{
						if (xmlNode.Attributes != null && xmlNode.Attributes.Count != 0)
						{
							HandlersUtil.ThrowException("Unrecognized attribute", xmlNode);
						}
						WebRequest.ClearPrefixes();
					}
					else
					{
						string prefix = HandlersUtil.ExtractAttributeValue("prefix", xmlNode);
						if (name == "add")
						{
							string typeName = HandlersUtil.ExtractAttributeValue("type", xmlNode, false);
							if (xmlNode.Attributes != null && xmlNode.Attributes.Count != 0)
							{
								HandlersUtil.ThrowException("Unrecognized attribute", xmlNode);
							}
							WebRequest.AddPrefix(prefix, typeName);
						}
						else if (name == "remove")
						{
							if (xmlNode.Attributes != null && xmlNode.Attributes.Count != 0)
							{
								HandlersUtil.ThrowException("Unrecognized attribute", xmlNode);
							}
							WebRequest.RemovePrefix(prefix);
						}
						else
						{
							HandlersUtil.ThrowException("Unexpected element", xmlNode);
						}
					}
				}
			}
			return null;
		}
	}
}
