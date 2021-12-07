using System;
using Game;

// Token: 0x02000638 RID: 1592
public class SeinKeystonesFloatProvider : FloatValueProvider
{
	// Token: 0x06002716 RID: 10006 RVA: 0x000AACCD File Offset: 0x000A8ECD
	public override float GetFloatValue()
	{
		return (float)Characters.Sein.Inventory.Keystones;
	}
}
