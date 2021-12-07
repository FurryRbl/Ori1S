using System;
using Game;

// Token: 0x02000266 RID: 614
public class SaveGameAction : ActionMethod
{
	// Token: 0x060014A7 RID: 5287 RVA: 0x0005D410 File Offset: 0x0005B610
	public override void Perform(IContext context)
	{
		SaveSlotBackupsManager.CreateCurrentBackup();
		float current = 0f;
		float amount = 0f;
		SeinCharacter sein = Characters.Sein;
		if (sein)
		{
			current = sein.Energy.Current;
			amount = sein.Mortality.Health.Amount;
			sein.Energy.Current = sein.Energy.Max;
			sein.Mortality.Health.Amount = (float)sein.Mortality.Health.MaxHealth;
		}
		GameController.Instance.CreateCheckpoint();
		if (sein)
		{
			sein.Energy.Current = current;
			sein.Mortality.Health.Amount = amount;
		}
		GameController.Instance.SaveGameController.PerformSave();
		GameController.Instance.PerformSaveGameSequence();
	}
}
