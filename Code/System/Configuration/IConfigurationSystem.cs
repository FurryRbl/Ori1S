using System;
using System.Runtime.InteropServices;

namespace System.Configuration
{
	/// <summary>Provides standard configuration methods.</summary>
	/// <filterpriority>2</filterpriority>
	// Token: 0x020001E0 RID: 480
	[ComVisible(false)]
	public interface IConfigurationSystem
	{
		/// <summary>Gets the specified configuration.</summary>
		/// <returns>The object representing the configuration.</returns>
		/// <param name="configKey">The configuration key.</param>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060010B5 RID: 4277
		object GetConfig(string configKey);

		/// <summary>Used for initialization.</summary>
		/// <filterpriority>2</filterpriority>
		// Token: 0x060010B6 RID: 4278
		void Init();
	}
}
