using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x02000477 RID: 1143
public class CleverMenuItemLayout : MonoBehaviour
{
	// Token: 0x06001F6B RID: 8043 RVA: 0x0008A6A5 File Offset: 0x000888A5
	public void OnEnable()
	{
		this.Sort();
	}

	// Token: 0x06001F6C RID: 8044 RVA: 0x0008A6B0 File Offset: 0x000888B0
	[ContextMenu("Apply")]
	public void Sort()
	{
		float num = 0f;
		foreach (CleverMenuItem cleverMenuItem in this.MenuItems)
		{
			if (cleverMenuItem.IsVisible)
			{
				cleverMenuItem.transform.localPosition = Vector3.down * num;
				num += cleverMenuItem.Space;
			}
		}
		foreach (CleverMenuItem cleverMenuItem2 in this.MenuItems)
		{
			if (cleverMenuItem2.IsVisible)
			{
				if (this.VerticalAlignment == CleverMenuItemLayout.Alignment.Center)
				{
					cleverMenuItem2.transform.localPosition += Vector3.up * num * 0.5f;
				}
				if (this.VerticalAlignment == CleverMenuItemLayout.Alignment.Bottom)
				{
					cleverMenuItem2.transform.localPosition += Vector3.up * num;
				}
			}
		}
	}

	// Token: 0x04001B0F RID: 6927
	public List<CleverMenuItem> MenuItems = new List<CleverMenuItem>();

	// Token: 0x04001B10 RID: 6928
	public CleverMenuItemLayout.Alignment VerticalAlignment;

	// Token: 0x02000478 RID: 1144
	public enum Alignment
	{
		// Token: 0x04001B12 RID: 6930
		Top,
		// Token: 0x04001B13 RID: 6931
		Center,
		// Token: 0x04001B14 RID: 6932
		Bottom
	}
}
