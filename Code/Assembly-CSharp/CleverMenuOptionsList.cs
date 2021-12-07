using System;
using System.Collections.Generic;
using Core;
using UnityEngine;

// Token: 0x0200010E RID: 270
public class CleverMenuOptionsList : MonoBehaviour
{
	// Token: 0x06000AA6 RID: 2726 RVA: 0x0002E4D0 File Offset: 0x0002C6D0
	public void ClearItems()
	{
		foreach (GameObject gameObject in this.m_items)
		{
			InstantiateUtility.Destroy(gameObject);
		}
		this.m_items.Clear();
		this.m_selectionManager.MenuItems.Clear();
	}

	// Token: 0x06000AA7 RID: 2727 RVA: 0x0002E544 File Offset: 0x0002C744
	public void SetSelection(int index)
	{
		this.m_selectionManager.SetCurrentItem(index);
	}

	// Token: 0x06000AA8 RID: 2728 RVA: 0x0002E554 File Offset: 0x0002C754
	public void Awake()
	{
		this.m_selectionManager = base.GetComponent<CleverMenuItemSelectionManager>();
		CleverMenuItemSelectionManager selectionManager = this.m_selectionManager;
		selectionManager.OptionPressedCallback = (Action)Delegate.Combine(selectionManager.OptionPressedCallback, new Action(this.OnOptionPressed));
		this.m_selectionManager.UnhighlightOnMouseLeave = true;
	}

	// Token: 0x06000AA9 RID: 2729 RVA: 0x0002E5A0 File Offset: 0x0002C7A0
	public void OnDestroy()
	{
		CleverMenuItemSelectionManager selectionManager = this.m_selectionManager;
		selectionManager.OptionPressedCallback = (Action)Delegate.Remove(selectionManager.OptionPressedCallback, new Action(this.OnOptionPressed));
	}

	// Token: 0x06000AAA RID: 2730 RVA: 0x0002E5C9 File Offset: 0x0002C7C9
	public void OnOptionPressed()
	{
	}

	// Token: 0x06000AAB RID: 2731 RVA: 0x0002E5CC File Offset: 0x0002C7CC
	public void OnEnable()
	{
		this.m_scrollPositionTarget = (this.m_scrollPosition = 0f);
	}

	// Token: 0x06000AAC RID: 2732 RVA: 0x0002E5F0 File Offset: 0x0002C7F0
	public void FixedUpdate()
	{
		if (Core.Input.LeftClick.OnPressed)
		{
			LateStartHook.AddLateStartMethod(delegate
			{
				base.GetComponent<CleverMenuItemGroup>().OnSelectionManagerBackPressed();
			});
		}
		if (Core.Input.MenuUp.Pressed || Core.Input.MenuDown.Pressed)
		{
			this.m_scrollPositionTarget = (float)this.m_selectionManager.Index - (float)this.OnScreenLimit * 0.5f;
		}
		if ((this.Scrollable & this.m_items.Count > this.OnScreenLimit) && this.ScrollPivot)
		{
			if (CursorController.IsVisible)
			{
				if (Core.Input.CursorPosition.y < 0.1f)
				{
					this.m_scrollPositionTarget += this.ScrollingSpeed * Time.deltaTime;
					CursorController.ResetIdleTime();
				}
				if (Core.Input.CursorPosition.y > 0.9f)
				{
					this.m_scrollPositionTarget -= this.ScrollingSpeed * Time.deltaTime;
					CursorController.ResetIdleTime();
				}
			}
			float max = (float)(this.m_items.Count - this.OnScreenLimit);
			this.m_scrollPositionTarget = Mathf.Clamp(this.m_scrollPositionTarget, 0f, max);
			this.m_scrollPosition = Mathf.Lerp(this.m_scrollPosition, this.m_scrollPositionTarget, 0.3f);
			this.ScrollPivot.transform.localPosition = Vector3.up * this.m_scrollPosition * this.Spacing;
			for (int i = 0; i < this.m_selectionManager.MenuItems.Count; i++)
			{
				CleverMenuItem cleverMenuItem = this.m_selectionManager.MenuItems[i];
				float num = (float)i - this.m_scrollPosition;
				if (num < 0f)
				{
					num = Mathf.InverseLerp(-1f, 0f, num);
				}
				else if (num > (float)(this.OnScreenLimit - 1))
				{
					num = Mathf.InverseLerp((float)this.OnScreenLimit, (float)(this.OnScreenLimit - 1), num);
				}
				else
				{
					num = 1f;
				}
				cleverMenuItem.SetOpacity(num);
			}
		}
	}

	// Token: 0x06000AAD RID: 2733 RVA: 0x0002E800 File Offset: 0x0002CA00
	public void AddItem(string label, Action onPressed)
	{
		if (this.ScrollPivot)
		{
			this.ScrollPivot.localPosition = Vector3.zero;
		}
		GameObject gameObject = (GameObject)InstantiateUtility.Instantiate(this.Item);
		this.m_items.Add(gameObject);
		if (this.ScrollPivot)
		{
			gameObject.transform.parent = this.ScrollPivot;
		}
		else
		{
			gameObject.transform.parent = base.transform;
		}
		gameObject.transform.position = this.Origin.position + Vector3.down * this.Spacing * (float)(this.m_items.Count - 1);
		TransparencyAnimator.Register(gameObject.transform);
		CleverMenuItem component = gameObject.GetComponent<CleverMenuItem>();
		this.m_selectionManager.MenuItems.Add(component);
		component.PressedCallback += onPressed;
		MessageBox componentInChildren = gameObject.GetComponentInChildren<MessageBox>();
		componentInChildren.SetMessage(new MessageDescriptor(label));
		component.ApplyColors();
	}

	// Token: 0x06000AAE RID: 2734 RVA: 0x0002E904 File Offset: 0x0002CB04
	public void AddItem(Language language, string label, Action onPressed)
	{
		if (this.ScrollPivot)
		{
			this.ScrollPivot.localPosition = Vector3.zero;
		}
		GameObject gameObject = (GameObject)InstantiateUtility.Instantiate(this.Item);
		this.m_items.Add(gameObject);
		gameObject.transform.parent = base.transform;
		gameObject.transform.position = this.Origin.position + Vector3.down * this.Spacing * (float)(this.m_items.Count - 1);
		TransparencyAnimator.Register(gameObject.transform);
		CleverMenuItem component = gameObject.GetComponent<CleverMenuItem>();
		this.m_selectionManager.MenuItems.Add(component);
		component.PressedCallback += onPressed;
		MessageBox componentInChildren = gameObject.GetComponentInChildren<MessageBox>();
		componentInChildren.OverrideLanuage(language);
		componentInChildren.SetMessage(new MessageDescriptor(label));
		componentInChildren.RefreshText();
		component.ApplyColors();
	}

	// Token: 0x040008B7 RID: 2231
	public GameObject Item;

	// Token: 0x040008B8 RID: 2232
	public float Spacing;

	// Token: 0x040008B9 RID: 2233
	public Transform Origin;

	// Token: 0x040008BA RID: 2234
	private readonly List<GameObject> m_items = new List<GameObject>();

	// Token: 0x040008BB RID: 2235
	public Transform ScrollPivot;

	// Token: 0x040008BC RID: 2236
	public bool Scrollable;

	// Token: 0x040008BD RID: 2237
	public int OnScreenLimit = 16;

	// Token: 0x040008BE RID: 2238
	private float m_scrollPosition;

	// Token: 0x040008BF RID: 2239
	private float m_scrollPositionTarget;

	// Token: 0x040008C0 RID: 2240
	public float ScrollingSpeed = 8f;

	// Token: 0x040008C1 RID: 2241
	private CleverMenuItemSelectionManager m_selectionManager;
}
