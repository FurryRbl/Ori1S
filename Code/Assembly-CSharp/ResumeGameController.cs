using System;
using Core;
using Game;
using UnityEngine;

// Token: 0x02000144 RID: 324
public class ResumeGameController : MonoBehaviour
{
	// Token: 0x06000C9F RID: 3231 RVA: 0x000393B8 File Offset: 0x000375B8
	public void PCFixedUpdate()
	{
		if (UnityEngine.Input.anyKey)
		{
			Core.Input.ActionButtonA.Used = true;
			Core.Input.Jump.Used = true;
			UI.Menu.HideResumeScreen();
		}
	}

	// Token: 0x06000CA0 RID: 3232 RVA: 0x000393EF File Offset: 0x000375EF
	private void FixedUpdate()
	{
		if (!ResumeGameController.IsGameSuspended)
		{
			ResumeGameController.IsGameSuspended = true;
			SuspensionManager.SuspendAll();
		}
		this.PCFixedUpdate();
	}

	// Token: 0x06000CA1 RID: 3233 RVA: 0x0003940C File Offset: 0x0003760C
	public void Hide()
	{
		if (ResumeGameController.IsGameSuspended)
		{
			SuspensionManager.ResumeAll();
			ResumeGameController.IsGameSuspended = false;
		}
		UnityEngine.Object.DestroyImmediate(base.gameObject);
	}

	// Token: 0x04000A6A RID: 2666
	public GameObject VisibleGroup;

	// Token: 0x04000A6B RID: 2667
	public static bool IsGameSuspended;
}
