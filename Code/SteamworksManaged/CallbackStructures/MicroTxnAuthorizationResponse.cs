using System;
using System.Runtime.InteropServices;

namespace ManagedSteam.CallbackStructures
{
	// Token: 0x0200002C RID: 44
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct MicroTxnAuthorizationResponse
	{
		// Token: 0x06000160 RID: 352 RVA: 0x00003A8E File Offset: 0x00001C8E
		internal static MicroTxnAuthorizationResponse Create(IntPtr data, int dataSize)
		{
			return NativeHelpers.ConvertStruct<MicroTxnAuthorizationResponse>(data, dataSize);
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000161 RID: 353 RVA: 0x00003A97 File Offset: 0x00001C97
		public uint AppID
		{
			get
			{
				return this.appID;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000162 RID: 354 RVA: 0x00003A9F File Offset: 0x00001C9F
		public ulong OrderID
		{
			get
			{
				return this.orderID;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000163 RID: 355 RVA: 0x00003AA7 File Offset: 0x00001CA7
		public byte Authorized
		{
			get
			{
				return this.authorized;
			}
		}

		// Token: 0x040000DD RID: 221
		private uint appID;

		// Token: 0x040000DE RID: 222
		private ulong orderID;

		// Token: 0x040000DF RID: 223
		private byte authorized;
	}
}
