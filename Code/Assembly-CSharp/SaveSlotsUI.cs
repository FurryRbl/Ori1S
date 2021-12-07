using System;
using System.Collections.Generic;
using Core;
using UnityEngine;

// Token: 0x020004EA RID: 1258
public class SaveSlotsUI : MonoBehaviour, ISuspendable
{
	// Token: 0x170005D6 RID: 1494
	// (get) Token: 0x060021E0 RID: 8672 RVA: 0x00093E08 File Offset: 0x00092008
	public SaveSlotUI CurrentSaveSlot
	{
		get
		{
			if (this.CurrentSlotIndex >= this.Items.Count)
			{
				return null;
			}
			return this.Items[this.CurrentSlotIndex];
		}
	}

	// Token: 0x170005D7 RID: 1495
	// (get) Token: 0x060021E1 RID: 8673 RVA: 0x00093E3E File Offset: 0x0009203E
	public List<SaveSlotUI> Items
	{
		get
		{
			return this.ItemsUI.Items;
		}
	}

	// Token: 0x170005D8 RID: 1496
	// (get) Token: 0x060021E2 RID: 8674 RVA: 0x00093E4B File Offset: 0x0009204B
	public bool IsVisible
	{
		get
		{
			return this.m_isVisible;
		}
	}

	// Token: 0x060021E3 RID: 8675 RVA: 0x00093E54 File Offset: 0x00092054
	public void SetVisible(bool visible)
	{
		if (visible)
		{
			this.FadeAnimator.gameObject.SetActive(true);
			this.m_isVisible = true;
			this.FadeAnimator.Initialize();
			this.FadeAnimator.AnimatorDriver.ContinueForward();
		}
		else
		{
			this.m_isVisible = false;
			this.FadeAnimator.Initialize();
			this.FadeAnimator.AnimatorDriver.ContinueBackwards();
		}
	}

	// Token: 0x060021E4 RID: 8676 RVA: 0x00093EC4 File Offset: 0x000920C4
	public void SetVisibleImmediate(bool visible)
	{
		if (visible)
		{
			this.FadeAnimator.gameObject.SetActive(true);
			this.m_isVisible = true;
			this.FadeAnimator.Initialize();
			this.FadeAnimator.AnimatorDriver.GoToEnd();
			this.FadeAnimator.AnimatorDriver.Pause();
		}
		else
		{
			this.m_isVisible = false;
			this.FadeAnimator.Initialize();
			this.FadeAnimator.AnimatorDriver.GoToStart();
			this.FadeAnimator.AnimatorDriver.Pause();
			this.FadeAnimator.gameObject.SetActive(false);
		}
	}

	// Token: 0x060021E5 RID: 8677 RVA: 0x00093F64 File Offset: 0x00092164
	public void OnEnable()
	{
		this.m_isVisible = true;
		this.Active = true;
		if (this.FadeAnimator)
		{
			this.FadeAnimator.Initialize();
			this.FadeAnimator.AnimatorDriver.ContinueForward();
		}
		this.RefreshSlots();
	}

	// Token: 0x060021E6 RID: 8678 RVA: 0x00093FB0 File Offset: 0x000921B0
	public void OnDisable()
	{
		this.m_isVisible = false;
		if (this.m_prompt)
		{
			InstantiateUtility.Destroy(this.m_prompt.gameObject);
		}
		if (this.IsCopying)
		{
			this.CancelCopying();
		}
	}

	// Token: 0x170005D9 RID: 1497
	// (get) Token: 0x060021E7 RID: 8679 RVA: 0x00093FEC File Offset: 0x000921EC
	public SaveSlotUI SaveSlotUnderCursor
	{
		get
		{
			Vector2 cursorPositionUI = Core.Input.CursorPositionUI;
			foreach (SaveSlotUI saveSlotUI in this.Items)
			{
				if (saveSlotUI.Bounds.Contains(cursorPositionUI))
				{
					return saveSlotUI;
				}
			}
			return null;
		}
	}

	// Token: 0x060021E8 RID: 8680 RVA: 0x00094068 File Offset: 0x00092268
	public void Awake()
	{
		SaveSlotsUI.Instance = this;
		SuspensionManager.Register(this);
	}

	// Token: 0x060021E9 RID: 8681 RVA: 0x00094078 File Offset: 0x00092278
	public void RefreshSlots()
	{
		this.ItemsUI.Refresh();
		if (this.CurrentSaveSlot)
		{
			this.CurrentSaveSlot.Highlight(true);
		}
	}

	// Token: 0x060021EA RID: 8682 RVA: 0x000940AC File Offset: 0x000922AC
	public void OnDestroy()
	{
		if (SaveSlotsUI.Instance == this)
		{
			SaveSlotsUI.Instance = null;
		}
		SuspensionManager.Unregister(this);
	}

	// Token: 0x170005DA RID: 1498
	// (get) Token: 0x060021EB RID: 8683 RVA: 0x000940CA File Offset: 0x000922CA
	public bool PromptIsOpen
	{
		get
		{
			return this.m_prompt != null;
		}
	}

	// Token: 0x170005DB RID: 1499
	// (get) Token: 0x060021EC RID: 8684 RVA: 0x000940D8 File Offset: 0x000922D8
	public bool IsCopying
	{
		get
		{
			return this.CopyingFrom != null;
		}
	}

	// Token: 0x170005DC RID: 1500
	// (get) Token: 0x060021ED RID: 8685 RVA: 0x000940E6 File Offset: 0x000922E6
	public bool SelectingDifficulty
	{
		get
		{
			return this.m_difficultyScreen != null;
		}
	}

	// Token: 0x060021EE RID: 8686 RVA: 0x000940F4 File Offset: 0x000922F4
	public void CopySaveSlotsNoQuestion()
	{
		this.CopySaveSlots();
	}

	// Token: 0x060021EF RID: 8687 RVA: 0x000940FC File Offset: 0x000922FC
	public void CopySaveSlots()
	{
		SaveSlotsManager.CopySlot(this.CopyingFrom.SaveSlotIndex, this.CurrentSaveSlot.SaveSlotIndex);
		if (this.CopyingFrom.SaveSlot.Difficulty == DifficultyMode.OneLife)
		{
			SaveSlotsManager.DeleteSlot(this.CopyingFrom.SaveSlotIndex);
		}
		this.CurrentSaveSlot.RefreshBackups();
		this.ExitCopyingState();
		this.RefreshSlots();
		if (this.CopySound)
		{
			Sound.Play(this.CopySound.GetSound(null), base.transform.position, null);
		}
	}

	// Token: 0x060021F0 RID: 8688 RVA: 0x00094190 File Offset: 0x00092390
	public void CancelCopying()
	{
		if (this.CancelCopySound)
		{
			Sound.Play(this.CancelCopySound.GetSound(null), base.transform.position, null);
		}
		this.ExitCopyingState();
	}

	// Token: 0x060021F1 RID: 8689 RVA: 0x000941D4 File Offset: 0x000923D4
	private void ExitCopyingState()
	{
		this.CopyLegend.SetMessageProvider(this.CopyLegendMessageProvider);
		this.CopyingFrom.SetCopying(false);
		this.CopyingFrom = null;
	}

	// Token: 0x060021F2 RID: 8690 RVA: 0x00094205 File Offset: 0x00092405
	public void OnOverrideNewGame()
	{
	}

	// Token: 0x060021F3 RID: 8691 RVA: 0x00094207 File Offset: 0x00092407
	public void OnOverrideCopyCancelled()
	{
		this.m_prompt = null;
	}

	// Token: 0x060021F4 RID: 8692 RVA: 0x00094210 File Offset: 0x00092410
	public void OnOverrideCopyConfirmed()
	{
		this.m_prompt = null;
		this.CopySaveSlots();
	}

	// Token: 0x060021F5 RID: 8693 RVA: 0x00094220 File Offset: 0x00092420
	public void AskPrompt(ConfirmOrCancel question, Action confirm, Action cancel)
	{
		Transform promptPosition = this.CurrentSaveSlot.PromptPosition;
		this.m_prompt = (ConfirmOrCancel)UnityEngine.Object.Instantiate(question, promptPosition.position, Quaternion.identity);
		this.m_prompt.transform.parent = this.CurrentSaveSlot.PromptPosition;
		ConfirmOrCancel prompt = this.m_prompt;
		prompt.OnCancel = (Action)Delegate.Combine(prompt.OnCancel, cancel);
		this.m_prompt.OnConfirm += confirm;
	}

	// Token: 0x060021F6 RID: 8694 RVA: 0x00094298 File Offset: 0x00092498
	public void OnDeleteSaveConfirmed()
	{
		this.DeleteSlot();
		this.CurrentSaveSlot.SetDeleting(false);
		this.m_prompt = null;
		if (this.DeleteSound)
		{
			Sound.Play(this.DeleteSound.GetSound(null), base.transform.position, null);
		}
	}

	// Token: 0x060021F7 RID: 8695 RVA: 0x000942EC File Offset: 0x000924EC
	public void DeleteSlot()
	{
		SaveSlotsManager.DeleteSlot(this.CurrentSaveSlot.SaveSlotIndex);
		this.CurrentSaveSlot.RefreshBackups();
		this.RefreshSlots();
	}

	// Token: 0x060021F8 RID: 8696 RVA: 0x0009431C File Offset: 0x0009251C
	public void OnDeleteSaveCancelled()
	{
		if (this.CancelDeleteSound)
		{
			Sound.Play(this.CancelDeleteSound.GetSound(null), base.transform.position, null);
		}
		this.CurrentSaveSlot.SetDeleting(false);
		this.m_prompt = null;
	}

	// Token: 0x060021F9 RID: 8697 RVA: 0x0009436C File Offset: 0x0009256C
	public void ClampCurrentItemIndex()
	{
		this.CurrentSlotIndex = Utility.Wrap(this.CurrentSlotIndex, 0, this.Items.Count);
	}

	// Token: 0x060021FA RID: 8698 RVA: 0x00094396 File Offset: 0x00092596
	private bool CanCopyOrDelete()
	{
		return true;
	}

	// Token: 0x060021FB RID: 8699 RVA: 0x0009439C File Offset: 0x0009259C
	public void HandleNavigation()
	{
		if (Core.Input.MenuLeft.OnPressed)
		{
			this.SetCurrentItemAndScroll(this.CurrentSlotIndex - 1);
		}
		if (Core.Input.MenuRight.OnPressed)
		{
			this.SetCurrentItemAndScroll(this.CurrentSlotIndex + 1);
		}
		if (Core.Input.CursorMoved)
		{
			SaveSlotUI saveSlotUnderCursor = this.SaveSlotUnderCursor;
			if (saveSlotUnderCursor && saveSlotUnderCursor != this.CurrentSaveSlot)
			{
				this.SetCurrentItem(saveSlotUnderCursor);
			}
		}
		if (GameSettings.Instance.CurrentControlScheme != ControlScheme.Controller && GameController.IsFocused && CursorController.IsVisible)
		{
			if (Core.Input.CursorPosition.x < 0.05f && Core.Input.CursorPosition.x >= 0f)
			{
				this.ItemsUI.TargetScroll -= Time.deltaTime * 3f;
				CursorController.ResetIdleTime();
			}
			if (Core.Input.CursorPosition.x > 0.95f && Core.Input.CursorPosition.x <= 1f)
			{
				this.ItemsUI.TargetScroll += Time.deltaTime * 3f;
				CursorController.ResetIdleTime();
			}
		}
	}

	// Token: 0x060021FC RID: 8700 RVA: 0x000944D0 File Offset: 0x000926D0
	public void SetCurrentItem(SaveSlotUI saveSlot)
	{
		int currentItem = this.Items.FindIndex((SaveSlotUI a) => a == saveSlot);
		this.SetCurrentItem(currentItem);
	}

	// Token: 0x060021FD RID: 8701 RVA: 0x00094509 File Offset: 0x00092709
	public void SetCurrentItemAndScroll(int index)
	{
		this.SetCurrentItem(index);
		this.ItemsUI.SetScrollFromIndex(this.CurrentSlotIndex);
	}

	// Token: 0x060021FE RID: 8702 RVA: 0x00094524 File Offset: 0x00092724
	public void SetCurrentItem(int index)
	{
		this.CurrentSaveSlot.Highlight(false);
		this.CurrentSlotIndex = index;
		this.ClampCurrentItemIndex();
		this.CurrentSaveSlot.Highlight(true);
		if (this.SelectSound)
		{
			Sound.Play(this.SelectSound.GetSound(null), base.transform.position, null);
		}
	}

	// Token: 0x060021FF RID: 8703 RVA: 0x00094584 File Offset: 0x00092784
	public void FixedUpdate()
	{
		if (this.IsSuspended)
		{
			return;
		}
		if (!GameController.IsFocused)
		{
			return;
		}
		if (!this.IsVisible)
		{
			if (this.FadeAnimator && this.FadeAnimator.AnimatorDriver.IsReversed && !this.FadeAnimator.AnimatorDriver.IsPlaying)
			{
				this.FadeAnimator.gameObject.SetActive(false);
			}
			return;
		}
		if (this.PromptIsOpen)
		{
			return;
		}
		if (!this.Active)
		{
			return;
		}
		this.ItemsUI.UpdateScroll();
		if (this.SelectingDifficulty)
		{
			return;
		}
		this.HandleNavigation();
		if (this.IsCopying)
		{
			if (this.CanCopyOrDelete() && (this.ClickedCurrentItem || (Core.Input.Jump.OnPressed && !Core.Input.Jump.Used) || (Core.Input.Copy.OnPressed && !Core.Input.Copy.Used)) && this.CopyingFrom != this.CurrentSaveSlot)
			{
				if (this.CurrentSaveSlot.HasSave)
				{
					this.AskPrompt(this.OverrideQuestion, new Action(this.OnOverrideCopyConfirmed), new Action(this.OnOverrideCopyCancelled));
				}
				else
				{
					this.CopySaveSlotsNoQuestion();
				}
				return;
			}
			if (Core.Input.Cancel.OnPressed && !Core.Input.Cancel.Used)
			{
				this.CancelCopying();
			}
		}
		else
		{
			if (Core.Input.Copy.OnPressed && !Core.Input.Copy.Used)
			{
				if (this.CurrentSaveSlot.CanBeCopied)
				{
					this.CopyingFrom = this.CurrentSaveSlot;
					this.CopyingFrom.SetCopying(true);
					if (this.BeginCopySound)
					{
						Sound.Play(this.BeginCopySound.GetSound(null), base.transform.position, null);
					}
					this.CopyLegend.SetMessageProvider(this.PasteLegendMessageProvider);
				}
				return;
			}
			if (Core.Input.Delete.OnPressed && !Core.Input.Delete.Used)
			{
				if (this.CurrentSaveSlot.HasSave)
				{
					this.CurrentSaveSlot.SetDeleting(true);
					if (this.BeginDeleteSound)
					{
						Sound.Play(this.BeginDeleteSound.GetSound(null), base.transform.position, null);
					}
					this.AskPrompt(this.DeleteQuestion, new Action(this.OnDeleteSaveConfirmed), new Action(this.OnDeleteSaveCancelled));
				}
				return;
			}
			if (this.ClickedCurrentItem || (Core.Input.ActionButtonA.OnPressed && !Core.Input.ActionButtonA.Used))
			{
				if (this.CurrentSaveSlot.HasSave)
				{
					if (this.CurrentSaveSlot.IsReady)
					{
						this.UsedSaveSlotSelected();
						this.Active = false;
					}
					else
					{
						this.PressedSaveSlotNotReady();
					}
				}
				else
				{
					this.EmptySaveSlotSelected();
				}
				return;
			}
			if (Core.Input.Cancel.OnPressed && this.OnBackPressedAction)
			{
				this.OnBackPressedAction.Perform(null);
			}
		}
	}

	// Token: 0x170005DD RID: 1501
	// (get) Token: 0x06002200 RID: 8704 RVA: 0x000948BC File Offset: 0x00092ABC
	public bool ClickedCurrentItem
	{
		get
		{
			return Core.Input.LeftClick.OnPressed && this.CurrentSaveSlot == this.SaveSlotUnderCursor;
		}
	}

	// Token: 0x06002201 RID: 8705 RVA: 0x000948F4 File Offset: 0x00092AF4
	public void UsedSaveSlotSelected()
	{
		SaveSlotsManager.BackupIndex = this.CurrentSaveSlot.BackupIndex;
		SaveSlotsManager.CurrentSlotIndex = this.CurrentSlotIndex;
		this.UsedSaveSlotPressedAction.Perform(null);
		if (this.SelectSound)
		{
			Sound.Play(this.SelectSound.GetSound(null), base.transform.position, null);
		}
	}

	// Token: 0x06002202 RID: 8706 RVA: 0x00094958 File Offset: 0x00092B58
	public void PressedSaveSlotNotReady()
	{
		SaveSlotsManager.CurrentSlotIndex = this.CurrentSlotIndex;
		this.PressedNotReadyAction.Perform(null);
		if (this.SelectSound)
		{
			Sound.Play(this.SelectSound.GetSound(null), base.transform.position, null);
		}
	}

	// Token: 0x06002203 RID: 8707 RVA: 0x000949AC File Offset: 0x00092BAC
	public void EmptySaveSlotSelected()
	{
		SaveSlotsManager.CurrentSlotIndex = this.CurrentSlotIndex;
		this.m_difficultyScreen = (GameObject)InstantiateUtility.Instantiate(this.CurrentSaveSlot.DifficultyScreen);
		this.m_difficultyScreen.GetComponent<CleverMenuItemSelectionManager>().SetVisible(true);
		this.m_difficultyScreen.transform.parent = this.CurrentSaveSlot.HighlightAnimator.transform;
		this.m_difficultyScreen.transform.localScale = Vector3.one * 1.5384f;
		this.m_difficultyScreen.transform.localPosition = Vector3.zero;
		if (this.OpenDifficultyMenuSound)
		{
			Sound.Play(this.OpenDifficultyMenuSound.GetSound(null), base.transform.position, null);
		}
	}

	// Token: 0x170005DE RID: 1502
	// (get) Token: 0x06002204 RID: 8708 RVA: 0x00094A72 File Offset: 0x00092C72
	// (set) Token: 0x06002205 RID: 8709 RVA: 0x00094A7A File Offset: 0x00092C7A
	public bool IsSuspended { get; set; }

	// Token: 0x06002206 RID: 8710 RVA: 0x00094A84 File Offset: 0x00092C84
	public void SetDifficulty(DifficultyMode difficulty)
	{
		this.m_difficultyScreen.GetComponent<CleverMenuItemSelectionManager>().SetVisible(false);
		DifficultyController.Instance.SetDifficulty(difficulty);
		InstantiateUtility.Destroy(this.m_difficultyScreen, 2f);
		this.m_difficultyScreen = null;
		this.EmptySaveSlotPressedAction.Perform(null);
		this.Active = false;
	}

	// Token: 0x06002207 RID: 8711 RVA: 0x00094AD8 File Offset: 0x00092CD8
	public void CancelDifficultyScreen()
	{
		this.m_difficultyScreen.GetComponent<CleverMenuItemSelectionManager>().SetVisible(false);
		InstantiateUtility.Destroy(this.m_difficultyScreen, 2f);
		this.m_difficultyScreen = null;
		if (this.CancelDifficultyMenuSound)
		{
			Sound.Play(this.CancelDifficultyMenuSound.GetSound(null), base.transform.position, null);
		}
	}

	// Token: 0x04001C70 RID: 7280
	public static SaveSlotsUI Instance;

	// Token: 0x04001C71 RID: 7281
	public SaveSlotsItemsUI ItemsUI;

	// Token: 0x04001C72 RID: 7282
	public SaveSlotUI CopyingFrom;

	// Token: 0x04001C73 RID: 7283
	public ConfirmOrCancel OverrideQuestion;

	// Token: 0x04001C74 RID: 7284
	public ConfirmOrCancel DeleteQuestion;

	// Token: 0x04001C75 RID: 7285
	public int CurrentSlotIndex;

	// Token: 0x04001C76 RID: 7286
	private ConfirmOrCancel m_prompt;

	// Token: 0x04001C77 RID: 7287
	public ActionMethod EmptySaveSlotPressedAction;

	// Token: 0x04001C78 RID: 7288
	public ActionMethod UsedSaveSlotPressedAction;

	// Token: 0x04001C79 RID: 7289
	public ActionMethod PressedNotReadyAction;

	// Token: 0x04001C7A RID: 7290
	public ActionMethod OnBackPressedAction;

	// Token: 0x04001C7B RID: 7291
	public bool Active = true;

	// Token: 0x04001C7C RID: 7292
	public SoundProvider SelectSound;

	// Token: 0x04001C7D RID: 7293
	public SoundProvider BeginCopySound;

	// Token: 0x04001C7E RID: 7294
	public SoundProvider CopySound;

	// Token: 0x04001C7F RID: 7295
	public SoundProvider BeginDeleteSound;

	// Token: 0x04001C80 RID: 7296
	public SoundProvider DeleteSound;

	// Token: 0x04001C81 RID: 7297
	public SoundProvider CancelCopySound;

	// Token: 0x04001C82 RID: 7298
	public SoundProvider CancelDeleteSound;

	// Token: 0x04001C83 RID: 7299
	public SoundProvider OpenDifficultyMenuSound;

	// Token: 0x04001C84 RID: 7300
	public SoundProvider CancelDifficultyMenuSound;

	// Token: 0x04001C85 RID: 7301
	public MessageProvider CompletedGameMessageProvider;

	// Token: 0x04001C86 RID: 7302
	public MessageProvider CopyLegendMessageProvider;

	// Token: 0x04001C87 RID: 7303
	public MessageProvider PasteLegendMessageProvider;

	// Token: 0x04001C88 RID: 7304
	public MessageBox CopyLegend;

	// Token: 0x04001C89 RID: 7305
	public TransparencyAnimator FadeAnimator;

	// Token: 0x04001C8A RID: 7306
	private bool m_isVisible;

	// Token: 0x04001C8B RID: 7307
	private GameObject m_difficultyScreen;
}
