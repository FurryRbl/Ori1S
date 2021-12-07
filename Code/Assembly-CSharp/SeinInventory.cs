using System;

// Token: 0x020001F5 RID: 501
public class SeinInventory : SaveSerialize
{
	// Token: 0x14000026 RID: 38
	// (add) Token: 0x06001156 RID: 4438 RVA: 0x0004F7F9 File Offset: 0x0004D9F9
	// (remove) Token: 0x06001157 RID: 4439 RVA: 0x0004F812 File Offset: 0x0004DA12
	public event Action OnCollectKeystones = delegate()
	{
	};

	// Token: 0x14000027 RID: 39
	// (add) Token: 0x06001158 RID: 4440 RVA: 0x0004F82B File Offset: 0x0004DA2B
	// (remove) Token: 0x06001159 RID: 4441 RVA: 0x0004F844 File Offset: 0x0004DA44
	public event Action OnCollectMapstone = delegate()
	{
	};

	// Token: 0x17000318 RID: 792
	// (get) Token: 0x0600115A RID: 4442 RVA: 0x0004F85D File Offset: 0x0004DA5D
	public bool HasKeystones
	{
		get
		{
			return this.Keystones != 0;
		}
	}

	// Token: 0x17000319 RID: 793
	// (get) Token: 0x0600115B RID: 4443 RVA: 0x0004F86B File Offset: 0x0004DA6B
	public bool HasMapstones
	{
		get
		{
			return this.MapStones != 0;
		}
	}

	// Token: 0x0600115C RID: 4444 RVA: 0x0004F879 File Offset: 0x0004DA79
	public bool CanAfford(int cost)
	{
		return this.Keystones >= cost;
	}

	// Token: 0x0600115D RID: 4445 RVA: 0x0004F887 File Offset: 0x0004DA87
	public void SpendKeystones(int cost)
	{
		this.Keystones -= cost;
		if (this.Keystones < 0)
		{
			this.Keystones = 0;
		}
	}

	// Token: 0x0600115E RID: 4446 RVA: 0x0004F8AA File Offset: 0x0004DAAA
	public void SpendMapstone(int cost)
	{
		this.MapStones -= cost;
		if (this.MapStones < 0)
		{
			this.MapStones = 0;
		}
	}

	// Token: 0x0600115F RID: 4447 RVA: 0x0004F8CD File Offset: 0x0004DACD
	public void CollectKeystones(int amount)
	{
		this.Keystones += amount;
		this.OnCollectKeystones();
	}

	// Token: 0x06001160 RID: 4448 RVA: 0x0004F8E8 File Offset: 0x0004DAE8
	public void CollectMapstone(int amount)
	{
		this.MapStones += amount;
		this.OnCollectMapstone();
	}

	// Token: 0x06001161 RID: 4449 RVA: 0x0004F903 File Offset: 0x0004DB03
	public void RestoreKeystones(int amount)
	{
		this.CollectKeystones(amount);
	}

	// Token: 0x06001162 RID: 4450 RVA: 0x0004F90C File Offset: 0x0004DB0C
	public override void Serialize(Archive ar)
	{
		ar.Serialize(ref this.Keystones);
		ar.Serialize(ref this.MapStones);
		ar.Serialize(ref this.SkillPointsCollected);
	}

	// Token: 0x04000EF0 RID: 3824
	public int Keystones;

	// Token: 0x04000EF1 RID: 3825
	public int MapStones;

	// Token: 0x04000EF2 RID: 3826
	public int SkillPointsCollected;
}
