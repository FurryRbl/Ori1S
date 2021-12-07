using System;
using System.Runtime.InteropServices;

namespace ManagedSteam.CallbackStructures
{
	// Token: 0x02000011 RID: 17
	[StructLayout(LayoutKind.Sequential, Pack = 8, Size = 4)]
	public struct DlcInstalled
	{
		// Token: 0x060000B5 RID: 181 RVA: 0x00003403 File Offset: 0x00001603
		internal static DlcInstalled Create(IntPtr data, int dataSize)
		{
			return NativeHelpers.ConvertStruct<DlcInstalled>(data, dataSize);
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x060000B6 RID: 182 RVA: 0x0000340C File Offset: 0x0000160C
		public uint AppID
		{
			get
			{
				return this.appID;
			}
		}

		// Token: 0x04000049 RID: 73
		private uint appID;
	}
}
