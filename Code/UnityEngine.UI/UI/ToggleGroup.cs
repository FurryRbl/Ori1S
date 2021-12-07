using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.EventSystems;

namespace UnityEngine.UI
{
	// Token: 0x0200007E RID: 126
	[DisallowMultipleComponent]
	[AddComponentMenu("UI/Toggle Group", 32)]
	public class ToggleGroup : UIBehaviour
	{
		// Token: 0x0600049F RID: 1183 RVA: 0x000159B8 File Offset: 0x00013BB8
		protected ToggleGroup()
		{
		}

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x060004A0 RID: 1184 RVA: 0x000159CC File Offset: 0x00013BCC
		// (set) Token: 0x060004A1 RID: 1185 RVA: 0x000159D4 File Offset: 0x00013BD4
		public bool allowSwitchOff
		{
			get
			{
				return this.m_AllowSwitchOff;
			}
			set
			{
				this.m_AllowSwitchOff = value;
			}
		}

		// Token: 0x060004A2 RID: 1186 RVA: 0x000159E0 File Offset: 0x00013BE0
		private void ValidateToggleIsInGroup(Toggle toggle)
		{
			if (toggle == null || !this.m_Toggles.Contains(toggle))
			{
				throw new ArgumentException(string.Format("Toggle {0} is not part of ToggleGroup {1}", new object[]
				{
					toggle,
					this
				}));
			}
		}

		// Token: 0x060004A3 RID: 1187 RVA: 0x00015A20 File Offset: 0x00013C20
		public void NotifyToggleOn(Toggle toggle)
		{
			this.ValidateToggleIsInGroup(toggle);
			for (int i = 0; i < this.m_Toggles.Count; i++)
			{
				if (!(this.m_Toggles[i] == toggle))
				{
					this.m_Toggles[i].isOn = false;
				}
			}
		}

		// Token: 0x060004A4 RID: 1188 RVA: 0x00015A80 File Offset: 0x00013C80
		public void UnregisterToggle(Toggle toggle)
		{
			if (this.m_Toggles.Contains(toggle))
			{
				this.m_Toggles.Remove(toggle);
			}
		}

		// Token: 0x060004A5 RID: 1189 RVA: 0x00015AA0 File Offset: 0x00013CA0
		public void RegisterToggle(Toggle toggle)
		{
			if (!this.m_Toggles.Contains(toggle))
			{
				this.m_Toggles.Add(toggle);
			}
		}

		// Token: 0x060004A6 RID: 1190 RVA: 0x00015AC0 File Offset: 0x00013CC0
		public bool AnyTogglesOn()
		{
			return this.m_Toggles.Find((Toggle x) => x.isOn) != null;
		}

		// Token: 0x060004A7 RID: 1191 RVA: 0x00015AFC File Offset: 0x00013CFC
		public IEnumerable<Toggle> ActiveToggles()
		{
			return from x in this.m_Toggles
			where x.isOn
			select x;
		}

		// Token: 0x060004A8 RID: 1192 RVA: 0x00015B34 File Offset: 0x00013D34
		public void SetAllTogglesOff()
		{
			bool allowSwitchOff = this.m_AllowSwitchOff;
			this.m_AllowSwitchOff = true;
			for (int i = 0; i < this.m_Toggles.Count; i++)
			{
				this.m_Toggles[i].isOn = false;
			}
			this.m_AllowSwitchOff = allowSwitchOff;
		}

		// Token: 0x04000238 RID: 568
		[SerializeField]
		private bool m_AllowSwitchOff;

		// Token: 0x04000239 RID: 569
		private List<Toggle> m_Toggles = new List<Toggle>();
	}
}
