using System;
using System.IO;
using System.Text;
using System.Xml;

namespace System.Configuration
{
	// Token: 0x0200006C RID: 108
	internal class SectionInfo : ConfigInfo
	{
		// Token: 0x060003A9 RID: 937 RVA: 0x0000A000 File Offset: 0x00008200
		public SectionInfo()
		{
		}

		// Token: 0x060003AA RID: 938 RVA: 0x0000A02C File Offset: 0x0000822C
		public SectionInfo(string sectionName, SectionInformation info)
		{
			this.Name = sectionName;
			this.TypeName = info.Type;
			this.allowLocation = info.AllowLocation;
			this.allowDefinition = info.AllowDefinition;
			this.allowExeDefinition = info.AllowExeDefinition;
			this.requirePermission = info.RequirePermission;
			this.restartOnExternalChanges = info.RestartOnExternalChanges;
		}

		// Token: 0x060003AB RID: 939 RVA: 0x0000A0B0 File Offset: 0x000082B0
		public override object CreateInstance()
		{
			object obj = base.CreateInstance();
			ConfigurationSection configurationSection = obj as ConfigurationSection;
			if (configurationSection != null)
			{
				configurationSection.SectionInformation.AllowLocation = this.allowLocation;
				configurationSection.SectionInformation.AllowDefinition = this.allowDefinition;
				configurationSection.SectionInformation.AllowExeDefinition = this.allowExeDefinition;
				configurationSection.SectionInformation.RequirePermission = this.requirePermission;
				configurationSection.SectionInformation.RestartOnExternalChanges = this.restartOnExternalChanges;
				configurationSection.SectionInformation.SetName(this.Name);
			}
			return obj;
		}

		// Token: 0x060003AC RID: 940 RVA: 0x0000A138 File Offset: 0x00008338
		public override bool HasDataContent(Configuration config)
		{
			return config.GetSectionInstance(this, false) != null || config.GetSectionXml(this) != null;
		}

		// Token: 0x060003AD RID: 941 RVA: 0x0000A158 File Offset: 0x00008358
		public override bool HasConfigContent(Configuration cfg)
		{
			return base.StreamName == cfg.FileName;
		}

		// Token: 0x060003AE RID: 942 RVA: 0x0000A16C File Offset: 0x0000836C
		public override void ReadConfig(Configuration cfg, string streamName, XmlReader reader)
		{
			base.StreamName = streamName;
			this.ConfigHost = cfg.ConfigHost;
			while (reader.MoveToNextAttribute())
			{
				string name = reader.Name;
				switch (name)
				{
				case "allowLocation":
				{
					string value = reader.Value;
					this.allowLocation = (value == "true");
					if (!this.allowLocation && value != "false")
					{
						base.ThrowException("Invalid attribute value", reader);
					}
					continue;
				}
				case "allowDefinition":
				{
					string value2 = reader.Value;
					try
					{
						this.allowDefinition = (ConfigurationAllowDefinition)((int)Enum.Parse(typeof(ConfigurationAllowDefinition), value2));
					}
					catch
					{
						base.ThrowException("Invalid attribute value", reader);
					}
					continue;
				}
				case "allowExeDefinition":
				{
					string value3 = reader.Value;
					try
					{
						this.allowExeDefinition = (ConfigurationAllowExeDefinition)((int)Enum.Parse(typeof(ConfigurationAllowExeDefinition), value3));
					}
					catch
					{
						base.ThrowException("Invalid attribute value", reader);
					}
					continue;
				}
				case "type":
					this.TypeName = reader.Value;
					continue;
				case "name":
					this.Name = reader.Value;
					if (this.Name == "location")
					{
						base.ThrowException("location is a reserved section name", reader);
					}
					continue;
				case "requirePermission":
				{
					string value4 = reader.Value;
					bool flag = value4 == "true";
					if (!flag && value4 != "false")
					{
						base.ThrowException("Invalid attribute value", reader);
					}
					this.requirePermission = flag;
					continue;
				}
				case "restartOnExternalChanges":
				{
					string value5 = reader.Value;
					bool flag2 = value5 == "true";
					if (!flag2 && value5 != "false")
					{
						base.ThrowException("Invalid attribute value", reader);
					}
					this.restartOnExternalChanges = flag2;
					continue;
				}
				}
				base.ThrowException(string.Format("Unrecognized attribute: {0}", reader.Name), reader);
			}
			if (this.Name == null || this.TypeName == null)
			{
				base.ThrowException("Required attribute missing", reader);
			}
			reader.MoveToElement();
			reader.Skip();
		}

		// Token: 0x060003AF RID: 943 RVA: 0x0000A468 File Offset: 0x00008668
		public override void WriteConfig(Configuration cfg, XmlWriter writer, ConfigurationSaveMode mode)
		{
			writer.WriteStartElement("section");
			writer.WriteAttributeString("name", this.Name);
			writer.WriteAttributeString("type", this.TypeName);
			if (!this.allowLocation)
			{
				writer.WriteAttributeString("allowLocation", "false");
			}
			if (this.allowDefinition != ConfigurationAllowDefinition.Everywhere)
			{
				writer.WriteAttributeString("allowDefinition", this.allowDefinition.ToString());
			}
			if (this.allowExeDefinition != ConfigurationAllowExeDefinition.MachineToApplication)
			{
				writer.WriteAttributeString("allowExeDefinition", this.allowExeDefinition.ToString());
			}
			if (!this.requirePermission)
			{
				writer.WriteAttributeString("requirePermission", "false");
			}
			writer.WriteEndElement();
		}

		// Token: 0x060003B0 RID: 944 RVA: 0x0000A534 File Offset: 0x00008734
		public override void ReadData(Configuration config, XmlReader reader, bool overrideAllowed)
		{
			if (!config.HasFile && !this.allowLocation)
			{
				throw new ConfigurationErrorsException("The configuration section <" + this.Name + "> cannot be defined inside a <location> element.", reader);
			}
			if (!config.ConfigHost.IsDefinitionAllowed(config.ConfigPath, this.allowDefinition, this.allowExeDefinition))
			{
				object obj = (this.allowExeDefinition == ConfigurationAllowExeDefinition.MachineToApplication) ? this.allowDefinition : this.allowExeDefinition;
				throw new ConfigurationErrorsException(string.Concat(new object[]
				{
					"The section <",
					this.Name,
					"> can't be defined in this configuration file (the allowed definition context is '",
					obj,
					"')."
				}), reader);
			}
			if (config.GetSectionXml(this) != null)
			{
				base.ThrowException("The section <" + this.Name + "> is defined more than once in the same configuration file.", reader);
			}
			config.SetSectionXml(this, reader.ReadOuterXml());
		}

		// Token: 0x060003B1 RID: 945 RVA: 0x0000A62C File Offset: 0x0000882C
		public override void WriteData(Configuration config, XmlWriter writer, ConfigurationSaveMode mode)
		{
			ConfigurationSection sectionInstance = config.GetSectionInstance(this, false);
			string text;
			if (sectionInstance != null)
			{
				ConfigurationSection parentElement = (config.Parent == null) ? null : config.Parent.GetSectionInstance(this, false);
				text = sectionInstance.SerializeSection(parentElement, this.Name, mode);
				string externalDataXml = sectionInstance.ExternalDataXml;
				string filePath = config.FilePath;
				if (!string.IsNullOrEmpty(filePath) && !string.IsNullOrEmpty(externalDataXml))
				{
					string path = Path.Combine(Path.GetDirectoryName(filePath), sectionInstance.SectionInformation.ConfigSource);
					using (StreamWriter streamWriter = new StreamWriter(path))
					{
						streamWriter.Write(externalDataXml);
					}
				}
				if (sectionInstance.SectionInformation.IsProtected)
				{
					StringBuilder stringBuilder = new StringBuilder();
					stringBuilder.AppendFormat("<{0} configProtectionProvider=\"{1}\">\n", this.Name, sectionInstance.SectionInformation.ProtectionProvider.Name);
					stringBuilder.Append(config.ConfigHost.EncryptSection(text, sectionInstance.SectionInformation.ProtectionProvider, ProtectedConfiguration.Section));
					stringBuilder.AppendFormat("</{0}>", this.Name);
					text = stringBuilder.ToString();
				}
			}
			else
			{
				text = config.GetSectionXml(this);
			}
			if (text != null)
			{
				writer.WriteRaw(text);
			}
		}

		// Token: 0x060003B2 RID: 946 RVA: 0x0000A78C File Offset: 0x0000898C
		internal override void Merge(ConfigInfo data)
		{
		}

		// Token: 0x04000124 RID: 292
		private bool allowLocation = true;

		// Token: 0x04000125 RID: 293
		private bool requirePermission = true;

		// Token: 0x04000126 RID: 294
		private bool restartOnExternalChanges;

		// Token: 0x04000127 RID: 295
		private ConfigurationAllowDefinition allowDefinition = ConfigurationAllowDefinition.Everywhere;

		// Token: 0x04000128 RID: 296
		private ConfigurationAllowExeDefinition allowExeDefinition = ConfigurationAllowExeDefinition.MachineToApplication;
	}
}
