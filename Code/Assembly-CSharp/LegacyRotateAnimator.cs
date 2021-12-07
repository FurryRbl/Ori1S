using System;
using UnityEngine;

// Token: 0x020003B4 RID: 948
public class LegacyRotateAnimator : LegacyAnimator
{
	// Token: 0x06001A6B RID: 6763 RVA: 0x00071B81 File Offset: 0x0006FD81
	public override void Awake()
	{
		base.Awake();
		this.m_transform = base.transform;
		this.m_originalRotation = this.m_transform.rotation;
	}

	// Token: 0x06001A6C RID: 6764 RVA: 0x00071BA6 File Offset: 0x0006FDA6
	public override void Restart()
	{
		this.m_originalRotation = this.m_transform.rotation;
		base.Restart();
	}

	// Token: 0x06001A6D RID: 6765 RVA: 0x00071BBF File Offset: 0x0006FDBF
	public override void RestartReverse()
	{
		this.m_originalRotation = this.m_transform.rotation;
		base.RestartReverse();
	}

	// Token: 0x06001A6E RID: 6766 RVA: 0x00071BD8 File Offset: 0x0006FDD8
	protected override void AnimateIt(float value)
	{
		this.m_transform.rotation = Quaternion.Euler(this.m_originalRotation.eulerAngles.x + ((this.RotateAxisFilter.x != 1f) ? 0f : value), this.m_originalRotation.eulerAngles.y + ((this.RotateAxisFilter.y != 1f) ? 0f : value), this.m_originalRotation.eulerAngles.z + ((this.RotateAxisFilter.z != 1f) ? 0f : value));
	}

	// Token: 0x06001A6F RID: 6767 RVA: 0x00071C91 File Offset: 0x0006FE91
	public override void RestoreToOriginalState()
	{
		this.AnimateIt(0f);
	}

	// Token: 0x040016DD RID: 5853
	public Vector3 RotateAxisFilter = new Vector3(0f, 0f, 1f);

	// Token: 0x040016DE RID: 5854
	private Transform m_transform;

	// Token: 0x040016DF RID: 5855
	private Quaternion m_originalRotation;
}
