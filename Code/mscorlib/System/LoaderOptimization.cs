using System;
using System.Runtime.InteropServices;

namespace System
{
	/// <summary>An enumeration used with the <see cref="T:System.LoaderOptimizationAttribute" /> class to specify loader optimizations for an executable.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x0200014A RID: 330
	[ComVisible(true)]
	[Serializable]
	public enum LoaderOptimization
	{
		/// <summary>Indicates that no optimizations for sharing internal resources are specified. If the default domain or hosting interface specified an optimization, then the loader uses that; otherwise, the loader uses <see cref="F:System.LoaderOptimization.SingleDomain" />.</summary>
		// Token: 0x0400051F RID: 1311
		NotSpecified,
		/// <summary>Indicates that the application will probably have a single domain, and loader must not share internal resources across application domains. </summary>
		// Token: 0x04000520 RID: 1312
		SingleDomain,
		/// <summary>Indicates that the application will probably have many domains that use the same code, and the loader must share maximal internal resources across application domains. </summary>
		// Token: 0x04000521 RID: 1313
		MultiDomain,
		/// <summary>Indicates that the application will probably host unique code in multiple domains, and the loader must share resources across application domains only for globally available (strong-named) assemblies that have been added to the global assembly cache. </summary>
		// Token: 0x04000522 RID: 1314
		MultiDomainHost,
		/// <summary>Do not use. This mask selects the domain-related values, screening out the unused <see cref="F:System.LoaderOptimization.DisallowBindings" /> flag.</summary>
		// Token: 0x04000523 RID: 1315
		[Obsolete]
		DomainMask = 3,
		/// <summary>Ignored by the common language runtime.</summary>
		// Token: 0x04000524 RID: 1316
		[Obsolete]
		DisallowBindings
	}
}
