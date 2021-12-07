using System;
using Game;
using UnityEngine;

// Token: 0x02000900 RID: 2304
public class PlayAnimatorBasedOnNightberry : MonoBehaviour
{
	// Token: 0x06003337 RID: 13111 RVA: 0x000D7D8B File Offset: 0x000D5F8B
	private void Start()
	{
		this.m_animators = base.GetComponentsInChildren<LegacyAnimator>();
	}

	// Token: 0x06003338 RID: 13112 RVA: 0x000D7D9C File Offset: 0x000D5F9C
	private void FixedUpdate()
	{
		if (Characters.Sein.Abilities.Carry.Active != this.m_wasActivated)
		{
			this.m_wasActivated = Characters.Sein.Abilities.Carry.Active;
			if (this.m_wasActivated)
			{
				foreach (LegacyAnimator legacyAnimator in this.m_animators)
				{
					legacyAnimator.Restart();
				}
			}
			else
			{
				foreach (LegacyAnimator legacyAnimator2 in this.m_animators)
				{
					legacyAnimator2.RestartReverse();
				}
			}
		}
	}

	// Token: 0x04002E2B RID: 11819
	private bool m_wasActivated;

	// Token: 0x04002E2C RID: 11820
	private LegacyAnimator[] m_animators;
}
