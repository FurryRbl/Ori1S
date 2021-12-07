using System;

// Token: 0x0200019F RID: 415
public struct SaveObject
{
	// Token: 0x06000FFA RID: 4090 RVA: 0x00049193 File Offset: 0x00047393
	public SaveObject(MoonGuid id)
	{
		this.Id = id;
		this.Data = new Archive();
	}

	// Token: 0x04000D1E RID: 3358
	public MoonGuid Id;

	// Token: 0x04000D1F RID: 3359
	public Archive Data;
}
