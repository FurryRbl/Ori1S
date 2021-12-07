using System;
using System.Runtime.InteropServices;
using ManagedSteam.SteamTypes;

namespace ManagedSteam.CallbackStructures
{
	// Token: 0x0200003E RID: 62
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct CloudSetUserPublishedFileActionResult
	{
		// Token: 0x060001BA RID: 442 RVA: 0x00003D7F File Offset: 0x00001F7F
		internal static CloudSetUserPublishedFileActionResult Create(IntPtr data, int dataSize)
		{
			return NativeHelpers.ConvertStruct<CloudSetUserPublishedFileActionResult>(data, dataSize);
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x060001BB RID: 443 RVA: 0x00003D88 File Offset: 0x00001F88
		public Result Result
		{
			get
			{
				return this.result;
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060001BC RID: 444 RVA: 0x00003D90 File Offset: 0x00001F90
		public PublishedFileId PublishedFileId
		{
			get
			{
				return this.publishedFileId;
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x060001BD RID: 445 RVA: 0x00003D98 File Offset: 0x00001F98
		public WorkshopFileAction Action
		{
			get
			{
				return this.action;
			}
		}

		// Token: 0x04000125 RID: 293
		private Result result;

		// Token: 0x04000126 RID: 294
		private PublishedFileId publishedFileId;

		// Token: 0x04000127 RID: 295
		private WorkshopFileAction action;
	}
}
