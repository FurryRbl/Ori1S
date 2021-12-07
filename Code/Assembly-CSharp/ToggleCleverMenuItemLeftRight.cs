using System;
using Core;
using UnityEngine;

// Token: 0x02000138 RID: 312
public class ToggleCleverMenuItemLeftRight : MonoBehaviour
{
	// Token: 0x06000C69 RID: 3177 RVA: 0x00038B89 File Offset: 0x00036D89
	public void Awake()
	{
		this.m_selectionManager = base.gameObject.GetComponentInParents<CleverMenuItemSelectionManager>();
		this.m_cleverMenuItem = base.GetComponent<CleverMenuItem>();
	}

	// Token: 0x1700026C RID: 620
	// (get) Token: 0x06000C6A RID: 3178 RVA: 0x00038BA8 File Offset: 0x00036DA8
	public bool ItemSelected
	{
		get
		{
			return this.m_selectionManager.IsActive && this.m_selectionManager.CurrentMenuItem == this.m_cleverMenuItem;
		}
	}

	// Token: 0x06000C6B RID: 3179 RVA: 0x00038BD4 File Offset: 0x00036DD4
	public void FixedUpdate()
	{
		if (this.ItemSelected)
		{
			if (Core.Input.Left.OnPressed)
			{
				this.LeftAction.Perform(null);
			}
			if (Core.Input.Right.OnPressed)
			{
				this.RightAction.Perform(null);
			}
		}
	}

	// Token: 0x04000A47 RID: 2631
	private CleverMenuItemSelectionManager m_selectionManager;

	// Token: 0x04000A48 RID: 2632
	private CleverMenuItem m_cleverMenuItem;

	// Token: 0x04000A49 RID: 2633
	public ActionMethod LeftAction;

	// Token: 0x04000A4A RID: 2634
	public ActionMethod RightAction;
}
