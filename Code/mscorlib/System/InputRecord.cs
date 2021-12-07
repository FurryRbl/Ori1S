using System;

namespace System
{
	// Token: 0x0200019A RID: 410
	internal struct InputRecord
	{
		// Token: 0x0400082A RID: 2090
		public short EventType;

		// Token: 0x0400082B RID: 2091
		public bool KeyDown;

		// Token: 0x0400082C RID: 2092
		public short RepeatCount;

		// Token: 0x0400082D RID: 2093
		public short VirtualKeyCode;

		// Token: 0x0400082E RID: 2094
		public short VirtualScanCode;

		// Token: 0x0400082F RID: 2095
		public char Character;

		// Token: 0x04000830 RID: 2096
		public int ControlKeyState;

		// Token: 0x04000831 RID: 2097
		private int pad1;

		// Token: 0x04000832 RID: 2098
		private bool pad2;
	}
}
