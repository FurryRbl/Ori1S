using System;
using System.Collections;
using UnityEngine;

// Token: 0x020003A6 RID: 934
public class LegacyColorFlashAnimator : LegacyAnimator
{
	// Token: 0x06001A25 RID: 6693 RVA: 0x000708F7 File Offset: 0x0006EAF7
	public override void Awake()
	{
		this.m_renderer = base.GetComponent<Renderer>();
		base.Awake();
	}

	// Token: 0x06001A26 RID: 6694 RVA: 0x0007090B File Offset: 0x0006EB0B
	public override void Start()
	{
		this.CacheShaderInformation();
		base.Start();
	}

	// Token: 0x06001A27 RID: 6695 RVA: 0x00070919 File Offset: 0x0006EB19
	public void OnMaterialChanged()
	{
		this.m_isDirty = true;
		this.CacheShaderInformation();
	}

	// Token: 0x06001A28 RID: 6696 RVA: 0x00070928 File Offset: 0x0006EB28
	public void CacheShaderInformation()
	{
		if (!this.m_isDirty)
		{
			return;
		}
		Material sharedMaterial = this.m_renderer.sharedMaterial;
		ArrayList arrayList = new ArrayList();
		foreach (string text in LegacyColorFlashAnimator.SupportedProperties)
		{
			if (sharedMaterial.HasProperty(text))
			{
				arrayList.Add(text);
			}
		}
		if (arrayList.Count == 0)
		{
			base.enabled = false;
		}
		this.m_originalColors = new Color[arrayList.Count];
		this.m_colorPropertyNames = new string[arrayList.Count];
		int num = 0;
		foreach (object obj in arrayList)
		{
			string text2 = (string)obj;
			this.m_colorPropertyNames[num] = text2;
			this.m_originalColors[num] = sharedMaterial.GetColor(text2);
			num++;
		}
		this.m_isDirty = false;
	}

	// Token: 0x06001A29 RID: 6697 RVA: 0x00070A48 File Offset: 0x0006EC48
	protected override void AnimateIt(float value)
	{
		if (this.m_colorPropertyNames == null)
		{
			return;
		}
		for (int i = 0; i < this.m_colorPropertyNames.Length; i++)
		{
			Color color = Color.Lerp(this.m_originalColors[i], this.FlashColor, value);
			UberShaderAPI.SetColorCustom(this.m_renderer, color, this.m_colorPropertyNames[i], !this.IsInScene);
		}
	}

	// Token: 0x06001A2A RID: 6698 RVA: 0x00070AB5 File Offset: 0x0006ECB5
	public override void RestoreToOriginalState()
	{
		this.AnimateIt(0f);
	}

	// Token: 0x04001698 RID: 5784
	private Renderer m_renderer;

	// Token: 0x04001699 RID: 5785
	private Color[] m_originalColors;

	// Token: 0x0400169A RID: 5786
	private string[] m_colorPropertyNames;

	// Token: 0x0400169B RID: 5787
	private bool m_isDirty = true;

	// Token: 0x0400169C RID: 5788
	private static string[] SupportedProperties = new string[]
	{
		"_TintColor",
		"_Color",
		"_AdditiveTintColor"
	};

	// Token: 0x0400169D RID: 5789
	public Color FlashColor;
}
