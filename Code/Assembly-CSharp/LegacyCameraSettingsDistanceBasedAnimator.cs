using System;
using Game;
using UnityEngine;

// Token: 0x0200039F RID: 927
public class LegacyCameraSettingsDistanceBasedAnimator : MonoBehaviour
{
	// Token: 0x060019FE RID: 6654 RVA: 0x0006FB78 File Offset: 0x0006DD78
	public Bounds GetOuterBounds()
	{
		return new Bounds(base.transform.position + (new Vector3(this.RightMargin, this.TopMargin) - new Vector3(this.LeftMargin, this.BottomMargin)) / 2f, base.transform.lossyScale + new Vector3(this.LeftMargin + this.RightMargin, this.TopMargin + this.BottomMargin));
	}

	// Token: 0x060019FF RID: 6655 RVA: 0x0006FBFC File Offset: 0x0006DDFC
	public Bounds GetInnerBounds()
	{
		return new Bounds(base.transform.position, base.transform.localScale);
	}

	// Token: 0x06001A00 RID: 6656 RVA: 0x0006FC24 File Offset: 0x0006DE24
	public float NormalizedMarginPenetration(Vector3 worldPosition)
	{
		Vector3 position = base.transform.position;
		Vector3 vector = worldPosition - position;
		Vector3 vector2 = base.transform.localScale * 0.5f;
		float a = 1f;
		float b = 1f;
		if (vector.x < -vector2.x)
		{
			a = Mathf.Clamp01(Mathf.InverseLerp(-vector2.x - this.LeftMargin, -vector2.x, vector.x));
		}
		if (vector.x > vector2.x)
		{
			a = Mathf.Clamp01(Mathf.InverseLerp(vector2.x + this.RightMargin, vector2.x, vector.x));
		}
		if (vector.y < -vector2.y)
		{
			b = Mathf.Clamp01(Mathf.InverseLerp(-vector2.y - this.BottomMargin, -vector2.y, vector.y));
		}
		if (vector.y > vector2.y)
		{
			b = Mathf.Clamp01(Mathf.InverseLerp(vector2.y + this.TopMargin, vector2.y, vector.y));
		}
		return Mathf.Min(a, b);
	}

	// Token: 0x06001A01 RID: 6657 RVA: 0x0006FD60 File Offset: 0x0006DF60
	public void FixedUpdate()
	{
		float num = this.NormalizedMarginPenetration(UI.Cameras.Current.TargetHelperPosition);
		if (!this.m_shouldAnimate && this.m_previousValue == num)
		{
			return;
		}
		this.AnimateIt(num);
		this.m_previousValue = num;
		this.m_shouldAnimate = false;
	}

	// Token: 0x06001A02 RID: 6658 RVA: 0x0006FDAB File Offset: 0x0006DFAB
	public void OnEnable()
	{
		this.m_shouldAnimate = true;
	}

	// Token: 0x06001A03 RID: 6659 RVA: 0x0006FDB4 File Offset: 0x0006DFB4
	public void OnDisable()
	{
	}

	// Token: 0x06001A04 RID: 6660 RVA: 0x0006FDB8 File Offset: 0x0006DFB8
	public void Init()
	{
		this.m_currentSettings = ScriptableObject.CreateInstance<CameraSettingsAsset>();
		this.m_originalCameraSettings = ScriptableObject.CreateInstance<CameraSettingsAsset>();
		UI.Cameras.Current.CameraPostProcessing.SaveCameraSettings(this.m_originalCameraSettings);
		UI.Cameras.Current.CameraPostProcessing.SaveCameraSettings(this.m_currentSettings);
		if (this.m_originalCameraSettings.Noise.GrainTexture != this.CameraSettings.Noise.GrainTexture)
		{
			return;
		}
		if (this.m_originalCameraSettings.BloomAndFlaresSettings.BlurIterations != this.CameraSettings.BloomAndFlaresSettings.BlurIterations)
		{
			return;
		}
		if (this.m_originalCameraSettings.UseBloomAndLensFlares != this.CameraSettings.UseBloomAndLensFlares)
		{
			return;
		}
		if (this.m_originalCameraSettings.UseSeinPostProcessing != this.CameraSettings.UseSeinPostProcessing)
		{
			return;
		}
	}

	// Token: 0x06001A05 RID: 6661 RVA: 0x0006FE8E File Offset: 0x0006E08E
	private void AnimateIt(float value)
	{
	}

	// Token: 0x04001663 RID: 5731
	public CameraSettingsAsset CameraSettings;

	// Token: 0x04001664 RID: 5732
	private CameraSettingsAsset m_currentSettings;

	// Token: 0x04001665 RID: 5733
	private CameraSettingsAsset m_originalCameraSettings;

	// Token: 0x04001666 RID: 5734
	public FogGradientController FogGradientController;

	// Token: 0x04001667 RID: 5735
	public float LeftMargin;

	// Token: 0x04001668 RID: 5736
	public float RightMargin;

	// Token: 0x04001669 RID: 5737
	public float TopMargin;

	// Token: 0x0400166A RID: 5738
	public float BottomMargin;

	// Token: 0x0400166B RID: 5739
	private Color m_outerColor;

	// Token: 0x0400166C RID: 5740
	private Color m_innerColor;

	// Token: 0x0400166D RID: 5741
	private float m_previousValue;

	// Token: 0x0400166E RID: 5742
	private bool m_shouldAnimate;
}
