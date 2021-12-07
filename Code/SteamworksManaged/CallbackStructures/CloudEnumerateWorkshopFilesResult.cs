using System;
using System.Runtime.InteropServices;
using ManagedSteam.SteamTypes;

namespace ManagedSteam.CallbackStructures
{
	// Token: 0x02000040 RID: 64
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct CloudEnumerateWorkshopFilesResult
	{
		// Token: 0x060001C5 RID: 453 RVA: 0x00003DD9 File Offset: 0x00001FD9
		internal static CloudEnumerateWorkshopFilesResult Create(IntPtr data, int dataSize)
		{
			return NativeHelpers.ConvertStruct<CloudEnumerateWorkshopFilesResult>(data, dataSize);
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x060001C6 RID: 454 RVA: 0x00003DE2 File Offset: 0x00001FE2
		public Result Result
		{
			get
			{
				return this.result;
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x060001C7 RID: 455 RVA: 0x00003DEA File Offset: 0x00001FEA
		public int ResultsReturned
		{
			get
			{
				return this.resultsReturned;
			}
		}

		// Token: 0x17000073 RID: 115
		// (get) Token: 0x060001C8 RID: 456 RVA: 0x00003DF2 File Offset: 0x00001FF2
		public int TotalResultCount
		{
			get
			{
				return this.totalResultCount;
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x060001C9 RID: 457 RVA: 0x00003DFA File Offset: 0x00001FFA
		public PublishedFileId[] PublishedFileId
		{
			get
			{
				return this.publishedFileId;
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x060001CA RID: 458 RVA: 0x00003E02 File Offset: 0x00002002
		public float[] Score
		{
			get
			{
				return this.score;
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x060001CB RID: 459 RVA: 0x00003E0A File Offset: 0x0000200A
		public AppID AppID
		{
			get
			{
				return this.appId;
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x060001CC RID: 460 RVA: 0x00003E12 File Offset: 0x00002012
		public uint StartIndex
		{
			get
			{
				return this.startIndex;
			}
		}

		// Token: 0x0400012E RID: 302
		private Result result;

		// Token: 0x0400012F RID: 303
		private int resultsReturned;

		// Token: 0x04000130 RID: 304
		private int totalResultCount;

		// Token: 0x04000131 RID: 305
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 50)]
		private PublishedFileId[] publishedFileId;

		// Token: 0x04000132 RID: 306
		[MarshalAs(UnmanagedType.ByValArray, SizeConst = 50)]
		private float[] score;

		// Token: 0x04000133 RID: 307
		private AppID appId;

		// Token: 0x04000134 RID: 308
		private uint startIndex;
	}
}
