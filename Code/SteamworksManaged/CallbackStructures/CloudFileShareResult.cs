using System;
using System.Runtime.InteropServices;
using ManagedSteam.SteamTypes;

namespace ManagedSteam.CallbackStructures
{
	// Token: 0x02000030 RID: 48
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct CloudFileShareResult
	{
		// Token: 0x0600016B RID: 363 RVA: 0x00003AEA File Offset: 0x00001CEA
		internal static CloudFileShareResult Create(IntPtr dataPointer, int dataSize)
		{
			return NativeHelpers.ConvertStruct<CloudFileShareResult>(dataPointer, dataSize);
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x0600016C RID: 364 RVA: 0x00003AF3 File Offset: 0x00001CF3
		public Result Result
		{
			get
			{
				return this.result;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600016D RID: 365 RVA: 0x00003AFB File Offset: 0x00001CFB
		public UGCHandle Handle
		{
			get
			{
				return this.handle;
			}
		}

		// Token: 0x040000E4 RID: 228
		private Result result;

		// Token: 0x040000E5 RID: 229
		private UGCHandle handle;
	}
}
