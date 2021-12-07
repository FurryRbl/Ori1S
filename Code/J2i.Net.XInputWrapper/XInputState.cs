using System;
using System.Runtime.InteropServices;

namespace J2i.Net.XInputWrapper
{
	// Token: 0x02000011 RID: 17
	[StructLayout(LayoutKind.Explicit)]
	public struct XInputState
	{
		// Token: 0x06000042 RID: 66 RVA: 0x00002849 File Offset: 0x00000A49
		public void Copy(XInputState source)
		{
			this.PacketNumber = source.PacketNumber;
			this.Gamepad.Copy(source.Gamepad);
		}

		// Token: 0x06000043 RID: 67 RVA: 0x0000286C File Offset: 0x00000A6C
		public override bool Equals(object obj)
		{
			if (obj == null || !(obj is XInputState))
			{
				return false;
			}
			XInputState xinputState = (XInputState)obj;
			return this.PacketNumber == xinputState.PacketNumber && this.Gamepad.Equals(xinputState.Gamepad);
		}

		// Token: 0x0400005F RID: 95
		[FieldOffset(0)]
		public int PacketNumber;

		// Token: 0x04000060 RID: 96
		[FieldOffset(4)]
		public XInputGamepad Gamepad;
	}
}
