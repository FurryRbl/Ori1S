using System;
using Game;

// Token: 0x02000625 RID: 1573
public class SeinSoulFlameCooldownValueProvider : FloatValueProvider
{
	// Token: 0x060026D0 RID: 9936 RVA: 0x000A9AA7 File Offset: 0x000A7CA7
	public override float GetFloatValue()
	{
		return Characters.Sein.SoulFlame.BarValue;
	}
}
