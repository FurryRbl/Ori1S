using System;
using Game;

// Token: 0x02000927 RID: 2343
public class RestoreHealthAction : ActionMethod
{
	// Token: 0x060033E9 RID: 13289 RVA: 0x000DA528 File Offset: 0x000D8728
	public override void Perform(IContext context)
	{
		SeinCharacter sein = Characters.Sein;
		sein.Energy.Gain(sein.Energy.Max - sein.Energy.Current);
		sein.Mortality.Health.GainHealth(sein.Mortality.Health.MaxHealth - (int)sein.Mortality.Health.Amount);
		sein.SoulFlame.FillSoulFlameBar();
	}

	// Token: 0x060033EA RID: 13290 RVA: 0x000DA59A File Offset: 0x000D879A
	public override void Serialize(Archive ar)
	{
		if (ar.Reading)
		{
		}
	}

	// Token: 0x04002ED3 RID: 11987
	public float FillDurationPerUnit = 0.2f;

	// Token: 0x04002ED4 RID: 11988
	private float m_startHealth;

	// Token: 0x04002ED5 RID: 11989
	private float m_startEnergy;

	// Token: 0x04002ED6 RID: 11990
	private float m_time;

	// Token: 0x04002ED7 RID: 11991
	private SoundPlayer m_fillSound;

	// Token: 0x04002ED8 RID: 11992
	private float m_soundDuration;

	// Token: 0x04002ED9 RID: 11993
	private float m_healthDuration;

	// Token: 0x04002EDA RID: 11994
	private float m_energyDuration;

	// Token: 0x04002EDB RID: 11995
	private float m_healthTime;

	// Token: 0x04002EDC RID: 11996
	private float m_energyTime;
}
