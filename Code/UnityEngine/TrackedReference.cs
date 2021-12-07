using System;
using System.Runtime.InteropServices;

namespace UnityEngine
{
	// Token: 0x0200030B RID: 779
	[StructLayout(LayoutKind.Sequential)]
	public class TrackedReference
	{
		// Token: 0x06002712 RID: 10002 RVA: 0x00036F24 File Offset: 0x00035124
		protected TrackedReference()
		{
		}

		// Token: 0x06002713 RID: 10003 RVA: 0x00036F2C File Offset: 0x0003512C
		public override bool Equals(object o)
		{
			return o as TrackedReference == this;
		}

		// Token: 0x06002714 RID: 10004 RVA: 0x00036F3C File Offset: 0x0003513C
		public override int GetHashCode()
		{
			return (int)this.m_Ptr;
		}

		// Token: 0x06002715 RID: 10005 RVA: 0x00036F4C File Offset: 0x0003514C
		public static bool operator ==(TrackedReference x, TrackedReference y)
		{
			if (y == null && x == null)
			{
				return true;
			}
			if (y == null)
			{
				return x.m_Ptr == IntPtr.Zero;
			}
			if (x == null)
			{
				return y.m_Ptr == IntPtr.Zero;
			}
			return x.m_Ptr == y.m_Ptr;
		}

		// Token: 0x06002716 RID: 10006 RVA: 0x00036FAC File Offset: 0x000351AC
		public static bool operator !=(TrackedReference x, TrackedReference y)
		{
			return !(x == y);
		}

		// Token: 0x06002717 RID: 10007 RVA: 0x00036FB8 File Offset: 0x000351B8
		public static implicit operator bool(TrackedReference exists)
		{
			return exists != null;
		}

		// Token: 0x04000C0C RID: 3084
		internal IntPtr m_Ptr;
	}
}
