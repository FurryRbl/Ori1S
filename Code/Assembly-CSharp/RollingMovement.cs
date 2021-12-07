using System;
using UnityEngine;

// Token: 0x02000586 RID: 1414
public class RollingMovement : SaveSerialize, ISuspendable
{
	// Token: 0x14000039 RID: 57
	// (add) Token: 0x06002469 RID: 9321 RVA: 0x0009F089 File Offset: 0x0009D289
	// (remove) Token: 0x0600246A RID: 9322 RVA: 0x0009F0A2 File Offset: 0x0009D2A2
	public event Action<Vector3, float, Collider> OnCollisionGroundEvent = delegate(Vector3 A_0, float A_1, Collider A_2)
	{
	};

	// Token: 0x1400003A RID: 58
	// (add) Token: 0x0600246B RID: 9323 RVA: 0x0009F0BB File Offset: 0x0009D2BB
	// (remove) Token: 0x0600246C RID: 9324 RVA: 0x0009F0D4 File Offset: 0x0009D2D4
	public event Action<Vector3, float, Collider> OnCollisionWallLeftEvent = delegate(Vector3 A_0, float A_1, Collider A_2)
	{
	};

	// Token: 0x1400003B RID: 59
	// (add) Token: 0x0600246D RID: 9325 RVA: 0x0009F0ED File Offset: 0x0009D2ED
	// (remove) Token: 0x0600246E RID: 9326 RVA: 0x0009F106 File Offset: 0x0009D306
	public event Action<Vector3, float, Collider> OnCollisionWallRightEvent = delegate(Vector3 A_0, float A_1, Collider A_2)
	{
	};

	// Token: 0x17000604 RID: 1540
	// (get) Token: 0x0600246F RID: 9327 RVA: 0x0009F11F File Offset: 0x0009D31F
	// (set) Token: 0x06002470 RID: 9328 RVA: 0x0009F12C File Offset: 0x0009D32C
	public float SpeedY
	{
		get
		{
			return this.Speed.y;
		}
		set
		{
			this.Speed.y = value;
		}
	}

	// Token: 0x17000605 RID: 1541
	// (get) Token: 0x06002471 RID: 9329 RVA: 0x0009F13A File Offset: 0x0009D33A
	// (set) Token: 0x06002472 RID: 9330 RVA: 0x0009F147 File Offset: 0x0009D347
	public float SpeedX
	{
		get
		{
			return this.Speed.x;
		}
		set
		{
			this.Speed.x = value;
		}
	}

	// Token: 0x17000606 RID: 1542
	// (get) Token: 0x06002473 RID: 9331 RVA: 0x0009F158 File Offset: 0x0009D358
	public float GroundAngle
	{
		get
		{
			return 57.29578f * Mathf.Atan2(-this.GroundNormal.x, this.GroundNormal.y);
		}
	}

	// Token: 0x06002474 RID: 9332 RVA: 0x0009F187 File Offset: 0x0009D387
	public Vector2 WorldToGround(Vector2 world)
	{
		return MoonMath.Angle.Unrotate(world, this.GroundAngle);
	}

	// Token: 0x06002475 RID: 9333 RVA: 0x0009F195 File Offset: 0x0009D395
	public Vector2 GroundToWorld(Vector2 local)
	{
		return MoonMath.Angle.Rotate(local, this.GroundAngle);
	}

	// Token: 0x17000607 RID: 1543
	// (get) Token: 0x06002476 RID: 9334 RVA: 0x0009F1A3 File Offset: 0x0009D3A3
	// (set) Token: 0x06002477 RID: 9335 RVA: 0x0009F1AB File Offset: 0x0009D3AB
	public bool IsSuspended { get; set; }

	// Token: 0x06002478 RID: 9336 RVA: 0x0009F1B4 File Offset: 0x0009D3B4
	public new void Awake()
	{
		base.Awake();
		this.m_rigidbody = base.GetComponent<Rigidbody>();
		this.m_rigidbody.sleepThreshold = 0f;
		SuspensionManager.Register(this);
	}

	// Token: 0x06002479 RID: 9337 RVA: 0x0009F1DE File Offset: 0x0009D3DE
	public override void OnDestroy()
	{
		SuspensionManager.Unregister(this);
	}

	// Token: 0x0600247A RID: 9338 RVA: 0x0009F1E6 File Offset: 0x0009D3E6
	public override void Serialize(Archive ar)
	{
		ar.Serialize(ref this.Speed);
	}

	// Token: 0x0600247B RID: 9339 RVA: 0x0009F1F4 File Offset: 0x0009D3F4
	public void OnCollisionEnter(Collision collision)
	{
		this.OnCollision(collision);
	}

	// Token: 0x0600247C RID: 9340 RVA: 0x0009F1FD File Offset: 0x0009D3FD
	public void OnCollisionStay(Collision collision)
	{
		this.OnCollision(collision);
	}

	// Token: 0x0600247D RID: 9341 RVA: 0x0009F208 File Offset: 0x0009D408
	public void OnCollision(Collision collision)
	{
		foreach (ContactPoint contactPoint in collision.contacts)
		{
			this.Speed -= Vector3.Dot(this.Speed.normalized, contactPoint.normal) * contactPoint.normal;
			if (Vector3.Dot(contactPoint.normal, Vector3.up) > Mathf.Cos(0.7853982f))
			{
				this.m_groundNormal += contactPoint.normal;
				this.Ground.FutureOn = true;
				this.OnCollisionGroundEvent(contactPoint.normal, Vector3.Dot(collision.relativeVelocity, contactPoint.normal), collision.collider);
			}
			if (Vector3.Dot(contactPoint.normal, Vector3.right) > Mathf.Cos(0.34906584f))
			{
				this.WallLeft.FutureOn = true;
				this.OnCollisionWallLeftEvent(contactPoint.normal, Vector3.Dot(collision.relativeVelocity, contactPoint.normal), collision.collider);
			}
			if (Vector3.Dot(contactPoint.normal, Vector3.left) > Mathf.Cos(0.34906584f))
			{
				this.WallRight.FutureOn = true;
				this.OnCollisionWallRightEvent(contactPoint.normal, Vector3.Dot(collision.relativeVelocity, contactPoint.normal), collision.collider);
			}
		}
	}

	// Token: 0x17000608 RID: 1544
	// (get) Token: 0x0600247E RID: 9342 RVA: 0x0009F38B File Offset: 0x0009D58B
	public Vector3 GroundBinormal
	{
		get
		{
			return Vector3.Cross(this.GroundNormal, Vector3.forward);
		}
	}

	// Token: 0x0600247F RID: 9343 RVA: 0x0009F3A0 File Offset: 0x0009D5A0
	public void FixedUpdate()
	{
		this.Ground.Update();
		this.WallLeft.Update();
		this.WallRight.Update();
		this.GroundNormal = ((this.m_groundNormal.magnitude != 0f) ? this.m_groundNormal.normalized : Vector3.up);
		this.IsOnGround = (this.m_groundNormal.magnitude != 0f);
		this.m_groundNormal = Vector3.zero;
		this.Speed.z = 0f;
		this.m_rigidbody.velocity = ((!this.IsSuspended) ? this.Speed : Vector3.zero);
		this.m_rigidbody.detectCollisions = true;
	}

	// Token: 0x04001EB1 RID: 7857
	private Rigidbody m_rigidbody;

	// Token: 0x04001EB2 RID: 7858
	public Vector3 Speed;

	// Token: 0x04001EB3 RID: 7859
	private Vector3 m_groundNormal;

	// Token: 0x04001EB4 RID: 7860
	public Vector3 GroundNormal;

	// Token: 0x04001EB5 RID: 7861
	public bool IsOnGround;

	// Token: 0x04001EB6 RID: 7862
	public IsOnCollisionState WallLeft = new IsOnCollisionState();

	// Token: 0x04001EB7 RID: 7863
	public IsOnCollisionState WallRight = new IsOnCollisionState();

	// Token: 0x04001EB8 RID: 7864
	public IsOnCollisionState Ground = new IsOnCollisionState();
}
