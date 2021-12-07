using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI.CoroutineTween;

namespace UnityEngine.UI
{
	// Token: 0x02000040 RID: 64
	[RequireComponent(typeof(RectTransform))]
	[AddComponentMenu("UI/Dropdown", 35)]
	public class Dropdown : Selectable, IEventSystemHandler, IPointerClickHandler, ISubmitHandler, ICancelHandler
	{
		// Token: 0x060001A3 RID: 419 RVA: 0x000075DC File Offset: 0x000057DC
		protected Dropdown()
		{
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x060001A5 RID: 421 RVA: 0x00007614 File Offset: 0x00005814
		// (set) Token: 0x060001A6 RID: 422 RVA: 0x0000761C File Offset: 0x0000581C
		public RectTransform template
		{
			get
			{
				return this.m_Template;
			}
			set
			{
				this.m_Template = value;
				this.RefreshShownValue();
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060001A7 RID: 423 RVA: 0x0000762C File Offset: 0x0000582C
		// (set) Token: 0x060001A8 RID: 424 RVA: 0x00007634 File Offset: 0x00005834
		public Text captionText
		{
			get
			{
				return this.m_CaptionText;
			}
			set
			{
				this.m_CaptionText = value;
				this.RefreshShownValue();
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x060001A9 RID: 425 RVA: 0x00007644 File Offset: 0x00005844
		// (set) Token: 0x060001AA RID: 426 RVA: 0x0000764C File Offset: 0x0000584C
		public Image captionImage
		{
			get
			{
				return this.m_CaptionImage;
			}
			set
			{
				this.m_CaptionImage = value;
				this.RefreshShownValue();
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x060001AB RID: 427 RVA: 0x0000765C File Offset: 0x0000585C
		// (set) Token: 0x060001AC RID: 428 RVA: 0x00007664 File Offset: 0x00005864
		public Text itemText
		{
			get
			{
				return this.m_ItemText;
			}
			set
			{
				this.m_ItemText = value;
				this.RefreshShownValue();
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x060001AD RID: 429 RVA: 0x00007674 File Offset: 0x00005874
		// (set) Token: 0x060001AE RID: 430 RVA: 0x0000767C File Offset: 0x0000587C
		public Image itemImage
		{
			get
			{
				return this.m_ItemImage;
			}
			set
			{
				this.m_ItemImage = value;
				this.RefreshShownValue();
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x060001AF RID: 431 RVA: 0x0000768C File Offset: 0x0000588C
		// (set) Token: 0x060001B0 RID: 432 RVA: 0x0000769C File Offset: 0x0000589C
		public List<Dropdown.OptionData> options
		{
			get
			{
				return this.m_Options.options;
			}
			set
			{
				this.m_Options.options = value;
				this.RefreshShownValue();
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x060001B1 RID: 433 RVA: 0x000076B0 File Offset: 0x000058B0
		// (set) Token: 0x060001B2 RID: 434 RVA: 0x000076B8 File Offset: 0x000058B8
		public Dropdown.DropdownEvent onValueChanged
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

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x060001B3 RID: 435 RVA: 0x000076C4 File Offset: 0x000058C4
		// (set) Token: 0x060001B4 RID: 436 RVA: 0x000076CC File Offset: 0x000058CC
		public int value
		{
			get
			{
				return this.m_Value;
			}
			set
			{
				if (Application.isPlaying && (value == this.m_Value || this.options.Count == 0))
				{
					return;
				}
				this.m_Value = Mathf.Clamp(value, 0, this.options.Count - 1);
				this.RefreshShownValue();
				this.m_OnValueChanged.Invoke(this.m_Value);
			}
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x00007734 File Offset: 0x00005934
		protected override void Awake()
		{
			this.m_AlphaTweenRunner = new TweenRunner<FloatTween>();
			this.m_AlphaTweenRunner.Init(this);
			if (this.m_CaptionImage)
			{
				this.m_CaptionImage.enabled = (this.m_CaptionImage.sprite != null);
			}
			if (this.m_Template)
			{
				this.m_Template.gameObject.SetActive(false);
			}
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x000077A8 File Offset: 0x000059A8
		public void RefreshShownValue()
		{
			Dropdown.OptionData optionData = Dropdown.s_NoOptionData;
			if (this.options.Count > 0)
			{
				optionData = this.options[Mathf.Clamp(this.m_Value, 0, this.options.Count - 1)];
			}
			if (this.m_CaptionText)
			{
				if (optionData != null && optionData.text != null)
				{
					this.m_CaptionText.text = optionData.text;
				}
				else
				{
					this.m_CaptionText.text = string.Empty;
				}
			}
			if (this.m_CaptionImage)
			{
				if (optionData != null)
				{
					this.m_CaptionImage.sprite = optionData.image;
				}
				else
				{
					this.m_CaptionImage.sprite = null;
				}
				this.m_CaptionImage.enabled = (this.m_CaptionImage.sprite != null);
			}
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x0000788C File Offset: 0x00005A8C
		public void AddOptions(List<Dropdown.OptionData> options)
		{
			this.options.AddRange(options);
			this.RefreshShownValue();
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x000078A0 File Offset: 0x00005AA0
		public void AddOptions(List<string> options)
		{
			for (int i = 0; i < options.Count; i++)
			{
				this.options.Add(new Dropdown.OptionData(options[i]));
			}
			this.RefreshShownValue();
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x000078E4 File Offset: 0x00005AE4
		public void AddOptions(List<Sprite> options)
		{
			for (int i = 0; i < options.Count; i++)
			{
				this.options.Add(new Dropdown.OptionData(options[i]));
			}
			this.RefreshShownValue();
		}

		// Token: 0x060001BA RID: 442 RVA: 0x00007928 File Offset: 0x00005B28
		public void ClearOptions()
		{
			this.options.Clear();
			this.RefreshShownValue();
		}

		// Token: 0x060001BB RID: 443 RVA: 0x0000793C File Offset: 0x00005B3C
		private void SetupTemplate()
		{
			this.validTemplate = false;
			if (!this.m_Template)
			{
				Debug.LogError("The dropdown template is not assigned. The template needs to be assigned and must have a child GameObject with a Toggle component serving as the item.", this);
				return;
			}
			GameObject gameObject = this.m_Template.gameObject;
			gameObject.SetActive(true);
			Toggle componentInChildren = this.m_Template.GetComponentInChildren<Toggle>();
			this.validTemplate = true;
			if (!componentInChildren || componentInChildren.transform == this.template)
			{
				this.validTemplate = false;
				Debug.LogError("The dropdown template is not valid. The template must have a child GameObject with a Toggle component serving as the item.", this.template);
			}
			else if (!(componentInChildren.transform.parent is RectTransform))
			{
				this.validTemplate = false;
				Debug.LogError("The dropdown template is not valid. The child GameObject with a Toggle component (the item) must have a RectTransform on its parent.", this.template);
			}
			else if (this.itemText != null && !this.itemText.transform.IsChildOf(componentInChildren.transform))
			{
				this.validTemplate = false;
				Debug.LogError("The dropdown template is not valid. The Item Text must be on the item GameObject or children of it.", this.template);
			}
			else if (this.itemImage != null && !this.itemImage.transform.IsChildOf(componentInChildren.transform))
			{
				this.validTemplate = false;
				Debug.LogError("The dropdown template is not valid. The Item Image must be on the item GameObject or children of it.", this.template);
			}
			if (!this.validTemplate)
			{
				gameObject.SetActive(false);
				return;
			}
			Dropdown.DropdownItem dropdownItem = componentInChildren.gameObject.AddComponent<Dropdown.DropdownItem>();
			dropdownItem.text = this.m_ItemText;
			dropdownItem.image = this.m_ItemImage;
			dropdownItem.toggle = componentInChildren;
			dropdownItem.rectTransform = (RectTransform)componentInChildren.transform;
			Canvas orAddComponent = Dropdown.GetOrAddComponent<Canvas>(gameObject);
			orAddComponent.overrideSorting = true;
			orAddComponent.sortingOrder = 30000;
			Dropdown.GetOrAddComponent<GraphicRaycaster>(gameObject);
			Dropdown.GetOrAddComponent<CanvasGroup>(gameObject);
			gameObject.SetActive(false);
			this.validTemplate = true;
		}

		// Token: 0x060001BC RID: 444 RVA: 0x00007B10 File Offset: 0x00005D10
		private static T GetOrAddComponent<T>(GameObject go) where T : Component
		{
			T t = go.GetComponent<T>();
			if (!t)
			{
				t = go.AddComponent<T>();
			}
			return t;
		}

		// Token: 0x060001BD RID: 445 RVA: 0x00007B3C File Offset: 0x00005D3C
		public virtual void OnPointerClick(PointerEventData eventData)
		{
			this.Show();
		}

		// Token: 0x060001BE RID: 446 RVA: 0x00007B44 File Offset: 0x00005D44
		public virtual void OnSubmit(BaseEventData eventData)
		{
			this.Show();
		}

		// Token: 0x060001BF RID: 447 RVA: 0x00007B4C File Offset: 0x00005D4C
		public virtual void OnCancel(BaseEventData eventData)
		{
			this.Hide();
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x00007B54 File Offset: 0x00005D54
		public void Show()
		{
			if (!this.IsActive() || !this.IsInteractable() || this.m_Dropdown != null)
			{
				return;
			}
			if (!this.validTemplate)
			{
				this.SetupTemplate();
				if (!this.validTemplate)
				{
					return;
				}
			}
			List<Canvas> list = ListPool<Canvas>.Get();
			base.gameObject.GetComponentsInParent<Canvas>(false, list);
			if (list.Count == 0)
			{
				return;
			}
			Canvas canvas = list[0];
			ListPool<Canvas>.Release(list);
			this.m_Template.gameObject.SetActive(true);
			this.m_Dropdown = this.CreateDropdownList(this.m_Template.gameObject);
			this.m_Dropdown.name = "Dropdown List";
			this.m_Dropdown.SetActive(true);
			RectTransform rectTransform = this.m_Dropdown.transform as RectTransform;
			rectTransform.SetParent(this.m_Template.transform.parent, false);
			Dropdown.DropdownItem componentInChildren = this.m_Dropdown.GetComponentInChildren<Dropdown.DropdownItem>();
			GameObject gameObject = componentInChildren.rectTransform.parent.gameObject;
			RectTransform rectTransform2 = gameObject.transform as RectTransform;
			componentInChildren.rectTransform.gameObject.SetActive(true);
			Rect rect = rectTransform2.rect;
			Rect rect2 = componentInChildren.rectTransform.rect;
			Vector2 vector = rect2.min - rect.min + componentInChildren.rectTransform.localPosition;
			Vector2 vector2 = rect2.max - rect.max + componentInChildren.rectTransform.localPosition;
			Vector2 size = rect2.size;
			this.m_Items.Clear();
			Toggle toggle = null;
			for (int i = 0; i < this.options.Count; i++)
			{
				Dropdown.OptionData data = this.options[i];
				Dropdown.DropdownItem item = this.AddItem(data, this.value == i, componentInChildren, this.m_Items);
				if (!(item == null))
				{
					item.toggle.isOn = (this.value == i);
					item.toggle.onValueChanged.AddListener(delegate(bool x)
					{
						this.OnSelectItem(item.toggle);
					});
					if (item.toggle.isOn)
					{
						item.toggle.Select();
					}
					if (toggle != null)
					{
						Navigation navigation = toggle.navigation;
						Navigation navigation2 = item.toggle.navigation;
						navigation.mode = Navigation.Mode.Explicit;
						navigation2.mode = Navigation.Mode.Explicit;
						navigation.selectOnDown = item.toggle;
						navigation.selectOnRight = item.toggle;
						navigation2.selectOnLeft = toggle;
						navigation2.selectOnUp = toggle;
						toggle.navigation = navigation;
						item.toggle.navigation = navigation2;
					}
					toggle = item.toggle;
				}
			}
			Vector2 sizeDelta = rectTransform2.sizeDelta;
			sizeDelta.y = size.y * (float)this.m_Items.Count + vector.y - vector2.y;
			rectTransform2.sizeDelta = sizeDelta;
			float num = rectTransform.rect.height - rectTransform2.rect.height;
			if (num > 0f)
			{
				rectTransform.sizeDelta = new Vector2(rectTransform.sizeDelta.x, rectTransform.sizeDelta.y - num);
			}
			Vector3[] array = new Vector3[4];
			rectTransform.GetWorldCorners(array);
			RectTransform rectTransform3 = canvas.transform as RectTransform;
			Rect rect3 = rectTransform3.rect;
			for (int j = 0; j < 2; j++)
			{
				bool flag = false;
				for (int k = 0; k < 4; k++)
				{
					Vector3 vector3 = rectTransform3.InverseTransformPoint(array[k]);
					if (vector3[j] < rect3.min[j] || vector3[j] > rect3.max[j])
					{
						flag = true;
						break;
					}
				}
				if (flag)
				{
					RectTransformUtility.FlipLayoutOnAxis(rectTransform, j, false, false);
				}
			}
			for (int l = 0; l < this.m_Items.Count; l++)
			{
				RectTransform rectTransform4 = this.m_Items[l].rectTransform;
				rectTransform4.anchorMin = new Vector2(rectTransform4.anchorMin.x, 0f);
				rectTransform4.anchorMax = new Vector2(rectTransform4.anchorMax.x, 0f);
				rectTransform4.anchoredPosition = new Vector2(rectTransform4.anchoredPosition.x, vector.y + size.y * (float)(this.m_Items.Count - 1 - l) + size.y * rectTransform4.pivot.y);
				rectTransform4.sizeDelta = new Vector2(rectTransform4.sizeDelta.x, size.y);
			}
			this.AlphaFadeList(0.15f, 0f, 1f);
			this.m_Template.gameObject.SetActive(false);
			componentInChildren.gameObject.SetActive(false);
			this.m_Blocker = this.CreateBlocker(canvas);
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x000080FC File Offset: 0x000062FC
		protected virtual GameObject CreateBlocker(Canvas rootCanvas)
		{
			GameObject gameObject = new GameObject("Blocker");
			RectTransform rectTransform = gameObject.AddComponent<RectTransform>();
			rectTransform.SetParent(rootCanvas.transform, false);
			rectTransform.anchorMin = Vector3.zero;
			rectTransform.anchorMax = Vector3.one;
			rectTransform.sizeDelta = Vector2.zero;
			Canvas canvas = gameObject.AddComponent<Canvas>();
			canvas.overrideSorting = true;
			Canvas component = this.m_Dropdown.GetComponent<Canvas>();
			canvas.sortingLayerID = component.sortingLayerID;
			canvas.sortingOrder = component.sortingOrder - 1;
			gameObject.AddComponent<GraphicRaycaster>();
			Image image = gameObject.AddComponent<Image>();
			image.color = Color.clear;
			Button button = gameObject.AddComponent<Button>();
			button.onClick.AddListener(new UnityAction(this.Hide));
			return gameObject;
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x000081C4 File Offset: 0x000063C4
		protected virtual void DestroyBlocker(GameObject blocker)
		{
			Object.Destroy(blocker);
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x000081CC File Offset: 0x000063CC
		protected virtual GameObject CreateDropdownList(GameObject template)
		{
			return Object.Instantiate<GameObject>(template);
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x000081D4 File Offset: 0x000063D4
		protected virtual void DestroyDropdownList(GameObject dropdownList)
		{
			Object.Destroy(dropdownList);
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x000081DC File Offset: 0x000063DC
		protected virtual Dropdown.DropdownItem CreateItem(Dropdown.DropdownItem itemTemplate)
		{
			return Object.Instantiate<Dropdown.DropdownItem>(itemTemplate);
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x000081E4 File Offset: 0x000063E4
		protected virtual void DestroyItem(Dropdown.DropdownItem item)
		{
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x000081E8 File Offset: 0x000063E8
		private Dropdown.DropdownItem AddItem(Dropdown.OptionData data, bool selected, Dropdown.DropdownItem itemTemplate, List<Dropdown.DropdownItem> items)
		{
			Dropdown.DropdownItem dropdownItem = this.CreateItem(itemTemplate);
			dropdownItem.rectTransform.SetParent(itemTemplate.rectTransform.parent, false);
			dropdownItem.gameObject.SetActive(true);
			dropdownItem.gameObject.name = "Item " + items.Count + ((data.text == null) ? string.Empty : (": " + data.text));
			if (dropdownItem.toggle != null)
			{
				dropdownItem.toggle.isOn = false;
			}
			if (dropdownItem.text)
			{
				dropdownItem.text.text = data.text;
			}
			if (dropdownItem.image)
			{
				dropdownItem.image.sprite = data.image;
				dropdownItem.image.enabled = (dropdownItem.image.sprite != null);
			}
			items.Add(dropdownItem);
			return dropdownItem;
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x000082EC File Offset: 0x000064EC
		private void AlphaFadeList(float duration, float alpha)
		{
			CanvasGroup component = this.m_Dropdown.GetComponent<CanvasGroup>();
			this.AlphaFadeList(duration, component.alpha, alpha);
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x00008314 File Offset: 0x00006514
		private void AlphaFadeList(float duration, float start, float end)
		{
			if (end.Equals(start))
			{
				return;
			}
			FloatTween info = new FloatTween
			{
				duration = duration,
				startValue = start,
				targetValue = end
			};
			info.AddOnChangedCallback(new UnityAction<float>(this.SetAlpha));
			info.ignoreTimeScale = true;
			this.m_AlphaTweenRunner.StartTween(info);
		}

		// Token: 0x060001CA RID: 458 RVA: 0x0000837C File Offset: 0x0000657C
		private void SetAlpha(float alpha)
		{
			if (!this.m_Dropdown)
			{
				return;
			}
			CanvasGroup component = this.m_Dropdown.GetComponent<CanvasGroup>();
			component.alpha = alpha;
		}

		// Token: 0x060001CB RID: 459 RVA: 0x000083B0 File Offset: 0x000065B0
		public void Hide()
		{
			if (this.m_Dropdown != null)
			{
				this.AlphaFadeList(0.15f, 0f);
				base.StartCoroutine(this.DelayedDestroyDropdownList(0.15f));
			}
			if (this.m_Blocker != null)
			{
				this.DestroyBlocker(this.m_Blocker);
			}
			this.m_Blocker = null;
			this.Select();
		}

		// Token: 0x060001CC RID: 460 RVA: 0x0000841C File Offset: 0x0000661C
		private IEnumerator DelayedDestroyDropdownList(float delay)
		{
			float waitTime = Time.realtimeSinceStartup + delay;
			while (Time.realtimeSinceStartup < waitTime)
			{
				yield return null;
			}
			for (int i = 0; i < this.m_Items.Count; i++)
			{
				if (this.m_Items[i] != null)
				{
					this.DestroyItem(this.m_Items[i]);
				}
				this.m_Items.Clear();
			}
			if (this.m_Dropdown != null)
			{
				this.DestroyDropdownList(this.m_Dropdown);
			}
			this.m_Dropdown = null;
			yield break;
		}

		// Token: 0x060001CD RID: 461 RVA: 0x00008448 File Offset: 0x00006648
		private void OnSelectItem(Toggle toggle)
		{
			if (!toggle.isOn)
			{
				toggle.isOn = true;
			}
			int num = -1;
			Transform transform = toggle.transform;
			Transform parent = transform.parent;
			for (int i = 0; i < parent.childCount; i++)
			{
				if (parent.GetChild(i) == transform)
				{
					num = i - 1;
					break;
				}
			}
			if (num < 0)
			{
				return;
			}
			this.value = num;
			this.Hide();
		}

		// Token: 0x040000CD RID: 205
		[SerializeField]
		private RectTransform m_Template;

		// Token: 0x040000CE RID: 206
		[SerializeField]
		private Text m_CaptionText;

		// Token: 0x040000CF RID: 207
		[SerializeField]
		private Image m_CaptionImage;

		// Token: 0x040000D0 RID: 208
		[Space]
		[SerializeField]
		private Text m_ItemText;

		// Token: 0x040000D1 RID: 209
		[SerializeField]
		private Image m_ItemImage;

		// Token: 0x040000D2 RID: 210
		[SerializeField]
		[Space]
		private int m_Value;

		// Token: 0x040000D3 RID: 211
		[Space]
		[SerializeField]
		private Dropdown.OptionDataList m_Options = new Dropdown.OptionDataList();

		// Token: 0x040000D4 RID: 212
		[SerializeField]
		[Space]
		private Dropdown.DropdownEvent m_OnValueChanged = new Dropdown.DropdownEvent();

		// Token: 0x040000D5 RID: 213
		private GameObject m_Dropdown;

		// Token: 0x040000D6 RID: 214
		private GameObject m_Blocker;

		// Token: 0x040000D7 RID: 215
		private List<Dropdown.DropdownItem> m_Items = new List<Dropdown.DropdownItem>();

		// Token: 0x040000D8 RID: 216
		private TweenRunner<FloatTween> m_AlphaTweenRunner;

		// Token: 0x040000D9 RID: 217
		private bool validTemplate;

		// Token: 0x040000DA RID: 218
		private static Dropdown.OptionData s_NoOptionData = new Dropdown.OptionData();

		// Token: 0x02000041 RID: 65
		protected internal class DropdownItem : MonoBehaviour, IEventSystemHandler, IPointerEnterHandler, ICancelHandler
		{
			// Token: 0x17000070 RID: 112
			// (get) Token: 0x060001CF RID: 463 RVA: 0x000084C8 File Offset: 0x000066C8
			// (set) Token: 0x060001D0 RID: 464 RVA: 0x000084D0 File Offset: 0x000066D0
			public Text text
			{
				get
				{
					return this.m_Text;
				}
				set
				{
					this.m_Text = value;
				}
			}

			// Token: 0x17000071 RID: 113
			// (get) Token: 0x060001D1 RID: 465 RVA: 0x000084DC File Offset: 0x000066DC
			// (set) Token: 0x060001D2 RID: 466 RVA: 0x000084E4 File Offset: 0x000066E4
			public Image image
			{
				get
				{
					return this.m_Image;
				}
				set
				{
					this.m_Image = value;
				}
			}

			// Token: 0x17000072 RID: 114
			// (get) Token: 0x060001D3 RID: 467 RVA: 0x000084F0 File Offset: 0x000066F0
			// (set) Token: 0x060001D4 RID: 468 RVA: 0x000084F8 File Offset: 0x000066F8
			public RectTransform rectTransform
			{
				get
				{
					return this.m_RectTransform;
				}
				set
				{
					this.m_RectTransform = value;
				}
			}

			// Token: 0x17000073 RID: 115
			// (get) Token: 0x060001D5 RID: 469 RVA: 0x00008504 File Offset: 0x00006704
			// (set) Token: 0x060001D6 RID: 470 RVA: 0x0000850C File Offset: 0x0000670C
			public Toggle toggle
			{
				get
				{
					return this.m_Toggle;
				}
				set
				{
					this.m_Toggle = value;
				}
			}

			// Token: 0x060001D7 RID: 471 RVA: 0x00008518 File Offset: 0x00006718
			public virtual void OnPointerEnter(PointerEventData eventData)
			{
				EventSystem.current.SetSelectedGameObject(base.gameObject);
			}

			// Token: 0x060001D8 RID: 472 RVA: 0x0000852C File Offset: 0x0000672C
			public virtual void OnCancel(BaseEventData eventData)
			{
				Dropdown componentInParent = base.GetComponentInParent<Dropdown>();
				if (componentInParent)
				{
					componentInParent.Hide();
				}
			}

			// Token: 0x040000DB RID: 219
			[SerializeField]
			private Text m_Text;

			// Token: 0x040000DC RID: 220
			[SerializeField]
			private Image m_Image;

			// Token: 0x040000DD RID: 221
			[SerializeField]
			private RectTransform m_RectTransform;

			// Token: 0x040000DE RID: 222
			[SerializeField]
			private Toggle m_Toggle;
		}

		// Token: 0x02000042 RID: 66
		[Serializable]
		public class OptionData
		{
			// Token: 0x060001D9 RID: 473 RVA: 0x00008554 File Offset: 0x00006754
			public OptionData()
			{
			}

			// Token: 0x060001DA RID: 474 RVA: 0x0000855C File Offset: 0x0000675C
			public OptionData(string text)
			{
				this.text = text;
			}

			// Token: 0x060001DB RID: 475 RVA: 0x0000856C File Offset: 0x0000676C
			public OptionData(Sprite image)
			{
				this.image = image;
			}

			// Token: 0x060001DC RID: 476 RVA: 0x0000857C File Offset: 0x0000677C
			public OptionData(string text, Sprite image)
			{
				this.text = text;
				this.image = image;
			}

			// Token: 0x17000074 RID: 116
			// (get) Token: 0x060001DD RID: 477 RVA: 0x00008594 File Offset: 0x00006794
			// (set) Token: 0x060001DE RID: 478 RVA: 0x0000859C File Offset: 0x0000679C
			public string text
			{
				get
				{
					return this.m_Text;
				}
				set
				{
					this.m_Text = value;
				}
			}

			// Token: 0x17000075 RID: 117
			// (get) Token: 0x060001DF RID: 479 RVA: 0x000085A8 File Offset: 0x000067A8
			// (set) Token: 0x060001E0 RID: 480 RVA: 0x000085B0 File Offset: 0x000067B0
			public Sprite image
			{
				get
				{
					return this.m_Image;
				}
				set
				{
					this.m_Image = value;
				}
			}

			// Token: 0x040000DF RID: 223
			[SerializeField]
			private string m_Text;

			// Token: 0x040000E0 RID: 224
			[SerializeField]
			private Sprite m_Image;
		}

		// Token: 0x02000043 RID: 67
		[Serializable]
		public class OptionDataList
		{
			// Token: 0x060001E1 RID: 481 RVA: 0x000085BC File Offset: 0x000067BC
			public OptionDataList()
			{
				this.options = new List<Dropdown.OptionData>();
			}

			// Token: 0x17000076 RID: 118
			// (get) Token: 0x060001E2 RID: 482 RVA: 0x000085D0 File Offset: 0x000067D0
			// (set) Token: 0x060001E3 RID: 483 RVA: 0x000085D8 File Offset: 0x000067D8
			public List<Dropdown.OptionData> options
			{
				get
				{
					return this.m_Options;
				}
				set
				{
					this.m_Options = value;
				}
			}

			// Token: 0x040000E1 RID: 225
			[SerializeField]
			private List<Dropdown.OptionData> m_Options;
		}

		// Token: 0x02000044 RID: 68
		[Serializable]
		public class DropdownEvent : UnityEvent<int>
		{
		}
	}
}
