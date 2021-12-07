using System;
using System.Collections;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace UnityEngine.UI
{
	// Token: 0x02000067 RID: 103
	[AddComponentMenu("UI/Scrollbar", 34)]
	[RequireComponent(typeof(RectTransform))]
	public class Scrollbar : Selectable, IEventSystemHandler, IBeginDragHandler, IInitializePotentialDragHandler, IDragHandler, ICanvasElement
	{
		// Token: 0x06000359 RID: 857 RVA: 0x000107F0 File Offset: 0x0000E9F0
		protected Scrollbar()
		{
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x0600035A RID: 858 RVA: 0x0001081C File Offset: 0x0000EA1C
		// (set) Token: 0x0600035B RID: 859 RVA: 0x00010824 File Offset: 0x0000EA24
		public RectTransform handleRect
		{
			get
			{
				return this.m_HandleRect;
			}
			set
			{
				if (SetPropertyUtility.SetClass<RectTransform>(ref this.m_HandleRect, value))
				{
					this.UpdateCachedReferences();
					this.UpdateVisuals();
				}
			}
		}

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x0600035C RID: 860 RVA: 0x00010844 File Offset: 0x0000EA44
		// (set) Token: 0x0600035D RID: 861 RVA: 0x0001084C File Offset: 0x0000EA4C
		public Scrollbar.Direction direction
		{
			get
			{
				return this.m_Direction;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<Scrollbar.Direction>(ref this.m_Direction, value))
				{
					this.UpdateVisuals();
				}
			}
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x0600035E RID: 862 RVA: 0x00010868 File Offset: 0x0000EA68
		// (set) Token: 0x0600035F RID: 863 RVA: 0x000108A4 File Offset: 0x0000EAA4
		public float value
		{
			get
			{
				float num = this.m_Value;
				if (this.m_NumberOfSteps > 1)
				{
					num = Mathf.Round(num * (float)(this.m_NumberOfSteps - 1)) / (float)(this.m_NumberOfSteps - 1);
				}
				return num;
			}
			set
			{
				this.Set(value);
			}
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x06000360 RID: 864 RVA: 0x000108B0 File Offset: 0x0000EAB0
		// (set) Token: 0x06000361 RID: 865 RVA: 0x000108B8 File Offset: 0x0000EAB8
		public float size
		{
			get
			{
				return this.m_Size;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<float>(ref this.m_Size, Mathf.Clamp01(value)))
				{
					this.UpdateVisuals();
				}
			}
		}

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x06000362 RID: 866 RVA: 0x000108D8 File Offset: 0x0000EAD8
		// (set) Token: 0x06000363 RID: 867 RVA: 0x000108E0 File Offset: 0x0000EAE0
		public int numberOfSteps
		{
			get
			{
				return this.m_NumberOfSteps;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<int>(ref this.m_NumberOfSteps, value))
				{
					this.Set(this.m_Value);
					this.UpdateVisuals();
				}
			}
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x06000364 RID: 868 RVA: 0x00010908 File Offset: 0x0000EB08
		// (set) Token: 0x06000365 RID: 869 RVA: 0x00010910 File Offset: 0x0000EB10
		public Scrollbar.ScrollEvent onValueChanged
		{
			get
			{
				return this.m_OnValueChanged;
			}
			set
			{
				this.m_OnValueChanged = value;
			}
		}

		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x06000366 RID: 870 RVA: 0x0001091C File Offset: 0x0000EB1C
		private float stepSize
		{
			get
			{
				return (this.m_NumberOfSteps <= 1) ? 0.1f : (1f / (float)(this.m_NumberOfSteps - 1));
			}
		}

		// Token: 0x06000367 RID: 871 RVA: 0x00010944 File Offset: 0x0000EB44
		public virtual void Rebuild(CanvasUpdate executing)
		{
		}

		// Token: 0x06000368 RID: 872 RVA: 0x00010948 File Offset: 0x0000EB48
		public virtual void LayoutComplete()
		{
		}

		// Token: 0x06000369 RID: 873 RVA: 0x0001094C File Offset: 0x0000EB4C
		public virtual void GraphicUpdateComplete()
		{
		}

		// Token: 0x0600036A RID: 874 RVA: 0x00010950 File Offset: 0x0000EB50
		protected override void OnEnable()
		{
			base.OnEnable();
			this.UpdateCachedReferences();
			this.Set(this.m_Value, false);
			this.UpdateVisuals();
		}

		// Token: 0x0600036B RID: 875 RVA: 0x00010974 File Offset: 0x0000EB74
		protected override void OnDisable()
		{
			this.m_Tracker.Clear();
			base.OnDisable();
		}

		// Token: 0x0600036C RID: 876 RVA: 0x00010988 File Offset: 0x0000EB88
		private void UpdateCachedReferences()
		{
			if (this.m_HandleRect && this.m_HandleRect.parent != null)
			{
				this.m_ContainerRect = this.m_HandleRect.parent.GetComponent<RectTransform>();
			}
			else
			{
				this.m_ContainerRect = null;
			}
		}

		// Token: 0x0600036D RID: 877 RVA: 0x000109E0 File Offset: 0x0000EBE0
		private void Set(float input)
		{
			this.Set(input, true);
		}

		// Token: 0x0600036E RID: 878 RVA: 0x000109EC File Offset: 0x0000EBEC
		private void Set(float input, bool sendCallback)
		{
			float value = this.m_Value;
			this.m_Value = Mathf.Clamp01(input);
			if (value == this.value)
			{
				return;
			}
			this.UpdateVisuals();
			if (sendCallback)
			{
				this.m_OnValueChanged.Invoke(this.value);
			}
		}

		// Token: 0x0600036F RID: 879 RVA: 0x00010A38 File Offset: 0x0000EC38
		protected override void OnRectTransformDimensionsChange()
		{
			base.OnRectTransformDimensionsChange();
			if (!this.IsActive())
			{
				return;
			}
			this.UpdateVisuals();
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x06000370 RID: 880 RVA: 0x00010A54 File Offset: 0x0000EC54
		private Scrollbar.Axis axis
		{
			get
			{
				return (this.m_Direction != Scrollbar.Direction.LeftToRight && this.m_Direction != Scrollbar.Direction.RightToLeft) ? Scrollbar.Axis.Vertical : Scrollbar.Axis.Horizontal;
			}
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x06000371 RID: 881 RVA: 0x00010A74 File Offset: 0x0000EC74
		private bool reverseValue
		{
			get
			{
				return this.m_Direction == Scrollbar.Direction.RightToLeft || this.m_Direction == Scrollbar.Direction.TopToBottom;
			}
		}

		// Token: 0x06000372 RID: 882 RVA: 0x00010A90 File Offset: 0x0000EC90
		private void UpdateVisuals()
		{
			this.m_Tracker.Clear();
			if (this.m_ContainerRect != null)
			{
				this.m_Tracker.Add(this, this.m_HandleRect, DrivenTransformProperties.Anchors);
				Vector2 zero = Vector2.zero;
				Vector2 one = Vector2.one;
				float num = this.value * (1f - this.size);
				if (this.reverseValue)
				{
					zero[(int)this.axis] = 1f - num - this.size;
					one[(int)this.axis] = 1f - num;
				}
				else
				{
					zero[(int)this.axis] = num;
					one[(int)this.axis] = num + this.size;
				}
				this.m_HandleRect.anchorMin = zero;
				this.m_HandleRect.anchorMax = one;
			}
		}

		// Token: 0x06000373 RID: 883 RVA: 0x00010B6C File Offset: 0x0000ED6C
		private void UpdateDrag(PointerEventData eventData)
		{
			if (eventData.button != PointerEventData.InputButton.Left)
			{
				return;
			}
			if (this.m_ContainerRect == null)
			{
				return;
			}
			Vector2 a;
			if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(this.m_ContainerRect, eventData.position, eventData.pressEventCamera, out a))
			{
				return;
			}
			Vector2 a2 = a - this.m_Offset - this.m_ContainerRect.rect.position;
			Vector2 vector = a2 - (this.m_HandleRect.rect.size - this.m_HandleRect.sizeDelta) * 0.5f;
			float num = (this.axis != Scrollbar.Axis.Horizontal) ? this.m_ContainerRect.rect.height : this.m_ContainerRect.rect.width;
			float num2 = num * (1f - this.size);
			if (num2 <= 0f)
			{
				return;
			}
			switch (this.m_Direction)
			{
			case Scrollbar.Direction.LeftToRight:
				this.Set(vector.x / num2);
				break;
			case Scrollbar.Direction.RightToLeft:
				this.Set(1f - vector.x / num2);
				break;
			case Scrollbar.Direction.BottomToTop:
				this.Set(vector.y / num2);
				break;
			case Scrollbar.Direction.TopToBottom:
				this.Set(1f - vector.y / num2);
				break;
			}
		}

		// Token: 0x06000374 RID: 884 RVA: 0x00010CEC File Offset: 0x0000EEEC
		private bool MayDrag(PointerEventData eventData)
		{
			return this.IsActive() && this.IsInteractable() && eventData.button == PointerEventData.InputButton.Left;
		}

		// Token: 0x06000375 RID: 885 RVA: 0x00010D1C File Offset: 0x0000EF1C
		public virtual void OnBeginDrag(PointerEventData eventData)
		{
			this.isPointerDownAndNotDragging = false;
			if (!this.MayDrag(eventData))
			{
				return;
			}
			if (this.m_ContainerRect == null)
			{
				return;
			}
			this.m_Offset = Vector2.zero;
			Vector2 a;
			if (RectTransformUtility.RectangleContainsScreenPoint(this.m_HandleRect, eventData.position, eventData.enterEventCamera) && RectTransformUtility.ScreenPointToLocalPointInRectangle(this.m_HandleRect, eventData.position, eventData.pressEventCamera, out a))
			{
				this.m_Offset = a - this.m_HandleRect.rect.center;
			}
		}

		// Token: 0x06000376 RID: 886 RVA: 0x00010DB4 File Offset: 0x0000EFB4
		public virtual void OnDrag(PointerEventData eventData)
		{
			if (!this.MayDrag(eventData))
			{
				return;
			}
			if (this.m_ContainerRect != null)
			{
				this.UpdateDrag(eventData);
			}
		}

		// Token: 0x06000377 RID: 887 RVA: 0x00010DDC File Offset: 0x0000EFDC
		public override void OnPointerDown(PointerEventData eventData)
		{
			if (!this.MayDrag(eventData))
			{
				return;
			}
			base.OnPointerDown(eventData);
			this.isPointerDownAndNotDragging = true;
			this.m_PointerDownRepeat = base.StartCoroutine(this.ClickRepeat(eventData));
		}

		// Token: 0x06000378 RID: 888 RVA: 0x00010E18 File Offset: 0x0000F018
		protected IEnumerator ClickRepeat(PointerEventData eventData)
		{
			while (this.isPointerDownAndNotDragging)
			{
				Vector2 localMousePos;
				if (!RectTransformUtility.RectangleContainsScreenPoint(this.m_HandleRect, eventData.position, eventData.enterEventCamera) && RectTransformUtility.ScreenPointToLocalPointInRectangle(this.m_HandleRect, eventData.position, eventData.pressEventCamera, out localMousePos))
				{
					float axisCoordinate = (this.axis != Scrollbar.Axis.Horizontal) ? localMousePos.y : localMousePos.x;
					if (axisCoordinate < 0f)
					{
						this.value -= this.size;
					}
					else
					{
						this.value += this.size;
					}
				}
				yield return new WaitForEndOfFrame();
			}
			base.StopCoroutine(this.m_PointerDownRepeat);
			yield break;
		}

		// Token: 0x06000379 RID: 889 RVA: 0x00010E44 File Offset: 0x0000F044
		public override void OnPointerUp(PointerEventData eventData)
		{
			base.OnPointerUp(eventData);
			this.isPointerDownAndNotDragging = false;
		}

		// Token: 0x0600037A RID: 890 RVA: 0x00010E54 File Offset: 0x0000F054
		public override void OnMove(AxisEventData eventData)
		{
			if (!this.IsActive() || !this.IsInteractable())
			{
				base.OnMove(eventData);
				return;
			}
			switch (eventData.moveDir)
			{
			case MoveDirection.Left:
				if (this.axis == Scrollbar.Axis.Horizontal && this.FindSelectableOnLeft() == null)
				{
					this.Set((!this.reverseValue) ? (this.value - this.stepSize) : (this.value + this.stepSize));
				}
				else
				{
					base.OnMove(eventData);
				}
				break;
			case MoveDirection.Up:
				if (this.axis == Scrollbar.Axis.Vertical && this.FindSelectableOnUp() == null)
				{
					this.Set((!this.reverseValue) ? (this.value + this.stepSize) : (this.value - this.stepSize));
				}
				else
				{
					base.OnMove(eventData);
				}
				break;
			case MoveDirection.Right:
				if (this.axis == Scrollbar.Axis.Horizontal && this.FindSelectableOnRight() == null)
				{
					this.Set((!this.reverseValue) ? (this.value + this.stepSize) : (this.value - this.stepSize));
				}
				else
				{
					base.OnMove(eventData);
				}
				break;
			case MoveDirection.Down:
				if (this.axis == Scrollbar.Axis.Vertical && this.FindSelectableOnDown() == null)
				{
					this.Set((!this.reverseValue) ? (this.value - this.stepSize) : (this.value + this.stepSize));
				}
				else
				{
					base.OnMove(eventData);
				}
				break;
			}
		}

		// Token: 0x0600037B RID: 891 RVA: 0x00011018 File Offset: 0x0000F218
		public override Selectable FindSelectableOnLeft()
		{
			if (base.navigation.mode == Navigation.Mode.Automatic && this.axis == Scrollbar.Axis.Horizontal)
			{
				return null;
			}
			return base.FindSelectableOnLeft();
		}

		// Token: 0x0600037C RID: 892 RVA: 0x0001104C File Offset: 0x0000F24C
		public override Selectable FindSelectableOnRight()
		{
			if (base.navigation.mode == Navigation.Mode.Automatic && this.axis == Scrollbar.Axis.Horizontal)
			{
				return null;
			}
			return base.FindSelectableOnRight();
		}

		// Token: 0x0600037D RID: 893 RVA: 0x00011080 File Offset: 0x0000F280
		public override Selectable FindSelectableOnUp()
		{
			if (base.navigation.mode == Navigation.Mode.Automatic && this.axis == Scrollbar.Axis.Vertical)
			{
				return null;
			}
			return base.FindSelectableOnUp();
		}

		// Token: 0x0600037E RID: 894 RVA: 0x000110B8 File Offset: 0x0000F2B8
		public override Selectable FindSelectableOnDown()
		{
			if (base.navigation.mode == Navigation.Mode.Automatic && this.axis == Scrollbar.Axis.Vertical)
			{
				return null;
			}
			return base.FindSelectableOnDown();
		}

		// Token: 0x0600037F RID: 895 RVA: 0x000110F0 File Offset: 0x0000F2F0
		public virtual void OnInitializePotentialDrag(PointerEventData eventData)
		{
			eventData.useDragThreshold = false;
		}

		// Token: 0x06000380 RID: 896 RVA: 0x000110FC File Offset: 0x0000F2FC
		public void SetDirection(Scrollbar.Direction direction, bool includeRectLayouts)
		{
			Scrollbar.Axis axis = this.axis;
			bool reverseValue = this.reverseValue;
			this.direction = direction;
			if (!includeRectLayouts)
			{
				return;
			}
			if (this.axis != axis)
			{
				RectTransformUtility.FlipLayoutAxes(base.transform as RectTransform, true, true);
			}
			if (this.reverseValue != reverseValue)
			{
				RectTransformUtility.FlipLayoutOnAxis(base.transform as RectTransform, (int)this.axis, true, true);
			}
		}

		// Token: 0x06000381 RID: 897 RVA: 0x00011168 File Offset: 0x0000F368
		virtual bool IsDestroyed()
		{
			return base.IsDestroyed();
		}

		// Token: 0x06000382 RID: 898 RVA: 0x00011170 File Offset: 0x0000F370
		virtual Transform get_transform()
		{
			return base.transform;
		}

		// Token: 0x040001AD RID: 429
		[SerializeField]
		private RectTransform m_HandleRect;

		// Token: 0x040001AE RID: 430
		[SerializeField]
		private Scrollbar.Direction m_Direction;

		// Token: 0x040001AF RID: 431
		[Range(0f, 1f)]
		[SerializeField]
		private float m_Value;

		// Token: 0x040001B0 RID: 432
		[SerializeField]
		[Range(0f, 1f)]
		private float m_Size = 0.2f;

		// Token: 0x040001B1 RID: 433
		[SerializeField]
		[Range(0f, 11f)]
		private int m_NumberOfSteps;

		// Token: 0x040001B2 RID: 434
		[SerializeField]
		[Space(6f)]
		private Scrollbar.ScrollEvent m_OnValueChanged = new Scrollbar.ScrollEvent();

		// Token: 0x040001B3 RID: 435
		private RectTransform m_ContainerRect;

		// Token: 0x040001B4 RID: 436
		private Vector2 m_Offset = Vector2.zero;

		// Token: 0x040001B5 RID: 437
		private DrivenRectTransformTracker m_Tracker;

		// Token: 0x040001B6 RID: 438
		private Coroutine m_PointerDownRepeat;

		// Token: 0x040001B7 RID: 439
		private bool isPointerDownAndNotDragging;

		// Token: 0x02000068 RID: 104
		public enum Direction
		{
			// Token: 0x040001B9 RID: 441
			LeftToRight,
			// Token: 0x040001BA RID: 442
			RightToLeft,
			// Token: 0x040001BB RID: 443
			BottomToTop,
			// Token: 0x040001BC RID: 444
			TopToBottom
		}

		// Token: 0x02000069 RID: 105
		[Serializable]
		public class ScrollEvent : UnityEvent<float>
		{
		}

		// Token: 0x0200006A RID: 106
		private enum Axis
		{
			// Token: 0x040001BE RID: 446
			Horizontal,
			// Token: 0x040001BF RID: 447
			Vertical
		}
	}
}
