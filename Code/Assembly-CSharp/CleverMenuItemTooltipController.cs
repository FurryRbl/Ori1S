using System;
using UnityEngine;

// Token: 0x0200010D RID: 269
public class CleverMenuItemTooltipController : MonoBehaviour
{
	// Token: 0x06000A9F RID: 2719 RVA: 0x0002E363 File Offset: 0x0002C563
	public void Awake()
	{
		CleverMenuItemSelectionManager selection = this.Selection;
		selection.OptionChangeCallback = (Action)Delegate.Combine(selection.OptionChangeCallback, new Action(this.OnOptionChanged));
	}

	// Token: 0x06000AA0 RID: 2720 RVA: 0x0002E38C File Offset: 0x0002C58C
	public void OnDestroy()
	{
		CleverMenuItemSelectionManager selection = this.Selection;
		selection.OptionChangeCallback = (Action)Delegate.Remove(selection.OptionChangeCallback, new Action(this.OnOptionChanged));
	}

	// Token: 0x06000AA1 RID: 2721 RVA: 0x0002E3B5 File Offset: 0x0002C5B5
	public void OnOptionChanged()
	{
		this.UpdateTooltip();
	}

	// Token: 0x06000AA2 RID: 2722 RVA: 0x0002E3BD File Offset: 0x0002C5BD
	public void OnEnable()
	{
		this.UpdateTooltip();
	}

	// Token: 0x06000AA3 RID: 2723 RVA: 0x0002E3C8 File Offset: 0x0002C5C8
	public void Update()
	{
		if (!this.Selection.IsActive && this.Tooltip.gameObject.activeSelf)
		{
			this.Tooltip.gameObject.SetActive(false);
		}
	}

	// Token: 0x06000AA4 RID: 2724 RVA: 0x0002E40C File Offset: 0x0002C60C
	public void UpdateTooltip()
	{
		if (this.Selection.CurrentMenuItem == null)
		{
			this.Tooltip.gameObject.SetActive(false);
		}
		else
		{
			CleverMenuItemTooltip component = this.Selection.CurrentMenuItem.GetComponent<CleverMenuItemTooltip>();
			if (component != null)
			{
				this.Tooltip.gameObject.SetActive(true);
				this.Tooltip.SetMessageProvider(component.Tooltip);
			}
			else
			{
				this.Tooltip.gameObject.SetActive(false);
			}
		}
	}

	// Token: 0x040008B5 RID: 2229
	public CleverMenuItemSelectionManager Selection;

	// Token: 0x040008B6 RID: 2230
	public MessageBox Tooltip;
}
