using System;
using System.Collections.Generic;

namespace UnityEngine.Assertions.Comparers
{
	// Token: 0x02000325 RID: 805
	public class FloatComparer : IEqualityComparer<float>
	{
		// Token: 0x060027D2 RID: 10194 RVA: 0x0003903C File Offset: 0x0003723C
		public FloatComparer() : this(1E-05f, false)
		{
		}

		// Token: 0x060027D3 RID: 10195 RVA: 0x0003904C File Offset: 0x0003724C
		public FloatComparer(bool relative) : this(1E-05f, relative)
		{
		}

		// Token: 0x060027D4 RID: 10196 RVA: 0x0003905C File Offset: 0x0003725C
		public FloatComparer(float error) : this(error, false)
		{
		}

		// Token: 0x060027D5 RID: 10197 RVA: 0x00039068 File Offset: 0x00037268
		public FloatComparer(float error, bool relative)
		{
			this.m_Error = error;
			this.m_Relative = relative;
		}

		// Token: 0x060027D7 RID: 10199 RVA: 0x00039094 File Offset: 0x00037294
		public bool Equals(float a, float b)
		{
			return (!this.m_Relative) ? FloatComparer.AreEqual(a, b, this.m_Error) : FloatComparer.AreEqualRelative(a, b, this.m_Error);
		}

		// Token: 0x060027D8 RID: 10200 RVA: 0x000390CC File Offset: 0x000372CC
		public int GetHashCode(float obj)
		{
			return base.GetHashCode();
		}

		// Token: 0x060027D9 RID: 10201 RVA: 0x000390D4 File Offset: 0x000372D4
		public static bool AreEqual(float expected, float actual, float error)
		{
			return Math.Abs(actual - expected) <= error;
		}

		// Token: 0x060027DA RID: 10202 RVA: 0x000390E4 File Offset: 0x000372E4
		public static bool AreEqualRelative(float expected, float actual, float error)
		{
			if (expected == actual)
			{
				return true;
			}
			float num = Math.Abs(expected);
			float num2 = Math.Abs(actual);
			float num3 = Math.Abs((actual - expected) / ((num <= num2) ? num2 : num));
			return num3 <= error;
		}

		// Token: 0x04000C49 RID: 3145
		public const float kEpsilon = 1E-05f;

		// Token: 0x04000C4A RID: 3146
		private readonly float m_Error;

		// Token: 0x04000C4B RID: 3147
		private readonly bool m_Relative;

		// Token: 0x04000C4C RID: 3148
		public static readonly FloatComparer s_ComparerWithDefaultTolerance = new FloatComparer(1E-05f);
	}
}
