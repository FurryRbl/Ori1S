using System;
using Sein.World;
using UnityEngine;

// Token: 0x02000935 RID: 2357
public class WindArea : MonoBehaviour, ISuspendable
{
	// Token: 0x17000835 RID: 2101
	// (get) Token: 0x0600341C RID: 13340 RVA: 0x000DB781 File Offset: 0x000D9981
	// (set) Token: 0x0600341D RID: 13341 RVA: 0x000DB789 File Offset: 0x000D9989
	public Transform Transform { get; set; }

	// Token: 0x0600341E RID: 13342 RVA: 0x000DB794 File Offset: 0x000D9994
	public void Awake()
	{
		if (this.RequiresWindRestored && !Events.WindRestored)
		{
			base.gameObject.SetActive(false);
		}
		this.Transform = base.transform;
		SuspensionManager.Register(this);
	}

	// Token: 0x0600341F RID: 13343 RVA: 0x000DB7D4 File Offset: 0x000D99D4
	public void OnDestroy()
	{
		SuspensionManager.Unregister(this);
	}

	// Token: 0x06003420 RID: 13344 RVA: 0x000DB7DC File Offset: 0x000D99DC
	public void OnTriggerEnter(Collider collider)
	{
		this.OnOverlap(collider);
	}

	// Token: 0x06003421 RID: 13345 RVA: 0x000DB7E5 File Offset: 0x000D99E5
	public void OnTriggerStay(Collider collider)
	{
		this.OnOverlap(collider);
	}

	// Token: 0x06003422 RID: 13346 RVA: 0x000DB7EE File Offset: 0x000D99EE
	public void OnOverlap(Collider collider)
	{
		if (this.IsSuspended)
		{
			return;
		}
		if (collider.CompareTag("Player"))
		{
			this.IsOverlapping = true;
		}
	}

	// Token: 0x06003423 RID: 13347 RVA: 0x000DB813 File Offset: 0x000D9A13
	public void FixedUpdate()
	{
		this.IsOverlapping = false;
	}

	// Token: 0x17000836 RID: 2102
	// (get) Token: 0x06003424 RID: 13348 RVA: 0x000DB81C File Offset: 0x000D9A1C
	public Vector3 WindSpeed
	{
		get
		{
			return this.Transform.rotation * Vector3.up * this.Speed;
		}
	}

	// Token: 0x17000837 RID: 2103
	// (get) Token: 0x06003425 RID: 13349 RVA: 0x000DB849 File Offset: 0x000D9A49
	public float WindHorizontalAcceleration
	{
		get
		{
			return this.HorizontalAccelerationOverSpeed.Evaluate(Mathf.Abs(this.Speed)) * this.HorizontalAcceleration;
		}
	}

	// Token: 0x17000838 RID: 2104
	// (get) Token: 0x06003426 RID: 13350 RVA: 0x000DB868 File Offset: 0x000D9A68
	public float WindVerticalAcceleration
	{
		get
		{
			return this.VerticalAccelerationOverSpeed.Evaluate(Mathf.Abs(this.Speed)) * this.VerticalAcceleration;
		}
	}

	// Token: 0x17000839 RID: 2105
	// (get) Token: 0x06003427 RID: 13351 RVA: 0x000DB887 File Offset: 0x000D9A87
	// (set) Token: 0x06003428 RID: 13352 RVA: 0x000DB88F File Offset: 0x000D9A8F
	public bool IsOverlapping { get; set; }

	// Token: 0x1700083A RID: 2106
	// (get) Token: 0x06003429 RID: 13353 RVA: 0x000DB898 File Offset: 0x000D9A98
	// (set) Token: 0x0600342A RID: 13354 RVA: 0x000DB8A0 File Offset: 0x000D9AA0
	public bool IsSuspended { get; set; }

	// Token: 0x04002F1B RID: 12059
	public bool RequiresWindRestored;

	// Token: 0x04002F1C RID: 12060
	public float Speed;

	// Token: 0x04002F1D RID: 12061
	public float HorizontalAcceleration;

	// Token: 0x04002F1E RID: 12062
	public float VerticalAcceleration = 26f;

	// Token: 0x04002F1F RID: 12063
	public AnimationCurve HorizontalAccelerationOverSpeed;

	// Token: 0x04002F20 RID: 12064
	public AnimationCurve VerticalAccelerationOverSpeed;

	// Token: 0x04002F21 RID: 12065
	public bool CancelGravity = true;
}
