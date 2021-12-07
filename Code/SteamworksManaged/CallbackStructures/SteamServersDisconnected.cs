using System;
using System.Runtime.InteropServices;
using ManagedSteam.SteamTypes;

namespace ManagedSteam.CallbackStructures
{
	// Token: 0x02000027 RID: 39
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct SteamServersDisconnected
	{
		// Token: 0x06000152 RID: 338 RVA: 0x00003A1A File Offset: 0x00001C1A
		internal static SteamServersDisconnected Create(IntPtr data, int dataSize)
		{
			return NativeHelpers.ConvertStruct<SteamServersDisconnected>(data, dataSize);
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000153 RID: 339 RVA: 0x00003A23 File Offset: 0x00001C23
		public Result Result
		{
			get
			{
				return this.result;
			}
		}

		// Token: 0x040000D0 RID: 208
		private Result result;
	}
}
