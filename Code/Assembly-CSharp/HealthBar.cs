using System;
using System.Collections.Generic;
using Game;
using UnityEngine;

// Token: 0x0200085D RID: 2141
public class HealthBar : MonoBehaviour
{
	// Token: 0x170007C3 RID: 1987
	// (get) Token: 0x06003086 RID: 12422 RVA: 0x000CE706 File Offset: 0x000CC906
	public float Value
	{
		get
		{
			return this.ValueProvider.GetFloatValue();
		}
	}

	// Token: 0x06003087 RID: 12423 RVA: 0x000CE714 File Offset: 0x000CC914
	public void Awake()
	{
		this.m_transform = base.transform;
		Game.Checkpoint.Events.OnPostRestore.Add(new Action(this.OnRestoreCheckpoint));
	}

	// Token: 0x06003088 RID: 12424 RVA: 0x000CE743 File Offset: 0x000CC943
	public void OnDestroy()
	{
		Game.Checkpoint.Events.OnPostRestore.Remove(new Action(this.OnRestoreCheckpoint));
	}

	// Token: 0x06003089 RID: 12425 RVA: 0x000CE75C File Offset: 0x000CC95C
	public void OnRestoreCheckpoint()
	{
		this.m_minValue = (this.m_maxValue = this.Value);
		this.OnChangeAnimator.AnimatorDriver.SetForward();
		this.OnChangeAnimator.AnimatorDriver.Stop();
		this.UpdateVisuals();
	}

	// Token: 0x0600308A RID: 12426 RVA: 0x000CE7A4 File Offset: 0x000CC9A4
	public void Start()
	{
		this.m_minValue = (this.m_maxValue = this.Value);
		this.UpdateVisuals();
		this.m_lastPosition = this.m_transform.position;
	}

	// Token: 0x0600308B RID: 12427 RVA: 0x000CE7E0 File Offset: 0x000CC9E0
	public void FixedUpdate()
	{
		this.m_transform.rotation = Quaternion.identity;
		float value = this.Value;
		if (this.m_minValue > value)
		{
			this.OnChangeAnimator.AnimatorDriver.SetBackwards();
			this.OnChangeAnimator.AnimatorDriver.Restart();
		}
		if (this.m_minValue != value)
		{
			this.m_minValue = value;
			this.m_maxValue = Mathf.Max(this.m_minValue, this.m_maxValue);
			this.UpdateVisuals();
		}
		if (this.m_minValue < this.m_maxValue)
		{
			this.m_maxValue = Mathf.Max(this.m_minValue, this.m_maxValue - this.ValueChangeRate * Time.deltaTime);
			this.UpdateVisuals();
		}
		if (Vector3.Distance(this.m_transform.position, this.m_lastPosition) > 1f)
		{
			this.OnChangeAnimator.AnimatorDriver.GoToStart();
		}
		this.m_lastPosition = this.m_transform.position;
	}

	// Token: 0x0600308C RID: 12428 RVA: 0x000CE8DC File Offset: 0x000CCADC
	public void UpdateVisuals()
	{
		for (int i = 0; i < this.Gradients.Count; i++)
		{
			LegacyMaterialColorGradientAnimator legacyMaterialColorGradientAnimator = this.Gradients[i];
			legacyMaterialColorGradientAnimator.Sample(this.m_minValue);
		}
		this.MinAnimator.SampleValue(this.m_minValue, true);
		this.MaxAnimator.SampleValue(this.m_maxValue, true);
	}

	// Token: 0x0600308D RID: 12429 RVA: 0x000CE942 File Offset: 0x000CCB42
	public void LateUpdate()
	{
		this.m_transform.rotation = Quaternion.identity;
	}

	// Token: 0x04002BD8 RID: 11224
	public FloatValueProvider ValueProvider;

	// Token: 0x04002BD9 RID: 11225
	public BaseAnimator OnChangeAnimator;

	// Token: 0x04002BDA RID: 11226
	public BaseAnimator MaxAnimator;

	// Token: 0x04002BDB RID: 11227
	public BaseAnimator MinAnimator;

	// Token: 0x04002BDC RID: 11228
	public List<LegacyMaterialColorGradientAnimator> Gradients;

	// Token: 0x04002BDD RID: 11229
	public float ValueChangeRate;

	// Token: 0x04002BDE RID: 11230
	private float m_minValue;

	// Token: 0x04002BDF RID: 11231
	private float m_maxValue;

	// Token: 0x04002BE0 RID: 11232
	private Transform m_transform;

	// Token: 0x04002BE1 RID: 11233
	private Vector3 m_lastPosition;
}
