using System;
using System.Runtime.InteropServices;
using ManagedSteam.SteamTypes;

namespace ManagedSteam.CallbackStructures
{
	// Token: 0x02000039 RID: 57
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct CloudUnsubscribePublishedFileResult
	{
		// Token: 0x060001A4 RID: 420 RVA: 0x00003CCA File Offset: 0x00001ECA
		internal static CloudUnsubscribePublishedFileResult Create(IntPtr data, int dataSize)
		{
			return NativeHelpers.ConvertStruct<CloudUnsubscribePublishedFileResult>(data, dataSize);
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x060001A5 RID: 421 RVA: 0x00003CD3 File Offset: 0x00001ED3
		public Result Result
		{
			get
			{
				return this.result;
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x060001A6 RID: 422 RVA: 0x00003CDB File Offset: 0x00001EDB
		public PublishedFileId PublishedFileId
		{
			get
			{
				return this.publishedFileId;
			}
		}

		// Token: 0x04000114 RID: 276
		private Result result;

		// Token: 0x04000115 RID: 277
		private PublishedFileId publishedFileId;
	}
}
