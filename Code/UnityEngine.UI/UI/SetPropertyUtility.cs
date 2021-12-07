using System;

namespace UnityEngine.UI
{
	// Token: 0x02000072 RID: 114
	internal static class SetPropertyUtility
	{
		// Token: 0x06000419 RID: 1049 RVA: 0x00013A04 File Offset: 0x00011C04
		public static bool SetColor(ref Color currentValue, Color newValue)
		{
			if (currentValue.r == newValue.r && currentValue.g == newValue.g && currentValue.b == newValue.b && currentValue.a == newValue.a)
			{
				return false;
			}
			currentValue = newValue;
			return true;
		}

		// Token: 0x0600041A RID: 1050 RVA: 0x00013A64 File Offset: 0x00011C64
		public static bool SetStruct<T>(ref T currentValue, T newValue) where T : struct
		{
			if (currentValue.Equals(newValue))
			{
				return false;
			}
			currentValue = newValue;
			return true;
		}

		// Token: 0x0600041B RID: 1051 RVA: 0x00013A88 File Offset: 0x00011C88
		public static bool SetClass<T>(ref T currentValue, T newValue) where T : class
		{
			if ((currentValue == null && newValue == null) || (currentValue != null && currentValue.Equals(newValue)))
			{
				return false;
			}
			currentValue = newValue;
			return true;
		}
	}
}
