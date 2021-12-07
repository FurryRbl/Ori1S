using System;
using UnityEngine;

// Token: 0x02000429 RID: 1065
public class DeathInformation
{
	// Token: 0x06001DB0 RID: 7600 RVA: 0x00083262 File Offset: 0x00081462
	public DeathInformation()
	{
	}

	// Token: 0x06001DB1 RID: 7601 RVA: 0x0008326A File Offset: 0x0008146A
	public DeathInformation(Vector3 position, int timeOfDeath, int progress, int deathNumber)
	{
		this.Position = position;
		this.TimeOfDeath = timeOfDeath;
		this.Progress = progress;
		this.DeathNumber = deathNumber;
	}

	// Token: 0x06001DB2 RID: 7602 RVA: 0x00083290 File Offset: 0x00081490
	public void Serialize(Archive ar)
	{
		ar.Serialize(ref this.Position);
		ar.Serialize(ref this.TimeOfDeath);
		ar.Serialize(ref this.Progress);
		ar.Serialize(ref this.DeathNumber);
	}

	// Token: 0x04001994 RID: 6548
	public Vector3 Position;

	// Token: 0x04001995 RID: 6549
	public int TimeOfDeath;

	// Token: 0x04001996 RID: 6550
	public int Progress;

	// Token: 0x04001997 RID: 6551
	public int DeathNumber;
}
