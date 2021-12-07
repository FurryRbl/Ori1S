using System;

// Token: 0x0200011A RID: 282
public class ControllerSchemeOptions : CleverMenuOptionsList
{
	// Token: 0x06000B08 RID: 2824 RVA: 0x000301B0 File Offset: 0x0002E3B0
	public new void Awake()
	{
		base.Awake();
		ControllerSchemeOptions.Instance = this;
	}

	// Token: 0x06000B09 RID: 2825 RVA: 0x000301BE File Offset: 0x0002E3BE
	public new void OnDestroy()
	{
		base.OnDestroy();
		if (ControllerSchemeOptions.Instance == this)
		{
			ControllerSchemeOptions.Instance = null;
		}
	}

	// Token: 0x06000B0A RID: 2826 RVA: 0x000301DC File Offset: 0x0002E3DC
	public void SetControlScheme(ControlScheme scheme)
	{
		SettingsScreen.Instance.SetDirty();
		GameSettings.Instance.SetControlScheme(scheme);
		ControlsScreen.Instance.Apply();
	}

	// Token: 0x06000B0B RID: 2827 RVA: 0x00030200 File Offset: 0x0002E400
	public new void OnEnable()
	{
		base.OnEnable();
		base.ClearItems();
		base.AddItem(this.ControllerLabel.ToString(), delegate()
		{
			this.SetControlScheme(ControlScheme.Controller);
		});
		base.AddItem(this.KeyboardLabel.ToString(), delegate()
		{
			this.SetControlScheme(ControlScheme.Keyboard);
		});
		base.AddItem(this.KeyboardMouseLabel.ToString(), delegate()
		{
			this.SetControlScheme(ControlScheme.KeyboardAndMouse);
		});
		base.SetSelection(0);
	}

	// Token: 0x040008FC RID: 2300
	public static ControllerSchemeOptions Instance;

	// Token: 0x040008FD RID: 2301
	public MessageProvider ControllerLabel;

	// Token: 0x040008FE RID: 2302
	public MessageProvider KeyboardLabel;

	// Token: 0x040008FF RID: 2303
	public MessageProvider KeyboardMouseLabel;
}
