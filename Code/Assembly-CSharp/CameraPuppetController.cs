using System;
using Game;
using UnityEngine;

// Token: 0x0200019E RID: 414
[ExecuteInEditMode]
public class CameraPuppetController : MonoBehaviour
{
	// Token: 0x06000FEF RID: 4079 RVA: 0x00048EB4 File Offset: 0x000470B4
	public void Awake()
	{
		this.m_transform = base.transform;
		if (Application.isPlaying)
		{
			Events.Scheduler.OnGameReset.Add(new Action(this.OnGameReset));
		}
		this.m_cameraController = base.GetComponent<CameraController>();
	}

	// Token: 0x06000FF0 RID: 4080 RVA: 0x00048EFE File Offset: 0x000470FE
	public void OnDestroy()
	{
		if (Application.isPlaying)
		{
			Events.Scheduler.OnGameReset.Remove(new Action(this.OnGameReset));
		}
	}

	// Token: 0x06000FF1 RID: 4081 RVA: 0x00048F25 File Offset: 0x00047125
	public void OnGameReset()
	{
		this.SetTween(0f);
		this.ClearWideScreenAdjustments();
		this.CinematicPuppet = null;
		this.UpdatePuppet();
	}

	// Token: 0x06000FF2 RID: 4082 RVA: 0x00048F45 File Offset: 0x00047145
	public void Reset()
	{
		this.SetTween(0f);
		this.ClearWideScreenAdjustments();
	}

	// Token: 0x06000FF3 RID: 4083 RVA: 0x00048F58 File Offset: 0x00047158
	public void SetTween(float amount)
	{
		this.Tween = amount;
	}

	// Token: 0x06000FF4 RID: 4084 RVA: 0x00048F61 File Offset: 0x00047161
	public void SetWideScreenHorizontalPanStrength(float amount)
	{
		this.WideScreenHorizontalPanStrength = amount;
	}

	// Token: 0x06000FF5 RID: 4085 RVA: 0x00048F6A File Offset: 0x0004716A
	public void SetWideScreenVerticalPanStrength(float amount)
	{
		this.WideScreenVerticalPanStrength = amount;
	}

	// Token: 0x06000FF6 RID: 4086 RVA: 0x00048F73 File Offset: 0x00047173
	public void SetWideScreenZoomStrength(float amount)
	{
		this.WideScreenZoomStrength = amount;
	}

	// Token: 0x06000FF7 RID: 4087 RVA: 0x00048F7C File Offset: 0x0004717C
	public void ClearWideScreenAdjustments()
	{
		this.SetWideScreenZoomStrength(0f);
		this.SetWideScreenHorizontalPanStrength(0f);
		this.SetWideScreenVerticalPanStrength(0f);
	}

	// Token: 0x06000FF8 RID: 4088 RVA: 0x00048F9F File Offset: 0x0004719F
	public void SetCinematicPuppet(Transform cinematicPuppet)
	{
		this.CinematicPuppet = cinematicPuppet;
	}

	// Token: 0x06000FF9 RID: 4089 RVA: 0x00048FA8 File Offset: 0x000471A8
	public void UpdatePuppet()
	{
		if (this.CinematicPuppet)
		{
			this.m_transform.position = Vector3.Lerp(this.GameplayPuppet.position, this.CinematicPuppet.position, this.Tween);
			this.m_transform.rotation = Quaternion.Lerp(this.GameplayPuppet.rotation, this.CinematicPuppet.rotation, this.Tween);
		}
		else
		{
			this.m_transform.position = this.GameplayPuppet.position;
			this.m_transform.rotation = this.GameplayPuppet.rotation;
		}
		float num = 60f;
		float num2 = -this.m_transform.position.z;
		float num3 = 2f * num2 * Mathf.Tan(num * 0.5f * 0.017453292f);
		float aspect = this.m_cameraController.Camera.aspect;
		float num4 = 1.7777778f;
		float num5 = Mathf.Lerp(1f, num4 / aspect, this.WideScreenZoomStrength);
		float num6 = 1f / Mathf.Tan(0.5235988f);
		float b = Mathf.Atan(num5 / num6) * 57.29578f * 2f;
		this.m_cameraController.FieldOfView = Mathf.Lerp(60f, b, this.Tween);
		float num7 = aspect * num3;
		float num8 = 1.7777778f * num3;
		float d = num7 - num8;
		this.m_transform.position += Vector3.left * d * 0.5f * this.WideScreenHorizontalPanStrength * this.Tween;
		this.m_transform.position += Vector3.down * d * 0.5f * this.WideScreenVerticalPanStrength * this.Tween;
	}

	// Token: 0x04000D16 RID: 3350
	private CameraController m_cameraController;

	// Token: 0x04000D17 RID: 3351
	public Transform GameplayPuppet;

	// Token: 0x04000D18 RID: 3352
	public Transform CinematicPuppet;

	// Token: 0x04000D19 RID: 3353
	public float Tween;

	// Token: 0x04000D1A RID: 3354
	public float WideScreenHorizontalPanStrength;

	// Token: 0x04000D1B RID: 3355
	public float WideScreenVerticalPanStrength;

	// Token: 0x04000D1C RID: 3356
	public float WideScreenZoomStrength;

	// Token: 0x04000D1D RID: 3357
	private Transform m_transform;
}
