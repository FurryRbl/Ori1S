using System;
using Core;
using UnityEngine;

// Token: 0x020008D5 RID: 2261
public class RedirectionPortal : MonoBehaviour
{
	// Token: 0x06003255 RID: 12885 RVA: 0x000D5084 File Offset: 0x000D3284
	private void OnCollisionEnter(Collision collision)
	{
		foreach (ContactPoint contactPoint in collision.contacts)
		{
			this.OnTrigger(contactPoint.otherCollider.gameObject);
		}
	}

	// Token: 0x06003256 RID: 12886 RVA: 0x000D50CB File Offset: 0x000D32CB
	private void OnTriggerEnter(Collider collider)
	{
		this.OnTrigger(collider.gameObject);
	}

	// Token: 0x06003257 RID: 12887 RVA: 0x000D50DC File Offset: 0x000D32DC
	private void OnTrigger(GameObject gameObject)
	{
		IReflectable reflectable = gameObject.FindComponent<IReflectable>();
		if (reflectable != null)
		{
			if (this.ShakeAnimator)
			{
				this.ShakeAnimator.Restart();
			}
			gameObject.transform.position = this.Target.transform.position;
			float num = MoonMath.Angle.AngleFromVector(this.Target.transform.up);
			num = Utility.Round(num, 90f);
			reflectable.Direction = MoonMath.Angle.VectorFromAngle(num);
			if (this.RedirectSoundProvider)
			{
				Sound.Play(this.RedirectSoundProvider.GetSound(null), base.transform.position, null);
			}
		}
	}

	// Token: 0x04002D54 RID: 11604
	public GameObject Target;

	// Token: 0x04002D55 RID: 11605
	public LegacyScaleAnimator ShakeAnimator;

	// Token: 0x04002D56 RID: 11606
	public Varying2DSoundProvider RedirectSoundProvider;
}
