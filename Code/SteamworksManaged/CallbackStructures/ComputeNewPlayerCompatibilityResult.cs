using System;
using System.Runtime.InteropServices;
using ManagedSteam.SteamTypes;

namespace ManagedSteam.CallbackStructures
{
	// Token: 0x0200010D RID: 269
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct ComputeNewPlayerCompatibilityResult
	{
		// Token: 0x060007CD RID: 1997 RVA: 0x0000B7AD File Offset: 0x000099AD
		internal static ComputeNewPlayerCompatibilityResult Create(IntPtr data, int dataSize)
		{
			return NativeHelpers.ConvertStruct<ComputeNewPlayerCompatibilityResult>(data, dataSize);
		}

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x060007CE RID: 1998 RVA: 0x0000B7B6 File Offset: 0x000099B6
		public Result Result
		{
			get
			{
				return this.result;
			}
		}

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x060007CF RID: 1999 RVA: 0x0000B7BE File Offset: 0x000099BE
		public int PlayersThatDontLikeCandidate
		{
			get
			{
				return this.playersThatDontLikeCandidate;
			}
		}

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x060007D0 RID: 2000 RVA: 0x0000B7C6 File Offset: 0x000099C6
		public int PlayersThatCandidateDoesntLike
		{
			get
			{
				return this.playersThatCandidateDoesntLike;
			}
		}

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x060007D1 RID: 2001 RVA: 0x0000B7CE File Offset: 0x000099CE
		public int ClanPlayersThatDontLikeCandidate
		{
			get
			{
				return this.clanPlayersThatDontLikeCandidate;
			}
		}

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x060007D2 RID: 2002 RVA: 0x0000B7D6 File Offset: 0x000099D6
		public SteamID SteamIDCandidate
		{
			get
			{
				return this.steamIDCandidate;
			}
		}

		// Token: 0x040004AF RID: 1199
		private Result result;

		// Token: 0x040004B0 RID: 1200
		private int playersThatDontLikeCandidate;

		// Token: 0x040004B1 RID: 1201
		private int playersThatCandidateDoesntLike;

		// Token: 0x040004B2 RID: 1202
		private int clanPlayersThatDontLikeCandidate;

		// Token: 0x040004B3 RID: 1203
		private SteamID steamIDCandidate;
	}
}
