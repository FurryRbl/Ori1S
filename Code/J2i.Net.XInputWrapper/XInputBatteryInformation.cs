using System;
using System.Runtime.InteropServices;

namespace J2i.Net.XInputWrapper
{
	// Token: 0x02000006 RID: 6
	[StructLayout(LayoutKind.Explicit)]
	public struct XInputBatteryInformation
	{
		// Token: 0x0600003C RID: 60 RVA: 0x00002712 File Offset: 0x00000912
		public override string ToString()
		{
			return string.Format("{0} {1}", (BatteryTypes)this.BatteryType, (BatteryLevel)this.BatteryLevel);
		}

		// Token: 0x04000018 RID: 24
		[FieldOffset(0)]
		[MarshalAs(UnmanagedType.I1)]
		public byte BatteryType;

		// Token: 0x04000019 RID: 25
		[FieldOffset(1)]
		[MarshalAs(UnmanagedType.I1)]
		public byte BatteryLevel;
	}
}
