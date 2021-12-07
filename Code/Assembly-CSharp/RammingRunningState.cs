using System;
using UnityEngine;

// Token: 0x02000592 RID: 1426
public class RammingRunningState : RammingEnemyState
{
	// Token: 0x0600249D RID: 9373 RVA: 0x0009FB5F File Offset: 0x0009DD5F
	public RammingRunningState(RammingEnemy rammingEnemy) : base(rammingEnemy)
	{
	}

	// Token: 0x0600249E RID: 9374 RVA: 0x0009FB68 File Offset: 0x0009DD68
	public override void OnEnter()
	{
		this.GroundEnemy.Animation.PlayLoop(this.RammingEnemy.Animations.Running, 0, null, false);
		if (this.GroundEnemy.gameObject.activeInHierarchy)
		{
			this.GroundEnemy.PlaySound(this.RammingEnemy.Sounds.Run);
		}
	}

	// Token: 0x0600249F RID: 9375 RVA: 0x0009FBC9 File Offset: 0x0009DDC9
	public override void OnExit()
	{
		this.GroundEnemy.StopSound(this.RammingEnemy.Sounds.Run);
	}

	// Token: 0x060024A0 RID: 9376 RVA: 0x0009FBE8 File Offset: 0x0009DDE8
	public override void UpdateState()
	{
		float accelerationDuration = this.RammingEnemy.Settings.AccelerationDuration;
		AnimationCurve runningSpeedMultipliedOverTime = this.RammingEnemy.Settings.RunningSpeedMultipliedOverTime;
		float runSpeed = this.RammingEnemy.Settings.RunSpeed;
		this.GroundEnemy.PlatformMovement.LocalSpeedX = (float)((!this.GroundEnemy.FaceLeft) ? 1 : -1) * runSpeed * runningSpeedMultipliedOverTime.Evaluate(base.CurrentStateTime / accelerationDuration);
	}
}
