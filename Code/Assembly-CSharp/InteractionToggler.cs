using System;
using UnityEngine;

// Token: 0x020004D0 RID: 1232
public class InteractionToggler : MonoBehaviour, IDebugMenuToggleable
{
	// Token: 0x170005AC RID: 1452
	// (get) Token: 0x06002154 RID: 8532 RVA: 0x00091FBB File Offset: 0x000901BB
	public string Name
	{
		get
		{
			return "Interaction";
		}
	}

	// Token: 0x170005AD RID: 1453
	// (get) Token: 0x06002155 RID: 8533 RVA: 0x00091FC2 File Offset: 0x000901C2
	public string HelpText
	{
		get
		{
			return "Toggle differnt interaction Options";
		}
	}

	// Token: 0x170005AE RID: 1454
	// (get) Token: 0x06002156 RID: 8534 RVA: 0x00091FC9 File Offset: 0x000901C9
	public string[] ToggleOptions
	{
		get
		{
			return new string[]
			{
				"Interaction Off",
				"Interaction On"
			};
		}
	}

	// Token: 0x170005AF RID: 1455
	// (get) Token: 0x06002157 RID: 8535 RVA: 0x00091FE1 File Offset: 0x000901E1
	// (set) Token: 0x06002158 RID: 8536 RVA: 0x00091FF5 File Offset: 0x000901F5
	public int CurrentToggleOptionId
	{
		get
		{
			return (!this.m_doInteraction) ? 0 : 1;
		}
		set
		{
			this.m_doInteraction = !this.m_doInteraction;
			UberInteractionManager.Instance.DoInteractions = this.m_doInteraction;
		}
	}

	// Token: 0x04001C2A RID: 7210
	private bool m_doInteraction = true;
}
