using System;

namespace System.Configuration
{
	/// <summary>A configuration element that contains a <see cref="T:System.String" /> name and <see cref="T:System.String" /> value. This class cannot be inherited.</summary>
	// Token: 0x0200005B RID: 91
	public sealed class NameValueConfigurationElement : ConfigurationElement
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Configuration.NameValueConfigurationElement" /> class based on supplied parameters.</summary>
		/// <param name="name">The name of the <see cref="T:System.Configuration.NameValueConfigurationElement" /> object.</param>
		/// <param name="value">The value of the <see cref="T:System.Configuration.NameValueConfigurationElement" /> object.</param>
		// Token: 0x06000337 RID: 823 RVA: 0x00009394 File Offset: 0x00007594
		public NameValueConfigurationElement(string name, string value)
		{
			base[NameValueConfigurationElement._propName] = name;
			base[NameValueConfigurationElement._propValue] = value;
		}

		// Token: 0x06000338 RID: 824 RVA: 0x000093B4 File Offset: 0x000075B4
		static NameValueConfigurationElement()
		{
			NameValueConfigurationElement._properties.Add(NameValueConfigurationElement._propName);
			NameValueConfigurationElement._properties.Add(NameValueConfigurationElement._propValue);
		}

		/// <summary>Gets the name of the <see cref="T:System.Configuration.NameValueConfigurationElement" /> object.</summary>
		/// <returns>The name of the <see cref="T:System.Configuration.NameValueConfigurationElement" /> object.</returns>
		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x06000339 RID: 825 RVA: 0x00009428 File Offset: 0x00007628
		[ConfigurationProperty("name", DefaultValue = "", Options = ConfigurationPropertyOptions.IsKey)]
		public string Name
		{
			get
			{
				return (string)base[NameValueConfigurationElement._propName];
			}
		}

		/// <summary>Gets or sets the value of the <see cref="T:System.Configuration.NameValueConfigurationElement" /> object.</summary>
		/// <returns>The value of the <see cref="T:System.Configuration.NameValueConfigurationElement" /> object.</returns>
		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x0600033A RID: 826 RVA: 0x0000943C File Offset: 0x0000763C
		// (set) Token: 0x0600033B RID: 827 RVA: 0x00009450 File Offset: 0x00007650
		[ConfigurationProperty("value", DefaultValue = "", Options = ConfigurationPropertyOptions.None)]
		public string Value
		{
			get
			{
				return (string)base[NameValueConfigurationElement._propValue];
			}
			set
			{
				base[NameValueConfigurationElement._propValue] = value;
			}
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x0600033C RID: 828 RVA: 0x00009460 File Offset: 0x00007660
		protected internal override ConfigurationPropertyCollection Properties
		{
			get
			{
				return NameValueConfigurationElement._properties;
			}
		}

		// Token: 0x040000FC RID: 252
		private static ConfigurationPropertyCollection _properties = new ConfigurationPropertyCollection();

		// Token: 0x040000FD RID: 253
		private static readonly ConfigurationProperty _propName = new ConfigurationProperty("name", typeof(string), string.Empty, ConfigurationPropertyOptions.IsKey);

		// Token: 0x040000FE RID: 254
		private static readonly ConfigurationProperty _propValue = new ConfigurationProperty("value", typeof(string), string.Empty);
	}
}
