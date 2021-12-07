using System;
using UnityEngine;

// Token: 0x02000503 RID: 1283
public class DashOwlFlyHomeState : DashOwlState
{
	// Token: 0x0600227E RID: 8830 RVA: 0x000970C0 File Offset: 0x000952C0
	public DashOwlFlyHomeState(DashOwlEnemy dashOwl) : base(dashOwl)
	{
	}

	// Token: 0x0600227F RID: 8831 RVA: 0x000970CC File Offset: 0x000952CC
	public bool IsHome()
	{
		return this.DashOwl.PositionToStartPosition.magnitude < 0.01f;
	}

	// Token: 0x06002280 RID: 8832 RVA: 0x000970F4 File Offset: 0x000952F4
	public override void UpdateState()
	{
		base.UpdateState();
		this.m_remainingTime -= Time.deltaTime;
		float num = this.DashOwl.Settings.FlyBackHorizontal.Evaluate(this.m_remainingTime);
		float y = this.DashOwl.Settings.FlyBackVertical.Evaluate(this.m_remainingTime);
		Vector3 b = new Vector3((!this.m_flyLeft) ? (-num) : num, y);
		Vector3 a = Vector2.Lerp(this.DashOwl.StartPosition + b, this.m_startPosition, this.m_remainingTime / this.m_duration);
		this.DashOwl.FlyMovement.VelocityAsDelta = a - this.DashOwl.Position;
		if (this.m_remainingTime < 0.2f)
		{
			this.DashOwl.Animation.PlayLoop(this.DashOwl.Animations.Idle, 0, null, false);
		}
	}

	// Token: 0x06002281 RID: 8833 RVA: 0x00097200 File Offset: 0x00095400
	public override void OnEnter()
	{
		this.m_flyLeft = (this.DashOwl.PositionToStartPosition.x < 0f);
		this.DashOwl.FaceLeft = this.m_flyLeft;
		this.DashOwl.Animation.PlayLoop(this.DashOwl.Animations.FlyHome, 0, null, false);
		this.m_startPosition = this.DashOwl.Position;
		this.m_duration = (this.DashOwl.StartPosition - this.m_startPosition).magnitude / this.DashOwl.Settings.MoveBackSpeed;
		this.m_remainingTime = this.m_duration;
		this.DashOwl.FlyMovement.Rigidbody.detectCollisions = false;
	}

	// Token: 0x06002282 RID: 8834 RVA: 0x000972CA File Offset: 0x000954CA
	public override void OnExit()
	{
		this.DashOwl.FlyMovement.Rigidbody.detectCollisions = true;
		base.OnExit();
	}

	// Token: 0x04001CE7 RID: 7399
	private bool m_flyLeft;

	// Token: 0x04001CE8 RID: 7400
	private float m_remainingTime;

	// Token: 0x04001CE9 RID: 7401
	private float m_duration;

	// Token: 0x04001CEA RID: 7402
	private Vector3 m_startPosition;
}
