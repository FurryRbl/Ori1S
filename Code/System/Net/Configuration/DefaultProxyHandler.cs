using System;
using System.Collections;
using System.Configuration;
using System.Xml;

namespace System.Net.Configuration
{
	// Token: 0x020002D2 RID: 722
	internal class DefaultProxyHandler : System.Configuration.IConfigurationSectionHandler
	{
		// Token: 0x060018C6 RID: 6342 RVA: 0x00044220 File Offset: 0x00042420
		public virtual object Create(object parent, object configContext, XmlNode section)
		{
			IWebProxy webProxy = parent as IWebProxy;
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
					if (name == "proxy")
					{
						string text = HandlersUtil.ExtractAttributeValue("usesystemdefault", xmlNode, true);
						string text2 = HandlersUtil.ExtractAttributeValue("bypassonlocal", xmlNode, true);
						string text3 = HandlersUtil.ExtractAttributeValue("proxyaddress", xmlNode, true);
						if (xmlNode.Attributes != null && xmlNode.Attributes.Count != 0)
						{
							HandlersUtil.ThrowException("Unrecognized attribute", xmlNode);
						}
						webProxy = new WebProxy();
						bool flag = text2 != null && string.Compare(text2, "true", true) == 0;
						if (!flag && text2 != null && string.Compare(text2, "false", true) != 0)
						{
							HandlersUtil.ThrowException("Invalid boolean value", xmlNode);
						}
						if (webProxy is WebProxy)
						{
							((WebProxy)webProxy).BypassProxyOnLocal = flag;
							if (text3 != null)
							{
								try
								{
									((WebProxy)webProxy).Address = new System.Uri(text3);
									continue;
								}
								catch (System.UriFormatException)
								{
								}
							}
							if (text != null && string.Compare(text, "true", true) == 0)
							{
								text3 = Environment.GetEnvironmentVariable("http_proxy");
								if (text3 == null)
								{
									text3 = Environment.GetEnvironmentVariable("HTTP_PROXY");
								}
								if (text3 != null)
								{
									try
									{
										System.Uri uri = new System.Uri(text3);
										IPAddress other;
										if (IPAddress.TryParse(uri.Host, out other))
										{
											if (IPAddress.Any.Equals(other))
											{
												uri = new System.UriBuilder(uri)
												{
													Host = "127.0.0.1"
												}.Uri;
											}
											else if (IPAddress.IPv6Any.Equals(other))
											{
												uri = new System.UriBuilder(uri)
												{
													Host = "[::1]"
												}.Uri;
											}
										}
										((WebProxy)webProxy).Address = uri;
									}
									catch (System.UriFormatException)
									{
									}
								}
							}
						}
					}
					else if (name == "bypasslist")
					{
						if (webProxy is WebProxy)
						{
							DefaultProxyHandler.FillByPassList(xmlNode, (WebProxy)webProxy);
						}
					}
					else
					{
						if (name == "module")
						{
							HandlersUtil.ThrowException("WARNING: module not implemented yet", xmlNode);
						}
						HandlersUtil.ThrowException("Unexpected element", xmlNode);
					}
				}
			}
			return webProxy;
		}

		// Token: 0x060018C7 RID: 6343 RVA: 0x0004454C File Offset: 0x0004274C
		private static void FillByPassList(XmlNode node, WebProxy proxy)
		{
			ArrayList arrayList = new ArrayList(proxy.BypassArrayList);
			if (node.Attributes != null && node.Attributes.Count != 0)
			{
				HandlersUtil.ThrowException("Unrecognized attribute", node);
			}
			XmlNodeList childNodes = node.ChildNodes;
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
					if (name == "add")
					{
						string text = HandlersUtil.ExtractAttributeValue("address", xmlNode);
						if (!arrayList.Contains(text))
						{
							arrayList.Add(text);
						}
					}
					else if (name == "remove")
					{
						string obj2 = HandlersUtil.ExtractAttributeValue("address", xmlNode);
						arrayList.Remove(obj2);
					}
					else if (name == "clear")
					{
						if (node.Attributes != null && node.Attributes.Count != 0)
						{
							HandlersUtil.ThrowException("Unrecognized attribute", node);
						}
						arrayList.Clear();
					}
					else
					{
						HandlersUtil.ThrowException("Unexpected element", xmlNode);
					}
				}
			}
			proxy.BypassList = (string[])arrayList.ToArray(typeof(string));
		}
	}
}
