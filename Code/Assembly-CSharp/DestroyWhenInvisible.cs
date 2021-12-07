using System;
using UnityEngine;

// Token: 0x0200095E RID: 2398
public class DestroyWhenInvisible : MonoBehaviour
{
	// Token: 0x060034C6 RID: 13510 RVA: 0x000DD750 File Offset: 0x000DB950
	private void FixedUpdate()
	{
		AnimatorDriver animatorDriver = this.Animator.AnimatorDriver;
		if (this.DestroyReversedToStart && animatorDriver.IsReversed && animatorDriver.CurrentTime <= 0f)
		{
			InstantiateUtility.Destroy(base.gameObject);
		}
		if (this.DestroyAtEnd && !animatorDriver.IsReversed && animatorDriver.CurrentTime >= animatorDriver.Duration)
		{
			InstantiateUtility.Destroy(base.gameObject);
		}
	}

	// Token: 0x04002F8D RID: 12173
	public BaseAnimator Animator;

	// Token: 0x04002F8E RID: 12174
	public bool DestroyReversedToStart = true;

	// Token: 0x04002F8F RID: 12175
	public bool DestroyAtEnd;
}
