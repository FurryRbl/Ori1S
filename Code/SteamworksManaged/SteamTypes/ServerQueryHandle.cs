using System;
using System.Runtime.InteropServices;

namespace ManagedSteam.SteamTypes
{
	// Token: 0x02000153 RID: 339
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct ServerQueryHandle : IEquatable<ServerQueryHandle>
	{
		// Token: 0x06000BAC RID: 2988 RVA: 0x0000FC00 File Offset: 0x0000DE00
		public ServerQueryHandle(int value)
		{
			this.handle = value;
		}

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x06000BAD RID: 2989 RVA: 0x0000FC09 File Offset: 0x0000DE09
		public int AsInt32
		{
			get
			{
				return this.handle;
			}
		}

		// Token: 0x06000BAE RID: 2990 RVA: 0x0000FC11 File Offset: 0x0000DE11
		public static bool operator ==(ServerQueryHandle x, ServerQueryHandle y)
		{
			return x.handle == y.handle;
		}

		// Token: 0x06000BAF RID: 2991 RVA: 0x0000FC23 File Offset: 0x0000DE23
		public static bool operator !=(ServerQueryHandle x, ServerQueryHandle y)
		{
			return x.handle != y.handle;
		}

		// Token: 0x06000BB0 RID: 2992 RVA: 0x0000FC38 File Offset: 0x0000DE38
		public bool Equals(ServerQueryHandle other)
		{
			return this == other;
		}

		// Token: 0x06000BB1 RID: 2993 RVA: 0x0000FC46 File Offset: 0x0000DE46
		public override bool Equals(object obj)
		{
			return obj is ServerQueryHandle && this == (ServerQueryHandle)obj;
		}

		// Token: 0x06000BB2 RID: 2994 RVA: 0x0000FC63 File Offset: 0x0000DE63
		public override int GetHashCode()
		{
			return this.handle.GetHashCode();
		}

		// Token: 0x06000BB3 RID: 2995 RVA: 0x0000FC70 File Offset: 0x0000DE70
		public override string ToString()
		{
			return this.handle.ToString(Steam.Instance.Culture);
		}

		// Token: 0x0400060B RID: 1547
		public static readonly ServerQueryHandle Invalid = new ServerQueryHandle(-1);

		// Token: 0x0400060C RID: 1548
		private int handle;
	}
}
