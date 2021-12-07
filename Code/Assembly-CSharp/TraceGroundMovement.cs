using System;
using UnityEngine;

// Token: 0x020004DC RID: 1244
public class TraceGroundMovement : SaveSerialize, IDamageReciever, ISuspendable
{
	// Token: 0x170005CA RID: 1482
	// (get) Token: 0x060021A5 RID: 8613 RVA: 0x000935CA File Offset: 0x000917CA
	// (set) Token: 0x060021A6 RID: 8614 RVA: 0x000935D2 File Offset: 0x000917D2
	public float Speed { get; set; }

	// Token: 0x060021A7 RID: 8615 RVA: 0x000935DB File Offset: 0x000917DB
	public override void Awake()
	{
		this.m_rigidbody = base.GetComponent<Rigidbody>();
		SuspensionManager.Register(this);
		base.Awake();
	}

	// Token: 0x060021A8 RID: 8616 RVA: 0x000935F5 File Offset: 0x000917F5
	public override void OnDestroy()
	{
		base.OnDestroy();
		SuspensionManager.Unregister(this);
	}

	// Token: 0x170005CB RID: 1483
	// (get) Token: 0x060021A9 RID: 8617 RVA: 0x00093603 File Offset: 0x00091803
	public Vector3 Right
	{
		get
		{
			return Vector3.Cross(Vector3.back, this.m_floorNormal);
		}
	}

	// Token: 0x170005CC RID: 1484
	// (get) Token: 0x060021AA RID: 8618 RVA: 0x00093615 File Offset: 0x00091815
	public Vector3 Left
	{
		get
		{
			return -this.Right;
		}
	}

	// Token: 0x170005CD RID: 1485
	// (get) Token: 0x060021AB RID: 8619 RVA: 0x00093622 File Offset: 0x00091822
	public Vector3 Up
	{
		get
		{
			return this.m_floorNormal;
		}
	}

	// Token: 0x170005CE RID: 1486
	// (get) Token: 0x060021AC RID: 8620 RVA: 0x0009362A File Offset: 0x0009182A
	public Vector3 Down
	{
		get
		{
			return -this.Up;
		}
	}

	// Token: 0x060021AD RID: 8621 RVA: 0x00093637 File Offset: 0x00091837
	public void OnCollisionEnter(Collision collision)
	{
		this.OnCollision(collision);
	}

	// Token: 0x060021AE RID: 8622 RVA: 0x00093640 File Offset: 0x00091840
	public void OnCollisionStay(Collision collision)
	{
		this.OnCollision(collision);
	}

	// Token: 0x060021AF RID: 8623 RVA: 0x0009364C File Offset: 0x0009184C
	public void OnCollision(Collision collision)
	{
		this.m_floorNormal = PhysicsHelper.CalculateAverageNormalFromContactPoints(collision.contacts);
		this.m_movingGround.SetGround(collision.transform);
		this.Surface = SurfaceToSoundProviderMap.ColliderMaterialToSurfaceMaterialType(collision.collider);
	}

	// Token: 0x060021B0 RID: 8624 RVA: 0x0009368C File Offset: 0x0009188C
	public void FixedUpdate()
	{
		this.m_movingGround.Update();
		this.Kickback.AdvanceTime();
		if (this.IsSuspended)
		{
			this.m_rigidbody.velocity = Vector3.zero;
			return;
		}
		float num = this.Speed;
		num += this.Kickback.CurrentKickbackSpeed;
		this.m_rigidbody.velocity = this.Right * num;
		Vector3 eulerAngles = base.transform.eulerAngles;
		eulerAngles = new Vector3(0f, 0f, Mathf.LerpAngle(eulerAngles.z, MoonMath.Angle.AngleFromVector(this.Right), 0.2f));
		base.transform.eulerAngles = eulerAngles;
		Vector3 vector = base.transform.position;
		Vector2 vector2 = this.m_movingGround.CalculateDelta(base.transform);
		vector.x += vector2.x;
		vector.y += vector2.y;
		float z = eulerAngles.z;
		float b = Mathf.DeltaAngle(z, this.m_lastAngle) / Time.deltaTime;
		this.m_lastAngle = z;
		this.CurrentAngularVelocity = Mathf.Lerp(this.CurrentAngularVelocity, b, 0.5f);
		if (Vector3.Distance(this.m_lastPosition, vector) > 0.03f)
		{
			this.m_lastPosition = vector;
			vector -= this.Down * 0.05f;
			base.transform.position = vector;
			RaycastHit raycastHit;
			if (this.m_rigidbody.SweepTest(this.Down, out raycastHit, 1f))
			{
				vector += this.Down * raycastHit.distance;
			}
		}
		base.transform.position = vector;
	}

	// Token: 0x060021B1 RID: 8625 RVA: 0x00093849 File Offset: 0x00091A49
	public void ApplyKickback(float kickbackMultiplier)
	{
		this.Kickback.ApplyKickback(kickbackMultiplier);
	}

	// Token: 0x060021B2 RID: 8626 RVA: 0x00093858 File Offset: 0x00091A58
	public void OnRecieveDamage(Damage damage)
	{
		if (damage.Type == DamageType.Acid)
		{
			return;
		}
		if (Vector3.Dot(this.Right, damage.Force) > 0f)
		{
			this.Kickback.ApplyKickback(damage.Force.magnitude);
		}
		else
		{
			this.Kickback.ApplyKickback(-damage.Force.magnitude);
		}
	}

	// Token: 0x060021B3 RID: 8627 RVA: 0x000938CC File Offset: 0x00091ACC
	public override void Serialize(Archive ar)
	{
		base.transform.position = ar.Serialize(base.transform.position);
		this.Speed = ar.Serialize(this.Speed);
		ar.Serialize(ref this.m_floorNormal);
	}

	// Token: 0x170005CF RID: 1487
	// (get) Token: 0x060021B4 RID: 8628 RVA: 0x00093913 File Offset: 0x00091B13
	// (set) Token: 0x060021B5 RID: 8629 RVA: 0x0009391B File Offset: 0x00091B1B
	public bool IsSuspended { get; set; }

	// Token: 0x04001C48 RID: 7240
	public Kickback Kickback = new Kickback();

	// Token: 0x04001C49 RID: 7241
	private Vector3 m_floorNormal = Vector3.up;

	// Token: 0x04001C4A RID: 7242
	private Rigidbody m_rigidbody;

	// Token: 0x04001C4B RID: 7243
	private readonly MovingGroundHelper m_movingGround = new MovingGroundHelper();

	// Token: 0x04001C4C RID: 7244
	public SurfaceMaterialType Surface;

	// Token: 0x04001C4D RID: 7245
	private Vector3 m_lastPosition;

	// Token: 0x04001C4E RID: 7246
	private float m_lastAngle;

	// Token: 0x04001C4F RID: 7247
	public float CurrentAngularVelocity;
}
