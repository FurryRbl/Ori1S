using System;

// Token: 0x02000458 RID: 1112
public class SeinWallDangle : CharacterState, ISeinReceiver
{
	// Token: 0x06001EC1 RID: 7873 RVA: 0x000877F6 File Offset: 0x000859F6
	public void SetReferenceToSein(SeinCharacter sein)
	{
		this.Sein = sein;
	}

	// Token: 0x04001A92 RID: 6802
	public SeinCharacter Sein;
}
