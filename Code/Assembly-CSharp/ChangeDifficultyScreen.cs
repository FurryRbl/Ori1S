using System;
using Game;
using UnityEngine;

// Token: 0x020004E3 RID: 1251
public class ChangeDifficultyScreen : MonoBehaviour
{
	// Token: 0x060021C7 RID: 8647 RVA: 0x00093A40 File Offset: 0x00091C40
	public string DifficultyToText(DifficultyMode mode)
	{
		switch (mode)
		{
		case DifficultyMode.Easy:
			return this.Easy.ToString();
		case DifficultyMode.Normal:
			return this.Normal.ToString();
		case DifficultyMode.Hard:
			return this.Hard.ToString();
		case DifficultyMode.OneLife:
			return this.OneLife.ToString();
		default:
			return null;
		}
	}

	// Token: 0x060021C8 RID: 8648 RVA: 0x00093A9B File Offset: 0x00091C9B
	public void Awake()
	{
		ChangeDifficultyScreen.Instance = this;
	}

	// Token: 0x060021C9 RID: 8649 RVA: 0x00093AA3 File Offset: 0x00091CA3
	public void OnDestroy()
	{
		ChangeDifficultyScreen.Instance = null;
	}

	// Token: 0x060021CA RID: 8650 RVA: 0x00093AAC File Offset: 0x00091CAC
	public void SetDifficulty(DifficultyMode difficulty)
	{
		GameObject gameObject = (GameObject)InstantiateUtility.Instantiate(this.ConfirmScreen);
		this.m_selectedDifficulty = difficulty;
		DifficultyMode mode = (DifficultyMode)Mathf.Min((int)this.m_selectedDifficulty, (int)DifficultyController.Instance.LowestDifficulty);
		string message = this.Message.ToString().Replace("[NewDifficulty]", this.DifficultyToText(this.m_selectedDifficulty)).Replace("[LowestDifficulty]", this.DifficultyToText(mode));
		gameObject.transform.FindChild("text").GetComponent<MessageBox>().SetMessage(new MessageDescriptor(message));
	}

	// Token: 0x060021CB RID: 8651 RVA: 0x00093B3C File Offset: 0x00091D3C
	public void Confirm()
	{
		DifficultyController.Instance.ChangeDifficulty(this.m_selectedDifficulty);
		InstantiateUtility.Destroy(base.gameObject);
		SaveSceneManager.Master.Save(Game.Checkpoint.SaveGameData.Master, DifficultyController.Instance);
	}

	// Token: 0x04001C59 RID: 7257
	public static ChangeDifficultyScreen Instance;

	// Token: 0x04001C5A RID: 7258
	public GameObject ConfirmScreen;

	// Token: 0x04001C5B RID: 7259
	private DifficultyMode m_selectedDifficulty;

	// Token: 0x04001C5C RID: 7260
	public MessageProvider Message;

	// Token: 0x04001C5D RID: 7261
	public MessageProvider Easy;

	// Token: 0x04001C5E RID: 7262
	public MessageProvider Normal;

	// Token: 0x04001C5F RID: 7263
	public MessageProvider Hard;

	// Token: 0x04001C60 RID: 7264
	public MessageProvider OneLife;
}
