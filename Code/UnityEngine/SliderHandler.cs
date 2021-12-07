using System;

namespace UnityEngine
{
	// Token: 0x02000214 RID: 532
	internal struct SliderHandler
	{
		// Token: 0x060020F6 RID: 8438 RVA: 0x00026620 File Offset: 0x00024820
		public SliderHandler(Rect position, float currentValue, float size, float start, float end, GUIStyle slider, GUIStyle thumb, bool horiz, int id)
		{
			this.position = position;
			this.currentValue = currentValue;
			this.size = size;
			this.start = start;
			this.end = end;
			this.slider = slider;
			this.thumb = thumb;
			this.horiz = horiz;
			this.id = id;
		}

		// Token: 0x060020F7 RID: 8439 RVA: 0x00026674 File Offset: 0x00024874
		public float Handle()
		{
			if (this.slider == null || this.thumb == null)
			{
				return this.currentValue;
			}
			switch (this.CurrentEventType())
			{
			case EventType.MouseDown:
				return this.OnMouseDown();
			case EventType.MouseUp:
				return this.OnMouseUp();
			case EventType.MouseDrag:
				return this.OnMouseDrag();
			case EventType.Repaint:
				return this.OnRepaint();
			}
			return this.currentValue;
		}

		// Token: 0x060020F8 RID: 8440 RVA: 0x000266F4 File Offset: 0x000248F4
		private float OnMouseDown()
		{
			if (!this.position.Contains(this.CurrentEvent().mousePosition) || this.IsEmptySlider())
			{
				return this.currentValue;
			}
			GUI.scrollTroughSide = 0;
			GUIUtility.hotControl = this.id;
			this.CurrentEvent().Use();
			if (this.ThumbSelectionRect().Contains(this.CurrentEvent().mousePosition))
			{
				this.StartDraggingWithValue(this.ClampedCurrentValue());
				return this.currentValue;
			}
			GUI.changed = true;
			if (this.SupportsPageMovements())
			{
				this.SliderState().isDragging = false;
				GUI.nextScrollStepTime = SystemClock.now.AddMilliseconds(250.0);
				GUI.scrollTroughSide = this.CurrentScrollTroughSide();
				return this.PageMovementValue();
			}
			float num = this.ValueForCurrentMousePosition();
			this.StartDraggingWithValue(num);
			return this.Clamp(num);
		}

		// Token: 0x060020F9 RID: 8441 RVA: 0x000267E0 File Offset: 0x000249E0
		private float OnMouseDrag()
		{
			if (GUIUtility.hotControl != this.id)
			{
				return this.currentValue;
			}
			SliderState sliderState = this.SliderState();
			if (!sliderState.isDragging)
			{
				return this.currentValue;
			}
			GUI.changed = true;
			this.CurrentEvent().Use();
			float num = this.MousePosition() - sliderState.dragStartPos;
			float value = sliderState.dragStartValue + num / this.ValuesPerPixel();
			return this.Clamp(value);
		}

		// Token: 0x060020FA RID: 8442 RVA: 0x00026854 File Offset: 0x00024A54
		private float OnMouseUp()
		{
			if (GUIUtility.hotControl == this.id)
			{
				this.CurrentEvent().Use();
				GUIUtility.hotControl = 0;
			}
			return this.currentValue;
		}

		// Token: 0x060020FB RID: 8443 RVA: 0x00026880 File Offset: 0x00024A80
		private float OnRepaint()
		{
			this.slider.Draw(this.position, GUIContent.none, this.id);
			if (!this.IsEmptySlider())
			{
				this.thumb.Draw(this.ThumbRect(), GUIContent.none, this.id);
			}
			if (GUIUtility.hotControl != this.id || !this.position.Contains(this.CurrentEvent().mousePosition) || this.IsEmptySlider())
			{
				return this.currentValue;
			}
			if (this.ThumbRect().Contains(this.CurrentEvent().mousePosition))
			{
				if (GUI.scrollTroughSide != 0)
				{
					GUIUtility.hotControl = 0;
				}
				return this.currentValue;
			}
			GUI.InternalRepaintEditorWindow();
			if (SystemClock.now < GUI.nextScrollStepTime)
			{
				return this.currentValue;
			}
			if (this.CurrentScrollTroughSide() != GUI.scrollTroughSide)
			{
				return this.currentValue;
			}
			GUI.nextScrollStepTime = SystemClock.now.AddMilliseconds(30.0);
			if (this.SupportsPageMovements())
			{
				this.SliderState().isDragging = false;
				GUI.changed = true;
				return this.PageMovementValue();
			}
			return this.ClampedCurrentValue();
		}

		// Token: 0x060020FC RID: 8444 RVA: 0x000269C4 File Offset: 0x00024BC4
		private EventType CurrentEventType()
		{
			return this.CurrentEvent().GetTypeForControl(this.id);
		}

		// Token: 0x060020FD RID: 8445 RVA: 0x000269D8 File Offset: 0x00024BD8
		private int CurrentScrollTroughSide()
		{
			float num = (!this.horiz) ? this.CurrentEvent().mousePosition.y : this.CurrentEvent().mousePosition.x;
			float num2 = (!this.horiz) ? this.ThumbRect().y : this.ThumbRect().x;
			return (num <= num2) ? -1 : 1;
		}

		// Token: 0x060020FE RID: 8446 RVA: 0x00026A5C File Offset: 0x00024C5C
		private bool IsEmptySlider()
		{
			return this.start == this.end;
		}

		// Token: 0x060020FF RID: 8447 RVA: 0x00026A6C File Offset: 0x00024C6C
		private bool SupportsPageMovements()
		{
			return this.size != 0f && GUI.usePageScrollbars;
		}

		// Token: 0x06002100 RID: 8448 RVA: 0x00026A88 File Offset: 0x00024C88
		private float PageMovementValue()
		{
			float num = this.currentValue;
			int num2 = (this.start <= this.end) ? 1 : -1;
			if (this.MousePosition() > this.PageUpMovementBound())
			{
				num += this.size * (float)num2 * 0.9f;
			}
			else
			{
				num -= this.size * (float)num2 * 0.9f;
			}
			return this.Clamp(num);
		}

		// Token: 0x06002101 RID: 8449 RVA: 0x00026AF8 File Offset: 0x00024CF8
		private float PageUpMovementBound()
		{
			if (this.horiz)
			{
				return this.ThumbRect().xMax - this.position.x;
			}
			return this.ThumbRect().yMax - this.position.y;
		}

		// Token: 0x06002102 RID: 8450 RVA: 0x00026B4C File Offset: 0x00024D4C
		private Event CurrentEvent()
		{
			return Event.current;
		}

		// Token: 0x06002103 RID: 8451 RVA: 0x00026B54 File Offset: 0x00024D54
		private float ValueForCurrentMousePosition()
		{
			if (this.horiz)
			{
				return (this.MousePosition() - this.ThumbRect().width * 0.5f) / this.ValuesPerPixel() + this.start - this.size * 0.5f;
			}
			return (this.MousePosition() - this.ThumbRect().height * 0.5f) / this.ValuesPerPixel() + this.start - this.size * 0.5f;
		}

		// Token: 0x06002104 RID: 8452 RVA: 0x00026BDC File Offset: 0x00024DDC
		private float Clamp(float value)
		{
			return Mathf.Clamp(value, this.MinValue(), this.MaxValue());
		}

		// Token: 0x06002105 RID: 8453 RVA: 0x00026BF0 File Offset: 0x00024DF0
		private Rect ThumbSelectionRect()
		{
			return this.ThumbRect();
		}

		// Token: 0x06002106 RID: 8454 RVA: 0x00026C08 File Offset: 0x00024E08
		private void StartDraggingWithValue(float dragStartValue)
		{
			SliderState sliderState = this.SliderState();
			sliderState.dragStartPos = this.MousePosition();
			sliderState.dragStartValue = dragStartValue;
			sliderState.isDragging = true;
		}

		// Token: 0x06002107 RID: 8455 RVA: 0x00026C38 File Offset: 0x00024E38
		private SliderState SliderState()
		{
			return (SliderState)GUIUtility.GetStateObject(typeof(SliderState), this.id);
		}

		// Token: 0x06002108 RID: 8456 RVA: 0x00026C54 File Offset: 0x00024E54
		private Rect ThumbRect()
		{
			return (!this.horiz) ? this.VerticalThumbRect() : this.HorizontalThumbRect();
		}

		// Token: 0x06002109 RID: 8457 RVA: 0x00026C74 File Offset: 0x00024E74
		private Rect VerticalThumbRect()
		{
			float num = this.ValuesPerPixel();
			if (this.start < this.end)
			{
				return new Rect(this.position.x + (float)this.slider.padding.left, (this.ClampedCurrentValue() - this.start) * num + this.position.y + (float)this.slider.padding.top, this.position.width - (float)this.slider.padding.horizontal, this.size * num + this.ThumbSize());
			}
			return new Rect(this.position.x + (float)this.slider.padding.left, (this.ClampedCurrentValue() + this.size - this.start) * num + this.position.y + (float)this.slider.padding.top, this.position.width - (float)this.slider.padding.horizontal, this.size * -num + this.ThumbSize());
		}

		// Token: 0x0600210A RID: 8458 RVA: 0x00026DB0 File Offset: 0x00024FB0
		private Rect HorizontalThumbRect()
		{
			float num = this.ValuesPerPixel();
			if (this.start < this.end)
			{
				return new Rect((this.ClampedCurrentValue() - this.start) * num + this.position.x + (float)this.slider.padding.left, this.position.y + (float)this.slider.padding.top, this.size * num + this.ThumbSize(), this.position.height - (float)this.slider.padding.vertical);
			}
			return new Rect((this.ClampedCurrentValue() + this.size - this.start) * num + this.position.x + (float)this.slider.padding.left, this.position.y, this.size * -num + this.ThumbSize(), this.position.height);
		}

		// Token: 0x0600210B RID: 8459 RVA: 0x00026EC8 File Offset: 0x000250C8
		private float ClampedCurrentValue()
		{
			return this.Clamp(this.currentValue);
		}

		// Token: 0x0600210C RID: 8460 RVA: 0x00026ED8 File Offset: 0x000250D8
		private float MousePosition()
		{
			if (this.horiz)
			{
				return this.CurrentEvent().mousePosition.x - this.position.x;
			}
			return this.CurrentEvent().mousePosition.y - this.position.y;
		}

		// Token: 0x0600210D RID: 8461 RVA: 0x00026F38 File Offset: 0x00025138
		private float ValuesPerPixel()
		{
			if (this.horiz)
			{
				return (this.position.width - (float)this.slider.padding.horizontal - this.ThumbSize()) / (this.end - this.start);
			}
			return (this.position.height - (float)this.slider.padding.vertical - this.ThumbSize()) / (this.end - this.start);
		}

		// Token: 0x0600210E RID: 8462 RVA: 0x00026FBC File Offset: 0x000251BC
		private float ThumbSize()
		{
			if (this.horiz)
			{
				return (this.thumb.fixedWidth == 0f) ? ((float)this.thumb.padding.horizontal) : this.thumb.fixedWidth;
			}
			return (this.thumb.fixedHeight == 0f) ? ((float)this.thumb.padding.vertical) : this.thumb.fixedHeight;
		}

		// Token: 0x0600210F RID: 8463 RVA: 0x00027044 File Offset: 0x00025244
		private float MaxValue()
		{
			return Mathf.Max(this.start, this.end) - this.size;
		}

		// Token: 0x06002110 RID: 8464 RVA: 0x00027060 File Offset: 0x00025260
		private float MinValue()
		{
			return Mathf.Min(this.start, this.end);
		}

		// Token: 0x0400083B RID: 2107
		private readonly Rect position;

		// Token: 0x0400083C RID: 2108
		private readonly float currentValue;

		// Token: 0x0400083D RID: 2109
		private readonly float size;

		// Token: 0x0400083E RID: 2110
		private readonly float start;

		// Token: 0x0400083F RID: 2111
		private readonly float end;

		// Token: 0x04000840 RID: 2112
		private readonly GUIStyle slider;

		// Token: 0x04000841 RID: 2113
		private readonly GUIStyle thumb;

		// Token: 0x04000842 RID: 2114
		private readonly bool horiz;

		// Token: 0x04000843 RID: 2115
		private readonly int id;
	}
}
