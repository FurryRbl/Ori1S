using System;
using System.Runtime.InteropServices;
using ManagedSteam.SteamTypes;

namespace ManagedSteam.CallbackStructures
{
	// Token: 0x0200003A RID: 58
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct CloudGetPublishedItemVoteDetailsResult
	{
		// Token: 0x060001A7 RID: 423 RVA: 0x00003CE3 File Offset: 0x00001EE3
		internal static CloudGetPublishedItemVoteDetailsResult Create(IntPtr data, int dataSize)
		{
			return NativeHelpers.ConvertStruct<CloudGetPublishedItemVoteDetailsResult>(data, dataSize);
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x060001A8 RID: 424 RVA: 0x00003CEC File Offset: 0x00001EEC
		public Result Result
		{
			get
			{
				return this.result;
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x060001A9 RID: 425 RVA: 0x00003CF4 File Offset: 0x00001EF4
		public PublishedFileId PublishedFileId
		{
			get
			{
				return this.publishedFileId;
			}
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x060001AA RID: 426 RVA: 0x00003CFC File Offset: 0x00001EFC
		public int VotesFor
		{
			get
			{
				return this.votesFor;
			}
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x060001AB RID: 427 RVA: 0x00003D04 File Offset: 0x00001F04
		public int VotesAgainst
		{
			get
			{
				return this.votesAgainst;
			}
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x060001AC RID: 428 RVA: 0x00003D0C File Offset: 0x00001F0C
		public int Reports
		{
			get
			{
				return this.reports;
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x060001AD RID: 429 RVA: 0x00003D14 File Offset: 0x00001F14
		public float Score
		{
			get
			{
				return this.score;
			}
		}

		// Token: 0x04000116 RID: 278
		private Result result;

		// Token: 0x04000117 RID: 279
		private PublishedFileId publishedFileId;

		// Token: 0x04000118 RID: 280
		private int votesFor;

		// Token: 0x04000119 RID: 281
		private int votesAgainst;

		// Token: 0x0400011A RID: 282
		private int reports;

		// Token: 0x0400011B RID: 283
		private float score;
	}
}
