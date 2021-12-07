using System;
using System.Configuration.Internal;

namespace System.Configuration
{
	// Token: 0x0200004F RID: 79
	internal class InternalConfigurationFactory : IInternalConfigConfigurationFactory
	{
		// Token: 0x060002C1 RID: 705 RVA: 0x000087F4 File Offset: 0x000069F4
		public Configuration Create(Type typeConfigHost, params object[] hostInitConfigurationParams)
		{
			InternalConfigurationSystem internalConfigurationSystem = new InternalConfigurationSystem();
			internalConfigurationSystem.Init(typeConfigHost, hostInitConfigurationParams);
			return new Configuration(internalConfigurationSystem, null);
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x00008818 File Offset: 0x00006A18
		public string NormalizeLocationSubPath(string subPath, IConfigErrorInfo errorInfo)
		{
			return subPath;
		}
	}
}
