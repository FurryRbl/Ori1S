using System;
using Game;

// Token: 0x02000639 RID: 1593
public class SeinMapstonesFloatProvider : FloatValueProvider
{
	// Token: 0x06002718 RID: 10008 RVA: 0x000AACE7 File Offset: 0x000A8EE7
	public override float GetFloatValue()
	{
		return (float)Characters.Sein.Inventory.MapStones;
	}
}
