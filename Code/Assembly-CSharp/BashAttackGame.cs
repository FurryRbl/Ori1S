using System;
using Core;
using Game;
using UnityEngine;

// Token: 0x02000052 RID: 82
internal class BashAttackGame : Suspendable, IPooled
{
	// Token: 0x1400000D RID: 13
	// (add) Token: 0x0600034C RID: 844 RVA: 0x0000DAE4 File Offset: 0x0000BCE4
	// (remove) Token: 0x0600034D RID: 845 RVA: 0x0000DAFD File Offset: 0x0000BCFD
	public event Action<float> BashGameComplete;

	// Token: 0x170000CD RID: 205
	// (get) Token: 0x0600034E RID: 846 RVA: 0x0000DB16 File Offset: 0x0000BD16
	// (set) Token: 0x0600034F RID: 847 RVA: 0x0000DB1E File Offset: 0x0000BD1E
	public override bool IsSuspended { get; set; }

	// Token: 0x06000350 RID: 848 RVA: 0x0000DB28 File Offset: 0x0000BD28
	public void OnPoolSpawned()
	{
		this.m_bashLoopingAudioSource = null;
		this.m_keyboardSpeed = 0f;
		this.m_keyboardAngle = 0f;
		this.m_keyboardClockwise = false;
		this.m_mode = BashAttackGame.Modes.Keyboard;
		this.m_currentState = BashAttackGame.State.Appearing;
		this.Angle = 0f;
		this.m_stateCurrentTime = 0f;
		this.m_nextBashLoopPlayedTime = 0f;
		this.BashAttackCritical.enabled = true;
		this.IsSuspended = false;
		this.BashGameComplete = null;
	}

	// Token: 0x06000351 RID: 849 RVA: 0x0000DBA4 File Offset: 0x0000BDA4
	public void ChangeState(BashAttackGame.State state)
	{
		this.m_currentState = state;
		this.m_stateCurrentTime = 0f;
		switch (state)
		{
		case BashAttackGame.State.Appearing:
			this.BashAttackCritical.enabled = false;
			break;
		case BashAttackGame.State.Playing:
			this.BashAttackCritical.enabled = true;
			break;
		case BashAttackGame.State.Disappearing:
			this.BashAttackCritical.enabled = false;
			if (this.m_bashLoopingAudioSource)
			{
				InstantiateUtility.Destroy(this.m_bashLoopingAudioSource.gameObject);
			}
			break;
		}
	}

	// Token: 0x06000352 RID: 850 RVA: 0x0000DC30 File Offset: 0x0000BE30
	public void UpdateMode()
	{
		if (Core.Input.AnalogAxisLeft.magnitude > 0.2f)
		{
			this.m_mode = BashAttackGame.Modes.Controller;
		}
		else if (Core.Input.CursorMoved || GameSettings.Instance.CurrentControlScheme == ControlScheme.KeyboardAndMouse)
		{
			this.m_mode = BashAttackGame.Modes.Mouse;
		}
		else if (Core.Input.DigiPadAxis.magnitude > 0.2f && this.m_mode != BashAttackGame.Modes.Mouse)
		{
			this.m_mode = BashAttackGame.Modes.Keyboard;
		}
	}

	// Token: 0x06000353 RID: 851 RVA: 0x0000DCB0 File Offset: 0x0000BEB0
	public void FixedUpdate()
	{
		if (this.IsSuspended)
		{
			return;
		}
		if (this.m_currentState != BashAttackGame.State.Disappearing)
		{
			this.UpdateMode();
			switch (this.m_mode)
			{
			case BashAttackGame.Modes.Mouse:
			{
				Vector2 v = UI.Cameras.Current.Camera.WorldToScreenPoint(base.transform.position);
				Vector2 b = UI.Cameras.System.GUICamera.ScreenToWorldPoint(v);
				Vector2 vector = Core.Input.CursorPositionUI - b;
				if (vector.magnitude > 0.001f)
				{
					vector.Normalize();
					this.Angle = Mathf.LerpAngle(this.Angle, Mathf.Atan2(-vector.x, vector.y) * 57.29578f, 0.5f);
				}
				break;
			}
			case BashAttackGame.Modes.Keyboard:
			{
				Vector2 digiPadAxis = Core.Input.DigiPadAxis;
				if ((double)digiPadAxis.magnitude > 0.2)
				{
					float target = MoonMath.Angle.AngleFromVector(digiPadAxis) - 90f;
					float f = Mathf.DeltaAngle(this.m_keyboardAngle, target);
					if (Mathf.Sign(f) != (float)((!this.m_keyboardClockwise) ? -1 : 1))
					{
						this.m_keyboardClockwise = (Mathf.Sign(f) > 0f);
						this.m_keyboardSpeed = 0f;
					}
					this.m_keyboardSpeed += Mathf.Min(Mathf.Abs(f), Time.deltaTime * 2000f);
					this.m_keyboardAngle = Mathf.MoveTowardsAngle(this.m_keyboardAngle, target, this.m_keyboardSpeed * Time.deltaTime);
				}
				else
				{
					this.m_keyboardSpeed = 0f;
				}
				this.Angle = Mathf.LerpAngle(this.Angle, this.m_keyboardAngle, 0.5f);
				break;
			}
			case BashAttackGame.Modes.Controller:
			{
				Vector2 a = Core.Input.AnalogAxisLeft;
				float sqrMagnitude = a.sqrMagnitude;
				if (sqrMagnitude > 0.040000003f)
				{
					a /= Mathf.Sqrt(sqrMagnitude);
					this.Angle = Mathf.LerpAngle(this.Angle, Mathf.Atan2(-a.x, a.y) * 57.29578f, 0.5f);
				}
				break;
			}
			}
		}
		this.ArrowSprite.transform.parent.rotation = Quaternion.Euler(0f, 0f, this.Angle);
		this.UpdateState();
		if (Characters.Sein && !Characters.Sein.Active)
		{
			InstantiateUtility.Destroy(base.gameObject);
		}
	}

	// Token: 0x06000354 RID: 852 RVA: 0x0000DF2F File Offset: 0x0000C12F
	public void SendDirection(Vector2 direction)
	{
		this.m_keyboardAngle = MoonMath.Angle.AngleFromVector(direction) - 90f;
	}

	// Token: 0x06000355 RID: 853 RVA: 0x0000DF44 File Offset: 0x0000C144
	public void UpdateState()
	{
		switch (this.m_currentState)
		{
		case BashAttackGame.State.Appearing:
			this.UpdateAppearingState();
			break;
		case BashAttackGame.State.Playing:
			this.UpdatePlayingState();
			break;
		case BashAttackGame.State.Disappearing:
			this.UpdateDisappearingState();
			break;
		}
		this.m_stateCurrentTime += Time.deltaTime;
	}

	// Token: 0x06000356 RID: 854 RVA: 0x0000DFA4 File Offset: 0x0000C1A4
	private void UpdateDisappearingState()
	{
		float time = Mathf.Clamp01(this.m_stateCurrentTime / this.DisappearTime);
		this.ArrowSprite.localScale = this.m_originalArrowScale * this.ArrowDisappearScaleCurve.Evaluate(time);
		InstantiateUtility.Destroy(base.gameObject, 1f);
	}

	// Token: 0x06000357 RID: 855 RVA: 0x0000DFF8 File Offset: 0x0000C1F8
	private void UpdatePlayingState()
	{
		if (this.m_nextBashLoopPlayedTime <= this.m_stateCurrentTime)
		{
			this.m_bashLoopingAudioSource = Sound.Play((!Characters.Sein.PlayerAbilities.BashBuff.HasAbility) ? Characters.Sein.Abilities.Bash.BashLoopSound.GetSound(null) : Characters.Sein.Abilities.Bash.UpgradedBashLoopSound.GetSound(null), base.transform.position, delegate()
			{
				this.m_bashLoopingAudioSource = null;
			});
			if (!InstantiateUtility.IsDestroyed(this.m_bashLoopingAudioSource))
			{
				this.m_nextBashLoopPlayedTime = this.m_stateCurrentTime + this.m_bashLoopingAudioSource.Length;
			}
		}
		if (this.BashAttackCritical.CurrentState == BashAttackCritical.State.Finished)
		{
			this.GameFinished();
		}
		if (this.ButtonBash.Released)
		{
			this.GameFinished();
		}
	}

	// Token: 0x06000358 RID: 856 RVA: 0x0000E0E0 File Offset: 0x0000C2E0
	private void UpdateAppearingState()
	{
		float num = Mathf.Clamp01(this.m_stateCurrentTime / this.AppearTime);
		this.ArrowSprite.localScale = this.m_originalArrowScale * this.ArrowAppearScaleCurve.Evaluate(num);
		if (num == 1f)
		{
			this.ChangeState(BashAttackGame.State.Playing);
		}
	}

	// Token: 0x06000359 RID: 857 RVA: 0x0000E134 File Offset: 0x0000C334
	public new void Awake()
	{
		base.Awake();
		this.m_originalArrowScale = this.ArrowSprite.localScale;
	}

	// Token: 0x0600035A RID: 858 RVA: 0x0000E14D File Offset: 0x0000C34D
	public void Start()
	{
		this.ChangeState(this.m_currentState);
		this.ArrowSprite.localScale = Vector3.zero;
	}

	// Token: 0x0600035B RID: 859 RVA: 0x0000E16C File Offset: 0x0000C36C
	private void GameFinished()
	{
		Sound.Play((!Characters.Sein.PlayerAbilities.BashBuff.HasAbility) ? Characters.Sein.Abilities.Bash.BashEndSound.GetSound(null) : Characters.Sein.Abilities.Bash.UpgradedBashEndSound.GetSound(null), base.transform.position, null);
		this.BashGameComplete(this.Angle);
		this.ChangeState(BashAttackGame.State.Disappearing);
	}

	// Token: 0x170000CE RID: 206
	// (get) Token: 0x0600035C RID: 860 RVA: 0x0000E1F5 File Offset: 0x0000C3F5
	public Core.Input.InputButtonProcessor ButtonBash
	{
		get
		{
			return Core.Input.Bash;
		}
	}

	// Token: 0x04000278 RID: 632
	public float Angle;

	// Token: 0x04000279 RID: 633
	public float ArrowSpeed = 45f;

	// Token: 0x0400027A RID: 634
	public Transform ArrowSprite;

	// Token: 0x0400027B RID: 635
	public BashAttackCritical BashAttackCritical;

	// Token: 0x0400027C RID: 636
	public float AppearTime;

	// Token: 0x0400027D RID: 637
	public float DisappearTime;

	// Token: 0x0400027E RID: 638
	public AnimationCurve ArrowAppearScaleCurve;

	// Token: 0x0400027F RID: 639
	public AnimationCurve ArrowDisappearScaleCurve;

	// Token: 0x04000280 RID: 640
	private BashAttackGame.State m_currentState;

	// Token: 0x04000281 RID: 641
	private float m_stateCurrentTime;

	// Token: 0x04000282 RID: 642
	private float m_nextBashLoopPlayedTime;

	// Token: 0x04000283 RID: 643
	private Vector3 m_originalArrowScale;

	// Token: 0x04000284 RID: 644
	private SoundPlayer m_bashLoopingAudioSource;

	// Token: 0x04000285 RID: 645
	private float m_keyboardSpeed;

	// Token: 0x04000286 RID: 646
	private float m_keyboardAngle;

	// Token: 0x04000287 RID: 647
	private bool m_keyboardClockwise;

	// Token: 0x04000288 RID: 648
	private BashAttackGame.Modes m_mode = BashAttackGame.Modes.Keyboard;

	// Token: 0x02000053 RID: 83
	public enum State
	{
		// Token: 0x0400028C RID: 652
		Appearing,
		// Token: 0x0400028D RID: 653
		Playing,
		// Token: 0x0400028E RID: 654
		Disappearing
	}

	// Token: 0x02000054 RID: 84
	public enum Modes
	{
		// Token: 0x04000290 RID: 656
		Mouse,
		// Token: 0x04000291 RID: 657
		Keyboard,
		// Token: 0x04000292 RID: 658
		Controller
	}
}
