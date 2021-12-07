using System;
using Core;
using Game;
using UnityEngine;

// Token: 0x02000468 RID: 1128
public class SeinDoorHandler : SaveSerialize, ISeinReceiver
{
	// Token: 0x1700053F RID: 1343
	// (get) Token: 0x06001EED RID: 7917 RVA: 0x000881DC File Offset: 0x000863DC
	// (set) Token: 0x06001EEE RID: 7918 RVA: 0x000881E4 File Offset: 0x000863E4
	public bool IsOverlappingDoor { get; private set; }

	// Token: 0x06001EEF RID: 7919 RVA: 0x000881ED File Offset: 0x000863ED
	public void SetReferenceToSein(SeinCharacter sein)
	{
		this.Sein = sein;
	}

	// Token: 0x06001EF0 RID: 7920 RVA: 0x000881F8 File Offset: 0x000863F8
	public void OnDoorOverlap(Door door)
	{
		if (this.m_enterDoorHint == null)
		{
			if (Characters.Sein.Controller.CanMove)
			{
				this.m_enterDoorHint = UI.Hints.Show((!door.OverrideEnterDoorMessage) ? this.EnterDoorMessage : door.OverrideEnterDoorMessage, HintLayer.Gameplay, 1f);
			}
		}
		else
		{
			this.m_enterDoorHint.Visibility.ResetWaitDuration();
		}
		this.m_isOverlappingDoor = true;
		if (this.Sein.Controller.CanMove && Core.Input.Up.OnPressed && this.Sein.PlatformBehaviour.PlatformMovement.IsOnGround && !this.Sein.Controller.IsBashing && !UI.MainMenuVisible)
		{
			this.EnterIntoDoor(door);
		}
	}

	// Token: 0x06001EF1 RID: 7921 RVA: 0x000882DC File Offset: 0x000864DC
	public void EnterIntoDoor(Door door)
	{
		if (this.m_enterDoorHint)
		{
			this.m_enterDoorHint.HideMessageScreen();
		}
		this.m_createCheckpoint = door.CreateCheckpoint;
		this.m_targetDoor = null;
		foreach (SceneManagerScene sceneManagerScene in Scenes.Manager.ActiveScenes)
		{
			if (sceneManagerScene.SceneRoot)
			{
				foreach (Door door2 in sceneManagerScene.SceneRoot.SceneRootData.Doors)
				{
					if (door2 != null && door2.name == door.OtherDoorName && door2 != door)
					{
						this.m_targetDoor = door2;
					}
				}
			}
		}
		if (this.m_targetDoor == null)
		{
			return;
		}
		GameObject gameObject = (GameObject)InstantiateUtility.Instantiate(this.EnterDoorAnimationPrefab);
		gameObject.transform.position = this.Sein.Position;
		if (Characters.Sein.Controller.FaceLeft)
		{
			gameObject.transform.localScale = Vector3.Scale(new Vector3(-1f, 1f, 1f), gameObject.transform.localScale);
		}
		if (door.EnterDoorAction)
		{
			door.EnterDoorAction.Perform(null);
		}
		Utility.DisableLate(this.Sein);
		UI.Fader.Fade(0.5f, 0.05f, 0.2f, new Action(this.OnFadedToBlack), null);
	}

	// Token: 0x06001EF2 RID: 7922 RVA: 0x000884C0 File Offset: 0x000866C0
	public void OnFadedToBlack()
	{
		Vector3 position = this.Sein.Position;
		if (this.m_targetDoor)
		{
			position = this.m_targetDoor.transform.position;
		}
		this.Sein.Position = position;
		CameraPivotZone.InstantUpdate();
		Scenes.Manager.UpdatePosition();
		Scenes.Manager.UnloadScenesAtPosition(true);
		Scenes.Manager.EnableDisabledScenesAtPosition(false);
		this.Sein.gameObject.SetActive(true);
		UI.Cameras.Current.MoveCameraToTargetInstantly(true);
		this.Sein.PlatformBehaviour.PlatformMovement.PlaceOnGround(0.5f, 0f);
		UI.Cameras.Current.MoveCameraToTargetInstantly(true);
		if (Characters.Ori)
		{
			Characters.Ori.MoveOriBackToPlayer();
		}
		if (this.m_createCheckpoint)
		{
			GameController.Instance.CreateCheckpoint();
			GameController.Instance.PerformSaveGameSequence();
		}
		LateStartHook.AddLateStartMethod(new Action(this.OnGoneThroughDoor));
	}

	// Token: 0x06001EF3 RID: 7923 RVA: 0x000885BC File Offset: 0x000867BC
	public void OnGoneThroughDoor()
	{
		if (this.m_targetDoor != null)
		{
			if (this.m_targetDoor.ComeOutOfDoorAction)
			{
				this.m_targetDoor.ComeOutOfDoorAction.Perform(null);
			}
		}
		this.m_targetDoor = null;
		CameraFrustumOptimizer.ForceUpdate();
	}

	// Token: 0x06001EF4 RID: 7924 RVA: 0x00088614 File Offset: 0x00086814
	public void FixedUpdate()
	{
		this.IsOverlappingDoor = this.m_isOverlappingDoor;
		this.m_isOverlappingDoor = false;
		if (this.Sein.IsSuspended)
		{
			return;
		}
	}

	// Token: 0x06001EF5 RID: 7925 RVA: 0x00088645 File Offset: 0x00086845
	public override void Serialize(Archive ar)
	{
	}

	// Token: 0x04001AE2 RID: 6882
	public SeinCharacter Sein;

	// Token: 0x04001AE3 RID: 6883
	public GameObject EnterDoorAnimationPrefab;

	// Token: 0x04001AE4 RID: 6884
	public MessageProvider EnterDoorMessage;

	// Token: 0x04001AE5 RID: 6885
	private MessageBox m_enterDoorHint;

	// Token: 0x04001AE6 RID: 6886
	private bool m_createCheckpoint;

	// Token: 0x04001AE7 RID: 6887
	private bool m_isOverlappingDoor;

	// Token: 0x04001AE8 RID: 6888
	private Door m_targetDoor;
}
