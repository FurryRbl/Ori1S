using System;
using CatlikeCoding.TextBox;
using UnityEngine;

// Token: 0x02000130 RID: 304
public static class TextBoxExtended
{
	// Token: 0x06000C55 RID: 3157 RVA: 0x0003838C File Offset: 0x0003658C
	public static Rect GetRect(TextBox textBox)
	{
		Vector2 vector = TextBoxExtended.ComputeAnchor(textBox);
		float num = float.MaxValue;
		float num2 = 0f;
		int lineCount = textBox.LineCount;
		for (int i = 0; i < lineCount; i++)
		{
			try
			{
				TextBoxLine lineInfo = textBox.GetLineInfo(i);
				int num3 = lineInfo.lastCharIndex;
				int num4 = lineInfo.firstCharIndex;
				CharMetaData characterMetaData = textBox.GetCharacterMetaData(num3);
				while (characterMetaData.type != CharType.Visible && num3 > num4)
				{
					num3--;
					characterMetaData = textBox.GetCharacterMetaData(num3);
				}
				CharMetaData characterMetaData2 = textBox.GetCharacterMetaData(num4);
				while (characterMetaData.type != CharType.Visible && num4 < num3)
				{
					num4++;
					characterMetaData2 = textBox.GetCharacterMetaData(num4);
				}
				BitmapFontChar bitmapFontChar = characterMetaData.font[characterMetaData.id];
				num = Mathf.Min(num, characterMetaData2.positionInBox.x + vector.x);
				num2 = Mathf.Max(num2, characterMetaData.positionInBox.x + vector.x + bitmapFontChar.width * characterMetaData.scale);
			}
			catch (Exception ex)
			{
			}
		}
		float num5 = textBox.GetLineInfo(0).top + vector.y;
		float num6 = textBox.GetLineInfo(textBox.LineCount - 1).bottom + vector.y;
		return new Rect(num, num5, num2 - num, num6 - num5);
	}

	// Token: 0x06000C56 RID: 3158 RVA: 0x00038514 File Offset: 0x00036714
	public static Vector2 ComputeAnchor(TextBox textBox)
	{
		HorizontalAnchorMode horizontalAnchor = textBox.horizontalAnchor;
		Vector2 result;
		if (horizontalAnchor != HorizontalAnchorMode.Left)
		{
			if (horizontalAnchor != HorizontalAnchorMode.Center)
			{
				result.x = -textBox.width;
			}
			else
			{
				result.x = textBox.width * -0.5f;
			}
		}
		else
		{
			result.x = 0f;
		}
		VerticalAnchorMode verticalAnchor = textBox.verticalAnchor;
		if (verticalAnchor != VerticalAnchorMode.Top)
		{
			if (verticalAnchor != VerticalAnchorMode.Middle)
			{
				result.y = textBox.boundsTop - textBox.boundsBottom;
			}
			else
			{
				result.y = (textBox.boundsTop - textBox.boundsBottom) * 0.5f;
			}
		}
		else
		{
			result.y = 0f;
		}
		return result;
	}

	// Token: 0x06000C57 RID: 3159 RVA: 0x000385DC File Offset: 0x000367DC
	public static int CountLetters(TextBox textBox)
	{
		if (textBox.LineCount == 0)
		{
			return 0;
		}
		int lastCharIndex = textBox.GetLineInfo(textBox.LineCount - 1).lastCharIndex;
		if (lastCharIndex == -1)
		{
			return 0;
		}
		return textBox.GetCharacterMetaData(lastCharIndex).unstyledIndex;
	}
}
