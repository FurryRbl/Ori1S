using System;
using Game;

// Token: 0x02000376 RID: 886
public class PlayerRespawnTrigger : Trigger
{
	// Token: 0x06001950 RID: 6480 RVA: 0x0006CF17 File Offset: 0x0006B117
	public new void Awake()
	{
		Game.Checkpoint.Events.OnPostRestore.Add(new Action(this.OnRestoreCheckpoint));
	}

	// Token: 0x06001951 RID: 6481 RVA: 0x0006CF2F File Offset: 0x0006B12F
	public new void OnDestroy()
	{
		Game.Checkpoint.Events.OnPostRestore.Remove(new Action(this.OnRestoreCheckpoint));
	}

	// Token: 0x06001952 RID: 6482 RVA: 0x0006CF47 File Offset: 0x0006B147
	public void OnRestoreCheckpoint()
	{
		base.DoTrigger(true);
	}
}
