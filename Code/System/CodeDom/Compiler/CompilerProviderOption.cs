using System;
using System.Configuration;

namespace System.CodeDom.Compiler
{
	// Token: 0x02000083 RID: 131
	internal sealed class CompilerProviderOption : ConfigurationElement
	{
		// Token: 0x06000575 RID: 1397 RVA: 0x00011574 File Offset: 0x0000F774
		static CompilerProviderOption()
		{
			CompilerProviderOption.properties.Add(CompilerProviderOption.nameProp);
			CompilerProviderOption.properties.Add(CompilerProviderOption.valueProp);
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x06000576 RID: 1398 RVA: 0x000115E8 File Offset: 0x0000F7E8
		// (set) Token: 0x06000577 RID: 1399 RVA: 0x000115FC File Offset: 0x0000F7FC
		[ConfigurationProperty("name", DefaultValue = "", Options = (ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey))]
		public string Name
		{
			get
			{
				return (string)base[CompilerProviderOption.nameProp];
			}
			set
			{
				base[CompilerProviderOption.nameProp] = value;
			}
		}

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x06000578 RID: 1400 RVA: 0x0001160C File Offset: 0x0000F80C
		// (set) Token: 0x06000579 RID: 1401 RVA: 0x00011620 File Offset: 0x0000F820
		[ConfigurationProperty("value", DefaultValue = "", Options = ConfigurationPropertyOptions.IsRequired)]
		public string Value
		{
			get
			{
				return (string)base[CompilerProviderOption.valueProp];
			}
			set
			{
				base[CompilerProviderOption.valueProp] = value;
			}
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x0600057A RID: 1402 RVA: 0x00011630 File Offset: 0x0000F830
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return CompilerProviderOption.properties;
			}
		}

		// Token: 0x04000154 RID: 340
		private static ConfigurationProperty nameProp = new ConfigurationProperty("name", typeof(string), string.Empty, ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey);

		// Token: 0x04000155 RID: 341
		private static ConfigurationProperty valueProp = new ConfigurationProperty("value", typeof(string), string.Empty, ConfigurationPropertyOptions.IsRequired);

		// Token: 0x04000156 RID: 342
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();
	}
}
