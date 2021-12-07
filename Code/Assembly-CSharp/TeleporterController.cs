using System;
using System.Collections.Generic;
using Core;
using Game;
using UnityEngine;

// Token: 0x020004AA RID: 1194
public class TeleporterController : SaveSerialize, ISuspendable
{
	// Token: 0x06002089 RID: 8329 RVA: 0x0008D89D File Offset: 0x0008BA9D
	private void Nullify()
	{
		this.m_teleportingStartSound = null;
	}

	// Token: 0x0600208A RID: 8330 RVA: 0x0008D8A8 File Offset: 0x0008BAA8
	public override void Serialize(Archive ar)
	{
		foreach (GameMapTeleporter gameMapTeleporter in this.Teleporters)
		{
			ar.Serialize(ref gameMapTeleporter.Activated);
		}
	}

	// Token: 0x0600208B RID: 8331 RVA: 0x0008D908 File Offset: 0x0008BB08
	public static bool CanTeleport(string ignoreIdentifier)
	{
		if (TeleporterController.Instance)
		{
			for (int i = 0; i < TeleporterController.Instance.Teleporters.Count; i++)
			{
				GameMapTeleporter gameMapTeleporter = TeleporterController.Instance.Teleporters[i];
				if (!(gameMapTeleporter.Identifier == ignoreIdentifier))
				{
					if (gameMapTeleporter.Activated)
					{
						return true;
					}
				}
			}
		}
		return false;
	}

	// Token: 0x0600208C RID: 8332 RVA: 0x0008D979 File Offset: 0x0008BB79
	public override void Awake()
	{
		base.Awake();
		TeleporterController.Instance = this;
		SuspensionManager.Register(this);
		Events.Scheduler.OnGameReset.Add(new Action(this.OnGameReset));
		this.DontTeleportForAnimationTesting = false;
	}

	// Token: 0x0600208D RID: 8333 RVA: 0x0008D9AF File Offset: 0x0008BBAF
	public override void OnDestroy()
	{
		base.OnDestroy();
		TeleporterController.Instance = null;
		SuspensionManager.Unregister(this);
		Events.Scheduler.OnGameReset.Remove(new Action(this.OnGameReset));
	}

	// Token: 0x0600208E RID: 8334 RVA: 0x0008D9E0 File Offset: 0x0008BBE0
	public void OnGameReset()
	{
		for (int i = 0; i < TeleporterController.Instance.Teleporters.Count; i++)
		{
			TeleporterController.Instance.Teleporters[i].Activated = false;
		}
		this.m_isTeleporting = false;
		this.m_isBlooming = false;
		if (!InstantiateUtility.IsDestroyed(this.m_teleportingStartSound))
		{
			this.m_teleportingStartSound.FadeOut(0.1f, true);
			this.m_teleportingStartSound = null;
		}
	}

	// Token: 0x0600208F RID: 8335 RVA: 0x0008DA5C File Offset: 0x0008BC5C
	public static void Show(string identifier)
	{
		UI.Menu.ShowWorldMap(false);
		GameMapUI.Instance.SetShowingTeleporters();
		GameMapUI.Instance.Teleporters.Select(identifier);
		AreaMapUI.Instance.Navigation.ScrollPosition = GameMapUI.Instance.Teleporters.SelectedTeleporter.WorldPosition;
		WorldMapUI.Instance.HideAreaSelection();
		if (GameMapUI.Instance.Teleporters.OpenWindowSound)
		{
			Sound.Play(GameMapUI.Instance.Teleporters.OpenWindowSound.GetSound(null), Vector3.zero, null);
		}
	}

	// Token: 0x06002090 RID: 8336 RVA: 0x0008DAFA File Offset: 0x0008BCFA
	public static void OnClose()
	{
		GameMapUI.Instance.SetNormal();
	}

	// Token: 0x06002091 RID: 8337 RVA: 0x0008DB08 File Offset: 0x0008BD08
	public static bool ActivateAll()
	{
		foreach (GameMapTeleporter gameMapTeleporter in TeleporterController.Instance.Teleporters)
		{
			gameMapTeleporter.Activated = true;
		}
		return true;
	}

	// Token: 0x06002092 RID: 8338 RVA: 0x0008DB68 File Offset: 0x0008BD68
	public static void Activate(string identifier)
	{
		foreach (GameMapTeleporter gameMapTeleporter in TeleporterController.Instance.Teleporters)
		{
			if (gameMapTeleporter.Identifier == identifier)
			{
				gameMapTeleporter.Activated = true;
			}
		}
	}

	// Token: 0x06002093 RID: 8339 RVA: 0x0008DBD8 File Offset: 0x0008BDD8
	public static void BeginTeleportation(GameMapTeleporter selectedTeleporter)
	{
		if (Vector3.Distance(selectedTeleporter.WorldPosition, Characters.Sein.Position) < 10f)
		{
			return;
		}
		if (!TeleporterController.Instance.DontTeleportForAnimationTesting)
		{
			Scenes.Manager.AdditivelyLoadScenesAtPosition(selectedTeleporter.WorldPosition, true, false, true);
			TeleporterController.Instance.m_teleporterTargetPosition = selectedTeleporter.WorldPosition;
		}
		TeleporterController.Instance.m_isTeleporting = true;
		Characters.Sein.Controller.PlayAnimation(TeleporterController.Instance.TeleportingStartAnimation);
		if (GameMapUI.Instance.Teleporters.StartTeleportingSound)
		{
			Sound.Play(GameMapUI.Instance.Teleporters.StartTeleportingSound.GetSound(null), Vector3.zero, null);
		}
		if (Characters.Sein.Abilities.Carry && Characters.Sein.Abilities.Carry.CurrentCarryable != null)
		{
			Characters.Sein.Abilities.Carry.CurrentCarryable.Drop();
		}
		if (TeleporterController.Instance.TeleportingStartSound != null)
		{
			TeleporterController.Instance.m_teleportingStartSound = Sound.Play(TeleporterController.Instance.TeleportingStartSound.GetSound(null), Characters.Sein.Position, new Action(TeleporterController.Instance.Nullify));
		}
		Characters.Sein.Controller.OnTriggeredAnimationFinished += TeleporterController.OnFinishedTeleportingStartAnimation;
		TeleporterController.Instance.m_startTime = Time.time;
		foreach (SavePedestal savePedestal in SavePedestal.All)
		{
			savePedestal.OnBeginTeleporting();
		}
	}

	// Token: 0x06002094 RID: 8340 RVA: 0x0008DDA4 File Offset: 0x0008BFA4
	public static void OnFinishedTeleportingStartAnimation()
	{
		Characters.Sein.Controller.OnTriggeredAnimationFinished -= TeleporterController.OnFinishedTeleportingStartAnimation;
		if (TeleporterController.Instance.m_isTeleporting)
		{
			Characters.Sein.Controller.PlayAnimation(TeleporterController.Instance.TeleportingLoopAnimation);
			TeleporterController.Instance.TeleportingTwirlAnimationSound.Play();
		}
	}

	// Token: 0x06002095 RID: 8341 RVA: 0x0008DE04 File Offset: 0x0008C004
	public void FixedUpdate()
	{
		if (this.m_isTeleporting)
		{
			float time = Time.time;
			float num = 7f;
			if (this.DontTeleportForAnimationTesting)
			{
				if (time > this.m_startTime + this.NoTeleportAnimationTime)
				{
					Characters.Sein.Controller.StopAnimation();
					Characters.Sein.Controller.PlayAnimation(TeleporterController.Instance.TeleportingFinishAnimation);
					TeleporterController.Instance.TeleportingTwirlAnimationSound.Stop();
					this.m_isTeleporting = false;
				}
			}
			else if (!Scenes.Manager.IsLoadingScenes && time > this.m_startTime + num)
			{
				this.m_isTeleporting = false;
				if (this.BloomFade)
				{
					InstantiateUtility.Instantiate(this.BloomFade);
					this.m_bloomCurrentTime = 0f;
					this.m_isBlooming = true;
					if (this.TeleportingBloomSound)
					{
						Sound.Play(this.TeleportingBloomSound.GetSound(null), Characters.Sein.Position, null);
					}
				}
				else
				{
					UI.Fader.Fade(0.5f, 0.05f, 0.2f, new Action(this.OnFadedToBlack), null);
				}
			}
		}
		if (this.m_isBlooming)
		{
			this.m_bloomCurrentTime += ((!this.IsSuspended) ? Time.deltaTime : 0f);
			if (this.m_bloomCurrentTime > this.BloomFadeDuration)
			{
				this.OnFadedToBlack();
				this.m_isBlooming = false;
			}
		}
	}

	// Token: 0x06002096 RID: 8342 RVA: 0x0008DF84 File Offset: 0x0008C184
	public void OnFadedToBlack()
	{
		foreach (SavePedestal savePedestal in SavePedestal.All)
		{
			savePedestal.OnFinishedTeleporting();
		}
		if (!InstantiateUtility.IsDestroyed(this.m_teleportingStartSound))
		{
			this.m_teleportingStartSound.FadeOut(0.5f, true);
			this.m_teleportingStartSound = null;
		}
		if (this.BloomFade)
		{
			UberGCManager.CollectResourcesIfNeeded();
		}
		Characters.Sein.Position = this.m_teleporterTargetPosition + Vector3.up * 1.6f;
		CameraPivotZone.InstantUpdate();
		Scenes.Manager.UpdatePosition();
		Scenes.Manager.UnloadScenesAtPosition(true);
		Scenes.Manager.EnableDisabledScenesAtPosition(false);
		Characters.Sein.Controller.StopAnimation();
		UI.Cameras.Current.MoveCameraToTargetInstantly(true);
		if (Characters.Ori)
		{
			Characters.Ori.BackToPlayerController();
		}
		GameController.Instance.CreateCheckpoint();
		GameController.Instance.PerformSaveGameSequence();
		LateStartHook.AddLateStartMethod(new Action(this.OnFinishedTeleporting));
	}

	// Token: 0x06002097 RID: 8343 RVA: 0x0008E0BC File Offset: 0x0008C2BC
	public void OnFinishedTeleporting()
	{
		CameraFrustumOptimizer.ForceUpdate();
		Characters.Sein.Controller.PlayAnimation(TeleporterController.Instance.TeleportingFinishAnimation);
		if (GameMapUI.Instance.Teleporters.ReachDestinationTeleporterSound)
		{
			Sound.Play(GameMapUI.Instance.Teleporters.ReachDestinationTeleporterSound.GetSound(null), base.transform.position, null);
		}
		this.TeleportingTwirlAnimationSound.Stop();
		if (this.TeleporterFinishEffect)
		{
			InstantiateUtility.Instantiate(this.TeleporterFinishEffect, this.m_teleporterTargetPosition, Quaternion.identity);
		}
		if (this.TeleportingEndSound)
		{
			Sound.Play(this.TeleportingEndSound.GetSound(null), Characters.Sein.Position, null);
		}
	}

	// Token: 0x17000590 RID: 1424
	// (get) Token: 0x06002098 RID: 8344 RVA: 0x0008E186 File Offset: 0x0008C386
	// (set) Token: 0x06002099 RID: 8345 RVA: 0x0008E18E File Offset: 0x0008C38E
	public bool IsSuspended { get; set; }

	// Token: 0x04001BAA RID: 7082
	public static TeleporterController Instance;

	// Token: 0x04001BAB RID: 7083
	public TextureAnimationWithTransitions TeleportingStartAnimation;

	// Token: 0x04001BAC RID: 7084
	public TextureAnimationWithTransitions TeleportingLoopAnimation;

	// Token: 0x04001BAD RID: 7085
	public TextureAnimationWithTransitions TeleportingFinishAnimation;

	// Token: 0x04001BAE RID: 7086
	public SoundSource TeleportingTwirlAnimationSound;

	// Token: 0x04001BAF RID: 7087
	public SoundProvider TeleportingStartSound;

	// Token: 0x04001BB0 RID: 7088
	public SoundProvider TeleportingBloomSound;

	// Token: 0x04001BB1 RID: 7089
	public SoundProvider TeleportingEndSound;

	// Token: 0x04001BB2 RID: 7090
	private SoundPlayer m_teleportingStartSound;

	// Token: 0x04001BB3 RID: 7091
	private float m_startTime;

	// Token: 0x04001BB4 RID: 7092
	public bool DontTeleportForAnimationTesting;

	// Token: 0x04001BB5 RID: 7093
	public float NoTeleportAnimationTime = 6f;

	// Token: 0x04001BB6 RID: 7094
	public List<GameMapTeleporter> Teleporters = new List<GameMapTeleporter>();

	// Token: 0x04001BB7 RID: 7095
	public GameObject BloomFade;

	// Token: 0x04001BB8 RID: 7096
	public float BloomFadeDuration;

	// Token: 0x04001BB9 RID: 7097
	public GameObject TeleporterFinishEffect;

	// Token: 0x04001BBA RID: 7098
	private bool m_isTeleporting;

	// Token: 0x04001BBB RID: 7099
	private bool m_isBlooming;

	// Token: 0x04001BBC RID: 7100
	private float m_bloomCurrentTime;

	// Token: 0x04001BBD RID: 7101
	private Vector3 m_teleporterTargetPosition;
}
