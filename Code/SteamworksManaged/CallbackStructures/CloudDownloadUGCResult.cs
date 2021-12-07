using System;
using System.Runtime.InteropServices;
using ManagedSteam.SteamTypes;

namespace ManagedSteam.CallbackStructures
{
	// Token: 0x02000031 RID: 49
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct CloudDownloadUGCResult
	{
		// Token: 0x0600016E RID: 366 RVA: 0x00003B03 File Offset: 0x00001D03
		internal static CloudDownloadUGCResult Create(IntPtr dataPointer, int dataSize)
		{
			return NativeHelpers.ConvertStruct<CloudDownloadUGCResult>(dataPointer, dataSize);
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x0600016F RID: 367 RVA: 0x00003B0C File Offset: 0x00001D0C
		public Result Result
		{
			get
			{
				return this.result;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000170 RID: 368 RVA: 0x00003B14 File Offset: 0x00001D14
		public UGCHandle Handle
		{
			get
			{
				return new UGCHandle(this.handle);
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000171 RID: 369 RVA: 0x00003B21 File Offset: 0x00001D21
		public AppID AppID
		{
			get
			{
				return new AppID(this.appID);
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000172 RID: 370 RVA: 0x00003B2E File Offset: 0x00001D2E
		public int Size
		{
			get
			{
				return this.size;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000173 RID: 371 RVA: 0x00003B36 File Offset: 0x00001D36
		public string FileName
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000174 RID: 372 RVA: 0x00003B3E File Offset: 0x00001D3E
		public SteamID CreatorID
		{
			get
			{
				return new SteamID(this.creatorID);
			}
		}

		// Token: 0x040000E6 RID: 230
		private Result result;

		// Token: 0x040000E7 RID: 231
		private ulong handle;

		// Token: 0x040000E8 RID: 232
		private uint appID;

		// Token: 0x040000E9 RID: 233
		private int size;

		// Token: 0x040000EA RID: 234
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
		private string name;

		// Token: 0x040000EB RID: 235
		private ulong creatorID;
	}
}
