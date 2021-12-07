using System;

// Token: 0x02000122 RID: 290
public class KeyboardLayoutOptions : CleverMenuOptionsList
{
	// Token: 0x06000BCC RID: 3020 RVA: 0x00034C43 File Offset: 0x00032E43
	public new void Awake()
	{
		base.Awake();
		KeyboardLayoutOptions.Instance = this;
	}

	// Token: 0x06000BCD RID: 3021 RVA: 0x00034C51 File Offset: 0x00032E51
	public new void OnDestroy()
	{
		base.OnDestroy();
		if (KeyboardLayoutOptions.Instance == this)
		{
			KeyboardLayoutOptions.Instance = null;
		}
	}

	// Token: 0x06000BCE RID: 3022 RVA: 0x00034C6F File Offset: 0x00032E6F
	public void SetKeyboardLayout(KeyboardLayout scheme)
	{
		GameSettings.Instance.KeyboardLayout = scheme;
	}

	// Token: 0x06000BCF RID: 3023 RVA: 0x00034C7C File Offset: 0x00032E7C
	public new void OnEnable()
	{
		base.OnEnable();
		base.ClearItems();
		base.AddItem(this.Qwerty.ToString(), delegate()
		{
			this.SetKeyboardLayout(KeyboardLayout.QWERTY);
			SettingsScreen.Instance.SetDirty();
		});
		base.AddItem(this.Azerty.ToString(), delegate()
		{
			this.SetKeyboardLayout(KeyboardLayout.AZERTY);
			SettingsScreen.Instance.SetDirty();
		});
		base.SetSelection(0);
	}

	// Token: 0x0400099A RID: 2458
	public static KeyboardLayoutOptions Instance;

	// Token: 0x0400099B RID: 2459
	public MessageProvider Qwerty;

	// Token: 0x0400099C RID: 2460
	public MessageProvider Azerty;
}
