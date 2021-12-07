using System;
using System.Runtime.InteropServices;
using ManagedSteam.SteamTypes;

namespace ManagedSteam.CallbackStructures
{
	// Token: 0x0200010C RID: 268
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct AssociateWithClanResult
	{
		// Token: 0x060007CB RID: 1995 RVA: 0x0000B79C File Offset: 0x0000999C
		internal static AssociateWithClanResult Create(IntPtr data, int dataSize)
		{
			return NativeHelpers.ConvertStruct<AssociateWithClanResult>(data, dataSize);
		}

		// Token: 0x17000154 RID: 340
		// (get) Token: 0x060007CC RID: 1996 RVA: 0x0000B7A5 File Offset: 0x000099A5
		public Result Result
		{
			get
			{
				return this.result;
			}
		}

		// Token: 0x040004AE RID: 1198
		private Result result;
	}
}
