using System;
using Game;
using UnityEngine;

// Token: 0x0200053E RID: 1342
public class DamageSoundSource : MonoBehaviour, IDamageReciever
{
	// Token: 0x06002342 RID: 9026 RVA: 0x0009A424 File Offset: 0x00098624
	public void OnRecieveDamage(Damage damage)
	{
		if (this.OneSoundOnly && this.m_lastDamageSound)
		{
			this.m_lastDamageSound.FadeOut(1f, true);
			UberPoolManager.Instance.RemoveOnDestroyed(this.m_lastDamageSound.gameObject);
			this.m_lastDamageSound = null;
		}
		this.m_lastDamageSound = Attacking.DamageSound.Play(damage, base.transform, this.DamageSound);
		if (this.m_lastDamageSound)
		{
			UberPoolManager.Instance.AddOnDestroyed(this.m_lastDamageSound.gameObject, delegate
			{
				this.m_lastDamageSound = null;
			});
		}
	}

	// Token: 0x04001DA9 RID: 7593
	public DamageBasedSoundProvider DamageSound;

	// Token: 0x04001DAA RID: 7594
	private SoundPlayer m_lastDamageSound;

	// Token: 0x04001DAB RID: 7595
	public bool OneSoundOnly;
}
