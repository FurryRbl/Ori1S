using System;
using Game;
using UnityEngine;

// Token: 0x020009C5 RID: 2501
public class RisingWater : SaveSerialize, ISuspendable
{
	// Token: 0x17000877 RID: 2167
	// (get) Token: 0x0600368A RID: 13962 RVA: 0x000E53F9 File Offset: 0x000E35F9
	private static RisingWater Instance
	{
		get
		{
			return RisingWater.m_instance;
		}
	}

	// Token: 0x0600368B RID: 13963 RVA: 0x000E5400 File Offset: 0x000E3600
	public static void SetProperties(float speed)
	{
		if (RisingWater.Instance)
		{
			RisingWater.Instance.Speed = speed;
		}
	}

	// Token: 0x17000878 RID: 2168
	// (get) Token: 0x0600368C RID: 13964 RVA: 0x000E541C File Offset: 0x000E361C
	public static bool Available
	{
		get
		{
			return RisingWater.Instance != null;
		}
	}

	// Token: 0x17000879 RID: 2169
	// (get) Token: 0x0600368D RID: 13965 RVA: 0x000E5429 File Offset: 0x000E3629
	public static Vector2 Position
	{
		get
		{
			return RisingWater.Instance.transform.position;
		}
	}

	// Token: 0x0600368E RID: 13966 RVA: 0x000E543F File Offset: 0x000E363F
	public override void Awake()
	{
		base.Awake();
		SuspensionManager.Register(this);
		RisingWater.m_instance = this;
	}

	// Token: 0x0600368F RID: 13967 RVA: 0x000E5453 File Offset: 0x000E3653
	public new void OnDestroy()
	{
		SuspensionManager.Unregister(this);
	}

	// Token: 0x06003690 RID: 13968 RVA: 0x000E545B File Offset: 0x000E365B
	public void Start()
	{
	}

	// Token: 0x06003691 RID: 13969 RVA: 0x000E5460 File Offset: 0x000E3660
	private void FixedUpdate()
	{
		if (this.IsSuspended)
		{
			return;
		}
		float num = this.Speed;
		float time = Characters.Sein.Position.y - base.transform.position.y;
		if (DifficultyController.Instance.Difficulty == DifficultyMode.Easy)
		{
			num += this.EasySpeedOverDistance.Evaluate(time);
		}
		else
		{
			num += this.SpeedOverDistance.Evaluate(time);
		}
		this.m_currentSpeed = Mathf.Lerp(this.m_currentSpeed, num, 0.1f);
		base.transform.position = new Vector3(base.transform.position.x, base.transform.position.y + this.m_currentSpeed * Time.fixedDeltaTime);
	}

	// Token: 0x06003692 RID: 13970 RVA: 0x000E5538 File Offset: 0x000E3738
	public override void Serialize(Archive ar)
	{
		ar.Serialize(ref this.Speed);
		base.transform.position = ar.Serialize(base.transform.position);
	}

	// Token: 0x1700087A RID: 2170
	// (get) Token: 0x06003693 RID: 13971 RVA: 0x000E556D File Offset: 0x000E376D
	// (set) Token: 0x06003694 RID: 13972 RVA: 0x000E5575 File Offset: 0x000E3775
	public bool IsSuspended { get; set; }

	// Token: 0x0400316F RID: 12655
	private static RisingWater m_instance;

	// Token: 0x04003170 RID: 12656
	public float Speed;

	// Token: 0x04003171 RID: 12657
	public float MinDistanceToAccelerate = 20f;

	// Token: 0x04003172 RID: 12658
	public float AcceleratedSpeed = 20f;

	// Token: 0x04003173 RID: 12659
	private float m_currentSpeed;

	// Token: 0x04003174 RID: 12660
	public float Acceleration = 2f;

	// Token: 0x04003175 RID: 12661
	public float Deceleration = 2f;

	// Token: 0x04003176 RID: 12662
	public AnimationCurve EasySpeedOverDistance;

	// Token: 0x04003177 RID: 12663
	public AnimationCurve SpeedOverDistance;
}
