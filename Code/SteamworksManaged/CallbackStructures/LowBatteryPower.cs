using System;
using System.Runtime.InteropServices;

namespace ManagedSteam.CallbackStructures
{
	// Token: 0x02000163 RID: 355
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct LowBatteryPower
	{
		// Token: 0x06000BDF RID: 3039 RVA: 0x00010265 File Offset: 0x0000E465
		internal static LowBatteryPower Create(IntPtr data, int dataSize)
		{
			return NativeHelpers.ConvertStruct<LowBatteryPower>(data, dataSize);
		}

		// Token: 0x170001AE RID: 430
		// (get) Token: 0x06000BE0 RID: 3040 RVA: 0x0001026E File Offset: 0x0000E46E
		public int MinutesBatteryLeft
		{
			get
			{
				return (int)this.minutesBatteryLeft;
			}
		}

		// Token: 0x04000643 RID: 1603
		private byte minutesBatteryLeft;
	}
}
