using System;
using UnityEngine;

// Token: 0x0200090C RID: 2316
internal class PathPositionFollower : SaveSerialize, ISuspendable
{
	// Token: 0x06003367 RID: 13159 RVA: 0x000D8D78 File Offset: 0x000D6F78
	private new void Awake()
	{
		base.Awake();
		SuspensionManager.Register(this);
	}

	// Token: 0x06003368 RID: 13160 RVA: 0x000D8D86 File Offset: 0x000D6F86
	private new void OnDestroy()
	{
		base.OnDestroy();
		SuspensionManager.Unregister(this);
	}

	// Token: 0x06003369 RID: 13161 RVA: 0x000D8D94 File Offset: 0x000D6F94
	private void OnValidate()
	{
		this.ApplySettings();
	}

	// Token: 0x0600336A RID: 13162 RVA: 0x000D8D9C File Offset: 0x000D6F9C
	private void ApplySettings()
	{
		this.m_follower.Speed = this.Speed;
		this.m_follower.Node = 0;
		this.m_follower.Distance = 0f;
		this.m_follower.Path = this.LinearPath;
	}

	// Token: 0x0600336B RID: 13163 RVA: 0x000D8DE8 File Offset: 0x000D6FE8
	private void Start()
	{
		this.m_startCalled = true;
		if (!this.m_hasLoaded)
		{
			this.ApplySettings();
		}
		this.m_follower.Path = this.LinearPath;
		this.ActiveBounds.center = this.ActiveBounds.center + base.transform.position;
	}

	// Token: 0x0600336C RID: 13164 RVA: 0x000D8E40 File Offset: 0x000D7040
	private void FixedUpdate()
	{
		if (this.IsSuspended)
		{
			return;
		}
		this.m_follower.UpdateFollower(Time.fixedDeltaTime);
		base.transform.position = this.m_follower.WorldPosition;
		if (this.ChangeAngle)
		{
			base.transform.eulerAngles = new Vector3(0f, 0f, Mathf.LerpAngle(base.transform.eulerAngles.z, this.m_follower.WorldAngle, 0.2f));
		}
	}

	// Token: 0x0600336D RID: 13165 RVA: 0x000D8ECC File Offset: 0x000D70CC
	public override void Serialize(Archive ar)
	{
		if (ar.Reading)
		{
			this.m_hasLoaded = true;
		}
		if (!this.m_startCalled && !this.m_hasLoaded && ar.Writing)
		{
			this.ApplySettings();
		}
		this.m_follower.Speed = ar.Serialize(this.m_follower.Speed);
		this.m_follower.Node = ar.Serialize(this.m_follower.Node);
		this.m_follower.Distance = ar.Serialize(this.m_follower.Distance);
	}

	// Token: 0x1700081D RID: 2077
	// (get) Token: 0x0600336E RID: 13166 RVA: 0x000D8F66 File Offset: 0x000D7166
	// (set) Token: 0x0600336F RID: 13167 RVA: 0x000D8F6E File Offset: 0x000D716E
	public bool IsSuspended { get; set; }

	// Token: 0x1700081E RID: 2078
	// (get) Token: 0x06003370 RID: 13168 RVA: 0x000D8F77 File Offset: 0x000D7177
	public Bounds Bounds
	{
		get
		{
			return this.ActiveBounds;
		}
	}

	// Token: 0x06003371 RID: 13169 RVA: 0x000D8F80 File Offset: 0x000D7180
	public void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireCube(this.ActiveBounds.center + base.transform.position, this.ActiveBounds.size);
	}

	// Token: 0x04002E72 RID: 11890
	public PathFollower m_follower = new PathFollower();

	// Token: 0x04002E73 RID: 11891
	public float Speed;

	// Token: 0x04002E74 RID: 11892
	public LinearPath LinearPath;

	// Token: 0x04002E75 RID: 11893
	public bool ChangeAngle;

	// Token: 0x04002E76 RID: 11894
	private bool m_startCalled;

	// Token: 0x04002E77 RID: 11895
	private bool m_hasLoaded;

	// Token: 0x04002E78 RID: 11896
	public Bounds ActiveBounds;
}
