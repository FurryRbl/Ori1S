using System;
using UnityEngine.Serialization;

namespace UnityEngine.UI
{
	// Token: 0x02000063 RID: 99
	[Serializable]
	public struct Navigation
	{
		// Token: 0x170000D5 RID: 213
		// (get) Token: 0x0600033B RID: 827 RVA: 0x000101F8 File Offset: 0x0000E3F8
		// (set) Token: 0x0600033C RID: 828 RVA: 0x00010200 File Offset: 0x0000E400
		public Navigation.Mode mode
		{
			get
			{
				return this.m_Mode;
			}
			set
			{
				this.m_Mode = value;
			}
		}

		// Token: 0x170000D6 RID: 214
		// (get) Token: 0x0600033D RID: 829 RVA: 0x0001020C File Offset: 0x0000E40C
		// (set) Token: 0x0600033E RID: 830 RVA: 0x00010214 File Offset: 0x0000E414
		public Selectable selectOnUp
		{
			get
			{
				return this.m_SelectOnUp;
			}
			set
			{
				this.m_SelectOnUp = value;
			}
		}

		// Token: 0x170000D7 RID: 215
		// (get) Token: 0x0600033F RID: 831 RVA: 0x00010220 File Offset: 0x0000E420
		// (set) Token: 0x06000340 RID: 832 RVA: 0x00010228 File Offset: 0x0000E428
		public Selectable selectOnDown
		{
			get
			{
				return this.m_SelectOnDown;
			}
			set
			{
				this.m_SelectOnDown = value;
			}
		}

		// Token: 0x170000D8 RID: 216
		// (get) Token: 0x06000341 RID: 833 RVA: 0x00010234 File Offset: 0x0000E434
		// (set) Token: 0x06000342 RID: 834 RVA: 0x0001023C File Offset: 0x0000E43C
		public Selectable selectOnLeft
		{
			get
			{
				return this.m_SelectOnLeft;
			}
			set
			{
				this.m_SelectOnLeft = value;
			}
		}

		// Token: 0x170000D9 RID: 217
		// (get) Token: 0x06000343 RID: 835 RVA: 0x00010248 File Offset: 0x0000E448
		// (set) Token: 0x06000344 RID: 836 RVA: 0x00010250 File Offset: 0x0000E450
		public Selectable selectOnRight
		{
			get
			{
				return this.m_SelectOnRight;
			}
			set
			{
				this.m_SelectOnRight = value;
			}
		}

		// Token: 0x170000DA RID: 218
		// (get) Token: 0x06000345 RID: 837 RVA: 0x0001025C File Offset: 0x0000E45C
		public static Navigation defaultNavigation
		{
			get
			{
				return new Navigation
				{
					m_Mode = Navigation.Mode.Automatic
				};
			}
		}

		// Token: 0x04000199 RID: 409
		[FormerlySerializedAs("mode")]
		[SerializeField]
		private Navigation.Mode m_Mode;

		// Token: 0x0400019A RID: 410
		[SerializeField]
		[FormerlySerializedAs("selectOnUp")]
		private Selectable m_SelectOnUp;

		// Token: 0x0400019B RID: 411
		[SerializeField]
		[FormerlySerializedAs("selectOnDown")]
		private Selectable m_SelectOnDown;

		// Token: 0x0400019C RID: 412
		[FormerlySerializedAs("selectOnLeft")]
		[SerializeField]
		private Selectable m_SelectOnLeft;

		// Token: 0x0400019D RID: 413
		[SerializeField]
		[FormerlySerializedAs("selectOnRight")]
		private Selectable m_SelectOnRight;

		// Token: 0x02000064 RID: 100
		[Flags]
		public enum Mode
		{
			// Token: 0x0400019F RID: 415
			None = 0,
			// Token: 0x040001A0 RID: 416
			Horizontal = 1,
			// Token: 0x040001A1 RID: 417
			Vertical = 2,
			// Token: 0x040001A2 RID: 418
			Automatic = 3,
			// Token: 0x040001A3 RID: 419
			Explicit = 4
		}
	}
}
