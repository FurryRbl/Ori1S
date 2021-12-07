using System;
using System.Runtime.InteropServices;

namespace ManagedSteam.CallbackStructures
{
	// Token: 0x0200014B RID: 331
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct DownloadClanActivityCountsResult
	{
		// Token: 0x06000B97 RID: 2967 RVA: 0x0000FB52 File Offset: 0x0000DD52
		internal static DownloadClanActivityCountsResult Create(IntPtr data, int dataSize)
		{
			return NativeHelpers.ConvertStruct<DownloadClanActivityCountsResult>(data, dataSize);
		}

		// Token: 0x17000195 RID: 405
		// (get) Token: 0x06000B98 RID: 2968 RVA: 0x0000FB5B File Offset: 0x0000DD5B
		public bool Success
		{
			get
			{
				return this.success;
			}
		}

		// Token: 0x040005DF RID: 1503
		[MarshalAs(UnmanagedType.I1)]
		private bool success;
	}
}
