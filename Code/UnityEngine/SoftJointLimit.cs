using System;

namespace UnityEngine
{
	// Token: 0x0200011C RID: 284
	public struct SoftJointLimit
	{
		// Token: 0x17000449 RID: 1097
		// (get) Token: 0x060011B8 RID: 4536 RVA: 0x0001445C File Offset: 0x0001265C
		// (set) Token: 0x060011B9 RID: 4537 RVA: 0x00014464 File Offset: 0x00012664
		public float limit
		{
			get
			{
				return this.m_Limit;
			}
			set
			{
				this.m_Limit = value;
			}
		}

		// Token: 0x1700044A RID: 1098
		// (get) Token: 0x060011BA RID: 4538 RVA: 0x00014470 File Offset: 0x00012670
		// (set) Token: 0x060011BB RID: 4539 RVA: 0x00014478 File Offset: 0x00012678
		[Obsolete("Spring has been moved to SoftJointLimitSpring class in Unity 5", true)]
		public float spring
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		// Token: 0x1700044B RID: 1099
		// (get) Token: 0x060011BC RID: 4540 RVA: 0x0001447C File Offset: 0x0001267C
		// (set) Token: 0x060011BD RID: 4541 RVA: 0x00014484 File Offset: 0x00012684
		[Obsolete("Damper has been moved to SoftJointLimitSpring class in Unity 5", true)]
		public float damper
		{
			get
			{
				return 0f;
			}
			set
			{
			}
		}

		// Token: 0x1700044C RID: 1100
		// (get) Token: 0x060011BE RID: 4542 RVA: 0x00014488 File Offset: 0x00012688
		// (set) Token: 0x060011BF RID: 4543 RVA: 0x00014490 File Offset: 0x00012690
		public float bounciness
		{
			get
			{
				return this.m_Bounciness;
			}
			set
			{
				this.m_Bounciness = value;
			}
		}

		// Token: 0x1700044D RID: 1101
		// (get) Token: 0x060011C0 RID: 4544 RVA: 0x0001449C File Offset: 0x0001269C
		// (set) Token: 0x060011C1 RID: 4545 RVA: 0x000144A4 File Offset: 0x000126A4
		public float contactDistance
		{
			get
			{
				return this.m_ContactDistance;
			}
			set
			{
				this.m_ContactDistance = value;
			}
		}

		// Token: 0x1700044E RID: 1102
		// (get) Token: 0x060011C2 RID: 4546 RVA: 0x000144B0 File Offset: 0x000126B0
		// (set) Token: 0x060011C3 RID: 4547 RVA: 0x000144B8 File Offset: 0x000126B8
		[Obsolete("Use SoftJointLimit.bounciness instead", true)]
		public float bouncyness
		{
			get
			{
				return this.m_Bounciness;
			}
			set
			{
				this.m_Bounciness = value;
			}
		}

		// Token: 0x04000353 RID: 851
		private float m_Limit;

		// Token: 0x04000354 RID: 852
		private float m_Bounciness;

		// Token: 0x04000355 RID: 853
		private float m_ContactDistance;
	}
}
