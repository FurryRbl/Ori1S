using System;
using System.Runtime.InteropServices;

namespace ManagedSteam.SteamTypes
{
	// Token: 0x020000FB RID: 251
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct UGCDetails
	{
		// Token: 0x0600075F RID: 1887 RVA: 0x0000B261 File Offset: 0x00009461
		internal static UGCDetails Create(IntPtr data, int dataSize)
		{
			return NativeHelpers.ConvertStruct<UGCDetails>(data, dataSize);
		}

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x06000760 RID: 1888 RVA: 0x0000B26A File Offset: 0x0000946A
		public PublishedFileId PublishedFileId
		{
			get
			{
				return this.publishedFileId;
			}
		}

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x06000761 RID: 1889 RVA: 0x0000B272 File Offset: 0x00009472
		public Result Result
		{
			get
			{
				return this.result;
			}
		}

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x06000762 RID: 1890 RVA: 0x0000B27A File Offset: 0x0000947A
		private WorkshopFileType WorkshopFileType
		{
			get
			{
				return this.workshopFileType;
			}
		}

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x06000763 RID: 1891 RVA: 0x0000B282 File Offset: 0x00009482
		private AppID CreatorAppID
		{
			get
			{
				return this.creatorAppID;
			}
		}

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x06000764 RID: 1892 RVA: 0x0000B28A File Offset: 0x0000948A
		private AppID ConsumerAppID
		{
			get
			{
				return this.ConsumerAppID;
			}
		}

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x06000765 RID: 1893 RVA: 0x0000B292 File Offset: 0x00009492
		private string Title
		{
			get
			{
				return this.title;
			}
		}

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x06000766 RID: 1894 RVA: 0x0000B29A File Offset: 0x0000949A
		private string Description
		{
			get
			{
				return this.description;
			}
		}

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x06000767 RID: 1895 RVA: 0x0000B2A2 File Offset: 0x000094A2
		private SteamID SteamIDOwner
		{
			get
			{
				return this.steamIDOwner;
			}
		}

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x06000768 RID: 1896 RVA: 0x0000B2AA File Offset: 0x000094AA
		private uint TimeCreated
		{
			get
			{
				return this.timeCreated;
			}
		}

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x06000769 RID: 1897 RVA: 0x0000B2B2 File Offset: 0x000094B2
		private uint TimeUpdated
		{
			get
			{
				return this.timeUpdated;
			}
		}

		// Token: 0x17000128 RID: 296
		// (get) Token: 0x0600076A RID: 1898 RVA: 0x0000B2BA File Offset: 0x000094BA
		private uint TimeAddedToUserList
		{
			get
			{
				return this.timeAddedToUserList;
			}
		}

		// Token: 0x17000129 RID: 297
		// (get) Token: 0x0600076B RID: 1899 RVA: 0x0000B2C2 File Offset: 0x000094C2
		private RemoteStoragePublishedFileVisibility Visibility
		{
			get
			{
				return this.visibility;
			}
		}

		// Token: 0x1700012A RID: 298
		// (get) Token: 0x0600076C RID: 1900 RVA: 0x0000B2CA File Offset: 0x000094CA
		private bool Banned
		{
			get
			{
				return this.banned;
			}
		}

		// Token: 0x1700012B RID: 299
		// (get) Token: 0x0600076D RID: 1901 RVA: 0x0000B2D2 File Offset: 0x000094D2
		private bool AcceptedForUse
		{
			get
			{
				return this.acceptedForUse;
			}
		}

		// Token: 0x1700012C RID: 300
		// (get) Token: 0x0600076E RID: 1902 RVA: 0x0000B2DA File Offset: 0x000094DA
		private bool TagsTruncated
		{
			get
			{
				return this.tagsTruncated;
			}
		}

		// Token: 0x1700012D RID: 301
		// (get) Token: 0x0600076F RID: 1903 RVA: 0x0000B2E2 File Offset: 0x000094E2
		private string Tags
		{
			get
			{
				return this.tags;
			}
		}

		// Token: 0x1700012E RID: 302
		// (get) Token: 0x06000770 RID: 1904 RVA: 0x0000B2EA File Offset: 0x000094EA
		private UGCHandle File
		{
			get
			{
				return this.file;
			}
		}

		// Token: 0x1700012F RID: 303
		// (get) Token: 0x06000771 RID: 1905 RVA: 0x0000B2F2 File Offset: 0x000094F2
		private UGCHandle PreviewFile
		{
			get
			{
				return this.previewFile;
			}
		}

		// Token: 0x17000130 RID: 304
		// (get) Token: 0x06000772 RID: 1906 RVA: 0x0000B2FA File Offset: 0x000094FA
		private string FileName
		{
			get
			{
				return this.fileName;
			}
		}

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x06000773 RID: 1907 RVA: 0x0000B302 File Offset: 0x00009502
		private int FileSize
		{
			get
			{
				return this.fileSize;
			}
		}

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x06000774 RID: 1908 RVA: 0x0000B30A File Offset: 0x0000950A
		private int PreviewFileSize
		{
			get
			{
				return this.previewFileSize;
			}
		}

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x06000775 RID: 1909 RVA: 0x0000B312 File Offset: 0x00009512
		private string URL
		{
			get
			{
				return this.url;
			}
		}

		// Token: 0x17000134 RID: 308
		// (get) Token: 0x06000776 RID: 1910 RVA: 0x0000B31A File Offset: 0x0000951A
		private uint VotesUp
		{
			get
			{
				return this.votesUp;
			}
		}

		// Token: 0x17000135 RID: 309
		// (get) Token: 0x06000777 RID: 1911 RVA: 0x0000B322 File Offset: 0x00009522
		private uint VotesDown
		{
			get
			{
				return this.votesDown;
			}
		}

		// Token: 0x17000136 RID: 310
		// (get) Token: 0x06000778 RID: 1912 RVA: 0x0000B32A File Offset: 0x0000952A
		private float Score
		{
			get
			{
				return this.score;
			}
		}

		// Token: 0x04000448 RID: 1096
		private PublishedFileId publishedFileId;

		// Token: 0x04000449 RID: 1097
		private Result result;

		// Token: 0x0400044A RID: 1098
		private WorkshopFileType workshopFileType;

		// Token: 0x0400044B RID: 1099
		private AppID creatorAppID;

		// Token: 0x0400044C RID: 1100
		private AppID consumerAppID;

		// Token: 0x0400044D RID: 1101
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 129)]
		private string title;

		// Token: 0x0400044E RID: 1102
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 8000)]
		private string description;

		// Token: 0x0400044F RID: 1103
		private SteamID steamIDOwner;

		// Token: 0x04000450 RID: 1104
		private uint timeCreated;

		// Token: 0x04000451 RID: 1105
		private uint timeUpdated;

		// Token: 0x04000452 RID: 1106
		private uint timeAddedToUserList;

		// Token: 0x04000453 RID: 1107
		private RemoteStoragePublishedFileVisibility visibility;

		// Token: 0x04000454 RID: 1108
		[MarshalAs(UnmanagedType.I1)]
		private bool banned;

		// Token: 0x04000455 RID: 1109
		[MarshalAs(UnmanagedType.I1)]
		private bool acceptedForUse;

		// Token: 0x04000456 RID: 1110
		[MarshalAs(UnmanagedType.I1)]
		private bool tagsTruncated;

		// Token: 0x04000457 RID: 1111
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 1025)]
		private string tags;

		// Token: 0x04000458 RID: 1112
		private UGCHandle file;

		// Token: 0x04000459 RID: 1113
		private UGCHandle previewFile;

		// Token: 0x0400045A RID: 1114
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
		private string fileName;

		// Token: 0x0400045B RID: 1115
		private int fileSize;

		// Token: 0x0400045C RID: 1116
		private int previewFileSize;

		// Token: 0x0400045D RID: 1117
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 256)]
		private string url;

		// Token: 0x0400045E RID: 1118
		private uint votesUp;

		// Token: 0x0400045F RID: 1119
		private uint votesDown;

		// Token: 0x04000460 RID: 1120
		private float score;
	}
}
