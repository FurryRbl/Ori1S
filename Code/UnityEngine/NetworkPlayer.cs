using System;
using System.Runtime.CompilerServices;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000073 RID: 115
	[RequiredByNativeCode]
	public struct NetworkPlayer
	{
		// Token: 0x060006BB RID: 1723 RVA: 0x0000A36C File Offset: 0x0000856C
		public NetworkPlayer(string ip, int port)
		{
			Debug.LogError("Not yet implemented");
			this.index = 0;
		}

		// Token: 0x060006BC RID: 1724
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern string Internal_GetIPAddress(int index);

		// Token: 0x060006BD RID: 1725
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int Internal_GetPort(int index);

		// Token: 0x060006BE RID: 1726
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern string Internal_GetExternalIP();

		// Token: 0x060006BF RID: 1727
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int Internal_GetExternalPort();

		// Token: 0x060006C0 RID: 1728
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern string Internal_GetLocalIP();

		// Token: 0x060006C1 RID: 1729
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int Internal_GetLocalPort();

		// Token: 0x060006C2 RID: 1730
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int Internal_GetPlayerIndex();

		// Token: 0x060006C3 RID: 1731
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern string Internal_GetGUID(int index);

		// Token: 0x060006C4 RID: 1732
		[WrapperlessIcall]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern string Internal_GetLocalGUID();

		// Token: 0x060006C5 RID: 1733 RVA: 0x0000A380 File Offset: 0x00008580
		public override int GetHashCode()
		{
			return this.index.GetHashCode();
		}

		// Token: 0x060006C6 RID: 1734 RVA: 0x0000A390 File Offset: 0x00008590
		public override bool Equals(object other)
		{
			return other is NetworkPlayer && ((NetworkPlayer)other).index == this.index;
		}

		// Token: 0x17000197 RID: 407
		// (get) Token: 0x060006C7 RID: 1735 RVA: 0x0000A3C0 File Offset: 0x000085C0
		public string ipAddress
		{
			get
			{
				if (this.index == NetworkPlayer.Internal_GetPlayerIndex())
				{
					return NetworkPlayer.Internal_GetLocalIP();
				}
				return NetworkPlayer.Internal_GetIPAddress(this.index);
			}
		}

		// Token: 0x17000198 RID: 408
		// (get) Token: 0x060006C8 RID: 1736 RVA: 0x0000A3E4 File Offset: 0x000085E4
		public int port
		{
			get
			{
				if (this.index == NetworkPlayer.Internal_GetPlayerIndex())
				{
					return NetworkPlayer.Internal_GetLocalPort();
				}
				return NetworkPlayer.Internal_GetPort(this.index);
			}
		}

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x060006C9 RID: 1737 RVA: 0x0000A408 File Offset: 0x00008608
		public string guid
		{
			get
			{
				if (this.index == NetworkPlayer.Internal_GetPlayerIndex())
				{
					return NetworkPlayer.Internal_GetLocalGUID();
				}
				return NetworkPlayer.Internal_GetGUID(this.index);
			}
		}

		// Token: 0x060006CA RID: 1738 RVA: 0x0000A42C File Offset: 0x0000862C
		public override string ToString()
		{
			return this.index.ToString();
		}

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x060006CB RID: 1739 RVA: 0x0000A43C File Offset: 0x0000863C
		public string externalIP
		{
			get
			{
				return NetworkPlayer.Internal_GetExternalIP();
			}
		}

		// Token: 0x1700019B RID: 411
		// (get) Token: 0x060006CC RID: 1740 RVA: 0x0000A444 File Offset: 0x00008644
		public int externalPort
		{
			get
			{
				return NetworkPlayer.Internal_GetExternalPort();
			}
		}

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x060006CD RID: 1741 RVA: 0x0000A44C File Offset: 0x0000864C
		internal static NetworkPlayer unassigned
		{
			get
			{
				NetworkPlayer result;
				result.index = -1;
				return result;
			}
		}

		// Token: 0x060006CE RID: 1742 RVA: 0x0000A464 File Offset: 0x00008664
		public static bool operator ==(NetworkPlayer lhs, NetworkPlayer rhs)
		{
			return lhs.index == rhs.index;
		}

		// Token: 0x060006CF RID: 1743 RVA: 0x0000A478 File Offset: 0x00008678
		public static bool operator !=(NetworkPlayer lhs, NetworkPlayer rhs)
		{
			return lhs.index != rhs.index;
		}

		// Token: 0x0400014B RID: 331
		internal int index;
	}
}
