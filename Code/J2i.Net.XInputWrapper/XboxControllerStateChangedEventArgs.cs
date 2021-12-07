using System;

namespace J2i.Net.XInputWrapper
{
	// Token: 0x02000004 RID: 4
	public class XboxControllerStateChangedEventArgs : EventArgs
	{
		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000034 RID: 52 RVA: 0x000026E8 File Offset: 0x000008E8
		// (set) Token: 0x06000035 RID: 53 RVA: 0x000026F0 File Offset: 0x000008F0
		public XInputState CurrentInputState { get; set; }

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000036 RID: 54 RVA: 0x000026F9 File Offset: 0x000008F9
		// (set) Token: 0x06000037 RID: 55 RVA: 0x00002701 File Offset: 0x00000901
		public XInputState PreviousInputState { get; set; }
	}
}
