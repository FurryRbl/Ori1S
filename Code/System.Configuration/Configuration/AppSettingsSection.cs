using System;
using System.ComponentModel;
using System.IO;
using System.Xml;

namespace System.Configuration
{
	/// <summary>Provides configuration system support for the appSettings configuration section. This class cannot be inherited.</summary>
	// Token: 0x02000014 RID: 20
	public sealed class AppSettingsSection : ConfigurationSection
	{
		// Token: 0x060000A2 RID: 162 RVA: 0x000027C4 File Offset: 0x000009C4
		static AppSettingsSection()
		{
			AppSettingsSection._properties = new ConfigurationPropertyCollection();
			AppSettingsSection._properties.Add(AppSettingsSection._propFile);
			AppSettingsSection._properties.Add(AppSettingsSection._propSettings);
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x0000283C File Offset: 0x00000A3C
		protected internal override bool IsModified()
		{
			return this.Settings.IsModified();
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x0000284C File Offset: 0x00000A4C
		[MonoInternalNote("file path?  do we use a System.Configuration api for opening it?  do we keep it open?  do we open it writable?")]
		protected internal override void DeserializeElement(XmlReader reader, bool serializeCollectionKey)
		{
			base.DeserializeElement(reader, serializeCollectionKey);
			if (this.File != string.Empty)
			{
				try
				{
					Stream stream = System.IO.File.OpenRead(this.File);
					XmlReader reader2 = new ConfigXmlTextReader(stream, this.File);
					base.DeserializeElement(reader2, serializeCollectionKey);
					stream.Close();
				}
				catch
				{
				}
			}
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x000028C4 File Offset: 0x00000AC4
		protected internal override void Reset(ConfigurationElement parentSection)
		{
			AppSettingsSection appSettingsSection = parentSection as AppSettingsSection;
			if (appSettingsSection != null)
			{
				this.Settings.Reset(appSettingsSection.Settings);
			}
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x000028F0 File Offset: 0x00000AF0
		[MonoTODO]
		protected internal override string SerializeSection(ConfigurationElement parent, string name, ConfigurationSaveMode mode)
		{
			if (this.File == string.Empty)
			{
				return base.SerializeSection(parent, name, mode);
			}
			throw new NotImplementedException();
		}

		/// <summary>Gets or sets a configuration file that provides additional settings or overrides the settings specified in the appSettings element.</summary>
		/// <returns>A configuration file that provides additional settings or overrides the settings specified in the appSettings element.</returns>
		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000A7 RID: 167 RVA: 0x00002924 File Offset: 0x00000B24
		// (set) Token: 0x060000A8 RID: 168 RVA: 0x00002938 File Offset: 0x00000B38
		[ConfigurationProperty("file", DefaultValue = "")]
		public string File
		{
			get
			{
				return (string)base[AppSettingsSection._propFile];
			}
			set
			{
				base[AppSettingsSection._propFile] = value;
			}
		}

		/// <summary>Gets a <see cref="T:System.Configuration.KeyValueConfigurationCollection" /> object of key/value pairs that contain application settings.</summary>
		/// <returns>A <see cref="T:System.Collections.Specialized.NameValueCollection" /> that contains the application settings from the configuration file.</returns>
		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000A9 RID: 169 RVA: 0x00002948 File Offset: 0x00000B48
		[ConfigurationProperty("", Options = ConfigurationPropertyOptions.IsDefaultCollection)]
		public KeyValueConfigurationCollection Settings
		{
			get
			{
				return (KeyValueConfigurationCollection)base[AppSettingsSection._propSettings];
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000AA RID: 170 RVA: 0x0000295C File Offset: 0x00000B5C
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return AppSettingsSection._properties;
			}
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00002964 File Offset: 0x00000B64
		protected internal override object GetRuntimeObject()
		{
			KeyValueInternalCollection keyValueInternalCollection = new KeyValueInternalCollection();
			foreach (string key in this.Settings.AllKeys)
			{
				KeyValueConfigurationElement keyValueConfigurationElement = this.Settings[key];
				keyValueInternalCollection.Add(keyValueConfigurationElement.Key, keyValueConfigurationElement.Value);
			}
			if (!ConfigurationManager.ConfigurationSystem.SupportsUserConfig)
			{
				keyValueInternalCollection.SetReadOnly();
			}
			return keyValueInternalCollection;
		}

		// Token: 0x04000026 RID: 38
		private static ConfigurationPropertyCollection _properties;

		// Token: 0x04000027 RID: 39
		private static readonly ConfigurationProperty _propFile = new ConfigurationProperty("file", typeof(string), string.Empty, new StringConverter(), null, ConfigurationPropertyOptions.None);

		// Token: 0x04000028 RID: 40
		private static readonly ConfigurationProperty _propSettings = new ConfigurationProperty(string.Empty, typeof(KeyValueConfigurationCollection), null, null, null, ConfigurationPropertyOptions.IsDefaultCollection);
	}
}
