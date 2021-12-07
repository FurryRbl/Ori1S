using System;
using System.Runtime.InteropServices;

namespace System.Configuration.Internal
{
	/// <summary>Defines an interface used by the .NET Framework to support configuration management.</summary>
	// Token: 0x02000007 RID: 7
	[ComVisible(false)]
	public interface IConfigurationManagerHelper
	{
		/// <summary>Ensures that the networking configuration is loaded.</summary>
		// Token: 0x06000035 RID: 53
		void EnsureNetConfigLoaded();
	}
}
