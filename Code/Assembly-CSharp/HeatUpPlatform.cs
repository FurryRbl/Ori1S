using System;
using UnityEngine;

// Token: 0x020002EE RID: 750
public class HeatUpPlatform : SaveSerialize
{
	// Token: 0x060016A2 RID: 5794 RVA: 0x00062F64 File Offset: 0x00061164
	public override void Awake()
	{
		base.Awake();
		this.m_transform = base.transform;
	}

	// Token: 0x060016A3 RID: 5795 RVA: 0x00062F78 File Offset: 0x00061178
	public void Start()
	{
		this.ChangeState(HeatUpPlatform.State.Cold);
	}

	// Token: 0x060016A4 RID: 5796 RVA: 0x00062F81 File Offset: 0x00061181
	public void OnCollisionEnter(Collision collision)
	{
		this.OnCollision(collision);
	}

	// Token: 0x060016A5 RID: 5797 RVA: 0x00062F8A File Offset: 0x0006118A
	public void OnCollisionStay(Collision collision)
	{
		this.OnCollision(collision);
	}

	// Token: 0x060016A6 RID: 5798 RVA: 0x00062F94 File Offset: 0x00061194
	public void OnCollision(Collision collision)
	{
		if (collision.gameObject.CompareTag("Player"))
		{
			this.m_beingTriggered = true;
		}
		else if (!this.m_beingTriggered)
		{
			return;
		}
		if (this.CurrentState == HeatUpPlatform.State.Hot)
		{
			SeinDamageReciever componentInChildren = collision.gameObject.GetComponentInChildren<SeinDamageReciever>();
			if (componentInChildren.Sein.Controller.IsBashing)
			{
				return;
			}
			if (componentInChildren)
			{
				Vector2 vector = componentInChildren.gameObject.transform.position - this.m_transform.position;
				Damage damage = new Damage(this.Damage, vector.normalized, this.m_transform.position, DamageType.Lava, base.gameObject);
				damage.DealToComponents(componentInChildren.gameObject);
			}
		}
	}

	// Token: 0x060016A7 RID: 5799 RVA: 0x0006305E File Offset: 0x0006125E
	public void FixedUpdate()
	{
		this.UpdateState();
		this.m_beingTriggered = false;
	}

	// Token: 0x060016A8 RID: 5800 RVA: 0x0006306D File Offset: 0x0006126D
	public void Trigger()
	{
		this.m_beingTriggered = true;
	}

	// Token: 0x060016A9 RID: 5801 RVA: 0x00063076 File Offset: 0x00061276
	public void ChangeState(HeatUpPlatform.State state)
	{
		this.CurrentState = state;
		this.m_stateCurrentTime = 0f;
	}

	// Token: 0x060016AA RID: 5802 RVA: 0x0006308C File Offset: 0x0006128C
	public void UpdateState()
	{
		switch (this.CurrentState)
		{
		case HeatUpPlatform.State.Cold:
			this.m_heat = 0f;
			if (this.m_beingTriggered && this.Activated)
			{
				this.ChangeState(HeatUpPlatform.State.Heating);
				if (this.ActivateSoundSource)
				{
					this.ActivateSoundSource.Play();
				}
			}
			break;
		case HeatUpPlatform.State.Heating:
			this.m_heat += Time.deltaTime / this.HeatingDuration;
			if (this.m_heat >= 1f)
			{
				this.m_heat = 1f;
				this.ChangeState(HeatUpPlatform.State.Hot);
			}
			if (!this.m_beingTriggered || !this.Activated)
			{
				this.ChangeState(HeatUpPlatform.State.Cooling);
				if (this.ActivateSoundSource)
				{
					this.ActivateSoundSource.StopAndFadeOut(0.2f);
				}
			}
			break;
		case HeatUpPlatform.State.Hot:
			this.m_heat = 1f;
			if (!this.m_beingTriggered || !this.Activated)
			{
				this.ChangeState(HeatUpPlatform.State.Cooling);
				if (this.DeacivateSoundSource)
				{
					this.DeacivateSoundSource.Play();
				}
			}
			break;
		case HeatUpPlatform.State.Cooling:
			this.m_heat -= Time.deltaTime / this.CoolingDuration;
			if (this.m_heat <= 0f)
			{
				this.m_heat = 0f;
				this.ChangeState(HeatUpPlatform.State.Cold);
			}
			if (this.m_beingTriggered && this.Activated)
			{
				this.ChangeState(HeatUpPlatform.State.Heating);
				if (this.DeacivateSoundSource)
				{
					this.DeacivateSoundSource.StopAndFadeOut(0.2f);
				}
				if (this.ActivateSoundSource)
				{
					this.ActivateSoundSource.Play();
				}
			}
			break;
		}
		this.m_stateCurrentTime += Time.deltaTime;
		this.UpdateMaterial();
	}

	// Token: 0x060016AB RID: 5803 RVA: 0x00063278 File Offset: 0x00061478
	public void UpdateMaterial()
	{
		Renderer renderer = this.Target;
		if (renderer == null)
		{
			renderer = base.GetComponent<Renderer>();
		}
		if (renderer)
		{
			renderer.sharedMaterial.SetColor(ShaderProperties.Color, Color.Lerp(this.ColdColor, this.HotColor, this.HeatCurve.Evaluate(this.m_heat)));
		}
	}

	// Token: 0x060016AC RID: 5804 RVA: 0x000632DC File Offset: 0x000614DC
	public override void Serialize(Archive ar)
	{
		ar.Serialize(ref this.Activated);
	}

	// Token: 0x0400137D RID: 4989
	private Transform m_transform;

	// Token: 0x0400137E RID: 4990
	public SoundSource ActivateSoundSource;

	// Token: 0x0400137F RID: 4991
	public SoundSource DeacivateSoundSource;

	// Token: 0x04001380 RID: 4992
	public bool Activated = true;

	// Token: 0x04001381 RID: 4993
	private bool m_beingTriggered;

	// Token: 0x04001382 RID: 4994
	public HeatUpPlatform.State CurrentState;

	// Token: 0x04001383 RID: 4995
	private float m_stateCurrentTime;

	// Token: 0x04001384 RID: 4996
	private float m_heat;

	// Token: 0x04001385 RID: 4997
	public Renderer Target;

	// Token: 0x04001386 RID: 4998
	public Color ColdColor;

	// Token: 0x04001387 RID: 4999
	public Color HotColor;

	// Token: 0x04001388 RID: 5000
	public AnimationCurve HeatCurve;

	// Token: 0x04001389 RID: 5001
	public float Damage;

	// Token: 0x0400138A RID: 5002
	public float HeatingDuration;

	// Token: 0x0400138B RID: 5003
	public float CoolingDuration;

	// Token: 0x020008F9 RID: 2297
	public enum State
	{
		// Token: 0x04002E1E RID: 11806
		Cold,
		// Token: 0x04002E1F RID: 11807
		Heating,
		// Token: 0x04002E20 RID: 11808
		Hot,
		// Token: 0x04002E21 RID: 11809
		Cooling
	}
}
