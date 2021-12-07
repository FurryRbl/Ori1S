using System;
using UnityEngine;

// Token: 0x02000916 RID: 2326
public class CollapsingSolid : SaveSerialize
{
	// Token: 0x06003390 RID: 13200 RVA: 0x000D93E4 File Offset: 0x000D75E4
	public void OnCollisionEnter(Collision other)
	{
		if (this.m_activated)
		{
			return;
		}
		if (other.gameObject.CompareTag("Player") && !this.m_activated)
		{
			this.m_activated = true;
			this.Animator.AnimatorDriver.Restart();
		}
	}

	// Token: 0x06003391 RID: 13201 RVA: 0x000D9434 File Offset: 0x000D7634
	public override void Serialize(Archive ar)
	{
		ar.Serialize(ref this.m_activated);
	}

	// Token: 0x04002E8F RID: 11919
	private bool m_activated;

	// Token: 0x04002E90 RID: 11920
	public BaseAnimator Animator;
}
