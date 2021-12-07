using System;
using System.Collections;
using Game;
using UnityEngine;

// Token: 0x020002BD RID: 701
[Category("Sein")]
public class BabySeinRunToTargetAction : PerformingAction
{
	// Token: 0x060015F0 RID: 5616 RVA: 0x000612E1 File Offset: 0x0005F4E1
	public override void Stop()
	{
	}

	// Token: 0x170003E1 RID: 993
	// (get) Token: 0x060015F1 RID: 5617 RVA: 0x000612E3 File Offset: 0x0005F4E3
	public override bool IsPerforming
	{
		get
		{
			return this.m_isPerforming;
		}
	}

	// Token: 0x060015F2 RID: 5618 RVA: 0x000612EC File Offset: 0x0005F4EC
	public override void Perform(IContext context)
	{
		this.m_isPerforming = true;
		Characters.BabySein.Controller.IgnoreControllerInput = true;
		base.StartCoroutine(this.Perform());
	}

	// Token: 0x060015F3 RID: 5619 RVA: 0x00061320 File Offset: 0x0005F520
	public IEnumerator Perform()
	{
		float distance = this.TargetPosition.position.x - Characters.BabySein.transform.position.x;
		while (Mathf.Abs(distance) > 0.1f)
		{
			Characters.BabySein.PlatformBehaviour.LeftRightMovement.HorizontalInput = Mathf.Sign(distance);
			yield return new WaitForFixedUpdate();
			distance = this.TargetPosition.position.x - Characters.BabySein.transform.position.x;
		}
		Characters.BabySein.Controller.IgnoreControllerInput = false;
		this.m_isPerforming = false;
		yield break;
	}

	// Token: 0x040012DC RID: 4828
	[NotNull]
	public Transform TargetPosition;

	// Token: 0x040012DD RID: 4829
	private bool m_isPerforming;
}
