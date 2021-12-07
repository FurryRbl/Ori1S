using System;
using UnityEngine;

// Token: 0x020001B0 RID: 432
public class TurbulenceSettings : ScriptableObject
{
	// Token: 0x170002DD RID: 733
	// (get) Token: 0x06001043 RID: 4163 RVA: 0x0004A1E0 File Offset: 0x000483E0
	public Texture2D TurbulenceTexture
	{
		get
		{
			if (this.CurveHasChanged() || this.m_turbulenceTexture == null)
			{
				this.m_turbulenceTexture = this.BakeAnimationCurve(this.TurbulenceCurve, TextureWrapMode.Repeat, 256, out this.Scale);
			}
			return this.m_turbulenceTexture;
		}
	}

	// Token: 0x170002DE RID: 734
	// (get) Token: 0x06001044 RID: 4164 RVA: 0x0004A230 File Offset: 0x00048430
	public float CurveDuration
	{
		get
		{
			if (this.m_didCalcDuration)
			{
				return this.m_duration;
			}
			Keyframe[] keys = this.TurbulenceCurve.keys;
			this.m_duration = keys[keys.Length - 1].time;
			this.m_didCalcDuration = true;
			return this.m_duration;
		}
	}

	// Token: 0x06001045 RID: 4165 RVA: 0x0004A280 File Offset: 0x00048480
	public Texture2D BakeAnimationCurve(AnimationCurve curve, TextureWrapMode wrapMode, int resolution, out float scale)
	{
		if (curve == null || curve.keys.Length == 0)
		{
			scale = 0f;
			return null;
		}
		scale = -1f;
		Keyframe[] keys = curve.keys;
		float time = keys[keys.Length - 1].time;
		Color[] array = new Color[resolution];
		Vector4[] array2 = new Vector4[resolution];
		for (int i = 0; i < resolution; i++)
		{
			float num = (float)i / (float)resolution * time;
			float num2 = curve.Evaluate(num);
			float num3 = curve.Evaluate(num + 1.333f);
			float num4 = curve.Evaluate(num + 2f);
			float num5 = curve.Evaluate(num + 3.5f);
			array2[i] = new Vector4(num2, num3, num4, num5);
			scale = Mathf.Max(new float[]
			{
				scale,
				Mathf.Abs(num2),
				Mathf.Abs(num3),
				Mathf.Abs(num4),
				Mathf.Abs(num5)
			});
		}
		Texture2D texture2D = new Texture2D(resolution, 1, TextureFormat.RGBA32, false);
		for (int j = 0; j < resolution; j++)
		{
			array[j] = new Color((array2[j].x / scale + 1f) / 2f, (array2[j].y / scale + 1f) / 2f, (array2[j].z / scale + 1f) / 2f, (array2[j].w / scale + 1f) / 2f);
		}
		texture2D.SetPixels(array);
		texture2D.wrapMode = wrapMode;
		texture2D.Apply();
		return texture2D;
	}

	// Token: 0x06001046 RID: 4166 RVA: 0x0004A448 File Offset: 0x00048648
	public bool CurveHasChanged()
	{
		if (Application.isPlaying)
		{
			return false;
		}
		Keyframe[] keys = this.TurbulenceCurve.keys;
		if (this.m_keys == null)
		{
			this.m_keys = keys;
			return true;
		}
		if (keys.Length != this.m_keys.Length)
		{
			this.m_keys = keys;
			return true;
		}
		for (int i = 0; i < keys.Length; i++)
		{
			Keyframe keyframe = this.m_keys[i];
			Keyframe keyframe2 = keys[i];
			if (keyframe.time != keyframe2.time || keyframe.inTangent != keyframe2.inTangent || keyframe.outTangent != keyframe2.outTangent || keyframe.value != keyframe2.value)
			{
				this.m_keys = keys;
				return true;
			}
		}
		return false;
	}

	// Token: 0x04000D79 RID: 3449
	public AnimationCurve TurbulenceCurve = AnimationCurve.Linear(0f, 0f, 1f, 0f);

	// Token: 0x04000D7A RID: 3450
	public float TurbulenceMagnitude = 1f;

	// Token: 0x04000D7B RID: 3451
	public float TurbulenceSpeed = 1f;

	// Token: 0x04000D7C RID: 3452
	public float TurbulenceValueOffset;

	// Token: 0x04000D7D RID: 3453
	public AnimationCurve TurbulenceSpeedOverTime = AnimationCurve.Linear(0f, 1f, 1f, 1f);

	// Token: 0x04000D7E RID: 3454
	public AnimationCurve TurbulenceMagnitudeOverTime = AnimationCurve.Linear(0f, 1f, 1f, 1f);

	// Token: 0x04000D7F RID: 3455
	[HideInInspector]
	[NonSerialized]
	public float Scale;

	// Token: 0x04000D80 RID: 3456
	[HideInInspector]
	[NonSerialized]
	public float TimeLineStr = 1f;

	// Token: 0x04000D81 RID: 3457
	[HideInInspector]
	[NonSerialized]
	public float TimeLineSpeed = 1f;

	// Token: 0x04000D82 RID: 3458
	private Keyframe[] m_keys;

	// Token: 0x04000D83 RID: 3459
	private Texture2D m_turbulenceTexture;

	// Token: 0x04000D84 RID: 3460
	private bool m_didCalcDuration;

	// Token: 0x04000D85 RID: 3461
	private float m_duration;
}
