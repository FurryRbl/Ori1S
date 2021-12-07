using System;
using Game;
using UnityEngine;

// Token: 0x02000159 RID: 345
public class DifficultyController : SaveSerialize
{
	// Token: 0x06000DFF RID: 3583 RVA: 0x00041518 File Offset: 0x0003F718
	public override void Awake()
	{
		base.Awake();
		DifficultyController.Instance = this;
		Events.Scheduler.OnGameReset.Add(new Action(this.OnGameReset));
	}

	// Token: 0x06000E00 RID: 3584 RVA: 0x00041541 File Offset: 0x0003F741
	public override void OnDestroy()
	{
		base.OnDestroy();
		Events.Scheduler.OnGameReset.Remove(new Action(this.OnGameReset));
	}

	// Token: 0x06000E01 RID: 3585 RVA: 0x00041564 File Offset: 0x0003F764
	public override void Serialize(Archive ar)
	{
		if (ar.Reading)
		{
			this.Difficulty = (DifficultyMode)ar.Serialize(0);
			this.LowestDifficulty = (DifficultyMode)ar.Serialize(0);
		}
		else
		{
			ar.Serialize((int)this.Difficulty);
			ar.Serialize((int)this.LowestDifficulty);
		}
	}

	// Token: 0x06000E02 RID: 3586 RVA: 0x000415B5 File Offset: 0x0003F7B5
	public void OnGameReset()
	{
		this.Difficulty = DifficultyMode.Normal;
		this.LowestDifficulty = DifficultyMode.Normal;
	}

	// Token: 0x06000E03 RID: 3587 RVA: 0x000415C8 File Offset: 0x0003F7C8
	public void ChangeDifficulty(DifficultyMode easy)
	{
		this.Difficulty = easy;
		this.LowestDifficulty = (DifficultyMode)Mathf.Min((int)easy, (int)this.LowestDifficulty);
		this.OnDifficultyChanged();
	}

	// Token: 0x06000E04 RID: 3588 RVA: 0x000415F9 File Offset: 0x0003F7F9
	public void SetDifficulty(DifficultyMode difficulty)
	{
		this.Difficulty = difficulty;
		this.LowestDifficulty = difficulty;
		this.OnDifficultyChanged();
	}

	// Token: 0x04000B57 RID: 2903
	public static DifficultyController Instance;

	// Token: 0x04000B58 RID: 2904
	public DifficultyMode Difficulty = DifficultyMode.Normal;

	// Token: 0x04000B59 RID: 2905
	public DifficultyMode LowestDifficulty = DifficultyMode.Normal;

	// Token: 0x04000B5A RID: 2906
	public Action OnDifficultyChanged = delegate()
	{
	};
}
