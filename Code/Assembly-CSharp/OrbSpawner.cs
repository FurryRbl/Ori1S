using System;
using Game;
using UnityEngine;

// Token: 0x0200050D RID: 1293
public class OrbSpawner : MonoBehaviour
{
	// Token: 0x06002291 RID: 8849 RVA: 0x0009760C File Offset: 0x0009580C
	public void CopySettings(OrbSpawner other)
	{
		this.NumberOfExpOrbs = other.NumberOfExpOrbs;
		this.NumberOfGreenOrbs = other.NumberOfGreenOrbs;
		this.NumberOfBlueOrbs = other.NumberOfBlueOrbs;
		this.NumberOfRedOrbs = other.NumberOfRedOrbs;
		this.NumberOfYellowOrbs = other.NumberOfYellowOrbs;
	}

	// Token: 0x06002292 RID: 8850 RVA: 0x00097655 File Offset: 0x00095855
	public void Awake()
	{
		this.m_transform = base.transform;
	}

	// Token: 0x06002293 RID: 8851 RVA: 0x00097664 File Offset: 0x00095864
	public void SetNumberOfExpOrbs(int amount)
	{
		this.NumberOfYellowOrbs = amount / 250;
		amount -= this.NumberOfYellowOrbs * 250;
		this.NumberOfRedOrbs = amount / 50;
		amount -= this.NumberOfRedOrbs * 50;
		this.NumberOfBlueOrbs = amount / 10;
		amount -= this.NumberOfBlueOrbs * 10;
		this.NumberOfGreenOrbs = amount / 1;
	}

	// Token: 0x170005F1 RID: 1521
	// (get) Token: 0x06002294 RID: 8852 RVA: 0x000976C5 File Offset: 0x000958C5
	private float DamageDirectionSpeed
	{
		get
		{
			return 2f;
		}
	}

	// Token: 0x06002295 RID: 8853 RVA: 0x000976CC File Offset: 0x000958CC
	public void SpawnOrbs(IContext context)
	{
		if (this.NumberOfGreenOrbs == 0 && this.NumberOfBlueOrbs == 0 && this.NumberOfRedOrbs == 0 && this.NumberOfYellowOrbs == 0)
		{
			this.SetNumberOfExpOrbs(this.NumberOfExpOrbs);
		}
		Vector2 force = Vector2.zero;
		IDamageContext damageContext = context as IDamageContext;
		if (damageContext != null)
		{
			IDamageContext damageContext2 = damageContext;
			force = damageContext2.Damage.Force;
		}
		int num = 0;
		int i = 0;
		while (i < this.NumberOfGreenOrbs)
		{
			this.SpawnPickup(OrbSpawnerManager.ItemType.GreenExpOrb, force, num);
			i++;
			num++;
		}
		int j = 0;
		while (j < this.NumberOfBlueOrbs)
		{
			this.SpawnPickup(OrbSpawnerManager.ItemType.BlueExpOrb, force, num);
			j++;
			num++;
		}
		int k = 0;
		while (k < this.NumberOfRedOrbs)
		{
			this.SpawnPickup(OrbSpawnerManager.ItemType.RedExpOrb, force, num);
			k++;
			num++;
		}
		int l = 0;
		while (l < this.NumberOfYellowOrbs)
		{
			this.SpawnPickup(OrbSpawnerManager.ItemType.YellowExpOrb, force, num);
			l++;
			num++;
		}
		if (DifficultyController.Instance.Difficulty == DifficultyMode.Easy)
		{
			if (!this.SpawnLoot())
			{
				this.SpawnLoot();
			}
		}
		else
		{
			this.SpawnLoot();
		}
	}

	// Token: 0x06002296 RID: 8854 RVA: 0x00097804 File Offset: 0x00095A04
	private bool SpawnLoot()
	{
		float num = FixedRandom.ValueFromPosition(base.transform.position);
		if (this.LootSettings.EnergyShardChance <= 0.5f && this.LootSettings.HeartChance <= 0.5f && !this.LootOnHard && DifficultyController.Instance.Difficulty == DifficultyMode.Hard)
		{
			return false;
		}
		if (num < this.LootSettings.HeartChance)
		{
			OrbSpawnerManager.Instance.Spawn(OrbSpawnerManager.ItemType.Health, this.m_transform.position, Vector2.zero, DropPickup.State.Hover);
			return true;
		}
		if (num < this.LootSettings.HeartChance + this.LootSettings.EnergyShardChance && Characters.Sein.Energy.EnergyActive)
		{
			OrbSpawnerManager.Instance.Spawn(OrbSpawnerManager.ItemType.Energy, this.m_transform.position, Vector2.zero, DropPickup.State.Hover);
			return true;
		}
		return false;
	}

	// Token: 0x06002297 RID: 8855 RVA: 0x000978F4 File Offset: 0x00095AF4
	private void SpawnPickup(OrbSpawnerManager.ItemType item, Vector2 force, int i)
	{
		Vector2 vector = new Vector2(this.HorizontalSpeed.Evaluate(FixedRandom.Values[i % FixedRandom.Values.Length]), this.VerticalSpeed.Evaluate(FixedRandom.Values[(i + 1) % FixedRandom.Values.Length]));
		vector += force * this.DamageDirectionSpeed;
		OrbSpawnerManager.Instance.Spawn(item, this.m_transform.position, vector, this.DropPickupState);
	}

	// Token: 0x06002298 RID: 8856 RVA: 0x00097974 File Offset: 0x00095B74
	public void LimitNumberOfOrbs(int i)
	{
		int num = 0;
		int num2 = 0;
		int num3 = 0;
		int num4 = 0;
		for (int j = 0; j < i; j++)
		{
			if (this.NumberOfYellowOrbs > 0)
			{
				this.NumberOfYellowOrbs--;
				num3++;
			}
			else if (this.NumberOfRedOrbs > 0)
			{
				this.NumberOfRedOrbs--;
				num4++;
			}
			else if (this.NumberOfGreenOrbs > 0)
			{
				this.NumberOfGreenOrbs--;
				num++;
			}
			else if (this.NumberOfBlueOrbs > 0)
			{
				this.NumberOfBlueOrbs--;
				num2++;
			}
		}
		this.NumberOfRedOrbs = num4;
		this.NumberOfGreenOrbs = num;
		this.NumberOfBlueOrbs = num2;
		this.NumberOfYellowOrbs = num3;
	}

	// Token: 0x04001CFB RID: 7419
	public DropLootSettings LootSettings = new DropLootSettings();

	// Token: 0x04001CFC RID: 7420
	public bool LootOnHard;

	// Token: 0x04001CFD RID: 7421
	private Transform m_transform;

	// Token: 0x04001CFE RID: 7422
	public int NumberOfExpOrbs;

	// Token: 0x04001CFF RID: 7423
	public int NumberOfGreenOrbs;

	// Token: 0x04001D00 RID: 7424
	public int NumberOfBlueOrbs;

	// Token: 0x04001D01 RID: 7425
	public int NumberOfRedOrbs;

	// Token: 0x04001D02 RID: 7426
	public int NumberOfYellowOrbs;

	// Token: 0x04001D03 RID: 7427
	public AnimationCurve HorizontalSpeed;

	// Token: 0x04001D04 RID: 7428
	public AnimationCurve VerticalSpeed;

	// Token: 0x04001D05 RID: 7429
	public DropPickup.State DropPickupState;
}
