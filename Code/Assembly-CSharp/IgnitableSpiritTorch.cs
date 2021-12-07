using System;
using System.Collections.Generic;
using Game;
using UnityEngine;

// Token: 0x02000248 RID: 584
public class IgnitableSpiritTorch : SaveSerialize
{
	// Token: 0x060013D8 RID: 5080 RVA: 0x0005AE6C File Offset: 0x0005906C
	// Note: this type is marked as 'beforefieldinit'.
	static IgnitableSpiritTorch()
	{
		IgnitableSpiritTorch.OnLightTorchWithGrenadeEvent = delegate()
		{
		};
	}

	// Token: 0x1400002A RID: 42
	// (add) Token: 0x060013D9 RID: 5081 RVA: 0x0005AEA5 File Offset: 0x000590A5
	// (remove) Token: 0x060013DA RID: 5082 RVA: 0x0005AEBC File Offset: 0x000590BC
	public static event Action OnLightTorchWithGrenadeEvent;

	// Token: 0x060013DB RID: 5083 RVA: 0x0005AED4 File Offset: 0x000590D4
	public override void Awake()
	{
		base.Awake();
		this.m_transform = base.transform;
		this.UpdateLightSettings();
		IgnitableSpiritTorch.m_all.Add(this);
	}

	// Token: 0x060013DC RID: 5084 RVA: 0x0005AF04 File Offset: 0x00059104
	public void UpdateLightSettings()
	{
		if (this.m_isLit)
		{
			this.LightSource.GetComponent<SpiritLightRadialVisualAffector>().Radius = this.LitRadius;
		}
		else
		{
			this.LightSource.GetComponent<SpiritLightRadialVisualAffector>().Radius = this.UnlitRadius;
		}
	}

	// Token: 0x060013DD RID: 5085 RVA: 0x0005AF4D File Offset: 0x0005914D
	public override void OnDestroy()
	{
		base.OnDestroy();
		IgnitableSpiritTorch.m_all.Remove(this);
	}

	// Token: 0x060013DE RID: 5086 RVA: 0x0005AF64 File Offset: 0x00059164
	public static IgnitableSpiritTorch IgniteAnyTorchesNearPosition(Vector3 position)
	{
		foreach (IgnitableSpiritTorch ignitableSpiritTorch in IgnitableSpiritTorch.m_all)
		{
			if (!ignitableSpiritTorch.m_isLit && Vector3.Distance(ignitableSpiritTorch.Position, position) < 2f)
			{
				ignitableSpiritTorch.Light(true);
				return ignitableSpiritTorch;
			}
		}
		return null;
	}

	// Token: 0x060013DF RID: 5087 RVA: 0x0005AFE8 File Offset: 0x000591E8
	public void Light(bool byGrenade)
	{
		this.m_isLit = true;
		if (this.OnLitAction)
		{
			this.OnLitAction.Perform(null);
		}
		this.UpdateLightSettings();
		if (byGrenade)
		{
			IgnitableSpiritTorch.OnLightTorchWithGrenadeEvent();
		}
	}

	// Token: 0x17000389 RID: 905
	// (get) Token: 0x060013E0 RID: 5088 RVA: 0x0005B023 File Offset: 0x00059223
	public Vector3 Position
	{
		get
		{
			return this.m_transform.position;
		}
	}

	// Token: 0x060013E1 RID: 5089 RVA: 0x0005B030 File Offset: 0x00059230
	public override void Serialize(Archive ar)
	{
		ar.Serialize(ref this.m_isLit);
		if (ar.Reading)
		{
			this.UpdateLightSettings();
		}
	}

	// Token: 0x060013E2 RID: 5090 RVA: 0x0005B050 File Offset: 0x00059250
	public void FixedUpdate()
	{
		if (!this.m_isLit && Items.LightTorch && Vector3.Distance(Items.LightTorch.Position, this.Position) < this.TouchRadius)
		{
			this.Light(false);
		}
	}

	// Token: 0x04001178 RID: 4472
	private const int GRENADE_IGNITE_RADIUS = 2;

	// Token: 0x04001179 RID: 4473
	private static List<IgnitableSpiritTorch> m_all = new List<IgnitableSpiritTorch>();

	// Token: 0x0400117A RID: 4474
	public ActionSequence OnLitAction;

	// Token: 0x0400117B RID: 4475
	public GameObject LightSource;

	// Token: 0x0400117C RID: 4476
	public float TouchRadius = 2f;

	// Token: 0x0400117D RID: 4477
	private Transform m_transform;

	// Token: 0x0400117E RID: 4478
	private bool m_isLit;

	// Token: 0x0400117F RID: 4479
	public float LitRadius = 5f;

	// Token: 0x04001180 RID: 4480
	public float UnlitRadius = 2f;

	// Token: 0x04001181 RID: 4481
	public BaseAnimator IgniteAnimator;
}
