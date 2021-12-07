using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020000E9 RID: 233
public class SpiritTreeTransformationAnimator : MonoBehaviour
{
	// Token: 0x06000933 RID: 2355 RVA: 0x00027950 File Offset: 0x00025B50
	public void OnEnable()
	{
		this.m_startTime = Time.fixedTime;
	}

	// Token: 0x06000934 RID: 2356 RVA: 0x00027960 File Offset: 0x00025B60
	public void FixedUpdate()
	{
		for (int i = this.AnimationEntries.Count - 1; i >= 0; i--)
		{
			AnimationEntry animationEntry = this.AnimationEntries[i];
			if (Time.fixedTime - this.m_startTime > animationEntry.Time)
			{
				foreach (LegacyAnimator legacyAnimator in animationEntry.Object.GetComponentsInChildren<LegacyAnimator>())
				{
					legacyAnimator.Speed *= animationEntry.SpeedMultiplier;
					legacyAnimator.Restart();
				}
				foreach (SpriteAnimator spriteAnimator in animationEntry.Object.GetComponentsInChildren<SpriteAnimator>())
				{
					spriteAnimator.AnimatorDriver.Resume();
				}
				this.AnimationEntries.RemoveAt(i);
			}
		}
	}

	// Token: 0x0400076C RID: 1900
	public float FastForwardToTime;

	// Token: 0x0400076D RID: 1901
	public List<AnimationEntry> AnimationEntries;

	// Token: 0x0400076E RID: 1902
	private float m_startTime;
}
