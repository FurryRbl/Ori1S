using System;
using UnityEngine;

// Token: 0x02000106 RID: 262
public class SettingsScreen : MonoBehaviour
{
	// Token: 0x06000A31 RID: 2609 RVA: 0x0002C3D7 File Offset: 0x0002A5D7
	public void Awake()
	{
		SettingsScreen.Instance = this;
	}

	// Token: 0x06000A32 RID: 2610 RVA: 0x0002C3DF File Offset: 0x0002A5DF
	public void OnDestroy()
	{
		if (SettingsScreen.Instance == this)
		{
			SettingsScreen.Instance = null;
		}
	}

	// Token: 0x06000A33 RID: 2611 RVA: 0x0002C3F7 File Offset: 0x0002A5F7
	public void OnEnable()
	{
	}

	// Token: 0x06000A34 RID: 2612 RVA: 0x0002C3F9 File Offset: 0x0002A5F9
	public void OnDisable()
	{
		this.FlushSettings();
	}

	// Token: 0x06000A35 RID: 2613 RVA: 0x0002C401 File Offset: 0x0002A601
	public void FlushSettings()
	{
		if (this.m_settingsChanged)
		{
			this.m_settingsChanged = false;
			GameSettings.Instance.SaveSettings();
			Telemetry.SendSettings();
		}
	}

	// Token: 0x06000A36 RID: 2614 RVA: 0x0002C424 File Offset: 0x0002A624
	public void SetDirty()
	{
		this.m_settingsChanged = true;
	}

	// Token: 0x04000862 RID: 2146
	public static SettingsScreen Instance;

	// Token: 0x04000863 RID: 2147
	private bool m_settingsChanged;
}
