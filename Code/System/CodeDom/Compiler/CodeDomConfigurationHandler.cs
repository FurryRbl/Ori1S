using System;
using System.Configuration;

namespace System.CodeDom.Compiler
{
	// Token: 0x02000077 RID: 119
	internal sealed class CodeDomConfigurationHandler : ConfigurationSection
	{
		// Token: 0x06000422 RID: 1058 RVA: 0x0000E40C File Offset: 0x0000C60C
		static CodeDomConfigurationHandler()
		{
			CodeDomConfigurationHandler.compilersProp = new ConfigurationProperty("compilers", typeof(CompilerCollection), CodeDomConfigurationHandler.default_compilers);
			CodeDomConfigurationHandler.properties = new ConfigurationPropertyCollection();
			CodeDomConfigurationHandler.properties.Add(CodeDomConfigurationHandler.compilersProp);
		}

		// Token: 0x06000423 RID: 1059 RVA: 0x0000E45C File Offset: 0x0000C65C
		protected override void InitializeDefault()
		{
			CodeDomConfigurationHandler.compilersProp = new ConfigurationProperty("compilers", typeof(CompilerCollection), CodeDomConfigurationHandler.default_compilers);
		}

		// Token: 0x06000424 RID: 1060 RVA: 0x0000E47C File Offset: 0x0000C67C
		[MonoTODO]
		protected override void PostDeserialize()
		{
			base.PostDeserialize();
		}

		// Token: 0x06000425 RID: 1061 RVA: 0x0000E484 File Offset: 0x0000C684
		protected override object GetRuntimeObject()
		{
			return this;
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x06000426 RID: 1062 RVA: 0x0000E488 File Offset: 0x0000C688
		[ConfigurationProperty("compilers")]
		public CompilerCollection Compilers
		{
			get
			{
				return (CompilerCollection)base[CodeDomConfigurationHandler.compilersProp];
			}
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x06000427 RID: 1063 RVA: 0x0000E49C File Offset: 0x0000C69C
		public CompilerInfo[] CompilerInfos
		{
			get
			{
				CompilerCollection compilerCollection = (CompilerCollection)base[CodeDomConfigurationHandler.compilersProp];
				if (compilerCollection == null)
				{
					return null;
				}
				return compilerCollection.CompilerInfos;
			}
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x06000428 RID: 1064 RVA: 0x0000E4C8 File Offset: 0x0000C6C8
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return CodeDomConfigurationHandler.properties;
			}
		}

		// Token: 0x04000121 RID: 289
		private static ConfigurationPropertyCollection properties;

		// Token: 0x04000122 RID: 290
		private static ConfigurationProperty compilersProp;

		// Token: 0x04000123 RID: 291
		private static CompilerCollection default_compilers = new CompilerCollection();
	}
}
