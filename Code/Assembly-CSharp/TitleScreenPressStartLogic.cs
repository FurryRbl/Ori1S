using System;
using Core;
using UnityEngine;

// Token: 0x02000161 RID: 353
public class TitleScreenPressStartLogic : MonoBehaviour
{
	// Token: 0x06000E2A RID: 3626 RVA: 0x00041C99 File Offset: 0x0003FE99
	public void FixedUpdate()
	{
		if (Core.Input.AnyStart.OnPressed)
		{
			XboxLiveController.Instance.StartPressedOnMainMenu(new Action(this.OnStartPressedCallback));
		}
	}

	// Token: 0x06000E2B RID: 3627 RVA: 0x00041CC0 File Offset: 0x0003FEC0
	public void OnStartPressedCallback()
	{
		GameStateMachine.Instance.SetToTitleScreen();
		this.OnPressed.Perform(null);
	}

	// Token: 0x04000B6E RID: 2926
	public ActionMethod OnPressed;
}
