using System;

// Token: 0x020006AA RID: 1706
public class MusicVolumeProvider : FloatValueProvider
{
	// Token: 0x06002931 RID: 10545 RVA: 0x000B1DE2 File Offset: 0x000AFFE2
	public override float GetFloatValue()
	{
		return GameSettings.Instance.MusicVolume;
	}
}
