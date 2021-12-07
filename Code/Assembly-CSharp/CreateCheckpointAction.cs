using System;
using Game;
using UnityEngine;

// Token: 0x020002D2 RID: 722
[Category("System")]
public class CreateCheckpointAction : ActionMethod
{
	// Token: 0x0600164F RID: 5711 RVA: 0x0006266C File Offset: 0x0006086C
	public override void Perform(IContext context)
	{
		GameController.Instance.CreateCheckpoint();
		Vector3 position = Characters.Current.Position;
		if (this.RespawnPosition != Vector2.zero)
		{
			Characters.Current.Position = this.RespawnPosition + base.transform.position;
		}
		GameController.Instance.CreateCheckpoint();
		Characters.Current.Position = position;
		if (this.SaveToDisk)
		{
			SaveSlotsManager.CurrentSaveSlot.WasKilled = false;
			GameController.Instance.SaveGameController.PerformSave();
			GameController.Instance.PerformSaveGameSequence();
		}
	}

	// Token: 0x0400134F RID: 4943
	public Vector2 RespawnPosition = Vector2.zero;

	// Token: 0x04001350 RID: 4944
	public bool SaveToDisk = true;
}
