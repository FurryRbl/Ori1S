using System;
using UnityEngine;

// Token: 0x020008F7 RID: 2295
public class SinMovingPlatform : SaveSerialize, ISuspendable, IDynamicGraphicHierarchy
{
	// Token: 0x17000817 RID: 2071
	// (get) Token: 0x06003318 RID: 13080 RVA: 0x000D77AF File Offset: 0x000D59AF
	// (set) Token: 0x06003319 RID: 13081 RVA: 0x000D77B7 File Offset: 0x000D59B7
	public bool IsSuspended { get; set; }

	// Token: 0x0600331A RID: 13082 RVA: 0x000D77C0 File Offset: 0x000D59C0
	public override void Awake()
	{
		SuspensionManager.Register(this);
		base.Awake();
	}

	// Token: 0x0600331B RID: 13083 RVA: 0x000D77CE File Offset: 0x000D59CE
	public override void OnDestroy()
	{
		SuspensionManager.Unregister(this);
		base.OnDestroy();
	}

	// Token: 0x0600331C RID: 13084 RVA: 0x000D77DC File Offset: 0x000D59DC
	public override void Serialize(Archive ar)
	{
		ar.Serialize(ref this.m_time);
		if (ar.Reading)
		{
			this.UpdatePosition();
		}
	}

	// Token: 0x0600331D RID: 13085 RVA: 0x000D77FC File Offset: 0x000D59FC
	public void Start()
	{
		this.m_positionCenter = base.transform.localPosition;
		if (base.GetComponent<Rigidbody>() == null)
		{
			base.gameObject.AddComponent<Rigidbody>();
			base.GetComponent<Rigidbody>().isKinematic = true;
		}
	}

	// Token: 0x0600331E RID: 13086 RVA: 0x000D7844 File Offset: 0x000D5A44
	public void FixedUpdate()
	{
		if (this.IsSuspended)
		{
			return;
		}
		this.m_time += Time.deltaTime;
		this.UpdatePosition();
	}

	// Token: 0x0600331F RID: 13087 RVA: 0x000D7878 File Offset: 0x000D5A78
	public void UpdatePosition()
	{
		float d = Mathf.Sin((this.m_time / this.Period + this.Offset) * 2f * 3.1415927f) * this.Range;
		base.transform.localPosition = this.m_positionCenter + d * Utility.DirectionToVector(this.Direction);
		int num = Math.Sign(Mathf.Cos((this.m_time / this.Period + this.Offset) * 2f * 3.1415927f));
		if (num != this.m_previousSign && num != 0)
		{
			this.m_previousSign = num;
			bool flag = this.Direction == Utility.MoveDirection.Up;
			if (num == 1)
			{
				if (this.UpSound && this.DownSound)
				{
					((!flag) ? this.DownSound : this.UpSound).Play();
				}
			}
			else if (this.UpSound && this.DownSound)
			{
				((!flag) ? this.UpSound : this.DownSound).Play();
			}
		}
	}

	// Token: 0x04002E0D RID: 11789
	public Utility.MoveDirection Direction;

	// Token: 0x04002E0E RID: 11790
	public bool DontMoveWhenDeactivatedInitially;

	// Token: 0x04002E0F RID: 11791
	public float Offset = 0.5f;

	// Token: 0x04002E10 RID: 11792
	public float Period = 3f;

	// Token: 0x04002E11 RID: 11793
	public float Range = 3f;

	// Token: 0x04002E12 RID: 11794
	public SoundSource UpSound;

	// Token: 0x04002E13 RID: 11795
	public SoundSource DownSound;

	// Token: 0x04002E14 RID: 11796
	private int m_previousSign = -1;

	// Token: 0x04002E15 RID: 11797
	private Vector3 m_positionCenter;

	// Token: 0x04002E16 RID: 11798
	private float m_time;
}
