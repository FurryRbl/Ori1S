using System;
using System.Xml;

namespace System.Configuration
{
	// Token: 0x0200006D RID: 109
	internal class SectionGroupInfo : ConfigInfo
	{
		// Token: 0x060003B3 RID: 947 RVA: 0x0000A790 File Offset: 0x00008990
		public SectionGroupInfo()
		{
			this.Type = typeof(ConfigurationSectionGroup);
		}

		// Token: 0x060003B4 RID: 948 RVA: 0x0000A7A8 File Offset: 0x000089A8
		public SectionGroupInfo(string groupName, string typeName)
		{
			this.Name = groupName;
			this.TypeName = typeName;
		}

		// Token: 0x060003B6 RID: 950 RVA: 0x0000A7CC File Offset: 0x000089CC
		public void AddChild(ConfigInfo data)
		{
			data.Parent = this;
			if (data is SectionInfo)
			{
				if (this.sections == null)
				{
					this.sections = new ConfigInfoCollection();
				}
				this.sections[data.Name] = data;
			}
			else
			{
				if (this.groups == null)
				{
					this.groups = new ConfigInfoCollection();
				}
				this.groups[data.Name] = data;
			}
		}

		// Token: 0x060003B7 RID: 951 RVA: 0x0000A840 File Offset: 0x00008A40
		public void Clear()
		{
			if (this.sections != null)
			{
				this.sections.Clear();
			}
			if (this.groups != null)
			{
				this.groups.Clear();
			}
		}

		// Token: 0x060003B8 RID: 952 RVA: 0x0000A87C File Offset: 0x00008A7C
		public bool HasChild(string name)
		{
			return (this.sections != null && this.sections[name] != null) || (this.groups != null && this.groups[name] != null);
		}

		// Token: 0x060003B9 RID: 953 RVA: 0x0000A8C8 File Offset: 0x00008AC8
		public void RemoveChild(string name)
		{
			if (this.sections != null)
			{
				this.sections.Remove(name);
			}
			if (this.groups != null)
			{
				this.groups.Remove(name);
			}
		}

		// Token: 0x060003BA RID: 954 RVA: 0x0000A904 File Offset: 0x00008B04
		public SectionInfo GetChildSection(string name)
		{
			if (this.sections != null)
			{
				return this.sections[name] as SectionInfo;
			}
			return null;
		}

		// Token: 0x060003BB RID: 955 RVA: 0x0000A924 File Offset: 0x00008B24
		public SectionGroupInfo GetChildGroup(string name)
		{
			if (this.groups != null)
			{
				return this.groups[name] as SectionGroupInfo;
			}
			return null;
		}

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x060003BC RID: 956 RVA: 0x0000A944 File Offset: 0x00008B44
		public ConfigInfoCollection Sections
		{
			get
			{
				if (this.sections == null)
				{
					return SectionGroupInfo.emptyList;
				}
				return this.sections;
			}
		}

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x060003BD RID: 957 RVA: 0x0000A960 File Offset: 0x00008B60
		public ConfigInfoCollection Groups
		{
			get
			{
				if (this.groups == null)
				{
					return SectionGroupInfo.emptyList;
				}
				return this.groups;
			}
		}

		// Token: 0x060003BE RID: 958 RVA: 0x0000A97C File Offset: 0x00008B7C
		public override bool HasDataContent(Configuration config)
		{
			foreach (ConfigInfoCollection configInfoCollection in new object[]
			{
				this.Sections,
				this.Groups
			})
			{
				foreach (object obj in configInfoCollection)
				{
					string name = (string)obj;
					ConfigInfo configInfo = configInfoCollection[name];
					if (configInfo.HasDataContent(config))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x060003BF RID: 959 RVA: 0x0000AA40 File Offset: 0x00008C40
		public override bool HasConfigContent(Configuration cfg)
		{
			if (base.StreamName == cfg.FileName)
			{
				return true;
			}
			foreach (ConfigInfoCollection configInfoCollection in new object[]
			{
				this.Sections,
				this.Groups
			})
			{
				foreach (object obj in configInfoCollection)
				{
					string name = (string)obj;
					ConfigInfo configInfo = configInfoCollection[name];
					if (configInfo.HasConfigContent(cfg))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x060003C0 RID: 960 RVA: 0x0000AB1C File Offset: 0x00008D1C
		public override void ReadConfig(Configuration cfg, string streamName, XmlReader reader)
		{
			base.StreamName = streamName;
			this.ConfigHost = cfg.ConfigHost;
			if (reader.LocalName != "configSections")
			{
				while (reader.MoveToNextAttribute())
				{
					if (reader.Name == "name")
					{
						this.Name = reader.Value;
					}
					else if (reader.Name == "type")
					{
						this.TypeName = reader.Value;
						this.Type = null;
					}
					else
					{
						base.ThrowException("Unrecognized attribute", reader);
					}
				}
				if (this.Name == null)
				{
					base.ThrowException("sectionGroup must have a 'name' attribute", reader);
				}
				if (this.Name == "location")
				{
					base.ThrowException("location is a reserved section name", reader);
				}
			}
			if (this.TypeName == null)
			{
				this.TypeName = "System.Configuration.ConfigurationSectionGroup";
			}
			if (reader.IsEmptyElement)
			{
				reader.Skip();
				return;
			}
			reader.ReadStartElement();
			reader.MoveToContent();
			while (reader.NodeType != XmlNodeType.EndElement)
			{
				if (reader.NodeType != XmlNodeType.Element)
				{
					reader.Skip();
				}
				else
				{
					string localName = reader.LocalName;
					ConfigInfo configInfo = null;
					if (localName == "remove")
					{
						this.ReadRemoveSection(reader);
					}
					else if (localName == "clear")
					{
						if (reader.HasAttributes)
						{
							base.ThrowException("Unrecognized attribute.", reader);
						}
						this.Clear();
						reader.Skip();
					}
					else
					{
						if (localName == "section")
						{
							configInfo = new SectionInfo();
						}
						else if (localName == "sectionGroup")
						{
							configInfo = new SectionGroupInfo();
						}
						else
						{
							base.ThrowException("Unrecognized element: " + reader.Name, reader);
						}
						configInfo.ReadConfig(cfg, streamName, reader);
						ConfigInfo configInfo2 = this.Groups[configInfo.Name];
						if (configInfo2 == null)
						{
							configInfo2 = this.Sections[configInfo.Name];
						}
						if (configInfo2 != null)
						{
							if (configInfo2.GetType() != configInfo.GetType())
							{
								base.ThrowException("A section or section group named '" + configInfo.Name + "' already exists", reader);
							}
							configInfo2.Merge(configInfo);
							configInfo2.StreamName = streamName;
						}
						else
						{
							this.AddChild(configInfo);
						}
					}
				}
			}
			reader.ReadEndElement();
		}

		// Token: 0x060003C1 RID: 961 RVA: 0x0000AD88 File Offset: 0x00008F88
		public override void WriteConfig(Configuration cfg, XmlWriter writer, ConfigurationSaveMode mode)
		{
			if (this.Name != null)
			{
				writer.WriteStartElement("sectionGroup");
				writer.WriteAttributeString("name", this.Name);
				if (this.TypeName != null && this.TypeName != string.Empty && this.TypeName != "System.Configuration.ConfigurationSectionGroup")
				{
					writer.WriteAttributeString("type", this.TypeName);
				}
			}
			else
			{
				writer.WriteStartElement("configSections");
			}
			foreach (ConfigInfoCollection configInfoCollection in new object[]
			{
				this.Sections,
				this.Groups
			})
			{
				foreach (object obj in configInfoCollection)
				{
					string name = (string)obj;
					ConfigInfo configInfo = configInfoCollection[name];
					if (configInfo.HasConfigContent(cfg))
					{
						configInfo.WriteConfig(cfg, writer, mode);
					}
				}
			}
			writer.WriteEndElement();
		}

		// Token: 0x060003C2 RID: 962 RVA: 0x0000AECC File Offset: 0x000090CC
		private void ReadRemoveSection(XmlReader reader)
		{
			if (!reader.MoveToNextAttribute() || reader.Name != "name")
			{
				base.ThrowException("Unrecognized attribute.", reader);
			}
			string value = reader.Value;
			if (string.IsNullOrEmpty(value))
			{
				base.ThrowException("Empty name to remove", reader);
			}
			reader.MoveToElement();
			if (!this.HasChild(value))
			{
				base.ThrowException("No factory for " + value, reader);
			}
			this.RemoveChild(value);
			reader.Skip();
		}

		// Token: 0x060003C3 RID: 963 RVA: 0x0000AF58 File Offset: 0x00009158
		public void ReadRootData(XmlReader reader, Configuration config, bool overrideAllowed)
		{
			reader.MoveToContent();
			this.ReadContent(reader, config, overrideAllowed, true);
		}

		// Token: 0x060003C4 RID: 964 RVA: 0x0000AF6C File Offset: 0x0000916C
		public override void ReadData(Configuration config, XmlReader reader, bool overrideAllowed)
		{
			reader.MoveToContent();
			if (!reader.IsEmptyElement)
			{
				reader.ReadStartElement();
				this.ReadContent(reader, config, overrideAllowed, false);
				reader.MoveToContent();
				reader.ReadEndElement();
			}
			else
			{
				reader.Read();
			}
		}

		// Token: 0x060003C5 RID: 965 RVA: 0x0000AFB4 File Offset: 0x000091B4
		private void ReadContent(XmlReader reader, Configuration config, bool overrideAllowed, bool root)
		{
			while (reader.NodeType != XmlNodeType.EndElement && reader.NodeType != XmlNodeType.None)
			{
				if (reader.NodeType != XmlNodeType.Element)
				{
					reader.Skip();
				}
				else if (reader.LocalName == "dllmap")
				{
					reader.Skip();
				}
				else if (reader.LocalName == "location")
				{
					if (!root)
					{
						base.ThrowException("<location> elements are only allowed in <configuration> elements.", reader);
					}
					string attribute = reader.GetAttribute("allowOverride");
					bool flag = attribute == null || attribute.Length == 0 || bool.Parse(attribute);
					string attribute2 = reader.GetAttribute("path");
					if (attribute2 != null && attribute2.Length > 0)
					{
						string xmlContent = reader.ReadOuterXml();
						string[] array = attribute2.Split(new char[]
						{
							','
						});
						foreach (string text in array)
						{
							string text2 = text.Trim();
							if (config.Locations.Find(text2) != null)
							{
								base.ThrowException("Sections must only appear once per config file.", reader);
							}
							ConfigurationLocation loc = new ConfigurationLocation(text2, xmlContent, config, flag);
							config.Locations.Add(loc);
						}
					}
					else
					{
						this.ReadData(config, reader, flag);
					}
				}
				else
				{
					ConfigInfo configInfo = this.GetConfigInfo(reader, this);
					if (configInfo != null)
					{
						configInfo.ReadData(config, reader, overrideAllowed);
					}
					else
					{
						base.ThrowException("Unrecognized configuration section <" + reader.LocalName + ">", reader);
					}
				}
			}
		}

		// Token: 0x060003C6 RID: 966 RVA: 0x0000B150 File Offset: 0x00009350
		private ConfigInfo GetConfigInfo(XmlReader reader, SectionGroupInfo current)
		{
			ConfigInfo configInfo = null;
			if (current.sections != null)
			{
				configInfo = current.sections[reader.LocalName];
			}
			if (configInfo != null)
			{
				return configInfo;
			}
			if (current.groups != null)
			{
				configInfo = current.groups[reader.LocalName];
			}
			if (configInfo != null)
			{
				return configInfo;
			}
			if (current.groups == null)
			{
				return null;
			}
			foreach (object obj in current.groups.AllKeys)
			{
				string name = (string)obj;
				configInfo = this.GetConfigInfo(reader, (SectionGroupInfo)current.groups[name]);
				if (configInfo != null)
				{
					return configInfo;
				}
			}
			return null;
		}

		// Token: 0x060003C7 RID: 967 RVA: 0x0000B244 File Offset: 0x00009444
		internal override void Merge(ConfigInfo newData)
		{
			SectionGroupInfo sectionGroupInfo = newData as SectionGroupInfo;
			if (sectionGroupInfo == null)
			{
				return;
			}
			if (sectionGroupInfo.sections != null && sectionGroupInfo.sections.Count > 0)
			{
				foreach (object obj in sectionGroupInfo.sections.AllKeys)
				{
					string name = (string)obj;
					ConfigInfo configInfo = this.sections[name];
					if (configInfo == null)
					{
						this.sections.Add(name, sectionGroupInfo.sections[name]);
					}
				}
			}
			if (sectionGroupInfo.groups != null && sectionGroupInfo.sections != null && sectionGroupInfo.sections.Count > 0)
			{
				foreach (object obj2 in sectionGroupInfo.groups.AllKeys)
				{
					string name2 = (string)obj2;
					ConfigInfo configInfo = this.groups[name2];
					if (configInfo == null)
					{
						this.groups.Add(name2, sectionGroupInfo.groups[name2]);
					}
				}
			}
		}

		// Token: 0x060003C8 RID: 968 RVA: 0x0000B3CC File Offset: 0x000095CC
		public void WriteRootData(XmlWriter writer, Configuration config, ConfigurationSaveMode mode)
		{
			this.WriteContent(writer, config, mode, false);
		}

		// Token: 0x060003C9 RID: 969 RVA: 0x0000B3D8 File Offset: 0x000095D8
		public override void WriteData(Configuration config, XmlWriter writer, ConfigurationSaveMode mode)
		{
			writer.WriteStartElement(this.Name);
			this.WriteContent(writer, config, mode, true);
			writer.WriteEndElement();
		}

		// Token: 0x060003CA RID: 970 RVA: 0x0000B3F8 File Offset: 0x000095F8
		public void WriteContent(XmlWriter writer, Configuration config, ConfigurationSaveMode mode, bool writeElem)
		{
			foreach (ConfigInfoCollection configInfoCollection in new object[]
			{
				this.Sections,
				this.Groups
			})
			{
				foreach (object obj in configInfoCollection)
				{
					string name = (string)obj;
					ConfigInfo configInfo = configInfoCollection[name];
					if (configInfo.HasDataContent(config))
					{
						configInfo.WriteData(config, writer, mode);
					}
				}
			}
		}

		// Token: 0x0400012A RID: 298
		private ConfigInfoCollection sections;

		// Token: 0x0400012B RID: 299
		private ConfigInfoCollection groups;

		// Token: 0x0400012C RID: 300
		private static ConfigInfoCollection emptyList = new ConfigInfoCollection();
	}
}
