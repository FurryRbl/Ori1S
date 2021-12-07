using System;
using System.Runtime.InteropServices;
using ManagedSteam.SteamTypes;

namespace ManagedSteam.CallbackStructures
{
	// Token: 0x02000026 RID: 38
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct SteamServerConnectFailure
	{
		// Token: 0x06000150 RID: 336 RVA: 0x00003A09 File Offset: 0x00001C09
		internal static SteamServerConnectFailure Create(IntPtr data, int dataSize)
		{
			return NativeHelpers.ConvertStruct<SteamServerConnectFailure>(data, dataSize);
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000151 RID: 337 RVA: 0x00003A12 File Offset: 0x00001C12
		public Result Result
		{
			get
			{
				return this.result;
			}
		}

		// Token: 0x040000CF RID: 207
		private Result result;
	}
}
