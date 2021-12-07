using System;
using UnityEngine;

// Token: 0x02000713 RID: 1811
public class ScaleWidthForAspectRatio : MonoBehaviour
{
	// Token: 0x06002B03 RID: 11011 RVA: 0x000B8157 File Offset: 0x000B6357
	public void OnAspectRatioChanged()
	{
		this.ApplyAspect();
	}

	// Token: 0x06002B04 RID: 11012 RVA: 0x000B8160 File Offset: 0x000B6360
	public void Start()
	{
		if (!this.m_registered)
		{
			AspectRatioManager.OnAspectChanged.Add(new Action(this.OnAspectRatioChanged));
			this.m_registered = true;
		}
		this.m_initialScale = base.transform.localScale;
		this.ApplyAspect();
	}

	// Token: 0x06002B05 RID: 11013 RVA: 0x000B81AC File Offset: 0x000B63AC
	public void OnDestroy()
	{
		if (this.m_registered)
		{
			this.m_registered = false;
			AspectRatioManager.OnAspectChanged.Remove(new Action(this.OnAspectRatioChanged));
		}
	}

	// Token: 0x06002B06 RID: 11014 RVA: 0x000B81E4 File Offset: 0x000B63E4
	private void ApplyAspect()
	{
		Vector3 localScale = base.transform.localScale;
		localScale.x = this.m_initialScale.x * AspectRatioManager.AspectRatio / 1.7777778f;
		base.transform.localScale = localScale;
	}

	// Token: 0x0400264F RID: 9807
	private bool m_registered;

	// Token: 0x04002650 RID: 9808
	private Vector3 m_initialScale;
}
