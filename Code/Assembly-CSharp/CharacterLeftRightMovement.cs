using System;
using UnityEngine;

// Token: 0x0200001E RID: 30
[RequireComponent(typeof(PlatformMovement))]
public class CharacterLeftRightMovement : CharacterState
{
	// Token: 0x14000009 RID: 9
	// (add) Token: 0x06000193 RID: 403 RVA: 0x00007469 File Offset: 0x00005669
	// (remove) Token: 0x06000194 RID: 404 RVA: 0x00007482 File Offset: 0x00005682
	public event Action<HorizontalPlatformMovementSettings> ModifyHorizontalPlatformMovementSettingsEvent = delegate(HorizontalPlatformMovementSettings A_0)
	{
	};

	// Token: 0x17000072 RID: 114
	// (get) Token: 0x06000195 RID: 405 RVA: 0x0000749B File Offset: 0x0000569B
	public PlatformMovement PlatformMovement
	{
		get
		{
			return this.PlatformBehaviour.PlatformMovement;
		}
	}

	// Token: 0x17000073 RID: 115
	// (get) Token: 0x06000196 RID: 406 RVA: 0x000074A8 File Offset: 0x000056A8
	public CharacterSpriteMirror SpriteMirror
	{
		get
		{
			return this.PlatformBehaviour.Visuals.SpriteMirror;
		}
	}

	// Token: 0x17000074 RID: 116
	// (get) Token: 0x06000197 RID: 407 RVA: 0x000074BA File Offset: 0x000056BA
	public HorizontalPlatformMovementSettings CurrentSettings
	{
		get
		{
			return this.m_settings;
		}
	}

	// Token: 0x17000075 RID: 117
	// (get) Token: 0x06000198 RID: 408 RVA: 0x000074C2 File Offset: 0x000056C2
	public HorizontalPlatformMovementSettings BaseSettings
	{
		get
		{
			return this.Settings;
		}
	}

	// Token: 0x06000199 RID: 409 RVA: 0x000074CA File Offset: 0x000056CA
	public override void Serialize(Archive ar)
	{
		ar.Serialize(ref this.m_horizontalInput);
		base.Serialize(ar);
	}

	// Token: 0x17000076 RID: 118
	// (get) Token: 0x0600019A RID: 410 RVA: 0x000074DF File Offset: 0x000056DF
	public HorizontalPlatformMovementSettings.SpeedSet SpeedSet
	{
		get
		{
			return (!this.PlatformMovement.IsOnGround) ? this.m_settings.Air : this.m_settings.Ground;
		}
	}

	// Token: 0x17000077 RID: 119
	// (get) Token: 0x0600019B RID: 411 RVA: 0x0000750C File Offset: 0x0000570C
	// (set) Token: 0x0600019C RID: 412 RVA: 0x00007539 File Offset: 0x00005739
	public float HorizontalInput
	{
		get
		{
			return (!this.CurrentSettings.LockInput) ? this.m_horizontalInput : 0f;
		}
		set
		{
			this.m_horizontalInput = value;
		}
	}

	// Token: 0x17000078 RID: 120
	// (get) Token: 0x0600019D RID: 413 RVA: 0x00007542 File Offset: 0x00005742
	// (set) Token: 0x0600019E RID: 414 RVA: 0x0000754A File Offset: 0x0000574A
	public float BaseHorizontalInput
	{
		get
		{
			return this.m_horizontalInput;
		}
		set
		{
			this.m_horizontalInput = value;
		}
	}

	// Token: 0x0600019F RID: 415 RVA: 0x00007553 File Offset: 0x00005753
	public void Start()
	{
		base.Active = true;
	}

	// Token: 0x060001A0 RID: 416 RVA: 0x0000755C File Offset: 0x0000575C
	public void ReverseHorizontalMovement()
	{
		this.PlatformMovement.LocalSpeedX *= -1f;
		if (this.SpriteMirror)
		{
			this.SpriteMirror.FaceLeft = !this.SpriteMirror.FaceLeft;
		}
	}

	// Token: 0x060001A1 RID: 417 RVA: 0x000075AC File Offset: 0x000057AC
	public void FixedUpdate()
	{
		if (this.PlatformBehaviour.PlatformMovement.IsSuspended)
		{
			return;
		}
		if (!base.Active)
		{
			return;
		}
		this.UpdateSettings();
		Vector2 localSpeed = this.PlatformMovement.LocalSpeed;
		float horizontalInput = this.HorizontalInput;
		if (this.UpdateFacingDirection)
		{
			if (horizontalInput < 0f)
			{
				this.SpriteMirror.FaceLeft = true;
			}
			if (horizontalInput > 0f)
			{
				this.SpriteMirror.FaceLeft = false;
			}
		}
		HorizontalPlatformMovementSettings.SpeedSet speedSet = this.SpeedSet;
		float offset = speedSet.Acceleration * Time.deltaTime * Utility.Normalize(horizontalInput);
		float num = horizontalInput * speedSet.MaxSpeed;
		float num2 = Mathf.Abs(num);
		localSpeed.x = MoonMath.Float.ClampedAdd(localSpeed.x, offset, -num2, num2);
		float offset2 = speedSet.Decceleration * Time.deltaTime * (float)((localSpeed.x <= 0f) ? -1 : 1);
		if (MoonMath.Float.Normalize(num) != MoonMath.Float.Normalize(localSpeed.x))
		{
			localSpeed.x = MoonMath.Float.ClampedSubtract(localSpeed.x, offset2, 0f, 0f);
		}
		else
		{
			localSpeed.x = MoonMath.Float.ClampedSubtract(localSpeed.x, offset2, -num2, num2);
		}
		this.PlatformMovement.LocalSpeed = localSpeed;
	}

	// Token: 0x060001A2 RID: 418 RVA: 0x00007700 File Offset: 0x00005900
	public void UpdateSettings()
	{
		this.m_settings.CopyFrom(this.Settings);
		this.ModifyHorizontalPlatformMovementSettingsEvent(this.m_settings);
	}

	// Token: 0x0400013C RID: 316
	private readonly HorizontalPlatformMovementSettings m_settings = new HorizontalPlatformMovementSettings();

	// Token: 0x0400013D RID: 317
	public PlatformBehaviour PlatformBehaviour;

	// Token: 0x0400013E RID: 318
	public HorizontalPlatformMovementSettings Settings;

	// Token: 0x0400013F RID: 319
	public bool UpdateFacingDirection = true;

	// Token: 0x04000140 RID: 320
	private float m_horizontalInput;
}
