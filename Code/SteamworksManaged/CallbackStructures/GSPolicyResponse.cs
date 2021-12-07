using System;
using System.Runtime.InteropServices;

namespace ManagedSteam.CallbackStructures
{
	// Token: 0x02000108 RID: 264
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct GSPolicyResponse
	{
		// Token: 0x060007B7 RID: 1975 RVA: 0x0000B6F2 File Offset: 0x000098F2
		internal static GSPolicyResponse Create(IntPtr data, int dataSize)
		{
			return NativeHelpers.ConvertStruct<GSPolicyResponse>(data, dataSize);
		}

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x060007B8 RID: 1976 RVA: 0x0000B6FB File Offset: 0x000098FB
		public bool Secure
		{
			get
			{
				return this.secure != 0;
			}
		}

		// Token: 0x0400049E RID: 1182
		private byte secure;
	}
}
