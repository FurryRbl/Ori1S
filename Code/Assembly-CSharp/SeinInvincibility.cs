using System;

// Token: 0x02000093 RID: 147
public class SeinInvincibility : CharacterState, ISeinReceiver
{
	// Token: 0x0600061D RID: 1565 RVA: 0x00017FCE File Offset: 0x000161CE
	public void SetReferenceToSein(SeinCharacter sein)
	{
		this.Sein = sein;
	}

	// Token: 0x0600061E RID: 1566 RVA: 0x00017FD8 File Offset: 0x000161D8
	public override void UpdateCharacterState()
	{
		if (this.Sein.Mortality.Health)
		{
			if (this.Sein.Mortality.Health.Amount == 1f)
			{
				if (this.LowHealthAnimator.Stopped)
				{
					this.LowHealthAnimator.Restart();
				}
			}
			else if (this.LowHealthAnimator.Stopped)
			{
				this.LowHealthAnimator.Sample(0f);
				this.LowHealthAnimator.Stop();
			}
		}
	}

	// Token: 0x040004B4 RID: 1204
	public float GainLevelInvincibilityDuration = 4f;

	// Token: 0x040004B5 RID: 1205
	public LegacyAnimator LowHealthAnimator;

	// Token: 0x040004B6 RID: 1206
	public SeinCharacter Sein;
}
