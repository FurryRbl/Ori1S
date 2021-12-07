using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200016A RID: 362
public class LegacyHierarchyTransparancyAnimatorController : MonoBehaviour
{
	// Token: 0x06000E6C RID: 3692 RVA: 0x00042574 File Offset: 0x00040774
	private void Start()
	{
		foreach (Transform transform in base.GetComponentsInChildren<Transform>())
		{
			if (!(transform == base.transform))
			{
				if (!(transform.gameObject.GetComponent<Renderer>() == null))
				{
					LegacyTransparancyAnimator legacyTransparancyAnimator = transform.gameObject.AddComponent<LegacyTransparancyAnimator>();
					legacyTransparancyAnimator.AnimationCurve = this.AnimationCurve;
					legacyTransparancyAnimator.PlayAutomatically = this.PlayAutomatically;
					legacyTransparancyAnimator.DeactivateWhenInvisible = this.DeactivateWhenInvisible;
					legacyTransparancyAnimator.OnAnimationEndEvent += this.OnAnimationEnd;
					this.m_list.Add(legacyTransparancyAnimator);
				}
			}
		}
	}

	// Token: 0x06000E6D RID: 3693 RVA: 0x0004261F File Offset: 0x0004081F
	private void OnAnimationEnd()
	{
	}

	// Token: 0x06000E6E RID: 3694 RVA: 0x00042621 File Offset: 0x00040821
	private void OnEnable()
	{
		if (this.RestartOnEnable)
		{
			this.RestartAnimators();
		}
	}

	// Token: 0x06000E6F RID: 3695 RVA: 0x00042634 File Offset: 0x00040834
	public void RestartAnimators()
	{
		foreach (LegacyTransparancyAnimator legacyTransparancyAnimator in this.m_list)
		{
			legacyTransparancyAnimator.Speed = this.Speed;
			legacyTransparancyAnimator.Restart();
		}
	}

	// Token: 0x04000B91 RID: 2961
	public AnimationCurve AnimationCurve;

	// Token: 0x04000B92 RID: 2962
	public bool PlayAutomatically;

	// Token: 0x04000B93 RID: 2963
	public bool DeactivateWhenInvisible;

	// Token: 0x04000B94 RID: 2964
	public bool RestartOnEnable;

	// Token: 0x04000B95 RID: 2965
	public float Speed = 1f;

	// Token: 0x04000B96 RID: 2966
	private List<LegacyTransparancyAnimator> m_list = new List<LegacyTransparancyAnimator>();
}
