using System;
using Game;
using UnityEngine;

// Token: 0x020009A7 RID: 2471
public class TransformFollower : MonoBehaviour
{
	// Token: 0x060035D4 RID: 13780 RVA: 0x000E1E8E File Offset: 0x000E008E
	public void Start()
	{
		if (this.FollowSein)
		{
			this.Target = Characters.Current.Transform;
		}
	}

	// Token: 0x060035D5 RID: 13781 RVA: 0x000E1EAC File Offset: 0x000E00AC
	private void FixedUpdate()
	{
		float num = base.transform.position.x - this.Target.position.x;
		float num2 = base.transform.position.y - this.Target.position.y;
		this.m_speed = new Vector3(this.XSpeedCurve.Evaluate(Mathf.Abs(num)), this.XSpeedCurve.Evaluate(Mathf.Abs(num2)), 0f);
		if (num > 0f)
		{
			this.m_speed.x = this.m_speed.x * -1f;
		}
		if (num2 > 0f)
		{
			this.m_speed.y = this.m_speed.y * -1f;
		}
		base.transform.position += this.m_speed * Time.fixedDeltaTime;
	}

	// Token: 0x04003068 RID: 12392
	public Transform Target;

	// Token: 0x04003069 RID: 12393
	public bool FollowSein;

	// Token: 0x0400306A RID: 12394
	public AnimationCurve XSpeedCurve;

	// Token: 0x0400306B RID: 12395
	public AnimationCurve YSpeedCurve;

	// Token: 0x0400306C RID: 12396
	private Vector3 m_speed = Vector3.zero;
}
