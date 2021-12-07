using System;

namespace UnityEngine.Networking
{
	// Token: 0x02000037 RID: 55
	internal class FloatConversion
	{
		// Token: 0x0600014A RID: 330 RVA: 0x000070D8 File Offset: 0x000052D8
		public static float ToSingle(uint value)
		{
			UIntFloat uintFloat = default(UIntFloat);
			uintFloat.intValue = value;
			return uintFloat.floatValue;
		}

		// Token: 0x0600014B RID: 331 RVA: 0x000070FC File Offset: 0x000052FC
		public static double ToDouble(ulong value)
		{
			UIntFloat uintFloat = default(UIntFloat);
			uintFloat.longValue = value;
			return uintFloat.doubleValue;
		}
	}
}
