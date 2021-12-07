using System;
using UnityEngine;

// Token: 0x020004D4 RID: 1236
public class LowestDifficultyToggler : MonoBehaviour, IDebugMenuToggleable
{
	// Token: 0x170005B9 RID: 1465
	// (get) Token: 0x06002178 RID: 8568 RVA: 0x00092E8A File Offset: 0x0009108A
	public string Name
	{
		get
		{
			return "Lowest Difficulty";
		}
	}

	// Token: 0x170005BA RID: 1466
	// (get) Token: 0x06002179 RID: 8569 RVA: 0x00092E91 File Offset: 0x00091091
	public string HelpText
	{
		get
		{
			return "Toggle lowest difficulty";
		}
	}

	// Token: 0x170005BB RID: 1467
	// (get) Token: 0x0600217A RID: 8570 RVA: 0x00092E98 File Offset: 0x00091098
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

	// Token: 0x170005BC RID: 1468
	// (get) Token: 0x0600217B RID: 8571 RVA: 0x00092ECB File Offset: 0x000910CB
	// (set) Token: 0x0600217C RID: 8572 RVA: 0x00092ED8 File Offset: 0x000910D8
	public int CurrentToggleOptionId
	{
		get
		{
			return (int)DifficultyController.Instance.LowestDifficulty;
		}
		set
		{
			DifficultyController.Instance.LowestDifficulty = (value % this.ToggleOptions.Length + (DifficultyMode)this.ToggleOptions.Length) % (DifficultyMode)this.ToggleOptions.Length;
		}
	}

	// Token: 0x04001C40 RID: 7232
	private int m_currentOption;
}
