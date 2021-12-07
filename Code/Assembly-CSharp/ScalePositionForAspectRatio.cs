using System;
using UnityEngine;

// Token: 0x02000712 RID: 1810
public class ScalePositionForAspectRatio : MonoBehaviour
{
	// Token: 0x06002AFE RID: 11006 RVA: 0x000B80BC File Offset: 0x000B62BC
	public void OnAspectRatioChanged()
	{
		this.ApplyAspect();
	}

	// Token: 0x06002AFF RID: 11007 RVA: 0x000B80C4 File Offset: 0x000B62C4
	public void Start()
	{
		AspectRatioManager.OnAspectChanged.Add(new Action(this.OnAspectRatioChanged));
		this.m_initialPosition = base.transform.localPosition;
		this.ApplyAspect();
	}

	// Token: 0x06002B00 RID: 11008 RVA: 0x000B80F3 File Offset: 0x000B62F3
	public void OnDestroy()
	{
		AspectRatioManager.OnAspectChanged.Remove(new Action(this.OnAspectRatioChanged));
	}

	// Token: 0x06002B01 RID: 11009 RVA: 0x000B810C File Offset: 0x000B630C
	private void ApplyAspect()
	{
		Vector3 localPosition = base.transform.localPosition;
		localPosition.x = this.m_initialPosition.x * AspectRatioManager.AspectRatio / 1.7777778f;
		base.transform.localPosition = localPosition;
	}

	// Token: 0x0400264E RID: 9806
	private Vector3 m_initialPosition;
}
