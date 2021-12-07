using System;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x020001AF RID: 431
	[RequiredByNativeCode]
	public struct AnimatorTransitionInfo
	{
		// Token: 0x060019F1 RID: 6641 RVA: 0x000190F4 File Offset: 0x000172F4
		public bool IsName(string name)
		{
			return Animator.StringToHash(name) == this.m_Name || Animator.StringToHash(name) == this.m_FullPath;
		}

		// Token: 0x060019F2 RID: 6642 RVA: 0x00019124 File Offset: 0x00017324
		public bool IsUserName(string name)
		{
			return Animator.StringToHash(name) == this.m_UserName;
		}

		// Token: 0x170006E6 RID: 1766
		// (get) Token: 0x060019F3 RID: 6643 RVA: 0x00019134 File Offset: 0x00017334
		public int fullPathHash
		{
			get
			{
				return this.m_FullPath;
			}
		}

		// Token: 0x170006E7 RID: 1767
		// (get) Token: 0x060019F4 RID: 6644 RVA: 0x0001913C File Offset: 0x0001733C
		public int nameHash
		{
			get
			{
				return this.m_Name;
			}
		}

		// Token: 0x170006E8 RID: 1768
		// (get) Token: 0x060019F5 RID: 6645 RVA: 0x00019144 File Offset: 0x00017344
		public int userNameHash
		{
			get
			{
				return this.m_UserName;
			}
		}

		// Token: 0x170006E9 RID: 1769
		// (get) Token: 0x060019F6 RID: 6646 RVA: 0x0001914C File Offset: 0x0001734C
		public float normalizedTime
		{
			get
			{
				return this.m_NormalizedTime;
			}
		}

		// Token: 0x170006EA RID: 1770
		// (get) Token: 0x060019F7 RID: 6647 RVA: 0x00019154 File Offset: 0x00017354
		public bool anyState
		{
			get
			{
				return this.m_AnyState;
			}
		}

		// Token: 0x170006EB RID: 1771
		// (get) Token: 0x060019F8 RID: 6648 RVA: 0x0001915C File Offset: 0x0001735C
		internal bool entry
		{
			get
			{
				return (this.m_TransitionType & 2) != 0;
			}
		}

		// Token: 0x170006EC RID: 1772
		// (get) Token: 0x060019F9 RID: 6649 RVA: 0x0001916C File Offset: 0x0001736C
		internal bool exit
		{
			get
			{
				return (this.m_TransitionType & 4) != 0;
			}
		}

		// Token: 0x040004DB RID: 1243
		private int m_FullPath;

		// Token: 0x040004DC RID: 1244
		private int m_UserName;

		// Token: 0x040004DD RID: 1245
		private int m_Name;

		// Token: 0x040004DE RID: 1246
		private float m_NormalizedTime;

		// Token: 0x040004DF RID: 1247
		private bool m_AnyState;

		// Token: 0x040004E0 RID: 1248
		private int m_TransitionType;
	}
}
