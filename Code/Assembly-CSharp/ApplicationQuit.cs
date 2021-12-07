using System;
using Core;
using UnityEngine;

// Token: 0x02000498 RID: 1176
[Category("System")]
public class ApplicationQuit : MonoBehaviour
{
	// Token: 0x06001FD7 RID: 8151 RVA: 0x0008BD05 File Offset: 0x00089F05
	private void Awake()
	{
		UnityEngine.Object.DontDestroyOnLoad(base.gameObject);
	}

	// Token: 0x06001FD8 RID: 8152 RVA: 0x0008BD12 File Offset: 0x00089F12
	private void Update()
	{
		if (this.QuitOnAnykey && MoonInput.anyKeyDown)
		{
			Application.Quit();
		}
		if (Core.Input.Bash.IsPressed && Core.Input.SpiritFlame.IsPressed)
		{
			Application.Quit();
		}
	}

	// Token: 0x04001B71 RID: 7025
	public bool QuitOnAnykey;
}
