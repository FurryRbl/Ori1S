using System;
using System.Runtime.InteropServices;

namespace System.Configuration.Internal
{
	/// <summary>Defines an interface used by the .NET Framework to initialize application configuration properties.</summary>
	// Token: 0x0200000F RID: 15
	[ComVisible(false)]
	public interface IInternalConfigSystem
	{
		/// <summary>Returns the configuration object based on the specified key. </summary>
		/// <returns>A configuration object.</returns>
		/// <param name="configKey">The configuration key value.</param>
		// Token: 0x06000087 RID: 135
		object GetSection(string configKey);

		/// <summary>Refreshes the configuration system based on the specified section name. </summary>
		/// <param name="sectionName">The name of the configuration section.</param>
		// Token: 0x06000088 RID: 136
		void RefreshConfig(string sectionName);

		/// <summary>Gets a value indicating whether the user configuration is supported. </summary>
		/// <returns>true if the user configuration is supported; otherwise, false.</returns>
		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000089 RID: 137
		bool SupportsUserConfig { get; }
	}
}
