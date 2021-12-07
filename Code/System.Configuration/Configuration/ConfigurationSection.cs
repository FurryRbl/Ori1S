using System;
using System.IO;
using System.Xml;

namespace System.Configuration
{
	/// <summary>Represents a section within a configuration file.</summary>
	// Token: 0x02000036 RID: 54
	public abstract class ConfigurationSection : ConfigurationElement
	{
		// Token: 0x17000097 RID: 151
		// (get) Token: 0x06000213 RID: 531 RVA: 0x0000714C File Offset: 0x0000534C
		internal string ExternalDataXml
		{
			get
			{
				return this.externalDataXml;
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x06000214 RID: 532 RVA: 0x00007154 File Offset: 0x00005354
		// (set) Token: 0x06000215 RID: 533 RVA: 0x0000715C File Offset: 0x0000535C
		internal IConfigurationSectionHandler SectionHandler
		{
			get
			{
				return this.section_handler;
			}
			set
			{
				this.section_handler = value;
			}
		}

		/// <summary>Gets a <see cref="T:System.Configuration.SectionInformation" /> object that contains the non-customizable information and functionality of the <see cref="T:System.Configuration.ConfigurationSection" /> object. </summary>
		/// <returns>A <see cref="T:System.Configuration.SectionInformation" /> that contains the non-customizable information and functionality of the <see cref="T:System.Configuration.ConfigurationSection" />.</returns>
		// Token: 0x17000099 RID: 153
		// (get) Token: 0x06000216 RID: 534 RVA: 0x00007168 File Offset: 0x00005368
		[MonoTODO]
		public SectionInformation SectionInformation
		{
			get
			{
				if (this.sectionInformation == null)
				{
					this.sectionInformation = new SectionInformation();
				}
				return this.sectionInformation;
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x06000217 RID: 535 RVA: 0x00007188 File Offset: 0x00005388
		// (set) Token: 0x06000218 RID: 536 RVA: 0x00007190 File Offset: 0x00005390
		internal object ConfigContext
		{
			get
			{
				return this._configContext;
			}
			set
			{
				this._configContext = value;
			}
		}

		/// <summary>Returns a custom object when overridden in a derived class.</summary>
		/// <returns>The object representing the section.</returns>
		// Token: 0x06000219 RID: 537 RVA: 0x0000719C File Offset: 0x0000539C
		[MonoTODO("Provide ConfigContext. Likely the culprit of bug #322493")]
		protected internal virtual object GetRuntimeObject()
		{
			if (this.SectionHandler == null)
			{
				return this;
			}
			ConfigurationSection configurationSection = (this.sectionInformation == null) ? null : this.sectionInformation.GetParentSection();
			object obj = (configurationSection == null) ? null : configurationSection.GetRuntimeObject();
			if (base.RawXml == null)
			{
				return obj;
			}
			try
			{
				XmlReader reader = new ConfigXmlTextReader(new StringReader(base.RawXml), base.Configuration.FilePath);
				this.DoDeserializeSection(reader);
				if (!string.IsNullOrEmpty(this.SectionInformation.ConfigSource))
				{
					string text = this.SectionInformation.ConfigFilePath;
					if (!string.IsNullOrEmpty(text))
					{
						text = Path.GetDirectoryName(text);
					}
					else
					{
						text = string.Empty;
					}
					string path = Path.Combine(text, this.SectionInformation.ConfigSource);
					if (File.Exists(path))
					{
						base.RawXml = File.ReadAllText(path);
						this.SectionInformation.SetRawXml(base.RawXml);
					}
				}
			}
			catch
			{
			}
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(base.RawXml);
			return this.SectionHandler.Create(obj, this.ConfigContext, xmlDocument.DocumentElement);
		}

		/// <summary>Indicates whether this configuration element has been modified since it was last saved or loaded when implemented in a derived class.</summary>
		/// <returns>true if the element has been modified; otherwise, false. </returns>
		// Token: 0x0600021A RID: 538 RVA: 0x000072E8 File Offset: 0x000054E8
		[MonoTODO]
		protected internal override bool IsModified()
		{
			return base.IsModified();
		}

		/// <summary>Resets the value of the <see cref="M:System.Configuration.ConfigurationElement.IsModified" /> method to false when implemented in a derived class.</summary>
		// Token: 0x0600021B RID: 539 RVA: 0x000072F0 File Offset: 0x000054F0
		[MonoTODO]
		protected internal override void ResetModified()
		{
			base.ResetModified();
		}

		// Token: 0x0600021C RID: 540 RVA: 0x000072F8 File Offset: 0x000054F8
		private ConfigurationElement CreateElement(Type t)
		{
			ConfigurationElement configurationElement = (ConfigurationElement)Activator.CreateInstance(t);
			configurationElement.Init();
			if (this.IsReadOnly())
			{
				configurationElement.SetReadOnly();
			}
			return configurationElement;
		}

		// Token: 0x0600021D RID: 541 RVA: 0x0000732C File Offset: 0x0000552C
		private void DoDeserializeSection(XmlReader reader)
		{
			reader.MoveToContent();
			string text = null;
			string text2 = null;
			while (reader.MoveToNextAttribute())
			{
				string localName = reader.LocalName;
				if (localName == "configProtectionProvider")
				{
					text = reader.Value;
				}
				else if (localName == "configSource")
				{
					text2 = reader.Value;
				}
			}
			if (text != null)
			{
				ProtectedConfigurationProvider provider = ProtectedConfiguration.GetProvider(text, true);
				XmlDocument xmlDocument = new XmlDocument();
				reader.MoveToElement();
				xmlDocument.Load(new StringReader(reader.ReadInnerXml()));
				XmlNode node = provider.Decrypt(xmlDocument);
				reader = new XmlNodeReader(node);
				this.SectionInformation.ProtectSection(text);
				reader.MoveToContent();
			}
			if (text2 != null)
			{
				this.SectionInformation.ConfigSource = text2;
			}
			this.SectionInformation.SetRawXml(base.RawXml);
			this.DeserializeElement(reader, false);
		}

		/// <summary>Reads XML from the configuration file.</summary>
		/// <param name="reader">The <see cref="T:System.Xml.XmlReader" /> object, which reads from the configuration file. </param>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">
		///   <paramref name="reader" /> found no elements in the configuration file.</exception>
		// Token: 0x0600021E RID: 542 RVA: 0x00007410 File Offset: 0x00005610
		[MonoInternalNote("find the proper location for the decryption stuff")]
		protected internal virtual void DeserializeSection(XmlReader reader)
		{
			this.DoDeserializeSection(reader);
		}

		// Token: 0x0600021F RID: 543 RVA: 0x0000741C File Offset: 0x0000561C
		internal void DeserializeConfigSource(string basePath)
		{
			string configSource = this.SectionInformation.ConfigSource;
			if (string.IsNullOrEmpty(configSource))
			{
				return;
			}
			if (Path.IsPathRooted(configSource))
			{
				throw new ConfigurationException("The configSource attribute must be a relative physical path.");
			}
			if (this.HasLocalModifications())
			{
				throw new ConfigurationException("A section using 'configSource' may contain no other attributes or elements.");
			}
			string text = Path.Combine(basePath, configSource);
			if (!File.Exists(text))
			{
				base.RawXml = null;
				this.SectionInformation.SetRawXml(null);
				return;
			}
			base.RawXml = File.ReadAllText(text);
			this.SectionInformation.SetRawXml(base.RawXml);
			this.DeserializeElement(new ConfigXmlTextReader(new StringReader(base.RawXml), text), false);
		}

		/// <summary>Creates an XML string containing an unmerged view of the <see cref="T:System.Configuration.ConfigurationSection" /> object as a single section to write to a file.</summary>
		/// <returns>An XML string containing an unmerged view of the <see cref="T:System.Configuration.ConfigurationSection" /> object.</returns>
		/// <param name="parentElement">The <see cref="T:System.Configuration.ConfigurationElement" /> instance to use as the parent when performing the un-merge.</param>
		/// <param name="name">The name of the section to create.</param>
		/// <param name="saveMode">The <see cref="T:System.Configuration.ConfigurationSaveMode" /> instance to use when writing to a string.</param>
		// Token: 0x06000220 RID: 544 RVA: 0x000074CC File Offset: 0x000056CC
		protected internal virtual string SerializeSection(ConfigurationElement parentElement, string name, ConfigurationSaveMode saveMode)
		{
			this.externalDataXml = null;
			ConfigurationElement configurationElement;
			if (parentElement != null)
			{
				configurationElement = this.CreateElement(base.GetType());
				configurationElement.Unmerge(this, parentElement, saveMode);
			}
			else
			{
				configurationElement = this;
			}
			string result;
			using (StringWriter stringWriter = new StringWriter())
			{
				using (XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter))
				{
					xmlTextWriter.Formatting = Formatting.Indented;
					configurationElement.SerializeToXmlElement(xmlTextWriter, name);
					xmlTextWriter.Close();
				}
				result = stringWriter.ToString();
			}
			string configSource = this.SectionInformation.ConfigSource;
			if (string.IsNullOrEmpty(configSource))
			{
				return result;
			}
			this.externalDataXml = result;
			string result2;
			using (StringWriter stringWriter2 = new StringWriter())
			{
				bool flag = !string.IsNullOrEmpty(name);
				using (XmlTextWriter xmlTextWriter2 = new XmlTextWriter(stringWriter2))
				{
					if (flag)
					{
						xmlTextWriter2.WriteStartElement(name);
					}
					xmlTextWriter2.WriteAttributeString("configSource", configSource);
					if (flag)
					{
						xmlTextWriter2.WriteEndElement();
					}
				}
				result2 = stringWriter2.ToString();
			}
			return result2;
		}

		// Token: 0x040000AE RID: 174
		private SectionInformation sectionInformation;

		// Token: 0x040000AF RID: 175
		private IConfigurationSectionHandler section_handler;

		// Token: 0x040000B0 RID: 176
		private string externalDataXml;

		// Token: 0x040000B1 RID: 177
		private object _configContext;
	}
}
