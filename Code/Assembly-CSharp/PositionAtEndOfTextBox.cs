using System;
using CatlikeCoding.TextBox;
using UnityEngine;

// Token: 0x0200012E RID: 302
public class PositionAtEndOfTextBox : MonoBehaviour
{
	// Token: 0x06000C32 RID: 3122 RVA: 0x00036B90 File Offset: 0x00034D90
	public void FixedUpdate()
	{
		Rect rect = TextBoxExtended.GetRect(this.TextBox);
		base.transform.localPosition = new Vector3(rect.xMax, rect.yMax);
	}

	// Token: 0x040009FE RID: 2558
	public TextBox TextBox;
}
