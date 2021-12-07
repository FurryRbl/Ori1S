using System;
using System.Runtime.InteropServices;

namespace ManagedSteam.CallbackStructures
{
	// Token: 0x02000167 RID: 359
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct GamepadTextInputDismissed
	{
		// Token: 0x06000BE6 RID: 3046 RVA: 0x000102A1 File Offset: 0x0000E4A1
		internal static GamepadTextInputDismissed Create(IntPtr data, int dataSize)
		{
			return NativeHelpers.ConvertStruct<GamepadTextInputDismissed>(data, dataSize);
		}

		// Token: 0x170001B1 RID: 433
		// (get) Token: 0x06000BE7 RID: 3047 RVA: 0x000102AA File Offset: 0x0000E4AA
		public bool Submitted
		{
			get
			{
				return this.submitted;
			}
		}

		// Token: 0x04000647 RID: 1607
		private bool submitted;

		// Token: 0x04000648 RID: 1608
		private uint submittedText;
	}
}
