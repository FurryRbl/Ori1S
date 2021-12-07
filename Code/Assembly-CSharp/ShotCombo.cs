using System;

// Token: 0x02000088 RID: 136
[Serializable]
public class ShotCombo
{
	// Token: 0x17000172 RID: 370
	// (get) Token: 0x060005D2 RID: 1490 RVA: 0x0001710A File Offset: 0x0001530A
	// (set) Token: 0x060005D3 RID: 1491 RVA: 0x00017112 File Offset: 0x00015312
	public int CurrentShot
	{
		get
		{
			return this.m_currentShot;
		}
		set
		{
			this.m_currentShot = value;
		}
	}

	// Token: 0x17000173 RID: 371
	// (get) Token: 0x060005D4 RID: 1492 RVA: 0x0001711B File Offset: 0x0001531B
	// (set) Token: 0x060005D5 RID: 1493 RVA: 0x00017123 File Offset: 0x00015323
	public bool CanShoot
	{
		get
		{
			return this.m_canShoot;
		}
		set
		{
			this.m_canShoot = value;
		}
	}

	// Token: 0x060005D6 RID: 1494 RVA: 0x0001712C File Offset: 0x0001532C
	public void Update(float dt)
	{
		this.m_timeSinceLastShot += dt;
		this.UpdateState();
	}

	// Token: 0x060005D7 RID: 1495 RVA: 0x00017144 File Offset: 0x00015344
	public void UpdateState()
	{
		if (this.CurrentShot == this.NumberOfShotsPerCombo)
		{
			if (this.m_timeSinceLastShot > this.CooldownTimeForCompletedCombo)
			{
				this.CanShoot = true;
				this.CurrentShot = 0;
			}
		}
		else if (this.CurrentShot == 0)
		{
			this.CanShoot = true;
		}
		else
		{
			if (this.m_timeSinceLastShot > this.ShotDelay || !this.UseShotDelay)
			{
				this.CanShoot = true;
			}
			if (this.m_timeSinceLastShot > this.CooldownTimeForIncompleteCombo)
			{
				this.CanShoot = true;
				this.CurrentShot = 0;
			}
		}
	}

	// Token: 0x060005D8 RID: 1496 RVA: 0x000171DF File Offset: 0x000153DF
	public void Shoot()
	{
		this.CurrentShot++;
		this.CanShoot = false;
		this.m_timeSinceLastShot = 0f;
	}

	// Token: 0x04000485 RID: 1157
	public int NumberOfShotsPerCombo = 3;

	// Token: 0x04000486 RID: 1158
	public float CooldownTimeForIncompleteCombo = 0.3f;

	// Token: 0x04000487 RID: 1159
	public float CooldownTimeForCompletedCombo = 0.5f;

	// Token: 0x04000488 RID: 1160
	public float ShotDelay;

	// Token: 0x04000489 RID: 1161
	public bool UseShotDelay;

	// Token: 0x0400048A RID: 1162
	private float m_timeSinceLastShot;

	// Token: 0x0400048B RID: 1163
	private bool m_canShoot;

	// Token: 0x0400048C RID: 1164
	private int m_currentShot;
}
