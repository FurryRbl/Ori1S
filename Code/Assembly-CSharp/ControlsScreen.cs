using System;
using UnityEngine;

// Token: 0x0200011B RID: 283
public class ControlsScreen : MonoBehaviour
{
	// Token: 0x17000252 RID: 594
	// (get) Token: 0x06000B10 RID: 2832 RVA: 0x0003029A File Offset: 0x0002E49A
	public static bool IsVisible
	{
		get
		{
			return ControlsScreen.Instance && ControlsScreen.Instance.gameObject.activeInHierarchy;
		}
	}

	// Token: 0x06000B11 RID: 2833 RVA: 0x000302BD File Offset: 0x0002E4BD
	public void Awake()
	{
		ControlsScreen.Instance = this;
	}

	// Token: 0x06000B12 RID: 2834 RVA: 0x000302C5 File Offset: 0x0002E4C5
	public void OnDestroy()
	{
		if (ControlsScreen.Instance == this)
		{
			ControlsScreen.Instance = null;
		}
	}

	// Token: 0x06000B13 RID: 2835 RVA: 0x000302DD File Offset: 0x0002E4DD
	public void OnEnable()
	{
		this.Apply();
	}

	// Token: 0x06000B14 RID: 2836 RVA: 0x000302E5 File Offset: 0x0002E4E5
	public void OnDisable()
	{
		if (SettingsScreen.Instance)
		{
			SettingsScreen.Instance.FlushSettings();
		}
	}

	// Token: 0x06000B15 RID: 2837 RVA: 0x00030300 File Offset: 0x0002E500
	public void Apply()
	{
		switch (GameSettings.Instance.CurrentControlScheme)
		{
		case ControlScheme.Controller:
			this.ControllerScheme.SetActive(true);
			this.MouseKeyboardScheme.SetActive(false);
			this.KeyboardScheme.SetActive(false);
			break;
		case ControlScheme.KeyboardAndMouse:
			this.ControllerScheme.SetActive(false);
			this.MouseKeyboardScheme.SetActive(true);
			this.KeyboardScheme.SetActive(false);
			break;
		case ControlScheme.Keyboard:
			this.ControllerScheme.SetActive(false);
			this.MouseKeyboardScheme.SetActive(false);
			this.KeyboardScheme.SetActive(true);
			break;
		}
	}

	// Token: 0x04000900 RID: 2304
	public static ControlsScreen Instance;

	// Token: 0x04000901 RID: 2305
	public GameObject KeyboardScheme;

	// Token: 0x04000902 RID: 2306
	public GameObject MouseKeyboardScheme;

	// Token: 0x04000903 RID: 2307
	public GameObject ControllerScheme;
}
