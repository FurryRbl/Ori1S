using System;

// Token: 0x0200005B RID: 91
[Serializable]
public class CharacterAbility
{
	// Token: 0x060003BF RID: 959 RVA: 0x0000F661 File Offset: 0x0000D861
	public static explicit operator bool(CharacterAbility characterAbility)
	{
		return characterAbility.HasAbility;
	}

	// Token: 0x040002F0 RID: 752
	public bool HasAbility;
}
