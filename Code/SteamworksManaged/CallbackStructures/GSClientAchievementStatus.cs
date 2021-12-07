using System;
using System.Runtime.InteropServices;
using ManagedSteam.SteamTypes;

namespace ManagedSteam.CallbackStructures
{
	// Token: 0x02000107 RID: 263
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct GSClientAchievementStatus
	{
		// Token: 0x060007B3 RID: 1971 RVA: 0x0000B6D1 File Offset: 0x000098D1
		internal static GSClientAchievementStatus Create(IntPtr data, int dataSize)
		{
			return NativeHelpers.ConvertStruct<GSClientAchievementStatus>(data, dataSize);
		}

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x060007B4 RID: 1972 RVA: 0x0000B6DA File Offset: 0x000098DA
		public SteamID SteamID
		{
			get
			{
				return this.steamID;
			}
		}

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x060007B5 RID: 1973 RVA: 0x0000B6E2 File Offset: 0x000098E2
		public string Achievement
		{
			get
			{
				return this.achievement;
			}
		}

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x060007B6 RID: 1974 RVA: 0x0000B6EA File Offset: 0x000098EA
		private bool Unlocked
		{
			get
			{
				return this.unlocked;
			}
		}

		// Token: 0x0400049B RID: 1179
		private SteamID steamID;

		// Token: 0x0400049C RID: 1180
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
		private string achievement;

		// Token: 0x0400049D RID: 1181
		[MarshalAs(UnmanagedType.I1)]
		private bool unlocked;
	}
}
