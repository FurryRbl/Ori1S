using System;
using System.Runtime.InteropServices;

namespace ManagedSteam.SteamTypes
{
	// Token: 0x0200012A RID: 298
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct IconHandle : IEquatable<IconHandle>
	{
		// Token: 0x06000A25 RID: 2597 RVA: 0x0000BEBB File Offset: 0x0000A0BB
		public IconHandle(int value)
		{
			this.handle = value;
		}

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x06000A26 RID: 2598 RVA: 0x0000BEC4 File Offset: 0x0000A0C4
		public int AsInt32
		{
			get
			{
				return this.handle;
			}
		}

		// Token: 0x06000A27 RID: 2599 RVA: 0x0000BECC File Offset: 0x0000A0CC
		public static bool operator ==(IconHandle x, IconHandle y)
		{
			return x.handle == y.handle;
		}

		// Token: 0x06000A28 RID: 2600 RVA: 0x0000BEDE File Offset: 0x0000A0DE
		public static bool operator !=(IconHandle x, IconHandle y)
		{
			return x.handle != y.handle;
		}

		// Token: 0x06000A29 RID: 2601 RVA: 0x0000BEF3 File Offset: 0x0000A0F3
		public bool Equals(IconHandle other)
		{
			return this == other;
		}

		// Token: 0x06000A2A RID: 2602 RVA: 0x0000BF01 File Offset: 0x0000A101
		public override bool Equals(object obj)
		{
			return obj is IconHandle && this == (IconHandle)obj;
		}

		// Token: 0x06000A2B RID: 2603 RVA: 0x0000BF1E File Offset: 0x0000A11E
		public override int GetHashCode()
		{
			return this.handle.GetHashCode();
		}

		// Token: 0x06000A2C RID: 2604 RVA: 0x0000BF2B File Offset: 0x0000A12B
		public override string ToString()
		{
			return this.handle.ToString(Steam.Instance.Culture);
		}

		// Token: 0x04000518 RID: 1304
		private int handle;
	}
}
