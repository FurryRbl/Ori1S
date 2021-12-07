using System;
using System.Runtime.InteropServices;

namespace ManagedSteam.SteamTypes
{
	// Token: 0x020000F2 RID: 242
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct PublishedFileId : IEquatable<PublishedFileId>
	{
		// Token: 0x06000675 RID: 1653 RVA: 0x00009AF9 File Offset: 0x00007CF9
		public PublishedFileId(ulong value)
		{
			this.handle = value;
		}

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x06000676 RID: 1654 RVA: 0x00009B02 File Offset: 0x00007D02
		public ulong AsUInt64
		{
			get
			{
				return this.handle;
			}
		}

		// Token: 0x06000677 RID: 1655 RVA: 0x00009B0A File Offset: 0x00007D0A
		public static bool operator ==(PublishedFileId x, PublishedFileId y)
		{
			return x.handle == y.handle;
		}

		// Token: 0x06000678 RID: 1656 RVA: 0x00009B1C File Offset: 0x00007D1C
		public static bool operator !=(PublishedFileId x, PublishedFileId y)
		{
			return x.handle != y.handle;
		}

		// Token: 0x06000679 RID: 1657 RVA: 0x00009B31 File Offset: 0x00007D31
		public bool Equals(PublishedFileId other)
		{
			return this == other;
		}

		// Token: 0x0600067A RID: 1658 RVA: 0x00009B3F File Offset: 0x00007D3F
		public override bool Equals(object obj)
		{
			return obj is PublishedFileId && this == (PublishedFileId)obj;
		}

		// Token: 0x0600067B RID: 1659 RVA: 0x00009B5C File Offset: 0x00007D5C
		public override int GetHashCode()
		{
			return this.handle.GetHashCode();
		}

		// Token: 0x0600067C RID: 1660 RVA: 0x00009B69 File Offset: 0x00007D69
		public override string ToString()
		{
			return this.handle.ToString(Steam.Instance.Culture);
		}

		// Token: 0x04000406 RID: 1030
		private ulong handle;
	}
}
