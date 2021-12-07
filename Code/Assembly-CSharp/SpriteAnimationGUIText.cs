using System;
using UnityEngine;

// Token: 0x020009A2 RID: 2466
public class SpriteAnimationGUIText : MonoBehaviour
{
	// Token: 0x060035BA RID: 13754 RVA: 0x000E18B0 File Offset: 0x000DFAB0
	public void FixedUpdate()
	{
		base.GetComponent<GUIText>().text = this.SpriteAnimator.CurrentAnimation.name;
	}

	// Token: 0x04003055 RID: 12373
	public SpriteAnimatorWithTransitions SpriteAnimator;
}
