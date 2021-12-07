using System;
using Core;
using Game;
using UnityEngine;

// Token: 0x020009B3 RID: 2483
public class CatAndMouseKuroKillController : MonoBehaviour
{
	// Token: 0x0600361F RID: 13855 RVA: 0x000E2F98 File Offset: 0x000E1198
	public void Awake()
	{
		this.m_zones = base.GetComponentsInChildren<CatAndMouseKuroLandZone>();
	}

	// Token: 0x06003620 RID: 13856 RVA: 0x000E2FA8 File Offset: 0x000E11A8
	public void Attack()
	{
		Vector3 position = Characters.Sein.Position;
		foreach (CatAndMouseKuroLandZone catAndMouseKuroLandZone in this.m_zones)
		{
			if (catAndMouseKuroLandZone.Bounds.Contains(position))
			{
				catAndMouseKuroLandZone.Animator.gameObject.SetActive(true);
				catAndMouseKuroLandZone.Animator.Initialize();
				catAndMouseKuroLandZone.Animator.AnimatorDriver.Restart();
				if (this.LandKillSound)
				{
					Sound.Play(this.LandKillSound.GetSound(null), base.transform.position, null);
				}
				return;
			}
		}
		if (this.KuroFlyAttack)
		{
			UnityEngine.Object.Instantiate(this.KuroFlyAttack, Characters.Sein.Position, Quaternion.identity);
			if (this.FlyKillSound)
			{
				Sound.Play(this.FlyKillSound.GetSound(null), base.transform.position, null);
			}
		}
	}

	// Token: 0x06003621 RID: 13857 RVA: 0x000E30A4 File Offset: 0x000E12A4
	public void Escaped()
	{
	}

	// Token: 0x040030AF RID: 12463
	public GameObject KuroFlyAttack;

	// Token: 0x040030B0 RID: 12464
	public SoundProvider LandKillSound;

	// Token: 0x040030B1 RID: 12465
	public SoundProvider FlyKillSound;

	// Token: 0x040030B2 RID: 12466
	private CatAndMouseKuroLandZone[] m_zones;
}
