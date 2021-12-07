using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000608 RID: 1544
public class MortarWormEnemyDeath : MonoBehaviour
{
	// Token: 0x0600268A RID: 9866 RVA: 0x000A915C File Offset: 0x000A735C
	public IEnumerator Start()
	{
		yield return new WaitForFixedUpdate();
		Vector3 right = base.transform.right;
		Vector3 up = base.transform.up;
		if (Vector3.Dot(right, Vector3.up) < 0f)
		{
			Vector3 scale = base.transform.localScale;
			scale.x *= -1f;
			base.transform.localScale = scale;
		}
		SpriteAnimator animator = base.GetComponentInChildren<SpriteAnimator>();
		float x = Vector3.Dot(up, Vector3.up);
		if (x > Mathf.Cos(1.5707964f))
		{
			animator.SetAnimation(this.Upright, true);
		}
		else if (x > Mathf.Cos(2.0943952f))
		{
			animator.SetAnimation(this.Wall, true);
		}
		else if (x > Mathf.Cos(2.6179938f))
		{
			animator.SetAnimation(this.Diagonal, true);
		}
		else
		{
			animator.SetAnimation(this.UpsideDown, true);
		}
		yield break;
	}

	// Token: 0x04002135 RID: 8501
	public TextureAnimation Diagonal;

	// Token: 0x04002136 RID: 8502
	public TextureAnimation Upright;

	// Token: 0x04002137 RID: 8503
	public TextureAnimation UpsideDown;

	// Token: 0x04002138 RID: 8504
	public TextureAnimation Wall;
}
