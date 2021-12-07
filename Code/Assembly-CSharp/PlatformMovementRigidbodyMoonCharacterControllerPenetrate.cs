using System;
using UnityEngine;

// Token: 0x02000425 RID: 1061
public class PlatformMovementRigidbodyMoonCharacterControllerPenetrate : PlatformMovement
{
	// Token: 0x170004FD RID: 1277
	// (get) Token: 0x06001D90 RID: 7568 RVA: 0x00081F50 File Offset: 0x00080150
	private float MaxGroundAngle
	{
		get
		{
			if (this.WallLeft.IsOn || this.WallRight.IsOn)
			{
				return 57f;
			}
			return 60f;
		}
	}

	// Token: 0x170004FE RID: 1278
	// (get) Token: 0x06001D91 RID: 7569 RVA: 0x00081F7D File Offset: 0x0008017D
	private float MaxCeilingAngle
	{
		get
		{
			if (this.WallLeft.IsOn || this.WallRight.IsOn)
			{
				return 37f;
			}
			return 40f;
		}
	}

	// Token: 0x170004FF RID: 1279
	// (get) Token: 0x06001D92 RID: 7570 RVA: 0x00081FAA File Offset: 0x000801AA
	// (set) Token: 0x06001D93 RID: 7571 RVA: 0x00081FB2 File Offset: 0x000801B2
	public override bool IsSuspended
	{
		get
		{
			return this.m_isSuspended;
		}
		set
		{
			this.m_isSuspended = value;
			if (this.m_isSuspended)
			{
				this.FixedUpdate();
			}
		}
	}

	// Token: 0x06001D94 RID: 7572 RVA: 0x00081FCC File Offset: 0x000801CC
	public new void Awake()
	{
		base.Awake();
		this.m_characterController = base.GetComponent<MoonCharacterController>();
		this.m_rigidbody = base.GetComponent<Rigidbody>();
		this.m_rigidbody.sleepThreshold = 0f;
		this.WallRightNormal = Vector3.left;
		this.GroundNormal = Vector3.up;
		this.WallLeftNormal = Vector3.right;
		this.CeilingNormal = Vector3.down;
	}

	// Token: 0x06001D95 RID: 7573 RVA: 0x00082033 File Offset: 0x00080233
	public new void OnDestroy()
	{
		base.OnDestroy();
	}

	// Token: 0x06001D96 RID: 7574 RVA: 0x0008203C File Offset: 0x0008023C
	public void OnMoonControllerColliderHit(ref MoonControllerColliderHit hitInfo)
	{
		Vector2 vector = hitInfo.Normal;
		Vector2 vector2 = base.WorldToLocal(vector);
		vector.Normalize();
		vector2.Normalize();
		Collider collider = hitInfo.Collider;
		if (PlatformMovement.IsGround(vector2, collider, this.MaxGroundAngle))
		{
			this.m_movingPlatform.OnGroundMovingPlatform(hitInfo.Collider.transform);
			base.OnCollisionGround(vector2, collider);
			this.m_groundContactNormal += vector;
			if (this.IsOnWall)
			{
				hitInfo.ContinueMoveLength = 0f;
			}
		}
		else if (PlatformMovement.IsCeiling(vector2, collider, this.MaxCeilingAngle))
		{
			this.m_movingPlatform.OnCeilingMovingPlatform(hitInfo.Collider.transform);
			base.OnCollisionCeiling(vector2, collider);
			this.m_ceilingContactNormal = vector;
			if (this.IsOnWall)
			{
				hitInfo.ContinueMoveLength = 0f;
			}
		}
		else if (PlatformMovement.IsWallLeft(vector2, collider, 90f))
		{
			this.m_movingPlatform.OnWallLeftMovingPlatform(hitInfo.Collider.transform);
			base.OnCollisionWallLeft(vector2, collider);
			this.m_wallLeftContactNormal = vector;
			if (base.IsOnGroundOrCeiling)
			{
				hitInfo.ContinueMoveLength = 0f;
			}
		}
		else if (PlatformMovement.IsWallRight(vector2, collider, 90f))
		{
			this.m_movingPlatform.OnWallRightMovingPlatform(hitInfo.Collider.transform);
			base.OnCollisionWallRight(vector2, collider);
			this.m_wallRightContactNormal = vector;
			if (base.IsOnGroundOrCeiling)
			{
				hitInfo.ContinueMoveLength = 0f;
			}
		}
	}

	// Token: 0x06001D97 RID: 7575 RVA: 0x000821D2 File Offset: 0x000803D2
	public void OnCollisionEnter(Collision collision)
	{
		this.OnCollision(collision);
	}

	// Token: 0x06001D98 RID: 7576 RVA: 0x000821DB File Offset: 0x000803DB
	public void OnCollisionStay(Collision collision)
	{
		this.OnCollision(collision);
	}

	// Token: 0x06001D99 RID: 7577 RVA: 0x000821E4 File Offset: 0x000803E4
	public void OnCollision(Collision collision)
	{
		if (this.IsSuspended)
		{
			return;
		}
		for (int i = 0; i < collision.contacts.Length; i++)
		{
			ContactPoint contactPoint = collision.contacts[i];
			Vector2 vector = base.WorldToLocal(contactPoint.normal);
			if (PlatformMovement.IsGround(vector, contactPoint.otherCollider, this.MaxGroundAngle))
			{
				this.m_movingPlatform.OnGroundMovingPlatform(contactPoint.otherCollider.transform);
				base.OnCollisionGround(vector, contactPoint.otherCollider);
			}
			else if (PlatformMovement.IsCeiling(vector, contactPoint.otherCollider, this.MaxCeilingAngle))
			{
				this.m_movingPlatform.OnCeilingMovingPlatform(contactPoint.otherCollider.transform);
				base.OnCollisionCeiling(vector, contactPoint.otherCollider);
			}
			else if (PlatformMovement.IsWallLeft(vector, contactPoint.otherCollider, 90f))
			{
				this.m_movingPlatform.OnWallLeftMovingPlatform(contactPoint.otherCollider.transform);
				base.OnCollisionWallLeft(vector, contactPoint.otherCollider);
			}
			else if (PlatformMovement.IsWallRight(vector, contactPoint.otherCollider, 90f))
			{
				this.m_movingPlatform.OnWallRightMovingPlatform(contactPoint.otherCollider.transform);
				base.OnCollisionWallRight(vector, contactPoint.otherCollider);
			}
		}
		this.KeepOnSurfaceDirection = -PhysicsHelper.CalculateAverageNormalFromContactPoints(collision.contacts);
	}

	// Token: 0x06001D9A RID: 7578 RVA: 0x00082368 File Offset: 0x00080568
	public void ShrinkCapsule()
	{
		base.CapsuleCollider.radius -= this.m_characterController.MinPenetration * 2f;
		base.CapsuleCollider.height -= this.m_characterController.MinPenetration * 4f;
	}

	// Token: 0x06001D9B RID: 7579 RVA: 0x000823BC File Offset: 0x000805BC
	public void ExpandCapsule()
	{
		base.CapsuleCollider.radius += this.m_characterController.MinPenetration * 2f;
		base.CapsuleCollider.height += this.m_characterController.MinPenetration * 4f;
	}

	// Token: 0x06001D9C RID: 7580 RVA: 0x0008240F File Offset: 0x0008060F
	public void Move(Vector3 move)
	{
		this.ShrinkCapsule();
		this.m_characterController.Move(move);
		this.ExpandCapsule();
	}

	// Token: 0x06001D9D RID: 7581 RVA: 0x0008242C File Offset: 0x0008062C
	public bool Test(Vector3 move, ref MoonControllerColliderHit moonControllerColliderHit)
	{
		this.ShrinkCapsule();
		bool result = this.m_characterController.Test(move, ref moonControllerColliderHit);
		this.ExpandCapsule();
		return result;
	}

	// Token: 0x06001D9E RID: 7582 RVA: 0x00082454 File Offset: 0x00080654
	public bool Test(Vector3 move)
	{
		this.ShrinkCapsule();
		bool result = this.m_characterController.Test(move);
		this.ExpandCapsule();
		return result;
	}

	// Token: 0x06001D9F RID: 7583 RVA: 0x0008247C File Offset: 0x0008067C
	public override void PlaceOnGround(float lift = 0.5f, float distance = 0f)
	{
		if (distance == 0f)
		{
			distance = 50f;
		}
		else
		{
			distance += lift;
		}
		this.Position += base.LocalToWorld(Vector3.up * lift);
		Vector3 move = base.LocalToWorld(Vector3.down * distance);
		MoonControllerColliderHit moonControllerColliderHit = default(MoonControllerColliderHit);
		if (this.Test(move, ref moonControllerColliderHit))
		{
			this.Position += moonControllerColliderHit.MoveDirection * moonControllerColliderHit.Distance;
			this.Ground.FutureOn = true;
			this.Ground.IsOn = true;
			this.Ground.WasOn = true;
			this.GroundNormal = moonControllerColliderHit.Normal;
		}
		else
		{
			this.Position += base.LocalToWorld(Vector3.down * 0.5f);
		}
	}

	// Token: 0x06001DA0 RID: 7584 RVA: 0x00082590 File Offset: 0x00080790
	public void TestAgainstWall()
	{
		MoonControllerColliderHit moonControllerColliderHit = default(MoonControllerColliderHit);
		if (this.Test(this.WallLeftNormal * -0.4f, ref moonControllerColliderHit) && PlatformMovement.IsWallLeft(base.WorldToLocal(moonControllerColliderHit.Normal), moonControllerColliderHit.Collider, 30f))
		{
			this.OnMoonControllerColliderHit(ref moonControllerColliderHit);
		}
		if (this.Test(this.WallRightNormal * -0.4f, ref moonControllerColliderHit) && PlatformMovement.IsWallRight(base.WorldToLocal(moonControllerColliderHit.Normal), moonControllerColliderHit.Collider, 30f))
		{
			this.OnMoonControllerColliderHit(ref moonControllerColliderHit);
		}
	}

	// Token: 0x06001DA1 RID: 7585 RVA: 0x0008264C File Offset: 0x0008084C
	public void FixedUpdate()
	{
		if (this.m_isSuspended)
		{
			this.m_rigidbody.velocity = Vector3.zero;
			this.m_rigidbody.detectCollisions = false;
			if (!this.m_rigidbody.isKinematic)
			{
				this.m_rigidbody.Sleep();
			}
			return;
		}
		if (!this.m_rigidbody.detectCollisions)
		{
			this.m_rigidbody.detectCollisions = true;
		}
		if (this.m_rigidbody.IsSleeping())
		{
			this.m_rigidbody.WakeUp();
		}
		base.PreFixedUpdate();
		if (this.ForceKeepInAir)
		{
			this.Ground.IsOn = (this.WallLeft.IsOn = (this.WallRight.IsOn = (this.Ceiling.IsOn = false)));
		}
		Vector3 vector = (this.WorldSpeed + base.GravityBinormal * this.AdditionalXSpeed) * Time.deltaTime;
		if (this.HasWallLeft)
		{
			vector = base.WallLeftBinormal * base.LocalSpeedY * Time.deltaTime;
			if (this.Ceiling.IsOn && base.LocalSpeedY > 0f)
			{
				vector = Vector3.zero;
			}
			if (this.Ground.IsOn && base.LocalSpeedY < 0f)
			{
				vector = Vector3.zero;
			}
		}
		else if (this.HasWallRight)
		{
			vector = base.WallRightBinormal * base.LocalSpeedY * Time.deltaTime;
			if (this.Ceiling.IsOn && base.LocalSpeedY > 0f)
			{
				vector = Vector3.zero;
			}
			if (this.Ground.IsOn && base.LocalSpeedY < 0f)
			{
				vector = Vector3.zero;
			}
		}
		else if (this.IsOnGround)
		{
			vector = base.GroundBinormal * (base.LocalSpeedX + this.AdditionalXSpeed) * Time.deltaTime;
			if (this.Ceiling.IsOn && Mathf.Sign(Vector3.Dot(base.GroundBinormal, this.CeilingNormal)) != Mathf.Sign(base.LocalSpeedX))
			{
				vector = Vector3.zero;
			}
		}
		else if (base.IsOnCeiling)
		{
			vector = base.CeilingBinormal * (base.LocalSpeedX + this.AdditionalXSpeed) * Time.deltaTime;
			if (this.Ground.IsOn && Mathf.Sign(Vector3.Dot(base.GroundBinormal, this.CeilingNormal)) != Mathf.Sign(base.LocalSpeedX))
			{
				vector = Vector3.zero;
			}
		}
		if (this.HasWallLeft && base.LocalSpeedX < 0f)
		{
			base.LocalSpeedX = Mathf.Clamp(base.LocalSpeedX, -12f, 12f);
			base.LocalSpeedX *= 0.9f;
		}
		if (this.HasWallRight && base.LocalSpeedX > 0f)
		{
			base.LocalSpeedX = Mathf.Clamp(base.LocalSpeedX, -12f, 12f);
			base.LocalSpeedX *= 0.9f;
		}
		if (this.IsOnGround && base.LocalSpeedY < 0f)
		{
			base.LocalSpeedY = 0f;
		}
		if (base.IsOnCeiling && base.LocalSpeedY > 0f)
		{
			base.LocalSpeedY = 0f;
		}
		if (this.IsOnGround && base.IsOnCeiling && Mathf.Sign(Vector3.Dot(base.GroundBinormal, this.CeilingNormal)) != Mathf.Sign(base.LocalSpeedX))
		{
			base.LocalSpeedX = 0f;
		}
		if (this.KinematicMode)
		{
			vector = this.LocalSpeed * Time.deltaTime;
		}
		Vector3 position = this.Position;
		this.Move(vector);
		base.UpdateRays();
		if (this.ForceKeepInAir)
		{
			this.Ground.IsOn = (this.WallLeft.IsOn = (this.WallRight.IsOn = (this.Ceiling.IsOn = false)));
			this.ForceKeepInAir = false;
		}
		if (base.IsOnGroundOrCeiling)
		{
			MoonControllerColliderHit moonControllerColliderHit = default(MoonControllerColliderHit);
			if (this.IsOnGround && this.GroundRayHit && this.Test(this.GroundNormal * -0.4f, ref moonControllerColliderHit) && PlatformMovement.IsGround(base.WorldToLocal(moonControllerColliderHit.Normal), moonControllerColliderHit.Collider, this.MaxGroundAngle))
			{
				this.Position += moonControllerColliderHit.Distance * moonControllerColliderHit.MoveDirection;
				this.OnMoonControllerColliderHit(ref moonControllerColliderHit);
			}
			if (this.HasWallLeft && this.WallLeftRayHit && this.Test(this.WallLeftNormal * -0.4f, ref moonControllerColliderHit))
			{
				Vector2 v = base.WorldToLocal(moonControllerColliderHit.Normal);
				if (!PlatformMovement.IsGround(v, moonControllerColliderHit.Collider, this.MaxGroundAngle) && !PlatformMovement.IsCeiling(v, moonControllerColliderHit.Collider, this.MaxCeilingAngle))
				{
					this.OnMoonControllerColliderHit(ref moonControllerColliderHit);
				}
			}
			if (this.HasWallRight && this.WallRightRayHit && this.Test(this.WallRightNormal * -0.4f, ref moonControllerColliderHit))
			{
				Vector2 v2 = base.WorldToLocal(moonControllerColliderHit.Normal);
				if (!PlatformMovement.IsGround(v2, moonControllerColliderHit.Collider, this.MaxGroundAngle) && !PlatformMovement.IsCeiling(v2, moonControllerColliderHit.Collider, this.MaxCeilingAngle))
				{
					this.OnMoonControllerColliderHit(ref moonControllerColliderHit);
				}
			}
		}
		else if (this.IsOnWall)
		{
			MoonControllerColliderHit moonControllerColliderHit2 = default(MoonControllerColliderHit);
			if (this.HasWallLeft && this.WallLeftRayHit && this.Test(this.WallLeftNormal * -0.4f, ref moonControllerColliderHit2))
			{
				Vector2 v3 = base.WorldToLocal(moonControllerColliderHit2.Normal);
				if (!PlatformMovement.IsGround(v3, moonControllerColliderHit2.Collider, this.MaxGroundAngle) && !PlatformMovement.IsCeiling(v3, moonControllerColliderHit2.Collider, this.MaxCeilingAngle))
				{
					this.OnMoonControllerColliderHit(ref moonControllerColliderHit2);
					this.Position += moonControllerColliderHit2.Distance * moonControllerColliderHit2.MoveDirection;
				}
			}
			if (this.HasWallRight && this.WallRightRayHit && this.Test(this.WallRightNormal * -0.4f, ref moonControllerColliderHit2))
			{
				Vector2 v4 = base.WorldToLocal(moonControllerColliderHit2.Normal);
				if (!PlatformMovement.IsGround(v4, moonControllerColliderHit2.Collider, this.MaxGroundAngle) && !PlatformMovement.IsCeiling(v4, moonControllerColliderHit2.Collider, this.MaxCeilingAngle))
				{
					this.OnMoonControllerColliderHit(ref moonControllerColliderHit2);
					this.Position += moonControllerColliderHit2.Distance * moonControllerColliderHit2.MoveDirection;
				}
			}
		}
		base.PostFixedUpdate();
		this.UpdateNormals();
		vector = this.Position - position;
		this.Position = position;
		this.m_rigidbody.velocity = vector / Time.deltaTime;
		this.UpdateHeadAndFeetAgainstTheWall();
		this.Unity5PhysicsBugWorkAround();
	}

	// Token: 0x06001DA2 RID: 7586 RVA: 0x00082E2C File Offset: 0x0008102C
	public void Unity5PhysicsBugWorkAround()
	{
		base.transform.eulerAngles = new Vector3(0f, 0f, this.GravityAngle);
		this.m_rigidbody.angularVelocity = Vector3.zero;
	}

	// Token: 0x17000500 RID: 1280
	// (get) Token: 0x06001DA3 RID: 7587 RVA: 0x00082E69 File Offset: 0x00081069
	// (set) Token: 0x06001DA4 RID: 7588 RVA: 0x00082E71 File Offset: 0x00081071
	public bool GroundNormalIsValid { get; set; }

	// Token: 0x06001DA5 RID: 7589 RVA: 0x00082E7C File Offset: 0x0008107C
	private void UpdateNormals()
	{
		this.GroundNormalIsValid = false;
		if (!this.WallRight.IsOn)
		{
			this.WallRightNormal = Vector3.left;
		}
		if (!this.Ground.IsOn)
		{
			this.GroundNormal = Vector3.up;
			this.GroundNormalIsValid = true;
		}
		if (!this.WallLeft.IsOn)
		{
			this.WallLeftNormal = Vector3.right;
		}
		if (!this.Ceiling.IsOn)
		{
			this.CeilingNormal = Vector3.down;
		}
		if (this.m_groundContactNormal.sqrMagnitude != 0f)
		{
			this.GroundNormal = this.m_groundContactNormal.normalized;
			this.GroundNormalIsValid = true;
		}
		if (this.m_ceilingContactNormal.sqrMagnitude != 0f)
		{
			this.CeilingNormal = this.m_ceilingContactNormal.normalized;
		}
		if (this.m_wallLeftContactNormal.sqrMagnitude != 0f)
		{
			this.WallLeftNormal = this.m_wallLeftContactNormal.normalized;
		}
		if (this.m_wallRightContactNormal.sqrMagnitude != 0f)
		{
			this.WallRightNormal = this.m_wallRightContactNormal.normalized;
		}
		this.m_groundContactNormal = Vector3.zero;
		this.m_wallLeftContactNormal = Vector3.zero;
		this.m_wallRightContactNormal = Vector3.zero;
		this.m_ceilingContactNormal = Vector3.zero;
	}

	// Token: 0x06001DA6 RID: 7590 RVA: 0x00082FF8 File Offset: 0x000811F8
	public void UpdateHeadAndFeetAgainstTheWall()
	{
		this.HeadAgainstWall = false;
		this.FeetAgainstWall = false;
		if (this.IsOnWall)
		{
			float maxDistance = base.CapsuleCollider.radius * base.transform.localScale.x * 2f;
			Vector3 direction = base.LocalToWorld((!this.HasWallLeft) ? Vector3.right : Vector3.left);
			if (Physics.Raycast(base.HeadPosition, direction, maxDistance))
			{
				this.HeadAgainstWall = true;
			}
			if (Physics.Raycast(this.FeetPosition, direction, maxDistance))
			{
				this.FeetAgainstWall = true;
			}
		}
	}

	// Token: 0x06001DA7 RID: 7591 RVA: 0x000830A4 File Offset: 0x000812A4
	public override void Serialize(Archive ar)
	{
		this.m_rigidbody.velocity = ar.Serialize(this.m_rigidbody.velocity);
		ar.Serialize(ref this.m_ceilingContactNormal);
		ar.Serialize(ref this.m_groundContactNormal);
		ar.Serialize(ref this.m_wallLeftContactNormal);
		ar.Serialize(ref this.m_wallRightContactNormal);
		base.Serialize(ar);
	}

	// Token: 0x04001989 RID: 6537
	private Vector2 m_ceilingContactNormal;

	// Token: 0x0400198A RID: 6538
	private MoonCharacterController m_characterController;

	// Token: 0x0400198B RID: 6539
	private Vector2 m_groundContactNormal;

	// Token: 0x0400198C RID: 6540
	private bool m_isSuspended;

	// Token: 0x0400198D RID: 6541
	private Rigidbody m_rigidbody;

	// Token: 0x0400198E RID: 6542
	private Vector2 m_wallLeftContactNormal;

	// Token: 0x0400198F RID: 6543
	private Vector2 m_wallRightContactNormal;
}
