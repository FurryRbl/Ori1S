using System;

// Token: 0x02000081 RID: 129
public class SeinIceSpiritFlame : CharacterState, ISeinReceiver
{
	// Token: 0x060005A5 RID: 1445 RVA: 0x000166F4 File Offset: 0x000148F4
	public void SetReferenceToSein(SeinCharacter sein)
	{
		this.m_sein = sein;
		this.m_sein.Abilities.IceSpiritFlame = this;
	}

	// Token: 0x17000166 RID: 358
	// (get) Token: 0x060005A6 RID: 1446 RVA: 0x0001670E File Offset: 0x0001490E
	public bool HasEnoughEnergy
	{
		get
		{
			return this.m_sein.Energy.CanAfford(1f);
		}
	}

	// Token: 0x060005A7 RID: 1447 RVA: 0x00016725 File Offset: 0x00014925
	public void SpendEnergy()
	{
		this.m_sein.Energy.Spend(1f);
	}

	// Token: 0x060005A8 RID: 1448 RVA: 0x0001673C File Offset: 0x0001493C
	public override void UpdateCharacterState()
	{
	}

	// Token: 0x04000469 RID: 1129
	public SpiritFlame IceSpiritFlame;

	// Token: 0x0400046A RID: 1130
	public float SpiritFlameRange;

	// Token: 0x0400046B RID: 1131
	private SeinCharacter m_sein;
}
