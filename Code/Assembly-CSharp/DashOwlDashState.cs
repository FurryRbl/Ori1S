using System;
using UnityEngine;

// Token: 0x02000500 RID: 1280
public class DashOwlDashState : DashOwlState
{
	// Token: 0x06002277 RID: 8823 RVA: 0x00096EC6 File Offset: 0x000950C6
	public DashOwlDashState(DashOwlEnemy dashOwl) : base(dashOwl)
	{
	}

	// Token: 0x06002278 RID: 8824 RVA: 0x00096ED0 File Offset: 0x000950D0
	public override void OnEnter()
	{
		this.m_dashTargetOffset = (this.DashOwl.Controller.LastSeenSeinPosition - this.DashOwl.transform.position).normalized * this.DashOwl.Settings.DashDistance;
		this.DashOwl.DashSound.Play();
		this.DashOwl.Animation.Play(this.DashOwl.Animations.Dash, 0, null);
		this.DashOwl.SpriteRotation.RotateTowardsTarget(this.DashOwl.PositionToPlayerPosition, this.DashOwl.FaceLeft);
	}

	// Token: 0x06002279 RID: 8825 RVA: 0x00096F7E File Offset: 0x0009517E
	public override void OnExit()
	{
		this.DashOwl.SpriteRotation.RotateBackToNormal();
	}

	// Token: 0x0600227A RID: 8826 RVA: 0x00096F90 File Offset: 0x00095190
	public override void UpdateState()
	{
		this.DashOwl.FlyMovement.Kickback.Stop();
		Vector3 a = this.m_dashTargetOffset * (this.DashOwl.Settings.DashCurve.Evaluate(base.CurrentStateTime + Time.deltaTime) - this.DashOwl.Settings.DashCurve.Evaluate(base.CurrentStateTime));
		this.DashOwl.FlyMovement.Velocity = ((Time.deltaTime != 0f) ? (a / Time.deltaTime) : Vector3.zero);
		base.UpdateState();
	}

	// Token: 0x04001CDF RID: 7391
	private Vector3 m_dashTargetOffset;
}
