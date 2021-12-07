using System;
using UnityEngine;

// Token: 0x020004CC RID: 1228
public class DifficultyToggler : MonoBehaviour, IDebugMenuToggleable
{
	// Token: 0x170005A0 RID: 1440
	// (get) Token: 0x06002139 RID: 8505 RVA: 0x00091D2C File Offset: 0x0008FF2C
	public string Name
	{
		get
		{
			return "Difficulty";
		}
	}

	// Token: 0x170005A1 RID: 1441
	// (get) Token: 0x0600213A RID: 8506 RVA: 0x00091D33 File Offset: 0x0008FF33
	public string HelpText
	{
		get
		{
			return "Toggle difficulty";
		}
	}

	// Token: 0x170005A2 RID: 1442
	// (get) Token: 0x0600213B RID: 8507 RVA: 0x00091D3C File Offset: 0x0008FF3C
	public string[] ToggleOptions
	{
		get
		{
			return new string[]
			{
				"Easy",
				"Normal",
				"Hard",
				"One Life"
			};
		}
	}

	// Token: 0x170005A3 RID: 1443
	// (get) Token: 0x0600213C RID: 8508 RVA: 0x00091D6F File Offset: 0x0008FF6F
	// (set) Token: 0x0600213D RID: 8509 RVA: 0x00091D7C File Offset: 0x0008FF7C
	public int CurrentToggleOptionId
	{
		get
		{
			return (int)DifficultyController.Instance.Difficulty;
		}
		set
		{
			DifficultyController.Instance.Difficulty = (value % this.ToggleOptions.Length + (DifficultyMode)this.ToggleOptions.Length) % (DifficultyMode)this.ToggleOptions.Length;
		}
	}

	// Token: 0x04001C20 RID: 7200
	private int m_currentOption;
}
