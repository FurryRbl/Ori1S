using System;
using System.Runtime.InteropServices;
using ManagedSteam.SteamTypes;

namespace ManagedSteam.CallbackStructures
{
	// Token: 0x02000060 RID: 96
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct UserAchievementStored
	{
		// Token: 0x06000330 RID: 816 RVA: 0x00006C29 File Offset: 0x00004E29
		internal static UserAchievementStored Create(IntPtr data, int dataSize)
		{
			return NativeHelpers.ConvertStruct<UserAchievementStored>(data, dataSize);
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x06000331 RID: 817 RVA: 0x00006C32 File Offset: 0x00004E32
		public GameID GameID
		{
			get
			{
				return this.gameID;
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x06000332 RID: 818 RVA: 0x00006C3A File Offset: 0x00004E3A
		public bool GroupAchievement
		{
			get
			{
				return this.groupAchievement;
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x06000333 RID: 819 RVA: 0x00006C42 File Offset: 0x00004E42
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x06000334 RID: 820 RVA: 0x00006C4A File Offset: 0x00004E4A
		public uint CurrentProgress
		{
			get
			{
				return this.currentProgress;
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x06000335 RID: 821 RVA: 0x00006C52 File Offset: 0x00004E52
		public uint MaxProgress
		{
			get
			{
				return this.maxProgress;
			}
		}

		// Token: 0x040001BE RID: 446
		private GameID gameID;

		// Token: 0x040001BF RID: 447
		[MarshalAs(UnmanagedType.I1)]
		private bool groupAchievement;

		// Token: 0x040001C0 RID: 448
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 128)]
		private string name;

		// Token: 0x040001C1 RID: 449
		private uint currentProgress;

		// Token: 0x040001C2 RID: 450
		private uint maxProgress;
	}
}
