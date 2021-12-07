using System;
using CatlikeCoding.TextBox;
using UnityEngine;

// Token: 0x020001BD RID: 445
public class SkillTreeInfoPanelTextLayout : MonoBehaviour
{
	// Token: 0x0600108B RID: 4235 RVA: 0x0004B828 File Offset: 0x00049A28
	public void FixedUpdate()
	{
		Rect rect = TextBoxExtended.GetRect(this.Description);
		base.transform.localPosition = new Vector3(0f, rect.height, 0f);
	}

	// Token: 0x04000DFD RID: 3581
	public TextBox Description;
}
