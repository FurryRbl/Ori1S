using System;
using UnityEngine;

// Token: 0x020006CD RID: 1741
[RequireComponent(typeof(Rigidbody))]
[ExecuteInEditMode]
public class ApplyTurbulentForce : MonoBehaviour, ISuspendable
{
	// Token: 0x060029C5 RID: 10693 RVA: 0x000B3DD5 File Offset: 0x000B1FD5
	private void Awake()
	{
		SuspensionManager.Register(this);
		this.m_rigidBody = base.GetComponent<Rigidbody>();
	}

	// Token: 0x060029C6 RID: 10694 RVA: 0x000B3DE9 File Offset: 0x000B1FE9
	public void OnDestroy()
	{
		SuspensionManager.Unregister(this);
	}

	// Token: 0x170006A0 RID: 1696
	// (get) Token: 0x060029C7 RID: 10695 RVA: 0x000B3DF1 File Offset: 0x000B1FF1
	// (set) Token: 0x060029C8 RID: 10696 RVA: 0x000B3DF9 File Offset: 0x000B1FF9
	public bool IsSuspended { get; set; }

	// Token: 0x060029C9 RID: 10697 RVA: 0x000B3E04 File Offset: 0x000B2004
	private void FixedUpdate()
	{
		if (this.IsSuspended)
		{
			return;
		}
		float d = 1f - 2f * Mathf.PerlinNoise((float)(FixedRandom.FixedUpdateIndex * 1) / 60f * this.TurbulenceFrequency + this.TurbulenceOffset, 0f);
		Vector3 vector = this.TurbulenceMagnitude * this.ForceMultiplier * d * 0.5f;
		if (TurbulenceManager.Instance != null)
		{
			vector *= TurbulenceManager.Instance.GetStrengthMultiplier();
		}
		float num = Vector3.Dot(this.m_rigidBody.velocity, vector);
		if (num > 0f || this.m_rigidBody.velocity.magnitude < 0.02f)
		{
			this.m_rigidBody.AddForceSafe(vector, ForceMode.Force);
		}
	}

	// Token: 0x04002538 RID: 9528
	public bool IsManagedByPhysicsSystemManager;

	// Token: 0x04002539 RID: 9529
	public Vector3 ForceMultiplier;

	// Token: 0x0400253A RID: 9530
	public float TurbulenceFrequency = 1f;

	// Token: 0x0400253B RID: 9531
	public float TurbulenceMagnitude = 1f;

	// Token: 0x0400253C RID: 9532
	public float TurbulenceOffset;

	// Token: 0x0400253D RID: 9533
	private Rigidbody m_rigidBody;
}
