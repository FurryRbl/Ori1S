using System;

namespace UnityEngine.EventSystems
{
	// Token: 0x02000021 RID: 33
	public class BaseEventData : AbstractEventData
	{
		// Token: 0x06000091 RID: 145 RVA: 0x00003294 File Offset: 0x00001494
		public BaseEventData(EventSystem eventSystem)
		{
			this.m_EventSystem = eventSystem;
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000092 RID: 146 RVA: 0x000032A4 File Offset: 0x000014A4
		public BaseInputModule currentInputModule
		{
			get
			{
				return this.m_EventSystem.currentInputModule;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000093 RID: 147 RVA: 0x000032B4 File Offset: 0x000014B4
		// (set) Token: 0x06000094 RID: 148 RVA: 0x000032C4 File Offset: 0x000014C4
		public GameObject selectedObject
		{
			get
			{
				return this.m_EventSystem.currentSelectedGameObject;
			}
			set
			{
				this.m_EventSystem.SetSelectedGameObject(value, this);
			}
		}

		// Token: 0x0400004C RID: 76
		private readonly EventSystem m_EventSystem;
	}
}
