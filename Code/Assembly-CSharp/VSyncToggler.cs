using System;
using UnityEngine;

// Token: 0x020004DD RID: 1245
public class VSyncToggler : MonoBehaviour, IDebugMenuToggleable
{
	// Token: 0x060021B7 RID: 8631 RVA: 0x0009392C File Offset: 0x00091B2C
	public void Awake()
	{
		this.m_currentOption = QualitySettings.vSyncCount;
	}

	// Token: 0x170005D0 RID: 1488
	// (get) Token: 0x060021B8 RID: 8632 RVA: 0x00093939 File Offset: 0x00091B39
	public string Name
	{
		get
		{
			return "VSync";
		}
	}

	// Token: 0x170005D1 RID: 1489
	// (get) Token: 0x060021B9 RID: 8633 RVA: 0x00093940 File Offset: 0x00091B40
	public string HelpText
	{
		get
		{
			return "Toggle differnt VSync Options";
		}
	}

	// Token: 0x170005D2 RID: 1490
	// (get) Token: 0x060021BA RID: 8634 RVA: 0x00093947 File Offset: 0x00091B47
	public string[] ToggleOptions
	{
		get
		{
			return new string[]
			{
				"Vsync Off",
				"Vsync On"
			};
		}
	}

	// Token: 0x170005D3 RID: 1491
	// (get) Token: 0x060021BB RID: 8635 RVA: 0x0009395F File Offset: 0x00091B5F
	// (set) Token: 0x060021BC RID: 8636 RVA: 0x00093968 File Offset: 0x00091B68
	public int CurrentToggleOptionId
	{
		get
		{
			return this.m_currentOption;
		}
		set
		{
			this.m_currentOption = Mathf.Abs(value) % 2;
			bool flag = this.m_currentOption == 1;
			QualitySettings.vSyncCount = ((!flag) ? 0 : 1);
		}
	}

	// Token: 0x04001C52 RID: 7250
	private int m_currentOption;
}
