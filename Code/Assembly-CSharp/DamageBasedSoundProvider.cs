using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020001D0 RID: 464
public class DamageBasedSoundProvider : MonoBehaviour
{
	// Token: 0x060010C0 RID: 4288 RVA: 0x0004C898 File Offset: 0x0004AA98
	public bool Match(Damage damage, SoundDamageType soundDamageType)
	{
		switch (soundDamageType)
		{
		case SoundDamageType.Water:
			return damage.Type == DamageType.Water;
		case SoundDamageType.Lava:
			return damage.Type == DamageType.Lava;
		case SoundDamageType.Ice:
			return damage.Type == DamageType.Ice;
		case SoundDamageType.Spikes:
			return damage.Type == DamageType.Spikes;
		case SoundDamageType.Laser:
			return damage.Type == DamageType.Laser;
		case SoundDamageType.Projectile:
			return damage.Type == DamageType.Projectile;
		case SoundDamageType.Acid:
			return damage.Type == DamageType.Acid;
		case SoundDamageType.SlugSpike:
			return damage.Type == DamageType.SlugSpike;
		case SoundDamageType.RegularSpiritFlame:
			if (damage.Type == DamageType.SpiritFlame)
			{
				SpiritFlameProjectile component = damage.Sender.GetComponent<SpiritFlameProjectile>();
				if (component.GetType() == typeof(StandardSpiritFlameProjectile))
				{
					return true;
				}
			}
			return false;
		case SoundDamageType.RegularSpiritFlameThirdShot:
			if (damage.Type == DamageType.SpiritFlame)
			{
				SpiritFlameProjectile component2 = damage.Sender.GetComponent<SpiritFlameProjectile>();
				if (component2.GetType() == typeof(StandardSpiritFlameThirdShotProjectile))
				{
					return true;
				}
			}
			return false;
		case SoundDamageType.IceSpiritFlame:
			if (damage.Type == DamageType.SpiritFlame)
			{
				SpiritFlameProjectile component3 = damage.Sender.GetComponent<SpiritFlameProjectile>();
				if (component3.GetType() == typeof(IceSpiritFlameProjectile))
				{
					return true;
				}
			}
			return false;
		case SoundDamageType.Bash:
			return damage.Type == DamageType.Bash;
		case SoundDamageType.Grenade:
			return damage.Type == DamageType.Grenade;
		case SoundDamageType.GrenadeSplatter:
			return damage.Type == DamageType.GrenadeSplatter;
		case SoundDamageType.UndergroundSpiritFlame:
			return false;
		case SoundDamageType.Dash:
			return false;
		case SoundDamageType.Stomp:
			return damage.Type == DamageType.Stomp;
		case SoundDamageType.StompBlast:
			return damage.Type == DamageType.StompBlast;
		case SoundDamageType.NightBerryDied:
			return damage.Type == DamageType.NightBerryDied;
		}
		return false;
	}

	// Token: 0x060010C1 RID: 4289 RVA: 0x0004CA30 File Offset: 0x0004AC30
	public SoundDescriptor GetSoundForDamage(Damage damage)
	{
		foreach (DamageTypeSoundPair damageTypeSoundPair in this.SoundPairs)
		{
			if (this.Match(damage, damageTypeSoundPair.DamageType))
			{
				return damageTypeSoundPair.IndependantSoundProvider.GetSound(null);
			}
		}
		if (this.DefaultSoundProvider)
		{
			return this.DefaultSoundProvider.GetSound(null);
		}
		return null;
	}

	// Token: 0x04000E40 RID: 3648
	public List<DamageTypeSoundPair> SoundPairs;

	// Token: 0x04000E41 RID: 3649
	public SoundProvider DefaultSoundProvider;
}
