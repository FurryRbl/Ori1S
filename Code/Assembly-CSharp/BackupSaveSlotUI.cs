using System;
using UnityEngine;

// Token: 0x02000702 RID: 1794
public class BackupSaveSlotUI : MonoBehaviour
{
	// Token: 0x170006CD RID: 1741
	// (get) Token: 0x06002AA2 RID: 10914 RVA: 0x000B6C58 File Offset: 0x000B4E58
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

	// Token: 0x170006CE RID: 1742
	// (get) Token: 0x06002AA3 RID: 10915 RVA: 0x000B6CB4 File Offset: 0x000B4EB4
	public Vector3 Position
	{
		get
		{
			return base.transform.position;
		}
	}

	// Token: 0x06002AA4 RID: 10916 RVA: 0x000B6CC1 File Offset: 0x000B4EC1
	public void OnEnable()
	{
	}

	// Token: 0x06002AA5 RID: 10917 RVA: 0x000B6CC3 File Offset: 0x000B4EC3
	public void Highlight(bool turnOn)
	{
		if (turnOn)
		{
			this.HighlightAnimator.AnimatorDriver.ContinueForward();
		}
		else
		{
			this.HighlightAnimator.AnimatorDriver.ContinueBackwards();
		}
	}

	// Token: 0x06002AA6 RID: 10918 RVA: 0x000B6CF0 File Offset: 0x000B4EF0
	public void Apply()
	{
		if (this.SaveSlot == null)
		{
			return;
		}
		this.AreaName.SetMessage(new MessageDescriptor(string.Concat(new object[]
		{
			this.SaveSlot.Hours.ToString("D2"),
			":",
			this.SaveSlot.Minutes.ToString("D2"),
			":",
			this.SaveSlot.Seconds.ToString("D2"),
			" - ",
			SaveSlotsScreenshotManager.Instance.FindAreaName(this.SaveSlot.AreaName),
			" - ",
			this.SaveSlot.Completion,
			"%"
		})));
	}

	// Token: 0x040025F1 RID: 9713
	public MessageBox AreaName;

	// Token: 0x040025F2 RID: 9714
	public Vector2 Size;

	// Token: 0x040025F3 RID: 9715
	public Vector2 Center;

	// Token: 0x040025F4 RID: 9716
	public BaseAnimator HighlightAnimator;

	// Token: 0x040025F5 RID: 9717
	private bool m_highlighted;

	// Token: 0x040025F6 RID: 9718
	public SaveSlotInfo SaveSlot;

	// Token: 0x040025F7 RID: 9719
	public int Index;
}
