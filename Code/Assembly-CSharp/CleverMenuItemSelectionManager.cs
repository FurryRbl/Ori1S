using System;
using System.Collections.Generic;
using System.Linq;
using Core;
using UnityEngine;

// Token: 0x0200010B RID: 267
public class CleverMenuItemSelectionManager : MonoBehaviour, ISuspendable
{
	// Token: 0x06000A7C RID: 2684 RVA: 0x0002D434 File Offset: 0x0002B634
	public void SetVisible(bool visible)
	{
		if (visible)
		{
			base.gameObject.SetActive(true);
			this.m_isVisible = true;
			if (this.FadeAnimator)
			{
				this.FadeAnimator.Initialize();
				this.FadeAnimator.AnimatorDriver.ContinueForward();
			}
		}
		else
		{
			this.m_isVisible = false;
			if (this.FadeAnimator)
			{
				this.FadeAnimator.Initialize();
				this.FadeAnimator.AnimatorDriver.ContinueBackwards();
			}
			else
			{
				base.gameObject.SetActive(false);
			}
		}
	}

	// Token: 0x06000A7D RID: 2685 RVA: 0x0002D4D0 File Offset: 0x0002B6D0
	public void SetVisibleImmediate(bool visible)
	{
		if (visible)
		{
			base.gameObject.SetActive(true);
			this.m_isVisible = true;
			if (this.FadeAnimator)
			{
				this.FadeAnimator.Initialize();
				this.FadeAnimator.AnimatorDriver.GoToEnd();
				this.FadeAnimator.AnimatorDriver.Pause();
			}
		}
		else
		{
			this.m_isVisible = false;
			if (this.FadeAnimator)
			{
				this.FadeAnimator.Initialize();
				this.FadeAnimator.AnimatorDriver.GoToStart();
				this.FadeAnimator.AnimatorDriver.Pause();
			}
			base.gameObject.SetActive(false);
		}
	}

	// Token: 0x1700023C RID: 572
	// (get) Token: 0x06000A7E RID: 2686 RVA: 0x0002D584 File Offset: 0x0002B784
	public bool IsVisible
	{
		get
		{
			return this.m_isVisible;
		}
	}

	// Token: 0x1700023D RID: 573
	// (get) Token: 0x06000A7F RID: 2687 RVA: 0x0002D58C File Offset: 0x0002B78C
	// (set) Token: 0x06000A80 RID: 2688 RVA: 0x0002D594 File Offset: 0x0002B794
	public bool IsHighlightVisible
	{
		get
		{
			return this.m_isHighlightVisible;
		}
		set
		{
			this.m_isHighlightVisible = value;
			if (this.m_isHighlightVisible)
			{
				if (this.CurrentMenuItem)
				{
					this.CurrentMenuItem.OnHighlight();
				}
			}
			else if (this.CurrentMenuItem)
			{
				this.CurrentMenuItem.OnUnhighlight();
			}
		}
	}

	// Token: 0x06000A81 RID: 2689 RVA: 0x0002D5F0 File Offset: 0x0002B7F0
	public void RefreshVisible()
	{
		foreach (CleverMenuItem cleverMenuItem in this.MenuItems)
		{
			cleverMenuItem.RefreshVisible();
		}
	}

	// Token: 0x06000A82 RID: 2690 RVA: 0x0002D64C File Offset: 0x0002B84C
	public void OnEnable()
	{
		this.m_isVisible = true;
		if (this.FadeAnimator)
		{
			this.FadeAnimator.Initialize();
			this.FadeAnimator.AnimatorDriver.ContinueForward();
		}
		this.RefreshVisible();
	}

	// Token: 0x06000A83 RID: 2691 RVA: 0x0002D691 File Offset: 0x0002B891
	public void OnDisable()
	{
		this.m_isVisible = false;
	}

	// Token: 0x1700023E RID: 574
	// (get) Token: 0x06000A84 RID: 2692 RVA: 0x0002D69A File Offset: 0x0002B89A
	// (set) Token: 0x06000A85 RID: 2693 RVA: 0x0002D6A2 File Offset: 0x0002B8A2
	public bool IsActive
	{
		get
		{
			return this.m_isActive;
		}
		set
		{
			this.m_isActive = value;
		}
	}

	// Token: 0x1700023F RID: 575
	// (get) Token: 0x06000A86 RID: 2694 RVA: 0x0002D6AB File Offset: 0x0002B8AB
	// (set) Token: 0x06000A87 RID: 2695 RVA: 0x0002D6B3 File Offset: 0x0002B8B3
	public bool IsLocked { get; set; }

	// Token: 0x17000240 RID: 576
	// (get) Token: 0x06000A88 RID: 2696 RVA: 0x0002D6BC File Offset: 0x0002B8BC
	public CleverMenuItem CurrentMenuItem
	{
		get
		{
			if (this.Index < 0 || this.Index >= this.MenuItems.Count)
			{
				return null;
			}
			return this.MenuItems[this.Index];
		}
	}

	// Token: 0x06000A89 RID: 2697 RVA: 0x0002D6F3 File Offset: 0x0002B8F3
	public void Awake()
	{
		SuspensionManager.Register(this);
	}

	// Token: 0x06000A8A RID: 2698 RVA: 0x0002D6FB File Offset: 0x0002B8FB
	public void OnDestroy()
	{
		SuspensionManager.Unregister(this);
	}

	// Token: 0x06000A8B RID: 2699 RVA: 0x0002D704 File Offset: 0x0002B904
	public void MoveSelection(bool forward)
	{
		int num = this.Index;
		int num2 = 0;
		if (forward)
		{
			for (;;)
			{
				num = (num + 1) % this.MenuItems.Count;
				if (num2++ > this.MenuItems.Count)
				{
					break;
				}
				if (this.MenuItems[num].IsActivated)
				{
					goto IL_56;
				}
			}
			num = this.Index;
			IL_56:;
		}
		else
		{
			for (;;)
			{
				num = ((num - 1 >= 0) ? (num - 1) : (this.MenuItems.Count - 1));
				if (num2++ > this.MenuItems.Count)
				{
					break;
				}
				if (this.MenuItems[num].IsActivated)
				{
					goto IL_B1;
				}
			}
			num = this.Index;
		}
		IL_B1:
		if (num == this.Index)
		{
			return;
		}
		if (this.MenuItems[num].IsActivated)
		{
			this.SetCurrentItem(num);
		}
	}

	// Token: 0x06000A8C RID: 2700 RVA: 0x0002D7EC File Offset: 0x0002B9EC
	public void SetCurrentMenuItem(CleverMenuItem menuItem)
	{
		int currentItem = this.MenuItems.FindIndex((CleverMenuItem a) => a == menuItem);
		this.SetCurrentItem(currentItem);
	}

	// Token: 0x06000A8D RID: 2701 RVA: 0x0002D828 File Offset: 0x0002BA28
	public void SetCurrentItem(int index)
	{
		if (this.CurrentMenuItem)
		{
			this.CurrentMenuItem.OnUnhighlight();
		}
		this.Index = index;
		if (this.CurrentMenuItem)
		{
			this.CurrentMenuItem.OnHighlight();
			this.OptionChangeCallback();
			if (this.OptionChangeAction)
			{
				this.OptionChangeAction.Perform(null);
			}
		}
	}

	// Token: 0x06000A8E RID: 2702 RVA: 0x0002D89C File Offset: 0x0002BA9C
	public void Start()
	{
		if (this.IsHighlightVisible && this.CurrentMenuItem)
		{
			this.CurrentMenuItem.OnHighlight();
		}
	}

	// Token: 0x06000A8F RID: 2703 RVA: 0x0002D8D0 File Offset: 0x0002BAD0
	public void SetIndexToFirst()
	{
		for (int i = 0; i < this.MenuItems.Count; i++)
		{
			CleverMenuItem cleverMenuItem = this.MenuItems[i];
			if (cleverMenuItem.IsActivated)
			{
				this.SetCurrentItem(i);
				return;
			}
		}
	}

	// Token: 0x06000A90 RID: 2704 RVA: 0x0002D91C File Offset: 0x0002BB1C
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
				base.gameObject.SetActive(false);
			}
			return;
		}
		if (this.CurrentMenuItem && this.CurrentMenuItem.IsPerforming())
		{
			return;
		}
		if (this.IsLocked)
		{
			return;
		}
		if (Core.Input.LeftClick.OnPressed)
		{
			CleverMenuItem cleverMenuItemUnderCursor = this.CleverMenuItemUnderCursor;
			if (cleverMenuItemUnderCursor)
			{
				this.SetCurrentMenuItem(cleverMenuItemUnderCursor);
				this.PressCurrentItem();
				return;
			}
		}
		if (Core.Input.CursorMoved && this.HighlightOnMouseOver)
		{
			CleverMenuItem cleverMenuItemUnderCursor2 = this.CleverMenuItemUnderCursor;
			if (cleverMenuItemUnderCursor2 && cleverMenuItemUnderCursor2 != this.CurrentMenuItem)
			{
				this.SetCurrentMenuItem(cleverMenuItemUnderCursor2);
			}
			if (this.UnhighlightOnMouseLeave && cleverMenuItemUnderCursor2 == null && this.CurrentMenuItem.IsHighlighted)
			{
				this.CurrentMenuItem.OnUnhighlight();
			}
			if (this.HighlightOnMouseOver && cleverMenuItemUnderCursor2 != null && !cleverMenuItemUnderCursor2.IsHighlighted)
			{
				this.CurrentMenuItem.OnHighlight();
			}
		}
		if (!this.IsActive)
		{
			return;
		}
		switch (this.ItemDirection)
		{
		case CleverMenuItemSelectionManager.Direction.LeftToRight:
			if (Core.Input.MenuLeft.OnPressed)
			{
				this.MoveSelection(false);
				this.m_holdRemainingTime = 0.4f;
			}
			if (Core.Input.MenuRight.OnPressed)
			{
				this.MoveSelection(true);
				this.m_holdRemainingTime = 0.4f;
			}
			if (Core.Input.MenuLeft.Pressed || Core.Input.MenuRight.Pressed)
			{
				this.m_holdRemainingTime -= Time.deltaTime;
				if (this.m_holdRemainingTime < 0f)
				{
					if (Core.Input.MenuLeft.Pressed)
					{
						this.MoveSelection(false);
					}
					if (Core.Input.MenuRight.Pressed)
					{
						this.MoveSelection(true);
					}
				}
			}
			break;
		case CleverMenuItemSelectionManager.Direction.TopToBottom:
			if (Core.Input.MenuUp.OnPressed)
			{
				this.MoveSelection(false);
				this.m_holdRemainingTime = 0.4f;
			}
			if (Core.Input.MenuDown.OnPressed)
			{
				this.MoveSelection(true);
				this.m_holdRemainingTime = 0.4f;
			}
			if (Core.Input.MenuUp.Pressed || Core.Input.MenuDown.Pressed)
			{
				this.m_holdRemainingTime -= Time.deltaTime;
				if (this.m_holdRemainingTime < 0f)
				{
					if (Core.Input.MenuUp.Pressed)
					{
						this.m_holdRemainingTime = 0.04f;
						this.MoveSelection(false);
					}
					if (Core.Input.MenuDown.Pressed)
					{
						this.m_holdRemainingTime = 0.04f;
						this.MoveSelection(true);
					}
				}
			}
			break;
		case CleverMenuItemSelectionManager.Direction.NavigationCage:
			this.HandleNavigationCage();
			break;
		}
		if (Core.Input.ActionButtonA.OnPressed && !Core.Input.ActionButtonA.Used)
		{
			if (this.m_buttonPressDelay <= 0f)
			{
				this.m_buttonPressDelay = this.ButtonPressDelay;
				Core.Input.ActionButtonA.Used = true;
				Core.Input.Jump.Used = true;
				this.PressCurrentItem();
			}
			return;
		}
		this.m_buttonPressDelay = Mathf.Max(0f, this.m_buttonPressDelay - Time.deltaTime);
		if (Core.Input.Cancel.OnPressed && !Core.Input.Cancel.Used)
		{
			Core.Input.Cancel.Used = true;
			Core.Input.SoulFlame.Used = true;
			this.OnBackPressed();
		}
	}

	// Token: 0x06000A91 RID: 2705 RVA: 0x0002DCEC File Offset: 0x0002BEEC
	public void OnDrawGizmosSelected()
	{
		if (this.ItemDirection == CleverMenuItemSelectionManager.Direction.NavigationCage)
		{
			Gizmos.color = Color.yellow;
			foreach (CleverMenuItemSelectionManager.NavigationData navigationData in this.Navigation)
			{
				if (navigationData.From && navigationData.To)
				{
					Gizmos.DrawLine(navigationData.From.transform.position, navigationData.To.transform.position);
				}
			}
			Gizmos.color = Color.white;
		}
	}

	// Token: 0x06000A92 RID: 2706 RVA: 0x0002DDA4 File Offset: 0x0002BFA4
	public void HandleNavigationCage()
	{
		if (Core.Input.Axis.magnitude > 0.5f)
		{
			if (this.m_nextPressDelay == 0f)
			{
				if (this.ChangeMenuItem())
				{
					this.m_nextPressDelay = 0.4f;
				}
				else
				{
					this.m_nextPressDelay = 0f;
				}
			}
			else if (this.m_nextPressDelay > 0f)
			{
				this.m_nextPressDelay -= Time.deltaTime;
				if (this.m_nextPressDelay < 0f)
				{
					this.m_nextPressDelay = 0f;
				}
			}
		}
		else
		{
			this.m_nextPressDelay = 0f;
		}
	}

	// Token: 0x06000A93 RID: 2707 RVA: 0x0002DE50 File Offset: 0x0002C050
	public bool ChangeMenuItem()
	{
		Vector2 normalized = Core.Input.Axis.normalized;
		if (!this.CurrentMenuItem)
		{
			return false;
		}
		Vector2 b = this.CurrentMenuItem.Transform.position;
		CleverMenuItem cleverMenuItem = this.CurrentMenuItem;
		float num = Mathf.Cos(this.AngleTolerance * 0.017453292f);
		foreach (CleverMenuItemSelectionManager.NavigationData navigationData in this.Navigation)
		{
			if ((navigationData.Condition == null || navigationData.Condition(navigationData)) && navigationData.From == this.CurrentMenuItem && navigationData.To.IsVisible)
			{
				Vector2 a = navigationData.To.Transform.position;
				Vector2 normalized2 = (a - b).normalized;
				float num2 = Vector2.Dot(normalized, normalized2);
				if (num2 > num)
				{
					num = num2;
					cleverMenuItem = navigationData.To;
				}
			}
		}
		if (cleverMenuItem != this.CurrentMenuItem)
		{
			this.SetCurrentMenuItem(cleverMenuItem);
			return true;
		}
		return false;
	}

	// Token: 0x06000A94 RID: 2708 RVA: 0x0002DFA0 File Offset: 0x0002C1A0
	public void PressCurrentItem()
	{
		this.OptionPressedCallback();
		if (this.CurrentMenuItem)
		{
			this.CurrentMenuItem.OnPressed();
		}
	}

	// Token: 0x06000A95 RID: 2709 RVA: 0x0002DFD4 File Offset: 0x0002C1D4
	public void OnBackPressed()
	{
		this.OnBackPressedCallback();
		if (this.BackItem)
		{
			this.BackItem.OnPressed();
		}
		if (this.BackAction)
		{
			this.BackAction.Perform(null);
		}
	}

	// Token: 0x17000241 RID: 577
	// (get) Token: 0x06000A96 RID: 2710 RVA: 0x0002E024 File Offset: 0x0002C224
	public CleverMenuItem CleverMenuItemUnderCursor
	{
		get
		{
			Vector2 cursorPositionUI = Core.Input.CursorPositionUI;
			float num = float.PositiveInfinity;
			CleverMenuItem result = null;
			foreach (CleverMenuItem cleverMenuItem in this.MenuItems)
			{
				if (cleverMenuItem.IsVisible && cleverMenuItem.Bounds.Contains(cursorPositionUI))
				{
					float num2 = Vector3.Distance(cleverMenuItem.Bounds.center, cursorPositionUI);
					if (num > num2)
					{
						num = num2;
						result = cleverMenuItem;
					}
				}
			}
			return result;
		}
	}

	// Token: 0x06000A97 RID: 2711 RVA: 0x0002E0D8 File Offset: 0x0002C2D8
	[ContextMenu("Create navigation from cage")]
	public void CreateNavigationStructureFromCageTool()
	{
		List<CleverMenuItem> list = UnityEngine.Object.FindObjectsOfType(typeof(CleverMenuItem)).Cast<CleverMenuItem>().ToList<CleverMenuItem>();
		Dictionary<CageStructureTool.Vertex, CleverMenuItem> dictionary = new Dictionary<CageStructureTool.Vertex, CleverMenuItem>();
		foreach (CageStructureTool.Vertex vertex in this.CopyFromCage.Vertices)
		{
			Vector3 a = this.CopyFromCage.transform.TransformPoint(vertex.Position);
			float num = float.MaxValue;
			CleverMenuItem value = null;
			foreach (CleverMenuItem cleverMenuItem in list)
			{
				float num2 = Vector3.Distance(a, cleverMenuItem.transform.position);
				if (num2 < num)
				{
					value = cleverMenuItem;
					num = num2;
				}
			}
			dictionary[vertex] = value;
		}
		this.Navigation.Clear();
		foreach (CageStructureTool.Edge edge in this.CopyFromCage.Edges)
		{
			CageStructureTool.Vertex key = this.CopyFromCage.VertexByIndex(edge.VertexA);
			CageStructureTool.Vertex key2 = this.CopyFromCage.VertexByIndex(edge.VertexB);
			this.Navigation.Add(new CleverMenuItemSelectionManager.NavigationData
			{
				From = dictionary[key],
				To = dictionary[key2]
			});
			this.Navigation.Add(new CleverMenuItemSelectionManager.NavigationData
			{
				From = dictionary[key2],
				To = dictionary[key]
			});
		}
		this.MenuItems.Clear();
		foreach (CleverMenuItemSelectionManager.NavigationData navigationData in this.Navigation)
		{
			if (!this.MenuItems.Contains(navigationData.From))
			{
				this.MenuItems.Add(navigationData.From);
			}
		}
	}

	// Token: 0x17000242 RID: 578
	// (get) Token: 0x06000A98 RID: 2712 RVA: 0x0002E33C File Offset: 0x0002C53C
	// (set) Token: 0x06000A99 RID: 2713 RVA: 0x0002E344 File Offset: 0x0002C544
	public bool IsSuspended { get; set; }

	// Token: 0x04000895 RID: 2197
	public const float HOLD_DELAY = 0.4f;

	// Token: 0x04000896 RID: 2198
	public const float HOLD_FAST_DELAY = 0.04f;

	// Token: 0x04000897 RID: 2199
	public List<CleverMenuItemSelectionManager.NavigationData> Navigation = new List<CleverMenuItemSelectionManager.NavigationData>();

	// Token: 0x04000898 RID: 2200
	public CageStructureTool CopyFromCage;

	// Token: 0x04000899 RID: 2201
	public List<CleverMenuItem> MenuItems;

	// Token: 0x0400089A RID: 2202
	public CleverMenuItemSelectionManager.Direction ItemDirection;

	// Token: 0x0400089B RID: 2203
	public ActionMethod OptionChangeAction;

	// Token: 0x0400089C RID: 2204
	public Action OptionChangeCallback = delegate()
	{
	};

	// Token: 0x0400089D RID: 2205
	public Action OptionPressedCallback = delegate()
	{
	};

	// Token: 0x0400089E RID: 2206
	public Action OnBackPressedCallback = delegate()
	{
	};

	// Token: 0x0400089F RID: 2207
	public bool HighlightOnMouseOver = true;

	// Token: 0x040008A0 RID: 2208
	public bool UnhighlightOnMouseLeave;

	// Token: 0x040008A1 RID: 2209
	public TransparencyAnimator FadeAnimator;

	// Token: 0x040008A2 RID: 2210
	public int Index;

	// Token: 0x040008A3 RID: 2211
	private int m_defaultIndex;

	// Token: 0x040008A4 RID: 2212
	public CleverMenuItem BackItem;

	// Token: 0x040008A5 RID: 2213
	public ActionMethod BackAction;

	// Token: 0x040008A6 RID: 2214
	public float ButtonPressDelay = 0.2f;

	// Token: 0x040008A7 RID: 2215
	public float AngleTolerance = 60f;

	// Token: 0x040008A8 RID: 2216
	private bool m_isVisible = true;

	// Token: 0x040008A9 RID: 2217
	private bool m_isActive = true;

	// Token: 0x040008AA RID: 2218
	private float m_buttonPressDelay;

	// Token: 0x040008AB RID: 2219
	private float m_nextPressDelay;

	// Token: 0x040008AC RID: 2220
	private float m_holdDelayDuration;

	// Token: 0x040008AD RID: 2221
	private float m_holdRemainingTime;

	// Token: 0x040008AE RID: 2222
	private bool m_isHighlightVisible = true;

	// Token: 0x020001C1 RID: 449
	[Serializable]
	public class NavigationData
	{
		// Token: 0x04000E0C RID: 3596
		public CleverMenuItem From;

		// Token: 0x04000E0D RID: 3597
		public CleverMenuItem To;

		// Token: 0x04000E0E RID: 3598
		public Func<CleverMenuItemSelectionManager.NavigationData, bool> Condition;
	}

	// Token: 0x02000479 RID: 1145
	public enum FocusState
	{
		// Token: 0x04001B16 RID: 6934
		None,
		// Token: 0x04001B17 RID: 6935
		InFocus,
		// Token: 0x04001B18 RID: 6936
		ChildInFocus
	}

	// Token: 0x0200047A RID: 1146
	public enum Direction
	{
		// Token: 0x04001B1A RID: 6938
		LeftToRight,
		// Token: 0x04001B1B RID: 6939
		TopToBottom,
		// Token: 0x04001B1C RID: 6940
		NavigationCage
	}
}
