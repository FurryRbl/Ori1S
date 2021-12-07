using System;
using System.Runtime.InteropServices;
using ManagedSteam.SteamTypes;

namespace ManagedSteam.CallbackStructures
{
	// Token: 0x02000035 RID: 53
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct CloudDeletePublishedFileResult
	{
		// Token: 0x06000193 RID: 403 RVA: 0x00003C3E File Offset: 0x00001E3E
		internal static CloudDeletePublishedFileResult Create(IntPtr data, int dataSize)
		{
			return NativeHelpers.ConvertStruct<CloudDeletePublishedFileResult>(data, dataSize);
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000194 RID: 404 RVA: 0x00003C47 File Offset: 0x00001E47
		public Result Result
		{
			get
			{
				return this.result;
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000195 RID: 405 RVA: 0x00003C4F File Offset: 0x00001E4F
		public PublishedFileId PublishedFileId
		{
			get
			{
				return this.publishedFileId;
			}
		}

		// Token: 0x04000107 RID: 263
		private Result result;

		// Token: 0x04000108 RID: 264
		private PublishedFileId publishedFileId;
	}
}
