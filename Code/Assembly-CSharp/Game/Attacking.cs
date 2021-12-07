using System;
using Core;
using UnityEngine;

namespace Game
{
	// Token: 0x0200043B RID: 1083
	public static class Attacking
	{
		// Token: 0x0200043C RID: 1084
		public static class DamageDisplayText
		{
			// Token: 0x17000514 RID: 1300
			// (get) Token: 0x06001E29 RID: 7721 RVA: 0x00084B18 File Offset: 0x00082D18
			public static GameObject DamageText
			{
				get
				{
					Attacking.DamageDisplayText.LoadDamageText();
					return Attacking.DamageDisplayText.m_damageText;
				}
			}

			// Token: 0x06001E2A RID: 7722 RVA: 0x00084B24 File Offset: 0x00082D24
			public static void LoadDamageText()
			{
				if (Attacking.DamageDisplayText.m_damageText == null)
				{
					Attacking.DamageDisplayText.m_damageText = (GameObject)Resources.Load("attacking/damageText", typeof(GameObject));
				}
			}

			// Token: 0x06001E2B RID: 7723 RVA: 0x00084B60 File Offset: 0x00082D60
			public static DamageText Create(Damage damage, Transform target)
			{
				if (!GameSettings.Instance.DamageTextEnabled)
				{
					return null;
				}
				if (damage.Amount < 100f)
				{
					GameObject gameObject = (GameObject)InstantiateUtility.Instantiate(Attacking.DamageDisplayText.DamageText);
					DamageText component = gameObject.GetComponent<DamageText>();
					gameObject.transform.position += target.position + Vector3.up * 0.5f;
					component.ChangeText(damage);
					return component;
				}
				return null;
			}

			// Token: 0x040019FD RID: 6653
			private static GameObject m_damageText;
		}

		// Token: 0x02000532 RID: 1330
		public static class DamageEffect
		{
			// Token: 0x06002327 RID: 8999 RVA: 0x0009A080 File Offset: 0x00098280
			public static GameObject Create(Damage damage, Transform target, DamageBasedPrefabProvider effectProvider)
			{
				if (effectProvider)
				{
					GameObject gameObject = effectProvider.Prefab(new DamageContext(damage));
					if (gameObject != null)
					{
						GameObject gameObject2 = (GameObject)InstantiateUtility.Instantiate(gameObject, target.position, Quaternion.identity);
						damage.DealToComponents(gameObject2);
						return gameObject2;
					}
				}
				return null;
			}

			// Token: 0x06002328 RID: 9000 RVA: 0x0009A0D8 File Offset: 0x000982D8
			public static GameObject CreateRotated(Damage damage, Transform target, DamageBasedPrefabProvider effectProvider)
			{
				if (effectProvider)
				{
					GameObject gameObject = effectProvider.Prefab(new DamageContext(damage));
					if (gameObject != null)
					{
						GameObject target2 = (GameObject)InstantiateUtility.Instantiate(gameObject, target.position, Quaternion.identity);
						damage.DealToComponents(target2);
						gameObject.transform.eulerAngles = new Vector3(0f, 0f, MoonMath.Angle.AngleFromVector(damage.Force));
						return gameObject;
					}
				}
				return null;
			}
		}

		// Token: 0x02000533 RID: 1331
		public static class DamageSound
		{
			// Token: 0x06002329 RID: 9001 RVA: 0x0009A154 File Offset: 0x00098354
			public static SoundPlayer Play(Damage damage, Transform target, DamageBasedSoundProvider soundProvider)
			{
				if (soundProvider == null)
				{
					return null;
				}
				SoundDescriptor soundForDamage = soundProvider.GetSoundForDamage(damage);
				return Sound.Play(soundForDamage, target.position, null);
			}
		}
	}
}
