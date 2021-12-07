using System;
using System.Runtime.InteropServices;
using ManagedSteam.SteamTypes;

namespace ManagedSteam.CallbackStructures
{
	// Token: 0x02000033 RID: 51
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct CloudUpdatePublishedFileResult
	{
		// Token: 0x06000179 RID: 377 RVA: 0x00003B6C File Offset: 0x00001D6C
		internal static CloudUpdatePublishedFileResult Create(IntPtr data, int dataSize)
		{
			return NativeHelpers.ConvertStruct<CloudUpdatePublishedFileResult>(data, dataSize);
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x0600017A RID: 378 RVA: 0x00003B75 File Offset: 0x00001D75
		public Result Result
		{
			get
			{
				return this.result;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x0600017B RID: 379 RVA: 0x00003B7D File Offset: 0x00001D7D
		public PublishedFileId PublishedFileId
		{
			get
			{
				return this.publishedFileId;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x0600017C RID: 380 RVA: 0x00003B85 File Offset: 0x00001D85
		public bool UserNeedsToAcceptWorkshopLegalAgreement
		{
			get
			{
				return this.userNeedsToAcceptWorkshopLegalAgreement;
			}
		}

		// Token: 0x040000EF RID: 239
		private Result result;

		// Token: 0x040000F0 RID: 240
		private PublishedFileId publishedFileId;

		// Token: 0x040000F1 RID: 241
		private bool userNeedsToAcceptWorkshopLegalAgreement;
	}
}
