using System;
using UnityEngine;

// Token: 0x02000392 RID: 914
public static class UberScreenManager
{
	// Token: 0x060019CE RID: 6606 RVA: 0x0006E684 File Offset: 0x0006C884
	public static float ConvertScreenTweak(float raw)
	{
		return -1f / (2f + raw);
	}

	// Token: 0x060019CF RID: 6607 RVA: 0x0006E694 File Offset: 0x0006C894
	public static Vector4 GetScreen(UberScreenMode mode, float tweak)
	{
		if (mode == UberScreenMode.None)
		{
			return Vector4.zero;
		}
		Vector4 result = Vector4.zero;
		tweak = UberScreenManager.ConvertScreenTweak(tweak);
		switch (mode)
		{
		case UberScreenMode.Red:
			result = new Vector4(1f, tweak, tweak, 0f);
			break;
		case UberScreenMode.Green:
			result = new Vector4(tweak, 1f, tweak, 0f);
			break;
		case UberScreenMode.Blue:
			result = new Vector4(tweak, tweak, 1f, 0f);
			break;
		case UberScreenMode.None:
			result = Vector4.one;
			break;
		}
		return result;
	}

	// Token: 0x060019D0 RID: 6608 RVA: 0x0006E730 File Offset: 0x0006C930
	public static Vector4 GetScreenMask(UberScreenMode mode)
	{
		if (mode == UberScreenMode.None)
		{
			return Vector4.zero;
		}
		Vector4 zero = Vector4.zero;
		switch (mode)
		{
		case UberScreenMode.Red:
			zero.x = -1f;
			break;
		case UberScreenMode.Green:
			zero.y = -1f;
			break;
		case UberScreenMode.Blue:
			zero.z = -1f;
			break;
		case UberScreenMode.None:
			zero = Vector4.zero;
			break;
		}
		zero.w = -1f;
		return zero;
	}
}
