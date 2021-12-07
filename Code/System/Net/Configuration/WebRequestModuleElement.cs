using System;
using System.ComponentModel;
using System.Configuration;

namespace System.Net.Configuration
{
	/// <summary>Represents a URI prefix and the associated class that handles creating Web requests for the prefix. This class cannot be inherited.</summary>
	// Token: 0x020002EB RID: 747
	public sealed class WebRequestModuleElement : ConfigurationElement
	{
		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Configuration.WebRequestModuleElement" /> class. </summary>
		// Token: 0x0600197C RID: 6524 RVA: 0x00045DD4 File Offset: 0x00043FD4
		public WebRequestModuleElement()
		{
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Configuration.WebRequestModuleElement" /> class using the specified URI prefix and type information. </summary>
		/// <param name="prefix">A string containing a URI prefix.</param>
		/// <param name="type">A string containing the type and assembly information for the class that handles creating requests for resources that use the <paramref name="prefix" /> URI prefix. For more information, see the Remarks section.</param>
		// Token: 0x0600197D RID: 6525 RVA: 0x00045DDC File Offset: 0x00043FDC
		public WebRequestModuleElement(string prefix, string type)
		{
			base[WebRequestModuleElement.typeProp] = type;
			this.Prefix = prefix;
		}

		/// <summary>Initializes a new instance of the <see cref="T:System.Net.Configuration.WebRequestModuleElement" /> class using the specified URI prefix and type identifier.</summary>
		/// <param name="prefix">A string containing a URI prefix.</param>
		/// <param name="type">A <see cref="T:System.Type" /> that identifies the class that handles creating requests for resources that use the <paramref name="prefix" /> URI prefix. </param>
		// Token: 0x0600197E RID: 6526 RVA: 0x00045DF8 File Offset: 0x00043FF8
		public WebRequestModuleElement(string prefix, Type type) : this(prefix, type.FullName)
		{
		}

		// Token: 0x0600197F RID: 6527 RVA: 0x00045E08 File Offset: 0x00044008
		static WebRequestModuleElement()
		{
			WebRequestModuleElement.properties = new ConfigurationPropertyCollection();
			WebRequestModuleElement.properties.Add(WebRequestModuleElement.prefixProp);
			WebRequestModuleElement.properties.Add(WebRequestModuleElement.typeProp);
		}

		/// <summary>Gets or sets the URI prefix for the current Web request module.</summary>
		/// <returns>A string that contains a URI prefix.</returns>
		// Token: 0x17000620 RID: 1568
		// (get) Token: 0x06001980 RID: 6528 RVA: 0x00045E74 File Offset: 0x00044074
		// (set) Token: 0x06001981 RID: 6529 RVA: 0x00045E88 File Offset: 0x00044088
		[ConfigurationProperty("prefix", Options = (ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey))]
		public string Prefix
		{
			get
			{
				return (string)base[WebRequestModuleElement.prefixProp];
			}
			set
			{
				base[WebRequestModuleElement.prefixProp] = value;
			}
		}

		/// <summary>Gets or sets a class that creates Web requests.</summary>
		/// <returns>A <see cref="T:System.Type" /> instance that identifies a Web request module.</returns>
		// Token: 0x17000621 RID: 1569
		// (get) Token: 0x06001982 RID: 6530 RVA: 0x00045E98 File Offset: 0x00044098
		// (set) Token: 0x06001983 RID: 6531 RVA: 0x00045EB0 File Offset: 0x000440B0
		[System.ComponentModel.TypeConverter(typeof(System.ComponentModel.TypeConverter))]
		[ConfigurationProperty("type")]
		public Type Type
		{
			get
			{
				return Type.GetType((string)base[WebRequestModuleElement.typeProp]);
			}
			set
			{
				base[WebRequestModuleElement.typeProp] = value.FullName;
			}
		}

		// Token: 0x17000622 RID: 1570
		// (get) Token: 0x06001984 RID: 6532 RVA: 0x00045EC4 File Offset: 0x000440C4
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return WebRequestModuleElement.properties;
			}
		}

		// Token: 0x04001004 RID: 4100
		private static ConfigurationPropertyCollection properties;

		// Token: 0x04001005 RID: 4101
		private static ConfigurationProperty prefixProp = new ConfigurationProperty("prefix", typeof(string), null, ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey);

		// Token: 0x04001006 RID: 4102
		private static ConfigurationProperty typeProp = new ConfigurationProperty("type", typeof(string));
	}
}
