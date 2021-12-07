using System;
using UnityEngine;

// Token: 0x02000283 RID: 643
public class CutsceneItem : MonoBehaviour
{
	// Token: 0x170003C7 RID: 967
	// (get) Token: 0x0600152C RID: 5420 RVA: 0x0005E60D File Offset: 0x0005C80D
	public bool IsLocked
	{
		get
		{
			return !GameSettings.Instance.CutsceneUnlocked(this.Cutscene);
		}
	}

	// Token: 0x0600152D RID: 5421 RVA: 0x0005E622 File Offset: 0x0005C822
	public void Awake()
	{
		base.GetComponent<CleverMenuItem>().PressedCallback += this.OnPressed;
	}

	// Token: 0x0600152E RID: 5422 RVA: 0x0005E63C File Offset: 0x0005C83C
	public void OnPressed()
	{
		if (this.IsLocked)
		{
			CutsceneScreenController.Instance.OnLockedItemPressed.Perform(null);
		}
		else
		{
			this.Pressed.Perform(null);
		}
	}

	// Token: 0x0600152F RID: 5423 RVA: 0x0005E678 File Offset: 0x0005C878
	public void OnEnable()
	{
		if (CutsceneScreenController.Instance == null)
		{
			return;
		}
		if (this.TitleLabel)
		{
			this.TitleLabel.SetMessageProvider((!this.IsLocked) ? this.TitleMessageProvider : CutsceneScreenController.Instance.LockedMessageProvider);
		}
	}

	// Token: 0x06001530 RID: 5424 RVA: 0x0005E6D1 File Offset: 0x0005C8D1
	public void FixedUpdate()
	{
		this.LockTexture.SetActive(this.IsLocked);
	}

	// Token: 0x04001252 RID: 4690
	public UnlockedCutscenes Cutscene;

	// Token: 0x04001253 RID: 4691
	public GameObject LockTexture;

	// Token: 0x04001254 RID: 4692
	public MessageProvider TitleMessageProvider;

	// Token: 0x04001255 RID: 4693
	public MessageBox TitleLabel;

	// Token: 0x04001256 RID: 4694
	public ActionMethod Pressed;
}
