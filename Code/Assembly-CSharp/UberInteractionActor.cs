using System;
using Core;
using UnityEngine;

// Token: 0x02000832 RID: 2098
[ExecuteInEditMode]
public class UberInteractionActor : MonoBehaviour, IPooled
{
	// Token: 0x170007AB RID: 1963
	// (get) Token: 0x06002FE6 RID: 12262 RVA: 0x000CAE70 File Offset: 0x000C9070
	// (set) Token: 0x06002FE7 RID: 12263 RVA: 0x000CAE78 File Offset: 0x000C9078
	public UberWaterControl Water { get; set; }

	// Token: 0x06002FE8 RID: 12264 RVA: 0x000CAE81 File Offset: 0x000C9081
	private void Awake()
	{
		this.m_frame = UnityEngine.Random.Range(0, 10);
		this.UpdateRadiusFromSphere();
	}

	// Token: 0x06002FE9 RID: 12265 RVA: 0x000CAE98 File Offset: 0x000C9098
	private void OnEnable()
	{
		if (UberInteractionManager.Instance != null)
		{
			UberInteractionManager.Instance.RegisterActor(this);
		}
		if (!Application.isPlaying)
		{
			this.UpdateRadiusFromSphere();
		}
		this.m_prevTime = Time.realtimeSinceStartup;
		this.PrevPos = base.transform.position;
		this.m_inited = true;
	}

	// Token: 0x06002FEA RID: 12266 RVA: 0x000CAEF3 File Offset: 0x000C90F3
	private void Start()
	{
		this.PrevPos = base.transform.position;
	}

	// Token: 0x06002FEB RID: 12267 RVA: 0x000CAF08 File Offset: 0x000C9108
	public void UpdateRadiusFromSphere()
	{
		SphereCollider component = base.GetComponent<SphereCollider>();
		if (component)
		{
			this.Radius = component.radius * Mathf.Max(new float[]
			{
				base.transform.lossyScale.x,
				base.transform.lossyScale.y,
				base.transform.lossyScale.z
			}) / 2f;
		}
	}

	// Token: 0x06002FEC RID: 12268 RVA: 0x000CAF88 File Offset: 0x000C9188
	private void OnDisable()
	{
		if (UberInteractionManager.Instance != null)
		{
			UberInteractionManager.Instance.RemoveActor(this);
		}
		this.m_inited = false;
	}

	// Token: 0x06002FED RID: 12269 RVA: 0x000CAFB8 File Offset: 0x000C91B8
	public void OnWaterEnter()
	{
		if (this.WaterEnter)
		{
			Sound.Play(this.WaterEnter.GetSound(null), base.transform.position, null);
		}
	}

	// Token: 0x06002FEE RID: 12270 RVA: 0x000CAFF3 File Offset: 0x000C91F3
	public void OnWaterExit()
	{
	}

	// Token: 0x06002FEF RID: 12271 RVA: 0x000CAFF5 File Offset: 0x000C91F5
	public void Teleport(Vector3 actorPos)
	{
		this.PrevPos = actorPos;
	}

	// Token: 0x06002FF0 RID: 12272 RVA: 0x000CB000 File Offset: 0x000C9200
	public virtual void InteractionUpdate()
	{
		if (!this.m_inited)
		{
			return;
		}
		this.m_frame++;
		if (this.m_frame % 2 == 0)
		{
			float d;
			if (!Application.isPlaying)
			{
				d = Time.realtimeSinceStartup - this.m_prevTime;
			}
			else
			{
				d = Time.deltaTime * 2f;
			}
			Vector3 velocity = (base.transform.position - this.PrevPos) / d;
			UberInteractionManager instance = UberInteractionManager.Instance;
			instance.Interact(this, velocity, this.PrevPos, this.Priority);
			this.m_prevTime = Time.realtimeSinceStartup;
			this.PrevPos = base.transform.position;
		}
	}

	// Token: 0x06002FF1 RID: 12273 RVA: 0x000CB0AF File Offset: 0x000C92AF
	public void OnPoolSpawned()
	{
		this.PrevPos = Vector3.zero;
		this.m_prevTime = 0f;
	}

	// Token: 0x04002B1D RID: 11037
	public float Radius = 1f;

	// Token: 0x04002B1E RID: 11038
	public float ZScale = 1f;

	// Token: 0x04002B1F RID: 11039
	public Vector4 Strength = Vector4.one;

	// Token: 0x04002B20 RID: 11040
	[NonSerialized]
	public Vector3 PrevPos;

	// Token: 0x04002B21 RID: 11041
	public GameObject SplashPrefab;

	// Token: 0x04002B22 RID: 11042
	public bool OverrideSplash;

	// Token: 0x04002B23 RID: 11043
	public bool OnlyWater;

	// Token: 0x04002B24 RID: 11044
	public SoundProvider WaterEnter;

	// Token: 0x04002B25 RID: 11045
	public int Priority;

	// Token: 0x04002B26 RID: 11046
	private float m_prevTime;

	// Token: 0x04002B27 RID: 11047
	private bool m_inited;

	// Token: 0x04002B28 RID: 11048
	[PooledSafe]
	private int m_frame;
}
