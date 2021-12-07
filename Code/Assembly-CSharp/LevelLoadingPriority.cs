using System;
using UnityEngine;

// Token: 0x020004D3 RID: 1235
public class LevelLoadingPriority : MonoBehaviour, IDebugMenuToggleable
{
	// Token: 0x170005B5 RID: 1461
	// (get) Token: 0x06002172 RID: 8562 RVA: 0x00092E07 File Offset: 0x00091007
	public string Name
	{
		get
		{
			return "Loading Priority";
		}
	}

	// Token: 0x170005B6 RID: 1462
	// (get) Token: 0x06002173 RID: 8563 RVA: 0x00092E0E File Offset: 0x0009100E
	public string HelpText
	{
		get
		{
			return "Change Loading Priority";
		}
	}

	// Token: 0x170005B7 RID: 1463
	// (get) Token: 0x06002174 RID: 8564 RVA: 0x00092E18 File Offset: 0x00091018
	public string[] ToggleOptions
	{
		get
		{
			return new string[]
			{
				"Low",
				"BelowNormal",
				"Normal",
				"High"
			};
		}
	}

	// Token: 0x170005B8 RID: 1464
	// (get) Token: 0x06002175 RID: 8565 RVA: 0x00092E4B File Offset: 0x0009104B
	// (set) Token: 0x06002176 RID: 8566 RVA: 0x00092E54 File Offset: 0x00091054
	public int CurrentToggleOptionId
	{
		get
		{
			return (int)Application.backgroundLoadingPriority;
		}
		set
		{
			Application.backgroundLoadingPriority = (value % this.ToggleOptions.Length + (ThreadPriority)this.ToggleOptions.Length) % (ThreadPriority)this.ToggleOptions.Length;
		}
	}

	// Token: 0x04001C3F RID: 7231
	private int m_currentOption;
}
