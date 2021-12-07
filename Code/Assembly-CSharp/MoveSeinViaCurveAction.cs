using System;
using System.Collections;
using Game;
using UnityEngine;

// Token: 0x02000302 RID: 770
[Category("Sein")]
public class MoveSeinViaCurveAction : ActionWithDuration
{
	// Token: 0x060016F5 RID: 5877 RVA: 0x00063D13 File Offset: 0x00061F13
	public override void Perform(IContext context)
	{
		base.StartCoroutine("PerformActionCoroutine");
	}

	// Token: 0x060016F6 RID: 5878 RVA: 0x00063D24 File Offset: 0x00061F24
	public override void Stop()
	{
		base.StopCoroutine("PerformActionCoroutine");
		Characters.Sein.PlatformBehaviour.PlatformMovement.KinematicMode = false;
	}

	// Token: 0x17000409 RID: 1033
	// (get) Token: 0x060016F7 RID: 5879 RVA: 0x00063D51 File Offset: 0x00061F51
	public override bool IsPerforming
	{
		get
		{
			return base.IsInvoking("PerformActionCoroutine");
		}
	}

	// Token: 0x060016F8 RID: 5880 RVA: 0x00063D60 File Offset: 0x00061F60
	public IEnumerator PerformActionCoroutine()
	{
		PlatformMovement platformMovement = Characters.Sein.PlatformBehaviour.PlatformMovement;
		platformMovement.KinematicMode = true;
		Vector3 originalPosition = platformMovement.Position;
		for (float t = 0f; t < this.Duration; t += ((!this.IsSuspended) ? Time.deltaTime : 0f))
		{
			Vector3 position = originalPosition + new Vector3(this.PositionX.Evaluate(t), this.PositionY.Evaluate(t), 0f);
			platformMovement.Position = position;
			yield return new WaitForFixedUpdate();
		}
		platformMovement.KinematicMode = false;
		yield break;
	}

	// Token: 0x1700040A RID: 1034
	// (get) Token: 0x060016F9 RID: 5881 RVA: 0x00063D7B File Offset: 0x00061F7B
	// (set) Token: 0x060016FA RID: 5882 RVA: 0x00063D83 File Offset: 0x00061F83
	public override float Duration
	{
		get
		{
			return this.DurationOfMovement;
		}
		set
		{
			this.DurationOfMovement = value;
		}
	}

	// Token: 0x040013BD RID: 5053
	public bool Active = true;

	// Token: 0x040013BE RID: 5054
	public AnimationCurve PositionX;

	// Token: 0x040013BF RID: 5055
	public AnimationCurve PositionY;

	// Token: 0x040013C0 RID: 5056
	public float DurationOfMovement;
}
