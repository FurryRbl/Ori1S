using System;
using Game;
using UnityEngine;

// Token: 0x02000080 RID: 128
public class SeinUI : MonoBehaviour
{
	// Token: 0x06000595 RID: 1429 RVA: 0x000162A8 File Offset: 0x000144A8
	public void OnSoulFlameReady()
	{
		Game.UI.Hints.Show(this.SoulFlameReadyMessage, HintLayer.SoulFlame, this.SoulFlameReadyMessageDuration);
		this.ShakeSoulFlame();
	}

	// Token: 0x06000596 RID: 1430 RVA: 0x000162C3 File Offset: 0x000144C3
	public void Awake()
	{
		Game.UI.SeinUI = this;
		Game.Checkpoint.Events.OnPostRestore.Add(new Action(this.OnRestoreCheckpoint));
		Events.Scheduler.OnGameReset.Add(new Action(this.OnGameReset));
	}

	// Token: 0x06000597 RID: 1431 RVA: 0x000162FC File Offset: 0x000144FC
	public void OnDestroy()
	{
		Game.Checkpoint.Events.OnPostRestore.Remove(new Action(this.OnRestoreCheckpoint));
		Events.Scheduler.OnGameReset.Remove(new Action(this.OnGameReset));
	}

	// Token: 0x06000598 RID: 1432 RVA: 0x0001632F File Offset: 0x0001452F
	public void OnRestoreCheckpoint()
	{
		Game.UI.SeinUI.ShowUI = true;
	}

	// Token: 0x06000599 RID: 1433 RVA: 0x0001633C File Offset: 0x0001453C
	public void OnGameReset()
	{
		Game.UI.SeinUI.ShowUI = true;
	}

	// Token: 0x0600059A RID: 1434 RVA: 0x00016349 File Offset: 0x00014549
	public void Start()
	{
		this.TransparencyAnimator.Initialize();
		this.TransparencyAnimator.SampleValue(0f, true);
	}

	// Token: 0x0600059B RID: 1435 RVA: 0x00016367 File Offset: 0x00014567
	public void SetTransparency(float amount)
	{
		this.FaderTransparency = amount;
	}

	// Token: 0x0600059C RID: 1436 RVA: 0x00016370 File Offset: 0x00014570
	public void ShakeExperienceBar()
	{
		this.ExperienceOrbShake.Restart();
	}

	// Token: 0x0600059D RID: 1437 RVA: 0x0001637D File Offset: 0x0001457D
	public void ShakeEnergyOrbBar()
	{
		this.EnergyOrbShake.Restart();
	}

	// Token: 0x0600059E RID: 1438 RVA: 0x0001638A File Offset: 0x0001458A
	public void ShakeSoulFlame()
	{
		this.SoulFlameNotReadyShake.Restart();
	}

	// Token: 0x0600059F RID: 1439 RVA: 0x00016397 File Offset: 0x00014597
	public void ShakeKeystones()
	{
		if (!this.KeystoneUI.activeSelf)
		{
			this.KeystoneUI.SetActive(true);
		}
		this.KeystonesShake.Restart();
	}

	// Token: 0x060005A0 RID: 1440 RVA: 0x000163C0 File Offset: 0x000145C0
	public void ShakeMapstones()
	{
		if (!this.MapStoneUI.activeSelf)
		{
			this.MapStoneUI.SetActive(true);
		}
		this.MapStonesShake.Restart();
	}

	// Token: 0x060005A1 RID: 1441 RVA: 0x000163E9 File Offset: 0x000145E9
	public void ShakeHealthbar()
	{
		this.HealthShake.Restart();
	}

	// Token: 0x060005A2 RID: 1442 RVA: 0x000163F6 File Offset: 0x000145F6
	public void SetActive(GameObject go, bool activate)
	{
		if (go.activeSelf != activate)
		{
			go.SetActive(activate);
		}
	}

	// Token: 0x060005A3 RID: 1443 RVA: 0x0001640C File Offset: 0x0001460C
	public void FixedUpdate()
	{
		bool flag = this.ShowUI && !Letterbox.ShowLetterboxes;
		SeinCharacter sein = Characters.Sein;
		bool flag2 = sein != null;
		if (flag2)
		{
			this.SetActive(this.EnergyBar, sein.Energy.EnergyActive);
		}
		if (this.KeystoneUI != null && sein)
		{
			bool activate = sein.Inventory.HasKeystones || (Characters.Ori && Characters.Ori.InsideDoor);
			this.SetActive(this.KeystoneUI, activate);
		}
		if (this.MapStoneUI != null && sein)
		{
			bool activate2 = sein.Inventory.HasMapstones || (Characters.Ori && Characters.Ori.InsideMapstone);
			this.SetActive(this.MapStoneUI, activate2);
		}
		bool flag3 = !SeinUI.DebugHideUI && !Game.UI.MainMenuVisible;
		if (flag3)
		{
			this.m_opacity = Mathf.Clamp01(this.m_opacity + Time.deltaTime / this.FadeTime * (float)((!flag) ? -1 : 1));
		}
		if (this.m_opacity < 0.0001f)
		{
			flag3 = false;
		}
		float num = this.m_opacity * this.FaderTransparency;
		if (this.m_lastOpacity != num)
		{
			this.m_lastOpacity = num;
			this.TransparencyAnimator.SampleValue(num, true);
		}
		this.SetActive(this.UI, flag3);
		if (!flag3)
		{
			this.m_opacity = 0f;
		}
		if (flag2)
		{
			this.SetActive(this.SoulFlameFull, sein.SoulFlame.CooldownRemaining == 0f);
			this.SetActive(this.SoulFlameUIFlame, sein.SoulFlame.ShowFlameOnUI);
			if (this.EnergyBarPulse.AnimatorDriver.IsPlaying && sein.Energy.Current > 0f)
			{
				this.EnergyBarPulse.AnimatorDriver.Stop();
			}
			if (!this.EnergyBarPulse.AnimatorDriver.IsPlaying && sein.Energy.Current == 0f)
			{
				this.EnergyBarPulse.AnimatorDriver.Restart();
			}
			if (this.HealthPulse.AnimatorDriver.IsPlaying && sein.Mortality.Health.Amount > 4f)
			{
				this.HealthPulse.AnimatorDriver.Stop();
			}
			if (!this.HealthPulse.AnimatorDriver.IsPlaying && sein.Mortality.Health.Amount <= 4f)
			{
				this.HealthPulse.AnimatorDriver.Restart();
			}
		}
	}

	// Token: 0x04000451 RID: 1105
	public TransparencyAnimator TransparencyAnimator;

	// Token: 0x04000452 RID: 1106
	public GameObject UI;

	// Token: 0x04000453 RID: 1107
	public LegacyAnimator ExperienceOrbShake;

	// Token: 0x04000454 RID: 1108
	public LegacyAnimator EnergyOrbShake;

	// Token: 0x04000455 RID: 1109
	public LegacyAnimator KeystonesShake;

	// Token: 0x04000456 RID: 1110
	public LegacyAnimator MapStonesShake;

	// Token: 0x04000457 RID: 1111
	public LegacyAnimator HealthShake;

	// Token: 0x04000458 RID: 1112
	public LegacyAnimator SoulFlameNotReadyShake;

	// Token: 0x04000459 RID: 1113
	public GameObject EnergyBar;

	// Token: 0x0400045A RID: 1114
	public GameObject KeystoneUI;

	// Token: 0x0400045B RID: 1115
	public GameObject MapStoneUI;

	// Token: 0x0400045C RID: 1116
	public GameObject SoulFlameFull;

	// Token: 0x0400045D RID: 1117
	public GameObject SoulFlameUI;

	// Token: 0x0400045E RID: 1118
	public GameObject SoulFlameUIFlame;

	// Token: 0x0400045F RID: 1119
	public MessageProvider SoulFlameReadyMessage;

	// Token: 0x04000460 RID: 1120
	public float SoulFlameReadyMessageDuration = 1.5f;

	// Token: 0x04000461 RID: 1121
	public BaseAnimator EnergyBarPulse;

	// Token: 0x04000462 RID: 1122
	public BaseAnimator HealthPulse;

	// Token: 0x04000463 RID: 1123
	public float FaderTransparency = 1f;

	// Token: 0x04000464 RID: 1124
	private float m_opacity;

	// Token: 0x04000465 RID: 1125
	public float FadeTime = 1f;

	// Token: 0x04000466 RID: 1126
	public bool ShowUI = true;

	// Token: 0x04000467 RID: 1127
	public static bool DebugHideUI;

	// Token: 0x04000468 RID: 1128
	private float m_lastOpacity = float.NaN;
}
