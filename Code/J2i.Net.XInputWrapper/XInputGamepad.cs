using System;
using System.Runtime.InteropServices;

namespace J2i.Net.XInputWrapper
{
	// Token: 0x0200000F RID: 15
	[StructLayout(LayoutKind.Explicit)]
	public struct XInputGamepad
	{
		// Token: 0x0600003E RID: 62 RVA: 0x0000273C File Offset: 0x0000093C
		public bool IsButtonPressed(int buttonFlags)
		{
			return ((int)this.wButtons & buttonFlags) == buttonFlags;
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002749 File Offset: 0x00000949
		public bool IsButtonPresent(int buttonFlags)
		{
			return ((int)this.wButtons & buttonFlags) == buttonFlags;
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002758 File Offset: 0x00000958
		public void Copy(XInputGamepad source)
		{
			this.sThumbLX = source.sThumbLX;
			this.sThumbLY = source.sThumbLY;
			this.sThumbRX = source.sThumbRX;
			this.sThumbRY = source.sThumbRY;
			this.bLeftTrigger = source.bLeftTrigger;
			this.bRightTrigger = source.bRightTrigger;
			this.wButtons = source.wButtons;
		}

		// Token: 0x06000041 RID: 65 RVA: 0x000027C0 File Offset: 0x000009C0
		public override bool Equals(object obj)
		{
			if (!(obj is XInputGamepad))
			{
				return false;
			}
			XInputGamepad xinputGamepad = (XInputGamepad)obj;
			return this.sThumbLX == xinputGamepad.sThumbLX && this.sThumbLY == xinputGamepad.sThumbLY && this.sThumbRX == xinputGamepad.sThumbRX && this.sThumbRY == xinputGamepad.sThumbRY && this.bLeftTrigger == xinputGamepad.bLeftTrigger && this.bRightTrigger == xinputGamepad.bRightTrigger && this.wButtons == xinputGamepad.wButtons;
		}

		// Token: 0x04000053 RID: 83
		[FieldOffset(0)]
		[MarshalAs(UnmanagedType.I2)]
		public short wButtons;

		// Token: 0x04000054 RID: 84
		[FieldOffset(2)]
		[MarshalAs(UnmanagedType.I1)]
		public byte bLeftTrigger;

		// Token: 0x04000055 RID: 85
		[FieldOffset(3)]
		[MarshalAs(UnmanagedType.I1)]
		public byte bRightTrigger;

		// Token: 0x04000056 RID: 86
		[FieldOffset(4)]
		[MarshalAs(UnmanagedType.I2)]
		public short sThumbLX;

		// Token: 0x04000057 RID: 87
		[FieldOffset(6)]
		[MarshalAs(UnmanagedType.I2)]
		public short sThumbLY;

		// Token: 0x04000058 RID: 88
		[FieldOffset(8)]
		[MarshalAs(UnmanagedType.I2)]
		public short sThumbRX;

		// Token: 0x04000059 RID: 89
		[FieldOffset(10)]
		[MarshalAs(UnmanagedType.I2)]
		public short sThumbRY;
	}
}
