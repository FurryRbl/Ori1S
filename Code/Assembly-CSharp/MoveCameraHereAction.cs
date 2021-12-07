using System;
using Game;
using UnityEngine;

// Token: 0x02000300 RID: 768
[Category("Camera")]
public class MoveCameraHereAction : ActionWithDuration
{
	// Token: 0x060016E8 RID: 5864 RVA: 0x00063BA0 File Offset: 0x00061DA0
	public override void Perform(IContext context)
	{
		Vector3 position = this.Target.transform.position;
		position.z = -this.ZoomOffset;
		float num = -UI.Cameras.Current.Controller.Position.z;
		float num2 = 2f * num * Mathf.Tan(0.5235988f);
		float aspect = UI.Cameras.Current.Camera.aspect;
		float num3 = aspect * num2;
		float num4 = 1.7777778f * num2;
		float d = num3 - num4;
		UI.Cameras.Current.MoveToTarget(position + Vector3.right * d * this.WideScreenAdjustment * 0.5f, this.Duration, this.IgnoreScrollLock);
	}

	// Token: 0x060016E9 RID: 5865 RVA: 0x00063C5B File Offset: 0x00061E5B
	public override void Stop()
	{
		throw new NotImplementedException();
	}

	// Token: 0x17000405 RID: 1029
	// (get) Token: 0x060016EA RID: 5866 RVA: 0x00063C62 File Offset: 0x00061E62
	public override bool IsPerforming
	{
		get
		{
			throw new NotImplementedException();
		}
	}

	// Token: 0x17000406 RID: 1030
	// (get) Token: 0x060016EB RID: 5867 RVA: 0x00063C69 File Offset: 0x00061E69
	// (set) Token: 0x060016EC RID: 5868 RVA: 0x00063C71 File Offset: 0x00061E71
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

	// Token: 0x040013B2 RID: 5042
	[NotNull]
	public GameObject Target;

	// Token: 0x040013B3 RID: 5043
	public bool Active = true;

	// Token: 0x040013B4 RID: 5044
	public bool SkipMiddle = true;

	// Token: 0x040013B5 RID: 5045
	public bool MoveCamera = true;

	// Token: 0x040013B6 RID: 5046
	public bool AutoDetermineSkipMiddle = true;

	// Token: 0x040013B7 RID: 5047
	public float AutoDistance = 20f;

	// Token: 0x040013B8 RID: 5048
	public float ZoomOffset;

	// Token: 0x040013B9 RID: 5049
	public bool IgnoreScrollLock;

	// Token: 0x040013BA RID: 5050
	[Range(-1f, 1f)]
	public float WideScreenAdjustment;

	// Token: 0x040013BB RID: 5051
	public float DurationOfMovement;
}
