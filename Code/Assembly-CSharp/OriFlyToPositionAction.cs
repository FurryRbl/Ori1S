using System;
using Core;
using Game;
using UnityEngine;

// Token: 0x02000308 RID: 776
[Category("Ori")]
public class OriFlyToPositionAction : ActionWithDuration
{
	// Token: 0x06001717 RID: 5911 RVA: 0x000640B4 File Offset: 0x000622B4
	public override void Perform(IContext context)
	{
		if (this.Target == null)
		{
			return;
		}
		if (this.LookAtIt && Characters.Sein)
		{
			Characters.Sein.FaceLeft = (this.Target.position.x < Characters.Sein.Position.x);
		}
		if (Characters.Ori)
		{
			Characters.Ori.MoveOriToPosition(this.Target.position, this.DurationOfMovement);
			if (this.MovingSoundProvider)
			{
				Sound.Play(this.MovingSoundProvider.GetSound(null), Characters.Ori.transform.position, null);
			}
		}
	}

	// Token: 0x06001718 RID: 5912 RVA: 0x0006417A File Offset: 0x0006237A
	public override void Stop()
	{
		throw new NotImplementedException();
	}

	// Token: 0x17000412 RID: 1042
	// (get) Token: 0x06001719 RID: 5913 RVA: 0x00064181 File Offset: 0x00062381
	public override bool IsPerforming
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	// Token: 0x17000413 RID: 1043
	// (get) Token: 0x0600171A RID: 5914 RVA: 0x00064188 File Offset: 0x00062388
	// (set) Token: 0x0600171B RID: 5915 RVA: 0x00064190 File Offset: 0x00062390
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

	// Token: 0x040013D9 RID: 5081
	[NotNull]
	public Transform Target;

	// Token: 0x040013DA RID: 5082
	public bool LookAtIt = true;

	// Token: 0x040013DB RID: 5083
	public SoundProvider MovingSoundProvider;

	// Token: 0x040013DC RID: 5084
	public float DurationOfMovement;
}
