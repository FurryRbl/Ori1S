using System;
using UnityEngine;

// Token: 0x02000068 RID: 104
public abstract class BaseAnimator : Suspendable, IPooled, IInScene, IDynamicGraphicHierarchy
{
	// Token: 0x17000113 RID: 275
	// (get) Token: 0x06000450 RID: 1104 RVA: 0x00011C54 File Offset: 0x0000FE54
	public AnimatorDriver AnimatorDriver
	{
		get
		{
			if (this.m_animatorDriver == null)
			{
				this.m_animatorDriver = new AnimatorDriver();
				this.m_animatorDriver.Animator = this;
			}
			return this.m_animatorDriver;
		}
	}

	// Token: 0x17000114 RID: 276
	// (get) Token: 0x06000451 RID: 1105
	public abstract bool IsLooping { get; }

	// Token: 0x17000115 RID: 277
	// (get) Token: 0x06000452 RID: 1106 RVA: 0x00011C89 File Offset: 0x0000FE89
	// (set) Token: 0x06000453 RID: 1107 RVA: 0x00011C91 File Offset: 0x0000FE91
	public bool IsInitialized { get; protected set; }

	// Token: 0x17000116 RID: 278
	// (get) Token: 0x06000454 RID: 1108 RVA: 0x00011C9A File Offset: 0x0000FE9A
	// (set) Token: 0x06000455 RID: 1109 RVA: 0x00011CA2 File Offset: 0x0000FEA2
	public bool IsInScene
	{
		get
		{
			return this.m_isInScene;
		}
		set
		{
			this.m_isInScene = value;
		}
	}

	// Token: 0x06000456 RID: 1110 RVA: 0x00011CAB File Offset: 0x0000FEAB
	public virtual void OnPoolSpawned()
	{
		if (!this.SampleOnStart)
		{
			this.RestoreToOriginalState();
		}
		if (this.m_animatorDriver != null)
		{
			this.m_animatorDriver.OnPoolSpawned();
		}
	}

	// Token: 0x06000457 RID: 1111 RVA: 0x00011CD4 File Offset: 0x0000FED4
	public new void Awake()
	{
		this.Initialize();
		base.Awake();
	}

	// Token: 0x06000458 RID: 1112 RVA: 0x00011CE2 File Offset: 0x0000FEE2
	public void Start()
	{
		if (this.PlayAtStart)
		{
			this.AnimatorDriver.Restart();
		}
		if (this.SampleOnStart)
		{
			this.SampleValue(0f, true);
		}
	}

	// Token: 0x06000459 RID: 1113 RVA: 0x00011D14 File Offset: 0x0000FF14
	public void FixedUpdate()
	{
		if (this.m_isSuspended)
		{
			return;
		}
		if (this.m_animatorDriver != null)
		{
			this.m_animatorDriver.FixedUpdate();
		}
	}

	// Token: 0x0600045A RID: 1114 RVA: 0x00011D43 File Offset: 0x0000FF43
	public void Initialize()
	{
		if (this.IsInitialized)
		{
			return;
		}
		this.CacheOriginals();
		this.IsInitialized = true;
	}

	// Token: 0x0600045B RID: 1115
	public abstract void CacheOriginals();

	// Token: 0x0600045C RID: 1116
	public abstract void SampleValue(float value, bool forceSample);

	// Token: 0x0600045D RID: 1117 RVA: 0x00011D5E File Offset: 0x0000FF5E
	public float AnimationCurveTimeToTime(float time)
	{
		return time / this.Speed + this.TimeOffset;
	}

	// Token: 0x0600045E RID: 1118 RVA: 0x00011D6F File Offset: 0x0000FF6F
	public float TimeToAnimationCurveTime(float time)
	{
		return (time - this.TimeOffset) * this.Speed;
	}

	// Token: 0x17000117 RID: 279
	// (get) Token: 0x0600045F RID: 1119
	public abstract float Duration { get; }

	// Token: 0x06000460 RID: 1120
	public abstract void RestoreToOriginalState();

	// Token: 0x17000118 RID: 280
	// (get) Token: 0x06000461 RID: 1121 RVA: 0x00011D80 File Offset: 0x0000FF80
	// (set) Token: 0x06000462 RID: 1122 RVA: 0x00011D88 File Offset: 0x0000FF88
	public override bool IsSuspended
	{
		get
		{
			return this.m_isSuspended;
		}
		set
		{
			this.m_isSuspended = value;
		}
	}

	// Token: 0x0400038F RID: 911
	private AnimatorDriver m_animatorDriver;

	// Token: 0x04000390 RID: 912
	public bool SampleOnStart;

	// Token: 0x04000391 RID: 913
	[SerializeField]
	[HideInInspector]
	private bool m_isInScene;

	// Token: 0x04000392 RID: 914
	public float TimeOffset;

	// Token: 0x04000393 RID: 915
	public float Speed = 1f;

	// Token: 0x04000394 RID: 916
	public bool PlayAtStart;

	// Token: 0x04000395 RID: 917
	private bool m_isSuspended;
}
