using System;
using System.Runtime.InteropServices;
using ManagedSteam.SteamTypes;

namespace ManagedSteam.CallbackStructures
{
	// Token: 0x0200003C RID: 60
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct CloudUserVoteDetails
	{
		// Token: 0x060001B1 RID: 433 RVA: 0x00003D35 File Offset: 0x00001F35
		internal static CloudUserVoteDetails Create(IntPtr data, int dataSize)
		{
			return NativeHelpers.ConvertStruct<CloudUserVoteDetails>(data, dataSize);
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x060001B2 RID: 434 RVA: 0x00003D3E File Offset: 0x00001F3E
		public Result Result
		{
			get
			{
				return this.result;
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x060001B3 RID: 435 RVA: 0x00003D46 File Offset: 0x00001F46
		public PublishedFileId PublishedFileId
		{
			get
			{
				return this.publishedFileId;
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x060001B4 RID: 436 RVA: 0x00003D4E File Offset: 0x00001F4E
		public WorkshopVote Vote
		{
			get
			{
				return this.vote;
			}
		}

		// Token: 0x0400011E RID: 286
		private Result result;

		// Token: 0x0400011F RID: 287
		private PublishedFileId publishedFileId;

		// Token: 0x04000120 RID: 288
		private WorkshopVote vote;
	}
}
