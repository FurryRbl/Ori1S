using System;

namespace UnityEngine
{
	// Token: 0x020001B2 RID: 434
	public sealed class AnimatorControllerParameter
	{
		// Token: 0x1700071C RID: 1820
		// (get) Token: 0x06001AEF RID: 6895 RVA: 0x00019A8C File Offset: 0x00017C8C
		public string name
		{
			get
			{
				return this.m_Name;
			}
		}

		// Token: 0x1700071D RID: 1821
		// (get) Token: 0x06001AF0 RID: 6896 RVA: 0x00019A94 File Offset: 0x00017C94
		public int nameHash
		{
			get
			{
				return Animator.StringToHash(this.m_Name);
			}
		}

		// Token: 0x1700071E RID: 1822
		// (get) Token: 0x06001AF1 RID: 6897 RVA: 0x00019AA4 File Offset: 0x00017CA4
		// (set) Token: 0x06001AF2 RID: 6898 RVA: 0x00019AAC File Offset: 0x00017CAC
		public AnimatorControllerParameterType type
		{
			get
			{
				return this.m_Type;
			}
			set
			{
				this.m_Type = value;
			}
		}

		// Token: 0x1700071F RID: 1823
		// (get) Token: 0x06001AF3 RID: 6899 RVA: 0x00019AB8 File Offset: 0x00017CB8
		// (set) Token: 0x06001AF4 RID: 6900 RVA: 0x00019AC0 File Offset: 0x00017CC0
		public float defaultFloat
		{
			get
			{
				return this.m_DefaultFloat;
			}
			set
			{
				this.m_DefaultFloat = value;
			}
		}

		// Token: 0x17000720 RID: 1824
		// (get) Token: 0x06001AF5 RID: 6901 RVA: 0x00019ACC File Offset: 0x00017CCC
		// (set) Token: 0x06001AF6 RID: 6902 RVA: 0x00019AD4 File Offset: 0x00017CD4
		public int defaultInt
		{
			get
			{
				return this.m_DefaultInt;
			}
			set
			{
				this.m_DefaultInt = value;
			}
		}

		// Token: 0x17000721 RID: 1825
		// (get) Token: 0x06001AF7 RID: 6903 RVA: 0x00019AE0 File Offset: 0x00017CE0
		// (set) Token: 0x06001AF8 RID: 6904 RVA: 0x00019AE8 File Offset: 0x00017CE8
		public bool defaultBool
		{
			get
			{
				return this.m_DefaultBool;
			}
			set
			{
				this.m_DefaultBool = value;
			}
		}

		// Token: 0x06001AF9 RID: 6905 RVA: 0x00019AF4 File Offset: 0x00017CF4
		public override bool Equals(object o)
		{
			AnimatorControllerParameter animatorControllerParameter = o as AnimatorControllerParameter;
			return animatorControllerParameter != null && this.m_Name == animatorControllerParameter.m_Name && this.m_Type == animatorControllerParameter.m_Type && this.m_DefaultFloat == animatorControllerParameter.m_DefaultFloat && this.m_DefaultInt == animatorControllerParameter.m_DefaultInt && this.m_DefaultBool == animatorControllerParameter.m_DefaultBool;
		}

		// Token: 0x06001AFA RID: 6906 RVA: 0x00019B68 File Offset: 0x00017D68
		public override int GetHashCode()
		{
			return this.name.GetHashCode();
		}

		// Token: 0x040004E3 RID: 1251
		internal string m_Name = string.Empty;

		// Token: 0x040004E4 RID: 1252
		internal AnimatorControllerParameterType m_Type;

		// Token: 0x040004E5 RID: 1253
		internal float m_DefaultFloat;

		// Token: 0x040004E6 RID: 1254
		internal int m_DefaultInt;

		// Token: 0x040004E7 RID: 1255
		internal bool m_DefaultBool;
	}
}
