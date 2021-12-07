using System;
using System.Runtime.InteropServices;

namespace ManagedSteam.SteamTypes
{
	// Token: 0x020000AA RID: 170
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct PublishedFileUpdateHandle : IEquatable<PublishedFileUpdateHandle>
	{
		// Token: 0x0600052E RID: 1326 RVA: 0x00009024 File Offset: 0x00007224
		public PublishedFileUpdateHandle(ulong value)
		{
			this.handle = value;
		}

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x0600052F RID: 1327 RVA: 0x0000902D File Offset: 0x0000722D
		public ulong AsUInt64
		{
			get
			{
				return this.handle;
			}
		}

		// Token: 0x06000530 RID: 1328 RVA: 0x00009035 File Offset: 0x00007235
		public static bool operator ==(PublishedFileUpdateHandle x, PublishedFileUpdateHandle y)
		{
			return x.handle == y.handle;
		}

		// Token: 0x06000531 RID: 1329 RVA: 0x00009047 File Offset: 0x00007247
		public static bool operator !=(PublishedFileUpdateHandle x, PublishedFileUpdateHandle y)
		{
			return x.handle != y.handle;
		}

		// Token: 0x06000532 RID: 1330 RVA: 0x0000905C File Offset: 0x0000725C
		public bool Equals(PublishedFileUpdateHandle other)
		{
			return this == other;
		}

		// Token: 0x06000533 RID: 1331 RVA: 0x0000906A File Offset: 0x0000726A
		public override bool Equals(object obj)
		{
			return obj is PublishedFileUpdateHandle && this == (PublishedFileUpdateHandle)obj;
		}

		// Token: 0x06000534 RID: 1332 RVA: 0x00009087 File Offset: 0x00007287
		public override int GetHashCode()
		{
			return this.handle.GetHashCode();
		}

		// Token: 0x06000535 RID: 1333 RVA: 0x00009094 File Offset: 0x00007294
		public override string ToString()
		{
			return this.handle.ToString(Steam.Instance.Culture);
		}

		// Token: 0x040002EB RID: 747
		public static readonly PublishedFileUpdateHandle Invalid = new PublishedFileUpdateHandle(ulong.MaxValue);

		// Token: 0x040002EC RID: 748
		private ulong handle;
	}
}
