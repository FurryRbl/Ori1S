using System;
using Game;

// Token: 0x02000387 RID: 903
public class TriggerOnScrollLockPass : Trigger
{
	// Token: 0x060019AB RID: 6571 RVA: 0x0006E0AB File Offset: 0x0006C2AB
	public new void Awake()
	{
		base.Awake();
		Game.Checkpoint.Events.OnScrollLockPassed.Add(new Action(this.OnScrollLockPassed));
	}

	// Token: 0x060019AC RID: 6572 RVA: 0x0006E0C9 File Offset: 0x0006C2C9
	public new void OnDestroy()
	{
		base.OnDestroy();
		Game.Checkpoint.Events.OnScrollLockPassed.Remove(new Action(this.OnScrollLockPassed));
	}

	// Token: 0x060019AD RID: 6573 RVA: 0x0006E0E8 File Offset: 0x0006C2E8
	public void OnScrollLockPassed()
	{
		if (UI.Cameras.Current.IsOnScreen(base.transform.position))
		{
			base.DoTrigger(true);
		}
	}
}
