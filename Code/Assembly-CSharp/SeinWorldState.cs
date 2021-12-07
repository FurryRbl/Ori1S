using System;
using Game;
using Sein.World;

// Token: 0x02000998 RID: 2456
public class SeinWorldState : SaveSerialize
{
	// Token: 0x0600359A RID: 13722 RVA: 0x000E0B56 File Offset: 0x000DED56
	public override void Awake()
	{
		base.Awake();
		SeinWorldState.Instance = this;
		Game.Events.Scheduler.OnGameReset.Add(new Action(this.OnGameReset));
	}

	// Token: 0x0600359B RID: 13723 RVA: 0x000E0B7F File Offset: 0x000DED7F
	public override void OnDestroy()
	{
		base.OnDestroy();
		Game.Events.Scheduler.OnGameReset.Remove(new Action(this.OnGameReset));
	}

	// Token: 0x0600359C RID: 13724 RVA: 0x000E0BA4 File Offset: 0x000DEDA4
	public override void Serialize(Archive ar)
	{
		ar.Serialize(ref Keys.ForlornRuins);
		ar.Serialize(ref Keys.GinsoTree);
		ar.Serialize(ref Keys.MountHoru);
		ar.Serialize(ref Sein.World.Events.GinsoTreeEntered);
		ar.Serialize(ref Sein.World.Events.MistLifted);
		ar.Serialize(ref Sein.World.Events.WaterPurified);
		ar.Serialize(ref Sein.World.Events.WindRestored);
		ar.Serialize(ref Sein.World.Events.GumoFree);
		Sein.World.Events.GravityActivated = ar.Serialize(Sein.World.Events.GravityActivated);
		ar.Serialize(ref Sein.World.Events.SpiritTreeReached);
		ar.Serialize(ref Sein.World.Events.WarmthReturned);
		ar.Serialize(ref Sein.World.Events.DarknessLifted);
	}

	// Token: 0x0600359D RID: 13725 RVA: 0x000E0C3C File Offset: 0x000DEE3C
	public void OnGameReset()
	{
		Keys.ForlornRuins = false;
		Keys.GinsoTree = false;
		Keys.MountHoru = false;
		Sein.World.Events.GinsoTreeEntered = false;
		Sein.World.Events.MistLifted = false;
		Sein.World.Events.WaterPurified = false;
		Sein.World.Events.WindRestored = false;
		Sein.World.Events.GumoFree = false;
		Sein.World.Events.GravityActivated = false;
		Sein.World.Events.SpiritTreeReached = false;
		Sein.World.Events.WarmthReturned = false;
		Sein.World.Events.DarknessLifted = false;
	}

	// Token: 0x04003029 RID: 12329
	public static SeinWorldState Instance;
}
