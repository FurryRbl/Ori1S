using System;
using System.Collections;
using System.Collections.Specialized;
using System.Xml;

namespace System.Configuration
{
	// Token: 0x020001CB RID: 459
	internal class ConfigHelper
	{
		// Token: 0x0600100F RID: 4111 RVA: 0x0002A6EC File Offset: 0x000288EC
		internal static IDictionary GetDictionary(IDictionary prev, XmlNode region, string nameAtt, string valueAtt)
		{
			Hashtable dict;
			if (prev == null)
			{
				dict = new Hashtable(CaseInsensitiveHashCodeProvider.Default, CaseInsensitiveComparer.Default);
			}
			else
			{
				Hashtable hashtable = (Hashtable)prev;
				dict = (Hashtable)hashtable.Clone();
			}
			ConfigHelper.CollectionWrapper collectionWrapper = new ConfigHelper.CollectionWrapper(dict);
			collectionWrapper = ConfigHelper.GoGetThem(collectionWrapper, region, nameAtt, valueAtt);
			if (collectionWrapper == null)
			{
				return null;
			}
			return collectionWrapper.UnWrap() as IDictionary;
		}

		// Token: 0x06001010 RID: 4112 RVA: 0x0002A74C File Offset: 0x0002894C
		internal static ConfigNameValueCollection GetNameValueCollection(System.Collections.Specialized.NameValueCollection prev, XmlNode region, string nameAtt, string valueAtt)
		{
			ConfigNameValueCollection configNameValueCollection = new ConfigNameValueCollection(CaseInsensitiveHashCodeProvider.Default, CaseInsensitiveComparer.Default);
			if (prev != null)
			{
				configNameValueCollection.Add(prev);
			}
			ConfigHelper.CollectionWrapper collectionWrapper = new ConfigHelper.CollectionWrapper(configNameValueCollection);
			collectionWrapper = ConfigHelper.GoGetThem(collectionWrapper, region, nameAtt, valueAtt);
			if (collectionWrapper == null)
			{
				return null;
			}
			return collectionWrapper.UnWrap() as ConfigNameValueCollection;
		}

		// Token: 0x06001011 RID: 4113 RVA: 0x0002A79C File Offset: 0x0002899C
		private static ConfigHelper.CollectionWrapper GoGetThem(ConfigHelper.CollectionWrapper result, XmlNode region, string nameAtt, string valueAtt)
		{
			if (region.Attributes != null && region.Attributes.Count != 0 && (region.Attributes.Count != 1 || region.Attributes[0].Name != "xmlns"))
			{
				throw new ConfigurationException("Unknown attribute", region);
			}
			XmlNodeList childNodes = region.ChildNodes;
			foreach (object obj in childNodes)
			{
				XmlNode xmlNode = (XmlNode)obj;
				XmlNodeType nodeType = xmlNode.NodeType;
				if (nodeType != XmlNodeType.Whitespace && nodeType != XmlNodeType.Comment)
				{
					if (nodeType != XmlNodeType.Element)
					{
						throw new ConfigurationException("Only XmlElement allowed", xmlNode);
					}
					string name = xmlNode.Name;
					if (name == "clear")
					{
						if (xmlNode.Attributes != null && xmlNode.Attributes.Count != 0)
						{
							throw new ConfigurationException("Unknown attribute", xmlNode);
						}
						result.Clear();
					}
					else if (name == "remove")
					{
						XmlNode xmlNode2 = null;
						if (xmlNode.Attributes != null)
						{
							xmlNode2 = xmlNode.Attributes.RemoveNamedItem(nameAtt);
						}
						if (xmlNode2 == null)
						{
							throw new ConfigurationException("Required attribute not found", xmlNode);
						}
						if (xmlNode2.Value == string.Empty)
						{
							throw new ConfigurationException("Required attribute is empty", xmlNode);
						}
						if (xmlNode.Attributes.Count != 0)
						{
							throw new ConfigurationException("Unknown attribute", xmlNode);
						}
						result.Remove(xmlNode2.Value);
					}
					else
					{
						if (!(name == "add"))
						{
							throw new ConfigurationException("Unknown element", xmlNode);
						}
						XmlNode xmlNode2 = null;
						if (xmlNode.Attributes != null)
						{
							xmlNode2 = xmlNode.Attributes.RemoveNamedItem(nameAtt);
						}
						if (xmlNode2 == null)
						{
							throw new ConfigurationException("Required attribute not found", xmlNode);
						}
						if (xmlNode2.Value == string.Empty)
						{
							throw new ConfigurationException("Required attribute is empty", xmlNode);
						}
						XmlNode xmlNode3 = xmlNode.Attributes.RemoveNamedItem(valueAtt);
						if (xmlNode3 == null)
						{
							throw new ConfigurationException("Required attribute not found", xmlNode);
						}
						if (xmlNode.Attributes.Count != 0)
						{
							throw new ConfigurationException("Unknown attribute", xmlNode);
						}
						result[xmlNode2.Value] = xmlNode3.Value;
					}
				}
			}
			return result;
		}

		// Token: 0x020001CC RID: 460
		private class CollectionWrapper
		{
			// Token: 0x06001012 RID: 4114 RVA: 0x0002AA2C File Offset: 0x00028C2C
			public CollectionWrapper(IDictionary dict)
			{
				this.dict = dict;
				this.isDict = true;
			}

			// Token: 0x06001013 RID: 4115 RVA: 0x0002AA44 File Offset: 0x00028C44
			public CollectionWrapper(System.Collections.Specialized.NameValueCollection collection)
			{
				this.collection = collection;
				this.isDict = false;
			}

			// Token: 0x06001014 RID: 4116 RVA: 0x0002AA5C File Offset: 0x00028C5C
			public void Remove(string s)
			{
				if (this.isDict)
				{
					this.dict.Remove(s);
				}
				else
				{
					this.collection.Remove(s);
				}
			}

			// Token: 0x06001015 RID: 4117 RVA: 0x0002AA94 File Offset: 0x00028C94
			public void Clear()
			{
				if (this.isDict)
				{
					this.dict.Clear();
				}
				else
				{
					this.collection.Clear();
				}
			}

			// Token: 0x17000392 RID: 914
			public string this[string key]
			{
				set
				{
					if (this.isDict)
					{
						this.dict[key] = value;
					}
					else
					{
						this.collection[key] = value;
					}
				}
			}

			// Token: 0x06001017 RID: 4119 RVA: 0x0002AB00 File Offset: 0x00028D00
			public object UnWrap()
			{
				if (this.isDict)
				{
					return this.dict;
				}
				return this.collection;
			}

			// Token: 0x0400047A RID: 1146
			private IDictionary dict;

			// Token: 0x0400047B RID: 1147
			private System.Collections.Specialized.NameValueCollection collection;

			// Token: 0x0400047C RID: 1148
			private bool isDict;
		}
	}
}
