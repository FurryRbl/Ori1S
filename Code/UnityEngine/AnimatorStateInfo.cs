using System;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x020001AE RID: 430
	[RequiredByNativeCode]
	public struct AnimatorStateInfo
	{
		// Token: 0x060019E6 RID: 6630 RVA: 0x0001905C File Offset: 0x0001725C
		public bool IsName(string name)
		{
			int num = Animator.StringToHash(name);
			return num == this.m_FullPath || num == this.m_Name || num == this.m_Path;
		}

		// Token: 0x170006DD RID: 1757
		// (get) Token: 0x060019E7 RID: 6631 RVA: 0x00019094 File Offset: 0x00017294
		public int fullPathHash
		{
			get
			{
				return this.m_FullPath;
			}
		}

		// Token: 0x170006DE RID: 1758
		// (get) Token: 0x060019E8 RID: 6632 RVA: 0x0001909C File Offset: 0x0001729C
		[Obsolete("Use AnimatorStateInfo.fullPathHash instead.")]
		public int nameHash
		{
			get
			{
				return this.m_Path;
			}
		}

		// Token: 0x170006DF RID: 1759
		// (get) Token: 0x060019E9 RID: 6633 RVA: 0x000190A4 File Offset: 0x000172A4
		public int shortNameHash
		{
			get
			{
				return this.m_Name;
			}
		}

		// Token: 0x170006E0 RID: 1760
		// (get) Token: 0x060019EA RID: 6634 RVA: 0x000190AC File Offset: 0x000172AC
		public float normalizedTime
		{
			get
			{
				return this.m_NormalizedTime;
			}
		}

		// Token: 0x170006E1 RID: 1761
		// (get) Token: 0x060019EB RID: 6635 RVA: 0x000190B4 File Offset: 0x000172B4
		public float length
		{
			get
			{
				return this.m_Length;
			}
		}

		// Token: 0x170006E2 RID: 1762
		// (get) Token: 0x060019EC RID: 6636 RVA: 0x000190BC File Offset: 0x000172BC
		public float speed
		{
			get
			{
				return this.m_Speed;
			}
		}

		// Token: 0x170006E3 RID: 1763
		// (get) Token: 0x060019ED RID: 6637 RVA: 0x000190C4 File Offset: 0x000172C4
		public float speedMultiplier
		{
			get
			{
				return this.m_SpeedMultiplier;
			}
		}

		// Token: 0x170006E4 RID: 1764
		// (get) Token: 0x060019EE RID: 6638 RVA: 0x000190CC File Offset: 0x000172CC
		public int tagHash
		{
			get
			{
				return this.m_Tag;
			}
		}

		// Token: 0x060019EF RID: 6639 RVA: 0x000190D4 File Offset: 0x000172D4
		public bool IsTag(string tag)
		{
			return Animator.StringToHash(tag) == this.m_Tag;
		}

		// Token: 0x170006E5 RID: 1765
		// (get) Token: 0x060019F0 RID: 6640 RVA: 0x000190E4 File Offset: 0x000172E4
		public bool loop
		{
			get
			{
				return this.m_Loop != 0;
			}
		}

		// Token: 0x040004D2 RID: 1234
		private int m_Name;

		// Token: 0x040004D3 RID: 1235
		private int m_Path;

		// Token: 0x040004D4 RID: 1236
		private int m_FullPath;

		// Token: 0x040004D5 RID: 1237
		private float m_NormalizedTime;

		// Token: 0x040004D6 RID: 1238
		private float m_Length;

		// Token: 0x040004D7 RID: 1239
		private float m_Speed;

		// Token: 0x040004D8 RID: 1240
		private float m_SpeedMultiplier;

		// Token: 0x040004D9 RID: 1241
		private int m_Tag;

		// Token: 0x040004DA RID: 1242
		private int m_Loop;
	}
}
