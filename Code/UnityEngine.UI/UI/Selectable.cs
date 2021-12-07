using System;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

namespace UnityEngine.UI
{
	// Token: 0x0200006F RID: 111
	[ExecuteInEditMode]
	[DisallowMultipleComponent]
	[SelectionBase]
	[AddComponentMenu("UI/Selectable", 70)]
	public class Selectable : UIBehaviour, IEventSystemHandler, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler, ISelectHandler, IDeselectHandler, IMoveHandler
	{
		// Token: 0x060003DD RID: 989 RVA: 0x00012DA0 File Offset: 0x00010FA0
		protected Selectable()
		{
		}

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x060003DF RID: 991 RVA: 0x00012E00 File Offset: 0x00011000
		public static List<Selectable> allSelectables
		{
			get
			{
				return Selectable.s_List;
			}
		}

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x060003E0 RID: 992 RVA: 0x00012E08 File Offset: 0x00011008
		// (set) Token: 0x060003E1 RID: 993 RVA: 0x00012E10 File Offset: 0x00011010
		public Navigation navigation
		{
			get
			{
				return this.m_Navigation;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<Navigation>(ref this.m_Navigation, value))
				{
					this.OnSetProperty();
				}
			}
		}

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x060003E2 RID: 994 RVA: 0x00012E2C File Offset: 0x0001102C
		// (set) Token: 0x060003E3 RID: 995 RVA: 0x00012E34 File Offset: 0x00011034
		public Selectable.Transition transition
		{
			get
			{
				return this.m_Transition;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<Selectable.Transition>(ref this.m_Transition, value))
				{
					this.OnSetProperty();
				}
			}
		}

		// Token: 0x1700010B RID: 267
		// (get) Token: 0x060003E4 RID: 996 RVA: 0x00012E50 File Offset: 0x00011050
		// (set) Token: 0x060003E5 RID: 997 RVA: 0x00012E58 File Offset: 0x00011058
		public ColorBlock colors
		{
			get
			{
				return this.m_Colors;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<ColorBlock>(ref this.m_Colors, value))
				{
					this.OnSetProperty();
				}
			}
		}

		// Token: 0x1700010C RID: 268
		// (get) Token: 0x060003E6 RID: 998 RVA: 0x00012E74 File Offset: 0x00011074
		// (set) Token: 0x060003E7 RID: 999 RVA: 0x00012E7C File Offset: 0x0001107C
		public SpriteState spriteState
		{
			get
			{
				return this.m_SpriteState;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<SpriteState>(ref this.m_SpriteState, value))
				{
					this.OnSetProperty();
				}
			}
		}

		// Token: 0x1700010D RID: 269
		// (get) Token: 0x060003E8 RID: 1000 RVA: 0x00012E98 File Offset: 0x00011098
		// (set) Token: 0x060003E9 RID: 1001 RVA: 0x00012EA0 File Offset: 0x000110A0
		public AnimationTriggers animationTriggers
		{
			get
			{
				return this.m_AnimationTriggers;
			}
			set
			{
				if (SetPropertyUtility.SetClass<AnimationTriggers>(ref this.m_AnimationTriggers, value))
				{
					this.OnSetProperty();
				}
			}
		}

		// Token: 0x1700010E RID: 270
		// (get) Token: 0x060003EA RID: 1002 RVA: 0x00012EBC File Offset: 0x000110BC
		// (set) Token: 0x060003EB RID: 1003 RVA: 0x00012EC4 File Offset: 0x000110C4
		public Graphic targetGraphic
		{
			get
			{
				return this.m_TargetGraphic;
			}
			set
			{
				if (SetPropertyUtility.SetClass<Graphic>(ref this.m_TargetGraphic, value))
				{
					this.OnSetProperty();
				}
			}
		}

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x060003EC RID: 1004 RVA: 0x00012EE0 File Offset: 0x000110E0
		// (set) Token: 0x060003ED RID: 1005 RVA: 0x00012EE8 File Offset: 0x000110E8
		public bool interactable
		{
			get
			{
				return this.m_Interactable;
			}
			set
			{
				if (SetPropertyUtility.SetStruct<bool>(ref this.m_Interactable, value))
				{
					this.OnSetProperty();
				}
			}
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x060003EE RID: 1006 RVA: 0x00012F04 File Offset: 0x00011104
		// (set) Token: 0x060003EF RID: 1007 RVA: 0x00012F0C File Offset: 0x0001110C
		private bool isPointerInside { get; set; }

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x060003F0 RID: 1008 RVA: 0x00012F18 File Offset: 0x00011118
		// (set) Token: 0x060003F1 RID: 1009 RVA: 0x00012F20 File Offset: 0x00011120
		private bool isPointerDown { get; set; }

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x060003F2 RID: 1010 RVA: 0x00012F2C File Offset: 0x0001112C
		// (set) Token: 0x060003F3 RID: 1011 RVA: 0x00012F34 File Offset: 0x00011134
		private bool hasSelection { get; set; }

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x060003F4 RID: 1012 RVA: 0x00012F40 File Offset: 0x00011140
		// (set) Token: 0x060003F5 RID: 1013 RVA: 0x00012F50 File Offset: 0x00011150
		public Image image
		{
			get
			{
				return this.m_TargetGraphic as Image;
			}
			set
			{
				this.m_TargetGraphic = value;
			}
		}

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x060003F6 RID: 1014 RVA: 0x00012F5C File Offset: 0x0001115C
		public Animator animator
		{
			get
			{
				return base.GetComponent<Animator>();
			}
		}

		// Token: 0x060003F7 RID: 1015 RVA: 0x00012F64 File Offset: 0x00011164
		protected override void Awake()
		{
			if (this.m_TargetGraphic == null)
			{
				this.m_TargetGraphic = base.GetComponent<Graphic>();
			}
		}

		// Token: 0x060003F8 RID: 1016 RVA: 0x00012F84 File Offset: 0x00011184
		protected override void OnCanvasGroupChanged()
		{
			bool flag = true;
			Transform transform = base.transform;
			while (transform != null)
			{
				transform.GetComponents<CanvasGroup>(this.m_CanvasGroupCache);
				bool flag2 = false;
				for (int i = 0; i < this.m_CanvasGroupCache.Count; i++)
				{
					if (!this.m_CanvasGroupCache[i].interactable)
					{
						flag = false;
						flag2 = true;
					}
					if (this.m_CanvasGroupCache[i].ignoreParentGroups)
					{
						flag2 = true;
					}
				}
				if (flag2)
				{
					break;
				}
				transform = transform.parent;
			}
			if (flag != this.m_GroupsAllowInteraction)
			{
				this.m_GroupsAllowInteraction = flag;
				this.OnSetProperty();
			}
		}

		// Token: 0x060003F9 RID: 1017 RVA: 0x00013034 File Offset: 0x00011234
		public virtual bool IsInteractable()
		{
			return this.m_GroupsAllowInteraction && this.m_Interactable;
		}

		// Token: 0x060003FA RID: 1018 RVA: 0x0001304C File Offset: 0x0001124C
		protected override void OnDidApplyAnimationProperties()
		{
			this.OnSetProperty();
		}

		// Token: 0x060003FB RID: 1019 RVA: 0x00013054 File Offset: 0x00011254
		protected override void OnEnable()
		{
			base.OnEnable();
			Selectable.s_List.Add(this);
			Selectable.SelectionState currentSelectionState = Selectable.SelectionState.Normal;
			if (this.hasSelection)
			{
				currentSelectionState = Selectable.SelectionState.Highlighted;
			}
			this.m_CurrentSelectionState = currentSelectionState;
			this.InternalEvaluateAndTransitionToSelectionState(true);
		}

		// Token: 0x060003FC RID: 1020 RVA: 0x00013090 File Offset: 0x00011290
		private void OnSetProperty()
		{
			this.InternalEvaluateAndTransitionToSelectionState(false);
		}

		// Token: 0x060003FD RID: 1021 RVA: 0x0001309C File Offset: 0x0001129C
		protected override void OnDisable()
		{
			Selectable.s_List.Remove(this);
			this.InstantClearState();
			base.OnDisable();
		}

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x060003FE RID: 1022 RVA: 0x000130B8 File Offset: 0x000112B8
		protected Selectable.SelectionState currentSelectionState
		{
			get
			{
				return this.m_CurrentSelectionState;
			}
		}

		// Token: 0x060003FF RID: 1023 RVA: 0x000130C0 File Offset: 0x000112C0
		protected virtual void InstantClearState()
		{
			string normalTrigger = this.m_AnimationTriggers.normalTrigger;
			this.isPointerInside = false;
			this.isPointerDown = false;
			this.hasSelection = false;
			switch (this.m_Transition)
			{
			case Selectable.Transition.ColorTint:
				this.StartColorTween(Color.white, true);
				break;
			case Selectable.Transition.SpriteSwap:
				this.DoSpriteSwap(null);
				break;
			case Selectable.Transition.Animation:
				this.TriggerAnimation(normalTrigger);
				break;
			}
		}

		// Token: 0x06000400 RID: 1024 RVA: 0x00013138 File Offset: 0x00011338
		protected virtual void DoStateTransition(Selectable.SelectionState state, bool instant)
		{
			Color a;
			Sprite newSprite;
			string triggername;
			switch (state)
			{
			case Selectable.SelectionState.Normal:
				a = this.m_Colors.normalColor;
				newSprite = null;
				triggername = this.m_AnimationTriggers.normalTrigger;
				break;
			case Selectable.SelectionState.Highlighted:
				a = this.m_Colors.highlightedColor;
				newSprite = this.m_SpriteState.highlightedSprite;
				triggername = this.m_AnimationTriggers.highlightedTrigger;
				break;
			case Selectable.SelectionState.Pressed:
				a = this.m_Colors.pressedColor;
				newSprite = this.m_SpriteState.pressedSprite;
				triggername = this.m_AnimationTriggers.pressedTrigger;
				break;
			case Selectable.SelectionState.Disabled:
				a = this.m_Colors.disabledColor;
				newSprite = this.m_SpriteState.disabledSprite;
				triggername = this.m_AnimationTriggers.disabledTrigger;
				break;
			default:
				a = Color.black;
				newSprite = null;
				triggername = string.Empty;
				break;
			}
			if (base.gameObject.activeInHierarchy)
			{
				switch (this.m_Transition)
				{
				case Selectable.Transition.ColorTint:
					this.StartColorTween(a * this.m_Colors.colorMultiplier, instant);
					break;
				case Selectable.Transition.SpriteSwap:
					this.DoSpriteSwap(newSprite);
					break;
				case Selectable.Transition.Animation:
					this.TriggerAnimation(triggername);
					break;
				}
			}
		}

		// Token: 0x06000401 RID: 1025 RVA: 0x00013278 File Offset: 0x00011478
		public Selectable FindSelectable(Vector3 dir)
		{
			dir = dir.normalized;
			Vector3 v = Quaternion.Inverse(base.transform.rotation) * dir;
			Vector3 b = base.transform.TransformPoint(Selectable.GetPointOnRectEdge(base.transform as RectTransform, v));
			float num = float.NegativeInfinity;
			Selectable result = null;
			for (int i = 0; i < Selectable.s_List.Count; i++)
			{
				Selectable selectable = Selectable.s_List[i];
				if (!(selectable == this) && !(selectable == null))
				{
					if (selectable.IsInteractable() && selectable.navigation.mode != Navigation.Mode.None)
					{
						RectTransform rectTransform = selectable.transform as RectTransform;
						Vector3 position = (!(rectTransform != null)) ? Vector3.zero : rectTransform.rect.center;
						Vector3 rhs = selectable.transform.TransformPoint(position) - b;
						float num2 = Vector3.Dot(dir, rhs);
						if (num2 > 0f)
						{
							float num3 = num2 / rhs.sqrMagnitude;
							if (num3 > num)
							{
								num = num3;
								result = selectable;
							}
						}
					}
				}
			}
			return result;
		}

		// Token: 0x06000402 RID: 1026 RVA: 0x000133CC File Offset: 0x000115CC
		private static Vector3 GetPointOnRectEdge(RectTransform rect, Vector2 dir)
		{
			if (rect == null)
			{
				return Vector3.zero;
			}
			if (dir != Vector2.zero)
			{
				dir /= Mathf.Max(Mathf.Abs(dir.x), Mathf.Abs(dir.y));
			}
			dir = rect.rect.center + Vector2.Scale(rect.rect.size, dir * 0.5f);
			return dir;
		}

		// Token: 0x06000403 RID: 1027 RVA: 0x0001345C File Offset: 0x0001165C
		private void Navigate(AxisEventData eventData, Selectable sel)
		{
			if (sel != null && sel.IsActive())
			{
				eventData.selectedObject = sel.gameObject;
			}
		}

		// Token: 0x06000404 RID: 1028 RVA: 0x00013484 File Offset: 0x00011684
		public virtual Selectable FindSelectableOnLeft()
		{
			if (this.m_Navigation.mode == Navigation.Mode.Explicit)
			{
				return this.m_Navigation.selectOnLeft;
			}
			if ((this.m_Navigation.mode & Navigation.Mode.Horizontal) != Navigation.Mode.None)
			{
				return this.FindSelectable(base.transform.rotation * Vector3.left);
			}
			return null;
		}

		// Token: 0x06000405 RID: 1029 RVA: 0x000134E0 File Offset: 0x000116E0
		public virtual Selectable FindSelectableOnRight()
		{
			if (this.m_Navigation.mode == Navigation.Mode.Explicit)
			{
				return this.m_Navigation.selectOnRight;
			}
			if ((this.m_Navigation.mode & Navigation.Mode.Horizontal) != Navigation.Mode.None)
			{
				return this.FindSelectable(base.transform.rotation * Vector3.right);
			}
			return null;
		}

		// Token: 0x06000406 RID: 1030 RVA: 0x0001353C File Offset: 0x0001173C
		public virtual Selectable FindSelectableOnUp()
		{
			if (this.m_Navigation.mode == Navigation.Mode.Explicit)
			{
				return this.m_Navigation.selectOnUp;
			}
			if ((this.m_Navigation.mode & Navigation.Mode.Vertical) != Navigation.Mode.None)
			{
				return this.FindSelectable(base.transform.rotation * Vector3.up);
			}
			return null;
		}

		// Token: 0x06000407 RID: 1031 RVA: 0x00013598 File Offset: 0x00011798
		public virtual Selectable FindSelectableOnDown()
		{
			if (this.m_Navigation.mode == Navigation.Mode.Explicit)
			{
				return this.m_Navigation.selectOnDown;
			}
			if ((this.m_Navigation.mode & Navigation.Mode.Vertical) != Navigation.Mode.None)
			{
				return this.FindSelectable(base.transform.rotation * Vector3.down);
			}
			return null;
		}

		// Token: 0x06000408 RID: 1032 RVA: 0x000135F4 File Offset: 0x000117F4
		public virtual void OnMove(AxisEventData eventData)
		{
			switch (eventData.moveDir)
			{
			case MoveDirection.Left:
				this.Navigate(eventData, this.FindSelectableOnLeft());
				break;
			case MoveDirection.Up:
				this.Navigate(eventData, this.FindSelectableOnUp());
				break;
			case MoveDirection.Right:
				this.Navigate(eventData, this.FindSelectableOnRight());
				break;
			case MoveDirection.Down:
				this.Navigate(eventData, this.FindSelectableOnDown());
				break;
			}
		}

		// Token: 0x06000409 RID: 1033 RVA: 0x0001366C File Offset: 0x0001186C
		private void StartColorTween(Color targetColor, bool instant)
		{
			if (this.m_TargetGraphic == null)
			{
				return;
			}
			this.m_TargetGraphic.CrossFadeColor(targetColor, (!instant) ? this.m_Colors.fadeDuration : 0f, true, true);
		}

		// Token: 0x0600040A RID: 1034 RVA: 0x000136B4 File Offset: 0x000118B4
		private void DoSpriteSwap(Sprite newSprite)
		{
			if (this.image == null)
			{
				return;
			}
			this.image.overrideSprite = newSprite;
		}

		// Token: 0x0600040B RID: 1035 RVA: 0x000136D4 File Offset: 0x000118D4
		private void TriggerAnimation(string triggername)
		{
			if (this.animator == null || !this.animator.isActiveAndEnabled || this.animator.runtimeAnimatorController == null || string.IsNullOrEmpty(triggername))
			{
				return;
			}
			this.animator.ResetTrigger(this.m_AnimationTriggers.normalTrigger);
			this.animator.ResetTrigger(this.m_AnimationTriggers.pressedTrigger);
			this.animator.ResetTrigger(this.m_AnimationTriggers.highlightedTrigger);
			this.animator.ResetTrigger(this.m_AnimationTriggers.disabledTrigger);
			this.animator.SetTrigger(triggername);
		}

		// Token: 0x0600040C RID: 1036 RVA: 0x00013788 File Offset: 0x00011988
		protected bool IsHighlighted(BaseEventData eventData)
		{
			if (!this.IsActive())
			{
				return false;
			}
			if (this.IsPressed())
			{
				return false;
			}
			bool flag = this.hasSelection;
			if (eventData is PointerEventData)
			{
				PointerEventData pointerEventData = eventData as PointerEventData;
				flag |= ((this.isPointerDown && !this.isPointerInside && pointerEventData.pointerPress == base.gameObject) || (!this.isPointerDown && this.isPointerInside && pointerEventData.pointerPress == base.gameObject) || (!this.isPointerDown && this.isPointerInside && pointerEventData.pointerPress == null));
			}
			else
			{
				flag |= this.isPointerInside;
			}
			return flag;
		}

		// Token: 0x0600040D RID: 1037 RVA: 0x0001385C File Offset: 0x00011A5C
		[Obsolete("Is Pressed no longer requires eventData", false)]
		protected bool IsPressed(BaseEventData eventData)
		{
			return this.IsPressed();
		}

		// Token: 0x0600040E RID: 1038 RVA: 0x00013864 File Offset: 0x00011A64
		protected bool IsPressed()
		{
			return this.IsActive() && this.isPointerInside && this.isPointerDown;
		}

		// Token: 0x0600040F RID: 1039 RVA: 0x00013894 File Offset: 0x00011A94
		protected void UpdateSelectionState(BaseEventData eventData)
		{
			if (this.IsPressed())
			{
				this.m_CurrentSelectionState = Selectable.SelectionState.Pressed;
				return;
			}
			if (this.IsHighlighted(eventData))
			{
				this.m_CurrentSelectionState = Selectable.SelectionState.Highlighted;
				return;
			}
			this.m_CurrentSelectionState = Selectable.SelectionState.Normal;
		}

		// Token: 0x06000410 RID: 1040 RVA: 0x000138D0 File Offset: 0x00011AD0
		private void EvaluateAndTransitionToSelectionState(BaseEventData eventData)
		{
			if (!this.IsActive())
			{
				return;
			}
			this.UpdateSelectionState(eventData);
			this.InternalEvaluateAndTransitionToSelectionState(false);
		}

		// Token: 0x06000411 RID: 1041 RVA: 0x000138EC File Offset: 0x00011AEC
		private void InternalEvaluateAndTransitionToSelectionState(bool instant)
		{
			Selectable.SelectionState state = this.m_CurrentSelectionState;
			if (this.IsActive() && !this.IsInteractable())
			{
				state = Selectable.SelectionState.Disabled;
			}
			this.DoStateTransition(state, instant);
		}

		// Token: 0x06000412 RID: 1042 RVA: 0x00013920 File Offset: 0x00011B20
		public virtual void OnPointerDown(PointerEventData eventData)
		{
			if (eventData.button != PointerEventData.InputButton.Left)
			{
				return;
			}
			if (this.IsInteractable() && this.navigation.mode != Navigation.Mode.None)
			{
				EventSystem.current.SetSelectedGameObject(base.gameObject, eventData);
			}
			this.isPointerDown = true;
			this.EvaluateAndTransitionToSelectionState(eventData);
		}

		// Token: 0x06000413 RID: 1043 RVA: 0x00013978 File Offset: 0x00011B78
		public virtual void OnPointerUp(PointerEventData eventData)
		{
			if (eventData.button != PointerEventData.InputButton.Left)
			{
				return;
			}
			this.isPointerDown = false;
			this.EvaluateAndTransitionToSelectionState(eventData);
		}

		// Token: 0x06000414 RID: 1044 RVA: 0x00013994 File Offset: 0x00011B94
		public virtual void OnPointerEnter(PointerEventData eventData)
		{
			this.isPointerInside = true;
			this.EvaluateAndTransitionToSelectionState(eventData);
		}

		// Token: 0x06000415 RID: 1045 RVA: 0x000139A4 File Offset: 0x00011BA4
		public virtual void OnPointerExit(PointerEventData eventData)
		{
			this.isPointerInside = false;
			this.EvaluateAndTransitionToSelectionState(eventData);
		}

		// Token: 0x06000416 RID: 1046 RVA: 0x000139B4 File Offset: 0x00011BB4
		public virtual void OnSelect(BaseEventData eventData)
		{
			this.hasSelection = true;
			this.EvaluateAndTransitionToSelectionState(eventData);
		}

		// Token: 0x06000417 RID: 1047 RVA: 0x000139C4 File Offset: 0x00011BC4
		public virtual void OnDeselect(BaseEventData eventData)
		{
			this.hasSelection = false;
			this.EvaluateAndTransitionToSelectionState(eventData);
		}

		// Token: 0x06000418 RID: 1048 RVA: 0x000139D4 File Offset: 0x00011BD4
		public virtual void Select()
		{
			if (EventSystem.current.alreadySelecting)
			{
				return;
			}
			EventSystem.current.SetSelectedGameObject(base.gameObject);
		}

		// Token: 0x040001EC RID: 492
		private static List<Selectable> s_List = new List<Selectable>();

		// Token: 0x040001ED RID: 493
		[SerializeField]
		[FormerlySerializedAs("navigation")]
		private Navigation m_Navigation = Navigation.defaultNavigation;

		// Token: 0x040001EE RID: 494
		[FormerlySerializedAs("transition")]
		[SerializeField]
		private Selectable.Transition m_Transition = Selectable.Transition.ColorTint;

		// Token: 0x040001EF RID: 495
		[SerializeField]
		[FormerlySerializedAs("colors")]
		private ColorBlock m_Colors = ColorBlock.defaultColorBlock;

		// Token: 0x040001F0 RID: 496
		[FormerlySerializedAs("spriteState")]
		[SerializeField]
		private SpriteState m_SpriteState;

		// Token: 0x040001F1 RID: 497
		[SerializeField]
		[FormerlySerializedAs("animationTriggers")]
		private AnimationTriggers m_AnimationTriggers = new AnimationTriggers();

		// Token: 0x040001F2 RID: 498
		[Tooltip("Can the Selectable be interacted with?")]
		[SerializeField]
		private bool m_Interactable = true;

		// Token: 0x040001F3 RID: 499
		[SerializeField]
		[FormerlySerializedAs("m_HighlightGraphic")]
		[FormerlySerializedAs("highlightGraphic")]
		private Graphic m_TargetGraphic;

		// Token: 0x040001F4 RID: 500
		private bool m_GroupsAllowInteraction = true;

		// Token: 0x040001F5 RID: 501
		private Selectable.SelectionState m_CurrentSelectionState;

		// Token: 0x040001F6 RID: 502
		private readonly List<CanvasGroup> m_CanvasGroupCache = new List<CanvasGroup>();

		// Token: 0x02000070 RID: 112
		public enum Transition
		{
			// Token: 0x040001FB RID: 507
			None,
			// Token: 0x040001FC RID: 508
			ColorTint,
			// Token: 0x040001FD RID: 509
			SpriteSwap,
			// Token: 0x040001FE RID: 510
			Animation
		}

		// Token: 0x02000071 RID: 113
		protected enum SelectionState
		{
			// Token: 0x04000200 RID: 512
			Normal,
			// Token: 0x04000201 RID: 513
			Highlighted,
			// Token: 0x04000202 RID: 514
			Pressed,
			// Token: 0x04000203 RID: 515
			Disabled
		}
	}
}
