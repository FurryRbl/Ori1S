using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x02000208 RID: 520
	[Serializable]
	public sealed class GUISettings
	{
		// Token: 0x17000819 RID: 2073
		// (get) Token: 0x06001FDF RID: 8159 RVA: 0x00024D40 File Offset: 0x00022F40
		// (set) Token: 0x06001FE0 RID: 8160 RVA: 0x00024D48 File Offset: 0x00022F48
		public bool doubleClickSelectsWord
		{
			get
			{
				return this.m_DoubleClickSelectsWord;
			}
			set
			{
				this.m_DoubleClickSelectsWord = value;
			}
		}

		// Token: 0x1700081A RID: 2074
		// (get) Token: 0x06001FE1 RID: 8161 RVA: 0x00024D54 File Offset: 0x00022F54
		// (set) Token: 0x06001FE2 RID: 8162 RVA: 0x00024D5C File Offset: 0x00022F5C
		public bool tripleClickSelectsLine
		{
			get
			{
				return this.m_TripleClickSelectsLine;
			}
			set
			{
				this.m_TripleClickSelectsLine = value;
			}
		}

		// Token: 0x1700081B RID: 2075
		// (get) Token: 0x06001FE3 RID: 8163 RVA: 0x00024D68 File Offset: 0x00022F68
		// (set) Token: 0x06001FE4 RID: 8164 RVA: 0x00024D70 File Offset: 0x00022F70
		public Color cursorColor
		{
			get
			{
				return this.m_CursorColor;
			}
			set
			{
				this.m_CursorColor = value;
			}
		}

		// Token: 0x1700081C RID: 2076
		// (get) Token: 0x06001FE5 RID: 8165 RVA: 0x00024D7C File Offset: 0x00022F7C
		// (set) Token: 0x06001FE6 RID: 8166 RVA: 0x00024D9C File Offset: 0x00022F9C
		public float cursorFlashSpeed
		{
			get
			{
				if (this.m_CursorFlashSpeed >= 0f)
				{
					return this.m_CursorFlashSpeed;
				}
				return GUISettings.Internal_GetCursorFlashSpeed();
			}
			set
			{
				this.m_CursorFlashSpeed = value;
			}
		}

		// Token: 0x1700081D RID: 2077
		// (get) Token: 0x06001FE7 RID: 8167 RVA: 0x00024DA8 File Offset: 0x00022FA8
		// (set) Token: 0x06001FE8 RID: 8168 RVA: 0x00024DB0 File Offset: 0x00022FB0
		public Color selectionColor
		{
			get
			{
				return this.m_SelectionColor;
			}
			set
			{
				this.m_SelectionColor = value;
			}
		}

		// Token: 0x06001FE9 RID: 8169
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern float Internal_GetCursorFlashSpeed();

		// Token: 0x040007F0 RID: 2032
		[SerializeField]
		private bool m_DoubleClickSelectsWord = true;

		// Token: 0x040007F1 RID: 2033
		[SerializeField]
		private bool m_TripleClickSelectsLine = true;

		// Token: 0x040007F2 RID: 2034
		[SerializeField]
		private Color m_CursorColor = Color.white;

		// Token: 0x040007F3 RID: 2035
		[SerializeField]
		private float m_CursorFlashSpeed = -1f;

		// Token: 0x040007F4 RID: 2036
		[SerializeField]
		private Color m_SelectionColor = new Color(0.5f, 0.5f, 1f);
	}
}
