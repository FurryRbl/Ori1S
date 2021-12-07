using System;
using System.Runtime.InteropServices;
using ManagedSteam.SteamTypes;

namespace ManagedSteam.CallbackStructures
{
	// Token: 0x0200003F RID: 63
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct CloudEnumeratePublishedFilesByUserActionResult
	{
		// Token: 0x060001BE RID: 446 RVA: 0x00003DA0 File Offset: 0x00001FA0
		internal static CloudEnumeratePublishedFilesByUserActionResult Create(IntPtr data, int dataSize)
		{
			return NativeHelpers.ConvertStruct<CloudEnumeratePublishedFilesByUserActionResult>(data, dataSize);
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x060001BF RID: 447 RVA: 0x00003DA9 File Offset: 0x00001FA9
		public Result Result
		{
			get
			{
				return this.result;
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x060001C0 RID: 448 RVA: 0x00003DB1 File Offset: 0x00001FB1
		public WorkshopFileAction Action
		{
			get
			{
				return this.action;
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x060001C1 RID: 449 RVA: 0x00003DB9 File Offset: 0x00001FB9
		public int ResultsReturned
		{
			get
			{
				return this.resultsReturned;
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x060001C2 RID: 450 RVA: 0x00003DC1 File Offset: 0x00001FC1
		public int TotalResultCount
		{
			get
			{
				return this.totalResultCount;
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x060001C3 RID: 451 RVA: 0x00003DC9 File Offset: 0x00001FC9
		public PublishedFileId[] PublishedFileId
		{
			get
			{
				return this.publishedFileId;
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x060001C4 RID: 452 RVA: 0x00003DD1 File Offset: 0x00001FD1
		public uint[] TimeUpdated
		{
			get
			{
				return this.timeUpdated;
			}
		}

		// Token: 0x04000128 RID: 296
		private Result result;

		// Token: 0x04000129 RID: 297
		private WorkshopFileAction action;

		// Token: 0x0400012A RID: 298
		private int resultsReturned;

		// Token: 0x0400012B RID: 299
		private int totalResultCount;

		// Token: 0x0400012C RID: 300
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 50)]
		private PublishedFileId[] publishedFileId;

		// Token: 0x0400012D RID: 301
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 50)]
		private uint[] timeUpdated;
	}
}
