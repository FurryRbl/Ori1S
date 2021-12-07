using System;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace UnityEngine.UI
{
	// Token: 0x02000073 RID: 115
	[RequireComponent(typeof(RectTransform))]
	[AddComponentMenu("UI/Slider", 33)]
	public class Slider : Selectable, IEventSystemHandler, IInitializePotentialDragHandler, IDragHandler, ICanvasElement
	{
		// Token: 0x0600041C RID: 1052 RVA: 0x00013AE4 File Offset: 0x00011CE4
		protected Slider()
		{
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x0600041D RID: 1053 RVA: 0x00013B10 File Offset: 0x00011D10
		// (set) Token: 0x0600041E RID: 1054 RVA: 0x00013B18 File Offset: 0x00011D18
		public RectTransform fillRect
		{
			get
			{
				return this.m_FillRect;
			}
			set
			{
				if (SetPropertyUtility.SetClass<RectTransform>(ref this.m_FillRect, value))
				{
					this.UpdateCachedReferences();
					this.UpdateVisuals();
				}
			}
		}

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x0600041F RID: 1055 RVA: 0x00013B38 File Offset: 0x00011D38
		// (set) Token: 0x06000420 RID: 1056 RVA: 0x00013B40 File Offset: 0x00011D40
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

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x06000421 RID: 1057 RVA: 0x00013B60 File Offset: 0x00011D60
		// (set) Token: 0x06000422 RID: 1058 RVA: 0x00013B68 File Offset: 0x00011D68
		public Slider.Direction direction
		{
			get
			{
				return this.m_Direction;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<Slider.Direction>(ref this.m_Direction, value))
				{
					this.UpdateVisuals();
				}
			}
		}

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x06000423 RID: 1059 RVA: 0x00013B84 File Offset: 0x00011D84
		// (set) Token: 0x06000424 RID: 1060 RVA: 0x00013B8C File Offset: 0x00011D8C
		public float minValue
		{
			get
			{
				return this.m_MinValue;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<float>(ref this.m_MinValue, value))
				{
					this.Set(this.m_Value);
					this.UpdateVisuals();
				}
			}
		}

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x06000425 RID: 1061 RVA: 0x00013BB4 File Offset: 0x00011DB4
		// (set) Token: 0x06000426 RID: 1062 RVA: 0x00013BBC File Offset: 0x00011DBC
		public float maxValue
		{
			get
			{
				return this.m_MaxValue;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<float>(ref this.m_MaxValue, value))
				{
					this.Set(this.m_Value);
					this.UpdateVisuals();
				}
			}
		}

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x06000427 RID: 1063 RVA: 0x00013BE4 File Offset: 0x00011DE4
		// (set) Token: 0x06000428 RID: 1064 RVA: 0x00013BEC File Offset: 0x00011DEC
		public bool wholeNumbers
		{
			get
			{
				return this.m_WholeNumbers;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<bool>(ref this.m_WholeNumbers, value))
				{
					this.Set(this.m_Value);
					this.UpdateVisuals();
				}
			}
		}

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x06000429 RID: 1065 RVA: 0x00013C14 File Offset: 0x00011E14
		// (set) Token: 0x0600042A RID: 1066 RVA: 0x00013C34 File Offset: 0x00011E34
		public virtual float value
		{
			get
			{
				if (this.wholeNumbers)
				{
					return Mathf.Round(this.m_Value);
				}
				return this.m_Value;
			}
			set
			{
				this.Set(value);
			}
		}

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x0600042B RID: 1067 RVA: 0x00013C40 File Offset: 0x00011E40
		// (set) Token: 0x0600042C RID: 1068 RVA: 0x00013C80 File Offset: 0x00011E80
		public float normalizedValue
		{
			get
			{
				if (Mathf.Approximately(this.minValue, this.maxValue))
				{
					return 0f;
				}
				return Mathf.InverseLerp(this.minValue, this.maxValue, this.value);
			}
			set
			{
				this.value = Mathf.Lerp(this.minValue, this.maxValue, value);
			}
		}

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x0600042D RID: 1069 RVA: 0x00013CA8 File Offset: 0x00011EA8
		// (set) Token: 0x0600042E RID: 1070 RVA: 0x00013CB0 File Offset: 0x00011EB0
		public Slider.SliderEvent onValueChanged
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

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x0600042F RID: 1071 RVA: 0x00013CBC File Offset: 0x00011EBC
		private float stepSize
		{
			get
			{
				return (!this.wholeNumbers) ? ((this.maxValue - this.minValue) * 0.1f) : 1f;
			}
		}

		// Token: 0x06000430 RID: 1072 RVA: 0x00013CF4 File Offset: 0x00011EF4
		public virtual void Rebuild(CanvasUpdate executing)
		{
		}

		// Token: 0x06000431 RID: 1073 RVA: 0x00013CF8 File Offset: 0x00011EF8
		public virtual void LayoutComplete()
		{
		}

		// Token: 0x06000432 RID: 1074 RVA: 0x00013CFC File Offset: 0x00011EFC
		public virtual void GraphicUpdateComplete()
		{
		}

		// Token: 0x06000433 RID: 1075 RVA: 0x00013D00 File Offset: 0x00011F00
		protected override void OnEnable()
		{
			base.OnEnable();
			this.UpdateCachedReferences();
			this.Set(this.m_Value, false);
			this.UpdateVisuals();
		}

		// Token: 0x06000434 RID: 1076 RVA: 0x00013D24 File Offset: 0x00011F24
		protected override void OnDisable()
		{
			this.m_Tracker.Clear();
			base.OnDisable();
		}

		// Token: 0x06000435 RID: 1077 RVA: 0x00013D38 File Offset: 0x00011F38
		protected override void OnDidApplyAnimationProperties()
		{
			this.m_Value = this.ClampValue(this.m_Value);
			float num = this.normalizedValue;
			if (this.m_FillContainerRect != null)
			{
				if (this.m_FillImage != null && this.m_FillImage.type == Image.Type.Filled)
				{
					num = this.m_FillImage.fillAmount;
				}
				else
				{
					num = ((!this.reverseValue) ? this.m_FillRect.anchorMax[(int)this.axis] : (1f - this.m_FillRect.anchorMin[(int)this.axis]));
				}
			}
			else if (this.m_HandleContainerRect != null)
			{
				num = ((!this.reverseValue) ? this.m_HandleRect.anchorMin[(int)this.axis] : (1f - this.m_HandleRect.anchorMin[(int)this.axis]));
			}
			this.UpdateVisuals();
			if (num != this.normalizedValue)
			{
				this.onValueChanged.Invoke(this.m_Value);
			}
		}

		// Token: 0x06000436 RID: 1078 RVA: 0x00013E70 File Offset: 0x00012070
		private void UpdateCachedReferences()
		{
			if (this.m_FillRect)
			{
				this.m_FillTransform = this.m_FillRect.transform;
				this.m_FillImage = this.m_FillRect.GetComponent<Image>();
				if (this.m_FillTransform.parent != null)
				{
					this.m_FillContainerRect = this.m_FillTransform.parent.GetComponent<RectTransform>();
				}
			}
			else
			{
				this.m_FillContainerRect = null;
				this.m_FillImage = null;
			}
			if (this.m_HandleRect)
			{
				this.m_HandleTransform = this.m_HandleRect.transform;
				if (this.m_HandleTransform.parent != null)
				{
					this.m_HandleContainerRect = this.m_HandleTransform.parent.GetComponent<RectTransform>();
				}
			}
			else
			{
				this.m_HandleContainerRect = null;
			}
		}

		// Token: 0x06000437 RID: 1079 RVA: 0x00013F48 File Offset: 0x00012148
		private float ClampValue(float input)
		{
			float num = Mathf.Clamp(input, this.minValue, this.maxValue);
			if (this.wholeNumbers)
			{
				num = Mathf.Round(num);
			}
			return num;
		}

		// Token: 0x06000438 RID: 1080 RVA: 0x00013F7C File Offset: 0x0001217C
		private void Set(float input)
		{
			this.Set(input, true);
		}

		// Token: 0x06000439 RID: 1081 RVA: 0x00013F88 File Offset: 0x00012188
		protected virtual void Set(float input, bool sendCallback)
		{
			float num = this.ClampValue(input);
			if (this.m_Value == num)
			{
				return;
			}
			this.m_Value = num;
			this.UpdateVisuals();
			if (sendCallback)
			{
				this.m_OnValueChanged.Invoke(num);
			}
		}

		// Token: 0x0600043A RID: 1082 RVA: 0x00013FCC File Offset: 0x000121CC
		protected override void OnRectTransformDimensionsChange()
		{
			base.OnRectTransformDimensionsChange();
			if (!this.IsActive())
			{
				return;
			}
			this.UpdateVisuals();
		}

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x0600043B RID: 1083 RVA: 0x00013FE8 File Offset: 0x000121E8
		private Slider.Axis axis
		{
			get
			{
				return (this.m_Direction != Slider.Direction.LeftToRight && this.m_Direction != Slider.Direction.RightToLeft) ? Slider.Axis.Vertical : Slider.Axis.Horizontal;
			}
		}

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x0600043C RID: 1084 RVA: 0x00014008 File Offset: 0x00012208
		private bool reverseValue
		{
			get
			{
				return this.m_Direction == Slider.Direction.RightToLeft || this.m_Direction == Slider.Direction.TopToBottom;
			}
		}

		// Token: 0x0600043D RID: 1085 RVA: 0x00014024 File Offset: 0x00012224
		private void UpdateVisuals()
		{
			this.m_Tracker.Clear();
			if (this.m_FillContainerRect != null)
			{
				this.m_Tracker.Add(this, this.m_FillRect, DrivenTransformProperties.Anchors);
				Vector2 zero = Vector2.zero;
				Vector2 one = Vector2.one;
				if (this.m_FillImage != null && this.m_FillImage.type == Image.Type.Filled)
				{
					this.m_FillImage.fillAmount = this.normalizedValue;
				}
				else if (this.reverseValue)
				{
					zero[(int)this.axis] = 1f - this.normalizedValue;
				}
				else
				{
					one[(int)this.axis] = this.normalizedValue;
				}
				this.m_FillRect.anchorMin = zero;
				this.m_FillRect.anchorMax = one;
			}
			if (this.m_HandleContainerRect != null)
			{
				this.m_Tracker.Add(this, this.m_HandleRect, DrivenTransformProperties.Anchors);
				Vector2 zero2 = Vector2.zero;
				Vector2 one2 = Vector2.one;
				int axis = (int)this.axis;
				float value = (!this.reverseValue) ? this.normalizedValue : (1f - this.normalizedValue);
				one2[(int)this.axis] = value;
				zero2[axis] = value;
				this.m_HandleRect.anchorMin = zero2;
				this.m_HandleRect.anchorMax = one2;
			}
		}

		// Token: 0x0600043E RID: 1086 RVA: 0x0001418C File Offset: 0x0001238C
		private void UpdateDrag(PointerEventData eventData, Camera cam)
		{
			RectTransform rectTransform = this.m_HandleContainerRect ?? this.m_FillContainerRect;
			if (rectTransform != null && rectTransform.rect.size[(int)this.axis] > 0f)
			{
				Vector2 a;
				if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, eventData.position, cam, out a))
				{
					return;
				}
				a -= rectTransform.rect.position;
				float num = Mathf.Clamp01((a - this.m_Offset)[(int)this.axis] / rectTransform.rect.size[(int)this.axis]);
				this.normalizedValue = ((!this.reverseValue) ? num : (1f - num));
			}
		}

		// Token: 0x0600043F RID: 1087 RVA: 0x0001426C File Offset: 0x0001246C
		private bool MayDrag(PointerEventData eventData)
		{
			return this.IsActive() && this.IsInteractable() && eventData.button == PointerEventData.InputButton.Left;
		}

		// Token: 0x06000440 RID: 1088 RVA: 0x0001429C File Offset: 0x0001249C
		public override void OnPointerDown(PointerEventData eventData)
		{
			if (!this.MayDrag(eventData))
			{
				return;
			}
			base.OnPointerDown(eventData);
			this.m_Offset = Vector2.zero;
			if (this.m_HandleContainerRect != null && RectTransformUtility.RectangleContainsScreenPoint(this.m_HandleRect, eventData.position, eventData.enterEventCamera))
			{
				Vector2 offset;
				if (RectTransformUtility.ScreenPointToLocalPointInRectangle(this.m_HandleRect, eventData.position, eventData.pressEventCamera, out offset))
				{
					this.m_Offset = offset;
				}
			}
			else
			{
				this.UpdateDrag(eventData, eventData.pressEventCamera);
			}
		}

		// Token: 0x06000441 RID: 1089 RVA: 0x0001432C File Offset: 0x0001252C
		public virtual void OnDrag(PointerEventData eventData)
		{
			if (!this.MayDrag(eventData))
			{
				return;
			}
			this.UpdateDrag(eventData, eventData.pressEventCamera);
		}

		// Token: 0x06000442 RID: 1090 RVA: 0x00014348 File Offset: 0x00012548
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
				if (this.axis == Slider.Axis.Horizontal && this.FindSelectableOnLeft() == null)
				{
					this.Set((!this.reverseValue) ? (this.value - this.stepSize) : (this.value + this.stepSize));
				}
				else
				{
					base.OnMove(eventData);
				}
				break;
			case MoveDirection.Up:
				if (this.axis == Slider.Axis.Vertical && this.FindSelectableOnUp() == null)
				{
					this.Set((!this.reverseValue) ? (this.value + this.stepSize) : (this.value - this.stepSize));
				}
				else
				{
					base.OnMove(eventData);
				}
				break;
			case MoveDirection.Right:
				if (this.axis == Slider.Axis.Horizontal && this.FindSelectableOnRight() == null)
				{
					this.Set((!this.reverseValue) ? (this.value + this.stepSize) : (this.value - this.stepSize));
				}
				else
				{
					base.OnMove(eventData);
				}
				break;
			case MoveDirection.Down:
				if (this.axis == Slider.Axis.Vertical && this.FindSelectableOnDown() == null)
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

		// Token: 0x06000443 RID: 1091 RVA: 0x0001450C File Offset: 0x0001270C
		public override Selectable FindSelectableOnLeft()
		{
			if (base.navigation.mode == Navigation.Mode.Automatic && this.axis == Slider.Axis.Horizontal)
			{
				return null;
			}
			return base.FindSelectableOnLeft();
		}

		// Token: 0x06000444 RID: 1092 RVA: 0x00014540 File Offset: 0x00012740
		public override Selectable FindSelectableOnRight()
		{
			if (base.navigation.mode == Navigation.Mode.Automatic && this.axis == Slider.Axis.Horizontal)
			{
				return null;
			}
			return base.FindSelectableOnRight();
		}

		// Token: 0x06000445 RID: 1093 RVA: 0x00014574 File Offset: 0x00012774
		public override Selectable FindSelectableOnUp()
		{
			if (base.navigation.mode == Navigation.Mode.Automatic && this.axis == Slider.Axis.Vertical)
			{
				return null;
			}
			return base.FindSelectableOnUp();
		}

		// Token: 0x06000446 RID: 1094 RVA: 0x000145AC File Offset: 0x000127AC
		public override Selectable FindSelectableOnDown()
		{
			if (base.navigation.mode == Navigation.Mode.Automatic && this.axis == Slider.Axis.Vertical)
			{
				return null;
			}
			return base.FindSelectableOnDown();
		}

		// Token: 0x06000447 RID: 1095 RVA: 0x000145E4 File Offset: 0x000127E4
		public virtual void OnInitializePotentialDrag(PointerEventData eventData)
		{
			eventData.useDragThreshold = false;
		}

		// Token: 0x06000448 RID: 1096 RVA: 0x000145F0 File Offset: 0x000127F0
		public void SetDirection(Slider.Direction direction, bool includeRectLayouts)
		{
			Slider.Axis axis = this.axis;
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

		// Token: 0x06000449 RID: 1097 RVA: 0x0001465C File Offset: 0x0001285C
		virtual bool IsDestroyed()
		{
			return base.IsDestroyed();
		}

		// Token: 0x0600044A RID: 1098 RVA: 0x00014664 File Offset: 0x00012864
		virtual Transform get_transform()
		{
			return base.transform;
		}

		// Token: 0x04000204 RID: 516
		[SerializeField]
		private RectTransform m_FillRect;

		// Token: 0x04000205 RID: 517
		[SerializeField]
		private RectTransform m_HandleRect;

		// Token: 0x04000206 RID: 518
		[Space]
		[SerializeField]
		private Slider.Direction m_Direction;

		// Token: 0x04000207 RID: 519
		[SerializeField]
		private float m_MinValue;

		// Token: 0x04000208 RID: 520
		[SerializeField]
		private float m_MaxValue = 1f;

		// Token: 0x04000209 RID: 521
		[SerializeField]
		private bool m_WholeNumbers;

		// Token: 0x0400020A RID: 522
		[SerializeField]
		protected float m_Value;

		// Token: 0x0400020B RID: 523
		[SerializeField]
		[Space]
		private Slider.SliderEvent m_OnValueChanged = new Slider.SliderEvent();

		// Token: 0x0400020C RID: 524
		private Image m_FillImage;

		// Token: 0x0400020D RID: 525
		private Transform m_FillTransform;

		// Token: 0x0400020E RID: 526
		private RectTransform m_FillContainerRect;

		// Token: 0x0400020F RID: 527
		private Transform m_HandleTransform;

		// Token: 0x04000210 RID: 528
		private RectTransform m_HandleContainerRect;

		// Token: 0x04000211 RID: 529
		private Vector2 m_Offset = Vector2.zero;

		// Token: 0x04000212 RID: 530
		private DrivenRectTransformTracker m_Tracker;

		// Token: 0x02000074 RID: 116
		public enum Direction
		{
			// Token: 0x04000214 RID: 532
			LeftToRight,
			// Token: 0x04000215 RID: 533
			RightToLeft,
			// Token: 0x04000216 RID: 534
			BottomToTop,
			// Token: 0x04000217 RID: 535
			TopToBottom
		}

		// Token: 0x02000075 RID: 117
		[Serializable]
		public class SliderEvent : UnityEvent<float>
		{
		}

		// Token: 0x02000076 RID: 118
		private enum Axis
		{
			// Token: 0x04000219 RID: 537
			Horizontal,
			// Token: 0x0400021A RID: 538
			Vertical
		}
	}
}
