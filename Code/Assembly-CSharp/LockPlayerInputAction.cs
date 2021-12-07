using System;
using System.Collections;
using UnityEngine;

// Token: 0x020002FB RID: 763
[Category("Sein")]
public class LockPlayerInputAction : ActionWithDuration
{
	// Token: 0x060016D1 RID: 5841 RVA: 0x00063933 File Offset: 0x00061B33
	public override void Perform(IContext context)
	{
		base.StartCoroutine("PerformActionCoroutine");
	}

	// Token: 0x060016D2 RID: 5842 RVA: 0x00063941 File Offset: 0x00061B41
	public override void Stop()
	{
		base.StopCoroutine("PerformActionCoroutine");
		GameController.Instance.LockInputByAction = false;
	}

	// Token: 0x17000400 RID: 1024
	// (get) Token: 0x060016D3 RID: 5843 RVA: 0x00063959 File Offset: 0x00061B59
	public override bool IsPerforming
	{
		get
		{
			return base.IsInvoking("PerformActionCoroutine");
		}
	}

	// Token: 0x060016D4 RID: 5844 RVA: 0x00063968 File Offset: 0x00061B68
	public IEnumerator PerformActionCoroutine()
	{
		GameController.Instance.LockInputByAction = true;
		for (float t = 0f; t < this.Duration; t += ((!this.IsSuspended) ? Time.deltaTime : 0f))
		{
			yield return new WaitForFixedUpdate();
		}
		GameController.Instance.LockInputByAction = false;
		yield break;
	}

	// Token: 0x17000401 RID: 1025
	// (get) Token: 0x060016D5 RID: 5845 RVA: 0x00063983 File Offset: 0x00061B83
	// (set) Token: 0x060016D6 RID: 5846 RVA: 0x0006398B File Offset: 0x00061B8B
	public override float Duration
	{
		get
		{
			return this.DurationPlayerCantMove;
		}
		set
		{
			this.DurationPlayerCantMove = value;
		}
	}

	// Token: 0x040013AA RID: 5034
	public float DurationPlayerCantMove;
}
