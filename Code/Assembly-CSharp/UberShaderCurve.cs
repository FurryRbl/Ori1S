using System;
using UnityEngine;

// Token: 0x020007D5 RID: 2005
[Serializable]
public class UberShaderCurve : UberShaderProperty
{
	// Token: 0x06002DFF RID: 11775 RVA: 0x000C3DD1 File Offset: 0x000C1FD1
	public UberShaderCurve()
	{
		this.m_curve = AnimationCurve.Linear(0f, 1f, 1f, 1f);
	}

	// Token: 0x06002E00 RID: 11776 RVA: 0x000C3E0E File Offset: 0x000C200E
	public UberShaderCurve(float value)
	{
		this.m_curve = AnimationCurve.Linear(0f, value, 1f, value);
	}

	// Token: 0x17000761 RID: 1889
	// (get) Token: 0x06002E01 RID: 11777 RVA: 0x000C3E43 File Offset: 0x000C2043
	public AnimationCurve Curve
	{
		get
		{
			return this.m_curve;
		}
	}

	// Token: 0x17000762 RID: 1890
	// (get) Token: 0x06002E02 RID: 11778 RVA: 0x000C3E4B File Offset: 0x000C204B
	public float MaxValue
	{
		get
		{
			return this.GetMaxCurveValue() * this.CurveScale;
		}
	}

	// Token: 0x17000763 RID: 1891
	// (get) Token: 0x06002E03 RID: 11779 RVA: 0x000C3E5A File Offset: 0x000C205A
	public float MeanValue
	{
		get
		{
			return this.GetMeanCurveValue() * this.CurveScale;
		}
	}

	// Token: 0x17000764 RID: 1892
	// (get) Token: 0x06002E04 RID: 11780 RVA: 0x000C3E6C File Offset: 0x000C206C
	public bool IsSimple
	{
		get
		{
			Keyframe[] keys = this.Curve.keys;
			float num = 0f;
			float value = keys[0].value;
			foreach (Keyframe keyframe in keys)
			{
				num = Mathf.Max(num, Mathf.Abs(value - keyframe.value));
			}
			return num * this.CurveScale < 0.1f;
		}
	}

	// Token: 0x06002E05 RID: 11781 RVA: 0x000C3EE8 File Offset: 0x000C20E8
	private float GetMeanCurveValue()
	{
		float time = this.Curve.keys[this.Curve.length - 1].time;
		float num = 0f;
		for (int i = 0; i < 16; i++)
		{
			float time2 = (float)i / 16f * time;
			num += this.Curve.Evaluate(time2);
		}
		return num / 16f;
	}

	// Token: 0x06002E06 RID: 11782 RVA: 0x000C3F54 File Offset: 0x000C2154
	private float GetMaxCurveValue()
	{
		float time = this.Curve.keys[this.Curve.length - 1].time;
		float num = -1f;
		for (int i = 0; i < 16; i++)
		{
			float time2 = (float)i / 16f * time;
			float f = this.Curve.Evaluate(time2);
			num = Mathf.Max(num, Mathf.Abs(f));
		}
		return num;
	}

	// Token: 0x06002E07 RID: 11783 RVA: 0x000C3FC8 File Offset: 0x000C21C8
	public override void BindProperties()
	{
		if (base.BindMaterial == null)
		{
			return;
		}
		this.CreateTexture();
		base.BindTexture(this.MainBindId, this.m_texture);
		base.BindVector(this.m_settingsId, this.GetSettings());
	}

	// Token: 0x06002E08 RID: 11784 RVA: 0x000C4014 File Offset: 0x000C2214
	private void CreateTexture()
	{
		if (this.m_texture != null)
		{
			UnityEngine.Object.DestroyImmediate(this.m_texture);
		}
		this.m_texture = UberShaderCurveBake.BakeAnimationCurve(this.m_curve, this.WrapMode, 64, out this.m_curveScale, out this.m_curveDuration);
	}

	// Token: 0x06002E09 RID: 11785 RVA: 0x000C4062 File Offset: 0x000C2262
	private Vector4 GetSettings()
	{
		return new Vector4(this.m_curveScale * this.CurveScale, this.m_curveDuration / this.TimeScale, this.TimeOffset);
	}

	// Token: 0x06002E0A RID: 11786 RVA: 0x000C408C File Offset: 0x000C228C
	public override void Set(string bindName, UberShaderBlock attachedBlock)
	{
		base.Set(bindName, attachedBlock);
		this.m_settingsId = Shader.PropertyToID(bindName + "_AnimationSettings");
		this.BindProperties();
	}

	// Token: 0x04002980 RID: 10624
	private const int cRes = 64;

	// Token: 0x04002981 RID: 10625
	[SerializeField]
	private AnimationCurve m_curve;

	// Token: 0x04002982 RID: 10626
	public float TimeScale = 1f;

	// Token: 0x04002983 RID: 10627
	public float CurveScale = 1f;

	// Token: 0x04002984 RID: 10628
	public float TimeOffset;

	// Token: 0x04002985 RID: 10629
	public TextureWrapMode WrapMode;

	// Token: 0x04002986 RID: 10630
	private float m_curveScale;

	// Token: 0x04002987 RID: 10631
	private float m_curveDuration;

	// Token: 0x04002988 RID: 10632
	private Texture2D m_texture;

	// Token: 0x04002989 RID: 10633
	private int m_settingsId;
}
