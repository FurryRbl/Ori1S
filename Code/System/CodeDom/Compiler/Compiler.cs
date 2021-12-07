using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;

namespace System.CodeDom.Compiler
{
	// Token: 0x0200007D RID: 125
	internal sealed class Compiler : ConfigurationElement
	{
		// Token: 0x06000507 RID: 1287 RVA: 0x00010734 File Offset: 0x0000E934
		internal Compiler()
		{
		}

		// Token: 0x06000508 RID: 1288 RVA: 0x0001073C File Offset: 0x0000E93C
		public Compiler(string compilerOptions, string extension, string language, string type, int warningLevel)
		{
			this.CompilerOptions = compilerOptions;
			this.Extension = extension;
			this.Language = language;
			this.Type = type;
			this.WarningLevel = warningLevel;
		}

		// Token: 0x06000509 RID: 1289 RVA: 0x00010774 File Offset: 0x0000E974
		static Compiler()
		{
			Compiler.properties.Add(Compiler.compilerOptionsProp);
			Compiler.properties.Add(Compiler.extensionProp);
			Compiler.properties.Add(Compiler.languageProp);
			Compiler.properties.Add(Compiler.typeProp);
			Compiler.properties.Add(Compiler.warningLevelProp);
			Compiler.properties.Add(Compiler.providerOptionsProp);
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x0600050A RID: 1290 RVA: 0x000108B4 File Offset: 0x0000EAB4
		// (set) Token: 0x0600050B RID: 1291 RVA: 0x000108C8 File Offset: 0x0000EAC8
		[ConfigurationProperty("compilerOptions", DefaultValue = "")]
		public string CompilerOptions
		{
			get
			{
				return (string)base[Compiler.compilerOptionsProp];
			}
			internal set
			{
				base[Compiler.compilerOptionsProp] = value;
			}
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x0600050C RID: 1292 RVA: 0x000108D8 File Offset: 0x0000EAD8
		// (set) Token: 0x0600050D RID: 1293 RVA: 0x000108EC File Offset: 0x0000EAEC
		[ConfigurationProperty("extension", DefaultValue = "")]
		public string Extension
		{
			get
			{
				return (string)base[Compiler.extensionProp];
			}
			internal set
			{
				base[Compiler.extensionProp] = value;
			}
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x0600050E RID: 1294 RVA: 0x000108FC File Offset: 0x0000EAFC
		// (set) Token: 0x0600050F RID: 1295 RVA: 0x00010910 File Offset: 0x0000EB10
		[ConfigurationProperty("language", DefaultValue = "", Options = (ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey))]
		public string Language
		{
			get
			{
				return (string)base[Compiler.languageProp];
			}
			internal set
			{
				base[Compiler.languageProp] = value;
			}
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x06000510 RID: 1296 RVA: 0x00010920 File Offset: 0x0000EB20
		// (set) Token: 0x06000511 RID: 1297 RVA: 0x00010934 File Offset: 0x0000EB34
		[ConfigurationProperty("type", DefaultValue = "", Options = ConfigurationPropertyOptions.IsRequired)]
		public string Type
		{
			get
			{
				return (string)base[Compiler.typeProp];
			}
			internal set
			{
				base[Compiler.typeProp] = value;
			}
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x06000512 RID: 1298 RVA: 0x00010944 File Offset: 0x0000EB44
		// (set) Token: 0x06000513 RID: 1299 RVA: 0x00010958 File Offset: 0x0000EB58
		[ConfigurationProperty("warningLevel", DefaultValue = "0")]
		[IntegerValidator(MinValue = 0, MaxValue = 4)]
		public int WarningLevel
		{
			get
			{
				return (int)base[Compiler.warningLevelProp];
			}
			internal set
			{
				base[Compiler.warningLevelProp] = value;
			}
		}

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x06000514 RID: 1300 RVA: 0x0001096C File Offset: 0x0000EB6C
		// (set) Token: 0x06000515 RID: 1301 RVA: 0x00010980 File Offset: 0x0000EB80
		[ConfigurationProperty("", Options = ConfigurationPropertyOptions.IsDefaultCollection)]
		public CompilerProviderOptionsCollection ProviderOptions
		{
			get
			{
				return (CompilerProviderOptionsCollection)base[Compiler.providerOptionsProp];
			}
			internal set
			{
				base[Compiler.providerOptionsProp] = value;
			}
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x06000516 RID: 1302 RVA: 0x00010990 File Offset: 0x0000EB90
		public Dictionary<string, string> ProviderOptionsDictionary
		{
			get
			{
				return this.ProviderOptions.ProviderOptions;
			}
		}

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x06000517 RID: 1303 RVA: 0x000109A0 File Offset: 0x0000EBA0
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return Compiler.properties;
			}
		}

		// Token: 0x0400012C RID: 300
		private static ConfigurationProperty compilerOptionsProp = new ConfigurationProperty("compilerOptions", typeof(string), string.Empty);

		// Token: 0x0400012D RID: 301
		private static ConfigurationProperty extensionProp = new ConfigurationProperty("extension", typeof(string), string.Empty);

		// Token: 0x0400012E RID: 302
		private static ConfigurationProperty languageProp = new ConfigurationProperty("language", typeof(string), string.Empty, ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey);

		// Token: 0x0400012F RID: 303
		private static ConfigurationProperty typeProp = new ConfigurationProperty("type", typeof(string), string.Empty, ConfigurationPropertyOptions.IsRequired);

		// Token: 0x04000130 RID: 304
		private static ConfigurationProperty warningLevelProp = new ConfigurationProperty("warningLevel", typeof(int), 0, System.ComponentModel.TypeDescriptor.GetConverter(typeof(int)), new IntegerValidator(0, 4), ConfigurationPropertyOptions.None);

		// Token: 0x04000131 RID: 305
		private static ConfigurationProperty providerOptionsProp = new ConfigurationProperty(string.Empty, typeof(CompilerProviderOptionsCollection), null, null, null, ConfigurationPropertyOptions.IsDefaultCollection);

		// Token: 0x04000132 RID: 306
		private static ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();
	}
}
