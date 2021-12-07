using System;
using System.Collections.Generic;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000216 RID: 534
	public class TextEditor
	{
		// Token: 0x06002116 RID: 8470 RVA: 0x00027174 File Offset: 0x00025374
		[RequiredByNativeCode]
		public TextEditor()
		{
		}

		// Token: 0x17000861 RID: 2145
		// (get) Token: 0x06002117 RID: 8471 RVA: 0x000271B0 File Offset: 0x000253B0
		// (set) Token: 0x06002118 RID: 8472 RVA: 0x000271B8 File Offset: 0x000253B8
		[Obsolete("Please use 'text' instead of 'content'", false)]
		public GUIContent content
		{
			get
			{
				return this.m_Content;
			}
			set
			{
				this.m_Content = value;
			}
		}

		// Token: 0x17000862 RID: 2146
		// (get) Token: 0x06002119 RID: 8473 RVA: 0x000271C4 File Offset: 0x000253C4
		// (set) Token: 0x0600211A RID: 8474 RVA: 0x000271D4 File Offset: 0x000253D4
		public string text
		{
			get
			{
				return this.m_Content.text;
			}
			set
			{
				this.m_Content.text = value;
				this.ClampTextIndex(ref this.m_CursorIndex);
				this.ClampTextIndex(ref this.m_SelectIndex);
			}
		}

		// Token: 0x17000863 RID: 2147
		// (get) Token: 0x0600211B RID: 8475 RVA: 0x00027208 File Offset: 0x00025408
		// (set) Token: 0x0600211C RID: 8476 RVA: 0x00027210 File Offset: 0x00025410
		public Rect position
		{
			get
			{
				return this.m_Position;
			}
			set
			{
				if (this.m_Position == value)
				{
					return;
				}
				this.m_Position = value;
				this.UpdateScrollOffset();
			}
		}

		// Token: 0x17000864 RID: 2148
		// (get) Token: 0x0600211D RID: 8477 RVA: 0x00027234 File Offset: 0x00025434
		// (set) Token: 0x0600211E RID: 8478 RVA: 0x0002723C File Offset: 0x0002543C
		public int cursorIndex
		{
			get
			{
				return this.m_CursorIndex;
			}
			set
			{
				int cursorIndex = this.m_CursorIndex;
				this.m_CursorIndex = value;
				this.ClampTextIndex(ref this.m_CursorIndex);
				if (this.m_CursorIndex != cursorIndex)
				{
					this.m_RevealCursor = true;
				}
			}
		}

		// Token: 0x17000865 RID: 2149
		// (get) Token: 0x0600211F RID: 8479 RVA: 0x00027278 File Offset: 0x00025478
		// (set) Token: 0x06002120 RID: 8480 RVA: 0x00027280 File Offset: 0x00025480
		public int selectIndex
		{
			get
			{
				return this.m_SelectIndex;
			}
			set
			{
				this.m_SelectIndex = value;
				this.ClampTextIndex(ref this.m_SelectIndex);
			}
		}

		// Token: 0x06002121 RID: 8481 RVA: 0x00027298 File Offset: 0x00025498
		private void ClearCursorPos()
		{
			this.hasHorizontalCursorPos = false;
			this.m_iAltCursorPos = -1;
		}

		// Token: 0x06002122 RID: 8482 RVA: 0x000272A8 File Offset: 0x000254A8
		public void OnFocus()
		{
			if (this.multiline)
			{
				int num = 0;
				this.selectIndex = num;
				this.cursorIndex = num;
			}
			else
			{
				this.SelectAll();
			}
			this.m_HasFocus = true;
		}

		// Token: 0x06002123 RID: 8483 RVA: 0x000272E4 File Offset: 0x000254E4
		public void OnLostFocus()
		{
			this.m_HasFocus = false;
			this.scrollOffset = Vector2.zero;
		}

		// Token: 0x06002124 RID: 8484 RVA: 0x000272F8 File Offset: 0x000254F8
		private void GrabGraphicalCursorPos()
		{
			if (!this.hasHorizontalCursorPos)
			{
				this.graphicalCursorPos = this.style.GetCursorPixelPosition(this.position, this.m_Content, this.cursorIndex);
				this.graphicalSelectCursorPos = this.style.GetCursorPixelPosition(this.position, this.m_Content, this.selectIndex);
				this.hasHorizontalCursorPos = false;
			}
		}

		// Token: 0x06002125 RID: 8485 RVA: 0x00027360 File Offset: 0x00025560
		public bool HandleKeyEvent(Event e)
		{
			this.InitKeyActions();
			EventModifiers modifiers = e.modifiers;
			e.modifiers &= ~EventModifiers.CapsLock;
			if (TextEditor.s_Keyactions.ContainsKey(e))
			{
				TextEditor.TextEditOp operation = TextEditor.s_Keyactions[e];
				this.PerformOperation(operation);
				e.modifiers = modifiers;
				return true;
			}
			e.modifiers = modifiers;
			return false;
		}

		// Token: 0x06002126 RID: 8486 RVA: 0x000273C0 File Offset: 0x000255C0
		public bool DeleteLineBack()
		{
			if (this.hasSelection)
			{
				this.DeleteSelection();
				return true;
			}
			int num = this.cursorIndex;
			int num2 = num;
			while (num2-- != 0)
			{
				if (this.text[num2] == '\n')
				{
					num = num2 + 1;
					break;
				}
			}
			if (num2 == -1)
			{
				num = 0;
			}
			if (this.cursorIndex != num)
			{
				this.m_Content.text = this.text.Remove(num, this.cursorIndex - num);
				int num3 = num;
				this.cursorIndex = num3;
				this.selectIndex = num3;
				return true;
			}
			return false;
		}

		// Token: 0x06002127 RID: 8487 RVA: 0x0002745C File Offset: 0x0002565C
		public bool DeleteWordBack()
		{
			if (this.hasSelection)
			{
				this.DeleteSelection();
				return true;
			}
			int num = this.FindEndOfPreviousWord(this.cursorIndex);
			if (this.cursorIndex != num)
			{
				this.m_Content.text = this.text.Remove(num, this.cursorIndex - num);
				int num2 = num;
				this.cursorIndex = num2;
				this.selectIndex = num2;
				return true;
			}
			return false;
		}

		// Token: 0x06002128 RID: 8488 RVA: 0x000274C8 File Offset: 0x000256C8
		public bool DeleteWordForward()
		{
			if (this.hasSelection)
			{
				this.DeleteSelection();
				return true;
			}
			int num = this.FindStartOfNextWord(this.cursorIndex);
			if (this.cursorIndex < this.text.Length)
			{
				this.m_Content.text = this.text.Remove(this.cursorIndex, num - this.cursorIndex);
				return true;
			}
			return false;
		}

		// Token: 0x06002129 RID: 8489 RVA: 0x00027534 File Offset: 0x00025734
		public bool Delete()
		{
			if (this.hasSelection)
			{
				this.DeleteSelection();
				return true;
			}
			if (this.cursorIndex < this.text.Length)
			{
				this.m_Content.text = this.text.Remove(this.cursorIndex, 1);
				return true;
			}
			return false;
		}

		// Token: 0x0600212A RID: 8490 RVA: 0x0002758C File Offset: 0x0002578C
		public bool CanPaste()
		{
			return GUIUtility.systemCopyBuffer.Length != 0;
		}

		// Token: 0x0600212B RID: 8491 RVA: 0x000275A0 File Offset: 0x000257A0
		public bool Backspace()
		{
			if (this.hasSelection)
			{
				this.DeleteSelection();
				return true;
			}
			if (this.cursorIndex > 0)
			{
				this.m_Content.text = this.text.Remove(this.cursorIndex - 1, 1);
				int num = this.cursorIndex - 1;
				this.cursorIndex = num;
				this.selectIndex = num;
				this.ClearCursorPos();
				return true;
			}
			return false;
		}

		// Token: 0x0600212C RID: 8492 RVA: 0x0002760C File Offset: 0x0002580C
		public void SelectAll()
		{
			this.cursorIndex = 0;
			this.selectIndex = this.text.Length;
			this.ClearCursorPos();
		}

		// Token: 0x0600212D RID: 8493 RVA: 0x00027638 File Offset: 0x00025838
		public void SelectNone()
		{
			this.selectIndex = this.cursorIndex;
			this.ClearCursorPos();
		}

		// Token: 0x17000866 RID: 2150
		// (get) Token: 0x0600212E RID: 8494 RVA: 0x0002764C File Offset: 0x0002584C
		public bool hasSelection
		{
			get
			{
				return this.cursorIndex != this.selectIndex;
			}
		}

		// Token: 0x17000867 RID: 2151
		// (get) Token: 0x0600212F RID: 8495 RVA: 0x00027660 File Offset: 0x00025860
		public string SelectedText
		{
			get
			{
				if (this.cursorIndex == this.selectIndex)
				{
					return string.Empty;
				}
				if (this.cursorIndex < this.selectIndex)
				{
					return this.text.Substring(this.cursorIndex, this.selectIndex - this.cursorIndex);
				}
				return this.text.Substring(this.selectIndex, this.cursorIndex - this.selectIndex);
			}
		}

		// Token: 0x06002130 RID: 8496 RVA: 0x000276D4 File Offset: 0x000258D4
		public bool DeleteSelection()
		{
			if (this.cursorIndex == this.selectIndex)
			{
				return false;
			}
			if (this.cursorIndex < this.selectIndex)
			{
				this.m_Content.text = this.text.Substring(0, this.cursorIndex) + this.text.Substring(this.selectIndex, this.text.Length - this.selectIndex);
				this.selectIndex = this.cursorIndex;
			}
			else
			{
				this.m_Content.text = this.text.Substring(0, this.selectIndex) + this.text.Substring(this.cursorIndex, this.text.Length - this.cursorIndex);
				this.cursorIndex = this.selectIndex;
			}
			this.ClearCursorPos();
			return true;
		}

		// Token: 0x06002131 RID: 8497 RVA: 0x000277B4 File Offset: 0x000259B4
		public void ReplaceSelection(string replace)
		{
			this.DeleteSelection();
			this.m_Content.text = this.text.Insert(this.cursorIndex, replace);
			this.selectIndex = (this.cursorIndex += replace.Length);
			this.ClearCursorPos();
		}

		// Token: 0x06002132 RID: 8498 RVA: 0x00027808 File Offset: 0x00025A08
		public void Insert(char c)
		{
			this.ReplaceSelection(c.ToString());
		}

		// Token: 0x06002133 RID: 8499 RVA: 0x00027818 File Offset: 0x00025A18
		public void MoveSelectionToAltCursor()
		{
			if (this.m_iAltCursorPos == -1)
			{
				return;
			}
			int iAltCursorPos = this.m_iAltCursorPos;
			string selectedText = this.SelectedText;
			this.m_Content.text = this.text.Insert(iAltCursorPos, selectedText);
			if (iAltCursorPos < this.cursorIndex)
			{
				this.cursorIndex += selectedText.Length;
				this.selectIndex += selectedText.Length;
			}
			this.DeleteSelection();
			int num = iAltCursorPos;
			this.cursorIndex = num;
			this.selectIndex = num;
			this.ClearCursorPos();
		}

		// Token: 0x06002134 RID: 8500 RVA: 0x000278A8 File Offset: 0x00025AA8
		public void MoveRight()
		{
			this.ClearCursorPos();
			if (this.selectIndex == this.cursorIndex)
			{
				this.cursorIndex++;
				this.DetectFocusChange();
				this.selectIndex = this.cursorIndex;
			}
			else if (this.selectIndex > this.cursorIndex)
			{
				this.cursorIndex = this.selectIndex;
			}
			else
			{
				this.selectIndex = this.cursorIndex;
			}
		}

		// Token: 0x06002135 RID: 8501 RVA: 0x00027920 File Offset: 0x00025B20
		public void MoveLeft()
		{
			if (this.selectIndex == this.cursorIndex)
			{
				this.cursorIndex--;
				this.selectIndex = this.cursorIndex;
			}
			else if (this.selectIndex > this.cursorIndex)
			{
				this.selectIndex = this.cursorIndex;
			}
			else
			{
				this.cursorIndex = this.selectIndex;
			}
			this.ClearCursorPos();
		}

		// Token: 0x06002136 RID: 8502 RVA: 0x00027994 File Offset: 0x00025B94
		public void MoveUp()
		{
			if (this.selectIndex < this.cursorIndex)
			{
				this.selectIndex = this.cursorIndex;
			}
			else
			{
				this.cursorIndex = this.selectIndex;
			}
			this.GrabGraphicalCursorPos();
			this.graphicalCursorPos.y = this.graphicalCursorPos.y - 1f;
			int cursorStringIndex = this.style.GetCursorStringIndex(this.position, this.m_Content, this.graphicalCursorPos);
			this.selectIndex = cursorStringIndex;
			this.cursorIndex = cursorStringIndex;
			if (this.cursorIndex <= 0)
			{
				this.ClearCursorPos();
			}
		}

		// Token: 0x06002137 RID: 8503 RVA: 0x00027A2C File Offset: 0x00025C2C
		public void MoveDown()
		{
			if (this.selectIndex > this.cursorIndex)
			{
				this.selectIndex = this.cursorIndex;
			}
			else
			{
				this.cursorIndex = this.selectIndex;
			}
			this.GrabGraphicalCursorPos();
			this.graphicalCursorPos.y = this.graphicalCursorPos.y + (this.style.lineHeight + 5f);
			int cursorStringIndex = this.style.GetCursorStringIndex(this.position, this.m_Content, this.graphicalCursorPos);
			this.selectIndex = cursorStringIndex;
			this.cursorIndex = cursorStringIndex;
			if (this.cursorIndex == this.text.Length)
			{
				this.ClearCursorPos();
			}
		}

		// Token: 0x06002138 RID: 8504 RVA: 0x00027AD8 File Offset: 0x00025CD8
		public void MoveLineStart()
		{
			int num = (this.selectIndex >= this.cursorIndex) ? this.cursorIndex : this.selectIndex;
			int num2 = num;
			int num3;
			while (num2-- != 0)
			{
				if (this.text[num2] == '\n')
				{
					num3 = num2 + 1;
					this.cursorIndex = num3;
					this.selectIndex = num3;
					return;
				}
			}
			num3 = 0;
			this.cursorIndex = num3;
			this.selectIndex = num3;
		}

		// Token: 0x06002139 RID: 8505 RVA: 0x00027B50 File Offset: 0x00025D50
		public void MoveLineEnd()
		{
			int num = (this.selectIndex <= this.cursorIndex) ? this.cursorIndex : this.selectIndex;
			int i = num;
			int length = this.text.Length;
			int num2;
			while (i < length)
			{
				if (this.text[i] == '\n')
				{
					num2 = i;
					this.cursorIndex = num2;
					this.selectIndex = num2;
					return;
				}
				i++;
			}
			num2 = length;
			this.cursorIndex = num2;
			this.selectIndex = num2;
		}

		// Token: 0x0600213A RID: 8506 RVA: 0x00027BD4 File Offset: 0x00025DD4
		public void MoveGraphicalLineStart()
		{
			int graphicalLineStart = this.GetGraphicalLineStart((this.cursorIndex >= this.selectIndex) ? this.selectIndex : this.cursorIndex);
			this.selectIndex = graphicalLineStart;
			this.cursorIndex = graphicalLineStart;
		}

		// Token: 0x0600213B RID: 8507 RVA: 0x00027C18 File Offset: 0x00025E18
		public void MoveGraphicalLineEnd()
		{
			int graphicalLineEnd = this.GetGraphicalLineEnd((this.cursorIndex <= this.selectIndex) ? this.selectIndex : this.cursorIndex);
			this.selectIndex = graphicalLineEnd;
			this.cursorIndex = graphicalLineEnd;
		}

		// Token: 0x0600213C RID: 8508 RVA: 0x00027C5C File Offset: 0x00025E5C
		public void MoveTextStart()
		{
			int num = 0;
			this.cursorIndex = num;
			this.selectIndex = num;
		}

		// Token: 0x0600213D RID: 8509 RVA: 0x00027C7C File Offset: 0x00025E7C
		public void MoveTextEnd()
		{
			int length = this.text.Length;
			this.cursorIndex = length;
			this.selectIndex = length;
		}

		// Token: 0x0600213E RID: 8510 RVA: 0x00027CA4 File Offset: 0x00025EA4
		private int IndexOfEndOfLine(int startIndex)
		{
			int num = this.text.IndexOf('\n', startIndex);
			return (num == -1) ? this.text.Length : num;
		}

		// Token: 0x0600213F RID: 8511 RVA: 0x00027CD8 File Offset: 0x00025ED8
		public void MoveParagraphForward()
		{
			this.cursorIndex = ((this.cursorIndex <= this.selectIndex) ? this.selectIndex : this.cursorIndex);
			if (this.cursorIndex < this.text.Length)
			{
				int num = this.IndexOfEndOfLine(this.cursorIndex + 1);
				this.cursorIndex = num;
				this.selectIndex = num;
			}
		}

		// Token: 0x06002140 RID: 8512 RVA: 0x00027D40 File Offset: 0x00025F40
		public void MoveParagraphBackward()
		{
			this.cursorIndex = ((this.cursorIndex >= this.selectIndex) ? this.selectIndex : this.cursorIndex);
			if (this.cursorIndex > 1)
			{
				int num = this.text.LastIndexOf('\n', this.cursorIndex - 2) + 1;
				this.cursorIndex = num;
				this.selectIndex = num;
			}
			else
			{
				int num = 0;
				this.cursorIndex = num;
				this.selectIndex = num;
			}
		}

		// Token: 0x06002141 RID: 8513 RVA: 0x00027DBC File Offset: 0x00025FBC
		public void MoveCursorToPosition(Vector2 cursorPosition)
		{
			this.selectIndex = this.style.GetCursorStringIndex(this.position, this.m_Content, cursorPosition + this.scrollOffset);
			if (!Event.current.shift)
			{
				this.cursorIndex = this.selectIndex;
			}
			this.DetectFocusChange();
		}

		// Token: 0x06002142 RID: 8514 RVA: 0x00027E14 File Offset: 0x00026014
		public void MoveAltCursorToPosition(Vector2 cursorPosition)
		{
			int cursorStringIndex = this.style.GetCursorStringIndex(this.position, this.m_Content, cursorPosition + this.scrollOffset);
			this.m_iAltCursorPos = Mathf.Min(this.text.Length, cursorStringIndex);
			this.DetectFocusChange();
		}

		// Token: 0x06002143 RID: 8515 RVA: 0x00027E64 File Offset: 0x00026064
		public bool IsOverSelection(Vector2 cursorPosition)
		{
			int cursorStringIndex = this.style.GetCursorStringIndex(this.position, this.m_Content, cursorPosition + this.scrollOffset);
			return cursorStringIndex < Mathf.Max(this.cursorIndex, this.selectIndex) && cursorStringIndex > Mathf.Min(this.cursorIndex, this.selectIndex);
		}

		// Token: 0x06002144 RID: 8516 RVA: 0x00027EC4 File Offset: 0x000260C4
		public void SelectToPosition(Vector2 cursorPosition)
		{
			if (!this.m_MouseDragSelectsWholeWords)
			{
				this.cursorIndex = this.style.GetCursorStringIndex(this.position, this.m_Content, cursorPosition + this.scrollOffset);
			}
			else
			{
				int num = this.style.GetCursorStringIndex(this.position, this.m_Content, cursorPosition + this.scrollOffset);
				if (this.m_DblClickSnap == TextEditor.DblClickSnapping.WORDS)
				{
					if (num < this.m_DblClickInitPos)
					{
						this.cursorIndex = this.FindEndOfClassification(num, -1);
						this.selectIndex = this.FindEndOfClassification(this.m_DblClickInitPos, 1);
					}
					else
					{
						if (num >= this.text.Length)
						{
							num = this.text.Length - 1;
						}
						this.cursorIndex = this.FindEndOfClassification(num, 1);
						this.selectIndex = this.FindEndOfClassification(this.m_DblClickInitPos - 1, -1);
					}
				}
				else if (num < this.m_DblClickInitPos)
				{
					if (num > 0)
					{
						this.cursorIndex = this.text.LastIndexOf('\n', Mathf.Max(0, num - 2)) + 1;
					}
					else
					{
						this.cursorIndex = 0;
					}
					this.selectIndex = this.text.LastIndexOf('\n', this.m_DblClickInitPos);
				}
				else
				{
					if (num < this.text.Length)
					{
						this.cursorIndex = this.IndexOfEndOfLine(num);
					}
					else
					{
						this.cursorIndex = this.text.Length;
					}
					this.selectIndex = this.text.LastIndexOf('\n', Mathf.Max(0, this.m_DblClickInitPos - 2)) + 1;
				}
			}
		}

		// Token: 0x06002145 RID: 8517 RVA: 0x00028064 File Offset: 0x00026264
		public void SelectLeft()
		{
			if (this.m_bJustSelected && this.cursorIndex > this.selectIndex)
			{
				int cursorIndex = this.cursorIndex;
				this.cursorIndex = this.selectIndex;
				this.selectIndex = cursorIndex;
			}
			this.m_bJustSelected = false;
			this.cursorIndex--;
		}

		// Token: 0x06002146 RID: 8518 RVA: 0x000280BC File Offset: 0x000262BC
		public void SelectRight()
		{
			if (this.m_bJustSelected && this.cursorIndex < this.selectIndex)
			{
				int cursorIndex = this.cursorIndex;
				this.cursorIndex = this.selectIndex;
				this.selectIndex = cursorIndex;
			}
			this.m_bJustSelected = false;
			this.cursorIndex++;
		}

		// Token: 0x06002147 RID: 8519 RVA: 0x00028114 File Offset: 0x00026314
		public void SelectUp()
		{
			this.GrabGraphicalCursorPos();
			this.graphicalCursorPos.y = this.graphicalCursorPos.y - 1f;
			this.cursorIndex = this.style.GetCursorStringIndex(this.position, this.m_Content, this.graphicalCursorPos);
		}

		// Token: 0x06002148 RID: 8520 RVA: 0x00028164 File Offset: 0x00026364
		public void SelectDown()
		{
			this.GrabGraphicalCursorPos();
			this.graphicalCursorPos.y = this.graphicalCursorPos.y + (this.style.lineHeight + 5f);
			this.cursorIndex = this.style.GetCursorStringIndex(this.position, this.m_Content, this.graphicalCursorPos);
		}

		// Token: 0x06002149 RID: 8521 RVA: 0x000281C0 File Offset: 0x000263C0
		public void SelectTextEnd()
		{
			this.cursorIndex = this.text.Length;
		}

		// Token: 0x0600214A RID: 8522 RVA: 0x000281D4 File Offset: 0x000263D4
		public void SelectTextStart()
		{
			this.cursorIndex = 0;
		}

		// Token: 0x0600214B RID: 8523 RVA: 0x000281E0 File Offset: 0x000263E0
		public void MouseDragSelectsWholeWords(bool on)
		{
			this.m_MouseDragSelectsWholeWords = on;
			this.m_DblClickInitPos = this.cursorIndex;
		}

		// Token: 0x0600214C RID: 8524 RVA: 0x000281F8 File Offset: 0x000263F8
		public void DblClickSnap(TextEditor.DblClickSnapping snapping)
		{
			this.m_DblClickSnap = snapping;
		}

		// Token: 0x0600214D RID: 8525 RVA: 0x00028204 File Offset: 0x00026404
		private int GetGraphicalLineStart(int p)
		{
			Vector2 cursorPixelPosition = this.style.GetCursorPixelPosition(this.position, this.m_Content, p);
			cursorPixelPosition.x = 0f;
			return this.style.GetCursorStringIndex(this.position, this.m_Content, cursorPixelPosition);
		}

		// Token: 0x0600214E RID: 8526 RVA: 0x00028250 File Offset: 0x00026450
		private int GetGraphicalLineEnd(int p)
		{
			Vector2 cursorPixelPosition = this.style.GetCursorPixelPosition(this.position, this.m_Content, p);
			cursorPixelPosition.x += 5000f;
			return this.style.GetCursorStringIndex(this.position, this.m_Content, cursorPixelPosition);
		}

		// Token: 0x0600214F RID: 8527 RVA: 0x000282A4 File Offset: 0x000264A4
		private int FindNextSeperator(int startPos)
		{
			int length = this.text.Length;
			while (startPos < length && !TextEditor.isLetterLikeChar(this.text[startPos]))
			{
				startPos++;
			}
			while (startPos < length && TextEditor.isLetterLikeChar(this.text[startPos]))
			{
				startPos++;
			}
			return startPos;
		}

		// Token: 0x06002150 RID: 8528 RVA: 0x0002830C File Offset: 0x0002650C
		private static bool isLetterLikeChar(char c)
		{
			return char.IsLetterOrDigit(c) || c == '\'';
		}

		// Token: 0x06002151 RID: 8529 RVA: 0x00028324 File Offset: 0x00026524
		private int FindPrevSeperator(int startPos)
		{
			startPos--;
			while (startPos > 0 && !TextEditor.isLetterLikeChar(this.text[startPos]))
			{
				startPos--;
			}
			while (startPos >= 0 && TextEditor.isLetterLikeChar(this.text[startPos]))
			{
				startPos--;
			}
			return startPos + 1;
		}

		// Token: 0x06002152 RID: 8530 RVA: 0x00028388 File Offset: 0x00026588
		public void MoveWordRight()
		{
			this.cursorIndex = ((this.cursorIndex <= this.selectIndex) ? this.selectIndex : this.cursorIndex);
			int num = this.FindNextSeperator(this.cursorIndex);
			this.selectIndex = num;
			this.cursorIndex = num;
			this.ClearCursorPos();
		}

		// Token: 0x06002153 RID: 8531 RVA: 0x000283E0 File Offset: 0x000265E0
		public void MoveToStartOfNextWord()
		{
			this.ClearCursorPos();
			if (this.cursorIndex != this.selectIndex)
			{
				this.MoveRight();
				return;
			}
			int num = this.FindStartOfNextWord(this.cursorIndex);
			this.selectIndex = num;
			this.cursorIndex = num;
		}

		// Token: 0x06002154 RID: 8532 RVA: 0x00028428 File Offset: 0x00026628
		public void MoveToEndOfPreviousWord()
		{
			this.ClearCursorPos();
			if (this.cursorIndex != this.selectIndex)
			{
				this.MoveLeft();
				return;
			}
			int num = this.FindEndOfPreviousWord(this.cursorIndex);
			this.selectIndex = num;
			this.cursorIndex = num;
		}

		// Token: 0x06002155 RID: 8533 RVA: 0x00028470 File Offset: 0x00026670
		public void SelectToStartOfNextWord()
		{
			this.ClearCursorPos();
			this.cursorIndex = this.FindStartOfNextWord(this.cursorIndex);
		}

		// Token: 0x06002156 RID: 8534 RVA: 0x00028498 File Offset: 0x00026698
		public void SelectToEndOfPreviousWord()
		{
			this.ClearCursorPos();
			this.cursorIndex = this.FindEndOfPreviousWord(this.cursorIndex);
		}

		// Token: 0x06002157 RID: 8535 RVA: 0x000284C0 File Offset: 0x000266C0
		private TextEditor.CharacterType ClassifyChar(char c)
		{
			if (char.IsWhiteSpace(c))
			{
				return TextEditor.CharacterType.WhiteSpace;
			}
			if (char.IsLetterOrDigit(c) || c == '\'')
			{
				return TextEditor.CharacterType.LetterLike;
			}
			return TextEditor.CharacterType.Symbol;
		}

		// Token: 0x06002158 RID: 8536 RVA: 0x000284E8 File Offset: 0x000266E8
		public int FindStartOfNextWord(int p)
		{
			int length = this.text.Length;
			if (p == length)
			{
				return p;
			}
			char c = this.text[p];
			TextEditor.CharacterType characterType = this.ClassifyChar(c);
			if (characterType != TextEditor.CharacterType.WhiteSpace)
			{
				p++;
				while (p < length && this.ClassifyChar(this.text[p]) == characterType)
				{
					p++;
				}
			}
			else if (c == '\t' || c == '\n')
			{
				return p + 1;
			}
			if (p == length)
			{
				return p;
			}
			c = this.text[p];
			if (c == ' ')
			{
				while (p < length && char.IsWhiteSpace(this.text[p]))
				{
					p++;
				}
			}
			else if (c == '\t' || c == '\n')
			{
				return p;
			}
			return p;
		}

		// Token: 0x06002159 RID: 8537 RVA: 0x000285CC File Offset: 0x000267CC
		private int FindEndOfPreviousWord(int p)
		{
			if (p == 0)
			{
				return p;
			}
			p--;
			while (p > 0 && this.text[p] == ' ')
			{
				p--;
			}
			TextEditor.CharacterType characterType = this.ClassifyChar(this.text[p]);
			if (characterType != TextEditor.CharacterType.WhiteSpace)
			{
				while (p > 0 && this.ClassifyChar(this.text[p - 1]) == characterType)
				{
					p--;
				}
			}
			return p;
		}

		// Token: 0x0600215A RID: 8538 RVA: 0x00028650 File Offset: 0x00026850
		public void MoveWordLeft()
		{
			this.cursorIndex = ((this.cursorIndex >= this.selectIndex) ? this.selectIndex : this.cursorIndex);
			this.cursorIndex = this.FindPrevSeperator(this.cursorIndex);
			this.selectIndex = this.cursorIndex;
		}

		// Token: 0x0600215B RID: 8539 RVA: 0x000286A4 File Offset: 0x000268A4
		public void SelectWordRight()
		{
			this.ClearCursorPos();
			int selectIndex = this.selectIndex;
			if (this.cursorIndex < this.selectIndex)
			{
				this.selectIndex = this.cursorIndex;
				this.MoveWordRight();
				this.selectIndex = selectIndex;
				this.cursorIndex = ((this.cursorIndex >= this.selectIndex) ? this.selectIndex : this.cursorIndex);
				return;
			}
			this.selectIndex = this.cursorIndex;
			this.MoveWordRight();
			this.selectIndex = selectIndex;
		}

		// Token: 0x0600215C RID: 8540 RVA: 0x0002872C File Offset: 0x0002692C
		public void SelectWordLeft()
		{
			this.ClearCursorPos();
			int selectIndex = this.selectIndex;
			if (this.cursorIndex > this.selectIndex)
			{
				this.selectIndex = this.cursorIndex;
				this.MoveWordLeft();
				this.selectIndex = selectIndex;
				this.cursorIndex = ((this.cursorIndex <= this.selectIndex) ? this.selectIndex : this.cursorIndex);
				return;
			}
			this.selectIndex = this.cursorIndex;
			this.MoveWordLeft();
			this.selectIndex = selectIndex;
		}

		// Token: 0x0600215D RID: 8541 RVA: 0x000287B4 File Offset: 0x000269B4
		public void ExpandSelectGraphicalLineStart()
		{
			this.ClearCursorPos();
			if (this.cursorIndex < this.selectIndex)
			{
				this.cursorIndex = this.GetGraphicalLineStart(this.cursorIndex);
			}
			else
			{
				int cursorIndex = this.cursorIndex;
				this.cursorIndex = this.GetGraphicalLineStart(this.selectIndex);
				this.selectIndex = cursorIndex;
			}
		}

		// Token: 0x0600215E RID: 8542 RVA: 0x00028810 File Offset: 0x00026A10
		public void ExpandSelectGraphicalLineEnd()
		{
			this.ClearCursorPos();
			if (this.cursorIndex > this.selectIndex)
			{
				this.cursorIndex = this.GetGraphicalLineEnd(this.cursorIndex);
			}
			else
			{
				int cursorIndex = this.cursorIndex;
				this.cursorIndex = this.GetGraphicalLineEnd(this.selectIndex);
				this.selectIndex = cursorIndex;
			}
		}

		// Token: 0x0600215F RID: 8543 RVA: 0x0002886C File Offset: 0x00026A6C
		public void SelectGraphicalLineStart()
		{
			this.ClearCursorPos();
			this.cursorIndex = this.GetGraphicalLineStart(this.cursorIndex);
		}

		// Token: 0x06002160 RID: 8544 RVA: 0x00028894 File Offset: 0x00026A94
		public void SelectGraphicalLineEnd()
		{
			this.ClearCursorPos();
			this.cursorIndex = this.GetGraphicalLineEnd(this.cursorIndex);
		}

		// Token: 0x06002161 RID: 8545 RVA: 0x000288BC File Offset: 0x00026ABC
		public void SelectParagraphForward()
		{
			this.ClearCursorPos();
			bool flag = this.cursorIndex < this.selectIndex;
			if (this.cursorIndex < this.text.Length)
			{
				this.cursorIndex = this.IndexOfEndOfLine(this.cursorIndex + 1);
				if (flag && this.cursorIndex > this.selectIndex)
				{
					this.cursorIndex = this.selectIndex;
				}
			}
		}

		// Token: 0x06002162 RID: 8546 RVA: 0x0002892C File Offset: 0x00026B2C
		public void SelectParagraphBackward()
		{
			this.ClearCursorPos();
			bool flag = this.cursorIndex > this.selectIndex;
			if (this.cursorIndex > 1)
			{
				this.cursorIndex = this.text.LastIndexOf('\n', this.cursorIndex - 2) + 1;
				if (flag && this.cursorIndex < this.selectIndex)
				{
					this.cursorIndex = this.selectIndex;
				}
			}
			else
			{
				int num = 0;
				this.cursorIndex = num;
				this.selectIndex = num;
			}
		}

		// Token: 0x06002163 RID: 8547 RVA: 0x000289B0 File Offset: 0x00026BB0
		public void SelectCurrentWord()
		{
			this.ClearCursorPos();
			int length = this.text.Length;
			this.selectIndex = this.cursorIndex;
			if (length == 0)
			{
				return;
			}
			if (this.cursorIndex >= length)
			{
				this.cursorIndex = length - 1;
			}
			if (this.selectIndex >= length)
			{
				this.selectIndex--;
			}
			if (this.cursorIndex < this.selectIndex)
			{
				this.cursorIndex = this.FindEndOfClassification(this.cursorIndex, -1);
				this.selectIndex = this.FindEndOfClassification(this.selectIndex, 1);
			}
			else
			{
				this.cursorIndex = this.FindEndOfClassification(this.cursorIndex, 1);
				this.selectIndex = this.FindEndOfClassification(this.selectIndex, -1);
			}
			this.m_bJustSelected = true;
		}

		// Token: 0x06002164 RID: 8548 RVA: 0x00028A7C File Offset: 0x00026C7C
		private int FindEndOfClassification(int p, int dir)
		{
			int length = this.text.Length;
			if (p >= length || p < 0)
			{
				return p;
			}
			TextEditor.CharacterType characterType = this.ClassifyChar(this.text[p]);
			for (;;)
			{
				p += dir;
				if (p < 0)
				{
					break;
				}
				if (p >= length)
				{
					return length;
				}
				if (this.ClassifyChar(this.text[p]) != characterType)
				{
					goto Block_4;
				}
			}
			return 0;
			Block_4:
			if (dir == 1)
			{
				return p;
			}
			return p + 1;
		}

		// Token: 0x06002165 RID: 8549 RVA: 0x00028AF4 File Offset: 0x00026CF4
		public void SelectCurrentParagraph()
		{
			this.ClearCursorPos();
			int length = this.text.Length;
			if (this.cursorIndex < length)
			{
				this.cursorIndex = this.IndexOfEndOfLine(this.cursorIndex) + 1;
			}
			if (this.selectIndex != 0)
			{
				this.selectIndex = this.text.LastIndexOf('\n', this.selectIndex - 1) + 1;
			}
		}

		// Token: 0x06002166 RID: 8550 RVA: 0x00028B5C File Offset: 0x00026D5C
		public void UpdateScrollOffsetIfNeeded()
		{
			if (Event.current.type != EventType.Repaint && Event.current.type != EventType.Layout)
			{
				this.UpdateScrollOffset();
			}
		}

		// Token: 0x06002167 RID: 8551 RVA: 0x00028B90 File Offset: 0x00026D90
		private void UpdateScrollOffset()
		{
			int cursorIndex = this.cursorIndex;
			this.graphicalCursorPos = this.style.GetCursorPixelPosition(new Rect(0f, 0f, this.position.width, this.position.height), this.m_Content, cursorIndex);
			Rect rect = this.style.padding.Remove(this.position);
			Vector2 vector = new Vector2(this.style.CalcSize(this.m_Content).x, this.style.CalcHeight(this.m_Content, this.position.width));
			if (vector.x < this.position.width)
			{
				this.scrollOffset.x = 0f;
			}
			else if (this.m_RevealCursor)
			{
				if (this.graphicalCursorPos.x + 1f > this.scrollOffset.x + rect.width)
				{
					this.scrollOffset.x = this.graphicalCursorPos.x - rect.width;
				}
				if (this.graphicalCursorPos.x < this.scrollOffset.x + (float)this.style.padding.left)
				{
					this.scrollOffset.x = this.graphicalCursorPos.x - (float)this.style.padding.left;
				}
			}
			if (vector.y < rect.height)
			{
				this.scrollOffset.y = 0f;
			}
			else if (this.m_RevealCursor)
			{
				if (this.graphicalCursorPos.y + this.style.lineHeight > this.scrollOffset.y + rect.height + (float)this.style.padding.top)
				{
					this.scrollOffset.y = this.graphicalCursorPos.y - rect.height - (float)this.style.padding.top + this.style.lineHeight;
				}
				if (this.graphicalCursorPos.y < this.scrollOffset.y + (float)this.style.padding.top)
				{
					this.scrollOffset.y = this.graphicalCursorPos.y - (float)this.style.padding.top;
				}
			}
			if (this.scrollOffset.y > 0f && vector.y - this.scrollOffset.y < rect.height)
			{
				this.scrollOffset.y = vector.y - rect.height - (float)this.style.padding.top - (float)this.style.padding.bottom;
			}
			this.scrollOffset.y = ((this.scrollOffset.y >= 0f) ? this.scrollOffset.y : 0f);
			this.m_RevealCursor = false;
		}

		// Token: 0x06002168 RID: 8552 RVA: 0x00028ECC File Offset: 0x000270CC
		public void DrawCursor(string newText)
		{
			string text = this.text;
			int num = this.cursorIndex;
			if (Input.compositionString.Length > 0)
			{
				this.m_Content.text = newText.Substring(0, this.cursorIndex) + Input.compositionString + newText.Substring(this.selectIndex);
				num += Input.compositionString.Length;
			}
			else
			{
				this.m_Content.text = newText;
			}
			this.graphicalCursorPos = this.style.GetCursorPixelPosition(new Rect(0f, 0f, this.position.width, this.position.height), this.m_Content, num);
			Vector2 contentOffset = this.style.contentOffset;
			this.style.contentOffset -= this.scrollOffset;
			this.style.Internal_clipOffset = this.scrollOffset;
			Input.compositionCursorPos = this.graphicalCursorPos + new Vector2(this.position.x, this.position.y + this.style.lineHeight) - this.scrollOffset;
			if (Input.compositionString.Length > 0)
			{
				this.style.DrawWithTextSelection(this.position, this.m_Content, this.controlID, this.cursorIndex, this.cursorIndex + Input.compositionString.Length, true);
			}
			else
			{
				this.style.DrawWithTextSelection(this.position, this.m_Content, this.controlID, this.cursorIndex, this.selectIndex);
			}
			if (this.m_iAltCursorPos != -1)
			{
				this.style.DrawCursor(this.position, this.m_Content, this.controlID, this.m_iAltCursorPos);
			}
			this.style.contentOffset = contentOffset;
			this.style.Internal_clipOffset = Vector2.zero;
			this.m_Content.text = text;
		}

		// Token: 0x06002169 RID: 8553 RVA: 0x000290D4 File Offset: 0x000272D4
		private bool PerformOperation(TextEditor.TextEditOp operation)
		{
			this.m_RevealCursor = true;
			switch (operation)
			{
			case TextEditor.TextEditOp.MoveLeft:
				this.MoveLeft();
				return false;
			case TextEditor.TextEditOp.MoveRight:
				this.MoveRight();
				return false;
			case TextEditor.TextEditOp.MoveUp:
				this.MoveUp();
				return false;
			case TextEditor.TextEditOp.MoveDown:
				this.MoveDown();
				return false;
			case TextEditor.TextEditOp.MoveLineStart:
				this.MoveLineStart();
				return false;
			case TextEditor.TextEditOp.MoveLineEnd:
				this.MoveLineEnd();
				return false;
			case TextEditor.TextEditOp.MoveTextStart:
				this.MoveTextStart();
				return false;
			case TextEditor.TextEditOp.MoveTextEnd:
				this.MoveTextEnd();
				return false;
			case TextEditor.TextEditOp.MoveGraphicalLineStart:
				this.MoveGraphicalLineStart();
				return false;
			case TextEditor.TextEditOp.MoveGraphicalLineEnd:
				this.MoveGraphicalLineEnd();
				return false;
			case TextEditor.TextEditOp.MoveWordLeft:
				this.MoveWordLeft();
				return false;
			case TextEditor.TextEditOp.MoveWordRight:
				this.MoveWordRight();
				return false;
			case TextEditor.TextEditOp.MoveParagraphForward:
				this.MoveParagraphForward();
				return false;
			case TextEditor.TextEditOp.MoveParagraphBackward:
				this.MoveParagraphBackward();
				return false;
			case TextEditor.TextEditOp.MoveToStartOfNextWord:
				this.MoveToStartOfNextWord();
				return false;
			case TextEditor.TextEditOp.MoveToEndOfPreviousWord:
				this.MoveToEndOfPreviousWord();
				return false;
			case TextEditor.TextEditOp.SelectLeft:
				this.SelectLeft();
				return false;
			case TextEditor.TextEditOp.SelectRight:
				this.SelectRight();
				return false;
			case TextEditor.TextEditOp.SelectUp:
				this.SelectUp();
				return false;
			case TextEditor.TextEditOp.SelectDown:
				this.SelectDown();
				return false;
			case TextEditor.TextEditOp.SelectTextStart:
				this.SelectTextStart();
				return false;
			case TextEditor.TextEditOp.SelectTextEnd:
				this.SelectTextEnd();
				return false;
			case TextEditor.TextEditOp.ExpandSelectGraphicalLineStart:
				this.ExpandSelectGraphicalLineStart();
				return false;
			case TextEditor.TextEditOp.ExpandSelectGraphicalLineEnd:
				this.ExpandSelectGraphicalLineEnd();
				return false;
			case TextEditor.TextEditOp.SelectGraphicalLineStart:
				this.SelectGraphicalLineStart();
				return false;
			case TextEditor.TextEditOp.SelectGraphicalLineEnd:
				this.SelectGraphicalLineEnd();
				return false;
			case TextEditor.TextEditOp.SelectWordLeft:
				this.SelectWordLeft();
				return false;
			case TextEditor.TextEditOp.SelectWordRight:
				this.SelectWordRight();
				return false;
			case TextEditor.TextEditOp.SelectToEndOfPreviousWord:
				this.SelectToEndOfPreviousWord();
				return false;
			case TextEditor.TextEditOp.SelectToStartOfNextWord:
				this.SelectToStartOfNextWord();
				return false;
			case TextEditor.TextEditOp.SelectParagraphBackward:
				this.SelectParagraphBackward();
				return false;
			case TextEditor.TextEditOp.SelectParagraphForward:
				this.SelectParagraphForward();
				return false;
			case TextEditor.TextEditOp.Delete:
				return this.Delete();
			case TextEditor.TextEditOp.Backspace:
				return this.Backspace();
			case TextEditor.TextEditOp.DeleteWordBack:
				return this.DeleteWordBack();
			case TextEditor.TextEditOp.DeleteWordForward:
				return this.DeleteWordForward();
			case TextEditor.TextEditOp.DeleteLineBack:
				return this.DeleteLineBack();
			case TextEditor.TextEditOp.Cut:
				return this.Cut();
			case TextEditor.TextEditOp.Copy:
				this.Copy();
				return false;
			case TextEditor.TextEditOp.Paste:
				return this.Paste();
			case TextEditor.TextEditOp.SelectAll:
				this.SelectAll();
				return false;
			case TextEditor.TextEditOp.SelectNone:
				this.SelectNone();
				return false;
			}
			Debug.Log("Unimplemented: " + operation);
			return false;
		}

		// Token: 0x0600216A RID: 8554 RVA: 0x0002937C File Offset: 0x0002757C
		public void SaveBackup()
		{
			this.oldText = this.text;
			this.oldPos = this.cursorIndex;
			this.oldSelectPos = this.selectIndex;
		}

		// Token: 0x0600216B RID: 8555 RVA: 0x000293B0 File Offset: 0x000275B0
		public void Undo()
		{
			this.m_Content.text = this.oldText;
			this.cursorIndex = this.oldPos;
			this.selectIndex = this.oldSelectPos;
		}

		// Token: 0x0600216C RID: 8556 RVA: 0x000293DC File Offset: 0x000275DC
		public bool Cut()
		{
			if (this.isPasswordField)
			{
				return false;
			}
			this.Copy();
			return this.DeleteSelection();
		}

		// Token: 0x0600216D RID: 8557 RVA: 0x000293F8 File Offset: 0x000275F8
		public void Copy()
		{
			if (this.selectIndex == this.cursorIndex)
			{
				return;
			}
			if (this.isPasswordField)
			{
				return;
			}
			string systemCopyBuffer;
			if (this.cursorIndex < this.selectIndex)
			{
				systemCopyBuffer = this.text.Substring(this.cursorIndex, this.selectIndex - this.cursorIndex);
			}
			else
			{
				systemCopyBuffer = this.text.Substring(this.selectIndex, this.cursorIndex - this.selectIndex);
			}
			GUIUtility.systemCopyBuffer = systemCopyBuffer;
		}

		// Token: 0x0600216E RID: 8558 RVA: 0x00029480 File Offset: 0x00027680
		private static string ReplaceNewlinesWithSpaces(string value)
		{
			value = value.Replace("\r\n", " ");
			value = value.Replace('\n', ' ');
			value = value.Replace('\r', ' ');
			return value;
		}

		// Token: 0x0600216F RID: 8559 RVA: 0x000294B0 File Offset: 0x000276B0
		public bool Paste()
		{
			string text = GUIUtility.systemCopyBuffer;
			if (text != string.Empty)
			{
				if (!this.multiline)
				{
					text = TextEditor.ReplaceNewlinesWithSpaces(text);
				}
				this.ReplaceSelection(text);
				return true;
			}
			return false;
		}

		// Token: 0x06002170 RID: 8560 RVA: 0x000294F0 File Offset: 0x000276F0
		private static void MapKey(string key, TextEditor.TextEditOp action)
		{
			TextEditor.s_Keyactions[Event.KeyboardEvent(key)] = action;
		}

		// Token: 0x06002171 RID: 8561 RVA: 0x00029504 File Offset: 0x00027704
		private void InitKeyActions()
		{
			if (TextEditor.s_Keyactions != null)
			{
				return;
			}
			TextEditor.s_Keyactions = new Dictionary<Event, TextEditor.TextEditOp>();
			TextEditor.MapKey("left", TextEditor.TextEditOp.MoveLeft);
			TextEditor.MapKey("right", TextEditor.TextEditOp.MoveRight);
			TextEditor.MapKey("up", TextEditor.TextEditOp.MoveUp);
			TextEditor.MapKey("down", TextEditor.TextEditOp.MoveDown);
			TextEditor.MapKey("#left", TextEditor.TextEditOp.SelectLeft);
			TextEditor.MapKey("#right", TextEditor.TextEditOp.SelectRight);
			TextEditor.MapKey("#up", TextEditor.TextEditOp.SelectUp);
			TextEditor.MapKey("#down", TextEditor.TextEditOp.SelectDown);
			TextEditor.MapKey("delete", TextEditor.TextEditOp.Delete);
			TextEditor.MapKey("backspace", TextEditor.TextEditOp.Backspace);
			TextEditor.MapKey("#backspace", TextEditor.TextEditOp.Backspace);
			if (Application.platform == RuntimePlatform.OSXPlayer || Application.platform == RuntimePlatform.OSXWebPlayer || Application.platform == RuntimePlatform.OSXDashboardPlayer || Application.platform == RuntimePlatform.OSXEditor || (Application.platform == RuntimePlatform.WebGLPlayer && SystemInfo.operatingSystem.StartsWith("Mac")))
			{
				TextEditor.MapKey("^left", TextEditor.TextEditOp.MoveGraphicalLineStart);
				TextEditor.MapKey("^right", TextEditor.TextEditOp.MoveGraphicalLineEnd);
				TextEditor.MapKey("&left", TextEditor.TextEditOp.MoveWordLeft);
				TextEditor.MapKey("&right", TextEditor.TextEditOp.MoveWordRight);
				TextEditor.MapKey("&up", TextEditor.TextEditOp.MoveParagraphBackward);
				TextEditor.MapKey("&down", TextEditor.TextEditOp.MoveParagraphForward);
				TextEditor.MapKey("%left", TextEditor.TextEditOp.MoveGraphicalLineStart);
				TextEditor.MapKey("%right", TextEditor.TextEditOp.MoveGraphicalLineEnd);
				TextEditor.MapKey("%up", TextEditor.TextEditOp.MoveTextStart);
				TextEditor.MapKey("%down", TextEditor.TextEditOp.MoveTextEnd);
				TextEditor.MapKey("#home", TextEditor.TextEditOp.SelectTextStart);
				TextEditor.MapKey("#end", TextEditor.TextEditOp.SelectTextEnd);
				TextEditor.MapKey("#^left", TextEditor.TextEditOp.ExpandSelectGraphicalLineStart);
				TextEditor.MapKey("#^right", TextEditor.TextEditOp.ExpandSelectGraphicalLineEnd);
				TextEditor.MapKey("#^up", TextEditor.TextEditOp.SelectParagraphBackward);
				TextEditor.MapKey("#^down", TextEditor.TextEditOp.SelectParagraphForward);
				TextEditor.MapKey("#&left", TextEditor.TextEditOp.SelectWordLeft);
				TextEditor.MapKey("#&right", TextEditor.TextEditOp.SelectWordRight);
				TextEditor.MapKey("#&up", TextEditor.TextEditOp.SelectParagraphBackward);
				TextEditor.MapKey("#&down", TextEditor.TextEditOp.SelectParagraphForward);
				TextEditor.MapKey("#%left", TextEditor.TextEditOp.ExpandSelectGraphicalLineStart);
				TextEditor.MapKey("#%right", TextEditor.TextEditOp.ExpandSelectGraphicalLineEnd);
				TextEditor.MapKey("#%up", TextEditor.TextEditOp.SelectTextStart);
				TextEditor.MapKey("#%down", TextEditor.TextEditOp.SelectTextEnd);
				TextEditor.MapKey("%a", TextEditor.TextEditOp.SelectAll);
				TextEditor.MapKey("%x", TextEditor.TextEditOp.Cut);
				TextEditor.MapKey("%c", TextEditor.TextEditOp.Copy);
				TextEditor.MapKey("%v", TextEditor.TextEditOp.Paste);
				TextEditor.MapKey("^d", TextEditor.TextEditOp.Delete);
				TextEditor.MapKey("^h", TextEditor.TextEditOp.Backspace);
				TextEditor.MapKey("^b", TextEditor.TextEditOp.MoveLeft);
				TextEditor.MapKey("^f", TextEditor.TextEditOp.MoveRight);
				TextEditor.MapKey("^a", TextEditor.TextEditOp.MoveLineStart);
				TextEditor.MapKey("^e", TextEditor.TextEditOp.MoveLineEnd);
				TextEditor.MapKey("&delete", TextEditor.TextEditOp.DeleteWordForward);
				TextEditor.MapKey("&backspace", TextEditor.TextEditOp.DeleteWordBack);
				TextEditor.MapKey("%backspace", TextEditor.TextEditOp.DeleteLineBack);
			}
			else
			{
				TextEditor.MapKey("home", TextEditor.TextEditOp.MoveGraphicalLineStart);
				TextEditor.MapKey("end", TextEditor.TextEditOp.MoveGraphicalLineEnd);
				TextEditor.MapKey("%left", TextEditor.TextEditOp.MoveWordLeft);
				TextEditor.MapKey("%right", TextEditor.TextEditOp.MoveWordRight);
				TextEditor.MapKey("%up", TextEditor.TextEditOp.MoveParagraphBackward);
				TextEditor.MapKey("%down", TextEditor.TextEditOp.MoveParagraphForward);
				TextEditor.MapKey("^left", TextEditor.TextEditOp.MoveToEndOfPreviousWord);
				TextEditor.MapKey("^right", TextEditor.TextEditOp.MoveToStartOfNextWord);
				TextEditor.MapKey("^up", TextEditor.TextEditOp.MoveParagraphBackward);
				TextEditor.MapKey("^down", TextEditor.TextEditOp.MoveParagraphForward);
				TextEditor.MapKey("#^left", TextEditor.TextEditOp.SelectToEndOfPreviousWord);
				TextEditor.MapKey("#^right", TextEditor.TextEditOp.SelectToStartOfNextWord);
				TextEditor.MapKey("#^up", TextEditor.TextEditOp.SelectParagraphBackward);
				TextEditor.MapKey("#^down", TextEditor.TextEditOp.SelectParagraphForward);
				TextEditor.MapKey("#home", TextEditor.TextEditOp.SelectGraphicalLineStart);
				TextEditor.MapKey("#end", TextEditor.TextEditOp.SelectGraphicalLineEnd);
				TextEditor.MapKey("^delete", TextEditor.TextEditOp.DeleteWordForward);
				TextEditor.MapKey("^backspace", TextEditor.TextEditOp.DeleteWordBack);
				TextEditor.MapKey("%backspace", TextEditor.TextEditOp.DeleteLineBack);
				TextEditor.MapKey("^a", TextEditor.TextEditOp.SelectAll);
				TextEditor.MapKey("^x", TextEditor.TextEditOp.Cut);
				TextEditor.MapKey("^c", TextEditor.TextEditOp.Copy);
				TextEditor.MapKey("^v", TextEditor.TextEditOp.Paste);
				TextEditor.MapKey("#delete", TextEditor.TextEditOp.Cut);
				TextEditor.MapKey("^insert", TextEditor.TextEditOp.Copy);
				TextEditor.MapKey("#insert", TextEditor.TextEditOp.Paste);
			}
		}

		// Token: 0x06002172 RID: 8562 RVA: 0x000298E4 File Offset: 0x00027AE4
		public void DetectFocusChange()
		{
			if (this.m_HasFocus && this.controlID != GUIUtility.keyboardControl)
			{
				this.OnLostFocus();
			}
			if (!this.m_HasFocus && this.controlID == GUIUtility.keyboardControl)
			{
				this.OnFocus();
			}
		}

		// Token: 0x06002173 RID: 8563 RVA: 0x00029934 File Offset: 0x00027B34
		private void ClampTextIndex(ref int index)
		{
			index = Mathf.Clamp(index, 0, this.text.Length);
		}

		// Token: 0x04000845 RID: 2117
		public TouchScreenKeyboard keyboardOnScreen;

		// Token: 0x04000846 RID: 2118
		public int controlID;

		// Token: 0x04000847 RID: 2119
		public GUIStyle style = GUIStyle.none;

		// Token: 0x04000848 RID: 2120
		public bool multiline;

		// Token: 0x04000849 RID: 2121
		public bool hasHorizontalCursorPos;

		// Token: 0x0400084A RID: 2122
		public bool isPasswordField;

		// Token: 0x0400084B RID: 2123
		internal bool m_HasFocus;

		// Token: 0x0400084C RID: 2124
		public Vector2 scrollOffset = Vector2.zero;

		// Token: 0x0400084D RID: 2125
		private GUIContent m_Content = new GUIContent();

		// Token: 0x0400084E RID: 2126
		private Rect m_Position;

		// Token: 0x0400084F RID: 2127
		private int m_CursorIndex;

		// Token: 0x04000850 RID: 2128
		private int m_SelectIndex;

		// Token: 0x04000851 RID: 2129
		private bool m_RevealCursor;

		// Token: 0x04000852 RID: 2130
		public Vector2 graphicalCursorPos;

		// Token: 0x04000853 RID: 2131
		public Vector2 graphicalSelectCursorPos;

		// Token: 0x04000854 RID: 2132
		private bool m_MouseDragSelectsWholeWords;

		// Token: 0x04000855 RID: 2133
		private int m_DblClickInitPos;

		// Token: 0x04000856 RID: 2134
		private TextEditor.DblClickSnapping m_DblClickSnap;

		// Token: 0x04000857 RID: 2135
		private bool m_bJustSelected;

		// Token: 0x04000858 RID: 2136
		private int m_iAltCursorPos = -1;

		// Token: 0x04000859 RID: 2137
		private string oldText;

		// Token: 0x0400085A RID: 2138
		private int oldPos;

		// Token: 0x0400085B RID: 2139
		private int oldSelectPos;

		// Token: 0x0400085C RID: 2140
		private static Dictionary<Event, TextEditor.TextEditOp> s_Keyactions;

		// Token: 0x02000217 RID: 535
		public enum DblClickSnapping : byte
		{
			// Token: 0x0400085E RID: 2142
			WORDS,
			// Token: 0x0400085F RID: 2143
			PARAGRAPHS
		}

		// Token: 0x02000218 RID: 536
		private enum CharacterType
		{
			// Token: 0x04000861 RID: 2145
			LetterLike,
			// Token: 0x04000862 RID: 2146
			Symbol,
			// Token: 0x04000863 RID: 2147
			Symbol2,
			// Token: 0x04000864 RID: 2148
			WhiteSpace
		}

		// Token: 0x02000219 RID: 537
		private enum TextEditOp
		{
			// Token: 0x04000866 RID: 2150
			MoveLeft,
			// Token: 0x04000867 RID: 2151
			MoveRight,
			// Token: 0x04000868 RID: 2152
			MoveUp,
			// Token: 0x04000869 RID: 2153
			MoveDown,
			// Token: 0x0400086A RID: 2154
			MoveLineStart,
			// Token: 0x0400086B RID: 2155
			MoveLineEnd,
			// Token: 0x0400086C RID: 2156
			MoveTextStart,
			// Token: 0x0400086D RID: 2157
			MoveTextEnd,
			// Token: 0x0400086E RID: 2158
			MovePageUp,
			// Token: 0x0400086F RID: 2159
			MovePageDown,
			// Token: 0x04000870 RID: 2160
			MoveGraphicalLineStart,
			// Token: 0x04000871 RID: 2161
			MoveGraphicalLineEnd,
			// Token: 0x04000872 RID: 2162
			MoveWordLeft,
			// Token: 0x04000873 RID: 2163
			MoveWordRight,
			// Token: 0x04000874 RID: 2164
			MoveParagraphForward,
			// Token: 0x04000875 RID: 2165
			MoveParagraphBackward,
			// Token: 0x04000876 RID: 2166
			MoveToStartOfNextWord,
			// Token: 0x04000877 RID: 2167
			MoveToEndOfPreviousWord,
			// Token: 0x04000878 RID: 2168
			SelectLeft,
			// Token: 0x04000879 RID: 2169
			SelectRight,
			// Token: 0x0400087A RID: 2170
			SelectUp,
			// Token: 0x0400087B RID: 2171
			SelectDown,
			// Token: 0x0400087C RID: 2172
			SelectTextStart,
			// Token: 0x0400087D RID: 2173
			SelectTextEnd,
			// Token: 0x0400087E RID: 2174
			SelectPageUp,
			// Token: 0x0400087F RID: 2175
			SelectPageDown,
			// Token: 0x04000880 RID: 2176
			ExpandSelectGraphicalLineStart,
			// Token: 0x04000881 RID: 2177
			ExpandSelectGraphicalLineEnd,
			// Token: 0x04000882 RID: 2178
			SelectGraphicalLineStart,
			// Token: 0x04000883 RID: 2179
			SelectGraphicalLineEnd,
			// Token: 0x04000884 RID: 2180
			SelectWordLeft,
			// Token: 0x04000885 RID: 2181
			SelectWordRight,
			// Token: 0x04000886 RID: 2182
			SelectToEndOfPreviousWord,
			// Token: 0x04000887 RID: 2183
			SelectToStartOfNextWord,
			// Token: 0x04000888 RID: 2184
			SelectParagraphBackward,
			// Token: 0x04000889 RID: 2185
			SelectParagraphForward,
			// Token: 0x0400088A RID: 2186
			Delete,
			// Token: 0x0400088B RID: 2187
			Backspace,
			// Token: 0x0400088C RID: 2188
			DeleteWordBack,
			// Token: 0x0400088D RID: 2189
			DeleteWordForward,
			// Token: 0x0400088E RID: 2190
			DeleteLineBack,
			// Token: 0x0400088F RID: 2191
			Cut,
			// Token: 0x04000890 RID: 2192
			Copy,
			// Token: 0x04000891 RID: 2193
			Paste,
			// Token: 0x04000892 RID: 2194
			SelectAll,
			// Token: 0x04000893 RID: 2195
			SelectNone,
			// Token: 0x04000894 RID: 2196
			ScrollStart,
			// Token: 0x04000895 RID: 2197
			ScrollEnd,
			// Token: 0x04000896 RID: 2198
			ScrollPageUp,
			// Token: 0x04000897 RID: 2199
			ScrollPageDown
		}
	}
}
