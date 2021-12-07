using System;
using CatlikeCoding.TextBox;
using UnityEngine;

// Token: 0x02000686 RID: 1670
[ExecuteInEditMode]
public class ScaleToTextBox : MonoBehaviour
{
	// Token: 0x0600288D RID: 10381 RVA: 0x000B0433 File Offset: 0x000AE633
	public void FixedUpdate()
	{
		this.UpdateSize();
	}

	// Token: 0x0600288E RID: 10382 RVA: 0x000B043C File Offset: 0x000AE63C
	public void UpdateSize()
	{
		if (this.TextBox == null)
		{
			return;
		}
		Transform transform = this.TextBox.transform;
		Rect rect = TextBoxExtended.GetRect(this.TextBox);
		rect.xMin -= this.TopLeftPadding.x;
		rect.yMin += this.TopLeftPadding.y;
		rect.xMax += this.BottomRightPadding.x;
		rect.yMax -= this.BottomRightPadding.y;
		if (-rect.height < this.MinHeight)
		{
			Vector2 center = rect.center;
			rect.height = -this.MinHeight;
			rect.center = center;
		}
		if (rect.width < this.MinWidth)
		{
			Vector2 center2 = rect.center;
			rect.width = this.MinWidth;
			rect.center = center2;
		}
		this.Background.localScale = Vector3.Scale(transform.localScale, new Vector3(rect.width, rect.height, 1f));
		this.Background.localPosition = transform.localPosition + Vector3.Scale(transform.localScale, new Vector3(rect.x, rect.y, 0f));
	}

	// Token: 0x04002411 RID: 9233
	public Transform Background;

	// Token: 0x04002412 RID: 9234
	public TextBox TextBox;

	// Token: 0x04002413 RID: 9235
	public Vector2 TopLeftPadding;

	// Token: 0x04002414 RID: 9236
	public Vector2 BottomRightPadding;

	// Token: 0x04002415 RID: 9237
	public float MinWidth;

	// Token: 0x04002416 RID: 9238
	public float MinHeight;
}
