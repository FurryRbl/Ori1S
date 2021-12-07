using System;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

namespace UnityEngine.UI
{
	// Token: 0x0200007B RID: 123
	[AddComponentMenu("UI/Toggle", 31)]
	[RequireComponent(typeof(RectTransform))]
	public class Toggle : Selectable, IEventSystemHandler, IPointerClickHandler, ISubmitHandler, ICanvasElement
	{
		// Token: 0x06000489 RID: 1161 RVA: 0x000156A0 File Offset: 0x000138A0
		protected Toggle()
		{
		}

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x0600048A RID: 1162 RVA: 0x000156BC File Offset: 0x000138BC
		// (set) Token: 0x0600048B RID: 1163 RVA: 0x000156C4 File Offset: 0x000138C4
		public ToggleGroup group
		{
			get
			{
				return this.m_Group;
			}
			set
			{
				this.m_Group = value;
				this.SetToggleGroup(this.m_Group, true);
				this.PlayEffect(true);
			}
		}

		// Token: 0x0600048C RID: 1164 RVA: 0x000156E4 File Offset: 0x000138E4
		public virtual void Rebuild(CanvasUpdate executing)
		{
		}

		// Token: 0x0600048D RID: 1165 RVA: 0x000156E8 File Offset: 0x000138E8
		public virtual void LayoutComplete()
		{
		}

		// Token: 0x0600048E RID: 1166 RVA: 0x000156EC File Offset: 0x000138EC
		public virtual void GraphicUpdateComplete()
		{
		}

		// Token: 0x0600048F RID: 1167 RVA: 0x000156F0 File Offset: 0x000138F0
		protected override void OnEnable()
		{
			base.OnEnable();
			this.SetToggleGroup(this.m_Group, false);
			this.PlayEffect(true);
		}

		// Token: 0x06000490 RID: 1168 RVA: 0x0001570C File Offset: 0x0001390C
		protected override void OnDisable()
		{
			this.SetToggleGroup(null, false);
			base.OnDisable();
		}

		// Token: 0x06000491 RID: 1169 RVA: 0x0001571C File Offset: 0x0001391C
		protected override void OnDidApplyAnimationProperties()
		{
			if (this.graphic != null)
			{
				bool flag = !Mathf.Approximately(this.graphic.canvasRenderer.GetColor().a, 0f);
				if (this.m_IsOn != flag)
				{
					this.m_IsOn = flag;
					this.Set(!flag);
				}
			}
			base.OnDidApplyAnimationProperties();
		}

		// Token: 0x06000492 RID: 1170 RVA: 0x00015784 File Offset: 0x00013984
		private void SetToggleGroup(ToggleGroup newGroup, bool setMemberValue)
		{
			ToggleGroup group = this.m_Group;
			if (this.m_Group != null)
			{
				this.m_Group.UnregisterToggle(this);
			}
			if (setMemberValue)
			{
				this.m_Group = newGroup;
			}
			if (this.m_Group != null && this.IsActive())
			{
				this.m_Group.RegisterToggle(this);
			}
			if (newGroup != null && newGroup != group && this.isOn && this.IsActive())
			{
				this.m_Group.NotifyToggleOn(this);
			}
		}

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x06000493 RID: 1171 RVA: 0x00015824 File Offset: 0x00013A24
		// (set) Token: 0x06000494 RID: 1172 RVA: 0x0001582C File Offset: 0x00013A2C
		public bool isOn
		{
			get
			{
				return this.m_IsOn;
			}
			set
			{
				this.Set(value);
			}
		}

		// Token: 0x06000495 RID: 1173 RVA: 0x00015838 File Offset: 0x00013A38
		private void Set(bool value)
		{
			this.Set(value, true);
		}

		// Token: 0x06000496 RID: 1174 RVA: 0x00015844 File Offset: 0x00013A44
		private void Set(bool value, bool sendCallback)
		{
			if (this.m_IsOn == value)
			{
				return;
			}
			this.m_IsOn = value;
			if (this.m_Group != null && this.IsActive() && (this.m_IsOn || (!this.m_Group.AnyTogglesOn() && !this.m_Group.allowSwitchOff)))
			{
				this.m_IsOn = true;
				this.m_Group.NotifyToggleOn(this);
			}
			this.PlayEffect(this.toggleTransition == Toggle.ToggleTransition.None);
			if (sendCallback)
			{
				this.onValueChanged.Invoke(this.m_IsOn);
			}
		}

		// Token: 0x06000497 RID: 1175 RVA: 0x000158E8 File Offset: 0x00013AE8
		private void PlayEffect(bool instant)
		{
			if (this.graphic == null)
			{
				return;
			}
			this.graphic.CrossFadeAlpha((!this.m_IsOn) ? 0f : 1f, (!instant) ? 0.1f : 0f, true);
		}

		// Token: 0x06000498 RID: 1176 RVA: 0x00015944 File Offset: 0x00013B44
		protected override void Start()
		{
			this.PlayEffect(true);
		}

		// Token: 0x06000499 RID: 1177 RVA: 0x00015950 File Offset: 0x00013B50
		private void InternalToggle()
		{
			if (!this.IsActive() || !this.IsInteractable())
			{
				return;
			}
			this.isOn = !this.isOn;
		}

		// Token: 0x0600049A RID: 1178 RVA: 0x00015984 File Offset: 0x00013B84
		public virtual void OnPointerClick(PointerEventData eventData)
		{
			if (eventData.button != PointerEventData.InputButton.Left)
			{
				return;
			}
			this.InternalToggle();
		}

		// Token: 0x0600049B RID: 1179 RVA: 0x00015998 File Offset: 0x00013B98
		public virtual void OnSubmit(BaseEventData eventData)
		{
			this.InternalToggle();
		}

		// Token: 0x0600049C RID: 1180 RVA: 0x000159A0 File Offset: 0x00013BA0
		virtual bool IsDestroyed()
		{
			return base.IsDestroyed();
		}

		// Token: 0x0600049D RID: 1181 RVA: 0x000159A8 File Offset: 0x00013BA8
		virtual Transform get_transform()
		{
			return base.transform;
		}

		// Token: 0x04000230 RID: 560
		public Toggle.ToggleTransition toggleTransition = Toggle.ToggleTransition.Fade;

		// Token: 0x04000231 RID: 561
		public Graphic graphic;

		// Token: 0x04000232 RID: 562
		[SerializeField]
		private ToggleGroup m_Group;

		// Token: 0x04000233 RID: 563
		public Toggle.ToggleEvent onValueChanged = new Toggle.ToggleEvent();

		// Token: 0x04000234 RID: 564
		[Tooltip("Is the toggle currently on or off?")]
		[SerializeField]
		[FormerlySerializedAs("m_IsActive")]
		private bool m_IsOn;

		// Token: 0x0200007C RID: 124
		public enum ToggleTransition
		{
			// Token: 0x04000236 RID: 566
			None,
			// Token: 0x04000237 RID: 567
			Fade
		}

		// Token: 0x0200007D RID: 125
		[Serializable]
		public class ToggleEvent : UnityEvent<bool>
		{
		}
	}
}
