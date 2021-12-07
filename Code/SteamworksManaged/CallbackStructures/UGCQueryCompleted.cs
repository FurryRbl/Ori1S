using System;
using System.Runtime.InteropServices;
using ManagedSteam.SteamTypes;

namespace ManagedSteam.CallbackStructures
{
	// Token: 0x02000095 RID: 149
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct UGCQueryCompleted
	{
		// Token: 0x0600047A RID: 1146 RVA: 0x00008252 File Offset: 0x00006452
		internal static UGCQueryCompleted Create(IntPtr data, int dataSize)
		{
			return NativeHelpers.ConvertStruct<UGCQueryCompleted>(data, dataSize);
		}

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x0600047B RID: 1147 RVA: 0x0000825B File Offset: 0x0000645B
		private UGCQueryHandle Handle
		{
			get
			{
				return this.handle;
			}
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x0600047C RID: 1148 RVA: 0x00008263 File Offset: 0x00006463
		private Result Result
		{
			get
			{
				return this.result;
			}
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x0600047D RID: 1149 RVA: 0x0000826B File Offset: 0x0000646B
		private uint NumResultsReturned
		{
			get
			{
				return this.numResultsReturned;
			}
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x0600047E RID: 1150 RVA: 0x00008273 File Offset: 0x00006473
		private uint TotalMatchingResults
		{
			get
			{
				return this.totalMatchingResults;
			}
		}

		// Token: 0x040002A0 RID: 672
		private UGCQueryHandle handle;

		// Token: 0x040002A1 RID: 673
		private Result result;

		// Token: 0x040002A2 RID: 674
		private uint numResultsReturned;

		// Token: 0x040002A3 RID: 675
		private uint totalMatchingResults;
	}
}
