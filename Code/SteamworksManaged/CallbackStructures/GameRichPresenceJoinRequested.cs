using System;
using System.Runtime.InteropServices;
using ManagedSteam.SteamTypes;

namespace ManagedSteam.CallbackStructures
{
	// Token: 0x02000147 RID: 327
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct GameRichPresenceJoinRequested
	{
		// Token: 0x06000B88 RID: 2952 RVA: 0x0000FAD6 File Offset: 0x0000DCD6
		internal static GameRichPresenceJoinRequested Create(IntPtr data, int dataSize)
		{
			return NativeHelpers.ConvertStruct<GameRichPresenceJoinRequested>(data, dataSize);
		}

		// Token: 0x1700018A RID: 394
		// (get) Token: 0x06000B89 RID: 2953 RVA: 0x0000FADF File Offset: 0x0000DCDF
		public SteamID SteamIDFriend
		{
			get
			{
				return this.steamIDFriend;
			}
		}

		// Token: 0x1700018B RID: 395
		// (get) Token: 0x06000B8A RID: 2954 RVA: 0x0000FAE7 File Offset: 0x0000DCE7
		public string Connect
		{
			get
			{
				return this.connect;
			}
		}

		// Token: 0x040005D4 RID: 1492
		private SteamID steamIDFriend;

		// Token: 0x040005D5 RID: 1493
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
		private string connect;
	}
}
