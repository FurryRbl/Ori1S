using System;
using System.Runtime.InteropServices;
using ManagedSteam.SteamTypes;

namespace ManagedSteam.CallbackStructures
{
	// Token: 0x02000038 RID: 56
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct CloudEnumerateUserSubscribedFilesResult
	{
		// Token: 0x0600019E RID: 414 RVA: 0x00003C99 File Offset: 0x00001E99
		internal static CloudEnumerateUserSubscribedFilesResult Create(IntPtr data, int dataSize)
		{
			return NativeHelpers.ConvertStruct<CloudEnumerateUserSubscribedFilesResult>(data, dataSize);
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x0600019F RID: 415 RVA: 0x00003CA2 File Offset: 0x00001EA2
		public Result Result
		{
			get
			{
				return this.result;
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060001A0 RID: 416 RVA: 0x00003CAA File Offset: 0x00001EAA
		public int ResultsReturned
		{
			get
			{
				return this.resultsReturned;
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060001A1 RID: 417 RVA: 0x00003CB2 File Offset: 0x00001EB2
		public int TotalResultCount
		{
			get
			{
				return this.totalResultCount;
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060001A2 RID: 418 RVA: 0x00003CBA File Offset: 0x00001EBA
		public PublishedFileId[] PublishedFileId
		{
			get
			{
				return this.publishedFileId;
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060001A3 RID: 419 RVA: 0x00003CC2 File Offset: 0x00001EC2
		public uint[] TimeSubscribed
		{
			get
			{
				return this.timeSubscribed;
			}
		}

		// Token: 0x0400010F RID: 271
		private Result result;

		// Token: 0x04000110 RID: 272
		private int resultsReturned;

		// Token: 0x04000111 RID: 273
		private int totalResultCount;

		// Token: 0x04000112 RID: 274
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 50)]
		private PublishedFileId[] publishedFileId;

		// Token: 0x04000113 RID: 275
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 50)]
		private uint[] timeSubscribed;
	}
}
