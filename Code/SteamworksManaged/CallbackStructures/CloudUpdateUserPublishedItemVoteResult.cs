using System;
using System.Runtime.InteropServices;
using ManagedSteam.SteamTypes;

namespace ManagedSteam.CallbackStructures
{
	// Token: 0x0200003B RID: 59
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct CloudUpdateUserPublishedItemVoteResult
	{
		// Token: 0x060001AE RID: 430 RVA: 0x00003D1C File Offset: 0x00001F1C
		internal static CloudUpdateUserPublishedItemVoteResult Create(IntPtr data, int dataSize)
		{
			return NativeHelpers.ConvertStruct<CloudUpdateUserPublishedItemVoteResult>(data, dataSize);
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x060001AF RID: 431 RVA: 0x00003D25 File Offset: 0x00001F25
		public Result Result
		{
			get
			{
				return this.result;
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x060001B0 RID: 432 RVA: 0x00003D2D File Offset: 0x00001F2D
		public PublishedFileId PublishedFileId
		{
			get
			{
				return this.publishedFileId;
			}
		}

		// Token: 0x0400011C RID: 284
		private Result result;

		// Token: 0x0400011D RID: 285
		private PublishedFileId publishedFileId;
	}
}
