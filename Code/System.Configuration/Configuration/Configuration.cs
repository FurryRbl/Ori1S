using System;
using System.Collections;
using System.Configuration.Internal;
using System.IO;
using System.Xml;

namespace System.Configuration
{
	/// <summary>Represents a configuration file applicable to a particular computer, application, or resource. This class cannot be inherited.</summary>
	// Token: 0x0200001C RID: 28
	public sealed class Configuration
	{
		// Token: 0x060000DC RID: 220 RVA: 0x00002E24 File Offset: 0x00001024
		internal Configuration(Configuration parent, string locationSubPath)
		{
			this.parent = parent;
			this.system = parent.system;
			this.rootGroup = parent.rootGroup;
			this.locationSubPath = locationSubPath;
			this.configPath = parent.ConfigPath;
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00002E74 File Offset: 0x00001074
		internal Configuration(InternalConfigurationSystem system, string locationSubPath)
		{
			this.hasFile = true;
			this.system = system;
			system.InitForConfiguration(ref locationSubPath, out this.configPath, out this.locationConfigPath);
			Configuration configuration = null;
			if (locationSubPath != null)
			{
				configuration = new Configuration(system, locationSubPath);
				if (this.locationConfigPath != null)
				{
					configuration = configuration.FindLocationConfiguration(this.locationConfigPath, configuration);
				}
			}
			this.Init(system, this.configPath, configuration);
		}

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x060000DE RID: 222 RVA: 0x00002EEC File Offset: 0x000010EC
		// (remove) Token: 0x060000DF RID: 223 RVA: 0x00002F04 File Offset: 0x00001104
		internal static event ConfigurationSaveEventHandler SaveStart;

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x060000E0 RID: 224 RVA: 0x00002F1C File Offset: 0x0000111C
		// (remove) Token: 0x060000E1 RID: 225 RVA: 0x00002F34 File Offset: 0x00001134
		internal static event ConfigurationSaveEventHandler SaveEnd;

		// Token: 0x060000E2 RID: 226 RVA: 0x00002F4C File Offset: 0x0000114C
		internal Configuration FindLocationConfiguration(string relativePath, Configuration defaultConfiguration)
		{
			Configuration configuration = defaultConfiguration;
			if (!string.IsNullOrEmpty(this.LocationConfigPath))
			{
				Configuration parentWithFile = this.GetParentWithFile();
				if (parentWithFile != null)
				{
					string configPathFromLocationSubPath = this.system.Host.GetConfigPathFromLocationSubPath(this.configPath, relativePath);
					configuration = parentWithFile.FindLocationConfiguration(configPathFromLocationSubPath, defaultConfiguration);
				}
			}
			string text = this.configPath.Substring(1) + "/";
			if (relativePath.StartsWith(text, StringComparison.Ordinal))
			{
				relativePath = relativePath.Substring(text.Length);
			}
			ConfigurationLocation configurationLocation = this.Locations.Find(relativePath);
			if (configurationLocation == null)
			{
				return configuration;
			}
			configurationLocation.SetParentConfiguration(configuration);
			return configurationLocation.OpenConfiguration();
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00002FF4 File Offset: 0x000011F4
		internal void Init(IConfigSystem system, string configPath, Configuration parent)
		{
			this.system = system;
			this.configPath = configPath;
			this.streamName = system.Host.GetStreamName(configPath);
			this.parent = parent;
			if (parent != null)
			{
				this.rootGroup = parent.rootGroup;
			}
			else
			{
				this.rootGroup = new SectionGroupInfo();
				this.rootGroup.StreamName = this.streamName;
			}
			if (this.streamName != null)
			{
				this.Load();
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000E4 RID: 228 RVA: 0x00003070 File Offset: 0x00001270
		// (set) Token: 0x060000E5 RID: 229 RVA: 0x00003078 File Offset: 0x00001278
		internal Configuration Parent
		{
			get
			{
				return this.parent;
			}
			set
			{
				this.parent = value;
			}
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x00003084 File Offset: 0x00001284
		internal Configuration GetParentWithFile()
		{
			Configuration configuration = this.Parent;
			while (configuration != null && !configuration.HasFile)
			{
				configuration = configuration.Parent;
			}
			return configuration;
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000E7 RID: 231 RVA: 0x000030B8 File Offset: 0x000012B8
		internal string FileName
		{
			get
			{
				return this.streamName;
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000E8 RID: 232 RVA: 0x000030C0 File Offset: 0x000012C0
		internal IInternalConfigHost ConfigHost
		{
			get
			{
				return this.system.Host;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000E9 RID: 233 RVA: 0x000030D0 File Offset: 0x000012D0
		internal string LocationConfigPath
		{
			get
			{
				return this.locationConfigPath;
			}
		}

		// Token: 0x060000EA RID: 234 RVA: 0x000030D8 File Offset: 0x000012D8
		internal string GetLocationSubPath()
		{
			Configuration configuration = this.parent;
			string text = null;
			while (configuration != null)
			{
				text = configuration.locationSubPath;
				if (!string.IsNullOrEmpty(text))
				{
					return text;
				}
				configuration = configuration.parent;
			}
			return text;
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000EB RID: 235 RVA: 0x00003118 File Offset: 0x00001318
		internal string ConfigPath
		{
			get
			{
				return this.configPath;
			}
		}

		/// <summary>Gets the <see cref="T:System.Configuration.AppSettingsSection" /> object configuration section that applies to this <see cref="T:System.Configuration.Configuration" /> object.</summary>
		/// <returns>An <see cref="T:System.Configuration.AppSettingsSection" /> object representing the appSettings configuration section that applies to this <see cref="T:System.Configuration.Configuration" /> object.</returns>
		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000EC RID: 236 RVA: 0x00003120 File Offset: 0x00001320
		public AppSettingsSection AppSettings
		{
			get
			{
				return (AppSettingsSection)this.GetSection("appSettings");
			}
		}

		/// <summary>Gets a <see cref="T:System.Configuration.ConnectionStringsSection" /> configuration-section object that applies to this <see cref="T:System.Configuration.Configuration" /> object.</summary>
		/// <returns>A <see cref="T:System.Configuration.ConnectionStringsSection" /> configuration-section object representing the connectionStrings configuration section that applies to this <see cref="T:System.Configuration.Configuration" /> object.</returns>
		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000ED RID: 237 RVA: 0x00003134 File Offset: 0x00001334
		public ConnectionStringsSection ConnectionStrings
		{
			get
			{
				return (ConnectionStringsSection)this.GetSection("connectionStrings");
			}
		}

		/// <summary>Gets the physical path to the configuration file represented by this <see cref="T:System.Configuration.Configuration" /> object.</summary>
		/// <returns>The physical path to the configuration file represented by this <see cref="T:System.Configuration.Configuration" /> object.</returns>
		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000EE RID: 238 RVA: 0x00003148 File Offset: 0x00001348
		public string FilePath
		{
			get
			{
				if (this.streamName == null && this.parent != null)
				{
					return this.parent.FilePath;
				}
				return this.streamName;
			}
		}

		/// <summary>Gets a value that indicates whether a file exists for the resource represented by this <see cref="T:System.Configuration.Configuration" /> object.</summary>
		/// <returns>true if there is a configuration file; otherwise, false.</returns>
		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000EF RID: 239 RVA: 0x00003180 File Offset: 0x00001380
		public bool HasFile
		{
			get
			{
				return this.hasFile;
			}
		}

		/// <summary>Gets the <see cref="T:System.Configuration.ContextInformation" /> object for the <see cref="T:System.Configuration.Configuration" /> object.</summary>
		/// <returns>The <see cref="T:System.Configuration.ContextInformation" /> object for the <see cref="T:System.Configuration.Configuration" /> object.</returns>
		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060000F0 RID: 240 RVA: 0x00003188 File Offset: 0x00001388
		public ContextInformation EvaluationContext
		{
			get
			{
				if (this.evaluationContext == null)
				{
					object ctx = this.system.Host.CreateConfigurationContext(this.configPath, this.GetLocationSubPath());
					this.evaluationContext = new ContextInformation(this, ctx);
				}
				return this.evaluationContext;
			}
		}

		/// <summary>Gets the locations defined within this <see cref="T:System.Configuration.Configuration" /> object.</summary>
		/// <returns>A <see cref="T:System.Configuration.ConfigurationLocationCollection" /> containing the locations defined within this <see cref="T:System.Configuration.Configuration" /> object.</returns>
		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060000F1 RID: 241 RVA: 0x000031D0 File Offset: 0x000013D0
		public ConfigurationLocationCollection Locations
		{
			get
			{
				if (this.locations == null)
				{
					this.locations = new ConfigurationLocationCollection();
				}
				return this.locations;
			}
		}

		/// <summary>Gets or sets a value indicating whether the configuration file has an XML namespace.</summary>
		/// <returns>true if the configuration file has an XML namespace; otherwise, false.</returns>
		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060000F2 RID: 242 RVA: 0x000031F0 File Offset: 0x000013F0
		// (set) Token: 0x060000F3 RID: 243 RVA: 0x00003200 File Offset: 0x00001400
		public bool NamespaceDeclared
		{
			get
			{
				return this.rootNamespace != null;
			}
			set
			{
				this.rootNamespace = ((!value) ? null : "http://schemas.microsoft.com/.NetConfiguration/v2.0");
			}
		}

		/// <summary>Gets the root <see cref="T:System.Configuration.ConfigurationSectionGroup" /> for this <see cref="T:System.Configuration.Configuration" /> object.</summary>
		/// <returns>The root section group for this <see cref="T:System.Configuration.Configuration" /> object.</returns>
		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060000F4 RID: 244 RVA: 0x0000321C File Offset: 0x0000141C
		public ConfigurationSectionGroup RootSectionGroup
		{
			get
			{
				if (this.rootSectionGroup == null)
				{
					this.rootSectionGroup = new ConfigurationSectionGroup();
					this.rootSectionGroup.Initialize(this, this.rootGroup);
				}
				return this.rootSectionGroup;
			}
		}

		/// <summary>Gets a collection of the section groups defined by this configuration.</summary>
		/// <returns>A <see cref="T:System.Configuration.ConfigurationSectionGroupCollection" /> collection representing the collection of section groups for this <see cref="T:System.Configuration.Configuration" /> object.</returns>
		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060000F5 RID: 245 RVA: 0x00003258 File Offset: 0x00001458
		public ConfigurationSectionGroupCollection SectionGroups
		{
			get
			{
				return this.RootSectionGroup.SectionGroups;
			}
		}

		/// <summary>Gets a collection of the sections defined by this <see cref="T:System.Configuration.Configuration" /> object.</summary>
		/// <returns>A collection of the sections defined by this <see cref="T:System.Configuration.Configuration" /> object.</returns>
		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060000F6 RID: 246 RVA: 0x00003268 File Offset: 0x00001468
		public ConfigurationSectionCollection Sections
		{
			get
			{
				return this.RootSectionGroup.Sections;
			}
		}

		/// <summary>Returns the specified <see cref="T:System.Configuration.ConfigurationSection" /> object.</summary>
		/// <returns>The specified <see cref="T:System.Configuration.ConfigurationSection" /> object.</returns>
		/// <param name="sectionName">The path to the section to be returned.</param>
		// Token: 0x060000F7 RID: 247 RVA: 0x00003278 File Offset: 0x00001478
		public ConfigurationSection GetSection(string path)
		{
			string[] array = path.Split(new char[]
			{
				'/'
			});
			if (array.Length == 1)
			{
				return this.Sections[array[0]];
			}
			ConfigurationSectionGroup configurationSectionGroup = this.SectionGroups[array[0]];
			int num = 1;
			while (configurationSectionGroup != null && num < array.Length - 1)
			{
				configurationSectionGroup = configurationSectionGroup.SectionGroups[array[num]];
				num++;
			}
			if (configurationSectionGroup != null)
			{
				return configurationSectionGroup.Sections[array[array.Length - 1]];
			}
			return null;
		}

		/// <summary>Gets the specified <see cref="T:System.Configuration.ConfigurationSectionGroup" /> object.</summary>
		/// <returns>The <see cref="T:System.Configuration.ConfigurationSectionGroup" /> specified.</returns>
		/// <param name="sectionGroupName">The path name of the <see cref="T:System.Configuration.ConfigurationSectionGroup" /> to return.</param>
		// Token: 0x060000F8 RID: 248 RVA: 0x00003304 File Offset: 0x00001504
		public ConfigurationSectionGroup GetSectionGroup(string path)
		{
			string[] array = path.Split(new char[]
			{
				'/'
			});
			ConfigurationSectionGroup configurationSectionGroup = this.SectionGroups[array[0]];
			int num = 1;
			while (configurationSectionGroup != null && num < array.Length)
			{
				configurationSectionGroup = configurationSectionGroup.SectionGroups[array[num]];
				num++;
			}
			return configurationSectionGroup;
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x0000335C File Offset: 0x0000155C
		internal ConfigurationSection GetSectionInstance(SectionInfo config, bool createDefaultInstance)
		{
			object obj = this.elementData[config];
			ConfigurationSection configurationSection = obj as ConfigurationSection;
			if (configurationSection != null || !createDefaultInstance)
			{
				return configurationSection;
			}
			object obj2 = config.CreateInstance();
			configurationSection = (obj2 as ConfigurationSection);
			if (configurationSection == null)
			{
				configurationSection = new DefaultSection
				{
					SectionHandler = (obj2 as IConfigurationSectionHandler)
				};
			}
			configurationSection.Configuration = this;
			ConfigurationSection configurationSection2 = null;
			if (this.parent != null)
			{
				configurationSection2 = this.parent.GetSectionInstance(config, true);
				configurationSection.SectionInformation.SetParentSection(configurationSection2);
			}
			configurationSection.SectionInformation.ConfigFilePath = this.FilePath;
			configurationSection.ConfigContext = this.system.Host.CreateDeprecatedConfigContext(this.configPath);
			string text = obj as string;
			configurationSection.RawXml = text;
			configurationSection.Reset(configurationSection2);
			if (text != null && text == obj)
			{
				XmlTextReader xmlTextReader = new ConfigXmlTextReader(new StringReader(text), this.FilePath);
				configurationSection.DeserializeSection(xmlTextReader);
				xmlTextReader.Close();
				if (!string.IsNullOrEmpty(configurationSection.SectionInformation.ConfigSource) && !string.IsNullOrEmpty(this.FilePath))
				{
					configurationSection.DeserializeConfigSource(Path.GetDirectoryName(this.FilePath));
				}
			}
			this.elementData[config] = configurationSection;
			return configurationSection;
		}

		// Token: 0x060000FA RID: 250 RVA: 0x000034A0 File Offset: 0x000016A0
		internal ConfigurationSectionGroup GetSectionGroupInstance(SectionGroupInfo group)
		{
			ConfigurationSectionGroup configurationSectionGroup = group.CreateInstance() as ConfigurationSectionGroup;
			if (configurationSectionGroup != null)
			{
				configurationSectionGroup.Initialize(this, group);
			}
			return configurationSectionGroup;
		}

		// Token: 0x060000FB RID: 251 RVA: 0x000034C8 File Offset: 0x000016C8
		internal void SetConfigurationSection(SectionInfo config, ConfigurationSection sec)
		{
			this.elementData[config] = sec;
		}

		// Token: 0x060000FC RID: 252 RVA: 0x000034D8 File Offset: 0x000016D8
		internal void SetSectionXml(SectionInfo config, string data)
		{
			this.elementData[config] = data;
		}

		// Token: 0x060000FD RID: 253 RVA: 0x000034E8 File Offset: 0x000016E8
		internal string GetSectionXml(SectionInfo config)
		{
			return this.elementData[config] as string;
		}

		// Token: 0x060000FE RID: 254 RVA: 0x000034FC File Offset: 0x000016FC
		internal void CreateSection(SectionGroupInfo group, string name, ConfigurationSection sec)
		{
			if (group.HasChild(name))
			{
				throw new ConfigurationException("Cannot add a ConfigurationSection. A section or section group already exists with the name '" + name + "'");
			}
			if (!this.HasFile && !sec.SectionInformation.AllowLocation)
			{
				throw new ConfigurationErrorsException("The configuration section <" + name + "> cannot be defined inside a <location> element.");
			}
			if (!this.system.Host.IsDefinitionAllowed(this.configPath, sec.SectionInformation.AllowDefinition, sec.SectionInformation.AllowExeDefinition))
			{
				object obj = (sec.SectionInformation.AllowExeDefinition == ConfigurationAllowExeDefinition.MachineToApplication) ? sec.SectionInformation.AllowDefinition : sec.SectionInformation.AllowExeDefinition;
				throw new ConfigurationErrorsException(string.Concat(new object[]
				{
					"The section <",
					name,
					"> can't be defined in this configuration file (the allowed definition context is '",
					obj,
					"')."
				}));
			}
			if (sec.SectionInformation.Type == null)
			{
				sec.SectionInformation.Type = this.system.Host.GetConfigTypeName(sec.GetType());
			}
			SectionInfo sectionInfo = new SectionInfo(name, sec.SectionInformation);
			sectionInfo.StreamName = this.streamName;
			sectionInfo.ConfigHost = this.system.Host;
			group.AddChild(sectionInfo);
			this.elementData[sectionInfo] = sec;
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00003668 File Offset: 0x00001868
		internal void CreateSectionGroup(SectionGroupInfo parentGroup, string name, ConfigurationSectionGroup sec)
		{
			if (parentGroup.HasChild(name))
			{
				throw new ConfigurationException("Cannot add a ConfigurationSectionGroup. A section or section group already exists with the name '" + name + "'");
			}
			if (sec.Type == null)
			{
				sec.Type = this.system.Host.GetConfigTypeName(sec.GetType());
			}
			sec.SetName(name);
			SectionGroupInfo sectionGroupInfo = new SectionGroupInfo(name, sec.Type);
			sectionGroupInfo.StreamName = this.streamName;
			sectionGroupInfo.ConfigHost = this.system.Host;
			parentGroup.AddChild(sectionGroupInfo);
			this.elementData[sectionGroupInfo] = sec;
			sec.Initialize(this, sectionGroupInfo);
		}

		// Token: 0x06000100 RID: 256 RVA: 0x0000370C File Offset: 0x0000190C
		internal void RemoveConfigInfo(ConfigInfo config)
		{
			this.elementData.Remove(config);
		}

		/// <summary>Writes the configuration settings contained within this <see cref="T:System.Configuration.Configuration" /> object to the current XML configuration file.</summary>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">The configuration file could not be written to.- or -The configuration file has changed. </exception>
		// Token: 0x06000101 RID: 257 RVA: 0x0000371C File Offset: 0x0000191C
		public void Save()
		{
			this.Save(ConfigurationSaveMode.Modified, false);
		}

		/// <summary>Writes the configuration settings contained within this <see cref="T:System.Configuration.Configuration" /> object to the current XML configuration file.</summary>
		/// <param name="saveMode">A <see cref="T:System.Configuration.ConfigurationSaveMode" /> value that determines which property values to save.</param>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">The configuration file could not be written to.- or -The configuration file has changed. </exception>
		// Token: 0x06000102 RID: 258 RVA: 0x00003728 File Offset: 0x00001928
		public void Save(ConfigurationSaveMode mode)
		{
			this.Save(mode, false);
		}

		/// <summary>Writes the configuration settings contained within this <see cref="T:System.Configuration.Configuration" /> object to the current XML configuration file.</summary>
		/// <param name="saveMode">A <see cref="T:System.Configuration.ConfigurationSaveMode" /> value that determines which property values to save.</param>
		/// <param name="forceSaveAll">true to save even if the configuration was not modified; otherwise, false.</param>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">The configuration file could not be written to.- or -The configuration file has changed. </exception>
		// Token: 0x06000103 RID: 259 RVA: 0x00003734 File Offset: 0x00001934
		public void Save(ConfigurationSaveMode mode, bool forceUpdateAll)
		{
			ConfigurationSaveEventHandler saveStart = Configuration.SaveStart;
			ConfigurationSaveEventHandler saveEnd = Configuration.SaveEnd;
			object obj = null;
			Exception ex = null;
			Stream stream = this.system.Host.OpenStreamForWrite(this.streamName, null, ref obj);
			try
			{
				if (saveStart != null)
				{
					saveStart(this, new ConfigurationSaveEventArgs(this.streamName, true, null, obj));
				}
				this.Save(stream, mode, forceUpdateAll);
				this.system.Host.WriteCompleted(this.streamName, true, obj);
			}
			catch (Exception ex2)
			{
				ex = ex2;
				this.system.Host.WriteCompleted(this.streamName, false, obj);
				throw;
			}
			finally
			{
				stream.Close();
				if (saveEnd != null)
				{
					saveEnd(this, new ConfigurationSaveEventArgs(this.streamName, false, ex, obj));
				}
			}
		}

		/// <summary>Writes the configuration settings contained within this <see cref="T:System.Configuration.Configuration" /> object to the specified XML configuration file.</summary>
		/// <param name="filename">The path and file name to save the configuration file to.</param>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">The configuration file could not be written to.- or -The configuration file has changed. </exception>
		// Token: 0x06000104 RID: 260 RVA: 0x0000382C File Offset: 0x00001A2C
		public void SaveAs(string filename)
		{
			this.SaveAs(filename, ConfigurationSaveMode.Modified, false);
		}

		/// <summary>Writes the configuration settings contained within this <see cref="T:System.Configuration.Configuration" /> object to the specified XML configuration file.</summary>
		/// <param name="filename">The path and file name to save the configuration file to.</param>
		/// <param name="saveMode">A <see cref="T:System.Configuration.ConfigurationSaveMode" /> value that determines which property values to save.</param>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">The configuration file could not be written to.- or -The configuration file has changed. </exception>
		// Token: 0x06000105 RID: 261 RVA: 0x00003838 File Offset: 0x00001A38
		public void SaveAs(string filename, ConfigurationSaveMode mode)
		{
			this.SaveAs(filename, mode, false);
		}

		/// <summary>Writes the configuration settings contained within this <see cref="T:System.Configuration.Configuration" /> object to the specified XML configuration file.</summary>
		/// <param name="filename">The path and file name to save the configuration file to.</param>
		/// <param name="saveMode">A <see cref="T:System.Configuration.ConfigurationSaveMode" /> value that determines which property values to save.</param>
		/// <param name="forceSaveAll">true to save even if the configuration was not modified; otherwise, false.</param>
		/// <exception cref="T:System.ArgumentException">
		///   <paramref name="filename" /> is null or an empty string ("").</exception>
		// Token: 0x06000106 RID: 262 RVA: 0x00003844 File Offset: 0x00001A44
		[MonoInternalNote("Detect if file has changed")]
		public void SaveAs(string filename, ConfigurationSaveMode mode, bool forceUpdateAll)
		{
			string directoryName = Path.GetDirectoryName(Path.GetFullPath(filename));
			if (!Directory.Exists(directoryName))
			{
				Directory.CreateDirectory(directoryName);
			}
			this.Save(new FileStream(filename, FileMode.OpenOrCreate, FileAccess.Write), mode, forceUpdateAll);
		}

		// Token: 0x06000107 RID: 263 RVA: 0x00003880 File Offset: 0x00001A80
		private void Save(Stream stream, ConfigurationSaveMode mode, bool forceUpdateAll)
		{
			XmlTextWriter xmlTextWriter = new XmlTextWriter(new StreamWriter(stream));
			xmlTextWriter.Formatting = Formatting.Indented;
			try
			{
				xmlTextWriter.WriteStartDocument();
				if (this.rootNamespace != null)
				{
					xmlTextWriter.WriteStartElement("configuration", this.rootNamespace);
				}
				else
				{
					xmlTextWriter.WriteStartElement("configuration");
				}
				if (this.rootGroup.HasConfigContent(this))
				{
					this.rootGroup.WriteConfig(this, xmlTextWriter, mode);
				}
				foreach (object obj in this.Locations)
				{
					ConfigurationLocation configurationLocation = (ConfigurationLocation)obj;
					if (configurationLocation.OpenedConfiguration == null)
					{
						xmlTextWriter.WriteRaw("\n");
						xmlTextWriter.WriteRaw(configurationLocation.XmlContent);
					}
					else
					{
						xmlTextWriter.WriteStartElement("location");
						xmlTextWriter.WriteAttributeString("path", configurationLocation.Path);
						if (!configurationLocation.AllowOverride)
						{
							xmlTextWriter.WriteAttributeString("allowOverride", "false");
						}
						configurationLocation.OpenedConfiguration.SaveData(xmlTextWriter, mode, forceUpdateAll);
						xmlTextWriter.WriteEndElement();
					}
				}
				this.SaveData(xmlTextWriter, mode, forceUpdateAll);
				xmlTextWriter.WriteEndElement();
			}
			finally
			{
				xmlTextWriter.Flush();
				xmlTextWriter.Close();
			}
		}

		// Token: 0x06000108 RID: 264 RVA: 0x000039F8 File Offset: 0x00001BF8
		private void SaveData(XmlTextWriter tw, ConfigurationSaveMode mode, bool forceUpdateAll)
		{
			this.rootGroup.WriteRootData(tw, this, mode);
		}

		// Token: 0x06000109 RID: 265 RVA: 0x00003A08 File Offset: 0x00001C08
		private bool Load()
		{
			if (string.IsNullOrEmpty(this.streamName))
			{
				return true;
			}
			Stream s = null;
			try
			{
				s = (s = this.system.Host.OpenStreamForRead(this.streamName));
			}
			catch
			{
				return false;
			}
			using (XmlTextReader xmlTextReader = new ConfigXmlTextReader(s, this.streamName))
			{
				this.ReadConfigFile(xmlTextReader, this.streamName);
			}
			return true;
		}

		// Token: 0x0600010A RID: 266 RVA: 0x00003AB8 File Offset: 0x00001CB8
		private void ReadConfigFile(XmlReader reader, string fileName)
		{
			reader.MoveToContent();
			if (reader.NodeType != XmlNodeType.Element || reader.Name != "configuration")
			{
				this.ThrowException("Configuration file does not have a valid root element", reader);
			}
			if (reader.HasAttributes)
			{
				while (reader.MoveToNextAttribute())
				{
					if (reader.LocalName == "xmlns")
					{
						this.rootNamespace = reader.Value;
					}
					else
					{
						this.ThrowException(string.Format("Unrecognized attribute '{0}' in root element", reader.LocalName), reader);
					}
				}
			}
			reader.MoveToElement();
			if (reader.IsEmptyElement)
			{
				reader.Skip();
				return;
			}
			reader.ReadStartElement();
			reader.MoveToContent();
			if (reader.LocalName == "configSections")
			{
				if (reader.HasAttributes)
				{
					this.ThrowException("Unrecognized attribute in <configSections>.", reader);
				}
				this.rootGroup.ReadConfig(this, fileName, reader);
			}
			this.rootGroup.ReadRootData(reader, this, true);
		}

		// Token: 0x0600010B RID: 267 RVA: 0x00003BC0 File Offset: 0x00001DC0
		internal void ReadData(XmlReader reader, bool allowOverride)
		{
			this.rootGroup.ReadData(this, reader, allowOverride);
		}

		// Token: 0x0600010C RID: 268 RVA: 0x00003BD0 File Offset: 0x00001DD0
		private void ThrowException(string text, XmlReader reader)
		{
			IXmlLineInfo xmlLineInfo = reader as IXmlLineInfo;
			throw new ConfigurationException(text, this.streamName, (xmlLineInfo == null) ? 0 : xmlLineInfo.LineNumber);
		}

		// Token: 0x04000038 RID: 56
		private Configuration parent;

		// Token: 0x04000039 RID: 57
		private Hashtable elementData = new Hashtable();

		// Token: 0x0400003A RID: 58
		private string streamName;

		// Token: 0x0400003B RID: 59
		private ConfigurationSectionGroup rootSectionGroup;

		// Token: 0x0400003C RID: 60
		private ConfigurationLocationCollection locations;

		// Token: 0x0400003D RID: 61
		private SectionGroupInfo rootGroup;

		// Token: 0x0400003E RID: 62
		private IConfigSystem system;

		// Token: 0x0400003F RID: 63
		private bool hasFile;

		// Token: 0x04000040 RID: 64
		private string rootNamespace;

		// Token: 0x04000041 RID: 65
		private string configPath;

		// Token: 0x04000042 RID: 66
		private string locationConfigPath;

		// Token: 0x04000043 RID: 67
		private string locationSubPath;

		// Token: 0x04000044 RID: 68
		private ContextInformation evaluationContext;
	}
}
