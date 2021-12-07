using System;
using UnityEngine;

// Token: 0x020008BF RID: 2239
public class ReEngageUI : MonoBehaviour
{
	// Token: 0x170007E0 RID: 2016
	// (get) Token: 0x060031C5 RID: 12741 RVA: 0x000D3687 File Offset: 0x000D1887
	private static ReEngageUI Instance
	{
		get
		{
			if (ReEngageUI.s_instance == null)
			{
				ReEngageUI.s_instance = UnityEngine.Object.FindObjectOfType<ReEngageUI>();
			}
			return ReEngageUI.s_instance;
		}
	}

	// Token: 0x170007E1 RID: 2017
	// (get) Token: 0x060031C6 RID: 12742 RVA: 0x000D36A8 File Offset: 0x000D18A8
	public static bool Ready
	{
		get
		{
			return ReEngageUI.Instance != null;
		}
	}

	// Token: 0x170007E2 RID: 2018
	// (get) Token: 0x060031C7 RID: 12743 RVA: 0x000D36B5 File Offset: 0x000D18B5
	// (set) Token: 0x060031C8 RID: 12744 RVA: 0x000D36D0 File Offset: 0x000D18D0
	public static bool Active
	{
		get
		{
			return ReEngageUI.Ready && ReEngageUI.Instance.enabled;
		}
		set
		{
			bool active = ReEngageUI.Active;
			if (!ReEngageUI.Ready || active == value)
			{
				return;
			}
			if (value)
			{
				ReEngageUI.Instance.enabled = true;
				SuspensionManager.SuspendAll();
			}
			else
			{
				SuspensionManager.ResumeAll();
				ReEngageUI.Instance.enabled = false;
			}
		}
	}

	// Token: 0x04002CF4 RID: 11508
	private static ReEngageUI s_instance;
}
