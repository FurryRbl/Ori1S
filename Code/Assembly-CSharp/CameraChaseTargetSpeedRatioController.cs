using System;
using Game;
using UnityEngine;

// Token: 0x020003DB RID: 987
public class CameraChaseTargetSpeedRatioController : Suspendable
{
	// Token: 0x06001B0B RID: 6923 RVA: 0x00073DDD File Offset: 0x00071FDD
	public void Start()
	{
		this.m_originalSpeedRatio = this.CameraChaseTarget.SpeedRatio;
	}

	// Token: 0x06001B0C RID: 6924 RVA: 0x00073DF0 File Offset: 0x00071FF0
	public void FixedUpdate()
	{
		if (this.IsSuspended)
		{
			return;
		}
		if (Characters.Sein)
		{
			PlatformMovement platformMovement = Characters.Sein.PlatformBehaviour.PlatformMovement;
			float magnitude = platformMovement.LocalSpeed.magnitude;
			float num = Mathf.Clamp01(Mathf.InverseLerp(this.MinSpeed, this.MaxSpeed, magnitude));
			this.CameraChaseTarget.SpeedRatio = this.m_originalSpeedRatio + this.AdditionalSpeed * num;
		}
	}

	// Token: 0x17000479 RID: 1145
	// (get) Token: 0x06001B0D RID: 6925 RVA: 0x00073E69 File Offset: 0x00072069
	// (set) Token: 0x06001B0E RID: 6926 RVA: 0x00073E71 File Offset: 0x00072071
	public override bool IsSuspended { get; set; }

	// Token: 0x0400177E RID: 6014
	public CameraChaseTarget CameraChaseTarget;

	// Token: 0x0400177F RID: 6015
	private float m_originalSpeedRatio;

	// Token: 0x04001780 RID: 6016
	public float AdditionalSpeed;

	// Token: 0x04001781 RID: 6017
	public float MinSpeed;

	// Token: 0x04001782 RID: 6018
	public float MaxSpeed;
}
