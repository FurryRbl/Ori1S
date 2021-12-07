using System;
using UnityEngine;

// Token: 0x02000463 RID: 1123
[RequireComponent(typeof(Rigidbody))]
public class MoonCharacterController : MonoBehaviour
{
	// Token: 0x06001EE1 RID: 7905 RVA: 0x00087EFC File Offset: 0x000860FC
	public void Awake()
	{
		this.m_controllerPenetrate = base.GetComponent<PlatformMovementRigidbodyMoonCharacterControllerPenetrate>();
		this.m_rigidbody = base.GetComponent<Rigidbody>();
	}

	// Token: 0x06001EE2 RID: 7906 RVA: 0x00087F16 File Offset: 0x00086116
	public void Move(Vector3 delta)
	{
		this.Move(delta, delta.normalized);
	}

	// Token: 0x06001EE3 RID: 7907 RVA: 0x00087F26 File Offset: 0x00086126
	public bool SweepTestSafe(Vector3 direction, out RaycastHit hitInfo, float magnitude)
	{
		return this.m_rigidbody.SweepTest(direction, out hitInfo, magnitude);
	}

	// Token: 0x06001EE4 RID: 7908 RVA: 0x00087F38 File Offset: 0x00086138
	public bool Test(Vector3 delta)
	{
		RaycastHit raycastHit;
		return this.SweepTestSafe(delta.normalized, out raycastHit, delta.magnitude);
	}

	// Token: 0x06001EE5 RID: 7909 RVA: 0x00087F64 File Offset: 0x00086164
	public bool Test(Vector3 delta, ref MoonControllerColliderHit moonHitInfo)
	{
		RaycastHit raycastHit;
		if (this.SweepTestSafe(delta.normalized, out raycastHit, delta.magnitude))
		{
			moonHitInfo = default(MoonControllerColliderHit);
			MoonControllerColliderHit moonControllerColliderHit = moonHitInfo;
			moonControllerColliderHit.Collider = raycastHit.collider;
			moonControllerColliderHit.Controller = this;
			moonControllerColliderHit.MoveDirection = delta.normalized;
			moonControllerColliderHit.MoveLength = delta.magnitude;
			moonControllerColliderHit.Normal = raycastHit.normal;
			moonControllerColliderHit.Point = raycastHit.point;
			moonControllerColliderHit.ContinueMoveLength = 0f;
			moonControllerColliderHit.ContinueMoveDirection = Vector3.zero;
			moonControllerColliderHit.Distance = raycastHit.distance - this.MinPenetration;
			moonHitInfo = moonControllerColliderHit;
			return true;
		}
		return false;
	}

	// Token: 0x06001EE6 RID: 7910 RVA: 0x0008801F File Offset: 0x0008621F
	public void Move(Vector3 delta, Vector3 originalDelta)
	{
		this.m_safeRecursion = 0;
		this.MovePrivate(delta, originalDelta);
	}

	// Token: 0x06001EE7 RID: 7911 RVA: 0x00088030 File Offset: 0x00086230
	private void MovePrivate(Vector3 delta, Vector3 originalDelta)
	{
		this.m_safeRecursion++;
		if (this.m_safeRecursion > 10)
		{
			return;
		}
		Vector3 normalized = delta.normalized;
		float magnitude = delta.magnitude;
		RaycastHit raycastHit;
		if (this.SweepTestSafe(normalized, out raycastHit, magnitude))
		{
			Vector3 normal = raycastHit.normal;
			float num = raycastHit.distance - this.MinPenetration;
			base.transform.position += num * normalized;
			Vector3 vector = Vector3.Cross(Vector3.forward, normal);
			float continueMoveLength = Vector3.Dot(vector, originalDelta.normalized * (magnitude - num));
			MoonControllerColliderHit moonControllerColliderHit = default(MoonControllerColliderHit);
			MoonControllerColliderHit moonControllerColliderHit2 = moonControllerColliderHit;
			moonControllerColliderHit2.Collider = raycastHit.collider;
			moonControllerColliderHit2.Controller = this;
			moonControllerColliderHit2.MoveDirection = normalized;
			moonControllerColliderHit2.MoveLength = magnitude;
			moonControllerColliderHit2.Normal = raycastHit.normal;
			moonControllerColliderHit2.Point = raycastHit.point;
			moonControllerColliderHit2.ContinueMoveLength = continueMoveLength;
			moonControllerColliderHit2.ContinueMoveDirection = vector;
			moonControllerColliderHit2.Distance = num;
			moonControllerColliderHit = moonControllerColliderHit2;
			this.m_controllerPenetrate.OnMoonControllerColliderHit(ref moonControllerColliderHit);
			Vector3 delta2 = moonControllerColliderHit.ContinueMoveDirection * moonControllerColliderHit.ContinueMoveLength;
			if ((double)delta2.magnitude < 0.004)
			{
				return;
			}
			this.MovePrivate(delta2, originalDelta);
		}
		else
		{
			base.transform.position += delta;
		}
	}

	// Token: 0x04001AB2 RID: 6834
	public float MinPenetration = 0.01f;

	// Token: 0x04001AB3 RID: 6835
	private int m_safeRecursion;

	// Token: 0x04001AB4 RID: 6836
	private Rigidbody m_rigidbody;

	// Token: 0x04001AB5 RID: 6837
	private PlatformMovementRigidbodyMoonCharacterControllerPenetrate m_controllerPenetrate;
}
