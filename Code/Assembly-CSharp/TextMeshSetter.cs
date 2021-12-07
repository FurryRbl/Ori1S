using System;
using CatlikeCoding.TextBox;
using UnityEngine;

// Token: 0x020001EE RID: 494
public class TextMeshSetter : MonoBehaviour
{
	// Token: 0x060010F1 RID: 4337 RVA: 0x0004D56A File Offset: 0x0004B76A
	public void Awake()
	{
		this.m_textBox = base.GetComponent<TextBox>();
	}

	// Token: 0x060010F2 RID: 4338 RVA: 0x0004D578 File Offset: 0x0004B778
	public void OnEnable()
	{
		this.SetText(this.StringValueProvider.GetStringValue());
	}

	// Token: 0x060010F3 RID: 4339 RVA: 0x0004D58B File Offset: 0x0004B78B
	public void FixedUpdate()
	{
		this.SetText(this.StringValueProvider.GetStringValue());
	}

	// Token: 0x060010F4 RID: 4340 RVA: 0x0004D5A0 File Offset: 0x0004B7A0
	public void SetText(string s)
	{
		if (this.m_previousString == s)
		{
			return;
		}
		this.m_previousString = s;
		this.m_textBox.SetText(s);
		this.m_textBox.RenderText();
	}

	// Token: 0x04000EB0 RID: 3760
	public StringValueProvider StringValueProvider;

	// Token: 0x04000EB1 RID: 3761
	private TextBox m_textBox;

	// Token: 0x04000EB2 RID: 3762
	private string m_previousString;
}
