using System;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace UnityEngine.EventSystems
{
	// Token: 0x02000016 RID: 22
	[AddComponentMenu("Event/Event Trigger")]
	public class EventTrigger : MonoBehaviour, IEventSystemHandler, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler, IBeginDragHandler, IInitializePotentialDragHandler, IDragHandler, IEndDragHandler, IDropHandler, IScrollHandler, IUpdateSelectedHandler, ISelectHandler, IDeselectHandler, IMoveHandler, ISubmitHandler, ICancelHandler
	{
		// Token: 0x0600002E RID: 46 RVA: 0x000027CC File Offset: 0x000009CC
		protected EventTrigger()
		{
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600002F RID: 47 RVA: 0x000027D4 File Offset: 0x000009D4
		// (set) Token: 0x06000030 RID: 48 RVA: 0x000027F4 File Offset: 0x000009F4
		public List<EventTrigger.Entry> triggers
		{
			get
			{
				if (this.m_Delegates == null)
				{
					this.m_Delegates = new List<EventTrigger.Entry>();
				}
				return this.m_Delegates;
			}
			set
			{
				this.m_Delegates = value;
			}
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002800 File Offset: 0x00000A00
		private void Execute(EventTriggerType id, BaseEventData eventData)
		{
			int i = 0;
			int count = this.triggers.Count;
			while (i < count)
			{
				EventTrigger.Entry entry = this.triggers[i];
				if (entry.eventID == id && entry.callback != null)
				{
					entry.callback.Invoke(eventData);
				}
				i++;
			}
		}

		// Token: 0x06000032 RID: 50 RVA: 0x0000285C File Offset: 0x00000A5C
		public virtual void OnPointerEnter(PointerEventData eventData)
		{
			this.Execute(EventTriggerType.PointerEnter, eventData);
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002868 File Offset: 0x00000A68
		public virtual void OnPointerExit(PointerEventData eventData)
		{
			this.Execute(EventTriggerType.PointerExit, eventData);
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002874 File Offset: 0x00000A74
		public virtual void OnDrag(PointerEventData eventData)
		{
			this.Execute(EventTriggerType.Drag, eventData);
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002880 File Offset: 0x00000A80
		public virtual void OnDrop(PointerEventData eventData)
		{
			this.Execute(EventTriggerType.Drop, eventData);
		}

		// Token: 0x06000036 RID: 54 RVA: 0x0000288C File Offset: 0x00000A8C
		public virtual void OnPointerDown(PointerEventData eventData)
		{
			this.Execute(EventTriggerType.PointerDown, eventData);
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002898 File Offset: 0x00000A98
		public virtual void OnPointerUp(PointerEventData eventData)
		{
			this.Execute(EventTriggerType.PointerUp, eventData);
		}

		// Token: 0x06000038 RID: 56 RVA: 0x000028A4 File Offset: 0x00000AA4
		public virtual void OnPointerClick(PointerEventData eventData)
		{
			this.Execute(EventTriggerType.PointerClick, eventData);
		}

		// Token: 0x06000039 RID: 57 RVA: 0x000028B0 File Offset: 0x00000AB0
		public virtual void OnSelect(BaseEventData eventData)
		{
			this.Execute(EventTriggerType.Select, eventData);
		}

		// Token: 0x0600003A RID: 58 RVA: 0x000028BC File Offset: 0x00000ABC
		public virtual void OnDeselect(BaseEventData eventData)
		{
			this.Execute(EventTriggerType.Deselect, eventData);
		}

		// Token: 0x0600003B RID: 59 RVA: 0x000028C8 File Offset: 0x00000AC8
		public virtual void OnScroll(PointerEventData eventData)
		{
			this.Execute(EventTriggerType.Scroll, eventData);
		}

		// Token: 0x0600003C RID: 60 RVA: 0x000028D4 File Offset: 0x00000AD4
		public virtual void OnMove(AxisEventData eventData)
		{
			this.Execute(EventTriggerType.Move, eventData);
		}

		// Token: 0x0600003D RID: 61 RVA: 0x000028E0 File Offset: 0x00000AE0
		public virtual void OnUpdateSelected(BaseEventData eventData)
		{
			this.Execute(EventTriggerType.UpdateSelected, eventData);
		}

		// Token: 0x0600003E RID: 62 RVA: 0x000028EC File Offset: 0x00000AEC
		public virtual void OnInitializePotentialDrag(PointerEventData eventData)
		{
			this.Execute(EventTriggerType.InitializePotentialDrag, eventData);
		}

		// Token: 0x0600003F RID: 63 RVA: 0x000028F8 File Offset: 0x00000AF8
		public virtual void OnBeginDrag(PointerEventData eventData)
		{
			this.Execute(EventTriggerType.BeginDrag, eventData);
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002904 File Offset: 0x00000B04
		public virtual void OnEndDrag(PointerEventData eventData)
		{
			this.Execute(EventTriggerType.EndDrag, eventData);
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002910 File Offset: 0x00000B10
		public virtual void OnSubmit(BaseEventData eventData)
		{
			this.Execute(EventTriggerType.Submit, eventData);
		}

		// Token: 0x06000042 RID: 66 RVA: 0x0000291C File Offset: 0x00000B1C
		public virtual void OnCancel(BaseEventData eventData)
		{
			this.Execute(EventTriggerType.Cancel, eventData);
		}

		// Token: 0x0400000E RID: 14
		[SerializeField]
		[FormerlySerializedAs("delegates")]
		private List<EventTrigger.Entry> m_Delegates;

		// Token: 0x0400000F RID: 15
		[Obsolete("Please use triggers instead (UnityUpgradable) -> triggers", true)]
		public List<EventTrigger.Entry> delegates;

		// Token: 0x02000017 RID: 23
		[Serializable]
		public class TriggerEvent : UnityEvent<BaseEventData>
		{
		}

		// Token: 0x02000018 RID: 24
		[Serializable]
		public class Entry
		{
			// Token: 0x04000010 RID: 16
			public EventTriggerType eventID = EventTriggerType.PointerClick;

			// Token: 0x04000011 RID: 17
			public EventTrigger.TriggerEvent callback = new EventTrigger.TriggerEvent();
		}
	}
}
