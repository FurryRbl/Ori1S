using System;
using System.Collections.Generic;
using System.IO;
using Core;
using Game;
using UnityEngine;

// Token: 0x02000252 RID: 594
public class CheatsHandler : SaveSerialize
{
	// Token: 0x06001415 RID: 5141 RVA: 0x0005B723 File Offset: 0x00059923
	public bool CanUseCheats()
	{
		return SpecialAbilityZone.IsInside;
	}

	// Token: 0x06001416 RID: 5142 RVA: 0x0005B734 File Offset: 0x00059934
	public bool IsInsideRainbowZone()
	{
		return SpecialAbilityZone.IsInsideRainbowZone;
	}

	// Token: 0x06001417 RID: 5143 RVA: 0x0005B748 File Offset: 0x00059948
	public void ChangeCharacterColor()
	{
		if (Characters.Sein)
		{
			Characters.Sein.PlatformBehaviour.Visuals.SpriteRenderer.material.color = new Color(FixedRandom.Values[0], FixedRandom.Values[1], FixedRandom.Values[2], 0.5f);
		}
	}

	// Token: 0x06001418 RID: 5144 RVA: 0x0005B7A1 File Offset: 0x000599A1
	public void MakeDashRainbow()
	{
		SeinDashAttack.RainbowDashActivated = true;
	}

	// Token: 0x06001419 RID: 5145 RVA: 0x0005B7AC File Offset: 0x000599AC
	public void TeleportOri()
	{
		if (Characters.Sein && Scenes.Manager && Scenes.Manager.CurrentScene != null && !GameController.Instance.InputLocked)
		{
			Characters.Sein.Position = Scenes.Manager.CurrentScene.PlaceholderPosition;
		}
	}

	// Token: 0x0600141A RID: 5146 RVA: 0x0005B813 File Offset: 0x00059A13
	public bool CanActivateInfiniteDoubleJumps()
	{
		return GameWorld.Instance && GameWorld.Instance.HasCompletedEverything();
	}

	// Token: 0x0600141B RID: 5147 RVA: 0x0005B830 File Offset: 0x00059A30
	public override void Awake()
	{
		CheatsHandler.Instance = this;
		List<Core.Input.InputButtonProcessor> list = new List<Core.Input.InputButtonProcessor>
		{
			Core.Input.Up,
			Core.Input.Up,
			Core.Input.Down,
			Core.Input.Down,
			Core.Input.RightStick
		};
		this.m_cheats.Add(new Cheat(list.ToArray(), new Action(this.ActivateDebugMenu), new Func<bool>(this.CanUseCheats)));
		this.m_cheats.Add(new Cheat(new Core.Input.InputButtonProcessor[]
		{
			Core.Input.Up,
			Core.Input.Right,
			Core.Input.Down,
			Core.Input.Left,
			Core.Input.RightStick,
			Core.Input.Up,
			Core.Input.Up,
			Core.Input.Jump
		}, new Action(this.TeleportOri), null));
		this.m_cheats.Add(new Cheat(new Core.Input.InputButtonProcessor[]
		{
			Core.Input.Left,
			Core.Input.Up,
			Core.Input.Right,
			Core.Input.Down,
			Core.Input.Up,
			Core.Input.Up,
			Core.Input.Up,
			Core.Input.Jump
		}, new Action(this.ChangeCharacterColor), null));
		this.m_cheats.Add(new Cheat(new Core.Input.InputButtonProcessor[]
		{
			Core.Input.Up,
			Core.Input.Up,
			Core.Input.Down,
			Core.Input.Down,
			Core.Input.Left,
			Core.Input.Right,
			Core.Input.Left,
			Core.Input.Right,
			Core.Input.SoulFlame,
			Core.Input.Jump
		}, new Action(this.MakeDashRainbow), new Func<bool>(this.IsInsideRainbowZone)));
		this.m_cheats.Add(new Cheat(new Core.Input.InputButtonProcessor[]
		{
			Core.Input.Jump,
			Core.Input.Up,
			Core.Input.Up,
			Core.Input.Down,
			Core.Input.Down,
			Core.Input.Jump,
			Core.Input.Up,
			Core.Input.Down
		}, delegate()
		{
			CheatsHandler.InfiniteDoubleJumps = true;
		}, new Func<bool>(this.CanActivateInfiniteDoubleJumps)));
		if (File.Exists("c:\\temp\\moonDebugPC.txt"))
		{
			CheatsHandler.DebugAlwaysEnabled = true;
		}
		Events.Scheduler.OnGameReset.Add(new Action(this.OnGameReset));
	}

	// Token: 0x0600141C RID: 5148 RVA: 0x0005BAA5 File Offset: 0x00059CA5
	public override void OnDestroy()
	{
		base.OnDestroy();
		Events.Scheduler.OnGameReset.Remove(new Action(this.OnGameReset));
	}

	// Token: 0x0600141D RID: 5149 RVA: 0x0005BAC8 File Offset: 0x00059CC8
	private void OnGameReset()
	{
		CheatsHandler.InfiniteDoubleJumps = false;
		SeinDashAttack.RainbowDashActivated = false;
	}

	// Token: 0x0600141E RID: 5150 RVA: 0x0005BAD6 File Offset: 0x00059CD6
	private void Update()
	{
		CheatsHandler.DebugWasEnabled = (CheatsHandler.DebugWasEnabled || this.DebugEnabled);
	}

	// Token: 0x0600141F RID: 5151 RVA: 0x0005BAF0 File Offset: 0x00059CF0
	private void FixedUpdate()
	{
		if (this.m_timer > 0f)
		{
			this.m_timer -= Time.fixedDeltaTime;
			if (this.m_timer <= 0f)
			{
				this.m_currentDebugCombinationIndex = 0;
			}
		}
		if (Core.Input.OnAnyButtonPressed)
		{
			this.m_timer = 0.6f;
			if (this.m_currentDebugCombinationIndex == 0)
			{
				foreach (Cheat cheat in this.m_cheats)
				{
					cheat.Processing = cheat.Combination[0].IsPressed;
				}
			}
			else
			{
				foreach (Cheat cheat2 in this.m_cheats)
				{
					if (cheat2.Processing)
					{
						cheat2.Processing = cheat2.Combination[this.m_currentDebugCombinationIndex].IsPressed;
						if (cheat2.Processing && cheat2.Combination.Length - 1 == this.m_currentDebugCombinationIndex)
						{
							cheat2.Processing = false;
							cheat2.PerformCheat();
						}
					}
				}
				if (!this.m_cheats.Exists((Cheat a) => a.Processing))
				{
					this.m_currentDebugCombinationIndex = 0;
				}
			}
			this.m_currentDebugCombinationIndex++;
		}
	}

	// Token: 0x06001420 RID: 5152 RVA: 0x0005BC88 File Offset: 0x00059E88
	public void ActivateDebugMenu()
	{
		this.DebugEnabled = true;
		DebugMenuB.DebugControlsEnabled = true;
		DebugMenuB.ToggleDebugMenu();
	}

	// Token: 0x06001421 RID: 5153 RVA: 0x0005BC9C File Offset: 0x00059E9C
	public void EnableCheatsEnabled()
	{
		this.DebugEnabled = true;
	}

	// Token: 0x06001422 RID: 5154 RVA: 0x0005BCA5 File Offset: 0x00059EA5
	public void DisableCheatsEnabled()
	{
		this.DebugEnabled = false;
	}

	// Token: 0x06001423 RID: 5155 RVA: 0x0005BCAE File Offset: 0x00059EAE
	public override void Serialize(Archive ar)
	{
		ar.Serialize(ref this.DebugEnabled);
	}

	// Token: 0x040011AA RID: 4522
	public static CheatsHandler Instance;

	// Token: 0x040011AB RID: 4523
	public bool DebugEnabled;

	// Token: 0x040011AC RID: 4524
	public static bool DebugWasEnabled;

	// Token: 0x040011AD RID: 4525
	public static bool DebugAlwaysEnabled;

	// Token: 0x040011AE RID: 4526
	public static bool InfiniteDoubleJumps;

	// Token: 0x040011AF RID: 4527
	private int m_currentDebugCombinationIndex;

	// Token: 0x040011B0 RID: 4528
	private List<Cheat> m_cheats = new List<Cheat>();

	// Token: 0x040011B1 RID: 4529
	private float m_timer;
}
