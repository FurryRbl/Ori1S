using System;
using Core;
using UnityEngine;

// Token: 0x0200026C RID: 620
public class AchievementsUI : MonoBehaviour
{
	// Token: 0x170003AC RID: 940
	// (get) Token: 0x060014C6 RID: 5318 RVA: 0x0005D8C4 File Offset: 0x0005BAC4
	private static AchievementsUI Instance
	{
		get
		{
			if (AchievementsUI.s_instance == null)
			{
				AchievementsUI.s_instance = UnityEngine.Object.FindObjectOfType<AchievementsUI>();
			}
			return AchievementsUI.s_instance;
		}
	}

	// Token: 0x170003AD RID: 941
	// (get) Token: 0x060014C7 RID: 5319 RVA: 0x0005D8E5 File Offset: 0x0005BAE5
	public static bool Available
	{
		get
		{
			return AchievementsUI.Instance != null;
		}
	}

	// Token: 0x170003AE RID: 942
	// (get) Token: 0x060014C8 RID: 5320 RVA: 0x0005D8F2 File Offset: 0x0005BAF2
	// (set) Token: 0x060014C9 RID: 5321 RVA: 0x0005D90C File Offset: 0x0005BB0C
	public static bool Visible
	{
		get
		{
			return AchievementsUI.Available && AchievementsUI.Instance.m_Visible;
		}
		set
		{
			if (!AchievementsUI.Available)
			{
				return;
			}
			if (AchievementsUI.Instance.m_Visible && !value)
			{
				SuspensionManager.ResumeAll();
			}
			else if (!AchievementsUI.Instance.m_Visible && value)
			{
				SuspensionManager.SuspendAll();
			}
			AchievementsUI.Instance.m_Visible = value;
		}
	}

	// Token: 0x060014CA RID: 5322 RVA: 0x0005D968 File Offset: 0x0005BB68
	private void FixedUpdate()
	{
		if (Core.Input.Cancel.IsPressed)
		{
			AchievementsUI.Visible = false;
		}
	}

	// Token: 0x060014CB RID: 5323 RVA: 0x0005D980 File Offset: 0x0005BB80
	private static Rect PushDown(ref Rect rect, float offset)
	{
		rect.Set(rect.x, rect.y + offset, rect.width, rect.height);
		return rect;
	}

	// Token: 0x0400120D RID: 4621
	private static AchievementsUI s_instance;

	// Token: 0x0400120E RID: 4622
	private bool m_Visible;
}
