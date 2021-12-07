using System;
using System.Runtime.InteropServices;

namespace ManagedSteam.CallbackStructures
{
	// Token: 0x02000141 RID: 321
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct GameOverlayActivated
	{
		// Token: 0x06000B74 RID: 2932 RVA: 0x0000FA1F File Offset: 0x0000DC1F
		internal static GameOverlayActivated Create(IntPtr data, int dataSize)
		{
			return NativeHelpers.ConvertStruct<GameOverlayActivated>(data, dataSize);
		}

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x06000B75 RID: 2933 RVA: 0x0000FA28 File Offset: 0x0000DC28
		public bool Active
		{
			get
			{
				return this.active != 0;
			}
		}

		// Token: 0x040005C6 RID: 1478
		private byte active;
	}
}
