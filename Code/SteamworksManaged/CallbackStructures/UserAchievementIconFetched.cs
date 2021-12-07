using System;
using System.Runtime.InteropServices;
using ManagedSteam.SteamTypes;

namespace ManagedSteam.CallbackStructures
{
	// Token: 0x02000061 RID: 97
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct UserAchievementIconFetched
	{
		// Token: 0x06000336 RID: 822 RVA: 0x00006C5A File Offset: 0x00004E5A
		internal static UserAchievementIconFetched Create(IntPtr data, int dataSize)
		{
			return NativeHelpers.ConvertStruct<UserAchievementIconFetched>(data, dataSize);
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x06000337 RID: 823 RVA: 0x00006C63 File Offset: 0x00004E63
		public GameID GameID
		{
			get
			{
				return this.gameID;
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x06000338 RID: 824 RVA: 0x00006C6B File Offset: 0x00004E6B
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x06000339 RID: 825 RVA: 0x00006C73 File Offset: 0x00004E73
		public bool Achieved
		{
			get
			{
				return this.achieved;
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x0600033A RID: 826 RVA: 0x00006C7B File Offset: 0x00004E7B
		public IconHandle IconHandle
		{
			get
			{
				return this.iconHandle;
			}
		}

		// Token: 0x040001C3 RID: 451
		private GameID gameID;

		// Token: 0x040001C4 RID: 452
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
		private string name;

		// Token: 0x040001C5 RID: 453
		[MarshalAs(UnmanagedType.I1)]
		private bool achieved;

		// Token: 0x040001C6 RID: 454
		private IconHandle iconHandle;
	}
}
