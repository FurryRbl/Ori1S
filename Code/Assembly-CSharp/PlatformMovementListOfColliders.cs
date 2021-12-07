using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200002F RID: 47
public class PlatformMovementListOfColliders : SaveSerialize
{
	// Token: 0x060001F3 RID: 499 RVA: 0x00008488 File Offset: 0x00006688
	public void Start()
	{
		this.PlatformMovement.OnCollisionGroundEvent += this.OnCollisionGroundEvent;
		this.PlatformMovement.OnCollisionCeilingEvent += this.OnCollisionCeilingEvent;
		this.PlatformMovement.OnCollisionWallLeftEvent += this.OnCollisionWallLeftEvent;
		this.PlatformMovement.OnCollisionWallRightEvent += this.OnCollisionWallRightEvent;
	}

	// Token: 0x060001F4 RID: 500 RVA: 0x000084F4 File Offset: 0x000066F4
	public new void OnDestroy()
	{
		base.OnDestroy();
		this.PlatformMovement.OnCollisionGroundEvent -= this.OnCollisionGroundEvent;
		this.PlatformMovement.OnCollisionCeilingEvent -= this.OnCollisionCeilingEvent;
		this.PlatformMovement.OnCollisionWallLeftEvent -= this.OnCollisionWallLeftEvent;
		this.PlatformMovement.OnCollisionWallRightEvent -= this.OnCollisionWallRightEvent;
	}

	// Token: 0x060001F5 RID: 501 RVA: 0x00008564 File Offset: 0x00006764
	public override void Serialize(Archive ar)
	{
		if (ar.Reading)
		{
			this.GroundColliders.Clear();
			this.CeilingColliders.Clear();
			this.WallLeftColliders.Clear();
			this.WallRightColliders.Clear();
		}
	}

	// Token: 0x17000093 RID: 147
	// (get) Token: 0x060001F6 RID: 502 RVA: 0x000085A8 File Offset: 0x000067A8
	public Collider GroundCollider
	{
		get
		{
			if (this.GroundColliders.Count == 0)
			{
				return null;
			}
			return this.GroundColliders[0];
		}
	}

	// Token: 0x17000094 RID: 148
	// (get) Token: 0x060001F7 RID: 503 RVA: 0x000085C8 File Offset: 0x000067C8
	public Collider CeilingCollider
	{
		get
		{
			if (this.CeilingColliders.Count == 0)
			{
				return null;
			}
			return this.CeilingColliders[0];
		}
	}

	// Token: 0x17000095 RID: 149
	// (get) Token: 0x060001F8 RID: 504 RVA: 0x000085E8 File Offset: 0x000067E8
	public Collider WallLeftCollider
	{
		get
		{
			if (this.WallLeftColliders.Count == 0)
			{
				return null;
			}
			return this.WallLeftColliders[0];
		}
	}

	// Token: 0x17000096 RID: 150
	// (get) Token: 0x060001F9 RID: 505 RVA: 0x00008608 File Offset: 0x00006808
	public Collider WallRightCollider
	{
		get
		{
			if (this.WallRightColliders.Count == 0)
			{
				return null;
			}
			return this.WallRightColliders[0];
		}
	}

	// Token: 0x060001FA RID: 506 RVA: 0x00008628 File Offset: 0x00006828
	private void OnCollisionGroundEvent(Vector3 normal, Collider collider)
	{
		if (!this.GroundColliders.Contains(collider))
		{
			this.GroundColliders.Add(collider);
		}
	}

	// Token: 0x060001FB RID: 507 RVA: 0x00008647 File Offset: 0x00006847
	private void OnCollisionCeilingEvent(Vector3 normal, Collider collider)
	{
		if (!this.CeilingColliders.Contains(collider))
		{
			this.CeilingColliders.Add(collider);
		}
	}

	// Token: 0x060001FC RID: 508 RVA: 0x00008666 File Offset: 0x00006866
	private void OnCollisionWallLeftEvent(Vector3 normal, Collider collider)
	{
		if (!this.WallLeftColliders.Contains(collider))
		{
			this.WallLeftColliders.Add(collider);
		}
	}

	// Token: 0x060001FD RID: 509 RVA: 0x00008685 File Offset: 0x00006885
	private void OnCollisionWallRightEvent(Vector3 normal, Collider collider)
	{
		if (!this.WallRightColliders.Contains(collider))
		{
			this.WallRightColliders.Add(collider);
		}
	}

	// Token: 0x060001FE RID: 510 RVA: 0x000086A4 File Offset: 0x000068A4
	public void FixedUpdate()
	{
		this.GroundColliders.Clear();
		this.CeilingColliders.Clear();
		this.WallLeftColliders.Clear();
		this.WallRightColliders.Clear();
	}

	// Token: 0x0400019E RID: 414
	public PlatformMovement PlatformMovement;

	// Token: 0x0400019F RID: 415
	public List<Collider> GroundColliders = new List<Collider>();

	// Token: 0x040001A0 RID: 416
	public List<Collider> CeilingColliders = new List<Collider>();

	// Token: 0x040001A1 RID: 417
	public List<Collider> WallLeftColliders = new List<Collider>();

	// Token: 0x040001A2 RID: 418
	public List<Collider> WallRightColliders = new List<Collider>();
}
