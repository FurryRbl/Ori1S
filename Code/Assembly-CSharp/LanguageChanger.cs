using System;
using Core;
using UnityEngine;

// Token: 0x02000123 RID: 291
public class LanguageChanger : MonoBehaviour
{
	// Token: 0x06000BD3 RID: 3027 RVA: 0x00034D04 File Offset: 0x00032F04
	public void Awake()
	{
		this.m_cleverMenuItem = base.GetComponent<CleverMenuItem>();
		this.m_cleverMenuItem.PressedCallback += this.OnItemPressed;
	}

	// Token: 0x06000BD4 RID: 3028 RVA: 0x00034D29 File Offset: 0x00032F29
	public void OnDestroy()
	{
		this.m_cleverMenuItem.PressedCallback -= this.OnItemPressed;
	}

	// Token: 0x06000BD5 RID: 3029 RVA: 0x00034D42 File Offset: 0x00032F42
	public void OnItemPressed()
	{
	}

	// Token: 0x17000258 RID: 600
	// (get) Token: 0x06000BD6 RID: 3030 RVA: 0x00034D44 File Offset: 0x00032F44
	public bool ItemSelected
	{
		get
		{
			return this.SelectionManager.IsActive && this.SelectionManager.CurrentMenuItem == this.m_cleverMenuItem;
		}
	}

	// Token: 0x06000BD7 RID: 3031 RVA: 0x00034D70 File Offset: 0x00032F70
	public void FixedUpdate()
	{
		if (this.ItemSelected)
		{
			if (Core.Input.Left.OnPressed)
			{
				int num = GameSettings.Instance.Language - Language.French;
				if (num < 0)
				{
					num = Enum.GetValues(typeof(Language)).Length - 1;
				}
				GameSettings.Instance.Language = (Language)num;
				if (this.ChangeSound)
				{
					Sound.Play(this.ChangeSound.GetSound(null), base.transform.position, null);
				}
			}
			if (Core.Input.Right.OnPressed)
			{
				int num2 = (int)(GameSettings.Instance.Language + 1);
				if (num2 >= Enum.GetValues(typeof(Language)).Length)
				{
					num2 = 0;
				}
				GameSettings.Instance.Language = (Language)num2;
				if (this.ChangeSound)
				{
					Sound.Play(this.ChangeSound.GetSound(null), base.transform.position, null);
				}
			}
		}
	}

	// Token: 0x0400099D RID: 2461
	private CleverMenuItem m_cleverMenuItem;

	// Token: 0x0400099E RID: 2462
	public CleverMenuItemSelectionManager SelectionManager;

	// Token: 0x0400099F RID: 2463
	public SoundProvider ChangeSound;
}
