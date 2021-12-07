using System;
using System.Runtime.InteropServices;
using ManagedSteam.SteamTypes;

namespace ManagedSteam.CallbackStructures
{
	// Token: 0x0200010A RID: 266
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct GSClientGroupStatus
	{
		// Token: 0x060007BE RID: 1982 RVA: 0x0000B732 File Offset: 0x00009932
		internal static GSClientGroupStatus Create(IntPtr data, int dataSize)
		{
			return NativeHelpers.ConvertStruct<GSClientGroupStatus>(data, dataSize);
		}

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x060007BF RID: 1983 RVA: 0x0000B73B File Offset: 0x0000993B
		public SteamID SteamIDUser
		{
			get
			{
				return this.steamIDUser;
			}
		}

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x060007C0 RID: 1984 RVA: 0x0000B743 File Offset: 0x00009943
		public SteamID SteamIDGroup
		{
			get
			{
				return this.steamIDGroup;
			}
		}

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x060007C1 RID: 1985 RVA: 0x0000B74B File Offset: 0x0000994B
		public bool Member
		{
			get
			{
				return this.member;
			}
		}

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x060007C2 RID: 1986 RVA: 0x0000B753 File Offset: 0x00009953
		public bool Officer
		{
			get
			{
				return this.officer;
			}
		}

		// Token: 0x040004A3 RID: 1187
		private SteamID steamIDUser;

		// Token: 0x040004A4 RID: 1188
		private SteamID steamIDGroup;

		// Token: 0x040004A5 RID: 1189
		[MarshalAs(UnmanagedType.I1)]
		private bool member;

		// Token: 0x040004A6 RID: 1190
		[MarshalAs(UnmanagedType.I1)]
		private bool officer;
	}
}
