using System;
using System.Runtime.InteropServices;
using ManagedSteam.SteamTypes;

namespace ManagedSteam.CallbackStructures
{
	// Token: 0x02000036 RID: 54
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct CloudEnumerateUserPublishedFilesResult
	{
		// Token: 0x06000196 RID: 406 RVA: 0x00003C57 File Offset: 0x00001E57
		internal static CloudEnumerateUserPublishedFilesResult Create(IntPtr data, int dataSize)
		{
			return NativeHelpers.ConvertStruct<CloudEnumerateUserPublishedFilesResult>(data, dataSize);
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000197 RID: 407 RVA: 0x00003C60 File Offset: 0x00001E60
		public Result Result
		{
			get
			{
				return this.result;
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x06000198 RID: 408 RVA: 0x00003C68 File Offset: 0x00001E68
		public int ResultsReturned
		{
			get
			{
				return this.resultsReturned;
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x06000199 RID: 409 RVA: 0x00003C70 File Offset: 0x00001E70
		public int TotalResultCount
		{
			get
			{
				return this.totalResultCount;
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x0600019A RID: 410 RVA: 0x00003C78 File Offset: 0x00001E78
		public PublishedFileId[] PublishedFileId
		{
			get
			{
				return this.publishedFileId;
			}
		}

		// Token: 0x04000109 RID: 265
		private Result result;

		// Token: 0x0400010A RID: 266
		private int resultsReturned;

		// Token: 0x0400010B RID: 267
		private int totalResultCount;

		// Token: 0x0400010C RID: 268
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 50)]
		private PublishedFileId[] publishedFileId;
	}
}
