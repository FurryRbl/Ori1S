using System;
using UnityEngine;

// Token: 0x020000FA RID: 250
public class OnScreenPositions : MonoSingleton<OnScreenPositions>
{
	// Token: 0x060009DD RID: 2525 RVA: 0x0002B223 File Offset: 0x00029423
	private void Start()
	{
	}

	// Token: 0x17000216 RID: 534
	// (get) Token: 0x060009DE RID: 2526 RVA: 0x0002B228 File Offset: 0x00029428
	public static Vector3 TopLeft
	{
		get
		{
			if (MonoSingleton<OnScreenPositions>.Instance.m_topLeft == Vector3.zero)
			{
				MonoSingleton<OnScreenPositions>.Instance.m_topLeft = MonoSingleton<OnScreenPositions>.Instance.TopLeftTransform.position;
			}
			return MonoSingleton<OnScreenPositions>.Instance.m_topLeft;
		}
	}

	// Token: 0x17000217 RID: 535
	// (get) Token: 0x060009DF RID: 2527 RVA: 0x0002B274 File Offset: 0x00029474
	public static Vector3 TopCenter
	{
		get
		{
			if (MonoSingleton<OnScreenPositions>.Instance.m_topCenter == Vector3.zero)
			{
				MonoSingleton<OnScreenPositions>.Instance.m_topCenter = MonoSingleton<OnScreenPositions>.Instance.TopCenterTransform.position;
			}
			return MonoSingleton<OnScreenPositions>.Instance.m_topCenter;
		}
	}

	// Token: 0x17000218 RID: 536
	// (get) Token: 0x060009E0 RID: 2528 RVA: 0x0002B2C0 File Offset: 0x000294C0
	public static Vector3 TopRight
	{
		get
		{
			if (MonoSingleton<OnScreenPositions>.Instance.m_topRight == Vector3.zero)
			{
				MonoSingleton<OnScreenPositions>.Instance.m_topRight = MonoSingleton<OnScreenPositions>.Instance.TopRightTransform.position;
			}
			return MonoSingleton<OnScreenPositions>.Instance.m_topRight;
		}
	}

	// Token: 0x17000219 RID: 537
	// (get) Token: 0x060009E1 RID: 2529 RVA: 0x0002B30C File Offset: 0x0002950C
	public static Vector3 MiddleLeft
	{
		get
		{
			if (MonoSingleton<OnScreenPositions>.Instance.m_middleLeft == Vector3.zero)
			{
				MonoSingleton<OnScreenPositions>.Instance.m_middleLeft = MonoSingleton<OnScreenPositions>.Instance.MiddleLeftTransform.position;
			}
			return MonoSingleton<OnScreenPositions>.Instance.m_middleLeft;
		}
	}

	// Token: 0x1700021A RID: 538
	// (get) Token: 0x060009E2 RID: 2530 RVA: 0x0002B358 File Offset: 0x00029558
	public static Vector3 MiddleCenter
	{
		get
		{
			if (MonoSingleton<OnScreenPositions>.Instance.m_middleCenter == Vector3.zero)
			{
				MonoSingleton<OnScreenPositions>.Instance.m_middleCenter = MonoSingleton<OnScreenPositions>.Instance.MiddleCenterTransform.position;
			}
			return MonoSingleton<OnScreenPositions>.Instance.m_middleCenter;
		}
	}

	// Token: 0x1700021B RID: 539
	// (get) Token: 0x060009E3 RID: 2531 RVA: 0x0002B3A4 File Offset: 0x000295A4
	public static Vector3 MiddleRight
	{
		get
		{
			if (MonoSingleton<OnScreenPositions>.Instance.m_middleRight == Vector3.zero)
			{
				MonoSingleton<OnScreenPositions>.Instance.m_middleRight = MonoSingleton<OnScreenPositions>.Instance.MiddleRightTransform.position;
			}
			return MonoSingleton<OnScreenPositions>.Instance.m_middleRight;
		}
	}

	// Token: 0x1700021C RID: 540
	// (get) Token: 0x060009E4 RID: 2532 RVA: 0x0002B3F0 File Offset: 0x000295F0
	public static Vector3 BottomLeft
	{
		get
		{
			if (MonoSingleton<OnScreenPositions>.Instance.m_bottomLeft == Vector3.zero)
			{
				MonoSingleton<OnScreenPositions>.Instance.m_bottomLeft = MonoSingleton<OnScreenPositions>.Instance.BottomLeftTransform.position;
			}
			return MonoSingleton<OnScreenPositions>.Instance.m_bottomLeft;
		}
	}

	// Token: 0x1700021D RID: 541
	// (get) Token: 0x060009E5 RID: 2533 RVA: 0x0002B43C File Offset: 0x0002963C
	public static Vector3 BottomCenter
	{
		get
		{
			if (MonoSingleton<OnScreenPositions>.Instance.m_bottomCenter == Vector3.zero)
			{
				MonoSingleton<OnScreenPositions>.Instance.m_bottomCenter = MonoSingleton<OnScreenPositions>.Instance.BottomCenterTransform.position;
			}
			return MonoSingleton<OnScreenPositions>.Instance.m_bottomCenter;
		}
	}

	// Token: 0x1700021E RID: 542
	// (get) Token: 0x060009E6 RID: 2534 RVA: 0x0002B488 File Offset: 0x00029688
	public static Vector3 BottomRight
	{
		get
		{
			if (MonoSingleton<OnScreenPositions>.Instance.m_bottomRight == Vector3.zero)
			{
				MonoSingleton<OnScreenPositions>.Instance.m_bottomRight = MonoSingleton<OnScreenPositions>.Instance.BottomRightTransform.position;
			}
			return MonoSingleton<OnScreenPositions>.Instance.m_bottomRight;
		}
	}

	// Token: 0x04000823 RID: 2083
	public Transform TopLeftTransform;

	// Token: 0x04000824 RID: 2084
	private Vector3 m_topLeft = Vector3.zero;

	// Token: 0x04000825 RID: 2085
	public Transform TopCenterTransform;

	// Token: 0x04000826 RID: 2086
	private Vector3 m_topCenter = Vector3.zero;

	// Token: 0x04000827 RID: 2087
	public Transform TopRightTransform;

	// Token: 0x04000828 RID: 2088
	private Vector3 m_topRight = Vector3.zero;

	// Token: 0x04000829 RID: 2089
	public Transform MiddleLeftTransform;

	// Token: 0x0400082A RID: 2090
	private Vector3 m_middleLeft = Vector3.zero;

	// Token: 0x0400082B RID: 2091
	public Transform MiddleCenterTransform;

	// Token: 0x0400082C RID: 2092
	private Vector3 m_middleCenter = Vector3.zero;

	// Token: 0x0400082D RID: 2093
	public Transform MiddleRightTransform;

	// Token: 0x0400082E RID: 2094
	private Vector3 m_middleRight = Vector3.zero;

	// Token: 0x0400082F RID: 2095
	public Transform BottomLeftTransform;

	// Token: 0x04000830 RID: 2096
	private Vector3 m_bottomLeft = Vector3.zero;

	// Token: 0x04000831 RID: 2097
	public Transform BottomCenterTransform;

	// Token: 0x04000832 RID: 2098
	private Vector3 m_bottomCenter = Vector3.zero;

	// Token: 0x04000833 RID: 2099
	public Transform BottomRightTransform;

	// Token: 0x04000834 RID: 2100
	private Vector3 m_bottomRight;
}
