using System;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace UnityEngine.UI
{
	// Token: 0x0200006B RID: 107
	[DisallowMultipleComponent]
	[AddComponentMenu("UI/Scroll Rect", 37)]
	[ExecuteInEditMode]
	[SelectionBase]
	[RequireComponent(typeof(RectTransform))]
	public class ScrollRect : UIBehaviour, IEventSystemHandler, IBeginDragHandler, IInitializePotentialDragHandler, IDragHandler, IEndDragHandler, IScrollHandler, ICanvasElement, ILayoutElement, ILayoutController, ILayoutGroup
	{
		// Token: 0x06000384 RID: 900 RVA: 0x00011180 File Offset: 0x0000F380
		protected ScrollRect()
		{
		}

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x06000385 RID: 901 RVA: 0x00011208 File Offset: 0x0000F408
		// (set) Token: 0x06000386 RID: 902 RVA: 0x00011210 File Offset: 0x0000F410
		public RectTransform content
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

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x06000387 RID: 903 RVA: 0x0001121C File Offset: 0x0000F41C
		// (set) Token: 0x06000388 RID: 904 RVA: 0x00011224 File Offset: 0x0000F424
		public bool horizontal
		{
			get
			{
				return this.m_Horizontal;
			}
			set
			{
				this.m_Horizontal = value;
			}
		}

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x06000389 RID: 905 RVA: 0x00011230 File Offset: 0x0000F430
		// (set) Token: 0x0600038A RID: 906 RVA: 0x00011238 File Offset: 0x0000F438
		public bool vertical
		{
			get
			{
				return this.m_Vertical;
			}
			set
			{
				this.m_Vertical = value;
			}
		}

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x0600038B RID: 907 RVA: 0x00011244 File Offset: 0x0000F444
		// (set) Token: 0x0600038C RID: 908 RVA: 0x0001124C File Offset: 0x0000F44C
		public ScrollRect.MovementType movementType
		{
			get
			{
				return this.m_MovementType;
			}
			set
			{
				this.m_MovementType = value;
			}
		}

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x0600038D RID: 909 RVA: 0x00011258 File Offset: 0x0000F458
		// (set) Token: 0x0600038E RID: 910 RVA: 0x00011260 File Offset: 0x0000F460
		public float elasticity
		{
			get
			{
				return this.m_Elasticity;
			}
			set
			{
				this.m_Elasticity = value;
			}
		}

		// Token: 0x170000EE RID: 238
		// (get) Token: 0x0600038F RID: 911 RVA: 0x0001126C File Offset: 0x0000F46C
		// (set) Token: 0x06000390 RID: 912 RVA: 0x00011274 File Offset: 0x0000F474
		public bool inertia
		{
			get
			{
				return this.m_Inertia;
			}
			set
			{
				this.m_Inertia = value;
			}
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x06000391 RID: 913 RVA: 0x00011280 File Offset: 0x0000F480
		// (set) Token: 0x06000392 RID: 914 RVA: 0x00011288 File Offset: 0x0000F488
		public float decelerationRate
		{
			get
			{
				return this.m_DecelerationRate;
			}
			set
			{
				this.m_DecelerationRate = value;
			}
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x06000393 RID: 915 RVA: 0x00011294 File Offset: 0x0000F494
		// (set) Token: 0x06000394 RID: 916 RVA: 0x0001129C File Offset: 0x0000F49C
		public float scrollSensitivity
		{
			get
			{
				return this.m_ScrollSensitivity;
			}
			set
			{
				this.m_ScrollSensitivity = value;
			}
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x06000395 RID: 917 RVA: 0x000112A8 File Offset: 0x0000F4A8
		// (set) Token: 0x06000396 RID: 918 RVA: 0x000112B0 File Offset: 0x0000F4B0
		public RectTransform viewport
		{
			get
			{
				return this.m_Viewport;
			}
			set
			{
				this.m_Viewport = value;
				this.SetDirtyCaching();
			}
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x06000397 RID: 919 RVA: 0x000112C0 File Offset: 0x0000F4C0
		// (set) Token: 0x06000398 RID: 920 RVA: 0x000112C8 File Offset: 0x0000F4C8
		public Scrollbar horizontalScrollbar
		{
			get
			{
				return this.m_HorizontalScrollbar;
			}
			set
			{
				if (this.m_HorizontalScrollbar)
				{
					this.m_HorizontalScrollbar.onValueChanged.RemoveListener(new UnityAction<float>(this.SetHorizontalNormalizedPosition));
				}
				this.m_HorizontalScrollbar = value;
				if (this.m_HorizontalScrollbar)
				{
					this.m_HorizontalScrollbar.onValueChanged.AddListener(new UnityAction<float>(this.SetHorizontalNormalizedPosition));
				}
				this.SetDirtyCaching();
			}
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x06000399 RID: 921 RVA: 0x0001133C File Offset: 0x0000F53C
		// (set) Token: 0x0600039A RID: 922 RVA: 0x00011344 File Offset: 0x0000F544
		public Scrollbar verticalScrollbar
		{
			get
			{
				return this.m_VerticalScrollbar;
			}
			set
			{
				if (this.m_VerticalScrollbar)
				{
					this.m_VerticalScrollbar.onValueChanged.RemoveListener(new UnityAction<float>(this.SetVerticalNormalizedPosition));
				}
				this.m_VerticalScrollbar = value;
				if (this.m_VerticalScrollbar)
				{
					this.m_VerticalScrollbar.onValueChanged.AddListener(new UnityAction<float>(this.SetVerticalNormalizedPosition));
				}
				this.SetDirtyCaching();
			}
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x0600039B RID: 923 RVA: 0x000113B8 File Offset: 0x0000F5B8
		// (set) Token: 0x0600039C RID: 924 RVA: 0x000113C0 File Offset: 0x0000F5C0
		public ScrollRect.ScrollbarVisibility horizontalScrollbarVisibility
		{
			get
			{
				return this.m_HorizontalScrollbarVisibility;
			}
			set
			{
				this.m_HorizontalScrollbarVisibility = value;
				this.SetDirtyCaching();
			}
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x0600039D RID: 925 RVA: 0x000113D0 File Offset: 0x0000F5D0
		// (set) Token: 0x0600039E RID: 926 RVA: 0x000113D8 File Offset: 0x0000F5D8
		public ScrollRect.ScrollbarVisibility verticalScrollbarVisibility
		{
			get
			{
				return this.m_VerticalScrollbarVisibility;
			}
			set
			{
				this.m_VerticalScrollbarVisibility = value;
				this.SetDirtyCaching();
			}
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x0600039F RID: 927 RVA: 0x000113E8 File Offset: 0x0000F5E8
		// (set) Token: 0x060003A0 RID: 928 RVA: 0x000113F0 File Offset: 0x0000F5F0
		public float horizontalScrollbarSpacing
		{
			get
			{
				return this.m_HorizontalScrollbarSpacing;
			}
			set
			{
				this.m_HorizontalScrollbarSpacing = value;
				this.SetDirty();
			}
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x060003A1 RID: 929 RVA: 0x00011400 File Offset: 0x0000F600
		// (set) Token: 0x060003A2 RID: 930 RVA: 0x00011408 File Offset: 0x0000F608
		public float verticalScrollbarSpacing
		{
			get
			{
				return this.m_VerticalScrollbarSpacing;
			}
			set
			{
				this.m_VerticalScrollbarSpacing = value;
				this.SetDirty();
			}
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x060003A3 RID: 931 RVA: 0x00011418 File Offset: 0x0000F618
		// (set) Token: 0x060003A4 RID: 932 RVA: 0x00011420 File Offset: 0x0000F620
		public ScrollRect.ScrollRectEvent onValueChanged
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

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x060003A5 RID: 933 RVA: 0x0001142C File Offset: 0x0000F62C
		protected RectTransform viewRect
		{
			get
			{
				if (this.m_ViewRect == null)
				{
					this.m_ViewRect = this.m_Viewport;
				}
				if (this.m_ViewRect == null)
				{
					this.m_ViewRect = (RectTransform)base.transform;
				}
				return this.m_ViewRect;
			}
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x060003A6 RID: 934 RVA: 0x00011480 File Offset: 0x0000F680
		// (set) Token: 0x060003A7 RID: 935 RVA: 0x00011488 File Offset: 0x0000F688
		public Vector2 velocity
		{
			get
			{
				return this.m_Velocity;
			}
			set
			{
				this.m_Velocity = value;
			}
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x060003A8 RID: 936 RVA: 0x00011494 File Offset: 0x0000F694
		private RectTransform rectTransform
		{
			get
			{
				if (this.m_Rect == null)
				{
					this.m_Rect = base.GetComponent<RectTransform>();
				}
				return this.m_Rect;
			}
		}

		// Token: 0x060003A9 RID: 937 RVA: 0x000114BC File Offset: 0x0000F6BC
		public virtual void Rebuild(CanvasUpdate executing)
		{
			if (executing == CanvasUpdate.Prelayout)
			{
				this.UpdateCachedData();
			}
			if (executing == CanvasUpdate.PostLayout)
			{
				this.UpdateBounds();
				this.UpdateScrollbars(Vector2.zero);
				this.UpdatePrevData();
				this.m_HasRebuiltLayout = true;
			}
		}

		// Token: 0x060003AA RID: 938 RVA: 0x000114FC File Offset: 0x0000F6FC
		public virtual void LayoutComplete()
		{
		}

		// Token: 0x060003AB RID: 939 RVA: 0x00011500 File Offset: 0x0000F700
		public virtual void GraphicUpdateComplete()
		{
		}

		// Token: 0x060003AC RID: 940 RVA: 0x00011504 File Offset: 0x0000F704
		private void UpdateCachedData()
		{
			Transform transform = base.transform;
			this.m_HorizontalScrollbarRect = ((!(this.m_HorizontalScrollbar == null)) ? (this.m_HorizontalScrollbar.transform as RectTransform) : null);
			this.m_VerticalScrollbarRect = ((!(this.m_VerticalScrollbar == null)) ? (this.m_VerticalScrollbar.transform as RectTransform) : null);
			bool flag = this.viewRect.parent == transform;
			bool flag2 = !this.m_HorizontalScrollbarRect || this.m_HorizontalScrollbarRect.parent == transform;
			bool flag3 = !this.m_VerticalScrollbarRect || this.m_VerticalScrollbarRect.parent == transform;
			bool flag4 = flag && flag2 && flag3;
			this.m_HSliderExpand = (flag4 && this.m_HorizontalScrollbarRect && this.horizontalScrollbarVisibility == ScrollRect.ScrollbarVisibility.AutoHideAndExpandViewport);
			this.m_VSliderExpand = (flag4 && this.m_VerticalScrollbarRect && this.verticalScrollbarVisibility == ScrollRect.ScrollbarVisibility.AutoHideAndExpandViewport);
			this.m_HSliderHeight = ((!(this.m_HorizontalScrollbarRect == null)) ? this.m_HorizontalScrollbarRect.rect.height : 0f);
			this.m_VSliderWidth = ((!(this.m_VerticalScrollbarRect == null)) ? this.m_VerticalScrollbarRect.rect.width : 0f);
		}

		// Token: 0x060003AD RID: 941 RVA: 0x0001169C File Offset: 0x0000F89C
		protected override void OnEnable()
		{
			base.OnEnable();
			if (this.m_HorizontalScrollbar)
			{
				this.m_HorizontalScrollbar.onValueChanged.AddListener(new UnityAction<float>(this.SetHorizontalNormalizedPosition));
			}
			if (this.m_VerticalScrollbar)
			{
				this.m_VerticalScrollbar.onValueChanged.AddListener(new UnityAction<float>(this.SetVerticalNormalizedPosition));
			}
			CanvasUpdateRegistry.RegisterCanvasElementForLayoutRebuild(this);
		}

		// Token: 0x060003AE RID: 942 RVA: 0x00011710 File Offset: 0x0000F910
		protected override void OnDisable()
		{
			CanvasUpdateRegistry.UnRegisterCanvasElementForRebuild(this);
			if (this.m_HorizontalScrollbar)
			{
				this.m_HorizontalScrollbar.onValueChanged.RemoveListener(new UnityAction<float>(this.SetHorizontalNormalizedPosition));
			}
			if (this.m_VerticalScrollbar)
			{
				this.m_VerticalScrollbar.onValueChanged.RemoveListener(new UnityAction<float>(this.SetVerticalNormalizedPosition));
			}
			this.m_HasRebuiltLayout = false;
			this.m_Tracker.Clear();
			this.m_Velocity = Vector2.zero;
			LayoutRebuilder.MarkLayoutForRebuild(this.rectTransform);
			base.OnDisable();
		}

		// Token: 0x060003AF RID: 943 RVA: 0x000117AC File Offset: 0x0000F9AC
		public override bool IsActive()
		{
			return base.IsActive() && this.m_Content != null;
		}

		// Token: 0x060003B0 RID: 944 RVA: 0x000117C8 File Offset: 0x0000F9C8
		private void EnsureLayoutHasRebuilt()
		{
			if (!this.m_HasRebuiltLayout && !CanvasUpdateRegistry.IsRebuildingLayout())
			{
				Canvas.ForceUpdateCanvases();
			}
		}

		// Token: 0x060003B1 RID: 945 RVA: 0x000117E4 File Offset: 0x0000F9E4
		public virtual void StopMovement()
		{
			this.m_Velocity = Vector2.zero;
		}

		// Token: 0x060003B2 RID: 946 RVA: 0x000117F4 File Offset: 0x0000F9F4
		public virtual void OnScroll(PointerEventData data)
		{
			if (!this.IsActive())
			{
				return;
			}
			this.EnsureLayoutHasRebuilt();
			this.UpdateBounds();
			Vector2 scrollDelta = data.scrollDelta;
			scrollDelta.y *= -1f;
			if (this.vertical && !this.horizontal)
			{
				if (Mathf.Abs(scrollDelta.x) > Mathf.Abs(scrollDelta.y))
				{
					scrollDelta.y = scrollDelta.x;
				}
				scrollDelta.x = 0f;
			}
			if (this.horizontal && !this.vertical)
			{
				if (Mathf.Abs(scrollDelta.y) > Mathf.Abs(scrollDelta.x))
				{
					scrollDelta.x = scrollDelta.y;
				}
				scrollDelta.y = 0f;
			}
			Vector2 vector = this.m_Content.anchoredPosition;
			vector += scrollDelta * this.m_ScrollSensitivity;
			if (this.m_MovementType == ScrollRect.MovementType.Clamped)
			{
				vector += this.CalculateOffset(vector - this.m_Content.anchoredPosition);
			}
			this.SetContentAnchoredPosition(vector);
			this.UpdateBounds();
		}

		// Token: 0x060003B3 RID: 947 RVA: 0x00011924 File Offset: 0x0000FB24
		public virtual void OnInitializePotentialDrag(PointerEventData eventData)
		{
			if (eventData.button != PointerEventData.InputButton.Left)
			{
				return;
			}
			this.m_Velocity = Vector2.zero;
		}

		// Token: 0x060003B4 RID: 948 RVA: 0x00011940 File Offset: 0x0000FB40
		public virtual void OnBeginDrag(PointerEventData eventData)
		{
			if (eventData.button != PointerEventData.InputButton.Left)
			{
				return;
			}
			if (!this.IsActive())
			{
				return;
			}
			this.UpdateBounds();
			this.m_PointerStartLocalCursor = Vector2.zero;
			RectTransformUtility.ScreenPointToLocalPointInRectangle(this.viewRect, eventData.position, eventData.pressEventCamera, out this.m_PointerStartLocalCursor);
			this.m_ContentStartPosition = this.m_Content.anchoredPosition;
			this.m_Dragging = true;
		}

		// Token: 0x060003B5 RID: 949 RVA: 0x000119AC File Offset: 0x0000FBAC
		public virtual void OnEndDrag(PointerEventData eventData)
		{
			if (eventData.button != PointerEventData.InputButton.Left)
			{
				return;
			}
			this.m_Dragging = false;
		}

		// Token: 0x060003B6 RID: 950 RVA: 0x000119C4 File Offset: 0x0000FBC4
		public virtual void OnDrag(PointerEventData eventData)
		{
			if (eventData.button != PointerEventData.InputButton.Left)
			{
				return;
			}
			if (!this.IsActive())
			{
				return;
			}
			Vector2 a;
			if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(this.viewRect, eventData.position, eventData.pressEventCamera, out a))
			{
				return;
			}
			this.UpdateBounds();
			Vector2 b = a - this.m_PointerStartLocalCursor;
			Vector2 vector = this.m_ContentStartPosition + b;
			Vector2 b2 = this.CalculateOffset(vector - this.m_Content.anchoredPosition);
			vector += b2;
			if (this.m_MovementType == ScrollRect.MovementType.Elastic)
			{
				if (b2.x != 0f)
				{
					vector.x -= ScrollRect.RubberDelta(b2.x, this.m_ViewBounds.size.x);
				}
				if (b2.y != 0f)
				{
					vector.y -= ScrollRect.RubberDelta(b2.y, this.m_ViewBounds.size.y);
				}
			}
			this.SetContentAnchoredPosition(vector);
		}

		// Token: 0x060003B7 RID: 951 RVA: 0x00011ADC File Offset: 0x0000FCDC
		protected virtual void SetContentAnchoredPosition(Vector2 position)
		{
			if (!this.m_Horizontal)
			{
				position.x = this.m_Content.anchoredPosition.x;
			}
			if (!this.m_Vertical)
			{
				position.y = this.m_Content.anchoredPosition.y;
			}
			if (position != this.m_Content.anchoredPosition)
			{
				this.m_Content.anchoredPosition = position;
				this.UpdateBounds();
			}
		}

		// Token: 0x060003B8 RID: 952 RVA: 0x00011B5C File Offset: 0x0000FD5C
		protected virtual void LateUpdate()
		{
			if (!this.m_Content)
			{
				return;
			}
			this.EnsureLayoutHasRebuilt();
			this.UpdateScrollbarVisibility();
			this.UpdateBounds();
			float unscaledDeltaTime = Time.unscaledDeltaTime;
			Vector2 vector = this.CalculateOffset(Vector2.zero);
			if (!this.m_Dragging && (vector != Vector2.zero || this.m_Velocity != Vector2.zero))
			{
				Vector2 vector2 = this.m_Content.anchoredPosition;
				for (int i = 0; i < 2; i++)
				{
					if (this.m_MovementType == ScrollRect.MovementType.Elastic && vector[i] != 0f)
					{
						float value = this.m_Velocity[i];
						vector2[i] = Mathf.SmoothDamp(this.m_Content.anchoredPosition[i], this.m_Content.anchoredPosition[i] + vector[i], ref value, this.m_Elasticity, float.PositiveInfinity, unscaledDeltaTime);
						this.m_Velocity[i] = value;
					}
					else if (this.m_Inertia)
					{
						ref Vector2 ptr = ref this.m_Velocity;
						int index2;
						int index = index2 = i;
						float num = ptr[index2];
						this.m_Velocity[index] = num * Mathf.Pow(this.m_DecelerationRate, unscaledDeltaTime);
						if (Mathf.Abs(this.m_Velocity[i]) < 1f)
						{
							this.m_Velocity[i] = 0f;
						}
						ref Vector2 ptr2 = ref vector2;
						int index3 = index2 = i;
						num = ptr2[index2];
						vector2[index3] = num + this.m_Velocity[i] * unscaledDeltaTime;
					}
					else
					{
						this.m_Velocity[i] = 0f;
					}
				}
				if (this.m_Velocity != Vector2.zero)
				{
					if (this.m_MovementType == ScrollRect.MovementType.Clamped)
					{
						vector = this.CalculateOffset(vector2 - this.m_Content.anchoredPosition);
						vector2 += vector;
					}
					this.SetContentAnchoredPosition(vector2);
				}
			}
			if (this.m_Dragging && this.m_Inertia)
			{
				Vector3 b = (this.m_Content.anchoredPosition - this.m_PrevPosition) / unscaledDeltaTime;
				this.m_Velocity = Vector3.Lerp(this.m_Velocity, b, unscaledDeltaTime * 10f);
			}
			if (this.m_ViewBounds != this.m_PrevViewBounds || this.m_ContentBounds != this.m_PrevContentBounds || this.m_Content.anchoredPosition != this.m_PrevPosition)
			{
				this.UpdateScrollbars(vector);
				this.m_OnValueChanged.Invoke(this.normalizedPosition);
				this.UpdatePrevData();
			}
		}

		// Token: 0x060003B9 RID: 953 RVA: 0x00011E28 File Offset: 0x00010028
		private void UpdatePrevData()
		{
			if (this.m_Content == null)
			{
				this.m_PrevPosition = Vector2.zero;
			}
			else
			{
				this.m_PrevPosition = this.m_Content.anchoredPosition;
			}
			this.m_PrevViewBounds = this.m_ViewBounds;
			this.m_PrevContentBounds = this.m_ContentBounds;
		}

		// Token: 0x060003BA RID: 954 RVA: 0x00011E80 File Offset: 0x00010080
		private void UpdateScrollbars(Vector2 offset)
		{
			if (this.m_HorizontalScrollbar)
			{
				if (this.m_ContentBounds.size.x > 0f)
				{
					this.m_HorizontalScrollbar.size = Mathf.Clamp01((this.m_ViewBounds.size.x - Mathf.Abs(offset.x)) / this.m_ContentBounds.size.x);
				}
				else
				{
					this.m_HorizontalScrollbar.size = 1f;
				}
				this.m_HorizontalScrollbar.value = this.horizontalNormalizedPosition;
			}
			if (this.m_VerticalScrollbar)
			{
				if (this.m_ContentBounds.size.y > 0f)
				{
					this.m_VerticalScrollbar.size = Mathf.Clamp01((this.m_ViewBounds.size.y - Mathf.Abs(offset.y)) / this.m_ContentBounds.size.y);
				}
				else
				{
					this.m_VerticalScrollbar.size = 1f;
				}
				this.m_VerticalScrollbar.value = this.verticalNormalizedPosition;
			}
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x060003BB RID: 955 RVA: 0x00011FC0 File Offset: 0x000101C0
		// (set) Token: 0x060003BC RID: 956 RVA: 0x00011FD4 File Offset: 0x000101D4
		public Vector2 normalizedPosition
		{
			get
			{
				return new Vector2(this.horizontalNormalizedPosition, this.verticalNormalizedPosition);
			}
			set
			{
				this.SetNormalizedPosition(value.x, 0);
				this.SetNormalizedPosition(value.y, 1);
			}
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x060003BD RID: 957 RVA: 0x00011FF4 File Offset: 0x000101F4
		// (set) Token: 0x060003BE RID: 958 RVA: 0x000120BC File Offset: 0x000102BC
		public float horizontalNormalizedPosition
		{
			get
			{
				this.UpdateBounds();
				if (this.m_ContentBounds.size.x <= this.m_ViewBounds.size.x)
				{
					return (float)((this.m_ViewBounds.min.x <= this.m_ContentBounds.min.x) ? 0 : 1);
				}
				return (this.m_ViewBounds.min.x - this.m_ContentBounds.min.x) / (this.m_ContentBounds.size.x - this.m_ViewBounds.size.x);
			}
			set
			{
				this.SetNormalizedPosition(value, 0);
			}
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x060003BF RID: 959 RVA: 0x000120C8 File Offset: 0x000102C8
		// (set) Token: 0x060003C0 RID: 960 RVA: 0x00012190 File Offset: 0x00010390
		public float verticalNormalizedPosition
		{
			get
			{
				this.UpdateBounds();
				if (this.m_ContentBounds.size.y <= this.m_ViewBounds.size.y)
				{
					return (float)((this.m_ViewBounds.min.y <= this.m_ContentBounds.min.y) ? 0 : 1);
				}
				return (this.m_ViewBounds.min.y - this.m_ContentBounds.min.y) / (this.m_ContentBounds.size.y - this.m_ViewBounds.size.y);
			}
			set
			{
				this.SetNormalizedPosition(value, 1);
			}
		}

		// Token: 0x060003C1 RID: 961 RVA: 0x0001219C File Offset: 0x0001039C
		private void SetHorizontalNormalizedPosition(float value)
		{
			this.SetNormalizedPosition(value, 0);
		}

		// Token: 0x060003C2 RID: 962 RVA: 0x000121A8 File Offset: 0x000103A8
		private void SetVerticalNormalizedPosition(float value)
		{
			this.SetNormalizedPosition(value, 1);
		}

		// Token: 0x060003C3 RID: 963 RVA: 0x000121B4 File Offset: 0x000103B4
		private void SetNormalizedPosition(float value, int axis)
		{
			this.EnsureLayoutHasRebuilt();
			this.UpdateBounds();
			float num = this.m_ContentBounds.size[axis] - this.m_ViewBounds.size[axis];
			float num2 = this.m_ViewBounds.min[axis] - value * num;
			float num3 = this.m_Content.localPosition[axis] + num2 - this.m_ContentBounds.min[axis];
			Vector3 localPosition = this.m_Content.localPosition;
			if (Mathf.Abs(localPosition[axis] - num3) > 0.01f)
			{
				localPosition[axis] = num3;
				this.m_Content.localPosition = localPosition;
				this.m_Velocity[axis] = 0f;
				this.UpdateBounds();
			}
		}

		// Token: 0x060003C4 RID: 964 RVA: 0x00012294 File Offset: 0x00010494
		private static float RubberDelta(float overStretching, float viewSize)
		{
			return (1f - 1f / (Mathf.Abs(overStretching) * 0.55f / viewSize + 1f)) * viewSize * Mathf.Sign(overStretching);
		}

		// Token: 0x060003C5 RID: 965 RVA: 0x000122C0 File Offset: 0x000104C0
		protected override void OnRectTransformDimensionsChange()
		{
			this.SetDirty();
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x060003C6 RID: 966 RVA: 0x000122C8 File Offset: 0x000104C8
		private bool hScrollingNeeded
		{
			get
			{
				return !Application.isPlaying || this.m_ContentBounds.size.x > this.m_ViewBounds.size.x + 0.01f;
			}
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x060003C7 RID: 967 RVA: 0x00012310 File Offset: 0x00010510
		private bool vScrollingNeeded
		{
			get
			{
				return !Application.isPlaying || this.m_ContentBounds.size.y > this.m_ViewBounds.size.y + 0.01f;
			}
		}

		// Token: 0x060003C8 RID: 968 RVA: 0x00012358 File Offset: 0x00010558
		public virtual void CalculateLayoutInputHorizontal()
		{
		}

		// Token: 0x060003C9 RID: 969 RVA: 0x0001235C File Offset: 0x0001055C
		public virtual void CalculateLayoutInputVertical()
		{
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x060003CA RID: 970 RVA: 0x00012360 File Offset: 0x00010560
		public virtual float minWidth
		{
			get
			{
				return -1f;
			}
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x060003CB RID: 971 RVA: 0x00012368 File Offset: 0x00010568
		public virtual float preferredWidth
		{
			get
			{
				return -1f;
			}
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x060003CC RID: 972 RVA: 0x00012370 File Offset: 0x00010570
		public virtual float flexibleWidth
		{
			get
			{
				return -1f;
			}
		}

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x060003CD RID: 973 RVA: 0x00012378 File Offset: 0x00010578
		public virtual float minHeight
		{
			get
			{
				return -1f;
			}
		}

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x060003CE RID: 974 RVA: 0x00012380 File Offset: 0x00010580
		public virtual float preferredHeight
		{
			get
			{
				return -1f;
			}
		}

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x060003CF RID: 975 RVA: 0x00012388 File Offset: 0x00010588
		public virtual float flexibleHeight
		{
			get
			{
				return -1f;
			}
		}

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x060003D0 RID: 976 RVA: 0x00012390 File Offset: 0x00010590
		public virtual int layoutPriority
		{
			get
			{
				return -1;
			}
		}

		// Token: 0x060003D1 RID: 977 RVA: 0x00012394 File Offset: 0x00010594
		public virtual void SetLayoutHorizontal()
		{
			this.m_Tracker.Clear();
			if (this.m_HSliderExpand || this.m_VSliderExpand)
			{
				this.m_Tracker.Add(this, this.viewRect, DrivenTransformProperties.AnchoredPositionX | DrivenTransformProperties.AnchoredPositionY | DrivenTransformProperties.AnchorMinX | DrivenTransformProperties.AnchorMinY | DrivenTransformProperties.AnchorMaxX | DrivenTransformProperties.AnchorMaxY | DrivenTransformProperties.SizeDeltaX | DrivenTransformProperties.SizeDeltaY);
				this.viewRect.anchorMin = Vector2.zero;
				this.viewRect.anchorMax = Vector2.one;
				this.viewRect.sizeDelta = Vector2.zero;
				this.viewRect.anchoredPosition = Vector2.zero;
				LayoutRebuilder.ForceRebuildLayoutImmediate(this.content);
				this.m_ViewBounds = new Bounds(this.viewRect.rect.center, this.viewRect.rect.size);
				this.m_ContentBounds = this.GetBounds();
			}
			if (this.m_VSliderExpand && this.vScrollingNeeded)
			{
				this.viewRect.sizeDelta = new Vector2(-(this.m_VSliderWidth + this.m_VerticalScrollbarSpacing), this.viewRect.sizeDelta.y);
				LayoutRebuilder.ForceRebuildLayoutImmediate(this.content);
				this.m_ViewBounds = new Bounds(this.viewRect.rect.center, this.viewRect.rect.size);
				this.m_ContentBounds = this.GetBounds();
			}
			if (this.m_HSliderExpand && this.hScrollingNeeded)
			{
				this.viewRect.sizeDelta = new Vector2(this.viewRect.sizeDelta.x, -(this.m_HSliderHeight + this.m_HorizontalScrollbarSpacing));
				this.m_ViewBounds = new Bounds(this.viewRect.rect.center, this.viewRect.rect.size);
				this.m_ContentBounds = this.GetBounds();
			}
			if (this.m_VSliderExpand && this.vScrollingNeeded && this.viewRect.sizeDelta.x == 0f && this.viewRect.sizeDelta.y < 0f)
			{
				this.viewRect.sizeDelta = new Vector2(-(this.m_VSliderWidth + this.m_VerticalScrollbarSpacing), this.viewRect.sizeDelta.y);
			}
		}

		// Token: 0x060003D2 RID: 978 RVA: 0x0001261C File Offset: 0x0001081C
		public virtual void SetLayoutVertical()
		{
			this.UpdateScrollbarLayout();
			this.m_ViewBounds = new Bounds(this.viewRect.rect.center, this.viewRect.rect.size);
			this.m_ContentBounds = this.GetBounds();
		}

		// Token: 0x060003D3 RID: 979 RVA: 0x00012678 File Offset: 0x00010878
		private void UpdateScrollbarVisibility()
		{
			if (this.m_VerticalScrollbar && this.m_VerticalScrollbarVisibility != ScrollRect.ScrollbarVisibility.Permanent && this.m_VerticalScrollbar.gameObject.activeSelf != this.vScrollingNeeded)
			{
				this.m_VerticalScrollbar.gameObject.SetActive(this.vScrollingNeeded);
			}
			if (this.m_HorizontalScrollbar && this.m_HorizontalScrollbarVisibility != ScrollRect.ScrollbarVisibility.Permanent && this.m_HorizontalScrollbar.gameObject.activeSelf != this.hScrollingNeeded)
			{
				this.m_HorizontalScrollbar.gameObject.SetActive(this.hScrollingNeeded);
			}
		}

		// Token: 0x060003D4 RID: 980 RVA: 0x00012720 File Offset: 0x00010920
		private void UpdateScrollbarLayout()
		{
			if (this.m_VSliderExpand && this.m_HorizontalScrollbar)
			{
				this.m_Tracker.Add(this, this.m_HorizontalScrollbarRect, DrivenTransformProperties.AnchoredPositionX | DrivenTransformProperties.AnchorMinX | DrivenTransformProperties.AnchorMaxX | DrivenTransformProperties.SizeDeltaX);
				this.m_HorizontalScrollbarRect.anchorMin = new Vector2(0f, this.m_HorizontalScrollbarRect.anchorMin.y);
				this.m_HorizontalScrollbarRect.anchorMax = new Vector2(1f, this.m_HorizontalScrollbarRect.anchorMax.y);
				this.m_HorizontalScrollbarRect.anchoredPosition = new Vector2(0f, this.m_HorizontalScrollbarRect.anchoredPosition.y);
				if (this.vScrollingNeeded)
				{
					this.m_HorizontalScrollbarRect.sizeDelta = new Vector2(-(this.m_VSliderWidth + this.m_VerticalScrollbarSpacing), this.m_HorizontalScrollbarRect.sizeDelta.y);
				}
				else
				{
					this.m_HorizontalScrollbarRect.sizeDelta = new Vector2(0f, this.m_HorizontalScrollbarRect.sizeDelta.y);
				}
			}
			if (this.m_HSliderExpand && this.m_VerticalScrollbar)
			{
				this.m_Tracker.Add(this, this.m_VerticalScrollbarRect, DrivenTransformProperties.AnchoredPositionY | DrivenTransformProperties.AnchorMinY | DrivenTransformProperties.AnchorMaxY | DrivenTransformProperties.SizeDeltaY);
				this.m_VerticalScrollbarRect.anchorMin = new Vector2(this.m_VerticalScrollbarRect.anchorMin.x, 0f);
				this.m_VerticalScrollbarRect.anchorMax = new Vector2(this.m_VerticalScrollbarRect.anchorMax.x, 1f);
				this.m_VerticalScrollbarRect.anchoredPosition = new Vector2(this.m_VerticalScrollbarRect.anchoredPosition.x, 0f);
				if (this.hScrollingNeeded)
				{
					this.m_VerticalScrollbarRect.sizeDelta = new Vector2(this.m_VerticalScrollbarRect.sizeDelta.x, -(this.m_HSliderHeight + this.m_HorizontalScrollbarSpacing));
				}
				else
				{
					this.m_VerticalScrollbarRect.sizeDelta = new Vector2(this.m_VerticalScrollbarRect.sizeDelta.x, 0f);
				}
			}
		}

		// Token: 0x060003D5 RID: 981 RVA: 0x0001295C File Offset: 0x00010B5C
		private void UpdateBounds()
		{
			this.m_ViewBounds = new Bounds(this.viewRect.rect.center, this.viewRect.rect.size);
			this.m_ContentBounds = this.GetBounds();
			if (this.m_Content == null)
			{
				return;
			}
			Vector3 size = this.m_ContentBounds.size;
			Vector3 center = this.m_ContentBounds.center;
			Vector3 vector = this.m_ViewBounds.size - size;
			if (vector.x > 0f)
			{
				center.x -= vector.x * (this.m_Content.pivot.x - 0.5f);
				size.x = this.m_ViewBounds.size.x;
			}
			if (vector.y > 0f)
			{
				center.y -= vector.y * (this.m_Content.pivot.y - 0.5f);
				size.y = this.m_ViewBounds.size.y;
			}
			this.m_ContentBounds.size = size;
			this.m_ContentBounds.center = center;
		}

		// Token: 0x060003D6 RID: 982 RVA: 0x00012AC0 File Offset: 0x00010CC0
		private Bounds GetBounds()
		{
			if (this.m_Content == null)
			{
				return default(Bounds);
			}
			Vector3 vector = new Vector3(float.MaxValue, float.MaxValue, float.MaxValue);
			Vector3 vector2 = new Vector3(float.MinValue, float.MinValue, float.MinValue);
			Matrix4x4 worldToLocalMatrix = this.viewRect.worldToLocalMatrix;
			this.m_Content.GetWorldCorners(this.m_Corners);
			for (int i = 0; i < 4; i++)
			{
				Vector3 lhs = worldToLocalMatrix.MultiplyPoint3x4(this.m_Corners[i]);
				vector = Vector3.Min(lhs, vector);
				vector2 = Vector3.Max(lhs, vector2);
			}
			Bounds result = new Bounds(vector, Vector3.zero);
			result.Encapsulate(vector2);
			return result;
		}

		// Token: 0x060003D7 RID: 983 RVA: 0x00012B88 File Offset: 0x00010D88
		private Vector2 CalculateOffset(Vector2 delta)
		{
			Vector2 zero = Vector2.zero;
			if (this.m_MovementType == ScrollRect.MovementType.Unrestricted)
			{
				return zero;
			}
			Vector2 vector = this.m_ContentBounds.min;
			Vector2 vector2 = this.m_ContentBounds.max;
			if (this.m_Horizontal)
			{
				vector.x += delta.x;
				vector2.x += delta.x;
				if (vector.x > this.m_ViewBounds.min.x)
				{
					zero.x = this.m_ViewBounds.min.x - vector.x;
				}
				else if (vector2.x < this.m_ViewBounds.max.x)
				{
					zero.x = this.m_ViewBounds.max.x - vector2.x;
				}
			}
			if (this.m_Vertical)
			{
				vector.y += delta.y;
				vector2.y += delta.y;
				if (vector2.y < this.m_ViewBounds.max.y)
				{
					zero.y = this.m_ViewBounds.max.y - vector2.y;
				}
				else if (vector.y > this.m_ViewBounds.min.y)
				{
					zero.y = this.m_ViewBounds.min.y - vector.y;
				}
			}
			return zero;
		}

		// Token: 0x060003D8 RID: 984 RVA: 0x00012D4C File Offset: 0x00010F4C
		protected void SetDirty()
		{
			if (!this.IsActive())
			{
				return;
			}
			LayoutRebuilder.MarkLayoutForRebuild(this.rectTransform);
		}

		// Token: 0x060003D9 RID: 985 RVA: 0x00012D68 File Offset: 0x00010F68
		protected void SetDirtyCaching()
		{
			if (!this.IsActive())
			{
				return;
			}
			CanvasUpdateRegistry.RegisterCanvasElementForLayoutRebuild(this);
			LayoutRebuilder.MarkLayoutForRebuild(this.rectTransform);
		}

		// Token: 0x060003DA RID: 986 RVA: 0x00012D88 File Offset: 0x00010F88
		virtual bool IsDestroyed()
		{
			return base.IsDestroyed();
		}

		// Token: 0x060003DB RID: 987 RVA: 0x00012D90 File Offset: 0x00010F90
		virtual Transform get_transform()
		{
			return base.transform;
		}

		// Token: 0x040001C0 RID: 448
		[SerializeField]
		private RectTransform m_Content;

		// Token: 0x040001C1 RID: 449
		[SerializeField]
		private bool m_Horizontal = true;

		// Token: 0x040001C2 RID: 450
		[SerializeField]
		private bool m_Vertical = true;

		// Token: 0x040001C3 RID: 451
		[SerializeField]
		private ScrollRect.MovementType m_MovementType = ScrollRect.MovementType.Elastic;

		// Token: 0x040001C4 RID: 452
		[SerializeField]
		private float m_Elasticity = 0.1f;

		// Token: 0x040001C5 RID: 453
		[SerializeField]
		private bool m_Inertia = true;

		// Token: 0x040001C6 RID: 454
		[SerializeField]
		private float m_DecelerationRate = 0.135f;

		// Token: 0x040001C7 RID: 455
		[SerializeField]
		private float m_ScrollSensitivity = 1f;

		// Token: 0x040001C8 RID: 456
		[SerializeField]
		private RectTransform m_Viewport;

		// Token: 0x040001C9 RID: 457
		[SerializeField]
		private Scrollbar m_HorizontalScrollbar;

		// Token: 0x040001CA RID: 458
		[SerializeField]
		private Scrollbar m_VerticalScrollbar;

		// Token: 0x040001CB RID: 459
		[SerializeField]
		private ScrollRect.ScrollbarVisibility m_HorizontalScrollbarVisibility;

		// Token: 0x040001CC RID: 460
		[SerializeField]
		private ScrollRect.ScrollbarVisibility m_VerticalScrollbarVisibility;

		// Token: 0x040001CD RID: 461
		[SerializeField]
		private float m_HorizontalScrollbarSpacing;

		// Token: 0x040001CE RID: 462
		[SerializeField]
		private float m_VerticalScrollbarSpacing;

		// Token: 0x040001CF RID: 463
		[SerializeField]
		private ScrollRect.ScrollRectEvent m_OnValueChanged = new ScrollRect.ScrollRectEvent();

		// Token: 0x040001D0 RID: 464
		private Vector2 m_PointerStartLocalCursor = Vector2.zero;

		// Token: 0x040001D1 RID: 465
		private Vector2 m_ContentStartPosition = Vector2.zero;

		// Token: 0x040001D2 RID: 466
		private RectTransform m_ViewRect;

		// Token: 0x040001D3 RID: 467
		private Bounds m_ContentBounds;

		// Token: 0x040001D4 RID: 468
		private Bounds m_ViewBounds;

		// Token: 0x040001D5 RID: 469
		private Vector2 m_Velocity;

		// Token: 0x040001D6 RID: 470
		private bool m_Dragging;

		// Token: 0x040001D7 RID: 471
		private Vector2 m_PrevPosition = Vector2.zero;

		// Token: 0x040001D8 RID: 472
		private Bounds m_PrevContentBounds;

		// Token: 0x040001D9 RID: 473
		private Bounds m_PrevViewBounds;

		// Token: 0x040001DA RID: 474
		[NonSerialized]
		private bool m_HasRebuiltLayout;

		// Token: 0x040001DB RID: 475
		private bool m_HSliderExpand;

		// Token: 0x040001DC RID: 476
		private bool m_VSliderExpand;

		// Token: 0x040001DD RID: 477
		private float m_HSliderHeight;

		// Token: 0x040001DE RID: 478
		private float m_VSliderWidth;

		// Token: 0x040001DF RID: 479
		[NonSerialized]
		private RectTransform m_Rect;

		// Token: 0x040001E0 RID: 480
		private RectTransform m_HorizontalScrollbarRect;

		// Token: 0x040001E1 RID: 481
		private RectTransform m_VerticalScrollbarRect;

		// Token: 0x040001E2 RID: 482
		private DrivenRectTransformTracker m_Tracker;

		// Token: 0x040001E3 RID: 483
		private readonly Vector3[] m_Corners = new Vector3[4];

		// Token: 0x0200006C RID: 108
		public enum MovementType
		{
			// Token: 0x040001E5 RID: 485
			Unrestricted,
			// Token: 0x040001E6 RID: 486
			Elastic,
			// Token: 0x040001E7 RID: 487
			Clamped
		}

		// Token: 0x0200006D RID: 109
		public enum ScrollbarVisibility
		{
			// Token: 0x040001E9 RID: 489
			Permanent,
			// Token: 0x040001EA RID: 490
			AutoHide,
			// Token: 0x040001EB RID: 491
			AutoHideAndExpandViewport
		}

		// Token: 0x0200006E RID: 110
		[Serializable]
		public class ScrollRectEvent : UnityEvent<Vector2>
		{
		}
	}
}
