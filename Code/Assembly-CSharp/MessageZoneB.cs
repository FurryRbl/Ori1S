using System;
using Game;
using UnityEngine;

// Token: 0x020000F6 RID: 246
public class MessageZoneB : MonoBehaviour, ISuspendable
{
	// Token: 0x060009D2 RID: 2514 RVA: 0x0002B0A0 File Offset: 0x000292A0
	public void Awake()
	{
		this.m_transform = base.transform;
		SuspensionManager.Register(this);
	}

	// Token: 0x060009D3 RID: 2515 RVA: 0x0002B0B4 File Offset: 0x000292B4
	public void OnDestroy()
	{
		SuspensionManager.Unregister(this);
	}

	// Token: 0x17000214 RID: 532
	// (get) Token: 0x060009D4 RID: 2516 RVA: 0x0002B0BC File Offset: 0x000292BC
	public Rect Bounds
	{
		get
		{
			return Utility.RectFromBounds(Utility.BoundsFromTransform(this.m_transform));
		}
	}

	// Token: 0x060009D5 RID: 2517 RVA: 0x0002B0D0 File Offset: 0x000292D0
	public void FixedUpdate()
	{
		if (this.IsSuspended)
		{
			return;
		}
		if (this.Condition && !this.Condition.Validate(null))
		{
			return;
		}
		if (!Characters.Sein)
		{
			return;
		}
		if (this.Bounds.Contains(Characters.Sein.Position) && this.HintMessage)
		{
			if (this.m_messageBox)
			{
				this.m_messageBox.Visibility.ResetWaitDuration();
			}
			else
			{
				this.m_messageBox = UI.Hints.Show(this.HintMessage, HintLayer.HintZone, this.Duration);
			}
		}
	}

	// Token: 0x17000215 RID: 533
	// (get) Token: 0x060009D6 RID: 2518 RVA: 0x0002B185 File Offset: 0x00029385
	// (set) Token: 0x060009D7 RID: 2519 RVA: 0x0002B18D File Offset: 0x0002938D
	public bool IsSuspended { get; set; }

	// Token: 0x04000818 RID: 2072
	[NotNull]
	public MessageProvider HintMessage;

	// Token: 0x04000819 RID: 2073
	public float Duration;

	// Token: 0x0400081A RID: 2074
	public Condition Condition;

	// Token: 0x0400081B RID: 2075
	private MessageBox m_messageBox;

	// Token: 0x0400081C RID: 2076
	private Transform m_transform;
}
