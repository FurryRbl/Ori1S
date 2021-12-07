using System;
using System.Runtime.InteropServices;
using ManagedSteam.SteamTypes;

namespace ManagedSteam.CallbackStructures
{
	// Token: 0x02000032 RID: 50
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct CloudPublishFileResult
	{
		// Token: 0x06000175 RID: 373 RVA: 0x00003B4B File Offset: 0x00001D4B
		internal static CloudPublishFileResult Create(IntPtr data, int dataSize)
		{
			return NativeHelpers.ConvertStruct<CloudPublishFileResult>(data, dataSize);
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000176 RID: 374 RVA: 0x00003B54 File Offset: 0x00001D54
		public Result Result
		{
			get
			{
				return this.result;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000177 RID: 375 RVA: 0x00003B5C File Offset: 0x00001D5C
		public PublishedFileId PublishedFileId
		{
			get
			{
				return this.publishedFileId;
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000178 RID: 376 RVA: 0x00003B64 File Offset: 0x00001D64
		public bool UserNeedsToAcceptWorkshopLegalAgreement
		{
			get
			{
				return this.userNeedsToAcceptWorkshopLegalAgreement;
			}
		}

		// Token: 0x040000EC RID: 236
		private Result result;

		// Token: 0x040000ED RID: 237
		private PublishedFileId publishedFileId;

		// Token: 0x040000EE RID: 238
		private bool userNeedsToAcceptWorkshopLegalAgreement;
	}
}
