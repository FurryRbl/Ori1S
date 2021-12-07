using System;
using System.Runtime.InteropServices;
using ManagedSteam.SteamTypes;

namespace ManagedSteam.CallbackStructures
{
	// Token: 0x02000037 RID: 55
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct CloudSubscribePublishedFileResult
	{
		// Token: 0x0600019B RID: 411 RVA: 0x00003C80 File Offset: 0x00001E80
		internal static CloudSubscribePublishedFileResult Create(IntPtr data, int dataSize)
		{
			return NativeHelpers.ConvertStruct<CloudSubscribePublishedFileResult>(data, dataSize);
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x0600019C RID: 412 RVA: 0x00003C89 File Offset: 0x00001E89
		public Result Result
		{
			get
			{
				return this.result;
			}
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x0600019D RID: 413 RVA: 0x00003C91 File Offset: 0x00001E91
		public PublishedFileId PublishedFileId
		{
			get
			{
				return this.publishedFileId;
			}
		}

		// Token: 0x0400010D RID: 269
		private Result result;

		// Token: 0x0400010E RID: 270
		private PublishedFileId publishedFileId;
	}
}
