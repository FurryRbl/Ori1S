using System;
using System.Collections.Generic;
using UnityEngine;

// Token: 0x020003BB RID: 955
public class LegacyUnityTextFadeWordsAnimator : LegacyAnimator
{
	// Token: 0x06001A87 RID: 6791 RVA: 0x00072398 File Offset: 0x00070598
	private string ColorToHex(Color32 color)
	{
		string text = color.r.ToString("X2") + color.g.ToString("X2") + color.b.ToString("X2") + color.a.ToString("X2");
		return text.ToLower();
	}

	// Token: 0x06001A88 RID: 6792 RVA: 0x000723F8 File Offset: 0x000705F8
	public override void Start()
	{
		this.m_textMesh = base.GetComponent<TextMesh>();
		string text = this.m_textMesh.text;
		bool flag = false;
		string text2 = string.Empty;
		foreach (char c in text)
		{
			if (char.IsWhiteSpace(c) && flag)
			{
				flag = false;
				this.m_text.Add(text2);
				text2 = string.Empty;
			}
			else
			{
				flag = true;
			}
			text2 += c;
		}
		if (text2.Length != 0)
		{
			this.m_text.Add(text2);
		}
		base.Start();
	}

	// Token: 0x06001A89 RID: 6793 RVA: 0x000724A4 File Offset: 0x000706A4
	protected override void AnimateIt(float value)
	{
		float num = value * (float)this.m_text.Count;
		int num2 = Mathf.FloorToInt(num);
		float t = this.WordFade.Evaluate(num - (float)num2);
		Color c = Color.Lerp(new Color(1f, 1f, 1f, 0f), Color.white, t);
		bool flag = false;
		string text = string.Empty;
		for (int i = 0; i < this.m_text.Count; i++)
		{
			string text2 = this.m_text[i];
			if (i == num2)
			{
				string text3 = text;
				text = string.Concat(new string[]
				{
					text3,
					"<color=#",
					this.ColorToHex(c),
					">",
					text2,
					"</color><color=#00000000>"
				});
				flag = true;
			}
			else
			{
				text += text2;
			}
		}
		if (flag)
		{
			text += "</color>";
		}
		this.m_textMesh.text = text;
	}

	// Token: 0x06001A8A RID: 6794 RVA: 0x000725B4 File Offset: 0x000707B4
	public override void RestoreToOriginalState()
	{
		string text = string.Empty;
		foreach (string str in this.m_text)
		{
			text += str;
		}
		this.m_textMesh.text = text;
	}

	// Token: 0x04001706 RID: 5894
	private TextMesh m_textMesh;

	// Token: 0x04001707 RID: 5895
	public AnimationCurve WordFade;

	// Token: 0x04001708 RID: 5896
	private readonly List<string> m_text = new List<string>();
}
