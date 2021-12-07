using System;
using System.Collections.Generic;
using Core;
using Game;
using UnityEngine;

// Token: 0x020003E1 RID: 993
public class CameraGoThroughScrollLocks
{
	// Token: 0x06001B22 RID: 6946 RVA: 0x00074364 File Offset: 0x00072564
	public CameraGoThroughScrollLocks(GameplayCamera cameraController)
	{
		this.m_cameraController = cameraController;
		Events.Scheduler.OnGameFixedUpdateLate.Add(new Action(this.OnGameEarlyFixedUpdate));
	}

	// Token: 0x06001B23 RID: 6947 RVA: 0x000743AB File Offset: 0x000725AB
	public void Destroy()
	{
		Events.Scheduler.OnGameFixedUpdateLate.Remove(new Action(this.OnGameEarlyFixedUpdate));
	}

	// Token: 0x06001B24 RID: 6948 RVA: 0x000743C8 File Offset: 0x000725C8
	public void Update()
	{
		if (!this.m_cameraController.ScrollLockIsFadingOut)
		{
			this.m_cameraController.CameraTarget.UpdateTargetPosition();
			Vector3 targetPosition = this.m_cameraController.CameraTarget.TargetPosition;
			CameraScrollLock lastPassedScrollLock;
			bool flag = this.m_cameraController.ScrollLockConstraint.HasPassedThroughScrollLock(this.m_cameraController.TargetHelperPosition, targetPosition, out lastPassedScrollLock);
			if (flag)
			{
				this.m_lastPassedScrollLock = lastPassedScrollLock;
				this.OnPassThroughScrollLock();
			}
		}
	}

	// Token: 0x06001B25 RID: 6949 RVA: 0x00074438 File Offset: 0x00072638
	public void OnPassThroughScrollLock()
	{
		if (!this.CanPassScrollocks)
		{
			return;
		}
		if (this.m_isFading)
		{
			return;
		}
		if (!this.m_lastPassedScrollLock.UseFader)
		{
			return;
		}
		UI.Fader.Fade(0.2f, 0f, 0.2f, new Action(this.OnScrollFaderFinishedFading), null);
		this.m_isFading = true;
		this.m_cameraController.ScrollLockIsFadingOut = true;
		if (Characters.Current as Component)
		{
			this.m_currentCharacter = (Characters.Current as Component).transform;
			if (this.m_lastPassedScrollLock.ScrollType != CameraScrollLock.Type.Vertical || this.m_currentCharacter.position.y <= this.m_lastPassedScrollLock.ScrollCenter.y)
			{
				if (!this.m_suspendedForScrollLock)
				{
					SuspensionManager.GetSuspendables(this.m_currentCharacterSuspendables, this.m_currentCharacter.gameObject);
					SuspensionManager.Suspend(this.m_currentCharacterSuspendables);
					this.m_suspendedForScrollLock = true;
				}
			}
		}
	}

	// Token: 0x06001B26 RID: 6950 RVA: 0x00074544 File Offset: 0x00072744
	public void OnScrollFaderFinishedFading()
	{
		this.m_waitForScrollLock = true;
		this.m_cameraController.UpdateTargetHelperPosition();
		this.m_cameraController.MoveCameraToTargetInstantly(true);
		Scenes.Manager.UpdatePosition();
		InstantLoadScenesController.Instance.LoadScenesAtPosition(null, false, true);
		Scenes.Manager.UnloadScenesAtPosition(true);
		Scenes.Manager.ForceTestForOutOfWorld();
	}

	// Token: 0x06001B27 RID: 6951 RVA: 0x0007459C File Offset: 0x0007279C
	public void OnGameEarlyFixedUpdate()
	{
		if (this.m_waitForScrollLock && !InstantLoadScenesController.Instance.IsLoading)
		{
			if (this.m_currentCharacter && this.m_suspendedForScrollLock)
			{
				this.m_suspendedForScrollLock = false;
				SuspensionManager.Resume(this.m_currentCharacterSuspendables);
				this.m_currentCharacterSuspendables.Clear();
			}
			GameController.Instance.GameScheduler.OnPassThroughScrollLock.Call();
			Game.Checkpoint.Events.OnScrollLockPassed.Call();
			this.m_waitForScrollLock = false;
			this.m_isFading = false;
			this.m_cameraController.Controller.PuppetController.Reset();
			this.m_cameraController.OffsetController.UpdateOffset(true);
			this.m_cameraController.MoveCameraToTargetInstantly(true);
			this.m_cameraController.ScrollLockIsFadingOut = false;
		}
	}

	// Token: 0x040017A0 RID: 6048
	private readonly GameplayCamera m_cameraController;

	// Token: 0x040017A1 RID: 6049
	public bool CanPassScrollocks = true;

	// Token: 0x040017A2 RID: 6050
	private bool m_isFading;

	// Token: 0x040017A3 RID: 6051
	private CameraScrollLock m_lastPassedScrollLock;

	// Token: 0x040017A4 RID: 6052
	private Transform m_currentCharacter;

	// Token: 0x040017A5 RID: 6053
	private bool m_suspendedForScrollLock;

	// Token: 0x040017A6 RID: 6054
	private bool m_waitForScrollLock;

	// Token: 0x040017A7 RID: 6055
	private readonly HashSet<ISuspendable> m_currentCharacterSuspendables = new HashSet<ISuspendable>();
}
