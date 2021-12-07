using System;
using System.Collections;
using System.Configuration;
using System.Threading;

namespace System.Diagnostics
{
	// Token: 0x02000219 RID: 537
	internal sealed class DiagnosticsConfiguration
	{
		// Token: 0x17000418 RID: 1048
		// (get) Token: 0x06001212 RID: 4626 RVA: 0x0002FEE8 File Offset: 0x0002E0E8
		public static IDictionary Settings
		{
			get
			{
				if (DiagnosticsConfiguration.settings == null)
				{
					object config = System.Configuration.ConfigurationSettings.GetConfig("system.diagnostics");
					if (config == null)
					{
						throw new Exception("INTERNAL configuration error: failed to get configuration 'system.diagnostics'");
					}
					Thread.MemoryBarrier();
					while (Interlocked.CompareExchange(ref DiagnosticsConfiguration.settings, config, null) == null)
					{
					}
					Thread.MemoryBarrier();
				}
				return (IDictionary)DiagnosticsConfiguration.settings;
			}
		}

		// Token: 0x04000528 RID: 1320
		private static object settings;
	}
}
