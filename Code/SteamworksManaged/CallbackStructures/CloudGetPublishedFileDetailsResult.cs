using System;
using System.Runtime.InteropServices;
using ManagedSteam.SteamTypes;

namespace ManagedSteam.CallbackStructures
{
	// Token: 0x02000034 RID: 52
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct CloudGetPublishedFileDetailsResult
	{
		// Token: 0x0600017D RID: 381 RVA: 0x00003B8D File Offset: 0x00001D8D
		internal static CloudGetPublishedFileDetailsResult Create(IntPtr data, int dataSize)
		{
			return NativeHelpers.ConvertStruct<CloudGetPublishedFileDetailsResult>(data, dataSize);
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x0600017E RID: 382 RVA: 0x00003B96 File Offset: 0x00001D96
		public Result Result
		{
			get
			{
				return this.result;
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x0600017F RID: 383 RVA: 0x00003B9E File Offset: 0x00001D9E
		public PublishedFileId PublishedFileId
		{
			get
			{
				return this.publishedFileId;
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000180 RID: 384 RVA: 0x00003BA6 File Offset: 0x00001DA6
		public AppID CreatorAppID
		{
			get
			{
				return this.creatorAppID;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000181 RID: 385 RVA: 0x00003BAE File Offset: 0x00001DAE
		public AppID ConsumerAppID
		{
			get
			{
				return this.consumerAppID;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000182 RID: 386 RVA: 0x00003BB6 File Offset: 0x00001DB6
		public string Title
		{
			get
			{
				return this.title;
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000183 RID: 387 RVA: 0x00003BBE File Offset: 0x00001DBE
		public string Description
		{
			get
			{
				return this.description;
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000184 RID: 388 RVA: 0x00003BC6 File Offset: 0x00001DC6
		public UGCHandle File
		{
			get
			{
				return this.file;
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x06000185 RID: 389 RVA: 0x00003BCE File Offset: 0x00001DCE
		public UGCHandle PreviewFile
		{
			get
			{
				return this.previewFile;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x06000186 RID: 390 RVA: 0x00003BD6 File Offset: 0x00001DD6
		public SteamID SteamIDOwner
		{
			get
			{
				return this.steamIDOwner;
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000187 RID: 391 RVA: 0x00003BDE File Offset: 0x00001DDE
		public uint TimeCreated
		{
			get
			{
				return this.timeCreated;
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000188 RID: 392 RVA: 0x00003BE6 File Offset: 0x00001DE6
		public uint TimeUpdated
		{
			get
			{
				return this.timeUpdated;
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000189 RID: 393 RVA: 0x00003BEE File Offset: 0x00001DEE
		public RemoteStoragePublishedFileVisibility Visibility
		{
			get
			{
				return this.visibility;
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x0600018A RID: 394 RVA: 0x00003BF6 File Offset: 0x00001DF6
		public bool Banned
		{
			get
			{
				return this.banned;
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x0600018B RID: 395 RVA: 0x00003BFE File Offset: 0x00001DFE
		public string Tags
		{
			get
			{
				return this.tags;
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x0600018C RID: 396 RVA: 0x00003C06 File Offset: 0x00001E06
		public bool TagsTruncated
		{
			get
			{
				return this.tagsTruncated;
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x0600018D RID: 397 RVA: 0x00003C0E File Offset: 0x00001E0E
		public string FileName
		{
			get
			{
				return this.fileName;
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x0600018E RID: 398 RVA: 0x00003C16 File Offset: 0x00001E16
		public int FileSize
		{
			get
			{
				return this.fileSize;
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x0600018F RID: 399 RVA: 0x00003C1E File Offset: 0x00001E1E
		public int PreviewFileSize
		{
			get
			{
				return this.previewFileSize;
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000190 RID: 400 RVA: 0x00003C26 File Offset: 0x00001E26
		public string Url
		{
			get
			{
				return this.url;
			}
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000191 RID: 401 RVA: 0x00003C2E File Offset: 0x00001E2E
		public WorkshopFileType FileType
		{
			get
			{
				return this.fileType;
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000192 RID: 402 RVA: 0x00003C36 File Offset: 0x00001E36
		public bool AcceptedForUse
		{
			get
			{
				return this.acceptedForUse;
			}
		}

		// Token: 0x040000F2 RID: 242
		private Result result;

		// Token: 0x040000F3 RID: 243
		private PublishedFileId publishedFileId;

		// Token: 0x040000F4 RID: 244
		private AppID creatorAppID;

		// Token: 0x040000F5 RID: 245
		private AppID consumerAppID;

		// Token: 0x040000F6 RID: 246
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 129)]
		private string title;

		// Token: 0x040000F7 RID: 247
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 8000)]
		private string description;

		// Token: 0x040000F8 RID: 248
		private UGCHandle file;

		// Token: 0x040000F9 RID: 249
		private UGCHandle previewFile;

		// Token: 0x040000FA RID: 250
		private SteamID steamIDOwner;

		// Token: 0x040000FB RID: 251
		private uint timeCreated;

		// Token: 0x040000FC RID: 252
		private uint timeUpdated;

		// Token: 0x040000FD RID: 253
		private RemoteStoragePublishedFileVisibility visibility;

		// Token: 0x040000FE RID: 254
		[MarshalAs(UnmanagedType.I1)]
		private bool banned;

		// Token: 0x040000FF RID: 255
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 1025)]
		private string tags;

		// Token: 0x04000100 RID: 256
		[MarshalAs(UnmanagedType.I1)]
		private bool tagsTruncated;

		// Token: 0x04000101 RID: 257
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
		private string fileName;

		// Token: 0x04000102 RID: 258
		private int fileSize;

		// Token: 0x04000103 RID: 259
		private int previewFileSize;

		// Token: 0x04000104 RID: 260
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
		private string url;

		// Token: 0x04000105 RID: 261
		private WorkshopFileType fileType;

		// Token: 0x04000106 RID: 262
		[MarshalAs(UnmanagedType.I1)]
		private bool acceptedForUse;
	}
}
