using System;

// Token: 0x020006AC RID: 1708
public class SoundVolumeProvider : FloatValueProvider
{
	// Token: 0x06002936 RID: 10550 RVA: 0x000B1F4B File Offset: 0x000B014B
	public override float GetFloatValue()
	{
		return GameSettings.Instance.SoundEffectsVolume;
	}
}
