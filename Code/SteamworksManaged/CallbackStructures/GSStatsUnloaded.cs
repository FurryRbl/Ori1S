using System;
using System.Runtime.InteropServices;
using ManagedSteam.SteamTypes;

namespace ManagedSteam.CallbackStructures
{
	// Token: 0x02000131 RID: 305
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct GSStatsUnloaded
	{
		// Token: 0x06000A5D RID: 2653 RVA: 0x0000C577 File Offset: 0x0000A777
		internal static GSStatsUnloaded Create(IntPtr data, int dataSize)
		{
			return NativeHelpers.ConvertStruct<GSStatsUnloaded>(data, dataSize);
		}

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x06000A5E RID: 2654 RVA: 0x0000C580 File Offset: 0x0000A780
		public SteamID SteamIDUser
		{
			get
			{
				return this.steamIDUser;
			}
		}

		// Token: 0x0400052E RID: 1326
		private SteamID steamIDUser;
	}
}
