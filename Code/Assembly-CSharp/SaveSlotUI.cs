using System;
using System.Collections.Generic;
using Core;
using UnityEngine;

// Token: 0x0200070A RID: 1802
public class SaveSlotUI : MonoBehaviour, ISuspendable
{
	// Token: 0x06002AD0 RID: 10960 RVA: 0x000B71D8 File Offset: 0x000B53D8
	public void OnDrawGizmos()
	{
		Gizmos.DrawWireCube(base.transform.position + this.Center, this.Size);
	}

	// Token: 0x170006D5 RID: 1749
	// (get) Token: 0x06002AD1 RID: 10961 RVA: 0x000B7210 File Offset: 0x000B5410
	public Rect Bounds
	{
		get
		{
			return new Rect
			{
				width = this.Size.x,
				height = this.Size.y,
				center = this.Position + this.Center
			};
		}
	}

	// Token: 0x170006D6 RID: 1750
	// (get) Token: 0x06002AD2 RID: 10962 RVA: 0x000B726C File Offset: 0x000B546C
	public Vector3 Position
	{
		get
		{
			return base.transform.position;
		}
	}

	// Token: 0x170006D7 RID: 1751
	// (get) Token: 0x06002AD3 RID: 10963 RVA: 0x000B7279 File Offset: 0x000B5479
	public bool CanBeCopied
	{
		get
		{
			return this.SaveSlot != null;
		}
	}

	// Token: 0x170006D8 RID: 1752
	// (get) Token: 0x06002AD4 RID: 10964 RVA: 0x000B7287 File Offset: 0x000B5487
	public bool HasSave
	{
		get
		{
			return this.SaveSlot != null;
		}
	}

	// Token: 0x06002AD5 RID: 10965 RVA: 0x000B7298 File Offset: 0x000B5498
	public void OnEnable()
	{
		this.ChangeSelectionIndex(-1);
		this.CopyingAnimator.Initialize();
		this.DeletingAnimator.Initialize();
		if (this.BackupsAnimator)
		{
			this.BackupsAnimator.Initialize();
		}
		this.CopyingAnimator.AnimatorDriver.GoToStart();
		this.DeletingAnimator.AnimatorDriver.GoToStart();
		if (this.BackupsAnimator)
		{
			this.BackupsAnimator.AnimatorDriver.GoToStart();
			this.m_hasPlayedBackupsOpenSound = false;
		}
	}

	// Token: 0x06002AD6 RID: 10966 RVA: 0x000B7324 File Offset: 0x000B5524
	public void Awake()
	{
		SuspensionManager.Register(this);
	}

	// Token: 0x06002AD7 RID: 10967 RVA: 0x000B732C File Offset: 0x000B552C
	public void OnDestroy()
	{
		SuspensionManager.Unregister(this);
	}

	// Token: 0x06002AD8 RID: 10968 RVA: 0x000B7334 File Offset: 0x000B5534
	public void Start()
	{
		if (this.HighlightAnimatorB)
		{
			this.HighlightAnimatorB.Initialize();
			this.HighlightAnimatorB.AnimatorDriver.GoToEnd();
		}
	}

	// Token: 0x06002AD9 RID: 10969 RVA: 0x000B7364 File Offset: 0x000B5564
	public void Highlight(bool turnOn)
	{
		this.m_highlighted = turnOn;
		if (turnOn)
		{
			this.HighlightAnimator.AnimatorDriver.ContinueForward();
			this.RefreshBackups();
		}
		else
		{
			this.ChangeSelectionIndex(-1);
			this.HighlightAnimator.AnimatorDriver.ContinueBackwards();
			if (this.BackupsAnimator)
			{
				this.BackupsAnimator.AnimatorDriver.ContinueBackwards();
				this.m_hasPlayedBackupsOpenSound = false;
			}
		}
	}

	// Token: 0x06002ADA RID: 10970 RVA: 0x000B73D8 File Offset: 0x000B55D8
	private void RefreshBackupsIfTheyNeedIt()
	{
		if (this.m_backupsNeedUpdating && this.m_highlighted)
		{
			this.m_backupsNeedUpdating = false;
			SaveSlotBackupsManager.RequestReadBackups(this.SaveSlotIndex, new Action(this.OnFinishedReadingBackups));
		}
	}

	// Token: 0x06002ADB RID: 10971 RVA: 0x000B741C File Offset: 0x000B561C
	public void ClearBackupSaveSlots()
	{
		foreach (BackupSaveSlotUI backupSaveSlotUI in this.m_backupSaveSlots)
		{
			InstantiateUtility.Destroy(backupSaveSlotUI.gameObject);
		}
		this.m_backupSaveSlots.Clear();
	}

	// Token: 0x06002ADC RID: 10972 RVA: 0x000B7488 File Offset: 0x000B5688
	public void OnFinishedReadingBackups()
	{
		SaveSlotBackup saveSlotBackup = SaveSlotBackupsManager.SaveSlotBackupAtIndex(this.SaveSlotIndex);
		List<SaveSlotBackupInfo> list = new List<SaveSlotBackupInfo>(saveSlotBackup.SaveSlotInfos);
		list.RemoveAll((SaveSlotBackupInfo a) => a == null);
		list.Sort((SaveSlotBackupInfo a, SaveSlotBackupInfo b) => a.SaveSlotInfo.Order.CompareTo(b.SaveSlotInfo.Order));
		list.Reverse();
		float num = -0.6f;
		foreach (SaveSlotBackupInfo saveSlotBackupInfo in list)
		{
			GameObject gameObject = (GameObject)InstantiateUtility.Instantiate(this.BackupSaveSlotPrefab, base.transform.position, Quaternion.identity);
			BackupSaveSlotUI component = gameObject.GetComponent<BackupSaveSlotUI>();
			component.transform.parent = this.BackupsAnimator.transform;
			component.transform.localScale = Vector3.one * 0.77f;
			component.transform.localPosition = Vector3.up * num;
			component.SaveSlot = saveSlotBackupInfo.SaveSlotInfo;
			component.Index = saveSlotBackupInfo.Index;
			component.Apply();
			this.m_backupSaveSlots.Add(component);
			TransparencyAnimator.Register(gameObject.transform);
			num += 0.44f;
		}
	}

	// Token: 0x06002ADD RID: 10973 RVA: 0x000B75FC File Offset: 0x000B57FC
	public void FixedUpdate()
	{
		if (this.IsSuspended)
		{
			return;
		}
		if (this.SaveSlot != null && this.m_highlighted)
		{
			if (Core.Input.Up.OnPressedNotUsed)
			{
				Core.Input.Up.Used = true;
				if (!this.m_hasPlayedBackupsOpenSound)
				{
					if (this.BackupsOpenSound)
					{
						Sound.Play(this.BackupsOpenSound.GetSound(null), base.transform.position, null);
					}
					this.m_hasPlayedBackupsOpenSound = true;
				}
				this.BackupsAnimator.AnimatorDriver.ContinueForward();
				this.ChangeSelectionIndex(this.m_backupIndex + 1);
				this.RefreshBackupsIfTheyNeedIt();
			}
			if (Core.Input.Down.OnPressedNotUsed)
			{
				Core.Input.Down.Used = true;
				if (!this.m_hasPlayedBackupsOpenSound)
				{
					if (this.BackupsOpenSound)
					{
						Sound.Play(this.BackupsOpenSound.GetSound(null), base.transform.position, null);
					}
					this.m_hasPlayedBackupsOpenSound = true;
				}
				this.BackupsAnimator.AnimatorDriver.ContinueForward();
				this.ChangeSelectionIndex(this.m_backupIndex - 1);
				this.RefreshBackupsIfTheyNeedIt();
			}
		}
	}

	// Token: 0x06002ADE RID: 10974 RVA: 0x000B7728 File Offset: 0x000B5928
	public void ChangeSelectionIndex(int index)
	{
		if (index < -1)
		{
			index = -1;
		}
		if (index >= this.m_backupSaveSlots.Count)
		{
			index = this.m_backupSaveSlots.Count - 1;
		}
		if (this.m_backupIndex == index)
		{
			return;
		}
		if (this.BackupsSelectSound)
		{
			Sound.Play(this.BackupsSelectSound.GetSound(null), base.transform.position, null);
		}
		if (this.m_backupIndex == -1)
		{
			if (this.HighlightAnimatorB)
			{
				this.HighlightAnimatorB.AnimatorDriver.ContinueBackwards();
			}
		}
		else if (this.m_backupIndex < this.m_backupSaveSlots.Count)
		{
			this.m_backupSaveSlots[this.m_backupIndex].Highlight(false);
		}
		this.m_backupIndex = index;
		if (this.m_backupIndex == -1)
		{
			if (this.HighlightAnimatorB)
			{
				this.HighlightAnimatorB.AnimatorDriver.ContinueForward();
			}
		}
		else
		{
			this.m_backupSaveSlots[this.m_backupIndex].Highlight(true);
		}
	}

	// Token: 0x06002ADF RID: 10975 RVA: 0x000B7847 File Offset: 0x000B5A47
	public void SetCopying(bool turnOn)
	{
		if (turnOn)
		{
			this.CopyingAnimator.AnimatorDriver.ContinueForward();
		}
		else
		{
			this.CopyingAnimator.AnimatorDriver.ContinueBackwards();
		}
	}

	// Token: 0x06002AE0 RID: 10976 RVA: 0x000B7874 File Offset: 0x000B5A74
	public void SetDeleting(bool turnOn)
	{
		if (turnOn)
		{
			this.DeletingAnimator.AnimatorDriver.ContinueForward();
		}
		else
		{
			this.DeletingAnimator.AnimatorDriver.ContinueBackwards();
		}
	}

	// Token: 0x170006D9 RID: 1753
	// (get) Token: 0x06002AE1 RID: 10977 RVA: 0x000B78A1 File Offset: 0x000B5AA1
	public SaveSlotInfo SaveSlot
	{
		get
		{
			return SaveSlotsManager.SlotByIndex(this.SaveSlotIndex);
		}
	}

	// Token: 0x170006DA RID: 1754
	// (get) Token: 0x06002AE2 RID: 10978 RVA: 0x000B78AE File Offset: 0x000B5AAE
	public bool IsReady
	{
		get
		{
			return this.SaveSlot.Progression < WorldProgression.EnteredGinsoTree || GameController.Instance.IsPackageFullyInstalled;
		}
	}

	// Token: 0x170006DB RID: 1755
	// (get) Token: 0x06002AE3 RID: 10979 RVA: 0x000B78CE File Offset: 0x000B5ACE
	public bool IsCompleted
	{
		get
		{
			return this.SaveSlot != null && this.SaveSlot.Completed;
		}
	}

	// Token: 0x170006DC RID: 1756
	// (get) Token: 0x06002AE4 RID: 10980 RVA: 0x000B78EC File Offset: 0x000B5AEC
	public int BackupIndex
	{
		get
		{
			if (this.m_backupIndex == -1)
			{
				return -1;
			}
			if (this.m_backupIndex < this.m_backupSaveSlots.Count)
			{
				return this.m_backupSaveSlots[this.m_backupIndex].Index;
			}
			return -1;
		}
	}

	// Token: 0x06002AE5 RID: 10981 RVA: 0x000B7938 File Offset: 0x000B5B38
	private MessageDescriptor DifficultyModeToMessage(DifficultyMode difficulty)
	{
		switch (difficulty)
		{
		case DifficultyMode.Easy:
			return new MessageDescriptor(this.DifficultyTextMessageProvider + ": " + this.EasyTextMessageProvider);
		case DifficultyMode.Normal:
			return new MessageDescriptor(this.DifficultyTextMessageProvider + ": " + this.NormalTextMessageProvider);
		case DifficultyMode.Hard:
			return new MessageDescriptor(this.DifficultyTextMessageProvider + ": " + this.HardTextMessageProvider);
		case DifficultyMode.OneLife:
			return new MessageDescriptor(this.DifficultyTextMessageProvider + ": " + this.OneLifeTestMessageProvider);
		default:
			return new MessageDescriptor("Error");
		}
	}

	// Token: 0x06002AE6 RID: 10982 RVA: 0x000B79DC File Offset: 0x000B5BDC
	public void Apply()
	{
		int num = this.SaveSlotIndex + 1;
		if (this.SaveSlot == null)
		{
			this.SaveGroup.SetActive(false);
			this.EmptyGroup.SetActive(true);
			this.FullyCompletedGroup.SetActive(false);
			this.DeathGroup.SetActive(false);
			this.Screenshot.material.SetTexture(ShaderProperties.MainTexture, this.EmptyTexture);
			this.EmptySlot.SetMessage(new MessageDescriptor(string.Concat(new object[]
			{
				"*",
				num,
				":* ",
				this.EmptySlotTextMessageProvider
			})));
		}
		else
		{
			this.SaveGroup.SetActive(true);
			this.EmptyGroup.SetActive(false);
			this.AreaName.SetMessage(new MessageDescriptor(string.Concat(new object[]
			{
				"*",
				num,
				":* ",
				SaveSlotsScreenshotManager.Instance.FindAreaName(this.SaveSlot.AreaName),
				" - ",
				this.SaveSlot.Completion,
				"%"
			})));
			this.Health.SetMessage(new MessageDescriptor(this.SaveSlot.MaxHealth - 3 + "/" + 12));
			this.Energy.SetMessage(new MessageDescriptor(this.SaveSlot.MaxEnergy + "/" + 15));
			this.Time.SetMessage(new MessageDescriptor(string.Concat(new string[]
			{
				this.SaveSlot.Hours.ToString("D2"),
				":",
				this.SaveSlot.Minutes.ToString("D2"),
				":",
				this.SaveSlot.Seconds.ToString("D2")
			})));
			if (this.Difficulty)
			{
				this.Difficulty.SetMessage(this.DifficultyModeToMessage(this.SaveSlot.Difficulty));
			}
			this.DifficultyEasy.SetActive(this.SaveSlot.LowestDifficulty == DifficultyMode.Easy);
			this.DifficultyNormal.SetActive(this.SaveSlot.LowestDifficulty == DifficultyMode.Normal);
			this.DifficultyHard.SetActive(this.SaveSlot.LowestDifficulty == DifficultyMode.Hard);
			this.DifficultyOneLife.SetActive(this.SaveSlot.LowestDifficulty == DifficultyMode.OneLife);
			this.FullyCompletedGroup.SetActive(this.SaveSlot.CompletedWithEverything);
			this.DeathGroup.SetActive(this.SaveSlot.WasKilled);
			Texture texture = SaveSlotsScreenshotManager.Instance.FindScreenshot(this.SaveSlot.AreaName);
			this.Screenshot.material.SetTexture(ShaderProperties.MainTexture, (!texture) ? this.EmptyTexture : texture);
			if (this.ScreenshotB)
			{
				this.ScreenshotB.material.SetTexture(ShaderProperties.MainTexture, (!texture) ? this.EmptyTexture : texture);
			}
		}
		foreach (BackupSaveSlotUI backupSaveSlotUI in this.m_backupSaveSlots)
		{
			if (backupSaveSlotUI)
			{
				backupSaveSlotUI.Apply();
			}
		}
	}

	// Token: 0x170006DD RID: 1757
	// (get) Token: 0x06002AE7 RID: 10983 RVA: 0x000B7D7C File Offset: 0x000B5F7C
	// (set) Token: 0x06002AE8 RID: 10984 RVA: 0x000B7D84 File Offset: 0x000B5F84
	public bool IsSuspended { get; set; }

	// Token: 0x06002AE9 RID: 10985 RVA: 0x000B7D8D File Offset: 0x000B5F8D
	public void RefreshBackups()
	{
		this.ClearBackupSaveSlots();
		this.m_backupsNeedUpdating = true;
	}

	// Token: 0x04002615 RID: 9749
	public MessageBox AreaName;

	// Token: 0x04002616 RID: 9750
	public MessageBox Health;

	// Token: 0x04002617 RID: 9751
	public MessageBox Energy;

	// Token: 0x04002618 RID: 9752
	public MessageBox Time;

	// Token: 0x04002619 RID: 9753
	public MessageBox EmptySlot;

	// Token: 0x0400261A RID: 9754
	public MessageBox Difficulty;

	// Token: 0x0400261B RID: 9755
	public TranslatedMessageProvider EmptySlotTextMessageProvider;

	// Token: 0x0400261C RID: 9756
	public MessageProvider EasyTextMessageProvider;

	// Token: 0x0400261D RID: 9757
	public MessageProvider NormalTextMessageProvider;

	// Token: 0x0400261E RID: 9758
	public MessageProvider HardTextMessageProvider;

	// Token: 0x0400261F RID: 9759
	public MessageProvider OneLifeTestMessageProvider;

	// Token: 0x04002620 RID: 9760
	public MessageProvider DifficultyTextMessageProvider;

	// Token: 0x04002621 RID: 9761
	public int SaveSlotIndex;

	// Token: 0x04002622 RID: 9762
	private bool m_highlighted;

	// Token: 0x04002623 RID: 9763
	public Transform PromptPosition;

	// Token: 0x04002624 RID: 9764
	public Renderer Screenshot;

	// Token: 0x04002625 RID: 9765
	public Renderer ScreenshotB;

	// Token: 0x04002626 RID: 9766
	public GameObject SaveGroup;

	// Token: 0x04002627 RID: 9767
	public GameObject EmptyGroup;

	// Token: 0x04002628 RID: 9768
	public GameObject FullyCompletedGroup;

	// Token: 0x04002629 RID: 9769
	public GameObject DeathGroup;

	// Token: 0x0400262A RID: 9770
	public GameObject DifficultyEasy;

	// Token: 0x0400262B RID: 9771
	public GameObject DifficultyNormal;

	// Token: 0x0400262C RID: 9772
	public GameObject DifficultyHard;

	// Token: 0x0400262D RID: 9773
	public GameObject DifficultyOneLife;

	// Token: 0x0400262E RID: 9774
	public Texture EmptyTexture;

	// Token: 0x0400262F RID: 9775
	public GameObject BackupSaveSlotPrefab;

	// Token: 0x04002630 RID: 9776
	private bool m_hasPlayedBackupsOpenSound;

	// Token: 0x04002631 RID: 9777
	public SoundProvider BackupsOpenSound;

	// Token: 0x04002632 RID: 9778
	public SoundProvider BackupsSelectSound;

	// Token: 0x04002633 RID: 9779
	public Vector2 Size;

	// Token: 0x04002634 RID: 9780
	public Vector2 Center;

	// Token: 0x04002635 RID: 9781
	public BaseAnimator HighlightAnimator;

	// Token: 0x04002636 RID: 9782
	public BaseAnimator CopyingAnimator;

	// Token: 0x04002637 RID: 9783
	public BaseAnimator DeletingAnimator;

	// Token: 0x04002638 RID: 9784
	public BaseAnimator HighlightAnimatorB;

	// Token: 0x04002639 RID: 9785
	public BaseAnimator BackupsAnimator;

	// Token: 0x0400263A RID: 9786
	public GameObject DifficultyScreen;

	// Token: 0x0400263B RID: 9787
	private Rect m_bounds;

	// Token: 0x0400263C RID: 9788
	private int m_backupIndex = -1;

	// Token: 0x0400263D RID: 9789
	private readonly List<BackupSaveSlotUI> m_backupSaveSlots = new List<BackupSaveSlotUI>();

	// Token: 0x0400263E RID: 9790
	private bool m_backupsNeedUpdating;
}
