using System;
using Core;
using Game;
using UnityEngine;

// Token: 0x0200007F RID: 127
public class Ori : SaveSerialize, ISuspendable
{
	// Token: 0x17000160 RID: 352
	// (get) Token: 0x06000576 RID: 1398 RVA: 0x00015B2F File Offset: 0x00013D2F
	private Transform m_target
	{
		get
		{
			return Characters.Sein.Transform;
		}
	}

	// Token: 0x17000161 RID: 353
	// (get) Token: 0x06000577 RID: 1399 RVA: 0x00015B3B File Offset: 0x00013D3B
	public Vector3 TargetPosition
	{
		get
		{
			return this.m_target.position;
		}
	}

	// Token: 0x17000162 RID: 354
	// (get) Token: 0x06000579 RID: 1401 RVA: 0x00015B51 File Offset: 0x00013D51
	// (set) Token: 0x06000578 RID: 1400 RVA: 0x00015B48 File Offset: 0x00013D48
	public bool EnableHoverWobbling
	{
		get
		{
			return this.m_enableHoverWobbling;
		}
		set
		{
			this.m_enableHoverWobbling = value;
		}
	}

	// Token: 0x17000163 RID: 355
	// (get) Token: 0x0600057A RID: 1402 RVA: 0x00015B5C File Offset: 0x00013D5C
	public Vector3 HoverOffset
	{
		get
		{
			return new Vector3(Mathf.Sin((this.m_stateCurrentTime / 2f + 5f) * 6.2831855f) * 1f, Mathf.Sin(this.m_stateCurrentTime / 3f * 6.2831855f) * 0.5f, 0f) * Mathf.SmoothStep(1f, 0.2f, this.m_listenTime);
		}
	}

	// Token: 0x17000164 RID: 356
	// (get) Token: 0x0600057B RID: 1403 RVA: 0x00015BCD File Offset: 0x00013DCD
	// (set) Token: 0x0600057C RID: 1404 RVA: 0x00015BDA File Offset: 0x00013DDA
	public Vector3 Position
	{
		get
		{
			return this.m_transform.position;
		}
		set
		{
			this.m_transform.position = value;
		}
	}

	// Token: 0x17000165 RID: 357
	// (get) Token: 0x0600057D RID: 1405 RVA: 0x00015BE8 File Offset: 0x00013DE8
	// (set) Token: 0x0600057E RID: 1406 RVA: 0x00015BF0 File Offset: 0x00013DF0
	public bool IsSuspended { get; set; }

	// Token: 0x0600057F RID: 1407 RVA: 0x00015BFC File Offset: 0x00013DFC
	public override void Awake()
	{
		base.Awake();
		SuspensionManager.Register(this);
		this.m_transform = base.transform;
		this.m_collider = base.GetComponentInChildren<Collider>();
		this.ChangeState(this.CurrentState);
		Game.Checkpoint.Events.OnPostRestore.Add(new Action(this.OnRestoreCheckpoint));
		Characters.Ori = this;
	}

	// Token: 0x06000580 RID: 1408 RVA: 0x00015C55 File Offset: 0x00013E55
	public void OnEnable()
	{
		this.MoveOriToPlayer();
	}

	// Token: 0x06000581 RID: 1409 RVA: 0x00015C60 File Offset: 0x00013E60
	public override void OnDestroy()
	{
		base.OnDestroy();
		SuspensionManager.Unregister(this);
		Game.Checkpoint.Events.OnPostRestore.Remove(new Action(this.OnRestoreCheckpoint));
	}

	// Token: 0x06000582 RID: 1410 RVA: 0x00015C8F File Offset: 0x00013E8F
	public override void Serialize(Archive ar)
	{
		if (ar.Reading)
		{
			this.ChangeState(Ori.State.Hovering);
			this.StopTwinkle();
			this.StopListening();
		}
	}

	// Token: 0x06000583 RID: 1411 RVA: 0x00015CAF File Offset: 0x00013EAF
	private void OnRestoreCheckpoint()
	{
		this.MoveOriToPlayer();
		this.StopTwinkle();
		this.StopListening();
	}

	// Token: 0x06000584 RID: 1412 RVA: 0x00015CC4 File Offset: 0x00013EC4
	public void ChangeState(Ori.State state)
	{
		Ori.State currentState = this.CurrentState;
		if (currentState != Ori.State.Hovering)
		{
			if (currentState != Ori.State.MoveToPosition)
			{
			}
		}
		this.CurrentState = state;
		this.m_stateCurrentTime = 0f;
		switch (this.CurrentState)
		{
		case Ori.State.Hovering:
			if (this.m_collider)
			{
				this.m_collider.enabled = false;
			}
			if (UI.Cameras.Current != null)
			{
				UI.Cameras.Current.ChangeTargetToCurrentCharacter();
			}
			break;
		}
	}

	// Token: 0x06000585 RID: 1413 RVA: 0x00015D69 File Offset: 0x00013F69
	public void MoveOriToPlayer()
	{
		this.MoveOriBackToPlayer();
	}

	// Token: 0x06000586 RID: 1414 RVA: 0x00015D74 File Offset: 0x00013F74
	public void MoveOriBackToPlayer()
	{
		if (this.m_target)
		{
			this.Position = this.m_target.position + this.TargetOffset;
		}
	}

	// Token: 0x06000587 RID: 1415 RVA: 0x00015DB0 File Offset: 0x00013FB0
	public void MoveOriToPosition(Vector3 position, float duration)
	{
		this.m_moveToPositionStartPosition = this.m_transform.position;
		this.m_moveToPositionEndPosition = position;
		this.m_moveToPositionDuration = duration;
		this.m_moveToPositionStartVelocity = Vector3.zero;
		this.m_moveToPositionTime = 0f;
		if (this.MoveToPositionSound)
		{
			Sound.Play(this.MoveToPositionSound.GetSound(null), base.transform.position, null);
		}
		this.ChangeState(Ori.State.MoveToPosition);
	}

	// Token: 0x06000588 RID: 1416 RVA: 0x00015E28 File Offset: 0x00014028
	public void MoveOriAlongCurve(AnimationCurve positionX, AnimationCurve positionY, AnimationCurve positionZ, Vector3 position, float duration)
	{
		this.m_moveToPositionStartPosition = this.Position;
		this.m_moveToPositionEndPosition = position;
		this.m_moveToPositionDuration = duration;
		this.m_positionXCurve = positionX;
		this.m_positionYCurve = positionY;
		this.m_positionZCurve = positionZ;
		this.ChangeState(Ori.State.MoveAlongCurve);
	}

	// Token: 0x06000589 RID: 1417 RVA: 0x00015E70 File Offset: 0x00014070
	public void UpdateState()
	{
		switch (this.CurrentState)
		{
		case Ori.State.Hovering:
			this.UpdateHoveringState();
			break;
		case Ori.State.MoveToPosition:
			this.UpdateMoveToPositionState();
			break;
		case Ori.State.MoveAlongCurve:
			this.UpdateMoveAlongCurveState();
			break;
		}
		this.m_stateCurrentTime += Time.deltaTime;
	}

	// Token: 0x0600058A RID: 1418 RVA: 0x00015ED0 File Offset: 0x000140D0
	private void UpdateMoveAlongCurveState()
	{
		Vector3 a = this.m_moveToPositionEndPosition - this.m_moveToPositionStartPosition;
		this.Position = this.m_moveToPositionStartPosition + Vector3.Scale(a, new Vector3(this.m_positionXCurve.Evaluate(this.m_stateCurrentTime / this.m_moveToPositionDuration), this.m_positionYCurve.Evaluate(this.m_stateCurrentTime / this.m_moveToPositionDuration), this.m_positionZCurve.Evaluate(this.m_stateCurrentTime / this.m_moveToPositionDuration)));
		if (this.EnableHoverWobbling)
		{
			this.Position += this.HoverOffset * 0.5f;
		}
	}

	// Token: 0x0600058B RID: 1419 RVA: 0x00015F80 File Offset: 0x00014180
	private void UpdateMoveToPositionState()
	{
		this.m_moveToPositionTime += Time.deltaTime;
		this.m_moveToPositionStartPosition += this.m_moveToPositionStartVelocity * Time.deltaTime;
		this.m_moveToPositionStartVelocity *= 0.9f;
		Vector3 vector = this.m_moveToPositionEndPosition;
		if (this.EnableHoverWobbling)
		{
			vector += this.HoverOffset;
		}
		Vector3 a = vector;
		if (this.m_moveToPositionDuration != 0f)
		{
			a = Vector3.Lerp(vector, this.m_moveToPositionStartPosition, this.MoveToPositionCurve.Evaluate(1f - this.m_moveToPositionTime / this.m_moveToPositionDuration));
		}
		Vector3 b = a - this.Position;
		this.m_transform.position += b;
	}

	// Token: 0x0600058C RID: 1420 RVA: 0x00016058 File Offset: 0x00014258
	private void UpdateHoveringState()
	{
		if (this.m_target == null)
		{
			return;
		}
		Vector3 vector = this.TargetOffset;
		vector += Vector2.Scale((!Characters.Sein.FaceLeft) ? new Vector2(1f, 1f) : new Vector2(-1f, 1f), this.ListenOffset * this.m_listenTime);
		if (this.EnableHoverWobbling)
		{
			vector += this.HoverOffset;
		}
		SeinCharacter sein = Characters.Sein;
		if (sein)
		{
			vector = MoonMath.Angle.Rotate(vector, sein.PlatformBehaviour.PlatformMovement.GravityAngle);
		}
		vector += this.TargetOffsetAttack;
		vector += this.m_target.position;
		Vector3 vector2 = vector - this.Position;
		this.m_transform.position += vector2.normalized * this.DistanceToSpeedCurve.Evaluate(vector2.magnitude) * Time.deltaTime;
	}

	// Token: 0x0600058D RID: 1421 RVA: 0x00016188 File Offset: 0x00014388
	private void FixedUpdate()
	{
		if (this.IsSuspended)
		{
			return;
		}
		this.m_twinkleTime = Mathf.Clamp01(this.m_twinkleTime + (float)((!this.m_isTwinkling) ? -1 : 1) * Time.deltaTime * 3f);
		this.m_listenTime = Mathf.Clamp01(this.m_listenTime + (float)((!this.m_isListening) ? -1 : 1) * Time.deltaTime * 3f);
		this.UpdateState();
	}

	// Token: 0x0600058E RID: 1422 RVA: 0x00016209 File Offset: 0x00014409
	public void BackToPlayerController()
	{
		this.MoveOriToPlayer();
		this.ChangeState(Ori.State.Hovering);
	}

	// Token: 0x0600058F RID: 1423 RVA: 0x00016218 File Offset: 0x00014418
	public Color SetRGB(Color old, Color newColor)
	{
		newColor.a = old.a;
		return newColor;
	}

	// Token: 0x06000590 RID: 1424 RVA: 0x00016229 File Offset: 0x00014429
	public void StopTwinkle()
	{
		this.m_isTwinkling = false;
		this.TwinkleAnimator.AnimatorDriver.Stop();
	}

	// Token: 0x06000591 RID: 1425 RVA: 0x00016242 File Offset: 0x00014442
	public void StartTwinkle()
	{
		this.m_isTwinkling = true;
		this.TwinkleAnimator.AnimatorDriver.Restart();
	}

	// Token: 0x06000592 RID: 1426 RVA: 0x0001625B File Offset: 0x0001445B
	public void StartListening()
	{
		this.m_isListening = true;
	}

	// Token: 0x06000593 RID: 1427 RVA: 0x00016264 File Offset: 0x00014464
	public void StopListening()
	{
		this.m_isListening = false;
	}

	// Token: 0x04000430 RID: 1072
	public bool InsideDoor;

	// Token: 0x04000431 RID: 1073
	public bool InsideMapstone;

	// Token: 0x04000432 RID: 1074
	public Ori.State CurrentState;

	// Token: 0x04000433 RID: 1075
	public AnimationCurve MoveToPositionCurve;

	// Token: 0x04000434 RID: 1076
	public AnimationCurve DistanceToSpeedCurve;

	// Token: 0x04000435 RID: 1077
	public LegacyAnimator ShootAnimation;

	// Token: 0x04000436 RID: 1078
	public Renderer SpriteRenderer;

	// Token: 0x04000437 RID: 1079
	public ScaleAnimator TwinkleAnimator;

	// Token: 0x04000438 RID: 1080
	public Vector3 TargetOffset;

	// Token: 0x04000439 RID: 1081
	public Vector3 TargetOffsetAttack;

	// Token: 0x0400043A RID: 1082
	public Vector2 ListenOffset = new Vector2(1.5f, 0f);

	// Token: 0x0400043B RID: 1083
	public bool UseZPosition;

	// Token: 0x0400043C RID: 1084
	private Collider m_collider;

	// Token: 0x0400043D RID: 1085
	private bool m_enableHoverWobbling = true;

	// Token: 0x0400043E RID: 1086
	private float m_moveToPositionDuration;

	// Token: 0x0400043F RID: 1087
	private Vector3 m_moveToPositionEndPosition;

	// Token: 0x04000440 RID: 1088
	private Vector3 m_moveToPositionStartPosition;

	// Token: 0x04000441 RID: 1089
	private Vector3 m_moveToPositionStartVelocity;

	// Token: 0x04000442 RID: 1090
	private float m_moveToPositionTime;

	// Token: 0x04000443 RID: 1091
	public SoundProvider MoveToPositionSound;

	// Token: 0x04000444 RID: 1092
	public SoundProvider OnHighlightInterestZoneSound;

	// Token: 0x04000445 RID: 1093
	public SoundProvider OnUnhighlightInterestZoneSound;

	// Token: 0x04000446 RID: 1094
	private AnimationCurve m_positionXCurve;

	// Token: 0x04000447 RID: 1095
	private AnimationCurve m_positionYCurve;

	// Token: 0x04000448 RID: 1096
	private AnimationCurve m_positionZCurve;

	// Token: 0x04000449 RID: 1097
	private Transform m_spriteTransform;

	// Token: 0x0400044A RID: 1098
	private float m_stateCurrentTime;

	// Token: 0x0400044B RID: 1099
	private Transform m_transform;

	// Token: 0x0400044C RID: 1100
	private float m_twinkleTime;

	// Token: 0x0400044D RID: 1101
	private float m_listenTime;

	// Token: 0x0400044E RID: 1102
	private bool m_isTwinkling;

	// Token: 0x0400044F RID: 1103
	private bool m_isListening;

	// Token: 0x02000307 RID: 775
	public enum State
	{
		// Token: 0x040013D5 RID: 5077
		Hovering,
		// Token: 0x040013D6 RID: 5078
		MoveToPosition,
		// Token: 0x040013D7 RID: 5079
		MoveAlongCurve,
		// Token: 0x040013D8 RID: 5080
		HoverForGrenade
	}
}
