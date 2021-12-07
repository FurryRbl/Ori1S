using System;
using System.Runtime.InteropServices;
using ManagedSteam.SteamTypes;

namespace ManagedSteam.CallbackStructures
{
	// Token: 0x02000166 RID: 358
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct CheckFileSignature
	{
		// Token: 0x06000BE4 RID: 3044 RVA: 0x00010290 File Offset: 0x0000E490
		internal static CheckFileSignature Create(IntPtr data, int dataSize)
		{
			return NativeHelpers.ConvertStruct<CheckFileSignature>(data, dataSize);
		}

		// Token: 0x170001B0 RID: 432
		// (get) Token: 0x06000BE5 RID: 3045 RVA: 0x00010299 File Offset: 0x0000E499
		public ECheckFileSignature FileSignature
		{
			get
			{
				return this.checkFileSignature;
			}
		}

		// Token: 0x04000646 RID: 1606
		private ECheckFileSignature checkFileSignature;
	}
}
