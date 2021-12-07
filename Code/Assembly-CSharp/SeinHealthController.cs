using System;
using UnityEngine;

// Token: 0x02000095 RID: 149
public class SeinHealthController : SaveSerialize, ISeinReceiver
{
	// Token: 0x06000621 RID: 1569 RVA: 0x00018079 File Offset: 0x00016279
	public void SetAmount(float amount)
	{
		this.Amount = amount;
		this.VisualMinAmount = amount;
		this.VisualMaxAmount = amount;
	}

	// Token: 0x06000622 RID: 1570 RVA: 0x00018090 File Offset: 0x00016290
	public void FixedUpdate()
	{
		this.VisualMinAmount = Mathf.MoveTowards(this.VisualMinAmount, this.Amount, Time.deltaTime * 4f);
		this.VisualMaxAmount = Mathf.MoveTowards(this.VisualMaxAmount, this.Amount, Time.deltaTime * 4f);
	}

	// Token: 0x1700017B RID: 379
	// (get) Token: 0x06000623 RID: 1571 RVA: 0x000180E1 File Offset: 0x000162E1
	public float VisualMinAmountNormalized
	{
		get
		{
			return this.VisualMinAmount / (float)this.MaxHealth;
		}
	}

	// Token: 0x1700017C RID: 380
	// (get) Token: 0x06000624 RID: 1572 RVA: 0x000180F1 File Offset: 0x000162F1
	public float VisualMaxAmountNormalized
	{
		get
		{
			return this.VisualMaxAmount / (float)this.MaxHealth;
		}
	}

	// Token: 0x1700017D RID: 381
	// (get) Token: 0x06000625 RID: 1573 RVA: 0x00018101 File Offset: 0x00016301
	public int HealthUpgradesCollected
	{
		get
		{
			return this.MaxHealth / 4 - 3;
		}
	}

	// Token: 0x06000626 RID: 1574 RVA: 0x00018110 File Offset: 0x00016310
	public void OnRespawn()
	{
		InstantiateUtility.Instantiate(this.RespawnEffect, this.m_sein.Transform.position, Quaternion.identity);
		this.m_sein.Mortality.DamageReciever.MakeInvincible(1f);
	}

	// Token: 0x06000627 RID: 1575 RVA: 0x00018158 File Offset: 0x00016358
	public void LoseHealth(int amount)
	{
		this.Amount -= (float)amount;
		if (this.Amount < 0f)
		{
			this.Amount = 0f;
		}
		this.VisualMinAmount = this.Amount;
	}

	// Token: 0x06000628 RID: 1576 RVA: 0x0001819B File Offset: 0x0001639B
	public void GainHealth(int amount)
	{
		this.Amount += (float)amount;
		this.Amount = Mathf.Min((float)this.MaxHealth, this.Amount);
		this.VisualMaxAmount = this.Amount;
	}

	// Token: 0x06000629 RID: 1577 RVA: 0x000181D0 File Offset: 0x000163D0
	public void GainMaxHeartContainer()
	{
		this.MaxHealth += 4;
		this.RestoreAllHealth();
	}

	// Token: 0x0600062A RID: 1578 RVA: 0x000181E6 File Offset: 0x000163E6
	public void RestoreAllHealth()
	{
		this.Amount = (float)this.MaxHealth;
		this.VisualMaxAmount = this.Amount;
	}

	// Token: 0x0600062B RID: 1579 RVA: 0x00018201 File Offset: 0x00016401
	public void TakeDamage(int amount)
	{
		this.Amount -= (float)amount;
		this.Amount = Mathf.Max(0f, this.Amount);
		this.VisualMinAmount = this.Amount;
	}

	// Token: 0x0600062C RID: 1580 RVA: 0x00018234 File Offset: 0x00016434
	public override void Serialize(Archive ar)
	{
		ar.Serialize(ref this.Amount);
		ar.Serialize(ref this.MaxHealth);
		if (ar.Reading)
		{
			this.VisualMaxAmount = (this.VisualMinAmount = this.Amount);
		}
	}

	// Token: 0x1700017E RID: 382
	// (get) Token: 0x0600062D RID: 1581 RVA: 0x00018279 File Offset: 0x00016479
	public bool IsFull
	{
		get
		{
			return this.Amount == (float)this.MaxHealth;
		}
	}

	// Token: 0x0600062E RID: 1582 RVA: 0x0001828A File Offset: 0x0001648A
	public void SetReferenceToSein(SeinCharacter sein)
	{
		this.m_sein = sein;
	}

	// Token: 0x040004BA RID: 1210
	public float Amount;

	// Token: 0x040004BB RID: 1211
	public int MaxHealth;

	// Token: 0x040004BC RID: 1212
	public float VisualMinAmount;

	// Token: 0x040004BD RID: 1213
	public float VisualMaxAmount;

	// Token: 0x040004BE RID: 1214
	public GameObject RespawnEffect;

	// Token: 0x040004BF RID: 1215
	private SeinCharacter m_sein;
}
