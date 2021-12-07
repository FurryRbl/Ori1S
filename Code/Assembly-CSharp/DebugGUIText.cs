using System;
using UnityEngine;

// Token: 0x020004BD RID: 1213
public class DebugGUIText : MonoBehaviour
{
	// Token: 0x060020D7 RID: 8407 RVA: 0x0008FDC8 File Offset: 0x0008DFC8
	// Note: this type is marked as 'beforefieldinit'.
	static DebugGUIText()
	{
		DebugGUIText.OnEnabledChangedEvent = delegate()
		{
		};
	}

	// Token: 0x14000037 RID: 55
	// (add) Token: 0x060020D8 RID: 8408 RVA: 0x0008FDF7 File Offset: 0x0008DFF7
	// (remove) Token: 0x060020D9 RID: 8409 RVA: 0x0008FE0E File Offset: 0x0008E00E
	private static event Action OnEnabledChangedEvent;

	// Token: 0x17000595 RID: 1429
	// (get) Token: 0x060020DA RID: 8410 RVA: 0x0008FE25 File Offset: 0x0008E025
	// (set) Token: 0x060020DB RID: 8411 RVA: 0x0008FE2C File Offset: 0x0008E02C
	public static bool Enabled
	{
		get
		{
			return DebugGUIText.m_enabled;
		}
		set
		{
			if (DebugGUIText.m_enabled != value)
			{
				DebugGUIText.m_enabled = value;
				DebugGUIText.OnEnabledChangedEvent();
			}
		}
	}

	// Token: 0x060020DC RID: 8412 RVA: 0x0008FE4C File Offset: 0x0008E04C
	public void Awake()
	{
		DebugGUIText.OnEnabledChangedEvent = (Action)Delegate.Combine(DebugGUIText.OnEnabledChangedEvent, new Action(this.OnEnabledChanged));
		this.OnEnabledChanged();
	}

	// Token: 0x060020DD RID: 8413 RVA: 0x0008FE80 File Offset: 0x0008E080
	public void OnDestroy()
	{
		DebugGUIText.OnEnabledChangedEvent = (Action)Delegate.Remove(DebugGUIText.OnEnabledChangedEvent, new Action(this.OnEnabledChanged));
	}

	// Token: 0x060020DE RID: 8414 RVA: 0x0008FEAD File Offset: 0x0008E0AD
	private void OnEnabledChanged()
	{
		base.gameObject.SetActive(DebugGUIText.Enabled);
	}

	// Token: 0x04001BD2 RID: 7122
	private static bool m_enabled;
}
