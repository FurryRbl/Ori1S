using System;
using Core;
using UnityEngine;

// Token: 0x02000105 RID: 261
public abstract class CleverValueSlider : CleverMenuItemGroupBase
{
	// Token: 0x17000225 RID: 549
	// (get) Token: 0x06000A1C RID: 2588
	// (set) Token: 0x06000A1D RID: 2589
	public abstract float Value { get; set; }

	// Token: 0x17000226 RID: 550
	// (get) Token: 0x06000A1E RID: 2590 RVA: 0x0002BDBB File Offset: 0x00029FBB
	// (set) Token: 0x06000A1F RID: 2591 RVA: 0x0002BDD4 File Offset: 0x00029FD4
	public float NormalizedValue
	{
		get
		{
			return Mathf.InverseLerp(this.MinValue, this.MaxValue, this.Value);
		}
		set
		{
			this.Value = Mathf.Lerp(this.MinValue, this.MaxValue, value);
		}
	}

	// Token: 0x17000227 RID: 551
	// (get) Token: 0x06000A20 RID: 2592 RVA: 0x0002BDEE File Offset: 0x00029FEE
	// (set) Token: 0x06000A21 RID: 2593 RVA: 0x0002BDF6 File Offset: 0x00029FF6
	public override bool IsActive
	{
		get
		{
			return this.m_isActive;
		}
		set
		{
			this.m_isActive = value;
			if (!value)
			{
				this.m_isDragged = false;
			}
		}
	}

	// Token: 0x17000228 RID: 552
	// (get) Token: 0x06000A22 RID: 2594 RVA: 0x0002BE0C File Offset: 0x0002A00C
	// (set) Token: 0x06000A23 RID: 2595 RVA: 0x0002BE14 File Offset: 0x0002A014
	public override bool IsVisible { get; set; }

	// Token: 0x17000229 RID: 553
	// (get) Token: 0x06000A24 RID: 2596 RVA: 0x0002BE1D File Offset: 0x0002A01D
	// (set) Token: 0x06000A25 RID: 2597 RVA: 0x0002BE28 File Offset: 0x0002A028
	public override bool IsHighlightVisible
	{
		get
		{
			return this.m_isHighlightVisible;
		}
		set
		{
			this.m_isHighlightVisible = value;
			if (this.HighlightAnimator)
			{
				if (value)
				{
					this.HighlightAnimator.Initialize();
					this.HighlightAnimator.AnimatorDriver.ContinueForward();
				}
				else
				{
					this.HighlightAnimator.Initialize();
					this.HighlightAnimator.AnimatorDriver.ContinueBackwards();
				}
			}
		}
	}

	// Token: 0x1700022A RID: 554
	// (get) Token: 0x06000A26 RID: 2598 RVA: 0x0002BE8D File Offset: 0x0002A08D
	public override bool CanBeEntered
	{
		get
		{
			return true;
		}
	}

	// Token: 0x06000A27 RID: 2599 RVA: 0x0002BE90 File Offset: 0x0002A090
	public override void EnterInGroup()
	{
		this.IsActive = true;
		this.IsHighlightVisible = true;
		foreach (MessageBox messageBox in this.NavigateMessageBoxes)
		{
			messageBox.SetMessageProvider(this.ActivateNavigateMessageProvider);
		}
	}

	// Token: 0x06000A28 RID: 2600 RVA: 0x0002BED8 File Offset: 0x0002A0D8
	private void PlayDownSound()
	{
		if (this.m_soundPlayer)
		{
			UberPoolManager.Instance.RemoveOnDestroyed(this.m_soundPlayer.gameObject);
			this.m_soundPlayer.FadeOut(0.1f, true);
			this.m_soundPlayer = null;
		}
		if (this.ChangeVolumeDownSound)
		{
			this.m_soundPlayer = Sound.Play(this.ChangeVolumeDownSound.GetSound(null), base.transform.position, delegate()
			{
				this.m_soundPlayer = null;
			});
		}
	}

	// Token: 0x06000A29 RID: 2601 RVA: 0x0002BF60 File Offset: 0x0002A160
	private void PlayUpSound()
	{
		if (this.m_soundPlayer)
		{
			UberPoolManager.Instance.RemoveOnDestroyed(this.m_soundPlayer.gameObject);
			this.m_soundPlayer.FadeOut(0.1f, true);
			this.m_soundPlayer = null;
		}
		if (this.ChangeVolumeUpSound)
		{
			this.m_soundPlayer = Sound.Play(this.ChangeVolumeUpSound.GetSound(null), base.transform.position, delegate()
			{
				this.m_soundPlayer = null;
			});
		}
	}

	// Token: 0x06000A2A RID: 2602 RVA: 0x0002BFE8 File Offset: 0x0002A1E8
	public void FixedUpdate()
	{
		this.UpdateSlider();
		if (!this.IsActive)
		{
			return;
		}
		if (Core.Input.MenuLeft.OnPressed)
		{
			Core.Input.MenuLeft.Used = true;
			this.m_holdRemainingTime = 0.5f;
			this.Value = Mathf.Clamp(this.Value - this.Step, this.MinValue, this.MaxValue);
			this.PlayDownSound();
		}
		if (Core.Input.MenuRight.OnPressed)
		{
			Core.Input.MenuRight.Used = true;
			this.m_holdRemainingTime = 0.5f;
			this.Value = Mathf.Clamp(this.Value + this.Step, this.MinValue, this.MaxValue);
			this.PlayUpSound();
		}
		if (Core.Input.MenuLeft.Pressed)
		{
			this.m_holdRemainingTime -= Time.deltaTime;
			if (this.m_holdRemainingTime < 0f)
			{
				this.m_holdRemainingTime = 0.05f;
				this.Value = Mathf.Clamp(this.Value - this.Step, this.MinValue, this.MaxValue);
				this.PlayDownSound();
			}
		}
		if (Core.Input.MenuRight.Pressed)
		{
			this.m_holdRemainingTime -= Time.deltaTime;
			if (this.m_holdRemainingTime < 0f)
			{
				this.m_holdRemainingTime = 0.05f;
				this.Value = Mathf.Clamp(this.Value + this.Step, this.MinValue, this.MaxValue);
				this.PlayUpSound();
			}
		}
		if (Core.Input.Cancel.OnPressed)
		{
			this.OnBackPressed();
			foreach (MessageBox messageBox in this.NavigateMessageBoxes)
			{
				messageBox.SetMessageProvider(this.DeactivateNavigateMessageProvider);
			}
		}
		if (Core.Input.ActionButtonA.OnPressedNotUsed)
		{
			this.OnBackPressed();
			foreach (MessageBox messageBox2 in this.NavigateMessageBoxes)
			{
				messageBox2.SetMessageProvider(this.DeactivateNavigateMessageProvider);
			}
		}
		if (Core.Input.LeftClick.OnPressed && this.DotRect.Contains(Core.Input.CursorPositionUI))
		{
			this.m_isDragged = true;
			this.PlayUpSound();
		}
		if (Core.Input.LeftClick.OnReleased)
		{
			this.m_isDragged = false;
			this.PlayUpSound();
		}
		if (this.m_isDragged)
		{
			float x = this.SliderDot.parent.TransformPoint(this.MinX, 0f, 0f).x;
			float x2 = this.SliderDot.parent.TransformPoint(this.MaxX, 0f, 0f).x;
			this.NormalizedValue = Utility.Round(Mathf.InverseLerp(x, x2, Core.Input.CursorPositionUI.x), 0.01f);
		}
		this.UpdateSlider();
	}

	// Token: 0x06000A2B RID: 2603 RVA: 0x0002C2E4 File Offset: 0x0002A4E4
	public void UpdateSlider()
	{
		this.SliderDot.localPosition = new Vector3(Mathf.Lerp(this.MinX, this.MaxX, this.NormalizedValue), this.SliderDot.localPosition.y, this.SliderDot.localPosition.z);
	}

	// Token: 0x1700022B RID: 555
	// (get) Token: 0x06000A2C RID: 2604 RVA: 0x0002C340 File Offset: 0x0002A540
	public Rect DotRect
	{
		get
		{
			return new Rect
			{
				width = 0.4f,
				height = 0.4f,
				center = this.SliderDot.transform.position
			};
		}
	}

	// Token: 0x06000A2D RID: 2605 RVA: 0x0002C38C File Offset: 0x0002A58C
	public override bool OnMenuItemChangedInGroup(CleverMenuItemGroup group)
	{
		if (group == this)
		{
			this.IsHighlightVisible = true;
			return true;
		}
		this.IsActive = false;
		this.IsHighlightVisible = false;
		return false;
	}

	// Token: 0x0400084F RID: 2127
	public CleverMenuItem CleverMenuItem;

	// Token: 0x04000850 RID: 2128
	public SoundProvider ChangeVolumeUpSound;

	// Token: 0x04000851 RID: 2129
	public SoundProvider ChangeVolumeDownSound;

	// Token: 0x04000852 RID: 2130
	public float MinValue;

	// Token: 0x04000853 RID: 2131
	public float MaxValue = 1f;

	// Token: 0x04000854 RID: 2132
	public float Step = 0.1f;

	// Token: 0x04000855 RID: 2133
	public MessageBox[] NavigateMessageBoxes;

	// Token: 0x04000856 RID: 2134
	public MessageProvider ActivateNavigateMessageProvider;

	// Token: 0x04000857 RID: 2135
	public MessageProvider DeactivateNavigateMessageProvider;

	// Token: 0x04000858 RID: 2136
	public BaseAnimator HighlightAnimator;

	// Token: 0x04000859 RID: 2137
	public Transform SliderDot;

	// Token: 0x0400085A RID: 2138
	public float MinX = 1f;

	// Token: 0x0400085B RID: 2139
	public float MaxX = 6f;

	// Token: 0x0400085C RID: 2140
	private SoundPlayer m_soundPlayer;

	// Token: 0x0400085D RID: 2141
	private bool m_isActive;

	// Token: 0x0400085E RID: 2142
	private bool m_isHighlightVisible;

	// Token: 0x0400085F RID: 2143
	private float m_holdRemainingTime;

	// Token: 0x04000860 RID: 2144
	private bool m_isDragged;
}
