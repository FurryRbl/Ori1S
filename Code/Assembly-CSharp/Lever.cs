using System;
using System.Collections.Generic;
using Core;
using Game;
using UnityEngine;

// Token: 0x020002C8 RID: 712
public class Lever : SaveSerialize, ISuspendable, IDynamicGraphicHierarchy
{
	// Token: 0x170003E7 RID: 999
	// (get) Token: 0x06001610 RID: 5648 RVA: 0x00061A58 File Offset: 0x0005FC58
	// (set) Token: 0x06001611 RID: 5649 RVA: 0x00061A60 File Offset: 0x0005FC60
	public bool InRange { get; private set; }

	// Token: 0x170003E8 RID: 1000
	// (get) Token: 0x06001612 RID: 5650 RVA: 0x00061A69 File Offset: 0x0005FC69
	// (set) Token: 0x06001613 RID: 5651 RVA: 0x00061A71 File Offset: 0x0005FC71
	public bool IsGrabbed { get; private set; }

	// Token: 0x170003E9 RID: 1001
	// (get) Token: 0x06001614 RID: 5652 RVA: 0x00061A7A File Offset: 0x0005FC7A
	public SeinCharacter Sein
	{
		get
		{
			return Characters.Sein;
		}
	}

	// Token: 0x170003EA RID: 1002
	// (get) Token: 0x06001615 RID: 5653 RVA: 0x00061A81 File Offset: 0x0005FC81
	public bool NeedsToBeOnGround
	{
		get
		{
			return this.LeverType != Lever.LeverMode.LeftRightToggle;
		}
	}

	// Token: 0x06001616 RID: 5654 RVA: 0x00061A8F File Offset: 0x0005FC8F
	public void OnEnable()
	{
		Lever.All.Add(this);
	}

	// Token: 0x06001617 RID: 5655 RVA: 0x00061A9C File Offset: 0x0005FC9C
	public void OnDisable()
	{
		Lever.All.Remove(this);
	}

	// Token: 0x06001618 RID: 5656 RVA: 0x00061AAA File Offset: 0x0005FCAA
	public override void Awake()
	{
		base.Awake();
		this.Transform = base.transform;
		SuspensionManager.Register(this);
	}

	// Token: 0x06001619 RID: 5657 RVA: 0x00061AC4 File Offset: 0x0005FCC4
	public override void OnDestroy()
	{
		base.OnDestroy();
		SuspensionManager.Unregister(this);
	}

	// Token: 0x0600161A RID: 5658 RVA: 0x00061AD4 File Offset: 0x0005FCD4
	public void OnEnterLever()
	{
		this.InRange = true;
		this.LeverEnterEvent();
		foreach (LegacyAnimator legacyAnimator in this.HighlightAnimators)
		{
			if (legacyAnimator)
			{
				legacyAnimator.ContinueForward();
			}
		}
	}

	// Token: 0x0600161B RID: 5659 RVA: 0x00061B24 File Offset: 0x0005FD24
	public void OnExitLever()
	{
		this.InRange = false;
		this.LeverExitEvent();
		foreach (LegacyAnimator legacyAnimator in this.HighlightAnimators)
		{
			if (legacyAnimator)
			{
				legacyAnimator.ContinueBackward();
			}
		}
	}

	// Token: 0x0600161C RID: 5660 RVA: 0x00061B73 File Offset: 0x0005FD73
	public void OnGrabLever()
	{
		this.IsGrabbed = true;
		this.GrabLeverEvent();
	}

	// Token: 0x0600161D RID: 5661 RVA: 0x00061B88 File Offset: 0x0005FD88
	public void OnReleaseLever()
	{
		if (this.LeverType == Lever.LeverMode.LeftMiddleRightSpring && this.Direction != Lever.LeverDirections.Middle)
		{
			this.OnPushLeverMiddle();
		}
		this.IsGrabbed = false;
		this.ReleaseLeverEvent();
	}

	// Token: 0x0600161E RID: 5662 RVA: 0x00061BC8 File Offset: 0x0005FDC8
	public void OnPushLeverLeft()
	{
		if (this.CanLeverLeft())
		{
			this.Direction = Lever.LeverDirections.Left;
			this.LeverLeftEvent();
			if (this.LeftSound)
			{
				Sound.Play(this.LeftSound.GetSound(null), base.transform.position, null);
			}
		}
		else
		{
			this.LeverLeftFailedEvent();
		}
	}

	// Token: 0x0600161F RID: 5663 RVA: 0x00061C38 File Offset: 0x0005FE38
	public void OnPushLeverRight()
	{
		if (this.CanLeverRight())
		{
			this.Direction = Lever.LeverDirections.Right;
			this.LeverRightEvent();
			if (this.RightSound)
			{
				Sound.Play(this.RightSound.GetSound(null), base.transform.position, null);
			}
		}
		else
		{
			this.LeverRightFailedEvent();
		}
	}

	// Token: 0x06001620 RID: 5664 RVA: 0x00061CA8 File Offset: 0x0005FEA8
	public void OnPushLeverMiddle()
	{
		this.Direction = Lever.LeverDirections.Middle;
		this.LeverMiddleEvent();
		if (this.MiddleSound)
		{
			Sound.Play(this.MiddleSound.GetSound(null), base.transform.position, null);
		}
	}

	// Token: 0x06001621 RID: 5665 RVA: 0x00061CF5 File Offset: 0x0005FEF5
	public void FixedUpdate()
	{
		if (this.IsSuspended)
		{
			return;
		}
		this.HackyRotatingHandle();
	}

	// Token: 0x06001622 RID: 5666 RVA: 0x00061D0C File Offset: 0x0005FF0C
	private void HackyRotatingHandle()
	{
		if (this.RotatingHandle)
		{
			if (this.Direction == Lever.LeverDirections.Left)
			{
				this.m_handleRotationTime = Mathf.Max(-1f, this.m_handleRotationTime - this.HandleRotationSpeed * Time.deltaTime);
			}
			else if (this.Direction == Lever.LeverDirections.Right)
			{
				this.m_handleRotationTime = Mathf.Min(1f, this.m_handleRotationTime + this.HandleRotationSpeed * Time.deltaTime);
			}
			else
			{
				if (this.m_handleRotationTime > 0f)
				{
					this.m_handleRotationTime = Mathf.Max(0f, this.m_handleRotationTime - this.HandleRotationSpeed * Time.deltaTime);
				}
				if (this.m_handleRotationTime < 0f)
				{
					this.m_handleRotationTime = Mathf.Min(0f, this.m_handleRotationTime + this.HandleRotationSpeed * Time.deltaTime);
				}
			}
			this.RotatingHandle.localEulerAngles = new Vector3(0f, 0f, -this.HandleRotation.Evaluate(this.m_handleRotationTime) * this.HandleRotationAmount);
		}
	}

	// Token: 0x06001623 RID: 5667 RVA: 0x00061E29 File Offset: 0x00060029
	public bool PlayLeverAnimation()
	{
		return this.InRange && this.IsGrabbed;
	}

	// Token: 0x170003EB RID: 1003
	// (get) Token: 0x06001624 RID: 5668 RVA: 0x00061E3F File Offset: 0x0006003F
	public Vector3 Position
	{
		get
		{
			return this.Transform.position;
		}
	}

	// Token: 0x170003EC RID: 1004
	// (get) Token: 0x06001625 RID: 5669 RVA: 0x00061E4C File Offset: 0x0006004C
	public float SeinPositionOffset
	{
		get
		{
			return 0.8f;
		}
	}

	// Token: 0x06001626 RID: 5670 RVA: 0x00061E54 File Offset: 0x00060054
	public void SetLeverDirection(Lever.LeverDirections leverDirection)
	{
		if (this.Direction == leverDirection)
		{
			return;
		}
		switch (leverDirection)
		{
		case Lever.LeverDirections.Left:
			this.OnPushLeverLeft();
			break;
		case Lever.LeverDirections.Middle:
			this.OnPushLeverMiddle();
			break;
		case Lever.LeverDirections.Right:
			this.OnPushLeverRight();
			break;
		}
	}

	// Token: 0x06001627 RID: 5671 RVA: 0x00061EA8 File Offset: 0x000600A8
	public override void Serialize(Archive ar)
	{
		this.Direction = (Lever.LeverDirections)ar.Serialize((int)this.Direction);
		if (ar.Reading)
		{
			this.IsGrabbed = false;
			this.InRange = false;
		}
	}

	// Token: 0x170003ED RID: 1005
	// (get) Token: 0x06001628 RID: 5672 RVA: 0x00061EE0 File Offset: 0x000600E0
	// (set) Token: 0x06001629 RID: 5673 RVA: 0x00061EE8 File Offset: 0x000600E8
	public bool IsSuspended { get; set; }

	// Token: 0x170003EE RID: 1006
	// (get) Token: 0x0600162A RID: 5674 RVA: 0x00061EF4 File Offset: 0x000600F4
	public bool CanBeGrabbed
	{
		get
		{
			return this.CanGrabCondition == null || this.CanGrabCondition.Validate(null);
		}
	}

	// Token: 0x040012FE RID: 4862
	public static List<Lever> All = new List<Lever>();

	// Token: 0x040012FF RID: 4863
	public float Radius = 2f;

	// Token: 0x04001300 RID: 4864
	public Transform RotatingHandle;

	// Token: 0x04001301 RID: 4865
	public Lever.LeverDirections Direction = Lever.LeverDirections.Middle;

	// Token: 0x04001302 RID: 4866
	public LegacyAnimator[] HighlightAnimators;

	// Token: 0x04001303 RID: 4867
	public Varying2DSoundProvider LeftSound;

	// Token: 0x04001304 RID: 4868
	public Varying2DSoundProvider MiddleSound;

	// Token: 0x04001305 RID: 4869
	public Varying2DSoundProvider RightSound;

	// Token: 0x04001306 RID: 4870
	public Condition CanGrabCondition;

	// Token: 0x04001307 RID: 4871
	public Lever.LeverMode LeverType = Lever.LeverMode.LeftMiddleRightSpring;

	// Token: 0x04001308 RID: 4872
	public Transform Transform;

	// Token: 0x04001309 RID: 4873
	public AnimationCurve HandleRotation;

	// Token: 0x0400130A RID: 4874
	private float m_handleRotationTime;

	// Token: 0x0400130B RID: 4875
	public float HandleRotationSpeed;

	// Token: 0x0400130C RID: 4876
	public float HandleRotationAmount = 40f;

	// Token: 0x0400130D RID: 4877
	public Action GrabLeverEvent = delegate()
	{
	};

	// Token: 0x0400130E RID: 4878
	public Action ReleaseLeverEvent = delegate()
	{
	};

	// Token: 0x0400130F RID: 4879
	public Action LeverLeftEvent = delegate()
	{
	};

	// Token: 0x04001310 RID: 4880
	public Action LeverRightEvent = delegate()
	{
	};

	// Token: 0x04001311 RID: 4881
	public Action LeverLeftFailedEvent = delegate()
	{
	};

	// Token: 0x04001312 RID: 4882
	public Action LeverRightFailedEvent = delegate()
	{
	};

	// Token: 0x04001313 RID: 4883
	public Action LeverMiddleEvent = delegate()
	{
	};

	// Token: 0x04001314 RID: 4884
	public Action LeverEnterEvent = delegate()
	{
	};

	// Token: 0x04001315 RID: 4885
	public Action LeverExitEvent = delegate()
	{
	};

	// Token: 0x04001316 RID: 4886
	public Func<bool> CanLeverLeft = () => true;

	// Token: 0x04001317 RID: 4887
	public Func<bool> CanLeverRight = () => true;

	// Token: 0x020002C9 RID: 713
	public enum LeverDirections
	{
		// Token: 0x04001327 RID: 4903
		Left,
		// Token: 0x04001328 RID: 4904
		Middle,
		// Token: 0x04001329 RID: 4905
		Right
	}

	// Token: 0x020008EC RID: 2284
	public enum LeverMode
	{
		// Token: 0x04002DF1 RID: 11761
		LeftRightToggle,
		// Token: 0x04002DF2 RID: 11762
		LeftRightGrab,
		// Token: 0x04002DF3 RID: 11763
		LeftMiddleRightSpring,
		// Token: 0x04002DF4 RID: 11764
		LeftMiddleRightStay
	}
}
