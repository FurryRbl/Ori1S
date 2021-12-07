using System;
using System.Collections.Generic;
using Core;
using UnityEngine;

// Token: 0x020004C6 RID: 1222
public class HierarchyDebugMenu : MonoBehaviour
{
	// Token: 0x06002122 RID: 8482 RVA: 0x00091488 File Offset: 0x0008F688
	public void Awake()
	{
		HierarchyDebugMenu.Style = this.Skin.FindStyle("debugMenuItem");
		HierarchyDebugMenu.SelectedStyle = this.Skin.FindStyle("selectedDebugMenuItem");
		HierarchyDebugMenu.PressedStyle = this.Skin.FindStyle("pressedDebugMenuItem");
		HierarchyDebugMenu.DebugMenuStyle = this.Skin.FindStyle("debugMenu");
		HierarchyDebugMenu.StyleEnabled = this.Skin.FindStyle("debugMenuItemEnabled");
		HierarchyDebugMenu.StyleDisabled = this.Skin.FindStyle("debugMenuItemDisabled");
	}

	// Token: 0x06002123 RID: 8483 RVA: 0x00091514 File Offset: 0x0008F714
	public void OnEnable()
	{
		this.m_selectionIndex = 0;
		SuspensionManager.SuspendAll();
		this.m_items.Clear();
		foreach (GameObject gameObject in Resources.FindObjectsOfTypeAll<GameObject>())
		{
			if (gameObject.hideFlags == HideFlags.None && gameObject.transform.parent == null && gameObject.activeInHierarchy == gameObject.activeSelf)
			{
				this.m_items.Add(new HierarchyDebugMenu.GameObjectItem(gameObject));
			}
		}
		this.m_items.Sort((HierarchyDebugMenu.GameObjectItem a, HierarchyDebugMenu.GameObjectItem b) => string.Compare(a.Target.name, b.Target.name, StringComparison.Ordinal));
	}

	// Token: 0x06002124 RID: 8484 RVA: 0x000915C1 File Offset: 0x0008F7C1
	public void OnDisable()
	{
		SuspensionManager.ResumeAll();
	}

	// Token: 0x06002125 RID: 8485 RVA: 0x000915C8 File Offset: 0x0008F7C8
	public void OnGUI()
	{
		int num = 0;
		int depth = 0;
		GUILayout.BeginArea(new Rect((float)(Screen.width / 2) - 200f, 0f, 400f, (float)Screen.height), GUI.skin.box);
		GUILayout.BeginVertical(GUI.skin.box, new GUILayoutOption[0]);
		GUILayout.FlexibleSpace();
		foreach (HierarchyDebugMenu.GameObjectItem item in this.m_items)
		{
			this.OnItemGUI(item, ref num, depth);
		}
		this.m_maxIndex = num - 1;
		GUILayout.FlexibleSpace();
		GUILayout.EndVertical();
		GUILayout.EndArea();
	}

	// Token: 0x06002126 RID: 8486 RVA: 0x00091690 File Offset: 0x0008F890
	public void MoveSelectionDown()
	{
		this.m_selectionIndex = Mathf.Min(this.m_maxIndex, this.m_selectionIndex + 1);
	}

	// Token: 0x06002127 RID: 8487 RVA: 0x000916AB File Offset: 0x0008F8AB
	public void MoveSelectionUp()
	{
		this.m_selectionIndex = Mathf.Max(0, this.m_selectionIndex - 1);
	}

	// Token: 0x06002128 RID: 8488 RVA: 0x000916C1 File Offset: 0x0008F8C1
	private void ResetHold()
	{
		this.m_holdSpeed = 2f;
		this.m_holdAccumulation = 0f;
	}

	// Token: 0x06002129 RID: 8489 RVA: 0x000916DC File Offset: 0x0008F8DC
	public void FixedUpdate()
	{
		if (Core.Input.Up.OnPressed)
		{
			this.MoveSelectionUp();
			this.ResetHold();
		}
		if (Core.Input.Down.OnPressed)
		{
			this.MoveSelectionDown();
			this.ResetHold();
		}
		if (Core.Input.ActionButtonA.OnPressed)
		{
			this.m_selected.Target.SetActive(!this.m_selected.Target.activeSelf);
		}
		if (Core.Input.Right.OnPressed)
		{
			this.m_selected.Expanded = true;
		}
		if (Core.Input.Left.OnPressed)
		{
			this.m_selected.Expanded = false;
		}
		if (Core.Input.Cancel.OnPressed)
		{
			base.enabled = false;
		}
		if (Core.Input.Up.Pressed || Core.Input.Down.Pressed)
		{
			this.m_holdSpeed += Time.deltaTime * 4f;
			this.m_holdAccumulation += this.m_holdSpeed * Time.deltaTime;
			while (this.m_holdAccumulation > 1f)
			{
				this.m_holdAccumulation -= 1f;
				if (Core.Input.Up.Pressed)
				{
					this.MoveSelectionUp();
				}
				if (Core.Input.Down.Pressed)
				{
					this.MoveSelectionDown();
				}
			}
		}
	}

	// Token: 0x0600212A RID: 8490 RVA: 0x00091840 File Offset: 0x0008FA40
	public void OnItemGUI(HierarchyDebugMenu.GameObjectItem item, ref int index, int depth)
	{
		if (item.Target == null)
		{
			return;
		}
		int num = index - this.m_selectionIndex;
		if (num == 0)
		{
			this.m_selected = item;
		}
		int num2 = this.ShowAboveCount;
		int num3 = this.ShowBelowCount;
		if (this.m_selectionIndex < num2)
		{
			num3 += num2 - this.m_selectionIndex;
		}
		if (this.m_selectionIndex > this.m_maxIndex - num3)
		{
			num2 += num3 - (this.m_maxIndex - this.m_selectionIndex);
		}
		if (num > -num2 && num < num3)
		{
			GUI.color = ((!item.Target.activeInHierarchy) ? Color.gray : Color.white);
			bool flag = this.m_selected == item;
			GUILayout.BeginHorizontal((!flag) ? HierarchyDebugMenu.Style : HierarchyDebugMenu.SelectedStyle, new GUILayoutOption[0]);
			GUILayout.Space((float)(depth * 16));
			GUILayout.Label((!item.HasChildren) ? string.Empty : ((!item.Expanded) ? "»" : "«"), HierarchyDebugMenu.Style, new GUILayoutOption[]
			{
				GUILayout.Width(16f)
			});
			GUILayout.Label(item.Label, HierarchyDebugMenu.Style, new GUILayoutOption[0]);
			GUILayout.EndHorizontal();
		}
		index++;
		if (item.Expanded)
		{
			foreach (HierarchyDebugMenu.GameObjectItem item2 in item.Children)
			{
				this.OnItemGUI(item2, ref index, depth + 1);
			}
		}
	}

	// Token: 0x04001C04 RID: 7172
	private float m_holdSpeed;

	// Token: 0x04001C05 RID: 7173
	private float m_holdAccumulation;

	// Token: 0x04001C06 RID: 7174
	public static GUIStyle SelectedStyle;

	// Token: 0x04001C07 RID: 7175
	public static GUIStyle Style;

	// Token: 0x04001C08 RID: 7176
	public static GUIStyle PressedStyle;

	// Token: 0x04001C09 RID: 7177
	public static GUIStyle DebugMenuStyle;

	// Token: 0x04001C0A RID: 7178
	public static GUIStyle StyleEnabled;

	// Token: 0x04001C0B RID: 7179
	public static GUIStyle StyleDisabled;

	// Token: 0x04001C0C RID: 7180
	public GUISkin Skin;

	// Token: 0x04001C0D RID: 7181
	public int ShowAboveCount = 10;

	// Token: 0x04001C0E RID: 7182
	public int ShowBelowCount = 10;

	// Token: 0x04001C0F RID: 7183
	private readonly List<HierarchyDebugMenu.GameObjectItem> m_items = new List<HierarchyDebugMenu.GameObjectItem>();

	// Token: 0x04001C10 RID: 7184
	private int m_selectionIndex;

	// Token: 0x04001C11 RID: 7185
	private int m_maxIndex;

	// Token: 0x04001C12 RID: 7186
	private HierarchyDebugMenu.GameObjectItem m_selected;

	// Token: 0x020004CE RID: 1230
	public class GameObjectItem
	{
		// Token: 0x0600214A RID: 8522 RVA: 0x00091E60 File Offset: 0x00090060
		public GameObjectItem(GameObject go)
		{
			this.Target = go;
			this.Label = go.name;
			foreach (object obj in go.transform)
			{
				Transform transform = (Transform)obj;
				this.Children.Add(new HierarchyDebugMenu.GameObjectItem(transform.gameObject));
			}
			this.Children.Sort((HierarchyDebugMenu.GameObjectItem a, HierarchyDebugMenu.GameObjectItem b) => string.Compare(a.Target.name, b.Target.name, StringComparison.Ordinal));
		}

		// Token: 0x170005A7 RID: 1447
		// (get) Token: 0x0600214B RID: 8523 RVA: 0x00091F20 File Offset: 0x00090120
		public bool HasChildren
		{
			get
			{
				return this.Children.Count > 0;
			}
		}

		// Token: 0x04001C24 RID: 7204
		public GameObject Target;

		// Token: 0x04001C25 RID: 7205
		public string Label;

		// Token: 0x04001C26 RID: 7206
		public List<HierarchyDebugMenu.GameObjectItem> Children = new List<HierarchyDebugMenu.GameObjectItem>();

		// Token: 0x04001C27 RID: 7207
		public bool Expanded;
	}
}
