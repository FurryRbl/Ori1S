using System;
using System.Collections;
using System.Collections.Generic;
using Core;
using Game;
using UnityEngine;

// Token: 0x02000263 RID: 611
public class SavePedestal : SaveSerialize
{
	// Token: 0x170003A5 RID: 933
	// (get) Token: 0x0600148E RID: 5262 RVA: 0x0005CBF4 File Offset: 0x0005ADF4
	public bool IsInside
	{
		get
		{
			return this.CurrentState == SavePedestal.State.Highlighted;
		}
	}

	// Token: 0x0600148F RID: 5263 RVA: 0x0005CC00 File Offset: 0x0005AE00
	public override void Awake()
	{
		base.Awake();
		this.m_transform = base.transform;
		this.m_sceneTeleporter = base.GetComponent<SceneTeleporter>();
		SavePedestal.All.Add(this);
	}

	// Token: 0x06001490 RID: 5264 RVA: 0x0005CC36 File Offset: 0x0005AE36
	public override void OnDestroy()
	{
		base.OnDestroy();
		SavePedestal.All.Remove(this);
	}

	// Token: 0x06001491 RID: 5265 RVA: 0x0005CC4A File Offset: 0x0005AE4A
	public override void Serialize(Archive ar)
	{
		ar.Serialize(ref this.m_hasBeenUsedBefore);
	}

	// Token: 0x170003A6 RID: 934
	// (get) Token: 0x06001492 RID: 5266 RVA: 0x0005CC58 File Offset: 0x0005AE58
	private bool CanTeleport
	{
		get
		{
			return this.m_sceneTeleporter && TeleporterController.CanTeleport(this.m_sceneTeleporter.Identifier);
		}
	}

	// Token: 0x06001493 RID: 5267 RVA: 0x0005CC80 File Offset: 0x0005AE80
	public void Highlight()
	{
		if (this.OriTarget)
		{
			Characters.Ori.MoveOriToPosition(this.OriTarget.position, this.OriDuration);
		}
		if (Characters.Sein.Abilities.SpiritFlame)
		{
			Characters.Sein.Abilities.SpiritFlame.AddLock("savePedestal");
		}
		Characters.Ori.GetComponent<Rigidbody>().velocity = Vector3.zero;
		Characters.Ori.EnableHoverWobbling = false;
		if (this.OriEnterAction)
		{
			this.OriEnterAction.Perform(null);
		}
		if (this.m_hint == null)
		{
			this.m_hint = UI.Hints.Show(this.SaveAndTeleportHintMessage, HintLayer.HintZone, 3f);
		}
		if (this.OnOriEnter)
		{
			Sound.Play(this.OnOriEnter.GetSound(null), base.transform.position, null);
		}
		if (this.m_sceneTeleporter)
		{
			TeleporterController.Activate(this.m_sceneTeleporter.Identifier);
		}
	}

	// Token: 0x06001494 RID: 5268 RVA: 0x0005CD9C File Offset: 0x0005AF9C
	public void Unhighlight()
	{
		this.m_used = false;
		Characters.Ori.ChangeState(Ori.State.Hovering);
		Characters.Ori.EnableHoverWobbling = true;
		if (Characters.Sein.Abilities.SpiritFlame)
		{
			Characters.Sein.Abilities.SpiritFlame.RemoveLock("savePedestal");
		}
		if (this.OriExitAction)
		{
			this.OriExitAction.Perform(null);
		}
		if (this.m_hint)
		{
			this.m_hint.HideMessageScreen();
		}
		if (this.OnOriExit)
		{
			Sound.Play(this.OnOriExit.GetSound(null), base.transform.position, null);
		}
	}

	// Token: 0x170003A7 RID: 935
	// (get) Token: 0x06001495 RID: 5269 RVA: 0x0005CE60 File Offset: 0x0005B060
	public bool OriHasTargets
	{
		get
		{
			SeinSpiritFlameTargetting spiritFlameTargetting = Characters.Sein.Abilities.SpiritFlameTargetting;
			return spiritFlameTargetting && spiritFlameTargetting.ClosestAttackables.Count > 0;
		}
	}

	// Token: 0x170003A8 RID: 936
	// (get) Token: 0x06001496 RID: 5270 RVA: 0x0005CE98 File Offset: 0x0005B098
	public float DistanceToSein
	{
		get
		{
			return Vector3.Distance(this.m_transform.position, Characters.Sein.Position);
		}
	}

	// Token: 0x06001497 RID: 5271 RVA: 0x0005CEB4 File Offset: 0x0005B0B4
	public void FixedUpdate()
	{
		if (Characters.Sein == null)
		{
			return;
		}
		if (Characters.Sein.IsSuspended)
		{
			return;
		}
		SavePedestal.State currentState = this.CurrentState;
		if (currentState != SavePedestal.State.Normal)
		{
			if (currentState == SavePedestal.State.Highlighted)
			{
				if ((!Characters.Sein.Controller.IsPlayingAnimation && this.DistanceToSein > this.Radius) || this.OriHasTargets)
				{
					this.Unhighlight();
					this.CurrentState = SavePedestal.State.Normal;
				}
				if (Characters.Sein.Controller.CanMove && Characters.Sein.PlatformBehaviour.PlatformMovement.IsOnGround)
				{
					if (Core.Input.SpiritFlame.OnPressed && !this.m_used)
					{
						this.SaveOnPedestal();
					}
					else if (Core.Input.SoulFlame.OnPressedNotUsed && !Core.Input.Cancel.Used)
					{
						if (this.m_hint)
						{
							this.m_hint.HideMessageScreen();
						}
						Core.Input.SoulFlame.Used = true;
						UI.Menu.ShowSkillTree();
					}
					else if (Core.Input.SpiritFlame.OnPressed && this.m_used)
					{
						if (this.OnSaveSecondTime)
						{
							Sound.Play(this.OnSaveSecondTime.GetSound(null), base.transform.position, null);
						}
					}
					else if (Core.Input.Bash.OnPressed && WorldMapUI.IsReady)
					{
						if (this.CanTeleport)
						{
							this.TeleportOnPedestal();
						}
						else
						{
							UI.Hints.Show(this.CantTeleportMessage, HintLayer.Gameplay, 2f);
						}
					}
				}
			}
		}
		else if (this.DistanceToSein < this.Radius && !this.OriHasTargets)
		{
			this.Highlight();
			this.CurrentState = SavePedestal.State.Highlighted;
		}
	}

	// Token: 0x06001498 RID: 5272 RVA: 0x0005D0A0 File Offset: 0x0005B2A0
	private void TeleportOnPedestal()
	{
		if (this.m_hint)
		{
			this.m_hint.HideMessageScreen();
		}
		this.MarkAsUsed();
		Characters.Sein.PlatformBehaviour.PlatformMovement.PositionX = base.transform.position.x;
		TeleporterController.Show(this.m_sceneTeleporter.Identifier);
	}

	// Token: 0x06001499 RID: 5273 RVA: 0x0005D108 File Offset: 0x0005B308
	public void OnBeginTeleporting()
	{
		if (this.TeleportEffect)
		{
			this.TeleportEffect.gameObject.SetActive(true);
			this.TeleportEffect.Initialize();
			this.TeleportEffect.AnimatorDriver.Restart();
		}
	}

	// Token: 0x0600149A RID: 5274 RVA: 0x0005D151 File Offset: 0x0005B351
	public void OnFinishedTeleporting()
	{
		if (this.TeleportEffect)
		{
			this.TeleportEffect.gameObject.SetActive(false);
		}
	}

	// Token: 0x0600149B RID: 5275 RVA: 0x0005D174 File Offset: 0x0005B374
	public void MarkAsUsed()
	{
		if (!this.m_hasBeenUsedBefore)
		{
			this.m_hasBeenUsedBefore = true;
			AchievementsLogic.Instance.OnSavePedestalUsedFirstTime();
		}
	}

	// Token: 0x0600149C RID: 5276 RVA: 0x0005D194 File Offset: 0x0005B394
	private void SaveOnPedestal()
	{
		if (this.m_hint)
		{
			this.m_hint.HideMessageScreen();
		}
		this.m_used = true;
		this.MarkAsUsed();
		if (Characters.Sein.Abilities.Carry && Characters.Sein.Abilities.Carry.CurrentCarryable != null)
		{
			Characters.Sein.Abilities.Carry.CurrentCarryable.Drop();
		}
		if (this.OnOpenedAction)
		{
			this.OnOpenedAction.Perform(null);
		}
		base.StartCoroutine(this.MoveSeinToCenterSmoothly());
	}

	// Token: 0x0600149D RID: 5277 RVA: 0x0005D240 File Offset: 0x0005B440
	public IEnumerator MoveSeinToCenterSmoothly()
	{
		PlatformMovement seinPlatformMovement = Characters.Sein.PlatformBehaviour.PlatformMovement;
		for (int i = 0; i < 10; i++)
		{
			seinPlatformMovement.PositionX = Mathf.Lerp(seinPlatformMovement.PositionX, base.transform.position.x, 0.2f);
			yield return new WaitForFixedUpdate();
		}
		seinPlatformMovement.PositionX = base.transform.position.x;
		yield break;
	}

	// Token: 0x040011E2 RID: 4578
	public static List<SavePedestal> All = new List<SavePedestal>();

	// Token: 0x040011E3 RID: 4579
	public Transform OriTarget;

	// Token: 0x040011E4 RID: 4580
	public float Radius = 2f;

	// Token: 0x040011E5 RID: 4581
	public float OriDuration = 1f;

	// Token: 0x040011E6 RID: 4582
	private Transform m_transform;

	// Token: 0x040011E7 RID: 4583
	private MessageBox m_hint;

	// Token: 0x040011E8 RID: 4584
	public MessageProvider CantTeleportMessage;

	// Token: 0x040011E9 RID: 4585
	public MessageProvider SaveAndTeleportHintMessage;

	// Token: 0x040011EA RID: 4586
	public SoundProvider OnOriEnter;

	// Token: 0x040011EB RID: 4587
	public SoundProvider OnOriExit;

	// Token: 0x040011EC RID: 4588
	public SoundProvider OnSaveSecondTime;

	// Token: 0x040011ED RID: 4589
	private bool m_hasBeenUsedBefore;

	// Token: 0x040011EE RID: 4590
	private SceneTeleporter m_sceneTeleporter;

	// Token: 0x040011EF RID: 4591
	public TimelineSequence TeleportEffect;

	// Token: 0x040011F0 RID: 4592
	public ActionMethod OriEnterAction;

	// Token: 0x040011F1 RID: 4593
	public ActionMethod OriExitAction;

	// Token: 0x040011F2 RID: 4594
	public ActionMethod OnOpenedAction;

	// Token: 0x040011F3 RID: 4595
	private bool m_used;

	// Token: 0x040011F4 RID: 4596
	public SavePedestal.State CurrentState;

	// Token: 0x02000929 RID: 2345
	public enum State
	{
		// Token: 0x04002EDE RID: 11998
		Normal,
		// Token: 0x04002EDF RID: 11999
		Highlighted
	}
}
