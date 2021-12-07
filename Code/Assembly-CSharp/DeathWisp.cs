using System;
using Game;
using UnityEngine;

// Token: 0x0200042A RID: 1066
public class DeathWisp : MonoBehaviour
{
	// Token: 0x06001DB4 RID: 7604 RVA: 0x000832E0 File Offset: 0x000814E0
	public void Awake()
	{
		this.m_transform = base.transform;
	}

	// Token: 0x17000502 RID: 1282
	// (get) Token: 0x06001DB5 RID: 7605 RVA: 0x000832EE File Offset: 0x000814EE
	public Vector3 Position
	{
		get
		{
			return this.m_transform.position;
		}
	}

	// Token: 0x06001DB6 RID: 7606 RVA: 0x000832FC File Offset: 0x000814FC
	public void FixedUpdate()
	{
		if (this.m_collected)
		{
			return;
		}
		SeinCharacter sein = Characters.Sein;
		if (!sein)
		{
			return;
		}
		if (sein.Controller.CanMove && Vector3.Distance(this.Position, sein.Position) < this.Radius)
		{
			this.m_collected = true;
			this.Disappear.Initialize();
			this.Disappear.AnimatorDriver.Restart();
			DeathWispsManager.Instance.Collected = this.DeathInfo;
			if (DeathWispsManager.Instance)
			{
				DeathWispsManager.Instance.CollectWispAction.Perform(null);
			}
			if (this.CollectionEffect)
			{
				InstantiateUtility.Instantiate(this.CollectionEffect, this.Position, Quaternion.identity);
			}
			UI.SeinUI.ShakeExperienceBar();
			int experienceNeedForNextLevel = sein.Level.ExperienceNeedForNextLevel;
			sein.Level.GainExperience(experienceNeedForNextLevel);
			SeinDeathsManager.Instance.Deaths.Remove(this.DeathInfo);
		}
	}

	// Token: 0x06001DB7 RID: 7607 RVA: 0x00083403 File Offset: 0x00081603
	public void Collect()
	{
	}

	// Token: 0x04001998 RID: 6552
	public DeathInformation DeathInfo;

	// Token: 0x04001999 RID: 6553
	public BaseAnimator Disappear;

	// Token: 0x0400199A RID: 6554
	public float Radius = 5f;

	// Token: 0x0400199B RID: 6555
	public GameObject CollectionEffect;

	// Token: 0x0400199C RID: 6556
	private Transform m_transform;

	// Token: 0x0400199D RID: 6557
	private bool m_collected;
}
