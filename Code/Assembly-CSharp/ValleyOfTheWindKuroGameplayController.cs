using System;
using Core;
using Game;
using UnityEngine;

// Token: 0x020009EA RID: 2538
public class ValleyOfTheWindKuroGameplayController : SaveSerialize, ISuspendable
{
	// Token: 0x06003723 RID: 14115 RVA: 0x000E75EF File Offset: 0x000E57EF
	public override void Awake()
	{
		SuspensionManager.Register(this);
		Events.Scheduler.OnGameSerializeLoad.Add(new Action(this.OnGameSerializeLoad));
		base.Awake();
	}

	// Token: 0x06003724 RID: 14116 RVA: 0x000E7618 File Offset: 0x000E5818
	public override void OnDestroy()
	{
		SuspensionManager.Unregister(this);
		Events.Scheduler.OnGameSerializeLoad.Remove(new Action(this.OnGameSerializeLoad));
		base.OnDestroy();
	}

	// Token: 0x06003725 RID: 14117 RVA: 0x000E7641 File Offset: 0x000E5841
	public void OnGameSerializeLoad()
	{
		this.m_hasBeenSeen = false;
		this.m_hasKilledPlayer = false;
		this.m_time = 0f;
	}

	// Token: 0x06003726 RID: 14118 RVA: 0x000E765C File Offset: 0x000E585C
	public void FixedUpdate()
	{
		bool flag = false;
		foreach (ValleyOfTheWindKuroDeathZone valleyOfTheWindKuroDeathZone in ValleyOfTheWindKuroDeathZone.All)
		{
			if (valleyOfTheWindKuroDeathZone.Bounds.Contains(Characters.Current.Position))
			{
				flag = true;
				Characters.Sein.SoulFlame.LockSoulFlame = true;
				break;
			}
		}
		if (flag && !this.m_hasBeenSeen)
		{
			this.m_hasBeenSeen = true;
			UnityEngine.Object.Instantiate(this.FlyingKuro, Characters.Sein.Position, Quaternion.identity);
			if (this.AttackSound)
			{
				Sound.Play(this.AttackSound.GetSound(null), base.transform.position, null);
			}
		}
		if (this.m_hasBeenSeen)
		{
			this.m_time += Time.deltaTime;
			if (this.m_time > this.DeathDelay && !this.m_hasKilledPlayer)
			{
				this.m_hasKilledPlayer = true;
				IDamageReciever damageReciever = Characters.Sein.gameObject.FindComponentInChildren<IDamageReciever>();
				if (damageReciever != null)
				{
					Damage damage = new Damage(10000f, Vector3.up, Characters.Sein.Position, DamageType.Lava, base.gameObject);
					damageReciever.OnRecieveDamage(damage);
				}
			}
		}
	}

	// Token: 0x17000883 RID: 2179
	// (get) Token: 0x06003727 RID: 14119 RVA: 0x000E77C8 File Offset: 0x000E59C8
	// (set) Token: 0x06003728 RID: 14120 RVA: 0x000E77D0 File Offset: 0x000E59D0
	public bool IsSuspended { get; set; }

	// Token: 0x06003729 RID: 14121 RVA: 0x000E77D9 File Offset: 0x000E59D9
	public override void Serialize(Archive ar)
	{
		ar.Serialize(ref this.m_time);
	}

	// Token: 0x0400321A RID: 12826
	public float DeathDelay = 0.5f;

	// Token: 0x0400321B RID: 12827
	public GameObject FlyingKuro;

	// Token: 0x0400321C RID: 12828
	public SoundProvider AttackSound;

	// Token: 0x0400321D RID: 12829
	private float m_time;

	// Token: 0x0400321E RID: 12830
	private bool m_hasKilledPlayer;

	// Token: 0x0400321F RID: 12831
	private bool m_hasBeenSeen;
}
