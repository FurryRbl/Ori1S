using System;
using UnityEngine;

// Token: 0x0200024B RID: 587
public class Enemy : SpriteEntity
{
	// Token: 0x060013F6 RID: 5110 RVA: 0x0005B20E File Offset: 0x0005940E
	public static float ScaleHealth(float health)
	{
		if (DifficultyController.Instance.Difficulty == DifficultyMode.Easy)
		{
			health = Mathf.Ceil(health * 0.65f);
		}
		return health;
	}
}
