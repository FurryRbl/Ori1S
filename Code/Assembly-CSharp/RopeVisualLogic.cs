using System;
using UnityEngine;

// Token: 0x020006D9 RID: 1753
public class RopeVisualLogic : MonoBehaviour
{
	// Token: 0x060029F2 RID: 10738 RVA: 0x000B4A90 File Offset: 0x000B2C90
	private void Awake()
	{
		this.m_ropeOriginalScale = this.RopeGraphic.transform.localScale.x;
	}

	// Token: 0x060029F3 RID: 10739 RVA: 0x000B4ABC File Offset: 0x000B2CBC
	private void FixedUpdate()
	{
		base.transform.position = (this.LeftAttachment.position + this.RightAttachment.position) / 2f;
		base.transform.LookAt(this.LeftAttachment, Vector3.forward);
		this.RopeGraphic.transform.localScale = new Vector3(this.m_ropeOriginalScale, (this.LeftAttachment.position - this.RightAttachment.position).magnitude, this.m_ropeOriginalScale);
	}

	// Token: 0x0400257B RID: 9595
	public Transform LeftAttachment;

	// Token: 0x0400257C RID: 9596
	public Transform RightAttachment;

	// Token: 0x0400257D RID: 9597
	public GameObject RopeGraphic;

	// Token: 0x0400257E RID: 9598
	private float m_ropeOriginalScale = 1f;
}
