using System;
using System.Runtime.InteropServices;
using ManagedSteam.SteamTypes;

namespace ManagedSteam.CallbackStructures
{
	// Token: 0x02000109 RID: 265
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct GSGameplayStats
	{
		// Token: 0x060007B9 RID: 1977 RVA: 0x0000B709 File Offset: 0x00009909
		internal static GSGameplayStats Create(IntPtr data, int dataSize)
		{
			return NativeHelpers.ConvertStruct<GSGameplayStats>(data, dataSize);
		}

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x060007BA RID: 1978 RVA: 0x0000B712 File Offset: 0x00009912
		public Result Result
		{
			get
			{
				return this.result;
			}
		}

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x060007BB RID: 1979 RVA: 0x0000B71A File Offset: 0x0000991A
		public int Rank
		{
			get
			{
				return this.rank;
			}
		}

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x060007BC RID: 1980 RVA: 0x0000B722 File Offset: 0x00009922
		public uint TotalConnects
		{
			get
			{
				return this.totalConnects;
			}
		}

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x060007BD RID: 1981 RVA: 0x0000B72A File Offset: 0x0000992A
		public uint TotalMinutesPlayed
		{
			get
			{
				return this.totalMinutesPlayed;
			}
		}

		// Token: 0x0400049F RID: 1183
		private Result result;

		// Token: 0x040004A0 RID: 1184
		private int rank;

		// Token: 0x040004A1 RID: 1185
		private uint totalConnects;

		// Token: 0x040004A2 RID: 1186
		private uint totalMinutesPlayed;
	}
}
