using System;
using System.Runtime.InteropServices;
using ManagedSteam.SteamTypes;

namespace ManagedSteam.CallbackStructures
{
	// Token: 0x02000013 RID: 19
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct AppProofOfPurchaseKeyResponse
	{
		// Token: 0x060000BA RID: 186 RVA: 0x0000342D File Offset: 0x0000162D
		internal static AppProofOfPurchaseKeyResponse Create(IntPtr data, int dataSize)
		{
			return NativeHelpers.ConvertStruct<AppProofOfPurchaseKeyResponse>(data, dataSize);
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x060000BB RID: 187 RVA: 0x00003436 File Offset: 0x00001636
		public Result Result
		{
			get
			{
				return this.result;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x060000BC RID: 188 RVA: 0x0000343E File Offset: 0x0000163E
		public uint AppID
		{
			get
			{
				return this.appID;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x060000BD RID: 189 RVA: 0x00003446 File Offset: 0x00001646
		public string Key
		{
			get
			{
				return this.key;
			}
		}

		// Token: 0x0400004C RID: 76
		private Result result;

		// Token: 0x0400004D RID: 77
		private uint appID;

		// Token: 0x0400004E RID: 78
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 64)]
		private string key;
	}
}
