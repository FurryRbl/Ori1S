using System;
using System.Collections.Generic;
using Game;
using UnityEngine;

// Token: 0x0200042B RID: 1067
public class SeinDeathsManager : SaveSerialize
{
	// Token: 0x06001DB9 RID: 7609 RVA: 0x00083418 File Offset: 0x00081618
	[ContextMenu("Fake a death here")]
	public void FakeADeathHere()
	{
		this.RecordDeath();
	}

	// Token: 0x06001DBA RID: 7610 RVA: 0x00083420 File Offset: 0x00081620
	public override void Awake()
	{
		base.Awake();
		SeinDeathsManager.Instance = this;
		Events.Scheduler.OnGameReset.Add(new Action(this.OnGameReset));
	}

	// Token: 0x06001DBB RID: 7611 RVA: 0x00083449 File Offset: 0x00081649
	public override void OnDestroy()
	{
		base.OnDestroy();
		if (SeinDeathsManager.Instance == this)
		{
			SeinDeathsManager.Instance = null;
		}
		Events.Scheduler.OnGameReset.Remove(new Action(this.OnGameReset));
	}

	// Token: 0x06001DBC RID: 7612 RVA: 0x00083482 File Offset: 0x00081682
	public void OnGameReset()
	{
		this.Deaths.Clear();
	}

	// Token: 0x06001DBD RID: 7613 RVA: 0x00083490 File Offset: 0x00081690
	public override void Serialize(Archive ar)
	{
		if (ar.Reading)
		{
			int num = ar.Serialize(0);
			this.Deaths.Clear();
			for (int i = 0; i < num; i++)
			{
				DeathInformation deathInformation = new DeathInformation();
				deathInformation.Serialize(ar);
				this.Deaths.Add(deathInformation);
			}
			DeathWispsManager.Refresh();
		}
		else
		{
			int count = this.Deaths.Count;
			ar.Serialize(count);
			for (int j = 0; j < count; j++)
			{
				this.Deaths[j].Serialize(ar);
			}
		}
	}

	// Token: 0x06001DBE RID: 7614 RVA: 0x00083530 File Offset: 0x00081730
	public static void OnDeath()
	{
		if (SeinDeathsManager.Instance && DifficultyController.Instance.Difficulty == DifficultyMode.OneLife)
		{
			SeinDeathsManager.Instance.Deaths.Clear();
			SeinDeathsManager.Instance.RecordDeath();
		}
	}

	// Token: 0x06001DBF RID: 7615 RVA: 0x00083578 File Offset: 0x00081778
	public void RecordDeath()
	{
		Vector3 position = Characters.Sein.Position;
		int gameTimeInSeconds = GameController.Instance.GameTimeInSeconds;
		int completionPercentage = GameWorld.Instance.CompletionPercentage;
		int count = this.Deaths.Count;
		this.Deaths.Add(new DeathInformation(position, gameTimeInSeconds, completionPercentage, count));
		SaveSceneManager.Master.Save(Game.Checkpoint.SaveGameData.Master, SeinDeathsManager.Instance);
	}

	// Token: 0x0400199E RID: 6558
	public static SeinDeathsManager Instance;

	// Token: 0x0400199F RID: 6559
	public List<DeathInformation> Deaths = new List<DeathInformation>();
}
