using System;
using UnityEngine;

// Token: 0x0200039D RID: 925
public class LegacyAnimatorSerializer : SaveSerialize
{
	// Token: 0x060019F7 RID: 6647 RVA: 0x0006FAAD File Offset: 0x0006DCAD
	public void OnValidate()
	{
		this.m_animators = base.GetComponents<LegacyAnimator>();
	}

	// Token: 0x060019F8 RID: 6648 RVA: 0x0006FABC File Offset: 0x0006DCBC
	public override void Serialize(Archive ar)
	{
		foreach (LegacyAnimator legacyAnimator in this.m_animators)
		{
			if (ar.Reading)
			{
				legacyAnimator.CurrentTime = ar.Serialize(legacyAnimator.CurrentTime);
				if (legacyAnimator.enabled || legacyAnimator.gameObject.activeInHierarchy)
				{
					legacyAnimator.Sample(legacyAnimator.CurrentTime);
				}
			}
			else
			{
				ar.Serialize(legacyAnimator.CurrentTime);
			}
			legacyAnimator.Stopped = ar.Serialize(legacyAnimator.Stopped);
			legacyAnimator.Reversed = ar.Serialize(legacyAnimator.Reversed);
		}
	}

	// Token: 0x04001661 RID: 5729
	[SerializeField]
	private LegacyAnimator[] m_animators;
}
