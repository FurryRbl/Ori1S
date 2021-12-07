using System;
using System.Runtime.InteropServices;

namespace ManagedSteam.SteamTypes
{
	// Token: 0x02000090 RID: 144
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct NetListenSocketHandle : IEquatable<NetListenSocketHandle>
	{
		// Token: 0x06000462 RID: 1122 RVA: 0x000081CB File Offset: 0x000063CB
		public NetListenSocketHandle(uint value)
		{
			this.handle = value;
		}

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x06000463 RID: 1123 RVA: 0x000081D4 File Offset: 0x000063D4
		public uint AsUInt32
		{
			get
			{
				return this.handle;
			}
		}

		// Token: 0x06000464 RID: 1124 RVA: 0x000081DC File Offset: 0x000063DC
		public static bool operator ==(NetListenSocketHandle x, NetListenSocketHandle y)
		{
			return x.handle == y.handle;
		}

		// Token: 0x06000465 RID: 1125 RVA: 0x000081EE File Offset: 0x000063EE
		public static bool operator !=(NetListenSocketHandle x, NetListenSocketHandle y)
		{
			return x.handle != y.handle;
		}

		// Token: 0x06000466 RID: 1126 RVA: 0x00008203 File Offset: 0x00006403
		public bool Equals(NetListenSocketHandle other)
		{
			return this == other;
		}

		// Token: 0x06000467 RID: 1127 RVA: 0x00008211 File Offset: 0x00006411
		public override bool Equals(object obj)
		{
			return obj is NetListenSocketHandle && this == (NetListenSocketHandle)obj;
		}

		// Token: 0x06000468 RID: 1128 RVA: 0x0000822E File Offset: 0x0000642E
		public override int GetHashCode()
		{
			return this.handle.GetHashCode();
		}

		// Token: 0x06000469 RID: 1129 RVA: 0x0000823B File Offset: 0x0000643B
		public override string ToString()
		{
			return this.handle.ToString(Steam.Instance.Culture);
		}

		// Token: 0x0400029F RID: 671
		private uint handle;
	}
}
