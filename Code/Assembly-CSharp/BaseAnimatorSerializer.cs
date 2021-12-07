using System;
using UnityEngine;

// Token: 0x02000774 RID: 1908
public class BaseAnimatorSerializer : SaveSerialize
{
	// Token: 0x06002C49 RID: 11337 RVA: 0x000BE7CA File Offset: 0x000BC9CA
	public void OnValidate()
	{
		this.m_animators = base.GetComponents<BaseAnimator>();
	}

	// Token: 0x06002C4A RID: 11338 RVA: 0x000BE7D8 File Offset: 0x000BC9D8
	public override void Serialize(Archive ar)
	{
		for (int i = 0; i < this.m_animators.Length; i++)
		{
			BaseAnimator baseAnimator = this.m_animators[i];
			baseAnimator.AnimatorDriver.IsPlaying = ar.Serialize(baseAnimator.AnimatorDriver.IsPlaying);
			if (ar.Reading)
			{
				baseAnimator.AnimatorDriver.CurrentTime = ar.Serialize(baseAnimator.AnimatorDriver.CurrentTime);
				baseAnimator.Initialize();
				baseAnimator.SampleValue(baseAnimator.AnimatorDriver.CurrentTime, true);
			}
			else
			{
				ar.Serialize(baseAnimator.AnimatorDriver.CurrentTime);
			}
		}
	}

	// Token: 0x04002833 RID: 10291
	[SerializeField]
	private BaseAnimator[] m_animators;
}
