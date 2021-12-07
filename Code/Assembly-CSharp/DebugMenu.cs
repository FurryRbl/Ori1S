using System;
using System.Collections.Generic;
using Core;
using Game;
using UnityEngine;

// Token: 0x020004A3 RID: 1187
public class DebugMenu : MonoBehaviour
{
	// Token: 0x0600206B RID: 8299 RVA: 0x0008D177 File Offset: 0x0008B377
	private static void SuspendGameplay()
	{
		SuspensionManager.GetSuspendables(DebugMenu.SuspendablesToIgnoreForGameplay, UI.Cameras.Current.GameObject);
		SuspensionManager.SuspendExcluding(DebugMenu.SuspendablesToIgnoreForGameplay);
	}

	// Token: 0x17000588 RID: 1416
	// (get) Token: 0x0600206C RID: 8300 RVA: 0x0008D198 File Offset: 0x0008B398
	public static bool DashOrGrenadeEnabled
	{
		get
		{
			return Characters.Sein && (Characters.Sein.PlayerAbilities.Dash.HasAbility || Characters.Sein.PlayerAbilities.Grenade.HasAbility);
		}
	}

	// Token: 0x0600206D RID: 8301 RVA: 0x0008D1E9 File Offset: 0x0008B3E9
	private static void ResumeGameplay()
	{
		SuspensionManager.ResumeExcluding(DebugMenu.SuspendablesToIgnoreForGameplay);
		DebugMenu.SuspendablesToIgnoreForGameplay.Clear();
	}

	// Token: 0x0600206E RID: 8302 RVA: 0x0008D200 File Offset: 0x0008B400
	public void FixedUpdate()
	{
		if (XboxLiveController.IsContentPackage)
		{
		}
		if (GameController.FreezeFixedUpdate)
		{
			return;
		}
		if (Characters.Current as Component && !UI.MainMenuVisible)
		{
			if (DebugMenuB.DebugControlsEnabled && !Core.Input.RightShoulder.Used && Core.Input.RightShoulder.IsPressed && !DebugMenu.DashOrGrenadeEnabled && !DebugMenuB.Active)
			{
				if (!this.m_noClipParamsEnabled)
				{
					this.m_noClipGhost = (GameObject)InstantiateUtility.Instantiate(this.NoClipGhostPrefab);
					this.m_noClipGhost.transform.position = Characters.Current.Position;
					UI.Cameras.Current.ChangeTarget(this.m_noClipGhost.transform);
					DebugMenu.SuspendGameplay();
					this.m_noClipParamsEnabled = true;
					if (UberPostProcess.Instance != null)
					{
						this.m_doMotionBlur = UberPostProcess.Instance.DoMotionBlur;
						UberPostProcess.Instance.DoMotionBlur = false;
					}
				}
				Vector2 vector = MoonMath.Vector.ApplyRectangleDeadzone(Core.Input.Axis, 0.15f, 0.15f);
				this.m_noClipGhost.transform.position += vector.normalized * this.AxisToSpeedCurve.Evaluate(vector.magnitude) * Time.deltaTime;
			}
			if (this.m_noClipParamsEnabled && !Core.Input.RightShoulder.IsPressed)
			{
				Characters.Current.Position = this.m_noClipGhost.transform.position;
				Characters.Current.Speed = Vector2.zero;
				if (Characters.Ori)
				{
					Characters.Ori.MoveOriBackToPlayer();
				}
				UI.Cameras.Current.ChangeTargetToCurrentCharacter();
				InstantiateUtility.Destroy(this.m_noClipGhost);
				DebugMenu.ResumeGameplay();
				this.m_noClipParamsEnabled = false;
				if (UberPostProcess.Instance != null)
				{
					UberPostProcess.Instance.DoMotionBlur = this.m_doMotionBlur;
				}
			}
		}
	}

	// Token: 0x04001B93 RID: 7059
	public GameObject NoClipGhostPrefab;

	// Token: 0x04001B94 RID: 7060
	public AnimationCurve AxisToSpeedCurve;

	// Token: 0x04001B95 RID: 7061
	private GameObject m_noClipGhost;

	// Token: 0x04001B96 RID: 7062
	private bool m_noClipParamsEnabled;

	// Token: 0x04001B97 RID: 7063
	private static readonly HashSet<ISuspendable> SuspendablesToIgnoreForGameplay = new HashSet<ISuspendable>();

	// Token: 0x04001B98 RID: 7064
	private bool m_doMotionBlur;
}
