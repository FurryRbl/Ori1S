using System;

// Token: 0x0200020D RID: 525
[Serializable]
public class AVProWindowsMediaMovieClip
{
	// Token: 0x06001271 RID: 4721 RVA: 0x0005399A File Offset: 0x00051B9A
	public AVProWindowsMediaMovieClip(string name, int inPoint, int outPoint)
	{
		this.name = name;
		this.inPoint = inPoint;
		this.outPoint = outPoint;
	}

	// Token: 0x04000FAD RID: 4013
	public string name;

	// Token: 0x04000FAE RID: 4014
	public int inPoint = -1;

	// Token: 0x04000FAF RID: 4015
	public int outPoint = -1;
}
