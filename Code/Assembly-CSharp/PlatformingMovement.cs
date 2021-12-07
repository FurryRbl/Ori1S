using System;
using UnityEngine;

// Token: 0x0200056C RID: 1388
public class PlatformingMovement : PlatformMovement
{
	// Token: 0x17000601 RID: 1537
	// (get) Token: 0x06002405 RID: 9221 RVA: 0x0009D1A9 File Offset: 0x0009B3A9
	// (set) Token: 0x06002406 RID: 9222 RVA: 0x0009D1B1 File Offset: 0x0009B3B1
	public override bool IsSuspended { get; set; }

	// Token: 0x06002407 RID: 9223 RVA: 0x0009D1BC File Offset: 0x0009B3BC
	public new void Awake()
	{
		base.Awake();
		this.m_rigidbody = base.GetComponent<Rigidbody>();
		this.m_rigidbody.sleepThreshold = 0f;
	}

	// Token: 0x06002408 RID: 9224 RVA: 0x0009D1EB File Offset: 0x0009B3EB
	public void OnCollisionEnter(Collision collision)
	{
		this.OnCollision(collision);
	}

	// Token: 0x06002409 RID: 9225 RVA: 0x0009D1F4 File Offset: 0x0009B3F4
	public void OnCollisionStay(Collision collision)
	{
		this.OnCollision(collision);
	}

	// Token: 0x0600240A RID: 9226 RVA: 0x0009D200 File Offset: 0x0009B400
	public void OnCollision(Collision collision)
	{
		for (int i = 0; i < collision.contacts.Length; i++)
		{
			ContactPoint contactPoint = collision.contacts[i];
			Vector2 vector = base.WorldToLocal(contactPoint.normal);
			if (PlatformMovement.IsWallLeft(vector, contactPoint.otherCollider, 30f))
			{
				base.OnCollisionWallLeft(vector, contactPoint.otherCollider);
			}
			if (PlatformMovement.IsWallRight(vector, contactPoint.otherCollider, 30f))
			{
				base.OnCollisionWallRight(vector, contactPoint.otherCollider);
			}
			if (PlatformMovement.IsGround(vector, contactPoint.otherCollider, 60f))
			{
				this.m_groundContactNormal += vector;
				base.OnCollisionGround(vector, contactPoint.otherCollider);
			}
			if (PlatformMovement.IsCeiling(vector, contactPoint.otherCollider, 60f))
			{
				base.OnCollisionCeiling(vector, contactPoint.otherCollider);
			}
		}
	}

	// Token: 0x0600240B RID: 9227 RVA: 0x0009D308 File Offset: 0x0009B508
	public void FixedUpdate()
	{
		if (this.IsSuspended)
		{
			this.m_rigidbody.velocity = Vector3.zero;
			if (this.m_rigidbody.detectCollisions)
			{
				this.m_rigidbody.detectCollisions = false;
			}
		}
		else
		{
			if (!this.m_rigidbody.detectCollisions)
			{
				this.m_rigidbody.detectCollisions = true;
			}
			base.PreFixedUpdate();
			if (this.m_groundContactNormal.magnitude == 0f)
			{
				this.GroundNormal = Vector3.up;
			}
			else
			{
				this.GroundNormal = this.m_groundContactNormal.normalized;
			}
			this.m_groundContactNormal = Vector3.zero;
			if (this.IsOnGround && !Physics.Raycast(new Ray(this.Position + base.WorldOffsetToBottomSphereOfCapsuleCollider, base.GravityDirection), base.CapsuleCollider.radius * base.transform.lossyScale.y + 0.5f))
			{
				this.Ground.IsOn = false;
			}
			if (this.IsOnGround)
			{
				base.LocalSpeedY = 0f;
				Vector3 position = base.transform.position;
				base.transform.position += base.GroundBinormal * base.LocalSpeedX * Time.deltaTime;
				base.transform.position += this.GroundNormal * 0.02f;
				float d = 0.04f + Mathf.Abs(base.LocalSpeedX) * Time.deltaTime;
				Vector3 vector = d * -this.GroundNormal;
				RaycastHit raycastHit;
				if (this.m_rigidbody.SweepTest(vector.normalized, out raycastHit, vector.magnitude))
				{
					base.transform.position += vector.normalized * (raycastHit.distance + 0.02f);
				}
				else
				{
					base.transform.position -= this.GroundNormal * 0.02f;
				}
				if (Time.deltaTime == 0f)
				{
					this.m_rigidbody.velocity = Vector3.zero;
				}
				else
				{
					this.m_rigidbody.velocity = (base.transform.position - position) / Time.deltaTime;
				}
				this.m_rigidbody.position = position;
			}
			else
			{
				this.m_rigidbody.velocity = this.WorldSpeed;
			}
			base.PostFixedUpdate();
		}
	}

	// Token: 0x0600240C RID: 9228 RVA: 0x0009D5B8 File Offset: 0x0009B7B8
	public override void PlaceOnGround(float lift = 0.5f, float distance = 0f)
	{
		this.Position += base.LocalToWorld(Vector3.up * lift);
		if (distance == 0f)
		{
			distance = 50f;
		}
		else
		{
			distance += lift;
		}
		Vector3 vector = base.LocalToWorld(Vector3.down * distance);
		RaycastHit raycastHit;
		if (this.m_rigidbody.SweepTest(vector.normalized, out raycastHit, vector.magnitude))
		{
			this.Position += raycastHit.distance * vector.normalized;
		}
		else
		{
			this.Position += base.LocalToWorld(Vector3.down * 0.5f);
		}
	}

	// Token: 0x04001E24 RID: 7716
	private Rigidbody m_rigidbody;

	// Token: 0x04001E25 RID: 7717
	private Vector2 m_groundContactNormal;
}
