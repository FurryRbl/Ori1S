using System;
using System.Collections;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

namespace UnityEngine.UI
{
	// Token: 0x02000038 RID: 56
	[AddComponentMenu("UI/Button", 30)]
	public class Button : Selectable, IEventSystemHandler, IPointerClickHandler, ISubmitHandler
	{
		// Token: 0x06000164 RID: 356 RVA: 0x00005BB8 File Offset: 0x00003DB8
		protected Button()
		{
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x06000165 RID: 357 RVA: 0x00005BCC File Offset: 0x00003DCC
		// (set) Token: 0x06000166 RID: 358 RVA: 0x00005BD4 File Offset: 0x00003DD4
		public Button.ButtonClickedEvent onClick
		{
			get
			{
				return this.m_OnClick;
			}
			set
			{
				this.m_OnClick = value;
			}
		}

		// Token: 0x06000167 RID: 359 RVA: 0x00005BE0 File Offset: 0x00003DE0
		private void Press()
		{
			if (!this.IsActive() || !this.IsInteractable())
			{
				return;
			}
			this.m_OnClick.Invoke();
		}

		// Token: 0x06000168 RID: 360 RVA: 0x00005C10 File Offset: 0x00003E10
		public virtual void OnPointerClick(PointerEventData eventData)
		{
			if (eventData.button != PointerEventData.InputButton.Left)
			{
				return;
			}
			this.Press();
		}

		// Token: 0x06000169 RID: 361 RVA: 0x00005C24 File Offset: 0x00003E24
		public virtual void OnSubmit(BaseEventData eventData)
		{
			this.Press();
			if (!this.IsActive() || !this.IsInteractable())
			{
				return;
			}
			this.DoStateTransition(Selectable.SelectionState.Pressed, false);
			base.StartCoroutine(this.OnFinishSubmit());
		}

		// Token: 0x0600016A RID: 362 RVA: 0x00005C64 File Offset: 0x00003E64
		private IEnumerator OnFinishSubmit()
		{
			float fadeTime = base.colors.fadeDuration;
			float elapsedTime = 0f;
			while (elapsedTime < fadeTime)
			{
				elapsedTime += Time.unscaledDeltaTime;
				yield return null;
			}
			this.DoStateTransition(base.currentSelectionState, false);
			yield break;
		}

		// Token: 0x040000A9 RID: 169
		[FormerlySerializedAs("onClick")]
		[SerializeField]
		private Button.ButtonClickedEvent m_OnClick = new Button.ButtonClickedEvent();

		// Token: 0x02000039 RID: 57
		[Serializable]
		public class ButtonClickedEvent : UnityEvent
		{
		}
	}
}
