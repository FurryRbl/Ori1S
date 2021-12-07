using System;
using UnityEngine;

// Token: 0x0200077B RID: 1915
public class InstantiateAnimator : BaseAnimator
{
	// Token: 0x1700070D RID: 1805
	// (get) Token: 0x06002C73 RID: 11379 RVA: 0x000BF096 File Offset: 0x000BD296
	public override bool IsLooping
	{
		get
		{
			return false;
		}
	}

	// Token: 0x06002C74 RID: 11380 RVA: 0x000BF099 File Offset: 0x000BD299
	public override void CacheOriginals()
	{
	}

	// Token: 0x06002C75 RID: 11381 RVA: 0x000BF09C File Offset: 0x000BD29C
	public override void SampleValue(float value, bool forceSample)
	{
		float num = base.TimeToAnimationCurveTime(value);
		if (this.m_gameObject != null)
		{
			if (num < 0f || num >= this.m_duration)
			{
				if (Application.isPlaying)
				{
					InstantiateUtility.Destroy(this.m_gameObject);
				}
				else
				{
					UnityEngine.Object.DestroyImmediate(this.m_gameObject);
				}
			}
		}
		else if (num >= 0f && num <= this.m_duration)
		{
			this.m_gameObject = (GameObject)InstantiateUtility.Instantiate(this.Prefab, this.Target.position, this.Target.rotation);
		}
		if (this.m_gameObject != null)
		{
		}
	}

	// Token: 0x1700070E RID: 1806
	// (get) Token: 0x06002C76 RID: 11382 RVA: 0x000BF157 File Offset: 0x000BD357
	public override float Duration
	{
		get
		{
			return this.m_duration;
		}
	}

	// Token: 0x06002C77 RID: 11383 RVA: 0x000BF15F File Offset: 0x000BD35F
	public override void RestoreToOriginalState()
	{
		if (this.m_gameObject)
		{
			if (Application.isPlaying)
			{
				InstantiateUtility.Destroy(this.m_gameObject);
			}
			else
			{
				UnityEngine.Object.DestroyImmediate(this.m_gameObject);
			}
		}
	}

	// Token: 0x04002841 RID: 10305
	public GameObject Prefab;

	// Token: 0x04002842 RID: 10306
	public Transform Target;

	// Token: 0x04002843 RID: 10307
	private GameObject m_gameObject;

	// Token: 0x04002844 RID: 10308
	[SerializePrivateVariables]
	private float m_duration = 2f;
}
