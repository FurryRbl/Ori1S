using System;
using UnityEngine;

// Token: 0x02000606 RID: 1542
[Serializable]
public class MortarWormDirectionalAnimations
{
	// Token: 0x06002684 RID: 9860 RVA: 0x000A8E48 File Offset: 0x000A7048
	public TextureAnimationWithTransitions PickWithDirection(Vector3 direction)
	{
		float num = Mathf.Cos(0.3926991f);
		if (Vector3.Dot(direction, Vector3.up) > num)
		{
			return this.Up;
		}
		Vector3 vector = new Vector3(-1f, 1f);
		if (Vector3.Dot(direction, vector.normalized) > num)
		{
			return this.UpLeft;
		}
		Vector3 vector2 = new Vector3(1f, 1f);
		if (Vector3.Dot(direction, vector2.normalized) > num)
		{
			return this.UpRight;
		}
		if (Vector3.Dot(direction, Vector3.left) > 0f)
		{
			return this.Left;
		}
		return this.Right;
	}

	// Token: 0x04002128 RID: 8488
	public TextureAnimationWithTransitions Left;

	// Token: 0x04002129 RID: 8489
	public TextureAnimationWithTransitions UpLeft;

	// Token: 0x0400212A RID: 8490
	public TextureAnimationWithTransitions Up;

	// Token: 0x0400212B RID: 8491
	public TextureAnimationWithTransitions UpRight;

	// Token: 0x0400212C RID: 8492
	public TextureAnimationWithTransitions Right;
}
