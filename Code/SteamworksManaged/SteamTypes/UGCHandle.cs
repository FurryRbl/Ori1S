using System;
using System.Runtime.InteropServices;

namespace ManagedSteam.SteamTypes
{
	// Token: 0x02000083 RID: 131
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct UGCHandle : IEquatable<UGCHandle>
	{
		// Token: 0x0600042C RID: 1068 RVA: 0x00007C44 File Offset: 0x00005E44
		public UGCHandle(ulong value)
		{
			this.handle = value;
		}

		// Token: 0x170000BD RID: 189
		// (get) Token: 0x0600042D RID: 1069 RVA: 0x00007C4D File Offset: 0x00005E4D
		public ulong AsUInt64
		{
			get
			{
				return this.handle;
			}
		}

		// Token: 0x0600042E RID: 1070 RVA: 0x00007C55 File Offset: 0x00005E55
		public static bool operator ==(UGCHandle x, UGCHandle y)
		{
			return x.handle == y.handle;
		}

		// Token: 0x0600042F RID: 1071 RVA: 0x00007C67 File Offset: 0x00005E67
		public static bool operator !=(UGCHandle x, UGCHandle y)
		{
			return x.handle != y.handle;
		}

		// Token: 0x06000430 RID: 1072 RVA: 0x00007C7C File Offset: 0x00005E7C
		public bool Equals(UGCHandle other)
		{
			return this == other;
		}

		// Token: 0x06000431 RID: 1073 RVA: 0x00007C8A File Offset: 0x00005E8A
		public override bool Equals(object obj)
		{
			return obj is UGCHandle && this == (UGCHandle)obj;
		}

		// Token: 0x06000432 RID: 1074 RVA: 0x00007CA7 File Offset: 0x00005EA7
		public override int GetHashCode()
		{
			return this.handle.GetHashCode();
		}

		// Token: 0x06000433 RID: 1075 RVA: 0x00007CB4 File Offset: 0x00005EB4
		public override string ToString()
		{
			return this.handle.ToString(Steam.Instance.Culture);
		}

		// Token: 0x04000215 RID: 533
		public static readonly UGCHandle Invalid = new UGCHandle(ulong.MaxValue);

		// Token: 0x04000216 RID: 534
		private ulong handle;
	}
}
