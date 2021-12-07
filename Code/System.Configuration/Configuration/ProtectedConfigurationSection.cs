using System;
using System.Xml;

namespace System.Configuration
{
	/// <summary>Provides programmatic access to the configProtectedData configuration section. This class cannot be inherited.</summary>
	// Token: 0x02000065 RID: 101
	public sealed class ProtectedConfigurationSection : ConfigurationSection
	{
		// Token: 0x06000370 RID: 880 RVA: 0x000098A8 File Offset: 0x00007AA8
		static ProtectedConfigurationSection()
		{
			ProtectedConfigurationSection.properties.Add(ProtectedConfigurationSection.defaultProviderProp);
			ProtectedConfigurationSection.properties.Add(ProtectedConfigurationSection.providersProp);
		}

		/// <summary>Gets or sets the name of the default <see cref="T:System.Configuration.ProtectedConfigurationProvider" /> object in the <see cref="P:System.Configuration.ProtectedConfigurationSection.Providers" /> collection property.</summary>
		/// <returns>The name of the default <see cref="T:System.Configuration.ProtectedConfigurationProvider" /> object in the <see cref="P:System.Configuration.ProtectedConfigurationSection.Providers" /> collection property. </returns>
		// Token: 0x17000100 RID: 256
		// (get) Token: 0x06000371 RID: 881 RVA: 0x00009918 File Offset: 0x00007B18
		// (set) Token: 0x06000372 RID: 882 RVA: 0x0000992C File Offset: 0x00007B2C
		[ConfigurationProperty("defaultProvider", DefaultValue = "RsaProtectedConfigurationProvider")]
		public string DefaultProvider
		{
			get
			{
				return (string)base[ProtectedConfigurationSection.defaultProviderProp];
			}
			set
			{
				base[ProtectedConfigurationSection.defaultProviderProp] = value;
			}
		}

		/// <summary>Gets a <see cref="T:System.Configuration.ProviderSettingsCollection" /> collection of all the <see cref="T:System.Configuration.ProtectedConfigurationProvider" /> objects in all participating configuration files.</summary>
		/// <returns>A <see cref="T:System.Configuration.ProviderSettingsCollection" /> collection of all the <see cref="T:System.Configuration.ProtectedConfigurationProvider" /> objects in all participating configuration files. </returns>
		// Token: 0x17000101 RID: 257
		// (get) Token: 0x06000373 RID: 883 RVA: 0x0000993C File Offset: 0x00007B3C
		[ConfigurationProperty("providers")]
		public ProviderSettingsCollection Providers
		{
			get
			{
				return (ProviderSettingsCollection)base[ProtectedConfigurationSection.providersProp];
			}
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x06000374 RID: 884 RVA: 0x00009950 File Offset: 0x00007B50
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return ProtectedConfigurationSection.properties;
			}
		}

		// Token: 0x06000375 RID: 885 RVA: 0x00009958 File Offset: 0x00007B58
		internal string EncryptSection(string clearXml, ProtectedConfigurationProvider protectionProvider)
		{
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.LoadXml(clearXml);
			XmlNode xmlNode = protectionProvider.Encrypt(xmlDocument.DocumentElement);
			return xmlNode.OuterXml;
		}

		// Token: 0x06000376 RID: 886 RVA: 0x00009988 File Offset: 0x00007B88
		internal string DecryptSection(string encryptedXml, ProtectedConfigurationProvider protectionProvider)
		{
			XmlNode xmlNode = protectionProvider.Decrypt(new XmlDocument
			{
				InnerXml = encryptedXml
			}.DocumentElement);
			return xmlNode.OuterXml;
		}

		// Token: 0x06000377 RID: 887 RVA: 0x000099B8 File Offset: 0x00007BB8
		internal ProtectedConfigurationProviderCollection GetAllProviders()
		{
			if (this.providers == null)
			{
				this.providers = new ProtectedConfigurationProviderCollection();
				foreach (object obj in this.Providers)
				{
					ProviderSettings ps = (ProviderSettings)obj;
					this.providers.Add(this.InstantiateProvider(ps));
				}
			}
			return this.providers;
		}

		// Token: 0x06000378 RID: 888 RVA: 0x00009A50 File Offset: 0x00007C50
		private ProtectedConfigurationProvider InstantiateProvider(ProviderSettings ps)
		{
			Type type = Type.GetType(ps.Type, true);
			ProtectedConfigurationProvider protectedConfigurationProvider = Activator.CreateInstance(type) as ProtectedConfigurationProvider;
			if (protectedConfigurationProvider == null)
			{
				throw new Exception("The type specified does not extend ProtectedConfigurationProvider class.");
			}
			protectedConfigurationProvider.Initialize(ps.Name, ps.Parameters);
			return protectedConfigurationProvider;
		}

		// Token: 0x04000111 RID: 273
		private static ConfigurationProperty defaultProviderProp = new ConfigurationProperty("defaultProvider", typeof(string), "RsaProtectedConfigurationProvider");

		// Token: 0x04000112 RID: 274
		private static ConfigurationProperty providersProp = new ConfigurationProperty("providers", typeof(ProviderSettingsCollection), null);

		// Token: 0x04000113 RID: 275
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x04000114 RID: 276
		private ProtectedConfigurationProviderCollection providers;
	}
}
