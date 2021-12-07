using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020004C7 RID: 1223
public class DebugMenuToggler : MonoBehaviour, IDebugMenuToggleable
{
	// Token: 0x0600212D RID: 8493 RVA: 0x00091A24 File Offset: 0x0008FC24
	private void SetCurrentToggleOption(int toggleOptionId)
	{
		if (toggleOptionId == -1)
		{
			return;
		}
		for (int i = 0; i < this.Options.Count; i++)
		{
			DebugMenuToggler.ToggleOption toggleOption = this.Options[i];
			foreach (GameObject gameObject in toggleOption.ObjectToEnable)
			{
				gameObject.SetActive(false);
			}
			foreach (MonoBehaviour monoBehaviour in toggleOption.ComponentsToEnable)
			{
				monoBehaviour.enabled = false;
			}
		}
		DebugMenuToggler.ToggleOption toggleOption2 = null;
		if (toggleOptionId < this.Options.Count)
		{
			toggleOption2 = this.Options[this.m_currentOptionId];
		}
		else
		{
			if (toggleOptionId != this.Options.Count)
			{
				return;
			}
			toggleOption2 = this.NoneEnabledOption;
		}
		foreach (GameObject gameObject2 in toggleOption2.ObjectToEnable)
		{
			gameObject2.SetActive(true);
		}
		foreach (MonoBehaviour monoBehaviour2 in toggleOption2.ComponentsToEnable)
		{
			monoBehaviour2.enabled = true;
		}
	}

	// Token: 0x1700059C RID: 1436
	// (get) Token: 0x0600212E RID: 8494 RVA: 0x00091BE4 File Offset: 0x0008FDE4
	public string Name
	{
		get
		{
			return this.TogglerName;
		}
	}

	// Token: 0x1700059D RID: 1437
	// (get) Token: 0x0600212F RID: 8495 RVA: 0x00091BEC File Offset: 0x0008FDEC
	public string HelpText
	{
		get
		{
			return this.HelpString;
		}
	}

	// Token: 0x1700059E RID: 1438
	// (get) Token: 0x06002130 RID: 8496 RVA: 0x00091BF4 File Offset: 0x0008FDF4
	public string[] ToggleOptions
	{
		get
		{
			if (this.m_cachedOptionList == null)
			{
				List<string> list = new List<string>();
				foreach (DebugMenuToggler.ToggleOption toggleOption in this.Options)
				{
					list.Add(toggleOption.Name);
				}
				list.Add(this.NoneEnabledOption.Name);
				this.m_cachedOptionList = list.ToArray();
			}
			return this.m_cachedOptionList;
		}
	}

	// Token: 0x1700059F RID: 1439
	// (get) Token: 0x06002131 RID: 8497 RVA: 0x00091C88 File Offset: 0x0008FE88
	// (set) Token: 0x06002132 RID: 8498 RVA: 0x00091C90 File Offset: 0x0008FE90
	public int CurrentToggleOptionId
	{
		get
		{
			return this.m_currentOptionId;
		}
		set
		{
			this.m_currentOptionId = value;
			if (this.m_currentOptionId < 0)
			{
				this.m_currentOptionId = this.Options.Count;
			}
			if (this.m_currentOptionId > this.Options.Count)
			{
				this.m_currentOptionId = 0;
			}
			this.SetCurrentToggleOption(this.m_currentOptionId);
		}
	}

	// Token: 0x04001C14 RID: 7188
	public string TogglerName;

	// Token: 0x04001C15 RID: 7189
	public string HelpString;

	// Token: 0x04001C16 RID: 7190
	public List<DebugMenuToggler.ToggleOption> Options;

	// Token: 0x04001C17 RID: 7191
	public DebugMenuToggler.ToggleOption NoneEnabledOption;

	// Token: 0x04001C18 RID: 7192
	private int m_currentOptionId = -1;

	// Token: 0x04001C19 RID: 7193
	private string[] m_cachedOptionList;

	// Token: 0x020004C8 RID: 1224
	[Serializable]
	public class ToggleOption
	{
		// Token: 0x04001C1A RID: 7194
		public string Name;

		// Token: 0x04001C1B RID: 7195
		public List<GameObject> ObjectToEnable;

		// Token: 0x04001C1C RID: 7196
		public List<MonoBehaviour> ComponentsToEnable;
	}
}
