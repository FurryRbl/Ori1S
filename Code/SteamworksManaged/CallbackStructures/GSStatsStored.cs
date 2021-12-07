using System;
using System.Runtime.InteropServices;
using ManagedSteam.SteamTypes;

namespace ManagedSteam.CallbackStructures
{
	// Token: 0x02000130 RID: 304
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct GSStatsStored
	{
		// Token: 0x06000A5A RID: 2650 RVA: 0x0000C55E File Offset: 0x0000A75E
		internal static GSStatsStored Create(IntPtr data, int dataSize)
		{
			return NativeHelpers.ConvertStruct<GSStatsStored>(data, dataSize);
		}

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x06000A5B RID: 2651 RVA: 0x0000C567 File Offset: 0x0000A767
		public Result Result
		{
			get
			{
				return this.result;
			}
		}

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x06000A5C RID: 2652 RVA: 0x0000C56F File Offset: 0x0000A76F
		public SteamID SteamIDUser
		{
			get
			{
				return this.steamIDUser;
			}
		}

		// Token: 0x0400052C RID: 1324
		private Result result;

		// Token: 0x0400052D RID: 1325
		private SteamID steamIDUser;
	}
}
