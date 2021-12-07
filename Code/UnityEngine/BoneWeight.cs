using System;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000025 RID: 37
	[UsedByNativeCode]
	public struct BoneWeight
	{
		// Token: 0x1700008B RID: 139
		// (get) Token: 0x0600019D RID: 413 RVA: 0x00002C7C File Offset: 0x00000E7C
		// (set) Token: 0x0600019E RID: 414 RVA: 0x00002C84 File Offset: 0x00000E84
		public float weight0
		{
			get
			{
				return this.m_Weight0;
			}
			set
			{
				this.m_Weight0 = value;
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x0600019F RID: 415 RVA: 0x00002C90 File Offset: 0x00000E90
		// (set) Token: 0x060001A0 RID: 416 RVA: 0x00002C98 File Offset: 0x00000E98
		public float weight1
		{
			get
			{
				return this.m_Weight1;
			}
			set
			{
				this.m_Weight1 = value;
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x060001A1 RID: 417 RVA: 0x00002CA4 File Offset: 0x00000EA4
		// (set) Token: 0x060001A2 RID: 418 RVA: 0x00002CAC File Offset: 0x00000EAC
		public float weight2
		{
			get
			{
				return this.m_Weight2;
			}
			set
			{
				this.m_Weight2 = value;
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x060001A3 RID: 419 RVA: 0x00002CB8 File Offset: 0x00000EB8
		// (set) Token: 0x060001A4 RID: 420 RVA: 0x00002CC0 File Offset: 0x00000EC0
		public float weight3
		{
			get
			{
				return this.m_Weight3;
			}
			set
			{
				this.m_Weight3 = value;
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x060001A5 RID: 421 RVA: 0x00002CCC File Offset: 0x00000ECC
		// (set) Token: 0x060001A6 RID: 422 RVA: 0x00002CD4 File Offset: 0x00000ED4
		public int boneIndex0
		{
			get
			{
				return this.m_BoneIndex0;
			}
			set
			{
				this.m_BoneIndex0 = value;
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x060001A7 RID: 423 RVA: 0x00002CE0 File Offset: 0x00000EE0
		// (set) Token: 0x060001A8 RID: 424 RVA: 0x00002CE8 File Offset: 0x00000EE8
		public int boneIndex1
		{
			get
			{
				return this.m_BoneIndex1;
			}
			set
			{
				this.m_BoneIndex1 = value;
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x060001A9 RID: 425 RVA: 0x00002CF4 File Offset: 0x00000EF4
		// (set) Token: 0x060001AA RID: 426 RVA: 0x00002CFC File Offset: 0x00000EFC
		public int boneIndex2
		{
			get
			{
				return this.m_BoneIndex2;
			}
			set
			{
				this.m_BoneIndex2 = value;
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x060001AB RID: 427 RVA: 0x00002D08 File Offset: 0x00000F08
		// (set) Token: 0x060001AC RID: 428 RVA: 0x00002D10 File Offset: 0x00000F10
		public int boneIndex3
		{
			get
			{
				return this.m_BoneIndex3;
			}
			set
			{
				this.m_BoneIndex3 = value;
			}
		}

		// Token: 0x060001AD RID: 429 RVA: 0x00002D1C File Offset: 0x00000F1C
		public override int GetHashCode()
		{
			return this.boneIndex0.GetHashCode() ^ this.boneIndex1.GetHashCode() << 2 ^ this.boneIndex2.GetHashCode() >> 2 ^ this.boneIndex3.GetHashCode() >> 1 ^ this.weight0.GetHashCode() << 5 ^ this.weight1.GetHashCode() << 4 ^ this.weight2.GetHashCode() >> 4 ^ this.weight3.GetHashCode() >> 3;
		}

		// Token: 0x060001AE RID: 430 RVA: 0x00002DB4 File Offset: 0x00000FB4
		public override bool Equals(object other)
		{
			if (!(other is BoneWeight))
			{
				return false;
			}
			BoneWeight boneWeight = (BoneWeight)other;
			bool result;
			if (this.boneIndex0.Equals(boneWeight.boneIndex0) && this.boneIndex1.Equals(boneWeight.boneIndex1) && this.boneIndex2.Equals(boneWeight.boneIndex2) && this.boneIndex3.Equals(boneWeight.boneIndex3))
			{
				Vector4 vector = new Vector4(this.weight0, this.weight1, this.weight2, this.weight3);
				result = vector.Equals(new Vector4(boneWeight.weight0, boneWeight.weight1, boneWeight.weight2, boneWeight.weight3));
			}
			else
			{
				result = false;
			}
			return result;
		}

		// Token: 0x060001AF RID: 431 RVA: 0x00002E90 File Offset: 0x00001090
		public static bool operator ==(BoneWeight lhs, BoneWeight rhs)
		{
			return lhs.boneIndex0 == rhs.boneIndex0 && lhs.boneIndex1 == rhs.boneIndex1 && lhs.boneIndex2 == rhs.boneIndex2 && lhs.boneIndex3 == rhs.boneIndex3 && new Vector4(lhs.weight0, lhs.weight1, lhs.weight2, lhs.weight3) == new Vector4(rhs.weight0, rhs.weight1, rhs.weight2, rhs.weight3);
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x00002F34 File Offset: 0x00001134
		public static bool operator !=(BoneWeight lhs, BoneWeight rhs)
		{
			return !(lhs == rhs);
		}

		// Token: 0x04000083 RID: 131
		private float m_Weight0;

		// Token: 0x04000084 RID: 132
		private float m_Weight1;

		// Token: 0x04000085 RID: 133
		private float m_Weight2;

		// Token: 0x04000086 RID: 134
		private float m_Weight3;

		// Token: 0x04000087 RID: 135
		private int m_BoneIndex0;

		// Token: 0x04000088 RID: 136
		private int m_BoneIndex1;

		// Token: 0x04000089 RID: 137
		private int m_BoneIndex2;

		// Token: 0x0400008A RID: 138
		private int m_BoneIndex3;
	}
}
