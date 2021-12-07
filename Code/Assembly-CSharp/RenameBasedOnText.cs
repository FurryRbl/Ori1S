using System;
using UnityEngine;

// Token: 0x02000993 RID: 2451
[ExecuteInEditMode]
public class RenameBasedOnText : MonoBehaviour
{
	// Token: 0x06003588 RID: 13704 RVA: 0x000E0848 File Offset: 0x000DEA48
	private void Update()
	{
		if (this.m_textMesh == null)
		{
			this.m_textMesh = base.GetComponent<TextMesh>();
		}
		if (this.m_textMesh == null)
		{
			return;
		}
		if (this.m_lastText != this.m_textMesh.text)
		{
			base.name = this.m_textMesh.text;
			this.m_lastText = this.m_textMesh.text;
		}
	}

	// Token: 0x04003020 RID: 12320
	private TextMesh m_textMesh;

	// Token: 0x04003021 RID: 12321
	private string m_lastText = string.Empty;
}
