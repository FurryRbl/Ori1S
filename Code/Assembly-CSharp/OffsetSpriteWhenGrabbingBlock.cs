using System;
using UnityEngine;

// Token: 0x020003FD RID: 1021
public class OffsetSpriteWhenGrabbingBlock : MonoBehaviour
{
	// Token: 0x06001BA7 RID: 7079 RVA: 0x00076F98 File Offset: 0x00075198
	private void FixedUpdate()
	{
		if (this.CharacterGrabBlock.Active && !this.m_isPushing)
		{
			this.m_isPushing = true;
			base.transform.localPosition += Vector3.right * this.Distance;
		}
		if (!this.CharacterGrabBlock.Active && this.m_isPushing)
		{
			this.m_isPushing = false;
			base.transform.localPosition -= Vector3.right * this.Distance;
		}
	}

	// Token: 0x0400180A RID: 6154
	public SeinGrabBlock CharacterGrabBlock;

	// Token: 0x0400180B RID: 6155
	private bool m_isPushing;

	// Token: 0x0400180C RID: 6156
	public float Distance;
}
