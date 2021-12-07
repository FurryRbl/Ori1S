using System;
using UnityEngine;

// Token: 0x020008F5 RID: 2293
public class RiseSinkPlatform : SaveSerialize, ISuspendable, IDynamicGraphicHierarchy
{
	// Token: 0x0600330F RID: 13071 RVA: 0x000D75ED File Offset: 0x000D57ED
	private new void Awake()
	{
		base.Awake();
		SuspensionManager.Register(this);
	}

	// Token: 0x06003310 RID: 13072 RVA: 0x000D75FB File Offset: 0x000D57FB
	private new void OnDestroy()
	{
		base.OnDestroy();
		SuspensionManager.Unregister(this);
	}

	// Token: 0x06003311 RID: 13073 RVA: 0x000D7609 File Offset: 0x000D5809
	private void Start()
	{
		this.m_startPosition = this.TransformToAffect.localPosition;
	}

	// Token: 0x06003312 RID: 13074 RVA: 0x000D761C File Offset: 0x000D581C
	private void OnCollisionStay(Collision collision)
	{
		if (collision.gameObject.CompareTag("Player"))
		{
			this.m_state = RiseSinkPlatform.State.Sink;
		}
	}

	// Token: 0x06003313 RID: 13075 RVA: 0x000D763C File Offset: 0x000D583C
	private void FixedUpdate()
	{
		if (this.IsSuspended)
		{
			return;
		}
		float fixedDeltaTime = Time.fixedDeltaTime;
		Vector3 down = Vector3.down;
		RiseSinkPlatform.State state = this.m_state;
		if (state != RiseSinkPlatform.State.Rise)
		{
			if (state == RiseSinkPlatform.State.Sink)
			{
				float d = fixedDeltaTime * this.SinkSpeed;
				this.TransformToAffect.localPosition += down * d;
				this.m_state = RiseSinkPlatform.State.Rise;
			}
		}
		else
		{
			float num = fixedDeltaTime * this.RiseSpeed;
			Vector3 vector = this.m_startPosition - this.TransformToAffect.localPosition;
			if (vector.magnitude > num)
			{
				vector.Normalize();
				vector *= num;
				this.TransformToAffect.localPosition += vector;
			}
			else
			{
				this.m_state = RiseSinkPlatform.State.Rest;
				this.TransformToAffect.localPosition = this.m_startPosition;
			}
		}
	}

	// Token: 0x06003314 RID: 13076 RVA: 0x000D7728 File Offset: 0x000D5928
	public override void Serialize(Archive ar)
	{
		this.m_state = (RiseSinkPlatform.State)ar.Serialize((int)this.m_state);
		this.TransformToAffect.localPosition = ar.Serialize(this.TransformToAffect.localPosition);
	}

	// Token: 0x17000816 RID: 2070
	// (get) Token: 0x06003315 RID: 13077 RVA: 0x000D7763 File Offset: 0x000D5963
	// (set) Token: 0x06003316 RID: 13078 RVA: 0x000D776B File Offset: 0x000D596B
	public bool IsSuspended { get; set; }

	// Token: 0x04002E03 RID: 11779
	public float SinkSpeed = 1f;

	// Token: 0x04002E04 RID: 11780
	public float RiseSpeed = 1f;

	// Token: 0x04002E05 RID: 11781
	private Vector3 m_startPosition;

	// Token: 0x04002E06 RID: 11782
	public Transform TransformToAffect;

	// Token: 0x04002E07 RID: 11783
	private RiseSinkPlatform.State m_state;

	// Token: 0x020008F6 RID: 2294
	private enum State
	{
		// Token: 0x04002E0A RID: 11786
		Rise,
		// Token: 0x04002E0B RID: 11787
		Sink,
		// Token: 0x04002E0C RID: 11788
		Rest
	}
}
