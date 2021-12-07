using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x0200070B RID: 1803
public class SaveSlotsItemsUI : MonoBehaviour
{
	// Token: 0x06002AED RID: 10989 RVA: 0x000B7DD4 File Offset: 0x000B5FD4
	public void Awake()
	{
		for (int i = 0; i < 10; i++)
		{
			this.Items.Add(null);
		}
	}

	// Token: 0x06002AEE RID: 10990 RVA: 0x000B7E00 File Offset: 0x000B6000
	public void OnEnable()
	{
		this.Refresh();
	}

	// Token: 0x06002AEF RID: 10991 RVA: 0x000B7E08 File Offset: 0x000B6008
	public void Refresh()
	{
		if (this.Items.Count == 0)
		{
			return;
		}
		for (int i = 0; i < 10; i++)
		{
			this.RefreshItem(i);
		}
	}

	// Token: 0x06002AF0 RID: 10992 RVA: 0x000B7E40 File Offset: 0x000B6040
	public void RefreshItem(int index)
	{
		bool flag = SaveSlotsManager.Instance.SaveSlotCompleted(index);
		SaveSlotUI saveSlotUI = (!flag) ? this.SaveSlotUI : this.SaveSlotCompletedUI;
		if (this.Items[index] && this.Items[index].name != saveSlotUI.name)
		{
			UnityEngine.Object.Destroy(this.Items[index].gameObject);
			this.Items[index] = null;
		}
		if (this.Items[index] == null)
		{
			SaveSlotUI saveSlotUI2 = UnityEngine.Object.Instantiate<SaveSlotUI>(saveSlotUI);
			saveSlotUI2.name = saveSlotUI.name;
			saveSlotUI2.transform.parent = base.transform;
			saveSlotUI2.transform.localScale = this.SaveSlotUI.transform.localScale;
			saveSlotUI2.transform.localPosition = Vector3.right * this.Spacing * (float)index;
			saveSlotUI2.SaveSlotIndex = index;
			this.Items[index] = saveSlotUI2;
			TransparencyAnimator.Register(saveSlotUI2.transform);
		}
		this.Items[index].Apply();
	}

	// Token: 0x06002AF1 RID: 10993 RVA: 0x000B7F74 File Offset: 0x000B6174
	public void UpdateScroll()
	{
		this.m_scroll = Mathf.Lerp(this.m_scroll, this.m_targetScroll, 0.3f);
		this.Scroll.localPosition = Vector3.left * this.m_scroll * this.Spacing;
	}

	// Token: 0x06002AF2 RID: 10994 RVA: 0x000B7FC3 File Offset: 0x000B61C3
	public void SetScrollFromIndex(int index)
	{
		this.TargetScroll = (float)(index - 1);
	}

	// Token: 0x170006DE RID: 1758
	// (get) Token: 0x06002AF3 RID: 10995 RVA: 0x000B7FCF File Offset: 0x000B61CF
	// (set) Token: 0x06002AF4 RID: 10996 RVA: 0x000B7FD7 File Offset: 0x000B61D7
	public float TargetScroll
	{
		get
		{
			return this.m_targetScroll;
		}
		set
		{
			this.m_targetScroll = value;
			this.m_targetScroll = Mathf.Clamp(this.m_targetScroll, 0f, (float)(this.Items.Count - 2));
		}
	}

	// Token: 0x04002642 RID: 9794
	public SaveSlotUI SaveSlotUI;

	// Token: 0x04002643 RID: 9795
	public SaveSlotUI SaveSlotCompletedUI;

	// Token: 0x04002644 RID: 9796
	public Transform Scroll;

	// Token: 0x04002645 RID: 9797
	public float Spacing;

	// Token: 0x04002646 RID: 9798
	public List<SaveSlotUI> Items = new List<SaveSlotUI>();

	// Token: 0x04002647 RID: 9799
	private float m_scroll;

	// Token: 0x04002648 RID: 9800
	private float m_targetScroll;
}
