using System;

namespace System.Configuration
{
	/// <summary>Represents a configuration element that contains a key/value pair. </summary>
	// Token: 0x02000057 RID: 87
	public class KeyValueConfigurationElement : ConfigurationElement
	{
		// Token: 0x06000316 RID: 790 RVA: 0x00009030 File Offset: 0x00007230
		internal KeyValueConfigurationElement()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.KeyValueConfigurationElement" /> class based on the supplied parameters.</summary>
		/// <param name="key">The key of the <see cref="T:System.Configuration.KeyValueConfigurationElement" />.</param>
		/// <param name="value">The value of the <see cref="T:System.Configuration.KeyValueConfigurationElement" />.</param>
		// Token: 0x06000317 RID: 791 RVA: 0x00009038 File Offset: 0x00007238
		public KeyValueConfigurationElement(string key, string value)
		{
			base[KeyValueConfigurationElement.keyProp] = key;
			base[KeyValueConfigurationElement.valueProp] = value;
		}

		// Token: 0x06000318 RID: 792 RVA: 0x00009058 File Offset: 0x00007258
		static KeyValueConfigurationElement()
		{
			KeyValueConfigurationElement.properties.Add(KeyValueConfigurationElement.keyProp);
			KeyValueConfigurationElement.properties.Add(KeyValueConfigurationElement.valueProp);
		}

		/// <summary>Gets the key of the <see cref="T:System.Configuration.KeyValueConfigurationElement" /> object.</summary>
		/// <returns>The key of the <see cref="T:System.Configuration.KeyValueConfigurationElement" />.</returns>
		// Token: 0x170000DC RID: 220
		// (get) Token: 0x06000319 RID: 793 RVA: 0x000090CC File Offset: 0x000072CC
		[ConfigurationProperty("key", DefaultValue = "", Options = ConfigurationPropertyOptions.IsKey)]
		public string Key
		{
			get
			{
				return (string)base[KeyValueConfigurationElement.keyProp];
			}
		}

		/// <summary>Gets or sets the value of the <see cref="T:System.Configuration.KeyValueConfigurationElement" /> object.</summary>
		/// <returns>The value of the <see cref="T:System.Configuration.KeyValueConfigurationElement" />.</returns>
		// Token: 0x170000DD RID: 221
		// (get) Token: 0x0600031A RID: 794 RVA: 0x000090E0 File Offset: 0x000072E0
		// (set) Token: 0x0600031B RID: 795 RVA: 0x000090F4 File Offset: 0x000072F4
		[ConfigurationProperty("value", DefaultValue = "")]
		public string Value
		{
			get
			{
				return (string)base[KeyValueConfigurationElement.valueProp];
			}
			set
			{
				base[KeyValueConfigurationElement.valueProp] = value;
			}
		}

		/// <summary>Sets the <see cref="T:System.Configuration.KeyValueConfigurationElement" /> object to its initial state.</summary>
		// Token: 0x0600031C RID: 796 RVA: 0x00009104 File Offset: 0x00007304
		[MonoTODO]
		protected internal override void Init()
		{
		}

		/// <summary>Gets the collection of properties. </summary>
		/// <returns>The <see cref="T:System.Configuration.ConfigurationPropertyCollection" /> of properties for the element.</returns>
		// Token: 0x170000DE RID: 222
		// (get) Token: 0x0600031D RID: 797 RVA: 0x00009108 File Offset: 0x00007308
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return KeyValueConfigurationElement.properties;
			}
		}

		// Token: 0x040000F0 RID: 240
		private static ConfigurationProperty keyProp = new ConfigurationProperty("key", typeof(string), string.Empty, ConfigurationPropertyOptions.IsKey);

		// Token: 0x040000F1 RID: 241
		private static ConfigurationProperty valueProp = new ConfigurationProperty("value", typeof(string), string.Empty);

		// Token: 0x040000F2 RID: 242
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();
	}
}
