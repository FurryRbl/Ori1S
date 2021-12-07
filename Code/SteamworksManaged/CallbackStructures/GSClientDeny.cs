using System;
using System.Runtime.InteropServices;
using ManagedSteam.SteamTypes;

namespace ManagedSteam.CallbackStructures
{
	// Token: 0x02000105 RID: 261
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct GSClientDeny
	{
		// Token: 0x060007AC RID: 1964 RVA: 0x0000B697 File Offset: 0x00009897
		internal static GSClientDeny Create(IntPtr data, int dataSize)
		{
			return NativeHelpers.ConvertStruct<GSClientDeny>(data, dataSize);
		}

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x060007AD RID: 1965 RVA: 0x0000B6A0 File Offset: 0x000098A0
		public SteamID SteamIDClient
		{
			get
			{
				return this.steamID;
			}
		}

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x060007AE RID: 1966 RVA: 0x0000B6A8 File Offset: 0x000098A8
		public DenyReason DenyReason
		{
			get
			{
				return this.denyReason;
			}
		}

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x060007AF RID: 1967 RVA: 0x0000B6B0 File Offset: 0x000098B0
		public string OptionalText
		{
			get
			{
				return this.optionalText;
			}
		}

		// Token: 0x04000496 RID: 1174
		private SteamID steamID;

		// Token: 0x04000497 RID: 1175
		private DenyReason denyReason;

		// Token: 0x04000498 RID: 1176
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
		private string optionalText;
	}
}
