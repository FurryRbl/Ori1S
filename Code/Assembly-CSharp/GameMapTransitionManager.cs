using System;
using Core;
using UnityEngine;

// Token: 0x0200014F RID: 335
public class GameMapTransitionManager : MonoBehaviour
{
	// Token: 0x17000296 RID: 662
	// (get) Token: 0x06000D96 RID: 3478 RVA: 0x0003F400 File Offset: 0x0003D600
	public bool IsTransitioning
	{
		get
		{
			return this.m_zoomTime != 0f && this.m_zoomTime < 1f;
		}
	}

	// Token: 0x17000297 RID: 663
	// (get) Token: 0x06000D97 RID: 3479 RVA: 0x0003F42D File Offset: 0x0003D62D
	public bool InWorldMapMode
	{
		get
		{
			return Mathf.Approximately(this.m_zoomTime, 0f);
		}
	}

	// Token: 0x17000298 RID: 664
	// (get) Token: 0x06000D98 RID: 3480 RVA: 0x0003F43F File Offset: 0x0003D63F
	public bool InAreaMapMode
	{
		get
		{
			return this.m_zoomTime >= 1f;
		}
	}

	// Token: 0x06000D99 RID: 3481 RVA: 0x0003F451 File Offset: 0x0003D651
	public void Awake()
	{
		GameMapTransitionManager.Instance = this;
	}

	// Token: 0x06000D9A RID: 3482 RVA: 0x0003F459 File Offset: 0x0003D659
	public void OnDestroy()
	{
		if (GameMapTransitionManager.Instance == this)
		{
			GameMapTransitionManager.Instance = null;
		}
	}

	// Token: 0x17000299 RID: 665
	// (get) Token: 0x06000D9B RID: 3483 RVA: 0x0003F471 File Offset: 0x0003D671
	public float ZoomTime
	{
		get
		{
			return this.m_zoomTime;
		}
	}

	// Token: 0x06000D9C RID: 3484 RVA: 0x0003F479 File Offset: 0x0003D679
	public void ZoomToWorldMap()
	{
		if (this.ZoomOutSound)
		{
			this.ZoomOutSound.Play();
		}
		if (this.InAreaMapZoomOutSound)
		{
			this.InAreaMapZoomOutSound.Stop();
		}
		this.GoToWorldMap();
	}

	// Token: 0x06000D9D RID: 3485 RVA: 0x0003F4B7 File Offset: 0x0003D6B7
	public void ZoomToAreaMap()
	{
		if (this.ZoomInSound)
		{
			this.ZoomInSound.Play();
		}
		this.GoToAreaMap();
	}

	// Token: 0x06000D9E RID: 3486 RVA: 0x0003F4DA File Offset: 0x0003D6DA
	public void Update()
	{
		this.m_mouseWheel += UnityEngine.Input.GetAxis("Mouse ScrollWheel");
	}

	// Token: 0x06000D9F RID: 3487 RVA: 0x0003F4F4 File Offset: 0x0003D6F4
	public void Advance()
	{
		if (!GameMapUI.Instance.ShowingObjective && !GameMapUI.Instance.RevealingMap)
		{
			bool flag = Core.Input.ZoomOut.Pressed;
			bool flag2 = Core.Input.ZoomIn.Pressed;
			float num = this.m_mouseWheel * 50f;
			this.m_mouseWheel = 0f;
			this.m_zoomSpeed = Mathf.Lerp(this.m_zoomSpeed, num, 0.5f);
			if (flag || flag2)
			{
				this.m_zoomSpeed = (float)(((!flag2) ? 0 : 1) - ((!flag) ? 0 : 1));
				this.m_zeroZoom = true;
			}
			else if (this.m_zeroZoom)
			{
				this.m_zoomSpeed = 0f;
				this.m_zeroZoom = false;
			}
			if (num > 0f)
			{
				flag2 = true;
			}
			else if (num < 0f)
			{
				flag = true;
			}
			if (flag)
			{
				if (this.m_areaMode && this.m_zoomTime <= 1f)
				{
					this.ZoomToWorldMap();
				}
			}
			else if (this.m_zoomSpeed >= 0.05f && this.InAreaMapZoomOutSound)
			{
				this.InAreaMapZoomOutSound.Stop();
			}
			if (flag2)
			{
				if (!this.m_areaMode)
				{
					this.ZoomToAreaMap();
				}
			}
			else if (this.m_zoomSpeed <= -0.05f && this.InAreaMapZoomInSound)
			{
				this.InAreaMapZoomInSound.Stop();
			}
			if (this.m_areaMode)
			{
				if (this.m_zoomTime >= 1f)
				{
					if (this.m_zoomSpeed < -0.05f)
					{
						if (this.InAreaMapZoomOutSound && !this.InAreaMapZoomOutSound.IsPlaying)
						{
							this.InAreaMapZoomOutSound.Play();
						}
						this.m_zoomTime += Time.deltaTime * this.m_zoomSpeed;
					}
					else if (this.m_zoomSpeed > 0.05f)
					{
						if (this.InAreaMapZoomInSound && !this.InAreaMapZoomInSound.IsPlaying)
						{
							this.InAreaMapZoomInSound.Play();
						}
						this.m_zoomTime += Time.deltaTime * this.m_zoomSpeed;
						this.m_zoomTime = Mathf.Clamp(this.m_zoomTime, 1f, 2f);
					}
				}
			}
			else if (Core.Input.ActionButtonA.OnPressed && !Core.Input.ActionButtonA.Used)
			{
				Core.Input.ActionButtonA.Used = true;
				this.ZoomToAreaMap();
			}
		}
		if (this.m_areaMode && this.m_zoomTime < 1f)
		{
			this.m_zoomTime += 1f / this.ZoomDuration * Time.deltaTime;
			this.m_zoomTime = Mathf.Clamp01(this.m_zoomTime);
			if (this.m_zoomTime == 1f)
			{
				WorldMapUI.Instance.Deactivate();
			}
		}
		else if (!this.m_areaMode)
		{
			this.m_zoomTime -= 1f / this.ZoomDuration * Time.deltaTime;
			this.m_zoomTime = Mathf.Clamp01(this.m_zoomTime);
			if (this.m_zoomTime == 0f)
			{
				AreaMapUI.Instance.Hide();
			}
		}
	}

	// Token: 0x06000DA0 RID: 3488 RVA: 0x0003F850 File Offset: 0x0003DA50
	public void GoToWorldMap()
	{
		WorldMapUI.Instance.Activate();
		this.m_areaMode = false;
		AreaMapUI.Instance.FadeOutAnimator.Initialize();
		AreaMapUI.Instance.FadeOutAnimator.AnimatorDriver.ContinueForward();
		WorldMapUI.Instance.CrossFade.Initialize();
		WorldMapUI.Instance.CrossFade.AnimatorDriver.ContinueForward();
	}

	// Token: 0x06000DA1 RID: 3489 RVA: 0x0003F8B4 File Offset: 0x0003DAB4
	public void GoToAreaMap()
	{
		AreaMapUI.Instance.ResetMaps();
		this.m_areaMode = true;
		AreaMapUI.Instance.Show();
		AreaMapUI.Instance.Init();
		AreaMapUI.Instance.FadeOutAnimator.Initialize();
		AreaMapUI.Instance.FadeOutAnimator.AnimatorDriver.ContinueBackwards();
		WorldMapUI.Instance.CrossFade.Initialize();
		WorldMapUI.Instance.CrossFade.AnimatorDriver.ContinueBackwards();
	}

	// Token: 0x06000DA2 RID: 3490 RVA: 0x0003F92C File Offset: 0x0003DB2C
	public void GoToAreaMapInstantly()
	{
		this.m_areaMode = true;
		this.m_zoomTime = 1f;
		WorldMapUI.Instance.Deactivate();
		WorldMapUI.Instance.CrossFade.Initialize();
		WorldMapUI.Instance.CrossFade.AnimatorDriver.GoToStart();
		WorldMapUI.Instance.CrossFade.AnimatorDriver.Pause();
		AreaMapUI.Instance.FadeOutAnimator.Initialize();
		AreaMapUI.Instance.FadeOutAnimator.AnimatorDriver.GoToStart();
		AreaMapUI.Instance.FadeOutAnimator.AnimatorDriver.Pause();
		AreaMapUI.Instance.Show();
		AreaMapUI.Instance.Init();
	}

	// Token: 0x06000DA3 RID: 3491 RVA: 0x0003F9D8 File Offset: 0x0003DBD8
	public void GoToWorldMapInstantly()
	{
		this.m_areaMode = false;
		this.m_zoomTime = 0f;
		AreaMapUI.Instance.Hide();
		WorldMapUI.Instance.Activate();
		WorldMapUI.Instance.CrossFade.Initialize();
		WorldMapUI.Instance.CrossFade.AnimatorDriver.GoToEnd();
		WorldMapUI.Instance.CrossFade.AnimatorDriver.Pause();
		AreaMapUI.Instance.FadeOutAnimator.Initialize();
		AreaMapUI.Instance.FadeOutAnimator.AnimatorDriver.GoToEnd();
		AreaMapUI.Instance.FadeOutAnimator.AnimatorDriver.Pause();
	}

	// Token: 0x04000B0E RID: 2830
	public static GameMapTransitionManager Instance;

	// Token: 0x04000B0F RID: 2831
	private float m_zoomTime = 1f;

	// Token: 0x04000B10 RID: 2832
	public SoundSource ZoomInSound;

	// Token: 0x04000B11 RID: 2833
	public SoundSource ZoomOutSound;

	// Token: 0x04000B12 RID: 2834
	public SoundSource InAreaMapZoomInSound;

	// Token: 0x04000B13 RID: 2835
	public SoundSource InAreaMapZoomOutSound;

	// Token: 0x04000B14 RID: 2836
	private bool m_areaMode = true;

	// Token: 0x04000B15 RID: 2837
	public float ZoomDuration = 1f;

	// Token: 0x04000B16 RID: 2838
	private float m_mouseWheelSmooth;

	// Token: 0x04000B17 RID: 2839
	private float m_zoomSpeed;

	// Token: 0x04000B18 RID: 2840
	private bool m_zeroZoom;

	// Token: 0x04000B19 RID: 2841
	private float m_mouseWheel;
}
