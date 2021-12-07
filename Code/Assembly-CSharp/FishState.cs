using System;
using fsm;

// Token: 0x0200051F RID: 1311
public abstract class FishState : IState
{
	// Token: 0x060022D7 RID: 8919 RVA: 0x00098B6B File Offset: 0x00096D6B
	public FishState(FishEnemy fish)
	{
		this.Fish = fish;
	}

	// Token: 0x060022D8 RID: 8920
	public abstract void UpdateState();

	// Token: 0x060022D9 RID: 8921
	public abstract void OnEnter();

	// Token: 0x060022DA RID: 8922
	public abstract void OnExit();

	// Token: 0x170005F4 RID: 1524
	// (get) Token: 0x060022DB RID: 8923 RVA: 0x00098B7A File Offset: 0x00096D7A
	public float CurrentStateTime
	{
		get
		{
			return this.Fish.Controller.StateMachine.CurrentStateTime;
		}
	}

	// Token: 0x04001D55 RID: 7509
	public FishEnemy Fish;
}
