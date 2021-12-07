using System;
using System.Runtime.InteropServices;

namespace ManagedSteam.CallbackStructures
{
	// Token: 0x02000041 RID: 65
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct CloudPublishFileProgress
	{
		// Token: 0x060001CD RID: 461 RVA: 0x00003E1A File Offset: 0x0000201A
		internal static CloudPublishFileProgress Create(IntPtr data, int dataSize)
		{
			return NativeHelpers.ConvertStruct<CloudPublishFileProgress>(data, dataSize);
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x060001CE RID: 462 RVA: 0x00003E23 File Offset: 0x00002023
		public double PercentFile
		{
			get
			{
				return this.percentFile;
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x060001CF RID: 463 RVA: 0x00003E2B File Offset: 0x0000202B
		public bool Preview
		{
			get
			{
				return this.preview;
			}
		}

		// Token: 0x04000135 RID: 309
		private double percentFile;

		// Token: 0x04000136 RID: 310
		private bool preview;
	}
}
