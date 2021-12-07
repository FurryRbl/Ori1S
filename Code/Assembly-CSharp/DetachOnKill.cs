using System;
using UnityEngine;

// Token: 0x0200045E RID: 1118
public class DetachOnKill : MonoBehaviour, IKillReciever, IPooled
{
	// Token: 0x06001ECF RID: 7887 RVA: 0x00087ACC File Offset: 0x00085CCC
	private void Awake()
	{
		this.m_emitter = base.GetComponent<ParticleEmitter>();
		this.m_particleSystem = base.GetComponent<ParticleSystem>();
		if (this.m_emitter)
		{
			this.m_minEmit = this.m_emitter.minEmission;
			this.m_maxEmit = this.m_emitter.maxEmission;
		}
		if (this.m_particleSystem)
		{
			this.m_emit = this.m_particleSystem.emissionRate;
		}
	}

	// Token: 0x06001ED0 RID: 7888 RVA: 0x00087B44 File Offset: 0x00085D44
	public void OnPoolSpawned()
	{
		this.m_killed = false;
		this.m_countDown = 0f;
		if (this.m_emitter)
		{
			this.m_emitter.minEmission = this.m_minEmit;
			this.m_emitter.maxEmission = this.m_maxEmit;
		}
		if (this.m_particleSystem)
		{
			this.m_particleSystem.emissionRate = this.m_emit;
		}
	}

	// Token: 0x06001ED1 RID: 7889 RVA: 0x00087BB8 File Offset: 0x00085DB8
	public static GameObject GetDetachOnKillObjectsParent()
	{
		if (DetachOnKill.s_detachOnKillObjectsParent == null)
		{
			DetachOnKill.s_detachOnKillObjectsParent = new GameObject("detachOnKillObjectsParent");
			Utility.DontAssociateWithAnyScene(DetachOnKill.s_detachOnKillObjectsParent);
		}
		return DetachOnKill.s_detachOnKillObjectsParent;
	}

	// Token: 0x06001ED2 RID: 7890 RVA: 0x00087BF4 File Offset: 0x00085DF4
	private void Update()
	{
		if (this.m_killed)
		{
			this.m_countDown -= Time.deltaTime;
			if (this.m_countDown < 0f)
			{
				InstantiateUtility.Destroy(base.gameObject);
			}
		}
	}

	// Token: 0x06001ED3 RID: 7891 RVA: 0x00087C3C File Offset: 0x00085E3C
	public void OnKill()
	{
		this.m_countDown = ((!this.LimitLifetime) ? 2f : this.LifetimeLimit);
		this.m_killed = true;
		base.transform.parent = DetachOnKill.GetDetachOnKillObjectsParent().transform;
		if (this.StopParticleEmission)
		{
			if (this.m_emitter)
			{
				this.m_emitter.minEmission = 0f;
				this.m_emitter.maxEmission = 0f;
			}
			if (this.m_particleSystem)
			{
				this.m_particleSystem.emissionRate = 0f;
			}
		}
	}

	// Token: 0x04001AA0 RID: 6816
	public bool LimitLifetime;

	// Token: 0x04001AA1 RID: 6817
	public bool StopParticleEmission;

	// Token: 0x04001AA2 RID: 6818
	public float LifetimeLimit = 1f;

	// Token: 0x04001AA3 RID: 6819
	private static GameObject s_detachOnKillObjectsParent;

	// Token: 0x04001AA4 RID: 6820
	private float m_minEmit;

	// Token: 0x04001AA5 RID: 6821
	private float m_maxEmit;

	// Token: 0x04001AA6 RID: 6822
	private float m_emit;

	// Token: 0x04001AA7 RID: 6823
	private ParticleEmitter m_emitter;

	// Token: 0x04001AA8 RID: 6824
	private ParticleSystem m_particleSystem;

	// Token: 0x04001AA9 RID: 6825
	private bool m_killed;

	// Token: 0x04001AAA RID: 6826
	private float m_countDown;
}
