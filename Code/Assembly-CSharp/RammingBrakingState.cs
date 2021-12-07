using System;
using UnityEngine;

// Token: 0x02000593 RID: 1427
public class RammingBrakingState : RammingEnemyState
{
	// Token: 0x060024A1 RID: 9377 RVA: 0x0009FC61 File Offset: 0x0009DE61
	public RammingBrakingState(RammingEnemy rammingEnemy) : base(rammingEnemy)
	{
	}

	// Token: 0x060024A2 RID: 9378 RVA: 0x0009FC6C File Offset: 0x0009DE6C
	public override void OnEnter()
	{
		this.GroundEnemy.FaceLeft = !this.GroundEnemy.FaceLeft;
		this.GroundEnemy.Animation.Play(this.RammingEnemy.Animations.Braking, 0, null);
		if (this.GroundEnemy.gameObject.activeInHierarchy)
		{
			this.GroundEnemy.PlaySound(this.RammingEnemy.Sounds.Brake);
		}
	}

	// Token: 0x060024A3 RID: 9379 RVA: 0x0009FCE5 File Offset: 0x0009DEE5
	public override void OnExit()
	{
	}

	// Token: 0x060024A4 RID: 9380 RVA: 0x0009FCE8 File Offset: 0x0009DEE8
	public override void UpdateState()
	{
		float runSpeed = this.RammingEnemy.Settings.RunSpeed;
		float brakingDuration = this.RammingEnemy.Settings.BrakingDuration;
		AnimationCurve brakingSpeedMultiplierOverTime = this.RammingEnemy.Settings.BrakingSpeedMultiplierOverTime;
		this.GroundEnemy.PlatformMovement.LocalSpeedX = (float)((!this.GroundEnemy.FaceLeft) ? -1 : 1) * runSpeed * brakingSpeedMultiplierOverTime.Evaluate(base.CurrentStateTime / brakingDuration);
	}

	// Token: 0x060024A5 RID: 9381 RVA: 0x0009FD61 File Offset: 0x0009DF61
	public bool HitWallIsAppropriate()
	{
		return base.CurrentStateTime < 0.1f;
	}
}
