using System;
using Game;
using UnityEngine;

// Token: 0x02000301 RID: 769
[Category("Camera")]
public class MoveCameraToPlayerAction : ActionWithDuration
{
	// Token: 0x060016EE RID: 5870 RVA: 0x00063C84 File Offset: 0x00061E84
	public override void Perform(IContext context)
	{
		if (Characters.Current as Component)
		{
			UI.Cameras.Current.Target = Characters.Current.Transform;
		}
		UI.Cameras.Current.MoveToTargetCharacter(this.Duration);
	}

	// Token: 0x060016EF RID: 5871 RVA: 0x00063CC9 File Offset: 0x00061EC9
	public override void Stop()
	{
		throw new NotImplementedException();
	}

	// Token: 0x17000407 RID: 1031
	// (get) Token: 0x060016F0 RID: 5872 RVA: 0x00063CD0 File Offset: 0x00061ED0
	public override bool IsPerforming
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	// Token: 0x17000408 RID: 1032
	// (get) Token: 0x060016F1 RID: 5873 RVA: 0x00063CD7 File Offset: 0x00061ED7
	// (set) Token: 0x060016F2 RID: 5874 RVA: 0x00063CDF File Offset: 0x00061EDF
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

	// Token: 0x060016F3 RID: 5875 RVA: 0x00063CE8 File Offset: 0x00061EE8
	public override string GetNiceName()
	{
		return "Move camera to player over " + this.Duration + " seconds";
	}

	// Token: 0x040013BC RID: 5052
	public float DurationOfMovement;
}
