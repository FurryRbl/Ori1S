using System;

namespace UnityEngine
{
	// Token: 0x020001B7 RID: 439
	public struct HumanDescription
	{
		// Token: 0x17000729 RID: 1833
		// (get) Token: 0x06001B0C RID: 6924 RVA: 0x00019C20 File Offset: 0x00017E20
		// (set) Token: 0x06001B0D RID: 6925 RVA: 0x00019C28 File Offset: 0x00017E28
		public float upperArmTwist
		{
			get
			{
				return this.m_ArmTwist;
			}
			set
			{
				this.m_ArmTwist = value;
			}
		}

		// Token: 0x1700072A RID: 1834
		// (get) Token: 0x06001B0E RID: 6926 RVA: 0x00019C34 File Offset: 0x00017E34
		// (set) Token: 0x06001B0F RID: 6927 RVA: 0x00019C3C File Offset: 0x00017E3C
		public float lowerArmTwist
		{
			get
			{
				return this.m_ForeArmTwist;
			}
			set
			{
				this.m_ForeArmTwist = value;
			}
		}

		// Token: 0x1700072B RID: 1835
		// (get) Token: 0x06001B10 RID: 6928 RVA: 0x00019C48 File Offset: 0x00017E48
		// (set) Token: 0x06001B11 RID: 6929 RVA: 0x00019C50 File Offset: 0x00017E50
		public float upperLegTwist
		{
			get
			{
				return this.m_UpperLegTwist;
			}
			set
			{
				this.m_UpperLegTwist = value;
			}
		}

		// Token: 0x1700072C RID: 1836
		// (get) Token: 0x06001B12 RID: 6930 RVA: 0x00019C5C File Offset: 0x00017E5C
		// (set) Token: 0x06001B13 RID: 6931 RVA: 0x00019C64 File Offset: 0x00017E64
		public float lowerLegTwist
		{
			get
			{
				return this.m_LegTwist;
			}
			set
			{
				this.m_LegTwist = value;
			}
		}

		// Token: 0x1700072D RID: 1837
		// (get) Token: 0x06001B14 RID: 6932 RVA: 0x00019C70 File Offset: 0x00017E70
		// (set) Token: 0x06001B15 RID: 6933 RVA: 0x00019C78 File Offset: 0x00017E78
		public float armStretch
		{
			get
			{
				return this.m_ArmStretch;
			}
			set
			{
				this.m_ArmStretch = value;
			}
		}

		// Token: 0x1700072E RID: 1838
		// (get) Token: 0x06001B16 RID: 6934 RVA: 0x00019C84 File Offset: 0x00017E84
		// (set) Token: 0x06001B17 RID: 6935 RVA: 0x00019C8C File Offset: 0x00017E8C
		public float legStretch
		{
			get
			{
				return this.m_LegStretch;
			}
			set
			{
				this.m_LegStretch = value;
			}
		}

		// Token: 0x1700072F RID: 1839
		// (get) Token: 0x06001B18 RID: 6936 RVA: 0x00019C98 File Offset: 0x00017E98
		// (set) Token: 0x06001B19 RID: 6937 RVA: 0x00019CA0 File Offset: 0x00017EA0
		public float feetSpacing
		{
			get
			{
				return this.m_FeetSpacing;
			}
			set
			{
				this.m_FeetSpacing = value;
			}
		}

		// Token: 0x17000730 RID: 1840
		// (get) Token: 0x06001B1A RID: 6938 RVA: 0x00019CAC File Offset: 0x00017EAC
		// (set) Token: 0x06001B1B RID: 6939 RVA: 0x00019CB4 File Offset: 0x00017EB4
		public bool hasTranslationDoF
		{
			get
			{
				return this.m_HasTranslationDoF;
			}
			set
			{
				this.m_HasTranslationDoF = value;
			}
		}

		// Token: 0x040004F5 RID: 1269
		public HumanBone[] human;

		// Token: 0x040004F6 RID: 1270
		public SkeletonBone[] skeleton;

		// Token: 0x040004F7 RID: 1271
		internal float m_ArmTwist;

		// Token: 0x040004F8 RID: 1272
		internal float m_ForeArmTwist;

		// Token: 0x040004F9 RID: 1273
		internal float m_UpperLegTwist;

		// Token: 0x040004FA RID: 1274
		internal float m_LegTwist;

		// Token: 0x040004FB RID: 1275
		internal float m_ArmStretch;

		// Token: 0x040004FC RID: 1276
		internal float m_LegStretch;

		// Token: 0x040004FD RID: 1277
		internal float m_FeetSpacing;

		// Token: 0x040004FE RID: 1278
		private bool m_HasTranslationDoF;
	}
}
