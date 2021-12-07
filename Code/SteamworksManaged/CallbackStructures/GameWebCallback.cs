using System;
using System.Runtime.InteropServices;

namespace ManagedSteam.CallbackStructures
{
	// Token: 0x0200002F RID: 47
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct GameWebCallback
	{
		// Token: 0x06000169 RID: 361 RVA: 0x00003AD9 File Offset: 0x00001CD9
		internal static GameWebCallback Create(IntPtr data, int dataSize)
		{
			return NativeHelpers.ConvertStruct<GameWebCallback>(data, dataSize);
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x0600016A RID: 362 RVA: 0x00003AE2 File Offset: 0x00001CE2
		public string URL
		{
			get
			{
				return this.url;
			}
		}

		// Token: 0x040000E3 RID: 227
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
		private string url;
	}
}
