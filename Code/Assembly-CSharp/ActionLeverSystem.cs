using System;
using UnityEngine;

// Token: 0x020008EB RID: 2283
public class ActionLeverSystem : MonoBehaviour
{
	// Token: 0x060032E9 RID: 13033 RVA: 0x000D7136 File Offset: 0x000D5336
	public void OnLeverEnter()
	{
		this.LeverEnterAction.Perform(null);
	}

	// Token: 0x060032EA RID: 13034 RVA: 0x000D7144 File Offset: 0x000D5344
	public void OnLeverExit()
	{
		this.LeverExitAction.Perform(null);
	}

	// Token: 0x060032EB RID: 13035 RVA: 0x000D7152 File Offset: 0x000D5352
	public void OnGrabLever()
	{
		this.LeverGrabbedAction.Perform(null);
	}

	// Token: 0x060032EC RID: 13036 RVA: 0x000D7160 File Offset: 0x000D5360
	public void OnReleaseLever()
	{
		this.LeverReleasedAction.Perform(null);
	}

	// Token: 0x060032ED RID: 13037 RVA: 0x000D716E File Offset: 0x000D536E
	public void OnLeverLeft()
	{
		this.LeverLeftAction.Perform(null);
	}

	// Token: 0x060032EE RID: 13038 RVA: 0x000D717C File Offset: 0x000D537C
	public void OnLeverRight()
	{
		this.LeverRightAction.Perform(null);
	}

	// Token: 0x060032EF RID: 13039 RVA: 0x000D718A File Offset: 0x000D538A
	public void OnLeverLeftFailed()
	{
		this.LeverLeftFailedAction.Perform(null);
	}

	// Token: 0x060032F0 RID: 13040 RVA: 0x000D7198 File Offset: 0x000D5398
	public void OnLeverRightFailed()
	{
		this.LeverRightFailedAction.Perform(null);
	}

	// Token: 0x060032F1 RID: 13041 RVA: 0x000D71A6 File Offset: 0x000D53A6
	public void OnLeftMiddle()
	{
		this.LeverMiddleAction.Perform(null);
	}

	// Token: 0x060032F2 RID: 13042 RVA: 0x000D71B4 File Offset: 0x000D53B4
	public bool CanLeverLeftCallback()
	{
		return this.CanLeverLeft.Validate(null);
	}

	// Token: 0x060032F3 RID: 13043 RVA: 0x000D71C2 File Offset: 0x000D53C2
	public bool CanLeverRightCallback()
	{
		return this.CanLeverRight.Validate(null);
	}

	// Token: 0x060032F4 RID: 13044 RVA: 0x000D71D0 File Offset: 0x000D53D0
	public void Awake()
	{
		if (this.LeverEnterAction)
		{
			this.Lever.LeverEnterEvent = new Action(this.OnLeverEnter);
		}
		if (this.LeverExitAction)
		{
			this.Lever.LeverExitEvent = new Action(this.OnLeverExit);
		}
		if (this.LeverGrabbedAction)
		{
			this.Lever.GrabLeverEvent = new Action(this.OnGrabLever);
		}
		if (this.LeverReleasedAction)
		{
			this.Lever.ReleaseLeverEvent = new Action(this.OnReleaseLever);
		}
		if (this.LeverLeftAction)
		{
			this.Lever.LeverLeftEvent = new Action(this.OnLeverLeft);
		}
		if (this.LeverRightAction)
		{
			this.Lever.LeverRightEvent = new Action(this.OnLeverRight);
		}
		if (this.LeverLeftFailedAction)
		{
			this.Lever.LeverLeftFailedEvent = new Action(this.OnLeverLeftFailed);
		}
		if (this.LeverRightFailedAction)
		{
			this.Lever.LeverRightFailedEvent = new Action(this.OnLeverRightFailed);
		}
		if (this.LeverMiddleAction)
		{
			this.Lever.LeverMiddleEvent = new Action(this.OnLeftMiddle);
		}
		if (this.CanLeverLeft)
		{
			this.Lever.CanLeverLeft = new Func<bool>(this.CanLeverLeftCallback);
		}
		if (this.CanLeverRight)
		{
			this.Lever.CanLeverRight = new Func<bool>(this.CanLeverRightCallback);
		}
	}

	// Token: 0x04002DE4 RID: 11748
	public Lever Lever;

	// Token: 0x04002DE5 RID: 11749
	public ActionMethod LeverEnterAction;

	// Token: 0x04002DE6 RID: 11750
	public ActionMethod LeverExitAction;

	// Token: 0x04002DE7 RID: 11751
	public ActionMethod LeverGrabbedAction;

	// Token: 0x04002DE8 RID: 11752
	public ActionMethod LeverReleasedAction;

	// Token: 0x04002DE9 RID: 11753
	public ActionMethod LeverLeftAction;

	// Token: 0x04002DEA RID: 11754
	public ActionMethod LeverRightAction;

	// Token: 0x04002DEB RID: 11755
	public ActionMethod LeverLeftFailedAction;

	// Token: 0x04002DEC RID: 11756
	public ActionMethod LeverRightFailedAction;

	// Token: 0x04002DED RID: 11757
	public ActionMethod LeverMiddleAction;

	// Token: 0x04002DEE RID: 11758
	public Condition CanLeverLeft;

	// Token: 0x04002DEF RID: 11759
	public Condition CanLeverRight;
}
