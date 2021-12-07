using System;
using System.Runtime.InteropServices;
using ManagedSteam.SteamTypes;

namespace ManagedSteam.CallbackStructures
{
	// Token: 0x0200005D RID: 93
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct UserStatsUnloaded
	{
		// Token: 0x06000328 RID: 808 RVA: 0x00006BE6 File Offset: 0x00004DE6
		internal static UserStatsUnloaded Create(IntPtr data, int dataSize)
		{
			return NativeHelpers.ConvertStruct<UserStatsUnloaded>(data, dataSize);
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x06000329 RID: 809 RVA: 0x00006BEF File Offset: 0x00004DEF
		public SteamID UserID
		{
			get
			{
				return this.userID;
			}
		}

		// Token: 0x040001B9 RID: 441
		private SteamID userID;
	}
}
