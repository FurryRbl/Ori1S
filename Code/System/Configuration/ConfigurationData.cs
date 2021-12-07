using System;
using System.Collections;
using System.IO;
using System.Security.Permissions;
using System.Xml;

namespace System.Configuration
{
	// Token: 0x020001D3 RID: 467
	internal class ConfigurationData
	{
		// Token: 0x0600103D RID: 4157 RVA: 0x0002AFF8 File Offset: 0x000291F8
		public ConfigurationData() : this(null)
		{
		}

		// Token: 0x0600103E RID: 4158 RVA: 0x0002B004 File Offset: 0x00029204
		public ConfigurationData(ConfigurationData parent)
		{
			this.parent = ((parent != this) ? parent : null);
			this.factories = new Hashtable();
		}

		// Token: 0x17000399 RID: 921
		// (get) Token: 0x06001040 RID: 4160 RVA: 0x0002B04C File Offset: 0x0002924C
		private Hashtable FileCache
		{
			get
			{
				if (this.cache != null)
				{
					return this.cache;
				}
				this.cache = new Hashtable();
				return this.cache;
			}
		}

		// Token: 0x06001041 RID: 4161 RVA: 0x0002B074 File Offset: 0x00029274
		[PermissionSet(SecurityAction.Assert, XML = "<PermissionSet class=\"System.Security.PermissionSet\"\nversion=\"1\">\n<IPermission class=\"System.Security.Permissions.FileIOPermission, mscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089\"\nversion=\"1\"\nUnrestricted=\"true\"/>\n</PermissionSet>\n")]
		public bool Load(string fileName)
		{
			this.fileName = fileName;
			if (fileName == null || !File.Exists(fileName))
			{
				return false;
			}
			XmlTextReader xmlTextReader = null;
			try
			{
				FileStream input = new FileStream(fileName, FileMode.Open, FileAccess.Read);
				xmlTextReader = new XmlTextReader(input);
				if (this.InitRead(xmlTextReader))
				{
					this.ReadConfigFile(xmlTextReader);
				}
			}
			catch (ConfigurationException)
			{
				throw;
			}
			catch (Exception inner)
			{
				throw new ConfigurationException("Error reading " + fileName, inner);
			}
			finally
			{
				if (xmlTextReader != null)
				{
					xmlTextReader.Close();
				}
			}
			return true;
		}

		// Token: 0x06001042 RID: 4162 RVA: 0x0002B140 File Offset: 0x00029340
		public bool LoadString(string data)
		{
			if (data == null)
			{
				return false;
			}
			XmlTextReader xmlTextReader = null;
			try
			{
				TextReader input = new StringReader(data);
				xmlTextReader = new XmlTextReader(input);
				if (this.InitRead(xmlTextReader))
				{
					this.ReadConfigFile(xmlTextReader);
				}
			}
			catch (ConfigurationException)
			{
				throw;
			}
			catch (Exception inner)
			{
				throw new ConfigurationException("Error reading " + this.fileName, inner);
			}
			finally
			{
				if (xmlTextReader != null)
				{
					xmlTextReader.Close();
				}
			}
			return true;
		}

		// Token: 0x06001043 RID: 4163 RVA: 0x0002B1FC File Offset: 0x000293FC
		private object GetHandler(string sectionName)
		{
			Hashtable obj = this.factories;
			object result;
			lock (obj)
			{
				object obj2 = this.factories[sectionName];
				if (obj2 == null || obj2 == ConfigurationData.removedMark)
				{
					if (this.parent != null)
					{
						result = this.parent.GetHandler(sectionName);
					}
					else
					{
						result = null;
					}
				}
				else if (obj2 is IConfigurationSectionHandler)
				{
					result = (IConfigurationSectionHandler)obj2;
				}
				else
				{
					obj2 = this.CreateNewHandler(sectionName, (SectionData)obj2);
					this.factories[sectionName] = obj2;
					result = obj2;
				}
			}
			return result;
		}

		// Token: 0x06001044 RID: 4164 RVA: 0x0002B2BC File Offset: 0x000294BC
		private object CreateNewHandler(string sectionName, SectionData section)
		{
			Type type = Type.GetType(section.TypeName);
			if (type == null)
			{
				throw new ConfigurationException("Cannot get Type for " + section.TypeName);
			}
			object obj = Activator.CreateInstance(type, true);
			if (obj == null)
			{
				throw new ConfigurationException("Cannot get instance for " + type);
			}
			return obj;
		}

		// Token: 0x06001045 RID: 4165 RVA: 0x0002B314 File Offset: 0x00029514
		private XmlDocument GetInnerDoc(XmlDocument doc, int i, string[] sectionPath)
		{
			if (++i >= sectionPath.Length)
			{
				return doc;
			}
			if (doc.DocumentElement == null)
			{
				return null;
			}
			for (XmlNode xmlNode = doc.DocumentElement.FirstChild; xmlNode != null; xmlNode = xmlNode.NextSibling)
			{
				if (xmlNode.Name == sectionPath[i])
				{
					ConfigXmlDocument configXmlDocument = new ConfigXmlDocument();
					configXmlDocument.Load(new StringReader(xmlNode.OuterXml));
					return this.GetInnerDoc(configXmlDocument, i, sectionPath);
				}
			}
			return null;
		}

		// Token: 0x06001046 RID: 4166 RVA: 0x0002B394 File Offset: 0x00029594
		private XmlDocument GetDocumentForSection(string sectionName)
		{
			ConfigXmlDocument configXmlDocument = new ConfigXmlDocument();
			if (this.pending == null)
			{
				return configXmlDocument;
			}
			string[] array = sectionName.Split(new char[]
			{
				'/'
			});
			string text = this.pending[array[0]] as string;
			if (text == null)
			{
				return configXmlDocument;
			}
			StringReader input = new StringReader(text);
			XmlTextReader xmlTextReader = new XmlTextReader(input);
			xmlTextReader.MoveToContent();
			configXmlDocument.LoadSingleElement(this.fileName, xmlTextReader);
			return this.GetInnerDoc(configXmlDocument, 0, array);
		}

		// Token: 0x06001047 RID: 4167 RVA: 0x0002B410 File Offset: 0x00029610
		private object GetConfigInternal(string sectionName)
		{
			object handler = this.GetHandler(sectionName);
			IConfigurationSectionHandler configurationSectionHandler = handler as IConfigurationSectionHandler;
			if (configurationSectionHandler == null)
			{
				return handler;
			}
			object result = null;
			if (this.parent != null)
			{
				result = this.parent.GetConfig(sectionName);
			}
			XmlDocument documentForSection = this.GetDocumentForSection(sectionName);
			if (documentForSection == null || documentForSection.DocumentElement == null)
			{
				return result;
			}
			return configurationSectionHandler.Create(result, this.fileName, documentForSection.DocumentElement);
		}

		// Token: 0x06001048 RID: 4168 RVA: 0x0002B47C File Offset: 0x0002967C
		public object GetConfig(string sectionName)
		{
			object obj;
			lock (this)
			{
				obj = this.FileCache[sectionName];
			}
			if (obj == ConfigurationData.emptyMark)
			{
				return null;
			}
			if (obj != null)
			{
				return obj;
			}
			lock (this)
			{
				obj = this.GetConfigInternal(sectionName);
				this.FileCache[sectionName] = ((obj != null) ? obj : ConfigurationData.emptyMark);
			}
			return obj;
		}

		// Token: 0x06001049 RID: 4169 RVA: 0x0002B530 File Offset: 0x00029730
		private object LookForFactory(string key)
		{
			object obj = this.factories[key];
			if (obj != null)
			{
				return obj;
			}
			if (this.parent != null)
			{
				return this.parent.LookForFactory(key);
			}
			return null;
		}

		// Token: 0x0600104A RID: 4170 RVA: 0x0002B56C File Offset: 0x0002976C
		private bool InitRead(XmlTextReader reader)
		{
			reader.MoveToContent();
			if (reader.NodeType != XmlNodeType.Element || reader.Name != "configuration")
			{
				this.ThrowException("Configuration file does not have a valid root element", reader);
			}
			if (reader.HasAttributes)
			{
				this.ThrowException("Unrecognized attribute in root element", reader);
			}
			if (reader.IsEmptyElement)
			{
				reader.Skip();
				return false;
			}
			reader.Read();
			reader.MoveToContent();
			return reader.NodeType != XmlNodeType.EndElement;
		}

		// Token: 0x0600104B RID: 4171 RVA: 0x0002B5F4 File Offset: 0x000297F4
		private void MoveToNextElement(XmlTextReader reader)
		{
			while (reader.Read())
			{
				XmlNodeType nodeType = reader.NodeType;
				if (nodeType == XmlNodeType.Element)
				{
					return;
				}
				if (nodeType != XmlNodeType.Whitespace && nodeType != XmlNodeType.Comment && nodeType != XmlNodeType.SignificantWhitespace && nodeType != XmlNodeType.EndElement)
				{
					this.ThrowException("Unrecognized element", reader);
				}
			}
		}

		// Token: 0x0600104C RID: 4172 RVA: 0x0002B64C File Offset: 0x0002984C
		private void ReadSection(XmlTextReader reader, string sectionName)
		{
			string text = null;
			string text2 = null;
			string text3 = null;
			string text4 = null;
			bool flag = false;
			string text5 = null;
			bool flag2 = true;
			AllowDefinition allowDefinition = AllowDefinition.Everywhere;
			while (reader.MoveToNextAttribute())
			{
				string name = reader.Name;
				if (name != null)
				{
					if (name == "allowLocation")
					{
						if (text3 != null)
						{
							this.ThrowException("Duplicated allowLocation attribute.", reader);
						}
						text3 = reader.Value;
						flag2 = (text3 == "true");
						if (!flag2 && text3 != "false")
						{
							this.ThrowException("Invalid attribute value", reader);
						}
					}
					else if (name == "requirePermission")
					{
						if (text5 != null)
						{
							this.ThrowException("Duplicated requirePermission attribute.", reader);
						}
						text5 = reader.Value;
						flag = (text5 == "true");
						if (!flag && text5 != "false")
						{
							this.ThrowException("Invalid attribute value", reader);
						}
					}
					else if (name == "allowDefinition")
					{
						if (text4 != null)
						{
							this.ThrowException("Duplicated allowDefinition attribute.", reader);
						}
						text4 = reader.Value;
						try
						{
							allowDefinition = (AllowDefinition)((int)Enum.Parse(typeof(AllowDefinition), text4));
						}
						catch
						{
							this.ThrowException("Invalid attribute value", reader);
						}
					}
					else if (name == "type")
					{
						if (text2 != null)
						{
							this.ThrowException("Duplicated type attribute.", reader);
						}
						text2 = reader.Value;
					}
					else if (name == "name")
					{
						if (text != null)
						{
							this.ThrowException("Duplicated name attribute.", reader);
						}
						text = reader.Value;
						if (text == "location")
						{
							this.ThrowException("location is a reserved section name", reader);
						}
					}
					else
					{
						this.ThrowException("Unrecognized attribute.", reader);
					}
				}
			}
			if (text == null || text2 == null)
			{
				this.ThrowException("Required attribute missing", reader);
			}
			if (sectionName != null)
			{
				text = sectionName + '/' + text;
			}
			reader.MoveToElement();
			object obj = this.LookForFactory(text);
			if (obj != null && obj != ConfigurationData.removedMark)
			{
				this.ThrowException("Already have a factory for " + text, reader);
			}
			SectionData sectionData = new SectionData(text, text2, flag2, allowDefinition, flag);
			sectionData.FileName = this.fileName;
			this.factories[text] = sectionData;
			if (reader.IsEmptyElement)
			{
				reader.Skip();
			}
			else
			{
				reader.Read();
				reader.MoveToContent();
				if (reader.NodeType != XmlNodeType.EndElement)
				{
					this.ReadSections(reader, text);
				}
				reader.ReadEndElement();
			}
			reader.MoveToContent();
		}

		// Token: 0x0600104D RID: 4173 RVA: 0x0002B924 File Offset: 0x00029B24
		private void ReadRemoveSection(XmlTextReader reader, string sectionName)
		{
			if (!reader.MoveToNextAttribute() || reader.Name != "name")
			{
				this.ThrowException("Unrecognized attribute.", reader);
			}
			string text = reader.Value;
			if (text == null || text.Length == 0)
			{
				this.ThrowException("Empty name to remove", reader);
			}
			reader.MoveToElement();
			if (sectionName != null)
			{
				text = sectionName + '/' + text;
			}
			object obj = this.LookForFactory(text);
			if (obj != null && obj == ConfigurationData.removedMark)
			{
				this.ThrowException("No factory for " + text, reader);
			}
			this.factories[text] = ConfigurationData.removedMark;
			this.MoveToNextElement(reader);
		}

		// Token: 0x0600104E RID: 4174 RVA: 0x0002B9E0 File Offset: 0x00029BE0
		private void ReadSectionGroup(XmlTextReader reader, string configSection)
		{
			if (!reader.MoveToNextAttribute())
			{
				this.ThrowException("sectionGroup must have a 'name' attribute.", reader);
			}
			string text = null;
			do
			{
				if (reader.Name == "name")
				{
					if (text != null)
					{
						this.ThrowException("Duplicate 'name' attribute.", reader);
					}
					text = reader.Value;
				}
				else if (reader.Name != "type")
				{
					this.ThrowException("Unrecognized attribute.", reader);
				}
			}
			while (reader.MoveToNextAttribute());
			if (text == null)
			{
				this.ThrowException("No 'name' attribute.", reader);
			}
			if (text == "location")
			{
				this.ThrowException("location is a reserved section name", reader);
			}
			if (configSection != null)
			{
				text = configSection + '/' + text;
			}
			object obj = this.LookForFactory(text);
			if (obj != null && obj != ConfigurationData.removedMark && obj != ConfigurationData.groupMark)
			{
				this.ThrowException("Already have a factory for " + text, reader);
			}
			this.factories[text] = ConfigurationData.groupMark;
			if (reader.IsEmptyElement)
			{
				reader.Skip();
				reader.MoveToContent();
			}
			else
			{
				reader.Read();
				reader.MoveToContent();
				if (reader.NodeType != XmlNodeType.EndElement)
				{
					this.ReadSections(reader, text);
				}
				reader.ReadEndElement();
				reader.MoveToContent();
			}
		}

		// Token: 0x0600104F RID: 4175 RVA: 0x0002BB3C File Offset: 0x00029D3C
		private void ReadSections(XmlTextReader reader, string configSection)
		{
			int depth = reader.Depth;
			reader.MoveToContent();
			while (reader.Depth == depth)
			{
				string name = reader.Name;
				if (name == "section")
				{
					this.ReadSection(reader, configSection);
				}
				else if (name == "remove")
				{
					this.ReadRemoveSection(reader, configSection);
				}
				else if (name == "clear")
				{
					if (reader.HasAttributes)
					{
						this.ThrowException("Unrecognized attribute.", reader);
					}
					this.factories.Clear();
					this.MoveToNextElement(reader);
				}
				else if (name == "sectionGroup")
				{
					this.ReadSectionGroup(reader, configSection);
				}
				else
				{
					this.ThrowException("Unrecognized element: " + reader.Name, reader);
				}
				reader.MoveToContent();
			}
		}

		// Token: 0x06001050 RID: 4176 RVA: 0x0002BC24 File Offset: 0x00029E24
		private void StorePending(string name, XmlTextReader reader)
		{
			if (this.pending == null)
			{
				this.pending = new Hashtable();
			}
			this.pending[name] = reader.ReadOuterXml();
		}

		// Token: 0x06001051 RID: 4177 RVA: 0x0002BC5C File Offset: 0x00029E5C
		private void ReadConfigFile(XmlTextReader reader)
		{
			reader.MoveToContent();
			while (!reader.EOF && reader.NodeType != XmlNodeType.EndElement)
			{
				string name = reader.Name;
				if (name == "configSections")
				{
					if (reader.HasAttributes)
					{
						this.ThrowException("Unrecognized attribute in <configSections>.", reader);
					}
					if (reader.IsEmptyElement)
					{
						reader.Skip();
					}
					else
					{
						reader.Read();
						reader.MoveToContent();
						if (reader.NodeType != XmlNodeType.EndElement)
						{
							this.ReadSections(reader, null);
						}
						reader.ReadEndElement();
					}
				}
				else if (name != null && name != string.Empty)
				{
					this.StorePending(name, reader);
					this.MoveToNextElement(reader);
				}
				else
				{
					this.MoveToNextElement(reader);
				}
				reader.MoveToContent();
			}
		}

		// Token: 0x06001052 RID: 4178 RVA: 0x0002BD38 File Offset: 0x00029F38
		private void ThrowException(string text, XmlTextReader reader)
		{
			throw new ConfigurationException(text, this.fileName, reader.LineNumber);
		}

		// Token: 0x0400048E RID: 1166
		private ConfigurationData parent;

		// Token: 0x0400048F RID: 1167
		private Hashtable factories;

		// Token: 0x04000490 RID: 1168
		private static object removedMark = new object();

		// Token: 0x04000491 RID: 1169
		private static object emptyMark = new object();

		// Token: 0x04000492 RID: 1170
		private Hashtable pending;

		// Token: 0x04000493 RID: 1171
		private string fileName;

		// Token: 0x04000494 RID: 1172
		private static object groupMark = new object();

		// Token: 0x04000495 RID: 1173
		private Hashtable cache;
	}
}
