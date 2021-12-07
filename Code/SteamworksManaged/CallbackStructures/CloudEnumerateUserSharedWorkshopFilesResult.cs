using System;
using System.Runtime.InteropServices;
using ManagedSteam.SteamTypes;

namespace ManagedSteam.CallbackStructures
{
	// Token: 0x0200003D RID: 61
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct CloudEnumerateUserSharedWorkshopFilesResult
	{
		// Token: 0x060001B5 RID: 437 RVA: 0x00003D56 File Offset: 0x00001F56
		internal static CloudEnumerateUserSharedWorkshopFilesResult Create(IntPtr data, int dataSize)
		{
			return NativeHelpers.ConvertStruct<CloudEnumerateUserSharedWorkshopFilesResult>(data, dataSize);
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x060001B6 RID: 438 RVA: 0x00003D5F File Offset: 0x00001F5F
		public Result Result
		{
			get
			{
				return this.result;
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x060001B7 RID: 439 RVA: 0x00003D67 File Offset: 0x00001F67
		public int ResultsReturned
		{
			get
			{
				return this.resultsReturned;
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x060001B8 RID: 440 RVA: 0x00003D6F File Offset: 0x00001F6F
		public int TotalResultCount
		{
			get
			{
				return this.totalResultCount;
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x060001B9 RID: 441 RVA: 0x00003D77 File Offset: 0x00001F77
		public PublishedFileId[] PublishedFileId
		{
			get
			{
				return this.publishedFileId;
			}
		}

		// Token: 0x04000121 RID: 289
		private Result result;

		// Token: 0x04000122 RID: 290
		private int resultsReturned;

		// Token: 0x04000123 RID: 291
		private int totalResultCount;

		// Token: 0x04000124 RID: 292
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 50)]
		private PublishedFileId[] publishedFileId;
	}
}
