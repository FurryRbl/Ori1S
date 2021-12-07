using System;
using System.Runtime.CompilerServices;

namespace Mono
{
	// Token: 0x0200008B RID: 139
	internal class Runtime
	{
		// Token: 0x0600082A RID: 2090
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void mono_runtime_install_handlers();

		// Token: 0x0600082B RID: 2091 RVA: 0x0001E0AC File Offset: 0x0001C2AC
		internal static void InstallSignalHandlers()
		{
			Runtime.mono_runtime_install_handlers();
		}

		// Token: 0x0600082C RID: 2092
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern string GetDisplayName();
	}
}
