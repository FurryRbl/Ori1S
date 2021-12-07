using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace CatlikeCoding.TextBox
{
	// Token: 0x0200012F RID: 303
	[PooledSafe]
	[ExecuteInEditMode]
	public sealed class TextBox : MonoBehaviour
	{
		// Token: 0x17000265 RID: 613
		// (get) Token: 0x06000C34 RID: 3124 RVA: 0x00036C3E File Offset: 0x00034E3E
		public int LineCount
		{
			get
			{
				return this.lines.Count;
			}
		}

		// Token: 0x17000266 RID: 614
		// (get) Token: 0x06000C35 RID: 3125 RVA: 0x00036C4B File Offset: 0x00034E4B
		public string DefaultText
		{
			get
			{
				return this.defaultText;
			}
		}

		// Token: 0x17000267 RID: 615
		// (get) Token: 0x06000C36 RID: 3126 RVA: 0x00036C53 File Offset: 0x00034E53
		// (set) Token: 0x06000C37 RID: 3127 RVA: 0x00036C5B File Offset: 0x00034E5B
		public float TabSize
		{
			get
			{
				return this.tabSize;
			}
			set
			{
				this.tabSize = ((value >= 0.001f) ? value : 0.001f);
			}
		}

		// Token: 0x06000C38 RID: 3128 RVA: 0x00036C7C File Offset: 0x00034E7C
		public void SetStyleCollection(TextStyleCollection textStyleCollection)
		{
			if (textStyleCollection != this.styleCollection)
			{
				this.styleCollection = textStyleCollection;
				if (this.textRenderers != null)
				{
					foreach (TextRenderer textRenderer in this.textRenderers)
					{
						if (Application.isPlaying)
						{
							InstantiateUtility.Destroy(textRenderer.gameObject);
						}
						else
						{
							UnityEngine.Object.DestroyImmediate(textRenderer.gameObject);
						}
					}
				}
				this.textRenderers = this.styleCollection.CreateRenderers(this);
			}
		}

		// Token: 0x17000268 RID: 616
		// (get) Token: 0x06000C39 RID: 3129 RVA: 0x00036D04 File Offset: 0x00034F04
		public int TextRendererCount
		{
			get
			{
				if (this.textRenderers == null || this.textRenderers.Length == 0)
				{
					if (this.styleCollection == null)
					{
						return 0;
					}
					this.textRenderers = this.styleCollection.CreateRenderers(this);
				}
				return this.textRenderers.Length;
			}
		}

		// Token: 0x06000C3A RID: 3130 RVA: 0x00036D56 File Offset: 0x00034F56
		public CharMetaData GetCharacterMetaData(int index)
		{
			return this.charMetaData[index];
		}

		// Token: 0x06000C3B RID: 3131 RVA: 0x00036D69 File Offset: 0x00034F69
		public void ResetTextRenderers()
		{
			this.textRenderers = null;
		}

		// Token: 0x06000C3C RID: 3132 RVA: 0x00036D72 File Offset: 0x00034F72
		public TextBoxLine GetLineInfo(int index)
		{
			return this.lines[index];
		}

		// Token: 0x06000C3D RID: 3133 RVA: 0x00036D80 File Offset: 0x00034F80
		public float GetLeftContour(float height)
		{
			float contourOffset = TextBox.GetContourOffset(this.leftContour, height);
			return (contourOffset >= 0f) ? (contourOffset + this.paddingLeft) : this.paddingLeft;
		}

		// Token: 0x06000C3E RID: 3134 RVA: 0x00036DB8 File Offset: 0x00034FB8
		public float GetRightContour(float height)
		{
			float contourOffset = TextBox.GetContourOffset(this.rightContour, height);
			return (contourOffset <= 0f) ? (contourOffset - this.paddingRight) : (-this.paddingRight);
		}

		// Token: 0x06000C3F RID: 3135 RVA: 0x00036DF1 File Offset: 0x00034FF1
		public void RenderText()
		{
			if (this.overflowFromBox == null)
			{
				this.RefreshText();
			}
			else
			{
				this.overflowFromBox.RenderText();
			}
		}

		// Token: 0x06000C40 RID: 3136 RVA: 0x00036E1C File Offset: 0x0003501C
		public void SetText(StringBuilder text)
		{
			if (text == null)
			{
				this.SetText(string.Empty);
				return;
			}
			this.firstCharIndex = 0;
			this.SetTextLength(text.Length);
			for (int i = 0; i < this.textLength; i++)
			{
				this.charMetaData[i].id = text[i];
			}
		}

		// Token: 0x06000C41 RID: 3137 RVA: 0x00036E80 File Offset: 0x00035080
		public void SetText(string text)
		{
			if (text == null)
			{
				text = string.Empty;
			}
			this.firstCharIndex = 0;
			this.SetTextLength(text.Length);
			for (int i = 0; i < this.textLength; i++)
			{
				this.charMetaData[i].id = text[i];
			}
		}

		// Token: 0x06000C42 RID: 3138 RVA: 0x00036EDC File Offset: 0x000350DC
		public TextRenderer GetTextRenderer(int index)
		{
			if (this.textRenderers == null || this.textRenderers.Length == 0)
			{
				if (this.styleCollection == null)
				{
					return null;
				}
				this.textRenderers = this.styleCollection.CreateRenderers(this);
			}
			if (this.textRenderers == null)
			{
				return null;
			}
			return this.textRenderers[index];
		}

		// Token: 0x06000C43 RID: 3139 RVA: 0x00036F3B File Offset: 0x0003513B
		private void OnEnable()
		{
			if (this.charMetaData == null)
			{
				this.SetText(this.defaultText);
				this.RenderText();
			}
		}

		// Token: 0x06000C44 RID: 3140 RVA: 0x00036F5C File Offset: 0x0003515C
		public void CreateRendersIfThereAreNone()
		{
			if (this.textRenderers == null || this.textRenderers.Length == 0)
			{
				this.textRenderers = this.styleCollection.CreateRenderers(this);
			}
		}

		// Token: 0x06000C45 RID: 3141 RVA: 0x00036F94 File Offset: 0x00035194
		private void RefreshText()
		{
			if (this.overflowBox != null && this.overflowBox != this)
			{
				this.overflowBox.overflowFromBox = this;
			}
			if (this.styleCollection == null)
			{
				return;
			}
			if (this.leftContour == null || this.leftContour.Length == 0)
			{
				this.leftContour = new Vector2[]
				{
					Vector2.zero
				};
			}
			if (this.rightContour == null || this.rightContour.Length == 0)
			{
				this.rightContour = new Vector2[]
				{
					Vector2.zero
				};
			}
			this.CreateRendersIfThereAreNone();
			int num = this.textRenderers.Length;
			for (int i = 0; i < num; i++)
			{
				this.textRenderers[i].renderedCharCount = 0;
			}
			this.GenerateMetaData();
			if (this.alignment != AlignmentMode.Left)
			{
				if (this.alignment == AlignmentMode.Justify)
				{
					this.JustifyText();
				}
				else
				{
					this.AlignTextCenterOrRight();
				}
			}
			Vector2 offset = this.ComputeAnchorAndBounds();
			for (int j = 0; j < num; j++)
			{
				this.textRenderers[j].Prepare();
			}
			for (int k = this.firstCharIndex; k <= this.lastCharIndex; k++)
			{
				this.charMetaData[k].RenderIfVisible(offset);
			}
			for (int l = 0; l < num; l++)
			{
				this.textRenderers[l].Apply();
			}
		}

		// Token: 0x06000C46 RID: 3142 RVA: 0x00037128 File Offset: 0x00035328
		private static float GetContourOffset(Vector2[] contourData, float height)
		{
			int i;
			for (i = 0; i < contourData.Length; i++)
			{
				if (contourData[i].y < height)
				{
					break;
				}
			}
			float result;
			if (i == 0)
			{
				result = contourData[i].x;
			}
			else if (i == contourData.Length)
			{
				result = contourData[i - 1].x;
			}
			else
			{
				Vector2 vector = contourData[i - 1];
				Vector2 vector2 = contourData[i];
				float y = vector.y;
				float num = vector2.y - vector.y;
				float num2 = height - y;
				float num3 = num2 / num;
				float x = vector.x;
				float num4 = vector2.x - vector.x;
				result = x + num4 * num3;
			}
			return result;
		}

		// Token: 0x06000C47 RID: 3143 RVA: 0x000371FC File Offset: 0x000353FC
		private Vector2 ComputeAnchorAndBounds()
		{
			HorizontalAnchorMode horizontalAnchorMode = this.horizontalAnchor;
			Vector2 result;
			if (horizontalAnchorMode != HorizontalAnchorMode.Left)
			{
				if (horizontalAnchorMode != HorizontalAnchorMode.Center)
				{
					result.x = -this.width;
				}
				else
				{
					result.x = this.width * -0.5f;
				}
			}
			else
			{
				result.x = 0f;
			}
			VerticalAnchorMode verticalAnchorMode = this.verticalAnchor;
			if (verticalAnchorMode != VerticalAnchorMode.Top)
			{
				if (verticalAnchorMode != VerticalAnchorMode.Middle)
				{
					result.y = this.paddingBottom - this.boundsBottom;
				}
				else
				{
					result.y = (this.paddingBottom - this.boundsBottom) * 0.5f;
				}
			}
			else
			{
				result.y = 0f;
			}
			this.boundsLeft = result.x;
			this.boundsRight = result.x + this.width;
			this.boundsTop = result.y;
			this.boundsBottom += result.y;
			return result;
		}

		// Token: 0x06000C48 RID: 3144 RVA: 0x00037304 File Offset: 0x00035504
		private void SetText(CharMetaData[] text, int startIndex, int length)
		{
			this.firstCharIndex = startIndex;
			this.SetTextLength(length);
			for (int i = 0; i < this.textLength; i++)
			{
				this.charMetaData[i].id = text[i].id;
			}
		}

		// Token: 0x06000C49 RID: 3145 RVA: 0x00037353 File Offset: 0x00035553
		private void SetTextLength(int length)
		{
			this.textLength = length;
			if (this.charMetaData == null)
			{
				this.charMetaData = new CharMetaData[length];
			}
			else if (this.charMetaData.Length < length)
			{
				Array.Resize<CharMetaData>(ref this.charMetaData, length);
			}
		}

		// Token: 0x06000C4A RID: 3146 RVA: 0x00037394 File Offset: 0x00035594
		private void ApplyStyleStatement()
		{
			if (this.styleStatementBuffer.Length > 0 && this.styleStatementBuffer[0] == '/')
			{
				if (this.styleStack.Count > 0)
				{
					this.currentStyle = this.styleStack.Pop();
				}
				return;
			}
			TextStyle[] styles = this.styleCollection.styles;
			for (int i = 0; i < styles.Length; i++)
			{
				string name = styles[i].name;
				if (name.Length == this.styleStatementBuffer.Length)
				{
					bool flag = true;
					for (int j = 0; j < name.Length; j++)
					{
						if (name[j] != this.styleStatementBuffer[j])
						{
							flag = false;
							break;
						}
					}
					if (flag)
					{
						this.styleStack.Push(this.currentStyle);
						this.currentStyle.ApplyOnTop(styles[i]);
						if (styles[i].rendererId >= 0)
						{
							this.currentStyle.renderer = this.textRenderers[styles[i].rendererId];
						}
						return;
					}
				}
			}
		}

		// Token: 0x06000C4B RID: 3147 RVA: 0x000374B4 File Offset: 0x000356B4
		private int ParseStyleStatementAt(int index, int unstyledCharIndex, Vector2 caret)
		{
			this.charMetaData[index].MarkAsStyleStatement(unstyledCharIndex, caret);
			this.styleStatementBuffer.Length = 0;
			for (index++; index <= this.lastCharIndex; index++)
			{
				char c = this.charMetaData[index].MarkAsStyleStatement(unstyledCharIndex, caret);
				if (c == '>')
				{
					break;
				}
				this.styleStatementBuffer.Append(c);
			}
			this.ApplyStyleStatement();
			return index;
		}

		// Token: 0x06000C4C RID: 3148 RVA: 0x00037534 File Offset: 0x00035734
		private void GenerateMetaData()
		{
			this.lastCharIndex = this.textLength - 1;
			this.lines.Clear();
			this.styleStack.Clear();
			this.currentStyle.Apply(this.styleCollection.styles[0], this.textRenderers[0]);
			this.currentStyle.size = this.currentStyle.size * this.size;
			this.currentStyle.color = this.currentStyle.color * this.color;
			this.currentStyle.lineHeight = this.currentStyle.lineHeight * this.size;
			this.currentStyle.lineDescent.baseline = this.currentStyle.lineDescent.baseline * this.size;
			this.currentStyle.lineDescent.baselineToBottom = this.currentStyle.lineDescent.baselineToBottom * this.size;
			int i = 0;
			int num = -1;
			Vector2 vector;
			vector.x = (vector.y = 0f);
			if (this.firstCharIndex > 0)
			{
				while (i < this.firstCharIndex)
				{
					if (this.charMetaData[i].id == '<')
					{
						i = this.ParseStyleStatementAt(i, num, vector);
					}
					i++;
				}
			}
			float num2 = this.paddingBottom - this.maxHeight;
			BitmapFontChar bitmapFontChar = null;
			this.overflowed = false;
			LineDescent lineDescent2;
			LineDescent lineDescent = lineDescent2 = this.currentStyle.lineDescent;
			TextBoxLine item;
			item.firstCharIndex = (item.lastCharIndex = i);
			item.baseline = (vector.y = lineDescent2.baseline - this.paddingTop);
			item.top = -this.paddingTop;
			item.bottom = item.top + lineDescent2.baseline + lineDescent2.baselineToBottom;
			item.horizontalStart = (vector.x = this.GetLeftContour(item.baseline));
			item.horizontalEnd = this.width + this.GetRightContour(item.baseline);
			while (i <= this.lastCharIndex)
			{
				char id = this.charMetaData[i].id;
				if (id <= ' ')
				{
					this.charMetaData[i].MarkAsWhitespace(++num, vector, ref this.currentStyle);
					if (bitmapFontChar != null)
					{
						lineDescent = lineDescent2;
					}
					bitmapFontChar = null;
					if (id == ' ')
					{
						vector.x += this.currentStyle.size * this.currentStyle.font.spaceAdvance;
					}
					else if (id == '\n')
					{
						item.lastCharIndex = i;
						this.lines.Add(item);
						lineDescent2 = this.currentStyle.lineDescent;
						item.firstCharIndex = i + 1;
						item.top = item.bottom;
						item.bottom = item.top + lineDescent2.baseline + lineDescent2.baselineToBottom;
						if (item.bottom < num2)
						{
							this.OverFlow(i);
							break;
						}
						vector.y = (item.baseline = item.top + lineDescent2.baseline);
						vector.x = (item.horizontalStart = this.GetLeftContour(vector.y));
						item.horizontalEnd = this.width + this.GetRightContour(vector.y);
					}
					else if (id == '\t')
					{
						vector.x = (1f + (float)((int)(vector.x / this.tabSize))) * this.tabSize;
					}
				}
				else if (id == '<')
				{
					i = this.ParseStyleStatementAt(i, num, vector);
					LineDescent lineDescent3;
					this.wordCache.Add(lineDescent3 = this.currentStyle.lineDescent);
					if (lineDescent2.baseline > lineDescent3.baseline)
					{
						this.AdjustBaseline(i, lineDescent3.baseline - lineDescent2.baseline, ref vector, ref item);
						lineDescent2.baseline = lineDescent3.baseline;
					}
					if (lineDescent2.baselineToBottom > lineDescent3.baselineToBottom)
					{
						lineDescent2.baselineToBottom = lineDescent3.baselineToBottom;
					}
					item.bottom = item.top + lineDescent2.baseline + lineDescent2.baselineToBottom;
				}
				else
				{
					if (bitmapFontChar == null)
					{
						this.wordCache.Clear();
						this.wordCache.Add(this.currentStyle.lineDescent);
					}
					else
					{
						vector.x += this.currentStyle.size * (this.currentStyle.letterSpacing + bitmapFontChar.GetKerning(id));
					}
					bitmapFontChar = this.charMetaData[i].MarkAsVisible(++num, vector, ref this.currentStyle);
					if (bitmapFontChar.height == 0f)
					{
						bitmapFontChar.width = this.currentStyle.font['□'].width;
						bitmapFontChar.advance = this.currentStyle.font['□'].advance;
						bitmapFontChar.xOffset = this.currentStyle.font['□'].xOffset;
					}
					vector.x += bitmapFontChar.advance * this.currentStyle.size;
					if (vector.x > item.horizontalEnd || item.bottom < num2)
					{
						int j = this.FindWrapStart(i, item.firstCharIndex);
						if (j >= 0)
						{
							if (lineDescent.baseline != lineDescent2.baseline)
							{
								this.AdjustBaseline(i + 1, lineDescent.baseline - lineDescent2.baseline, ref vector, ref item);
								lineDescent2.baseline = lineDescent.baseline;
							}
							lineDescent2.baselineToBottom = lineDescent.baselineToBottom;
							item.lastCharIndex = j - 1;
							item.bottom = item.top + lineDescent2.baseline + lineDescent2.baselineToBottom;
							this.lines.Add(item);
							item.firstCharIndex = j;
							item.top = item.bottom;
							lineDescent2 = this.FindLineDataForLastWord();
							item.baseline = item.top + lineDescent2.baseline;
							item.bottom = item.baseline + lineDescent2.baselineToBottom;
							if (item.bottom < num2)
							{
								lineDescent2.baselineToBottom = lineDescent.baselineToBottom;
								this.EraseVisibleCharacters(j, i);
								this.OverFlow(j);
								break;
							}
							item.horizontalStart = this.GetLeftContour(item.baseline);
							item.horizontalEnd = this.width + this.GetRightContour(item.baseline);
							Vector2 delta;
							delta.x = item.horizontalStart - this.charMetaData[j].positionInBox.x;
							delta.y = item.baseline - vector.y;
							while (j <= i)
							{
								this.charMetaData[j].AdjustPositionInBox(delta);
								j++;
							}
							vector.x += delta.x;
							vector.y = item.baseline;
						}
					}
				}
				i++;
			}
			this.boundsBottom = vector.y + lineDescent2.baselineToBottom - this.paddingBottom;
			if (!this.overflowed)
			{
				item.lastCharIndex = this.lastCharIndex;
				this.lines.Add(item);
				if (this.overflowBox != null && this.overflowBox != this)
				{
					this.overflowBox.SetText(this.charMetaData, 0, 0);
					this.overflowBox.RefreshText();
				}
			}
		}

		// Token: 0x06000C4D RID: 3149 RVA: 0x00037D00 File Offset: 0x00035F00
		private LineDescent FindLineDataForLastWord()
		{
			LineDescent result = this.wordCache[0];
			for (int i = 1; i < this.wordCache.Count; i++)
			{
				LineDescent lineDescent = this.wordCache[i];
				if (lineDescent.baseline > result.baseline)
				{
					result.baseline = lineDescent.baseline;
				}
				if (lineDescent.baselineToBottom > result.baselineToBottom)
				{
					result.baselineToBottom = lineDescent.baselineToBottom;
				}
			}
			return result;
		}

		// Token: 0x06000C4E RID: 3150 RVA: 0x00037D88 File Offset: 0x00035F88
		private void EraseVisibleCharacters(int startIndex, int endIndex)
		{
			while (startIndex <= endIndex)
			{
				this.charMetaData[startIndex++].EraseIfVisible();
			}
		}

		// Token: 0x06000C4F RID: 3151 RVA: 0x00037DB8 File Offset: 0x00035FB8
		private void OverFlow(int overflowIndex)
		{
			if (this.overflowBox != null && this.overflowBox != this)
			{
				this.overflowBox.SetText(this.charMetaData, overflowIndex, this.textLength);
				this.overflowBox.RefreshText();
			}
			this.lastCharIndex = overflowIndex - 1;
			this.overflowed = true;
		}

		// Token: 0x06000C50 RID: 3152 RVA: 0x00037E1C File Offset: 0x0003601C
		private void AdjustBaseline(int currentCharIndex, float baselineAdjustment, ref Vector2 caret, ref TextBoxLine line)
		{
			line.baseline += baselineAdjustment;
			float num = this.GetLeftContour(line.baseline);
			float num2 = num - line.horizontalStart;
			for (int i = line.firstCharIndex; i < currentCharIndex; i++)
			{
				this.charMetaData[i].AdjustPositionInBox(num2, baselineAdjustment);
			}
			caret.x += num2;
			caret.y += baselineAdjustment;
			line.horizontalStart = num;
			line.horizontalEnd = this.width + this.GetRightContour(line.baseline);
		}

		// Token: 0x06000C51 RID: 3153 RVA: 0x00037EBC File Offset: 0x000360BC
		private int FindWrapStart(int textIndex, int firstCharIndex)
		{
			if (textIndex <= firstCharIndex)
			{
				return -1;
			}
			int num = textIndex - 1;
			if (this.charMetaData[textIndex].id == '!' || this.charMetaData[textIndex].id == '%' || this.charMetaData[textIndex].id == ',' || this.charMetaData[textIndex].id == '.' || this.charMetaData[textIndex].id == ':' || this.charMetaData[textIndex].id == ';' || this.charMetaData[textIndex].id == '?')
			{
				num--;
			}
			while (this.charMetaData[num].type != CharType.Whitespace && this.charMetaData[num].id != '、' && this.charMetaData[num].id != '。' && this.charMetaData[num].id != '!' && this.charMetaData[num].id != '！' && this.charMetaData[num].id != '，' && this.charMetaData[num].id != '？')
			{
				if (this.charMetaData[num].id == ',' || this.charMetaData[num].id == '.')
				{
					List<int> list = new List<int>();
					list.AddRange(new int[]
					{
						48,
						49,
						50,
						51,
						52,
						53,
						54,
						55,
						56,
						57
					});
					if (!list.Contains((int)this.charMetaData[num + 1].id))
					{
						return num + 1;
					}
				}
				if (--num <= firstCharIndex)
				{
					return -1;
				}
			}
			return num + 1;
		}

		// Token: 0x06000C52 RID: 3154 RVA: 0x000380CC File Offset: 0x000362CC
		private void AlignTextCenterOrRight()
		{
			for (int i = 0; i < this.lines.Count; i++)
			{
				TextBoxLine textBoxLine = this.lines[i];
				int j = textBoxLine.firstCharIndex;
				int num = textBoxLine.lastCharIndex;
				int k;
				for (k = ((num > this.lastCharIndex) ? this.lastCharIndex : num); k >= j; k--)
				{
					if (this.charMetaData[k].type == CharType.Visible)
					{
						break;
					}
				}
				if (k >= j)
				{
					float num2 = textBoxLine.horizontalEnd - this.charMetaData[k].After;
					if (this.alignment == AlignmentMode.Center)
					{
						num2 *= 0.5f;
					}
					while (j <= num)
					{
						CharMetaData charMetaData = this.charMetaData[j];
						charMetaData.positionInBox.x = charMetaData.positionInBox.x + num2;
						this.charMetaData[j] = charMetaData;
						j++;
					}
				}
			}
		}

		// Token: 0x06000C53 RID: 3155 RVA: 0x000381E8 File Offset: 0x000363E8
		private void JustifyText()
		{
			for (int i = 0; i < this.lines.Count; i++)
			{
				TextBoxLine textBoxLine = this.lines[i];
				int first = textBoxLine.firstCharIndex;
				int num = textBoxLine.lastCharIndex;
				if ((this.overflowed || num < this.lastCharIndex) && this.charMetaData[num].id != '\n')
				{
					this.JustifyLine(first, num, textBoxLine.horizontalEnd);
				}
			}
		}

		// Token: 0x06000C54 RID: 3156 RVA: 0x0003826C File Offset: 0x0003646C
		private void JustifyLine(int first, int last, float width)
		{
			int num = last;
			while (num >= first && this.charMetaData[num].type != CharType.Visible)
			{
				num--;
			}
			float after = this.charMetaData[num].After;
			int num2 = 0;
			for (int i = first; i < num; i++)
			{
				if (this.charMetaData[i].type == CharType.Whitespace)
				{
					num2++;
				}
			}
			if (num2 <= 0)
			{
				return;
			}
			float num3 = (width - after) / (float)num2;
			float num4 = 0f;
			if (num3 > 0f)
			{
				for (int j = first; j <= num; j++)
				{
					CharMetaData[] array = this.charMetaData;
					int num5 = j;
					array[num5].positionInBox.x = array[num5].positionInBox.x + num4;
					if (this.charMetaData[j].type == CharType.Whitespace)
					{
						num4 += num3;
					}
				}
				for (int k = num + 1; k <= last; k++)
				{
					this.charMetaData[k].positionInBox.x = width;
				}
			}
		}

		// Token: 0x040009FF RID: 2559
		public AlignmentMode alignment;

		// Token: 0x04000A00 RID: 2560
		public HorizontalAnchorMode horizontalAnchor;

		// Token: 0x04000A01 RID: 2561
		public VerticalAnchorMode verticalAnchor;

		// Token: 0x04000A02 RID: 2562
		[SerializeField]
		private string defaultText;

		// Token: 0x04000A03 RID: 2563
		[SerializeField]
		private float tabSize = 2f;

		// Token: 0x04000A04 RID: 2564
		public float width = 10f;

		// Token: 0x04000A05 RID: 2565
		public float paddingLeft;

		// Token: 0x04000A06 RID: 2566
		public float paddingRight;

		// Token: 0x04000A07 RID: 2567
		public float paddingTop;

		// Token: 0x04000A08 RID: 2568
		public float paddingBottom;

		// Token: 0x04000A09 RID: 2569
		[NonSerialized]
		public float boundsLeft;

		// Token: 0x04000A0A RID: 2570
		[NonSerialized]
		public float boundsRight;

		// Token: 0x04000A0B RID: 2571
		[NonSerialized]
		public float boundsTop;

		// Token: 0x04000A0C RID: 2572
		[NonSerialized]
		public float boundsBottom;

		// Token: 0x04000A0D RID: 2573
		public Vector2[] leftContour;

		// Token: 0x04000A0E RID: 2574
		public Vector2[] rightContour;

		// Token: 0x04000A0F RID: 2575
		public float maxHeight = 10f;

		// Token: 0x04000A10 RID: 2576
		public TextBox overflowBox;

		// Token: 0x04000A11 RID: 2577
		public TextStyleCollection styleCollection;

		// Token: 0x04000A12 RID: 2578
		private int textLength;

		// Token: 0x04000A13 RID: 2579
		private int firstCharIndex;

		// Token: 0x04000A14 RID: 2580
		private int lastCharIndex;

		// Token: 0x04000A15 RID: 2581
		private Stack<AppliedTextStyle> styleStack = new Stack<AppliedTextStyle>();

		// Token: 0x04000A16 RID: 2582
		private AppliedTextStyle currentStyle;

		// Token: 0x04000A17 RID: 2583
		private StringBuilder styleStatementBuffer = new StringBuilder();

		// Token: 0x04000A18 RID: 2584
		private List<LineDescent> wordCache = new List<LineDescent>();

		// Token: 0x04000A19 RID: 2585
		private List<TextBoxLine> lines = new List<TextBoxLine>();

		// Token: 0x04000A1A RID: 2586
		[NonSerialized]
		private CharMetaData[] charMetaData;

		// Token: 0x04000A1B RID: 2587
		[NonSerialized]
		public TextRenderer[] textRenderers;

		// Token: 0x04000A1C RID: 2588
		[NonSerialized]
		private TextBox overflowFromBox;

		// Token: 0x04000A1D RID: 2589
		private bool overflowed;

		// Token: 0x04000A1E RID: 2590
		public float size = 1f;

		// Token: 0x04000A1F RID: 2591
		public Color color = Color.white;
	}
}
