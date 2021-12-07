using System;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000069 RID: 105
	[RequiredByNativeCode]
	public struct Keyframe
	{
		// Token: 0x06000693 RID: 1683 RVA: 0x0000A160 File Offset: 0x00008360
		public Keyframe(float time, float value)
		{
			this.m_Time = time;
			this.m_Value = value;
			this.m_InTangent = 0f;
			this.m_OutTangent = 0f;
		}

		// Token: 0x06000694 RID: 1684 RVA: 0x0000A194 File Offset: 0x00008394
		public Keyframe(float time, float value, float inTangent, float outTangent)
		{
			this.m_Time = time;
			this.m_Value = value;
			this.m_InTangent = inTangent;
			this.m_OutTangent = outTangent;
		}

		// Token: 0x1700018D RID: 397
		// (get) Token: 0x06000695 RID: 1685 RVA: 0x0000A1B4 File Offset: 0x000083B4
		// (set) Token: 0x06000696 RID: 1686 RVA: 0x0000A1BC File Offset: 0x000083BC
		public float time
		{
			get
			{
				return this.m_Time;
			}
			set
			{
				this.m_Time = value;
			}
		}

		// Token: 0x1700018E RID: 398
		// (get) Token: 0x06000697 RID: 1687 RVA: 0x0000A1C8 File Offset: 0x000083C8
		// (set) Token: 0x06000698 RID: 1688 RVA: 0x0000A1D0 File Offset: 0x000083D0
		public float value
		{
			get
			{
				return this.m_Value;
			}
			set
			{
				this.m_Value = value;
			}
		}

		// Token: 0x1700018F RID: 399
		// (get) Token: 0x06000699 RID: 1689 RVA: 0x0000A1DC File Offset: 0x000083DC
		// (set) Token: 0x0600069A RID: 1690 RVA: 0x0000A1E4 File Offset: 0x000083E4
		public float inTangent
		{
			get
			{
				return this.m_InTangent;
			}
			set
			{
				this.m_InTangent = value;
			}
		}

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x0600069B RID: 1691 RVA: 0x0000A1F0 File Offset: 0x000083F0
		// (set) Token: 0x0600069C RID: 1692 RVA: 0x0000A1F8 File Offset: 0x000083F8
		public float outTangent
		{
			get
			{
				return this.m_OutTangent;
			}
			set
			{
				this.m_OutTangent = value;
			}
		}

		// Token: 0x17000191 RID: 401
		// (get) Token: 0x0600069D RID: 1693 RVA: 0x0000A204 File Offset: 0x00008404
		// (set) Token: 0x0600069E RID: 1694 RVA: 0x0000A208 File Offset: 0x00008408
		public int tangentMode
		{
			get
			{
				return 0;
			}
			set
			{
			}
		}

		// Token: 0x0400010E RID: 270
		private float m_Time;

		// Token: 0x0400010F RID: 271
		private float m_Value;

		// Token: 0x04000110 RID: 272
		private float m_InTangent;

		// Token: 0x04000111 RID: 273
		private float m_OutTangent;
	}
}
