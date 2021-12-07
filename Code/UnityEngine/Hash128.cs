using System;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x020000A1 RID: 161
	public struct Hash128
	{
		// Token: 0x0600092D RID: 2349 RVA: 0x0000CD18 File Offset: 0x0000AF18
		public Hash128(uint u32_0, uint u32_1, uint u32_2, uint u32_3)
		{
			this.m_u32_0 = u32_0;
			this.m_u32_1 = u32_1;
			this.m_u32_2 = u32_2;
			this.m_u32_3 = u32_3;
		}

		// Token: 0x17000216 RID: 534
		// (get) Token: 0x0600092E RID: 2350 RVA: 0x0000CD38 File Offset: 0x0000AF38
		public bool isValid
		{
			get
			{
				return this.m_u32_0 != 0U || this.m_u32_1 != 0U || this.m_u32_2 != 0U || this.m_u32_3 != 0U;
			}
		}

		// Token: 0x0600092F RID: 2351 RVA: 0x0000CD78 File Offset: 0x0000AF78
		public override string ToString()
		{
			return Hash128.Internal_Hash128ToString(this.m_u32_0, this.m_u32_1, this.m_u32_2, this.m_u32_3);
		}

		// Token: 0x06000930 RID: 2352 RVA: 0x0000CD98 File Offset: 0x0000AF98
		public static Hash128 Parse(string hashString)
		{
			Hash128 result;
			Hash128.INTERNAL_CALL_Parse(hashString, out result);
			return result;
		}

		// Token: 0x06000931 RID: 2353
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_Parse(string hashString, out Hash128 value);

		// Token: 0x06000932 RID: 2354
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern string Internal_Hash128ToString(uint d0, uint d1, uint d2, uint d3);

		// Token: 0x040001EE RID: 494
		private uint m_u32_0;

		// Token: 0x040001EF RID: 495
		private uint m_u32_1;

		// Token: 0x040001F0 RID: 496
		private uint m_u32_2;

		// Token: 0x040001F1 RID: 497
		private uint m_u32_3;
	}
}
