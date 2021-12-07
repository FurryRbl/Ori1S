using System;
using System.Runtime.CompilerServices;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000074 RID: 116
	[RequiredByNativeCode]
	public struct NetworkViewID
	{
		// Token: 0x1700019D RID: 413
		// (get) Token: 0x060006D0 RID: 1744 RVA: 0x0000A490 File Offset: 0x00008690
		public static NetworkViewID unassigned
		{
			get
			{
				NetworkViewID result;
				NetworkViewID.INTERNAL_get_unassigned(out result);
				return result;
			}
		}

		// Token: 0x060006D1 RID: 1745
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_get_unassigned(out NetworkViewID value);

		// Token: 0x060006D2 RID: 1746 RVA: 0x0000A4A8 File Offset: 0x000086A8
		internal static bool Internal_IsMine(NetworkViewID value)
		{
			return NetworkViewID.INTERNAL_CALL_Internal_IsMine(ref value);
		}

		// Token: 0x060006D3 RID: 1747
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool INTERNAL_CALL_Internal_IsMine(ref NetworkViewID value);

		// Token: 0x060006D4 RID: 1748 RVA: 0x0000A4B4 File Offset: 0x000086B4
		internal static void Internal_GetOwner(NetworkViewID value, out NetworkPlayer player)
		{
			NetworkViewID.INTERNAL_CALL_Internal_GetOwner(ref value, out player);
		}

		// Token: 0x060006D5 RID: 1749
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void INTERNAL_CALL_Internal_GetOwner(ref NetworkViewID value, out NetworkPlayer player);

		// Token: 0x060006D6 RID: 1750 RVA: 0x0000A4C0 File Offset: 0x000086C0
		internal static string Internal_GetString(NetworkViewID value)
		{
			return NetworkViewID.INTERNAL_CALL_Internal_GetString(ref value);
		}

		// Token: 0x060006D7 RID: 1751
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern string INTERNAL_CALL_Internal_GetString(ref NetworkViewID value);

		// Token: 0x060006D8 RID: 1752 RVA: 0x0000A4CC File Offset: 0x000086CC
		internal static bool Internal_Compare(NetworkViewID lhs, NetworkViewID rhs)
		{
			return NetworkViewID.INTERNAL_CALL_Internal_Compare(ref lhs, ref rhs);
		}

		// Token: 0x060006D9 RID: 1753
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool INTERNAL_CALL_Internal_Compare(ref NetworkViewID lhs, ref NetworkViewID rhs);

		// Token: 0x060006DA RID: 1754 RVA: 0x0000A4D8 File Offset: 0x000086D8
		public override int GetHashCode()
		{
			return this.a ^ this.b ^ this.c;
		}

		// Token: 0x060006DB RID: 1755 RVA: 0x0000A4F0 File Offset: 0x000086F0
		public override bool Equals(object other)
		{
			if (!(other is NetworkViewID))
			{
				return false;
			}
			NetworkViewID rhs = (NetworkViewID)other;
			return NetworkViewID.Internal_Compare(this, rhs);
		}

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x060006DC RID: 1756 RVA: 0x0000A520 File Offset: 0x00008720
		public bool isMine
		{
			get
			{
				return NetworkViewID.Internal_IsMine(this);
			}
		}

		// Token: 0x1700019F RID: 415
		// (get) Token: 0x060006DD RID: 1757 RVA: 0x0000A530 File Offset: 0x00008730
		public NetworkPlayer owner
		{
			get
			{
				NetworkPlayer result;
				NetworkViewID.Internal_GetOwner(this, out result);
				return result;
			}
		}

		// Token: 0x060006DE RID: 1758 RVA: 0x0000A54C File Offset: 0x0000874C
		public override string ToString()
		{
			return NetworkViewID.Internal_GetString(this);
		}

		// Token: 0x060006DF RID: 1759 RVA: 0x0000A55C File Offset: 0x0000875C
		public static bool operator ==(NetworkViewID lhs, NetworkViewID rhs)
		{
			return NetworkViewID.Internal_Compare(lhs, rhs);
		}

		// Token: 0x060006E0 RID: 1760 RVA: 0x0000A568 File Offset: 0x00008768
		public static bool operator !=(NetworkViewID lhs, NetworkViewID rhs)
		{
			return !NetworkViewID.Internal_Compare(lhs, rhs);
		}

		// Token: 0x0400014C RID: 332
		private int a;

		// Token: 0x0400014D RID: 333
		private int b;

		// Token: 0x0400014E RID: 334
		private int c;
	}
}
