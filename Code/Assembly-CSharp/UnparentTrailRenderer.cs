using System;
using System.Collections;
using UnityEngine;

// Token: 0x020009A9 RID: 2473
public class UnparentTrailRenderer : MonoBehaviour, IPooled
{
	// Token: 0x060035DA RID: 13786 RVA: 0x000E220C File Offset: 0x000E040C
	private void Awake()
	{
		this.m_targetTransform = base.transform.parent;
		this.m_localPosition = base.transform.localPosition;
		Renderer component = base.GetComponent<Renderer>();
		component.material = component.material;
		this.m_trailRenderer = base.GetComponentInChildren<TrailRenderer>();
	}

	// Token: 0x060035DB RID: 13787 RVA: 0x000E225C File Offset: 0x000E045C
	public void OnPoolSpawned()
	{
		this.m_localPosition = Vector3.zero;
		this.m_time = 0f;
		this.m_didDestroy = false;
		if (this.m_trailRenderer)
		{
			base.StartCoroutine(UnparentTrailRenderer.ResetTrail(this.m_trailRenderer));
		}
	}

	// Token: 0x060035DC RID: 13788 RVA: 0x000E22A8 File Offset: 0x000E04A8
	private static IEnumerator ResetTrail(TrailRenderer trail)
	{
		float trailTime = trail.time;
		trail.time = 0f;
		yield return 0;
		trail.time = trailTime;
		yield break;
	}

	// Token: 0x060035DD RID: 13789 RVA: 0x000E22CA File Offset: 0x000E04CA
	private void OnDestroy()
	{
		UnityEngine.Object.Destroy(base.GetComponent<Renderer>().material);
	}

	// Token: 0x060035DE RID: 13790 RVA: 0x000E22DC File Offset: 0x000E04DC
	private void Start()
	{
		base.transform.parent = null;
		Utility.DontAssociateWithAnyScene(base.gameObject);
		ParticleSystem component = base.GetComponent<ParticleSystem>();
		if (component != null)
		{
			component.Clear();
		}
	}

	// Token: 0x060035DF RID: 13791 RVA: 0x000E231C File Offset: 0x000E051C
	private void Update()
	{
		if (this.m_time > this.DestroyDelayAfterTarget)
		{
			InstantiateUtility.Destroy(base.gameObject);
		}
		if (!InstantiateUtility.IsDestroyed(this.m_targetTransform))
		{
			base.transform.position = this.m_targetTransform.position + this.m_localPosition;
		}
		else
		{
			if (!this.m_didDestroy)
			{
				if (this.Fade != null)
				{
					this.Fade.AnimatorDriver.Restart();
				}
				this.m_didDestroy = true;
			}
			this.m_time += Time.deltaTime;
		}
	}

	// Token: 0x04003075 RID: 12405
	private Transform m_targetTransform;

	// Token: 0x04003076 RID: 12406
	private Vector3 m_localPosition;

	// Token: 0x04003077 RID: 12407
	public float DestroyDelayAfterTarget;

	// Token: 0x04003078 RID: 12408
	public TransparencyAnimator Fade;

	// Token: 0x04003079 RID: 12409
	private float m_time;

	// Token: 0x0400307A RID: 12410
	private bool m_didDestroy;

	// Token: 0x0400307B RID: 12411
	private TrailRenderer m_trailRenderer;
}
