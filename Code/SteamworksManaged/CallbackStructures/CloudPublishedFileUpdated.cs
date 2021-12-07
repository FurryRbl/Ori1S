using System;
using System.Runtime.InteropServices;
using ManagedSteam.SteamTypes;

namespace ManagedSteam.CallbackStructures
{
	// Token: 0x02000042 RID: 66
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct CloudPublishedFileUpdated
	{
		// Token: 0x060001D0 RID: 464 RVA: 0x00003E33 File Offset: 0x00002033
		internal static CloudPublishedFileUpdated Create(IntPtr data, int dataSize)
		{
			return NativeHelpers.ConvertStruct<CloudPublishedFileUpdated>(data, dataSize);
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x060001D1 RID: 465 RVA: 0x00003E3C File Offset: 0x0000203C
		public PublishedFileId PublishedFileId
		{
			get
			{
				return this.publishedFileId;
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x060001D2 RID: 466 RVA: 0x00003E44 File Offset: 0x00002044
		public AppID AppId
		{
			get
			{
				return this.appId;
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x060001D3 RID: 467 RVA: 0x00003E4C File Offset: 0x0000204C
		public UGCHandle UGCHandle
		{
			get
			{
				return this.ugcHandle;
			}
		}

		// Token: 0x04000137 RID: 311
		private PublishedFileId publishedFileId;

		// Token: 0x04000138 RID: 312
		private AppID appId;

		// Token: 0x04000139 RID: 313
		private UGCHandle ugcHandle;
	}
}
