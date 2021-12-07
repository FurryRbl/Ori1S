using System;
using UnityEngine;

namespace SmartInput
{
	// Token: 0x02000196 RID: 406
	public class KeyCodeButtonInput : IButtonInput
	{
		// Token: 0x06000FBF RID: 4031 RVA: 0x000488B9 File Offset: 0x00046AB9
		public KeyCodeButtonInput(KeyCode keyCode)
		{
			this.m_keyCode = keyCode;
		}

		// Token: 0x06000FC0 RID: 4032 RVA: 0x000488C8 File Offset: 0x00046AC8
		public bool GetButton()
		{
			return MoonInput.GetKey(this.m_keyCode);
		}

		// Token: 0x170002CF RID: 719
		// (get) Token: 0x06000FC1 RID: 4033 RVA: 0x000488D5 File Offset: 0x00046AD5
		public KeyCode KeyCode
		{
			get
			{
				return this.m_keyCode;
			}
		}

		// Token: 0x04000CEA RID: 3306
		private readonly KeyCode m_keyCode;
	}
}
