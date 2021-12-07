using System;
using UnityEngine;

// Token: 0x020003B9 RID: 953
public class LegacyTranslateAnimator : LegacyAnimator
{
	// Token: 0x06001A7D RID: 6781 RVA: 0x000721A0 File Offset: 0x000703A0
	public override void Awake()
	{
		base.Awake();
		this.CacheOriginals();
	}

	// Token: 0x06001A7E RID: 6782 RVA: 0x000721B0 File Offset: 0x000703B0
	private void CacheOriginals()
	{
		if (this.Target)
		{
			this.m_transform = this.Target;
			this.m_originalPosition = this.Target.localPosition;
		}
		else
		{
			this.m_transform = base.transform;
			this.m_originalPosition = this.m_transform.localPosition;
		}
	}

	// Token: 0x06001A7F RID: 6783 RVA: 0x0007220C File Offset: 0x0007040C
	public override void OnPoolSpawned()
	{
		this.CacheOriginals();
		base.OnPoolSpawned();
	}

	// Token: 0x06001A80 RID: 6784 RVA: 0x0007221C File Offset: 0x0007041C
	protected override void AnimateIt(float value)
	{
		if (this.AnimateX && this.AnimateY && this.AnimateZ)
		{
			this.m_transform.localPosition = this.m_originalPosition + this.Translate * value;
		}
		else
		{
			Vector3 localPosition = this.m_transform.localPosition;
			if (this.AnimateX)
			{
				localPosition.x = this.m_originalPosition.x + this.Translate.x * value;
			}
			if (this.AnimateY)
			{
				localPosition.y = this.m_originalPosition.y + this.Translate.y * value;
			}
			if (this.AnimateZ)
			{
				localPosition.z = this.m_originalPosition.z + this.Translate.z * value;
			}
			this.m_transform.localPosition = localPosition;
		}
	}

	// Token: 0x06001A81 RID: 6785 RVA: 0x0007230A File Offset: 0x0007050A
	public override void RestoreToOriginalState()
	{
		this.AnimateIt(0f);
	}

	// Token: 0x040016FC RID: 5884
	public Transform Target;

	// Token: 0x040016FD RID: 5885
	private Transform m_transform;

	// Token: 0x040016FE RID: 5886
	private Vector3 m_originalPosition;

	// Token: 0x040016FF RID: 5887
	public Vector3 Translate;

	// Token: 0x04001700 RID: 5888
	public bool AnimateX = true;

	// Token: 0x04001701 RID: 5889
	public bool AnimateY = true;

	// Token: 0x04001702 RID: 5890
	public bool AnimateZ = true;
}
