using System;
using Core;
using Game;
using UnityEngine;

// Token: 0x02000077 RID: 119
public class SeinEnergy : SaveSerialize
{
	// Token: 0x0600051A RID: 1306 RVA: 0x000145A8 File Offset: 0x000127A8
	public void SetCurrent(float current)
	{
		this.Current = current;
		this.MinVisual = this.Current;
		this.MaxVisual = this.Current;
	}

	// Token: 0x0600051B RID: 1307 RVA: 0x000145CC File Offset: 0x000127CC
	public void NotifyOutOfEnergy()
	{
		UI.SeinUI.ShakeEnergyOrbBar();
		Sound.Play(this.OutOfEnergySound.GetSound(null), base.transform.position, null);
	}

	// Token: 0x0600051C RID: 1308 RVA: 0x00014601 File Offset: 0x00012801
	public bool CanAfford(float amount)
	{
		return this.Current >= amount;
	}

	// Token: 0x17000141 RID: 321
	// (get) Token: 0x0600051D RID: 1309 RVA: 0x0001460F File Offset: 0x0001280F
	public float VisualMin
	{
		get
		{
			return this.MinVisual / this.Max;
		}
	}

	// Token: 0x17000142 RID: 322
	// (get) Token: 0x0600051E RID: 1310 RVA: 0x0001461E File Offset: 0x0001281E
	public float VisualMax
	{
		get
		{
			return this.MaxVisual / this.Max;
		}
	}

	// Token: 0x0600051F RID: 1311 RVA: 0x0001462D File Offset: 0x0001282D
	public void Gain(float amount)
	{
		this.Current += amount;
		if (this.Current > this.Max)
		{
			this.Current = this.Max;
		}
		this.MaxVisual = this.Current;
	}

	// Token: 0x06000520 RID: 1312 RVA: 0x00014666 File Offset: 0x00012866
	public void Spend(float amount)
	{
		this.Current -= amount;
		if (this.Current < 0f)
		{
			this.Current = 0f;
		}
		this.MinVisual = this.Current;
	}

	// Token: 0x06000521 RID: 1313 RVA: 0x000146A0 File Offset: 0x000128A0
	public override void Serialize(Archive ar)
	{
		ar.Serialize(ref this.Current);
		ar.Serialize(ref this.Max);
		if (ar.Reading)
		{
			this.MinVisual = (this.MaxVisual = this.Current);
		}
	}

	// Token: 0x17000143 RID: 323
	// (get) Token: 0x06000522 RID: 1314 RVA: 0x000146E5 File Offset: 0x000128E5
	public bool EnergyActive
	{
		get
		{
			return this.Max > 0f;
		}
	}

	// Token: 0x17000144 RID: 324
	// (get) Token: 0x06000523 RID: 1315 RVA: 0x000146F4 File Offset: 0x000128F4
	public float VisualMaxNormalized
	{
		get
		{
			return this.MaxVisual / this.Max;
		}
	}

	// Token: 0x17000145 RID: 325
	// (get) Token: 0x06000524 RID: 1316 RVA: 0x00014703 File Offset: 0x00012903
	public float VisualMinNormalized
	{
		get
		{
			return this.MinVisual / this.Max;
		}
	}

	// Token: 0x17000146 RID: 326
	// (get) Token: 0x06000525 RID: 1317 RVA: 0x00014712 File Offset: 0x00012912
	public object EnergyUpgradesCollected
	{
		get
		{
			return this.Max;
		}
	}

	// Token: 0x06000526 RID: 1318 RVA: 0x0001471F File Offset: 0x0001291F
	public void Update()
	{
		this.MinVisual = Mathf.MoveTowards(this.MinVisual, this.Current, Time.deltaTime);
		this.MaxVisual = Mathf.MoveTowards(this.MaxVisual, this.Current, Time.deltaTime);
	}

	// Token: 0x06000527 RID: 1319 RVA: 0x00014759 File Offset: 0x00012959
	public void RestoreAllEnergy()
	{
		this.Current = this.Max;
	}

	// Token: 0x04000411 RID: 1041
	public float MinVisual;

	// Token: 0x04000412 RID: 1042
	public float MaxVisual;

	// Token: 0x04000413 RID: 1043
	public float Current;

	// Token: 0x04000414 RID: 1044
	public float Max = 3f;

	// Token: 0x04000415 RID: 1045
	public SoundProvider OutOfEnergySound;
}
