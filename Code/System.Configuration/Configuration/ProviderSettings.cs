using System;
using System.Collections.Specialized;

namespace System.Configuration
{
	/// <summary>Represents the configuration elements associated with a provider.</summary>
	// Token: 0x02000067 RID: 103
	public sealed class ProviderSettings : ConfigurationElement
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.ProviderSettings" /> class. </summary>
		// Token: 0x0600037D RID: 893 RVA: 0x00009B04 File Offset: 0x00007D04
		public ProviderSettings()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.ProviderSettings" /> class. </summary>
		/// <param name="name">The name of the provider to configure settings for.</param>
		/// <param name="type">The type of the provider to configure settings for.</param>
		// Token: 0x0600037E RID: 894 RVA: 0x00009B0C File Offset: 0x00007D0C
		public ProviderSettings(string name, string type)
		{
			this.Name = name;
			this.Type = type;
		}

		// Token: 0x0600037F RID: 895 RVA: 0x00009B24 File Offset: 0x00007D24
		static ProviderSettings()
		{
			ProviderSettings.properties.Add(ProviderSettings.nameProp);
			ProviderSettings.properties.Add(ProviderSettings.typeProp);
		}

		// Token: 0x06000380 RID: 896 RVA: 0x00009B90 File Offset: 0x00007D90
		protected override bool OnDeserializeUnrecognizedAttribute(string name, string value)
		{
			if (this.parameters == null)
			{
				this.parameters = new ConfigNameValueCollection();
			}
			this.parameters[name] = value;
			this.parameters.ResetModified();
			return true;
		}

		// Token: 0x06000381 RID: 897 RVA: 0x00009BC4 File Offset: 0x00007DC4
		protected internal override bool IsModified()
		{
			return (this.parameters != null && this.parameters.IsModified) || base.IsModified();
		}

		// Token: 0x06000382 RID: 898 RVA: 0x00009BF8 File Offset: 0x00007DF8
		protected internal override void Reset(ConfigurationElement parentElement)
		{
			base.Reset(parentElement);
			ProviderSettings providerSettings = parentElement as ProviderSettings;
			if (providerSettings != null && providerSettings.parameters != null)
			{
				this.parameters = new ConfigNameValueCollection(providerSettings.parameters);
			}
			else
			{
				this.parameters = null;
			}
		}

		// Token: 0x06000383 RID: 899 RVA: 0x00009C44 File Offset: 0x00007E44
		[MonoTODO]
		protected internal override void Unmerge(ConfigurationElement source, ConfigurationElement parent, ConfigurationSaveMode updateMode)
		{
			base.Unmerge(source, parent, updateMode);
		}

		/// <summary>Gets or sets the name of the provider configured by this class.</summary>
		/// <returns>The name of the provider.</returns>
		// Token: 0x17000105 RID: 261
		// (get) Token: 0x06000384 RID: 900 RVA: 0x00009C50 File Offset: 0x00007E50
		// (set) Token: 0x06000385 RID: 901 RVA: 0x00009C64 File Offset: 0x00007E64
		[ConfigurationProperty("name", Options = (ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey))]
		public string Name
		{
			get
			{
				return (string)base[ProviderSettings.nameProp];
			}
			set
			{
				base[ProviderSettings.nameProp] = value;
			}
		}

		/// <summary>Gets or sets the type of the provider configured by this class.</summary>
		/// <returns>The fully qualified namespace and class name for the type of provider configured by this <see cref="T:System.Configuration.ProviderSettings" /> instance.</returns>
		// Token: 0x17000106 RID: 262
		// (get) Token: 0x06000386 RID: 902 RVA: 0x00009C74 File Offset: 0x00007E74
		// (set) Token: 0x06000387 RID: 903 RVA: 0x00009C88 File Offset: 0x00007E88
		[ConfigurationProperty("type", Options = ConfigurationPropertyOptions.IsRequired)]
		public string Type
		{
			get
			{
				return (string)base[ProviderSettings.typeProp];
			}
			set
			{
				base[ProviderSettings.typeProp] = value;
			}
		}

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x06000388 RID: 904 RVA: 0x00009C98 File Offset: 0x00007E98
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return ProviderSettings.properties;
			}
		}

		/// <summary>Gets a collection of user-defined parameters for the provider.</summary>
		/// <returns>A <see cref="T:System.Collections.Specialized.NameValueCollection" /> of parameters for the provider.</returns>
		// Token: 0x17000108 RID: 264
		// (get) Token: 0x06000389 RID: 905 RVA: 0x00009CA0 File Offset: 0x00007EA0
		public NameValueCollection Parameters
		{
			get
			{
				if (this.parameters == null)
				{
					this.parameters = new ConfigNameValueCollection();
				}
				return this.parameters;
			}
		}

		// Token: 0x04000117 RID: 279
		private ConfigNameValueCollection parameters;

		// Token: 0x04000118 RID: 280
		private static ConfigurationProperty nameProp = new ConfigurationProperty("name", typeof(string), null, ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey);

		// Token: 0x04000119 RID: 281
		private static ConfigurationProperty typeProp = new ConfigurationProperty("type", typeof(string), null, ConfigurationPropertyOptions.IsRequired);

		// Token: 0x0400011A RID: 282
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();
	}
}
