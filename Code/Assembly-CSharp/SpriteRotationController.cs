using System;
using UnityEngine;

// Token: 0x020004F9 RID: 1273
public class SpriteRotationController : MonoBehaviour, ISuspendable
{
	// Token: 0x0600225A RID: 8794 RVA: 0x0009690A File Offset: 0x00094B0A
	public void Awake()
	{
		this.m_transform = base.transform;
		SuspensionManager.Register(this);
	}

	// Token: 0x0600225B RID: 8795 RVA: 0x0009691E File Offset: 0x00094B1E
	public void OnDestroy()
	{
		SuspensionManager.Unregister(this);
	}

	// Token: 0x0600225C RID: 8796 RVA: 0x00096928 File Offset: 0x00094B28
	public void FixedUpdate()
	{
		if (this.IsSuspended)
		{
			return;
		}
		Vector3 eulerAngles = this.m_transform.eulerAngles;
		eulerAngles.z = Mathf.LerpAngle(eulerAngles.z, this.TargetAngle, this.RotationMultiplier);
		this.m_transform.transform.eulerAngles = eulerAngles;
	}

	// Token: 0x0600225D RID: 8797 RVA: 0x0009697D File Offset: 0x00094B7D
	public void RotateBackToNormal()
	{
		this.TargetAngle = 0f;
	}

	// Token: 0x0600225E RID: 8798 RVA: 0x0009698A File Offset: 0x00094B8A
	public void RotateTowardsTarget(Vector3 target, bool faceLeft)
	{
		this.TargetAngle = MoonMath.Angle.AngleFromVector(target);
		if (faceLeft)
		{
			this.TargetAngle = 180f - this.TargetAngle;
		}
	}

	// Token: 0x0600225F RID: 8799 RVA: 0x000969B5 File Offset: 0x00094BB5
	public void RotateTowardsTarget(Vector3 target, float angleOffset)
	{
		this.TargetAngle = MoonMath.Angle.AngleFromVector(target) + angleOffset;
	}

	// Token: 0x06002260 RID: 8800 RVA: 0x000969CC File Offset: 0x00094BCC
	public void RotateToTargetImmediately()
	{
		Vector3 eulerAngles = this.m_transform.eulerAngles;
		eulerAngles.z = this.TargetAngle;
		this.m_transform.transform.eulerAngles = eulerAngles;
	}

	// Token: 0x170005EC RID: 1516
	// (get) Token: 0x06002261 RID: 8801 RVA: 0x00096A03 File Offset: 0x00094C03
	// (set) Token: 0x06002262 RID: 8802 RVA: 0x00096A0B File Offset: 0x00094C0B
	public bool IsSuspended { get; set; }

	// Token: 0x04001CCA RID: 7370
	private Transform m_transform;

	// Token: 0x04001CCB RID: 7371
	public float TargetAngle;

	// Token: 0x04001CCC RID: 7372
	public float RotationMultiplier = 0.2f;
}
